using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubwayModel.Model.Passengers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SubwayModel.Model.Tests
{
    [TestClass()]
    public class SubwayTests
    {
        [TestMethod()]
        public void SubwayTest()
        {
            string name = "vsed";
            int Transmittance = 12;

            var subway = new Subway(name, Transmittance);

            Assert.IsNotNull(subway);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod()]
        public void SubwayTransmittanceExceptionTest()
        {
            string name = "vsed";
            int Transmittance = 0;

            new Subway(name, Transmittance);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod()]
        public void SubwayNameExceptionTest()
        {
            string name = "";
            int Transmittance = 2;

            new Subway(name, Transmittance);
        }

        [TestMethod()]
        public void PassengersGetOnTrainTest()
        {
            Settings.DefaultSettings();
            var list = new List<string>() { "cesd", "sed", "ved" };
            var train = new Train(10);
            var subway = new Subway("fe", 3);

            subway.PassengersEnter(list, new TakeSpacePassengerFactoryLow());
            subway.PassengersGetOnTrain(train);
            var result = Statistics.passengersWaitingTrains[subway.Name].Average() == 0;

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void PassengersNotGetOnTrainTest()
        {
            Settings.DefaultSettings();
            var list = new List<string>() { "cesd", "sed", "ved" };
            var train = new Train(1);
            var subway = new Subway("fe", 10);
            train.AddPassenger(new OrdinaryPassenger(new List<string> { "fe", "vedc" }));

            subway.PassengersEnter(list, new TakeSpacePassengerFactoryLow());
            subway.PassengersGetOnTrain(train);
            var result = Statistics.passengersWaitingTrains[subway.Name].Average() > 0;

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void PassengersEnterTest()
        {
            var list = new List<string>() { "cesd", "sed", "ved" };
            var subway = new Subway("vsed", 12);

            subway.PassengersEnter(list, new TakeSpacePassengerFactoryLow());

        }
    }
}