using System.Numerics;
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
            ball.BallDiameter = 40f;
            ball.BallRadius = 20f;
            ball.CurrentPosition = new Vector2(0, 0);
            ball.Speed = 20f;
            ball.Mass = 2f;
            ball.Velocity = new Vector2(2f, 2f);

            Assert.AreEqual(ball.Speed, 20f);
            Assert.AreEqual(ball.BallRadius, 20f);
            Assert.AreEqual(ball.BallDiameter, 40f);
            Assert.AreEqual(ball.Mass, 2f);
            Assert.AreEqual(ball.CurrentPosition, new Vector2(0, 0));
            Assert.AreEqual(ball.Velocity, new Vector2(2f, 2f));
        }

        [TestMethod]
        public void BallMoveTest()
        {
            BallManager ballManager = new BallManager();

            Ball ball = new Ball();
            ball.BallDiameter = 40f;
            ball.BallRadius = 20f;
            ball.CurrentPosition = new Vector2(100, 100);
            ball.Velocity = new Vector2(1f, 1f);
            ball.Speed = 20f;
            ball.Mass = 1f;

            var currentPos = ball.CurrentPosition;
            Assert.IsNotNull(currentPos);

            ballManager.MoveBall(ball);

            Assert.AreNotEqual(ball.CurrentPosition, currentPos);
        }
    }
}
