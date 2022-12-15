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
        static public int averageTransmittanceTrains = 7;
        static public int TrainsCapacity = 6;
        static public List<Subway> Subways = new List<Subway>()
        {
            new Subway("кет", 30),
            new Subway("diyb", 33),
            new Subway("abcdf", 27),
            new Subway("linq", 25),
        };
    }

    static public class Statistics
    {
        static public List<int> averageSubwayWaitingTime = new List<int>();
        static public int averageWaitingTime;
        static public List<int> passengersWaitingTrains = new List<int>();
        static public double averagePassengersWaitingTrains = .0f;
    }
}
