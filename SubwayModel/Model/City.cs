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
        private List<Train> trains;

        public City (List<Subway> subways)
        {
            _subways = subways;
            trains = new List<Train> ();
        }

        public void Simulation()
        {
            Statistics.averageSubwayWaiting.Clear();
            Statistics.ratioSubwayPassengers.Clear();

            for (int currTime = 0; currTime < Settings.simulationTime * 60; currTime += Settings.simulationInterval)
            {
                
                LeftSide();

            }

            foreach (var subway in _subways)
            {
                subway.CalculateStatistics();
            }

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
                if (trains.Count == 0)
                {
                    for (int Minutes = 0; Minutes < Settings.simulationInterval; Minutes += Settings.averageTransmittanceTrains)
                    {
                        var train = new Train(Settings.TrainsCapacity);
                        _subways[i].PassengerEnter(_subways.Select(s => s.Name).ToList());
                        _subways[i].Simulation(train);
                        trains.Add(new Train(Settings.TrainsCapacity));
                    }
                }
                else
                {
                    for (int id = 0; id < trains.Count; id ++)
                    {
                        trains[id].EnterSubway(_subways[i]);
                        _subways[i].PassengerEnter(_subways.Select(s => s.Name).ToList());
                        _subways[i].Simulation(trains[id]);
                    }
                }
                Statistics.ratioSubwayPassengers.Add(_subways[i]._passengers);
                _subways[i]._passengers = 0;
            }
            trains.Clear();
        }
    }
}
