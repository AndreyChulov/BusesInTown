using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusesInTown.Routes
{
    internal class RouteFactory
    {
        public static BaseRoute CreateRoute(int routeNomber)
        {
            switch (routeNomber)
            {
                case 10:
                    return new Route_10();
                case 15:
                    return new Route_15();
                case 18:
                    return new Route_18();
                default:
                    throw new ArgumentOutOfRangeException(
                        $"Can not create route with number {routeNomber}");
            }
        }

    }
}
