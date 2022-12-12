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

        public abstract Train EnterSubway(Subway subway);

        public abstract bool AreAvailableSeats(Passenger passenger);
    }
}
