using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model.Passengers
{
    public abstract class Passenger
    {
        public int timeWaiting { get; set; }

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

        public abstract void EnterTrain(Train train, Subway subway);

        public bool LeaveSubway(Random rnd)
        {
            if (rnd.Next(0, 2) == 1)
                return true;
            else
                return false;
        }
    }
}
