using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusesInTown.TownWatchUtility
{
    internal class TimeEventArgs : IComparable<TimeEventArgs>
    {
        public object Sender { get; }
        public TimeSpan TimeToWait { get; }
        public TimeSpan RestTimeToWait { get; private set; }

        public bool IsEventShouldHappenedNow => RestTimeToWait <= TimeSpan.Zero;

        public TimeEventArgs(object sender, TimeSpan timeToWait)
        {
            Sender = sender;
            TimeToWait = timeToWait;
            RestTimeToWait = new TimeSpan(timeToWait.Ticks);
        }

        public void DecreaseRestTimeToWait(TimeSpan decreaseTimeSpan)
        {
            RestTimeToWait -= decreaseTimeSpan;
        }

        public int CompareTo(TimeEventArgs other)
        {
            return this.RestTimeToWait.CompareTo(other.RestTimeToWait);
        }
    }
}
