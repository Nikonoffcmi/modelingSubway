using SubwayModel.Model.Passengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model
{
    public interface IPassengerFactory
    {
        Passenger CreatePassenger(List<string> listSubway);
    }
}
