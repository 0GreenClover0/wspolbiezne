using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Wspolbiezne.Logic;

namespace Wspolbiezne.Presentation.Model
{
    internal class Playground
    {
        public int ballRadius = 50;
        public List<Ball> balls = new List<Ball>();
        public List<Ellipse> ellipses = new List<Ellipse>();
    }
}
