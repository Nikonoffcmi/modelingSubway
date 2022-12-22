using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SubwayModel.Model.Passengers.Tests
{
    [TestClass()]
    public class PassengerLuggageTests
    {
        [TestMethod()]
        public void PassengerLuggageTest()
        {
            var list = new List<string>() { "cesd", "fe", "ved" };

            var passengerLuggage = new PassengerLuggage(list);

            Assert.IsNotNull(passengerLuggage);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void FalsePassengerLuggageTest()
        {
            var list = new List<string>();
            new PassengerLuggage(list);
        }

        [TestMethod()]
        public void TryEnterEmptyTrainTest()
        {
            var train = new Train(3);
            var list = new List<string>() { "cesd", "fe", "ved" };
            var passengerLuggage = new PassengerLuggage(list);

            var result = passengerLuggage.TryEnterTrain(train);

            Assert.IsTrue(result);
        }



        [TestMethod()]
        public void TryEnterFullTrainTest()
        {
            var list = new List<string>() { "cesd", "fe", "ved" };
            var passengerLuggage = new PassengerLuggage(list);
            var train = new Train(4);
            train.AddPassenger(new PassengerLuggage(list));
            train.AddPassenger(new PassengerLuggage(list));

            var result = passengerLuggage.TryEnterTrain(train);

            Assert.IsFalse(result);
        }
    }
}