using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubwayModel.Model.Passengers;

namespace SubwayModel.Model
{
    public abstract class Train
    {
        protected int _maxPassengers;
        protected List<Passenger> _passengers;

        public Train(int maxPassengers)
        {
            this._maxPassengers = maxPassengers;
            _passengers = new List<Passenger>();
        }

        public void EnterSubway(Subway subway)
        {
            _passengers.RemoveAll(p => p.Destination == subway.Name);
        }

        public bool AreAvailableSeats(Passenger passenger)
        {
            if (_passengers.Count < _maxPassengers)
            {
                _passengers.Add(passenger);
                return true;
            }
            else
                return false;
        }

        public abstract bool IsTakeCorrectDirection(Passenger passenger);
    }
}
