using System.Collections.Generic;

namespace SubwayModel.Model.Passengers
{
    public class PassengerLuggage
        : Passenger
    {
        public PassengerLuggage(List<string> listSubway) : base(listSubway)
        {
            _takesSpace = 2;
        }
    }
}
