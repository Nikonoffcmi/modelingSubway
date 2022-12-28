using SubwayModel.Model.Passengers;
using System.Collections.Generic;

namespace SubwayModel.Model.Factory
{
    public class TakeSpacePassengerFactoryMiddle : IPassengerFactory
    {
        public Passenger CreatePassenger(List<string> listSubway)
        {
            if (Settings.random.Next(0, 3) == 1)
                return new PassengerLuggage(listSubway);
            else
                return new OrdinaryPassenger(listSubway);
        }
    }
}
