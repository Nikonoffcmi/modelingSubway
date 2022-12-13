using SubwayModel.Model.Passengers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model
{
    public class Subway
    {
        private string _name;
        private int _averageTransmittancePassengers;
        private List<Passenger> _passengersWaitTrain;
        private List<int> _waitingTime;
        private int _notPlacedTrainPassengers;

        public string Name => _name;
        public int AverageTransmittancePassengers => _averageTransmittancePassengers;
        public int NotPlacedTrainPassengers { get => _notPlacedTrainPassengers; set => _notPlacedTrainPassengers = 0; }

        public Subway (string name, int averageTransmittancePassengers)
        {
            _name = name;
            _notPlacedTrainPassengers = 0;
            _averageTransmittancePassengers = averageTransmittancePassengers;
            _passengersWaitTrain = new List<Passenger>();
            _waitingTime = new List<int>();
        }

        public void Simulation(Train train)
        {
            var temp = new List<Passenger>(_passengersWaitTrain);
            foreach (var passenger in temp)
            {
                if (!passenger.TryEnterTrain(train))
                    _notPlacedTrainPassengers++;
                else
                {
                    _passengersWaitTrain.Remove(passenger);
                    _waitingTime.Add(passenger.TimeWaiting);
                }
            }            
        }

        public void PassengerEnter(List<string> listSubway)
        {
            var newPassengers = Settings.random.Next(
                (int)Math.Round(_averageTransmittancePassengers * 0.85 / (60 / Settings.averageTransmittanceTrains)),
                (int)Math.Round(_averageTransmittancePassengers * 1.15 / (60 / Settings.averageTransmittanceTrains)));

            for (int i = 0; i < newPassengers; i++)
            {
                if (Settings.random.Next(0, 10) == 1)
                    _passengersWaitTrain.Add(new PassengerLuggage(listSubway, _name));
                else
                    _passengersWaitTrain.Add(new OrdinaryPassenger(listSubway, _name));
            }
        }

        public void CalculateStatistics()
        {
            
            if (_waitingTime.Count > 0)
                Statistics.averageSubwayWaiting.Add((int)Math.Round(_waitingTime.Average()));
            else
                Statistics.averageSubwayWaiting.Add(0);
            _waitingTime.Clear();
            _passengersWaitTrain.Clear();
        }
    }
}
