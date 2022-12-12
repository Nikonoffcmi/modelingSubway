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

        public City ()
        {
            _subways = new List<Subway> ();
            _leftTrains = new List<TrainLeft> ();
            _rightTrains = new List<TrainRight> ();
        }

        public void Simulation()
        {
            State.averageSubwayWaiting.Clear();
            State.ratioSubwayPassengers.Clear();

            double time = 6 * 60 / State.simulationInterval;
            for (int currTime = 0; currTime < time; currTime++)
            {
                LeftSide();

                RightSide();
            }

            foreach (var subway in _subways)
            {
                subway.CalculateStatistics();
            }

            if (State.averageSubwayWaiting.Count > 0)
                State.averageWaiting = (int)Math.Round(State.averageSubwayWaiting.Average());
            else
                State.averageWaiting = 0;

            if (State.ratioSubwayPassengers.Count > 0)
                State.ratioPassengers = Math.Round(State.ratioSubwayPassengers.Average());
            else
                State.ratioPassengers = 0;
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
                    for (int Minutes = 0; Minutes < State.simulationInterval; Minutes += State.averageTransmittanceTrains)
                    {
                        _leftTrains.Add(new TrainLeft(State.TrainsCapacity));
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
                    for (int Minutes = 0; Minutes < State.simulationInterval; Minutes += State.averageTransmittanceTrains)
                    {
                        _rightTrains.Add(new TrainRight(State.TrainsCapacity));
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
