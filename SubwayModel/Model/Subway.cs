using SubwayModel.Model.Passengers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model
{
    public class Subway
    {
        private string _name { get; }
        private int _freeSpace;
        private int _departedPassengers;
        private int _gonePassengers;
        private int _averageTransmittancePassengers;
        private List<Passenger> _passengersWaitTrain;
        private List<Passenger> _passengersWaitEnter;
        private List<TrainLeft> _leftTrains;
        private List<TrainRight> _rightTrains;

        public string Name => _name;
        public int FreeSpace => _freeSpace;

        public Subway (string name, int freeSpace, int averageTransmittancePassengers)
        {
            _name = name;
            _freeSpace = freeSpace;
            _departedPassengers = 0;
            _gonePassengers = 0;
            _averageTransmittancePassengers = averageTransmittancePassengers;
            _passengersWaitTrain = new List<Passenger>();
            _passengersWaitEnter = new List<Passenger>();
            _leftTrains = new List<TrainLeft>();
            _rightTrains = new List<TrainRight>();
        }

        public List<TrainLeft> SimulationLeftSide(List<TrainLeft> LeftTrains)
        {
            _leftTrains.Clear();
            if (LeftTrains.Count == 0)
            {
                for (int Minutes = 0; Minutes < State.simulationInterval; Minutes += State.averageTransmittanceTrains)
                {
                    _leftTrains.Add(new TrainLeft(State.TrainsCapacity).EnterSubway(this));
                }
            }
            else
            {
                int i = 0;
                for (int Minutes = 0; Minutes < State.simulationInterval; Minutes += State.averageTransmittanceTrains)
                {
                    LeftTrains[i].EnterSubway(this);
                    i++;
                }
            }

            foreach (var train in _leftTrains)
            {
                var temp = new List<Passenger>(_passengersWaitTrain);
                foreach (var passenger in temp)
                {
                    if (!passenger.TryEnterTrain(train))
                    {
                        if (passenger.TryLeaveSubway())
                            _passengersWaitTrain.Remove(passenger);
                    }
                    else
                        _passengersWaitTrain.Remove(passenger);

                }
            }

            return _leftTrains;
        }

        public List<TrainRight> SimulationRightSide(List<TrainRight> RightTrains)
        {
            this._rightTrains.Clear();
            if (RightTrains.Count == 0)
            {
                for (int Minutes = 0; Minutes < State.simulationInterval; Minutes += State.averageTransmittanceTrains)
                {
                    new TrainRight(State.TrainsCapacity).EnterSubway(this);
                }
            }
            else
            {
                int i = 0;
                for (int Minutes = 0; Minutes < State.simulationInterval; Minutes += State.averageTransmittanceTrains)
                {
                    RightTrains[i].EnterSubway(this);
                    i++;
                }
            }

            foreach (var train in _rightTrains)
            {
                var temp = new List<Passenger>(_passengersWaitTrain);
                foreach (var passenger in temp)
                {
                    if (!passenger.TryEnterTrain(train))
                    {
                        if (passenger.TryLeaveSubway())
                            _passengersWaitTrain.Remove(passenger);
                    }
                    else
                        _passengersWaitTrain.Remove(passenger);

                }
            }

            return _rightTrains;
        }

        public void PassengerEnter(List<string> listSubway, string currSubway)
        {
            foreach (Passenger passenger in _passengersWaitEnter.ToArray())
            {
                passenger.TryEnterSubway(this);
            }

            for (int i = 0; i < State.random.Next(_averageTransmittancePassengers / 2, _averageTransmittancePassengers); i++)
            {
                new Passenger(listSubway, currSubway).TryEnterSubway(this);
            }
        }

        public bool AreAvailableSpace(Passenger passenger)
        {
            if (_freeSpace > 0)
            {
                _freeSpace -= 1;
                _passengersWaitTrain.Add(passenger);
                _passengersWaitEnter.Remove(passenger);
                return true;
            }
            else
            {
                if (_passengersWaitEnter.Contains(passenger))
                {
                    if (passenger.TryLeaveSubway())
                    {
                        _passengersWaitEnter.Remove(passenger);
                        _gonePassengers += 1;
                        return false;
                    }
                    else
                        return false;

                }
                else
                {
                    _passengersWaitEnter.Add(passenger);
                    return false;
                }
            }
        }
    }
}
