using SubwayModel.Model.Passengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model
{
    public class TakeSpacePassengerFactory : IPassengerFactory
    {
        public Passenger CreatePassenger(List<string> listSubway)
        {
            if (Settings.random.Next(0, 10) == 1)
                return new PassengerLuggage(listSubway);
            else
                return new OrdinaryPassenger(listSubway);
        }
    }
}
