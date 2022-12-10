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

        override public void TakePassengers(List<Passenger> passengers, Subway subway)
        {
            foreach (Passenger passenger in passengers)
            {
                if (passenger.trainSide == 1 && this.passengers.Count < maxPassengers)
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
