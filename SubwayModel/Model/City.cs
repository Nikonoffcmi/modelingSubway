using SubwayModel.Model.Passengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model
{
    public class City
    {
        public List<Subway> subways;
        public List<TrainLeft> LeftTrains;
        public List<TrainRight> RightTrains;
        public List<List<Train>> WaitingTrains;

        public City ()
        {
            subways = new List<Subway> ();
            LeftTrains = new List<TrainLeft> ();
            RightTrains = new List<TrainRight> ();
            WaitingTrains = new List<List<Train>> ();
        }

        public void Simulation()
        {
            double time = 60 / State.simulationInterval;
            for (int currTime = 0; currTime < time; currTime++)
            {
                for (int i = 0; i < subways.Count; i++)
                {
                    subways[i].PassengerEnter(subways.Select(s => s.Name).ToList(), subways[i].Name);
                    
                    var temp = subways[i].SimulationLeftSide(LeftTrains);
                    LeftTrains.Clear();
                    LeftTrains.AddRange(temp);
                }
                LeftTrains.Clear();

                for (int i = subways.Count - 1; i >= 0; i--)
                {

                    var temp = subways[i].SimulationRightSide(RightTrains);
                    RightTrains.Clear();
                    RightTrains.AddRange(temp);
                }
                RightTrains.Clear();
            }

            foreach (var subway in subways)
            {
                subway.passengersWaitEnter.ForEach(p => subway.gonePassengers++);
                subway.passengersWaitEnter.Clear();
                subway.passengersWaitTrain.ForEach(p => subway.gonePassengers++);
                State.ratioPassengers = subway.departedPassengers * 100 / (subway.departedPassengers + subway.gonePassengers);
                var l = new List<int>(subway.passengersWaiting.Count);
                subway.passengersWaiting.ForEach(p => l.Add(p.timeWaiting));
                if (l.Count > 0)
                    State.averageEnterWaiting = (int)Math.Round(l.Average());
                else
                    State.averageEnterWaiting = 0;
                subway.passengersWaiting.Clear();
            }
        }
    }
}
