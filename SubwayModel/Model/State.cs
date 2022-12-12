using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model
{
    static public class State
    {
        static public Random random = new Random();
        static public int simulationInterval = 60;
        static public int simulationTime = 5;
        static public int averageTransmittanceTrains = 10;
        static public int TrainsCapacity = 4;
        static public List<int> averageSubwayWaiting = new List<int>();
        static public int averageWaiting;
        static public List<double> ratioSubwayPassengers = new List<double>();
        static public double ratioPassengers = .0f;
    }
}
