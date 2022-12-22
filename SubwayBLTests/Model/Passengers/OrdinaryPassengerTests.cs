using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SubwayModel.Model.Passengers.Tests
{
    [TestClass()]
    public class OrdinaryPassengerTests
    {
        [TestMethod()]
        public void OrdinaryPassengerTest()
        {
            var list = new List<string>() { "cesd", "fe", "ved"};

            var ordinaryPassenger = new OrdinaryPassenger(list);

            Assert.IsNotNull(ordinaryPassenger);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void FalseOrdinaryPassengerTest()
        {
            var list = new List<string>();
            new OrdinaryPassenger(list);
        }

        [TestMethod()]
        public void TryEnterEmptyTrainTest()
        {
            var train = new Train(3);
            var list = new List<string>() { "cesd", "fe", "ved" };
            var ordinaryPassenger = new OrdinaryPassenger(list);

            var result = ordinaryPassenger.TryEnterTrain(train);

            Assert.IsTrue(result);
        }



        [TestMethod()]
        public void TryEnterFullTrainTest()
        {
            var list = new List<string>() { "cesd", "fe", "ved" };
            var ordinaryPassenger = new OrdinaryPassenger(list);
            var train = new Train(3);
            train.AddPassenger(new OrdinaryPassenger(list));
            train.AddPassenger(new OrdinaryPassenger(list));
            train.AddPassenger(new OrdinaryPassenger(list));

            var result = ordinaryPassenger.TryEnterTrain(train);

            Assert.IsFalse(result);
        }
    }
}