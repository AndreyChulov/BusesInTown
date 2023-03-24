using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusesInTown.Stations
{
    internal class Station
    {
        public string StationName { get; }

        public Station(string stationName)
        {
            StationName = stationName;
        }

        public override string ToString()
        {
            return $"Station -> StationName = {StationName}";
        }
    }
}
