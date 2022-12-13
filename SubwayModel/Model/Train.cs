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
        private int _totalCapacity;

        public Train(int maxCapacity)
        {
            _maxCapacity = maxCapacity;
            _passengers = new List<Passenger>();
            _totalCapacity = 0;
        }

        public void EnterSubway(Subway subway)
        {
            _passengers.RemoveAll(p => p.Destination == subway.Name);
        }

        public bool AreAvailableSeats(Passenger passenger)
        {
            var newCapacity = _totalCapacity + passenger.TakesSpace;
            if (newCapacity < _maxCapacity)
            {
                _passengers.Add(passenger);
                _totalCapacity += passenger.TakesSpace;
                return true;
            }
            else
                return false;
        }
    }
}
