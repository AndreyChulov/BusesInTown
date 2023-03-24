using BusesInTown.Routes;
using BusesInTown.TownWatchUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusesInTown.Buses
{
    internal class Bus : TownWatchSingleProxy
    {
        public event EventHandler BusOnStartStation;
        public event EventHandler BusOnEndStation;

        private static int _lastBusId = 0;
        private readonly IRoute _route;

        public int BusId { get; }

        public int BusNumber => _route.RouteNumber;

        public Bus(IRoute route)
        {
            BusId = ++_lastBusId;
            _route = route;
        }

        public override void Start()
        {
            base.Start();
            Invoke(() => BusOnStartStation?.Invoke(this, EventArgs.Empty));
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
