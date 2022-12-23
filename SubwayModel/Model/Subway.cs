using SubwayModel.Model.Passengers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SubwayModel.Model
{
    public class Subway
    {
        private string _name;
        private int _averageTransmittancePassengers;
        private List<Passenger> _passengersWaitTrain;
        private List<int> _waitingTime;

        public string Name => _name;
        public int AverageTransmittancePassengers => _averageTransmittancePassengers;

        public Subway (string name, int averageTransmittancePassengers)
        {
            if (name == null || name.Length == 0)
                throw new ArgumentNullException(nameof(name));
            if (averageTransmittancePassengers < 1)
                throw new ArgumentOutOfRangeException(averageTransmittancePassengers.ToString(), "Пассажиропоток должен быть больше 0");

            _name = name;
            _averageTransmittancePassengers = averageTransmittancePassengers;
            _passengersWaitTrain = new List<Passenger>();
            _waitingTime = new List<int>();
        }

        public void PassengersGetOnTrain(Train train)
        {
            var notPlacedTrainPassengers = 0;
            if (train == null)
                throw new ArgumentNullException(nameof(train));

            var temp = new List<Passenger>(_passengersWaitTrain);
            foreach (var passenger in temp)
            {
                if (!passenger.TryEnterTrain(train))
                    notPlacedTrainPassengers++;
                else
                {
                    _passengersWaitTrain.Remove(passenger);
                    _waitingTime.Add(passenger.TimeWaiting);
                }
            }

            if (Statistics.passengersWaitingTrains.ContainsKey(_name))
                Statistics.passengersWaitingTrains[_name].Add(notPlacedTrainPassengers);
            else
            {
                var l = new List<int>(); l.Add(notPlacedTrainPassengers);
                Statistics.passengersWaitingTrains.Add(_name, l);
            }
        }

        public void PassengersEnter(List<string> listSubway, IPassengerFactory passengerFactory)
        {
            if (listSubway == null)
                throw new ArgumentNullException(nameof(listSubway));
            if (listSubway.Count == 0)
                throw new ArgumentException(nameof(listSubway), "Нет доступных станций для перемещения");
            if (listSubway.Where(x => x.Equals(_name)).Count() == 1)
                throw new ArgumentException(nameof(listSubway), "Станции не должно быть в списке ");

            var newPassengers = Settings.random.Next(
                (int)Math.Round(_averageTransmittancePassengers * 0.85 / (60 / Settings.averageTransmittanceTrains)),
                (int)Math.Round(_averageTransmittancePassengers * 1.15 / (60 / Settings.averageTransmittanceTrains)));

            for (int i = 0; i < newPassengers; i++)
            {
                _passengersWaitTrain.Add(passengerFactory.CreatePassenger(listSubway));
            }
        }

        public void CalculateStatistics()
        {
            _waitingTime.AddRange(_passengersWaitTrain.Select(p => p.TimeWaiting));
            int wt = 0;
            if (_waitingTime.Count > 0) 
                wt = (int)Math.Round(_waitingTime.Average());
            if (Statistics.averageSubwayWaitingTime.ContainsKey(_name))
                Statistics.averageSubwayWaitingTime[_name].Add(wt);
            else
            {
                var l = new List<int>(); l.Add(wt);
                Statistics.averageSubwayWaitingTime.Add(_name, l);
            }
            _waitingTime.Clear();
            _passengersWaitTrain.Clear();
        }
    }
}
