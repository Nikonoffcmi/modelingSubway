using SubwayModel.Model.Passengers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model
{
    public class Subway
    {
        public int freeSpace;
        public int departedPassengers;
        public int gonePassengers;

        public Random random;
        public List<Passenger> passengersWaitTrain;
        public List<Passenger> passengersWaitEnter;
        public List<Passenger> passengersWaiting;
        public List<Train> trains;

        public void Simulation(int time)
        {
            for (int currTime = 0; currTime < time; currTime++)
            {
                foreach (Passenger passenger in passengersWaitEnter)
                {
                    passenger.EnterSubway(this);
                }

                for (int i = 0; i < random.Next(0, State.averageTransmittance + 1); i++)
                {
                    if (random.Next(0, 1) == 0)
                        new PassengerLeft().EnterSubway(this);
                    else
                        new PassengerRight().EnterSubway(this);
                }

                var temppassengersWaitTrain = new List<Passenger>(passengersWaitTrain.Count);
                foreach (var passenger in passengersWaitTrain)
                    temppassengersWaitTrain.Add(passenger);

                foreach(var passenger in temppassengersWaitTrain)
                {
                    for (int i = 0; i < trains.Count; i++)
                    {
                        for (int tenMinutes = 0; tenMinutes < State.simulationInterval; tenMinutes += 10)
                        {
                            if (random.Next(0, 2) == 1)
                            {
                                trains[i].TakePassenger(passenger, this);
                                passengersWaitTrain.Remove(passenger);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
