using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Wspolbiezne.Data;
using Wspolbiezne.Logic;

namespace Tests
{
    [TestClass]
    public class BallTests
    {
        [TestMethod]
        public void BallCreationTest()
        {
            Ball ball = new Ball();
            ball.Ellipse = null;
            ball.BallRadius = 20;
            ball.CurrentPosition = new Vector2(0, 0);
            ball.Speed = 20f;

            Assert.AreEqual(ball.Speed, 20f);
            Assert.AreEqual(ball.BallRadius, 20);
            Assert.AreEqual(ball.CurrentPosition, new Vector2(0, 0));
            Assert.IsNull(ball.Ellipse);
            Assert.IsNull(ball.NextPosition);
        }

        [TestMethod]
        public void BallMoveTest()
        {
            BallManager ballManager = new BallManager();

            Ball ball = new Ball();
            ball.Ellipse = null;
            ball.BallRadius = 20;
            ball.CurrentPosition = new Vector2(0, 0);
            ball.Speed = 20f;

            Assert.IsNull(ball.NextPosition);

            ballManager.MoveBall(1920, 180, ball, 0.05f);

            Assert.IsNotNull(ball.NextPosition);
            Assert.AreNotEqual(ball.CurrentPosition, ball.NextPosition);
        }
    }
}
