using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Passenger(List<string> listSubway, string currSubway)
        {
            _timeWaiting = 0;
            _destination = currSubway;
            while (_destination == currSubway)
            {
                _destination = listSubway[Settings.random.Next(listSubway.Count)];
            }
        }

        public bool TryEnterTrain(Train train)
        {
            if (!train.AreAvailableSeats(this))
            {
                _timeWaiting += Settings.averageTransmittanceTrains;
                return false;
            }
            return true;
        }
    }
}
