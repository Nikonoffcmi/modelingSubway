using SubwayModel.Model.Passengers;
using System.Collections.Generic;

namespace SubwayModel.Model.Factory
{
    public class TakeSpacePassengerFactoryHigh : IPassengerFactory
    {
        public Passenger CreatePassenger(List<string> listSubway)
        {
            if (Settings.random.Next(0, 2) == 1)
                return new PassengerLuggage(listSubway);
            else
                return new OrdinaryPassenger(listSubway);
        }
    }
}
