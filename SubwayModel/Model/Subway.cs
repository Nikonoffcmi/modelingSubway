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
        public string Name { get; }
        public int freeSpace;
        public int departedPassengers;
        public int gonePassengers;
        public int averageTransmittancePassengers;
        public Random random;
        public List<Passenger> passengersWaitTrain;
        public List<Passenger> passengersWaitEnter;
        public List<Passenger> passengersWaiting;
        public List<TrainLeft> LeftTrains;
        public List<TrainRight> RightTrains;

        public Subway (string name, int freeSpace, int averageTransmittancePassengers)
        {
            Name = name;
            this.freeSpace = freeSpace;
            departedPassengers = 1;
            gonePassengers = 0;
            this.averageTransmittancePassengers = averageTransmittancePassengers;
            random = new Random();
            passengersWaitTrain = new List<Passenger>();
            passengersWaitEnter = new List<Passenger>();
            passengersWaiting = new List<Passenger>();
            LeftTrains = new List<TrainLeft>();
            RightTrains = new List<TrainRight>();
        }

        public List<TrainLeft> SimulationLeftSide(List<TrainLeft> LeftTrains)
        {
            this.LeftTrains.Clear();
            if (LeftTrains.Count == 0)
            {
                for (int Minutes = 0; Minutes < State.simulationInterval; Minutes += State.averageTransmittanceTrains)
                {
                    new TrainLeft(State.TrainsCapacity).EnterSubway(this);
                }
            }
            else
            {
                int i = 0;
                for (int Minutes = 0; Minutes < State.simulationInterval; Minutes += State.averageTransmittanceTrains)
                {
                    LeftTrains[i].EnterSubway(this);
                    i++;
                }
            }

            foreach (var train in this.LeftTrains)
            {
                var temp = new List<Passenger>(passengersWaitTrain);
                train.TakePassengers(temp, this);
            }

            return this.LeftTrains;
        }

        public List<TrainRight> SimulationRightSide(List<TrainRight> RightTrains)
        {
            this.RightTrains.Clear();
            if (RightTrains.Count == 0)
            {
                for (int Minutes = 0; Minutes < State.simulationInterval; Minutes += State.averageTransmittanceTrains)
                {
                    new TrainRight(State.TrainsCapacity).EnterSubway(this);
                }
            }
            else
            {
                int i = 0;
                for (int Minutes = 0; Minutes < State.simulationInterval; Minutes += State.averageTransmittanceTrains)
                {
                    RightTrains[i].EnterSubway(this);
                    i++;
                }
            }

            foreach (var train in this.RightTrains)
            {
                var temp = new List<Passenger>(passengersWaitTrain);
                train.TakePassengers(temp, this);
            }
            return this.RightTrains;
        }

        public void PassengerEnter(List<string> listSubway, string currSubway)
        {
            foreach (Passenger passenger in passengersWaitEnter.ToArray())
            {
                passenger.EnterSubway(this);
            }

            for (int i = 0; i < random.Next(averageTransmittancePassengers / 2, averageTransmittancePassengers); i++)
            {
                new Passenger(random, listSubway, currSubway).EnterSubway(this);
            }
        }
    }
}
