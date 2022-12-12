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
            double time = 60 / State.simulationInterval;
            for (int currTime = 0; currTime < time; currTime++)
            {
                for (int i = 0; i < _subways.Count; i++)
                {
                    _subways[i].PassengerEnter(_subways.Select(s => s._name).ToList(), _subways[i]._name);
                    
                    var temp = _subways[i].SimulationLeftSide(_leftTrains);
                    _leftTrains.Clear();
                    _leftTrains.AddRange(temp);
                }
                _leftTrains.Clear();

                for (int i = _subways.Count - 1; i >= 0; i--)
                {

                    var temp = _subways[i].SimulationRightSide(_rightTrains);
                    _rightTrains.Clear();
                    _rightTrains.AddRange(temp);
                }
                _rightTrains.Clear();
            }

            foreach (var subway in _subways)
            {
                subway.passengersWaitEnter.ForEach(p => subway._gonePassengers++);
                subway.passengersWaitEnter.Clear();
                subway.passengersWaitTrain.ForEach(p => subway._gonePassengers++);
                State.ratioPassengers = subway._departedPassengers * 100 / (subway._departedPassengers + subway._gonePassengers);
                var l = new List<int>(subway.passengersWaiting.Count);
                subway.passengersWaiting.ForEach(p => l.Add(p.timeWaiting));
                if (l.Count > 0)
                    State.averageEnterWaiting = (int)Math.Round(l.Average());
                else
                    State.averageEnterWaiting = 0;
                subway.passengersWaiting.Clear();
            }
        }
    }
}
