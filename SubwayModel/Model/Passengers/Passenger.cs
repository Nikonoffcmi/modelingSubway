using System;
using System.Collections.Generic;

namespace SubwayModel.Model.Passengers
{
    public abstract class Passenger
    {

        private int _timeWaiting;
        private string _destination;
        protected int _takesSpace;

        public string Destination => _destination;
        public int TimeWaiting => _timeWaiting;
        public int TakesSpace => _takesSpace;

        public Passenger(List<string> listSubway)
        {
            if (listSubway == null)
                throw new ArgumentNullException(nameof(listSubway));
            if (listSubway.Count == 0)
                throw new ArgumentException(nameof(listSubway), "Нет доступных станций для перемещения");

            _timeWaiting = 0;
            _destination = listSubway[Settings.random.Next(listSubway.Count)];
        }

        public bool TryEnterTrain(Train train)
        {
            if (train == null)
                throw new ArgumentNullException(nameof(train));

            if (!train.AreAvailableSeats(this))
            {
                _timeWaiting += Settings.averageTransmittanceTrains;
                return false;
            }
            else
                train.AddPassenger(this);
            return true;
        }
    }
}
