using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubwayModel.Model.Passengers;

namespace SubwayModel.Model
{
    public class TrainRight
        : Train
    {
        public TrainRight(int maxPassengers) : base(maxPassengers)
        {
        }

        public override bool IsTakeCorrectDirection(Passenger passenger)
        {
            if (passenger.SideTrain == 2)
                return true;
            else
                return false;
        }
    }
}
