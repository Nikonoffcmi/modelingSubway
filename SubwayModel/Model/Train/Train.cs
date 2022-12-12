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

        public Train(int maxPassengers)
        {
            this.maxPassengers = maxPassengers;
            passengers = new List<Passenger>();
        }

        public abstract void EnterSubway(Subway subway);

        public abstract void TakePassengers(List<Passenger> passengers, Subway subway);
    }
}
