using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubwayModel.Model;
using SubwayModel.Model.Passengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model.Tests
{
    [TestClass()]
    public class TrainTests
    {
        [TestMethod()]
        public void TrainTest()
        {
            int capacity = 3;

            var train = new Train(capacity);

            Assert.IsNotNull(train);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod()]
        public void TrainExceptionTest()
        {
            int capacity = 0;

            var train = new Train(capacity);

            Assert.IsNotNull(train);
        }

        [TestMethod()]
        public void EnterSubwayTest()
        {
            int capacity = 2;
            var train = new Train(capacity);
            var subway = new Subway("fe", 2);
            train.AddPassenger(new OrdinaryPassenger(new List<string>{ "fe"}));

            train.EnterSubway(subway);
            var result = train.AreAvailableSeats(new PassengerLuggage(new List<string> { "fxdcfh", "grsd" }));
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void AreAvailableSeatsTest()
        {
            int capacity = 3;
            var train = new Train(capacity);
            train.AddPassenger(new OrdinaryPassenger(new List<string> { "fe" , "vedc"}));

            var result = train.AreAvailableSeats(new PassengerLuggage(new List<string> { "bdrf" }));
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void AreAvailableSeatsFalseTest()
        {
            int capacity = 2;
            var train = new Train(capacity);
            train.AddPassenger(new OrdinaryPassenger(new List<string> { "fe", "vedc" }));

            var result = train.AreAvailableSeats(new PassengerLuggage(new List<string> { "bdrf" }));
            Assert.IsFalse(result);
        }
    }
}