using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubwayModel.Model.Passengers;

namespace SubwayModel.Model
{
    public class TrainLeft
        : Train
    {
        public TrainLeft(int maxPassengers) : base(maxPassengers)
        {
        }

        public override bool IsTakeCorrectDirection(Passenger passenger)
        {
            if (passenger.SideTrain == 1)
                return true;
            else
                return false;
        }
    }
}
