using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BusesInTown.TownWatchUtility
{
    internal class TownWatch : TownWatchSingleProxy, ITownClock<object>
    {
        public DateTime TownTime { get; private set; }
        public TimeSpan TownWatchSleepTime { get; }

        public event EventHandler<TimeEventArgs> TimeEventOccured;

        private List<TimeEventArgs> _timeEvents;
        private Thread _timeThread;

        private object _timeEventsLockObject = new object();

        public TownWatch(DateTime townTime, TimeSpan townWatchSleepTime)
        {
            TownTime = townTime;
            TownWatchSleepTime = townWatchSleepTime;
            _timeEvents = new List<TimeEventArgs>();
            _timeThread = new Thread(TimeThreadWorker);
        }

        public override void Start()
        {
            base.Start();
            _timeThread.Start();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public void AddTimeEvent(object sender, TimeSpan timeToWait)
        {
            lock (_timeEventsLockObject)
            {
                _timeEvents.Add(new TimeEventArgs(sender, timeToWait));
                _timeEvents.Sort();
            }
        }

        private void TimeThreadWorker()
        {
            while (_isRunning)
            {
                Action startEventAction = () => { };

                lock (_timeEventsLockObject)
                {
                    if (_timeEvents.Count == 0)
                    {
                        continue;
                    }

                    TimeEventArgs timeEvent = _timeEvents.First();

                    foreach (TimeEventArgs timeEventArgs in _timeEvents)
                    {
                        timeEventArgs.DecreaseRestTimeToWait(timeEvent.TimeToWait);
                    }

                    TownTime += timeEvent.TimeToWait;

                    _timeEvents = _timeEvents.Skip(1).ToList();
                    
                    startEventAction = () => TimeEventOccured?.Invoke(timeEvent.Sender, timeEvent);
                }

                startEventAction();

                Thread.CurrentThread.Join(TownWatchSleepTime);
            }
        }
    }
}
