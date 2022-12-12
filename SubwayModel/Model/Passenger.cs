using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayModel.Model.Passengers
{
    public class Passenger
    {

        private int _timeWaiting;
        private string _destination;
        private int _sideTrain;

        public string Destination => _destination;
        public int SideTrain => _sideTrain;

        public Passenger(List<string> listSubway, string currSubway)
        {
            _timeWaiting = 0;
            _destination = currSubway;
            while (_destination == currSubway)
            {
                _destination = listSubway[State.random.Next(listSubway.Count)];
            }

            int id = listSubway.IndexOf(_destination);
            int idCurr = listSubway.IndexOf(currSubway);
            if (id > idCurr)
                _sideTrain = 1;
            else
                _sideTrain = 2;
        }

        public void TryEnterSubway(Subway subway)
        {
            if (!subway.AreAvailableSpace(this))
                _timeWaiting += 10;
        }

        public bool TryEnterTrain(Train train)
        {
            if (!train.AreAvailableSeats(this))
            {
                _timeWaiting += 10;
                return false;
            }
            return true;
        }

        public bool TryLeaveSubway()
        {
            if (State.random.Next(0, 3) == 1)
                return true;
            else
                return false;
        }
    }
}
