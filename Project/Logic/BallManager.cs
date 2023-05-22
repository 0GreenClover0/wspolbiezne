using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Numerics;
using Wspolbiezne.Presentation.Model;
using Wspolbiezne.Data;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;

namespace Wspolbiezne.Logic
{
    public class BallManager
    {
        public double CanvasWidth { get; set; }
        public double CanvasHeight { get; set; }

        private readonly Random random = new Random();
        private Stopwatch timer = new Stopwatch();

        public BallManager()
        {
            timer.Start();
        }

        public void CreateBall(Canvas canvas)
        {
            int ballDiameter = Playground.ballDiameter;
            Brush brush = new SolidColorBrush(Color.FromRgb(
                    (byte)random.Next(1, 255),
                    (byte)random.Next(1, 255),
                    (byte)random.Next(1, 255)
                )
            );
            brush.Freeze();
            
            Ball ball = new Ball();
            ball.BallDiameter = ballDiameter;
            ball.BallRadius = ballDiameter / 2f;
            ball.Brush = brush;
            ball.Mass = (random.NextSingle() + 0.5f) * 1.5f;

            bool colliding = true;
            float x = 0f, y = 0f;
            while (colliding)
            {
                x = random.Next(0 + ballDiameter, (int)canvas.ActualWidth) - ballDiameter;
                y = random.Next(0 + ballDiameter, (int)canvas.ActualHeight) - ballDiameter;

                colliding = false;
                for (int i = 0; i < Playground.ModelBalls.Count; i++)
                {
                    float distanceSquared = MathF.Pow(x - Playground.ModelBalls[i].X, 2f) + MathF.Pow(y - Playground.ModelBalls[i].Y, 2f);

                    if (distanceSquared < MathF.Pow(ball.BallRadius + Playground.ModelBalls[i].BallRadius, 2f))
                    { 
                        colliding = true;
                        break;
                    }
                }
            }
            ball.X = x;
            ball.Y = y;
            ball.Speed = 20f;

            // Random direction in X axis in range -1, 1. Y axis will be complementary to 1 (max speed)
            float directionX = (float)(random.NextDouble() + (-1) * random.Next(0, 2));

            float directionY;
            if (random.Next(0, 2) == 0)
                directionY = (float)Math.Sqrt(1 - directionX * directionX);
            else
                directionY = (float)Math.Sqrt(1 - directionX * directionX) * -1f;

            ball.Velocity = new Vector2(directionX, directionY);

            Playground.ModelBalls.Add(ball);
        }

        public void RemoveBall()
        {
            if (Playground.ModelBalls.Count == 0)
                return;

            Playground.ModelBalls.RemoveAt(random.Next(0, Playground.ModelBalls.Count));
        }

        public void MoveBall(double width, double height, Ball ball, float deltaTime)
        {
            Vector2 nextPosition = CalculateNextPosition(ball, deltaTime);

            if ((nextPosition.X >= width - ball.BallDiameter && ball.Velocity.X > 0f) || (nextPosition.X < 0f && ball.Velocity.X < 0f))
                ball.Velocity.X =  -ball.Velocity.X;

            if ((nextPosition.Y >= height - ball.BallDiameter && ball.Velocity.Y > 0f) || (nextPosition.Y < 0f && ball.Velocity.Y < 0f))
                ball.Velocity.Y = -ball.Velocity.Y;

            nextPosition = CalculateNextPosition(ball, deltaTime);
            float x = nextPosition.X;
            float y = nextPosition.Y;

            int ballCount = Playground.ModelBalls.Count;
            for (int i = 0; i < ballCount; i++)
            {
                lock (Playground.ModelBalls[i])
                {
                    float distanceSquared = MathF.Pow(x - Playground.ModelBalls[i].X, 2f) + MathF.Pow(y - Playground.ModelBalls[i].Y, 2f);

                    if (distanceSquared >= MathF.Pow(ball.BallRadius + Playground.ModelBalls[i].BallRadius, 2f))
                        continue;

                    if (Playground.ModelBalls[i] == ball)
                        continue;
                
                    float distance = MathF.Sqrt(distanceSquared);

                    if (distance == 0f)
                        continue;

                    Vector2 normal = (Playground.ModelBalls[i].CurrentPosition - ball.CurrentPosition) / distance;
                    float p = (2f / (ball.Mass + Playground.ModelBalls[i].Mass)) * (ball.Velocity * normal - Playground.ModelBalls[i].Velocity * normal).Length();
                    ball.Velocity -= p * ball.Mass * normal;
                    Playground.ModelBalls[i].Velocity += p * Playground.ModelBalls[i].Mass * normal;
                }
            }

            nextPosition = CalculateNextPosition(ball, deltaTime);
            ball.X = nextPosition.X;
            ball.Y = nextPosition.Y;
        }

        private Vector2 CalculateNextPosition(Ball ball, float deltaTime)
        {
            return MoveTowards(ball.CurrentPosition, ball.CurrentPosition + ball.Velocity, ball.Speed * deltaTime);
        }

        public void SetBalls(Canvas canvas, int count)
        {
            int currentBallCount = Playground.ModelBalls.Count;
            if (currentBallCount > count)
            {
                for (int i = 0; i < currentBallCount - count; i++)
                {
                    // Remove one random ball
                    RemoveBall();
                }
            }
            else if (currentBallCount < count)
            {
                for (int i = 0; i < count - currentBallCount; i++)
                {
                    // Create one random ball
                    CreateBall(canvas);
                }
            }
        }

        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            if (maxDistanceDelta == 0f)
                return current;

            Vector2 a = target - current;
            float magnitude = a.Length();

            if (magnitude <= maxDistanceDelta || magnitude == 0f)
                return target;

            return current + a / magnitude * maxDistanceDelta;
        }

        public void Update(object sender, EventArgs e)
        {
            float deltaTime = timer.ElapsedMilliseconds * 0.01f;

            Task.WhenAll(
                from ball in Playground.ModelBalls
                select Task.Run(() => MoveBall(CanvasWidth, CanvasHeight, ball, deltaTime))
            );

            timer.Restart();
        }
    }
}
