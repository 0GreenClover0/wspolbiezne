using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Wspolbiezne.Data
{
    public class Ball
    {
        private Ellipse ellipse;
        public Ellipse Ellipse { get => ellipse; set => ellipse = value; }

        public int x { get; set; }
        public int y { get; set; }

        public Vector2 Direction;

        private Vector2 currentPosition;
        public Vector2 CurrentPosition { get => currentPosition; set => currentPosition = value; }

        private Vector2? nextPosition;
        public Vector2? NextPosition { get => nextPosition; set => nextPosition = value; }

        private int ballRadius;
        public int BallRadius { get => ballRadius; set => ballRadius = value; }

        private float speed;
        public float Speed { get => speed; set => speed = value; }
    }
}
