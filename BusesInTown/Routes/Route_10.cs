using BusesInTown.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusesInTown.Routes
{
    internal class Route_10 : BaseRoute
    {
        public override int RouteNumber => 10;

        public Route_10() : base() 
        {
            StationToStationTime[0] = 10;
            StationToStationTime[StationToStationTime.Length - 1] = 15;
            Stations[0] = new Station("Start 10");
            Stations[Stations.Length - 1] = new Station("End 10");
        }
    }
}
