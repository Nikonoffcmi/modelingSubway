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
        private int _departedPassengers;
        private int _gonePassengers;
        private int _averageTransmittancePassengers;
        private List<Passenger> _passengersWaitTrain;
        private List<Passenger> _passengersWaitEnter;
        private List<int> _waitingTime;

        public string Name => _name;
        public int AverageTransmittancePassengers => _averageTransmittancePassengers;

        public Subway (string name, int averageTransmittancePassengers)
        {
            _name = name;
            _departedPassengers = 0;
            _gonePassengers = 0;
            _averageTransmittancePassengers = averageTransmittancePassengers;
            _passengersWaitTrain = new List<Passenger>();
            _passengersWaitEnter = new List<Passenger>();
            _waitingTime = new List<int>();
        }

        public void Simulation(List<Train> trains)
        {
            foreach (var train in trains)
            {
                var temp = new List<Passenger>(_passengersWaitTrain);
                foreach (var passenger in temp)
                {
                    if (train.IsTakeCorrectDirection(passenger) && !passenger.TryEnterTrain(train))
                    {
                        if (passenger.TryLeaveSubway())
                        {
                            _passengersWaitTrain.Remove(passenger);
                            _gonePassengers++;
                            _waitingTime.Add(passenger.TimeWaiting);
                        }
                    }
                    else if (train.IsTakeCorrectDirection(passenger))
                    {
                        _passengersWaitTrain.Remove(passenger);
                        _departedPassengers++;
                        _waitingTime.Add(passenger.TimeWaiting);
                    }
                }
            }
        }

        public void PassengerEnter(List<string> listSubway, string currSubway)
        {
            var newPassengers = Settings.random.Next((int)Math.Round(_averageTransmittancePassengers * 0.85),
                (int)Math.Round(_averageTransmittancePassengers * 1.15));
            for (int i = 0; i < newPassengers; i++)
            {
                new Passenger(listSubway, currSubway).TryEnterSubway(this);
            }
        }

        public bool AreAvailableSpace(Passenger passenger)
        {
            _passengersWaitTrain.Add(passenger);
            return true;
            //if (_freeSpace > 0)
            //{
            //    _freeSpace -= 1;
            //    _passengersWaitTrain.Add(passenger);
            //    _passengersWaitEnter.Remove(passenger);
            //    return true;
            //}
            //else
            //{
            //    if (_passengersWaitEnter.Contains(passenger))
            //    {
            //        if (passenger.TryLeaveSubway())
            //        {
            //            _passengersWaitEnter.Remove(passenger);
            //            _gonePassengers += 1;
            //            return false;
            //        }
            //        else
            //            return false;

            //    }
            //    else
            //    {
            //        _passengersWaitEnter.Add(passenger);
            //        return false;
            //    }
            //}
        }

        public void CalculateStatistics()
        {
            _passengersWaitEnter.ForEach(p => _gonePassengers++);
            _passengersWaitEnter.Clear();
            _passengersWaitTrain.ForEach(p => _gonePassengers++);
            Statistics.ratioSubwayPassengers.Add(_departedPassengers * 100 / (_departedPassengers + _gonePassengers));
            if (_waitingTime.Count > 0)
                Statistics.averageSubwayWaiting.Add((int)Math.Round(_waitingTime.Average()));
            else
                Statistics.averageSubwayWaiting.Add(0);
            _waitingTime.Clear();
            _passengersWaitTrain.Clear();
            _departedPassengers = 0;
            _gonePassengers = 0;
        }
    }
}
