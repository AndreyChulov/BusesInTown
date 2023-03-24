using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusesInTown.TownWatchUtility
{
    internal interface ITownClock<T>
    {
        DateTime TownTime { get; }
        TimeSpan TownWatchSleepTime { get; }

        event EventHandler<TimeEventArgs> TimeEventOccured;

        void Start();
        void Stop();

        void AddTimeEvent(T sender, TimeSpan timeToWait);
    }
}
