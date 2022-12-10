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

        override public void TakePassengers(List<Passenger> passengers, Subway subway)
        {
            foreach (Passenger passenger in passengers)
            {
                if (passenger.trainSide == 2 && this.passengers.Count < maxPassengers)
                {
                    this.passengers.Add(passenger);
                    subway.passengersWaitTrain.Remove(passenger);
                    subway.departedPassengers++;
                    subway.freeSpace++;
                }
            }
        }

    }
}
