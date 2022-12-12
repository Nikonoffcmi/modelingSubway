using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubwayModel.Model.Passengers;

namespace SubwayModel.Model
{
    public class TrainRight
        : Train
    {
        public TrainRight(int maxPassengers) : base(maxPassengers)
        {
        }

        override public Train EnterSubway(Subway subway)
        {
            _passengers.RemoveAll(p => p.Destination == subway.Name);
            return this;
        }

        override public bool AreAvailableSeats(Passenger passenger)
        {
            if (passenger.SideTrain == 2 && this._passengers.Count < _maxPassengers)
            {
                _passengers.Add(passenger);
                return true;
            }
            else
                return false;
        }

    }
}
