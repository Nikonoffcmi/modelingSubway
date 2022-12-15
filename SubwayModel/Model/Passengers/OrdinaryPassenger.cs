using System.Collections.Generic;

namespace SubwayModel.Model.Passengers
{
    public class OrdinaryPassenger
        : Passenger
    {
        public OrdinaryPassenger(List<string> listSubway) : base(listSubway)
        {
            _takesSpace = 1;
        }
    }
}
