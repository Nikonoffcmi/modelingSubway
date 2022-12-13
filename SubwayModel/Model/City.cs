using SubwayModel.Model.Passengers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model
{
    public class City
    {
        private List<Subway> _subways;
        private List<Train> _trains;

        public City (List<Subway> subways)
        {
            _subways = subways;
            _trains = new List<Train> ();
        }

        public void Simulation()
        {
            Statistics.averageSubwayWaiting.Clear();
            Statistics.ratioSubwayPassengers.Clear();

            for (int currTime = 0; currTime < Settings.simulationTime; currTime ++)
            {
                LeftSide();
                Statistics.ratioSubwayPassengers.RemoveAt(Statistics.ratioSubwayPassengers.Count - 1);
            }

            foreach (var subway in _subways)
            {
                subway.CalculateStatistics();
            }

            Statistics.averageSubwayWaiting.RemoveAt(_subways.Count - 1);
            if (Statistics.averageSubwayWaiting.Count > 0)
                Statistics.averageWaiting = (int)Math.Round(Statistics.averageSubwayWaiting.Average());
            else
                Statistics.averageWaiting = 0;

            if (Statistics.ratioSubwayPassengers.Count > 0)
                Statistics.ratioPassengers = Math.Round(Statistics.ratioSubwayPassengers.Average());
            else
                Statistics.ratioPassengers = 0;
        }

        private void LeftSide()
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
                Statistics.ratioSubwayPassengers.Add(_subways[i].NotPlacedTrainPassengers);
                _subways[i].NotPlacedTrainPassengers = 0;
            }            
            _trains.Clear();
        }
    }
}
