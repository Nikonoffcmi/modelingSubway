using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model.Passengers
{
    public class Passenger
    {
        public int timeWaiting { get; set; }
        public string destination { get; }
        public int sideTrain { get; }

        public Passenger(Random random, List<string> listSubway, string currSubway)
        {
            timeWaiting = 0;
            destination = currSubway;
            while (destination == currSubway)
            {
                destination = listSubway[random.Next(listSubway.Count)];
            }

            int id = listSubway.IndexOf(destination);
            int idCurr = listSubway.IndexOf(currSubway);
            if (id > idCurr)
                sideTrain = 1;
            else
                sideTrain = 2;
        }

        public void EnterSubway(Subway subway)
        {
            if (subway.freeSpace > 0)
            {
                subway.freeSpace -= 1;
                subway.passengersWaitTrain.Add(this);
                subway.passengersWaitEnter.Remove(this);
            }
            else
            {
                if (!subway.passengersWaitEnter.Contains(this))
                    subway.passengersWaitEnter.Add(this);
                else
                {
                    if (subway.passengersWaiting.Contains(this))
                    {
                        if (LeaveSubway(subway.random))
                        {
                            subway.passengersWaitEnter.Remove(this);
                            subway.gonePassengers += 1;
                        }
                        else
                            timeWaiting += 30;

                    }
                    else
                    {
                        timeWaiting += 30;
                        subway.passengersWaiting.Add(this);
                    }
                }
            }
        }

        public bool LeaveSubway(Random rnd)
        {
            if (rnd.Next(0, 2) == 1)
                return true;
            else
                return false;
        }
    }
}
