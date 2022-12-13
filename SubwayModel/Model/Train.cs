using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubwayModel.Model.Passengers;

namespace SubwayModel.Model
{
    public class Train
    {
        private int _maxCapacity;
        private List<Passenger> _passengers;

        public Train(int maxCapacity)
        {
            _maxCapacity = maxCapacity;
            _passengers = new List<Passenger>();
        }

        public void EnterSubway(Subway subway)
        {
            _passengers.RemoveAll(p => p.Destination == subway.Name);
        }

        public bool AreAvailableSeats(Passenger passenger)
        {
            var newCapacity = _passengers.Select(p => p.TakesSpace).ToArray().Sum() + passenger.TakesSpace;
            if (newCapacity <= _maxCapacity)
            {
                _passengers.Add(passenger);
                return true;
            }
            else
                return false;
        }
    }
}
