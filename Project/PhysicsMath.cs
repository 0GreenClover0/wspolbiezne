using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wspolbiezne
{
    public class PhysicsMath
    {
        private const double gravitationalConstant = 6.6743015e-11;

        /// <summary>
        /// Calculates the value of the gravitational force between two bodies.
        /// </summary>
        /// <param name="massA">Mass of the first body in kilograms</param>
        /// <param name="massB">Mass of the second body in kilograms</param>
        /// <param name="distance">Distance between two bodies in meters</param>
        /// <returns></returns>
        public static double CalculateGravitationalForce(double massA, double massB, double distance)
        {
            return gravitationalConstant * massA * massB / (distance * distance);
        }
    }
}
