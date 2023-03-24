using BusesInTown.Buses;
using BusesInTown.Routes;
using BusesInTown.TownWatchUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusesInTown
{
    public partial class Form1 : Form
    {
        private delegate void AddTextToLogHandle(string text);
        private TownWatchMultiProxy _townWatch;

        public Form1()
        {
            InitializeComponent();

            _townWatch = new TownWatchMultiProxy(8, TimeSpan.FromSeconds(1));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AddTextToLog("Симулятор загружен");

            #region create routes
            BaseRoute[] routes = new[] { 
                RouteFactory.CreateRoute(10),
                RouteFactory.CreateRoute(15),
                RouteFactory.CreateRoute(18),
                    };
            #endregion

            Bus[] buses = new Bus[]
            {
                new Bus(routes[0]), //10-1
                new Bus(routes[0]), //10-2
                new Bus(routes[1]), //15-1
                new Bus(routes[2]), //18-1
                new Bus(routes[2]), //18-2
                new Bus(routes[2]), //18-3
            };

            buses.ToList().ForEach(bus => bus.BusOnStartStation += Bus_BusOnStartStation);
            buses.ToList().ForEach(bus => bus.Start());

            _townWatch.Start();

            /*_townWatch.Invoke(() => _townWatch.AddTimeEvent($"[{Thread.CurrentThread.ManagedThreadId}]bus1", TimeSpan.FromMinutes(5)));
            _townWatch.Invoke(() => _townWatch.AddTimeEvent($"[{Thread.CurrentThread.ManagedThreadId}]bus2", TimeSpan.FromMinutes(3)));
            _townWatch.Invoke(() => _townWatch.AddTimeEvent($"[{Thread.CurrentThread.ManagedThreadId}]bus3", TimeSpan.FromMinutes(7)));
            _townWatch.AddTimeEvent($"[{Thread.CurrentThread.ManagedThreadId}]bus01", TimeSpan.FromMinutes(5));
            _townWatch.AddTimeEvent($"[{Thread.CurrentThread.ManagedThreadId}]bus02", TimeSpan.FromMinutes(3));
            _townWatch.AddTimeEvent($"[{Thread.CurrentThread.ManagedThreadId}]bus03", TimeSpan.FromMinutes(7));
            _townWatch.TimeEventOccured += TownWatch_TimeEventOccured;
            _townWatch.Start();*/
        }

        private void Bus_BusOnStartStation(object sender, EventArgs e)
        {
            Bus bus = (Bus)sender;

            AddTextToLog($"[{Thread.CurrentThread.ManagedThreadId}]Bus No {bus.BusId} is on the route {bus.BusNumber} at start station");
        }

        private void TownWatch_TimeEventOccured(object sender, TimeEventArgs e)
        {
            var bus = sender as string;

            AddTextToLog($"{bus} is on the station at {_townWatch.TownTime} town clock");

            switch (bus)
            {
                case "bus1":
                    //_townWatch.AddTimeEvent("bus11", TimeSpan.FromMinutes(7));
                    break;
                case "bus2":
                    //_townWatch.AddTimeEvent("bus22", TimeSpan.FromMinutes(5));
                    break;
                case "bus3":
                    break;
                default:
                    break;
            }
        }

        private void AddTextToLog(string text)
        {
            if (rtxtLog.InvokeRequired)
            {
                rtxtLog.Invoke((AddTextToLogHandle)AddTextToLog, text);
            }
            else
            {
                rtxtLog.AppendText($"{DateTime.Now.ToLongTimeString()} => {text}\n");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _townWatch.Stop();
        }
    }
}
