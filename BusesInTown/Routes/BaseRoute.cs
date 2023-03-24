using BusesInTown.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusesInTown.Routes
{
    abstract class BaseRoute : IRoute
    {
        public abstract int RouteNumber { get; }

        protected int[] StationToStationTime { get; }
        protected Station[] Stations { get; }

        public BaseRoute()
        {
            StationToStationTime = new[] { 0, 5, 10, 7, 0 };
            Stations = new Station[] { 
                null,// start
                new Station("station 1"),
                new Station("station 2"),
                new Station("station 3"),
                new Station("station 4"),
                null,//finish
            };
        }
    }
}
