using System;

namespace BusesInTown.TownWatchUtility
{
    internal class TownWatchMultiProxy : ITownClock<TownWatchSingleProxy>
    {
        TownWatch _internalTownWatch;

        public DateTime TownTime => _internalTownWatch.TownTime;

        public TimeSpan TownWatchSleepTime => _internalTownWatch.TownWatchSleepTime;

        public event EventHandler<TimeEventArgs> TimeEventOccured;

        public TownWatchMultiProxy(
            int startHourOnTownClock, TimeSpan townWatchThreadSleepTime)
        {
            var townWatchStartTime = DateTime.Now;

            townWatchStartTime = townWatchStartTime.AddHours(-townWatchStartTime.Hour + 8);
            townWatchStartTime = townWatchStartTime.AddMinutes(-townWatchStartTime.Minute);
            townWatchStartTime = townWatchStartTime.AddSeconds(-townWatchStartTime.Second);

            _internalTownWatch = new TownWatch(townWatchStartTime, townWatchThreadSleepTime);
            _internalTownWatch.TimeEventOccured += _internalTownWatch_TimeEventOccured;
        }

        private void _internalTownWatch_TimeEventOccured(object sender, TimeEventArgs e)
        {
            ((TownWatchSingleProxy)sender).Invoke(
                () => TimeEventOccured?.Invoke(sender, e)
                );
        }

        public void AddTimeEvent(TownWatchSingleProxy sender, TimeSpan timeToWait)
        {
            _internalTownWatch.Invoke(() => _internalTownWatch.AddTimeEvent(sender, timeToWait));
        }

        public void Start()
        {
            _internalTownWatch.Start();
        }

        public void Stop()
        {
            _internalTownWatch.Stop();
        }
    }
}
