using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model.Passengers
{
    public class PassengerRight
        : Passenger
    {

        override public void EnterTrain(Train train, Subway subway)
        {
            //if (train.GetType() == typeof(TrainRight))
        }
    }
}
