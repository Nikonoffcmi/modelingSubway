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

        public Subway ()
        {
            freeSpace = 10;
            departedPassengers = 0;
            gonePassengers = 0;
            random = new Random();
            passengersWaitTrain = new List<Passenger> ();
            passengersWaitEnter = new List<Passenger> ();
            passengersWaiting = new List<Passenger> ();
            trains = new List<Train> ();

        }
        public void Simulation()
        {
            double time = 24 * 60 / State.simulationInterval;
            for (int currTime = 0; currTime < time; currTime++)
            {
                foreach (Passenger passenger in passengersWaitEnter.ToArray())
                {
                    passenger.EnterSubway(this);
                }

                for (int i = 0; i < random.Next(State.averageTransmittancePassengers/2, State.averageTransmittancePassengers); i++)
                {
                    new Passenger(random).EnterSubway(this);
                }

                for (int Minutes = 0; Minutes < State.simulationInterval; Minutes += State.averageTransmittanceTrains)
                {
                    new TrainLeft(4).EnterSubway(this);
                    new TrainRight(4).EnterSubway(this);
                }

                foreach (var train in trains)
                {
                    var temp = new List<Passenger>(passengersWaitTrain);
                    train.TakePassengers(temp, this);
                }

                trains.Clear();
            }

            passengersWaitEnter.ForEach(p => gonePassengers++);
            passengersWaitEnter.Clear();
            passengersWaitTrain.ForEach(p => gonePassengers++);
            State.ratioGuests = departedPassengers * 100 / (departedPassengers + gonePassengers);
            var l = new List<int>(passengersWaiting.Count);
            passengersWaiting.ForEach(p => l.Add(p.timeWaiting));
            if (l.Count > 0)
                State.averageEnterWaiting = (int)Math.Round(l.Average());
            else
                State.averageEnterWaiting = 0;
            passengersWaiting.Clear();
        }
    }
}
