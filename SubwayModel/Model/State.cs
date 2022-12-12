using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model
{
    static public class Settings
    {
        static public Random random = new Random();
        static public int simulationTime = 5;
        static public int averageTransmittanceTrains = 10;
        static public int TrainsCapacity = 20;
        static public List<Subway> Subways = new List<Subway>()
        {
            new Subway("кет", 15),
            new Subway("diyb", 8),
            new Subway("linq", 11),
        };
    }

    static public class Statistics
    {
        static public List<int> averageSubwayWaiting = new List<int>();
        static public int averageWaiting;
        static public List<double> ratioSubwayPassengers = new List<double>();
        static public double ratioPassengers = .0f;
    }
}
