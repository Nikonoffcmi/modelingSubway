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
            var factoty = new TakeSpacePassengerFactoryLow();

            var city = new SybwayModeling(subways, factoty);
            
            Assert.IsNotNull(city);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void SybwayModelingSubwaysExceptionTest()
        {
            var subways = new List<Subway>();
            var factoty = new TakeSpacePassengerFactoryLow();

            new SybwayModeling(subways, factoty);
        }

        [TestMethod()]
        public void DefaultSimulationTest()
        {
            Settings.DefaultSettings();
            var city = new SybwayModeling(Settings.Subways, new TakeSpacePassengerFactoryLow());

            city.Simulation();

            var result = Statistics.averagePassengersWaitingTrains >= 5 && Statistics.averagePassengersWaitingTrains < 10;
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void SmallAverageTransmittanceTrainsSimulationTest()
        {
            Settings.averageTransmittanceTrains = 2;
            var city = new SybwayModeling(Settings.Subways, new TakeSpacePassengerFactoryLow());

            city.Simulation();

            Settings.DefaultSettings();
            var result = Statistics.averagePassengersWaitingTrains >= 0 && Statistics.averagePassengersWaitingTrains <= 5;
            Assert.IsTrue(result);
        }
    }
}