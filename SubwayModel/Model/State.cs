using System;
using System.Collections.Generic;

namespace SubwayModel.Model
{
    static public class Settings
    {
        static public Random random = new Random();
        static public int simulationTime = 5;
        static public int averageTransmittanceTrains = 7;
        static public int numberVan = 3;
        static public int vanCapacity = 2;
        static public int numberRuns = 1000;
        static public IPassengerFactory passengerFactory = new TakeSpacePassengerFactoryLow();
        static public List<Subway> Subways = new List<Subway>()
        {
            new Subway("кет", 30),
            new Subway("diyb", 33),
            new Subway("abcdf", 27),
            new Subway("linq", 25),
        };

        static public void DefaultSettings()
        {
            simulationTime = 5;
            averageTransmittanceTrains = 7;
            vanCapacity = 2;
            numberRuns = 1000;
            numberVan = 4;
            passengerFactory = new TakeSpacePassengerFactoryLow();
            Subways = new List<Subway>()
            {
            new Subway("кет", 30),
            new Subway("diyb", 33),
            new Subway("abcdf", 27),
            new Subway("linq", 25),
            };
        }
    }

    static public class Statistics
    {
        static public Dictionary<string, List<int>> averageSubwayWaitingTime = new Dictionary<string, List<int>>();
        static public int averageWaitingTime;
        static public Dictionary<string, List<int>> passengersWaitingTrains = new Dictionary<string, List<int>>();
        static public double averagePassengersWaitingTrains = .0f;

        static public void DefaultStatistics()
        {
            averageSubwayWaitingTime.Clear();
            averageWaitingTime = 0;
            passengersWaitingTrains.Clear();
            averagePassengersWaitingTrains = .0f;
        }
    }
}
