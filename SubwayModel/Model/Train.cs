using System;
using System.Collections.Generic;
using System.Linq;
using SubwayModel.Model.Passengers;

namespace SubwayModel.Model
{
    public class Train
    {
        private int _maxCapacity;
        private List<Passenger> _passengers;

        public Train(int maxCapacity)
        {
            if (maxCapacity < 1)
                throw new ArgumentOutOfRangeException(maxCapacity.ToString(), "Максимальная вместимость поезда должна быть больше нуля.\n");
            
            _maxCapacity = maxCapacity;
            _passengers = new List<Passenger>();
        }

        public void EnterSubway(Subway subway)
        {
            if (subway == null)
                throw new ArgumentNullException(nameof(subway));
            if (_passengers != null)
                _passengers.RemoveAll(p => p.Destination == subway.Name);
        }

        public bool AreAvailableSeats(Passenger passenger)
        {
            if (passenger == null)
                throw new ArgumentNullException(nameof(passenger));

            var newCapacity = _passengers.Select(p => p.TakesSpace).ToArray().Sum() + passenger.TakesSpace;
            if (newCapacity <= _maxCapacity)
                return true;
            else
                return false;
        }

        public void AddPassenger(Passenger passenger)
        {
            if (AreAvailableSeats(passenger))
                _passengers.Add(passenger);
            else
                throw new ArgumentException(_maxCapacity.ToString(),"Нет места для пассажира");
        }
    }
}
