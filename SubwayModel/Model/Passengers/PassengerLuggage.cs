using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model.Passengers
{
    public class PassengerLuggage
        : Passenger
    {
        public PassengerLuggage(List<string> listSubway, string currSubway) : base(listSubway, currSubway)
        {
            _takesSpace = 2;
        }
    }
}
