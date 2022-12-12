using SubwayModel.Model.Passengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model
{
    public class City
    {
        private List<Subway> _subways;
        private List<TrainLeft> _leftTrains;
        private List<TrainRight> _rightTrains;

        public City (List<Subway> subways)
        {
            _subways = subways;
            _leftTrains = new List<TrainLeft> ();
            _rightTrains = new List<TrainRight> ();
        }

        public void Simulation()
        {
            Statistics.averageSubwayWaiting.Clear();
            Statistics.ratioSubwayPassengers.Clear();

            for (int currTime = 0; currTime < Settings.simulationTime * 60; currTime += Settings.averageTransmittanceTrains)
            {
                LeftSide();

                RightSide();
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

        public void AddSubway(Subway subway)
        {
            _subways.Add(subway);
        }

        public List<Subway> GetSubways()
        {
            var s = new List<Subway>(_subways);

            return s;
        }

        private void LeftSide()
        {
            for (int i = 0; i < _subways.Count; i++)
            {
                if (_leftTrains.Count == 0)
                {
                    for (int Minutes = 0; Minutes < 60; Minutes += Settings.averageTransmittanceTrains)
                    {
                        _leftTrains.Add(new TrainLeft(Settings.TrainsCapacity));
                    }
                }
                else
                {
                    foreach (var train in _leftTrains)
                        train.EnterSubway(_subways[i]);
                }

                _subways[i].PassengerEnter(_subways.Select(s => s.Name).ToList(), _subways[i].Name);

                _subways[i].Simulation(_leftTrains.ToList<Train>());
            }
            _leftTrains.Clear();
        }

        private void RightSide()
        {
            for (int i = 0; i < _subways.Count; i++)
            {
                if (_rightTrains.Count == 0)
                {
                    for (int Minutes = 0; Minutes < 60; Minutes += Settings.averageTransmittanceTrains)
                    {
                        _rightTrains.Add(new TrainRight(Settings.TrainsCapacity));
                    }
                }
                else
                {
                    foreach (var train in _rightTrains)
                        train.EnterSubway(_subways[i]);
                }

                _subways[i].Simulation(_rightTrains.ToList<Train>());
            }
            _rightTrains.Clear();
        }
    }
}
