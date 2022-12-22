using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SubwayModel.Model.Tests
{
    [TestClass()]
    public class SybwayModelingTests
    {
        [TestMethod()]
        public void SybwayModelingTest()
        {
            var subways = Settings.Subways;
            var factoty = new TakeSpacePassengerFactory();

            var city = new SybwayModeling(subways, factoty);
            
            Assert.IsNotNull(city);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void SybwayModelingSubwaysExceptionTest()
        {
            var subways = new List<Subway>();
            var factoty = new TakeSpacePassengerFactory();

            new SybwayModeling(subways, factoty);
        }

        [TestMethod()]
        public void DefaultSimulationTest()
        {
            Settings.DefaultSettings();
            var city = new SybwayModeling(Settings.Subways, new TakeSpacePassengerFactory());

            city.Simulation();

            var result = Statistics.averagePassengersWaitingTrains > 59 && Statistics.averagePassengersWaitingTrains < 66;
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void SmallAverageTransmittanceTrainsSimulationTest()
        {
            Settings.averageTransmittanceTrains = 2;
            var city = new SybwayModeling(Settings.Subways, new TakeSpacePassengerFactory());

            city.Simulation();

            Settings.DefaultSettings();
            var result = Statistics.averagePassengersWaitingTrains >= 0 && Statistics.averagePassengersWaitingTrains <= 5;
            Assert.IsTrue(result);
        }
    }
}