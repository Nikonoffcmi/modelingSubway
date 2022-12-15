using System;
using System.Collections.Generic;
using System.Linq;

namespace SubwayModel.Model
{
    public class City
    {
        private List<Subway> _subways;
        private List<Train> _trains;

        public City (List<Subway> subways)
        {
            if (subways == null)
                throw new ArgumentNullException(nameof(subways));
            if (subways.Count == 0)
                throw new ArgumentException(nameof(subways), "Нет доступных станций для перемещения");

            _subways = subways;
            _trains = new List<Train> ();
        }

        public void Simulation()
        {
            if (_subways.Count == 0)
                throw new ArgumentException(nameof(_subways), "Нет доступных станций для перемещения");

            for (int currTime = 0; currTime < Settings.simulationTime; currTime ++)
            {
                SimulationHour();
                Statistics.passengersWaitingTrains.Remove(_subways[_subways.Count-1].Name);
            }

            foreach (var subway in _subways)
            {
                subway.CalculateStatistics();
            }
        }

        private void SimulationHour()
        {
            for (int i = 0; i < _subways.Count; i++)
            {
                var SubwayNames = _subways.Select(s => s.Name).ToList();
                SubwayNames.RemoveRange(0, i + 1);
                if (_trains.Count == 0)
                {
                    for (int Minutes = 0; Minutes < 60; Minutes += Settings.averageTransmittanceTrains)
                    {
                        var train = new Train(Settings.TrainsCapacity);
                        _subways[i].PassengerEnter(SubwayNames);
                        _subways[i].Simulation(train);
                        _trains.Add(train);
                    }
                }
                else
                {
                    for (int j = 0; j < _trains.Count; j ++)
                    {
                        _trains[j].EnterSubway(_subways[i]);
                        if (i+1 != _subways.Count)
                            _subways[i].PassengerEnter(SubwayNames);
                        _subways[i].Simulation(_trains[j]);
                    }
                }

                if (Statistics.passengersWaitingTrains.ContainsKey(_subways[i].Name))
                    Statistics.passengersWaitingTrains[_subways[i].Name].Add(_subways[i].NotPlacedTrainPassengers);
                else
                {
                    var l = new List<int>(); l.Add(_subways[i].NotPlacedTrainPassengers);
                    Statistics.passengersWaitingTrains.Add(_subways[i].Name, l);
                }
                _subways[i].NotPlacedTrainPassengers = 0;
            }            
            _trains.Clear();
        }
    }
}
