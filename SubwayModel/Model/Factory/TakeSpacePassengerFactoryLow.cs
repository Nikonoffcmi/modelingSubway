using SubwayModel.Model.Passengers;
using System.Collections.Generic;

namespace SubwayModel.Model
{
    public class TakeSpacePassengerFactoryLow : IPassengerFactory
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
