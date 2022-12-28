using SubwayModel.Model.Passengers;
using System.Collections.Generic;

namespace SubwayModel.Model
{
    public interface IPassengerFactory
    {
        Passenger CreatePassenger(List<string> listSubway);
    }
}
