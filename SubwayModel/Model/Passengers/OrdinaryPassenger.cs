using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model.Passengers
{
    public class OrdinaryPassenger
        : Passenger
    {
        public OrdinaryPassenger(List<string> listSubway, string currSubway) : base(listSubway)
        {
            _takesSpace = 1;
        }
    }
}
