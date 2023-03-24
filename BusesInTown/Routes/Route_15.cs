using BusesInTown.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusesInTown.Routes
{
    internal class Route_15 : BaseRoute
    {
        public override int RouteNumber => 15;

        public Route_15() : base()
        {
            StationToStationTime[0] = 5;
            StationToStationTime[StationToStationTime.Length - 1] = 20;
            Stations[0] = new Station("Start 15");
            Stations[Stations.Length - 1] = new Station("End 15");
        }

    }
}
