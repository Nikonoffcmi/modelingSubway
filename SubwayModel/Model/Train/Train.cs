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
        public int maxPassengers { get;}
        public List<Passenger> passengers { get; set; }

        public abstract void TakePassenger(Passenger passenger, Subway subway);

        public void LeavePassenger(Passenger passenger, Subway subway)
        {

        }
    }
}
