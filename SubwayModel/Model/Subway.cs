﻿using SubwayModel.Model.Passengers;
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
        private List<int> _waitingTime;

        public string Name => _name;
        public int FreeSpace => _freeSpace;
        public int AverageTransmittancePassengers => _averageTransmittancePassengers;

        public Subway (string name, int freeSpace, int averageTransmittancePassengers)
        {
            _name = name;
            _freeSpace = freeSpace;
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
                            _freeSpace++;
                        }
                    }
                    else if (train.IsTakeCorrectDirection(passenger))
                    {
                        _passengersWaitTrain.Remove(passenger);
                        _departedPassengers++;
                        _waitingTime.Add(passenger.TimeWaiting);
                        _freeSpace++;
                    }
                }
            }
        }

        public void PassengerEnter(List<string> listSubway, string currSubway)
        {
            foreach (Passenger passenger in _passengersWaitEnter.ToArray())
            {
                passenger.TryEnterSubway(this);
            }

            var newPassengers = State.random.Next((int)Math.Round(_averageTransmittancePassengers * 0.9),
                (int)Math.Round(_averageTransmittancePassengers * 1.1));
            for (int i = 0; i < newPassengers; i++)
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

        public void CalculateStatistics()
        {
            _passengersWaitEnter.ForEach(p => _gonePassengers++);
            _passengersWaitEnter.Clear();
            _passengersWaitTrain.ForEach(p => _gonePassengers++);
            State.ratioSubwayPassengers.Add(_departedPassengers * 100 / (_departedPassengers + _gonePassengers));
            if (_waitingTime.Count > 0)
                State.averageSubwayWaiting.Add((int)Math.Round(_waitingTime.Average()));
            else
                State.averageSubwayWaiting.Add(0);
            _waitingTime.Clear();
        }
    }
}
