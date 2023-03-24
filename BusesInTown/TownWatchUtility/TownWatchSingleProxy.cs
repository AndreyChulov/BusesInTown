using System;
using System.Collections.Generic;
using System.Threading;

namespace BusesInTown.TownWatchUtility
{
    internal class TownWatchSingleProxy
    {
        private Queue<Action> _actionQueue;
        private Thread _currentThread;
        private Object _actionQueueLockObj;

        protected bool _isRunning;

        public bool IsRunning => _isRunning;

        public TownWatchSingleProxy()
        {
            _actionQueue = new Queue<Action>();
            _currentThread = new Thread(ThreadFunction);
            _isRunning = false;
            _actionQueueLockObj = new Object();
        }

        public void Invoke(Action actionToInvoke)
        {
            lock (_actionQueueLockObj)
            {
                _actionQueue.Enqueue(actionToInvoke);
            }
        }

        public virtual void Start()
        {
            _isRunning = true;
            _currentThread.Start();
        }

        public virtual void Stop()
        {
            _isRunning = false;
        }

        private void ThreadFunction()
        {
            while (_isRunning)
            {
                if (_actionQueue.Count > 0)
                {
                    lock (_actionQueueLockObj)
                    {
                        _actionQueue.Dequeue()();
                    }
                }

                Thread.Sleep(100);
            }
        }
    }
}
