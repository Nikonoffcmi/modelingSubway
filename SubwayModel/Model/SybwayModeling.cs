using System;
using System.Collections.Generic;
using System.Linq;

namespace SubwayModel.Model
{
    public class SybwayModeling
    {
        private List<Subway> _subways;
        private List<Train> _trains;
        private IPassengerFactory _passengerFactory;

        public SybwayModeling (List<Subway> subways, IPassengerFactory passengerFactory)
        {
            if (subways == null)
                throw new ArgumentNullException(nameof(subways));
            if (subways.Count <= 1)
                throw new ArgumentException(nameof(subways), "Нет доступных станций для перемещения");

            _subways = subways;
            _trains = new List<Train> ();
            _passengerFactory = passengerFactory;
        }

        public void Simulation()
        {
            Statistics.DefaultStatistics();

            if (_subways.Count == 0)
                throw new ArgumentException(nameof(_subways), "Нет доступных станций для перемещения");

            for (int i = 0; i < Settings.numberRuns; i++)
            {
                for (int currTime = 0; currTime < Settings.simulationTime; currTime++)
                {
                    SimulationHour();
                    Statistics.passengersWaitingTrains.Remove(_subways[_subways.Count - 1].Name);
                }

                foreach (var subway in _subways)
                {
                    if (_subways[_subways.Count - 1].Name != subway.Name)
                        subway.CalculateStatistics();
                }
            }

            var list = new List<int>();
            foreach (var s in Settings.Subways)
                if (_subways[_subways.Count - 1].Name != s.Name)
                    list.AddRange(Statistics.averageSubwayWaitingTime[s.Name]);
            Statistics.averageSubwayWaitingTime.Add("Общая", list);
            Statistics.averageWaitingTime = (int)Math.Round(list.Average());

            var list2 = new List<int>();
            for (int i = 0; i < Settings.Subways.Count - 1; i++)
                    list2.AddRange(Statistics.passengersWaitingTrains[Settings.Subways[i].Name]);
            Statistics.passengersWaitingTrains.Add("Общая", list2);
            Statistics.averagePassengersWaitingTrains = (int)Math.Round(list2.Average());
        }

        private void SimulationHour()
        {
            var trainCapacity = Settings.vanCapacity * Settings.numberVan;
            for (int i = 0; i < _subways.Count; i++)
            {
                var SubwayNames = _subways.Select(s => s.Name).ToList();
                SubwayNames.RemoveRange(0, i + 1);
                if (_trains.Count == 0)
                {
                    for (int Minutes = 0; Minutes < 60; Minutes += Settings.averageTransmittanceTrains)
                    {
                        var train = new Train(trainCapacity);
                        _subways[i].PassengersEnter(SubwayNames, _passengerFactory);
                        _subways[i].PassengersGetOnTrain(train);
                        _trains.Add(train);
                    }
                }
                else
                {
                    for (int j = 0; j < _trains.Count; j ++)
                    {
                        _trains[j].EnterSubway(_subways[i]);
                        if (i+1 != _subways.Count)
                            _subways[i].PassengersEnter(SubwayNames, _passengerFactory);
                        _subways[i].PassengersGetOnTrain(_trains[j]);
                    }
                }
            }            
            _trains.Clear();
        }
    }
}
