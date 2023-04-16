using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Numerics;
using Wspolbiezne.Presentation.Model;

namespace Wspolbiezne.Logic
{
    internal class BallManager
    {
        private readonly Random random = new Random();

        internal void CreateBall(Canvas canvas, Playground playground)
        {
            int ballRadius = playground.ballRadius;
            Brush brush = new SolidColorBrush(Color.FromRgb(
                    (byte)random.Next(1, 255),
                    (byte)random.Next(1, 255),
                    (byte)random.Next(1, 255)
                )
            );

            Ellipse ellipse = new Ellipse
            {
                Width = ballRadius,
                Height = ballRadius,
                Fill = brush,
                Stroke = Brushes.Black,
                StrokeThickness = 3
            };

            int x = random.Next(0 + ballRadius, (int)canvas.ActualWidth) - ballRadius;
            int y = random.Next(0 + ballRadius, (int)canvas.ActualHeight) - ballRadius;
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
            
            Ball ball = new Ball();
            ball.Ellipse = ellipse;
            ball.BallRadius = playground.ballRadius;
            ball.CurrentPosition = new Vector2(x, y);
            ball.Speed = 1f;

            playground.balls.Add(ball);
            playground.ellipses.Add(ellipse);
            canvas.Children.Add(ellipse);
        }

        internal void RemoveBall(Canvas canvas, Playground playground)
        {
            if (playground.ellipses.Count == 0)
                return;

            Ball ball = playground.balls.ElementAt(random.Next(0, playground.balls.Count));
            canvas.Children.Remove(ball.Ellipse);
            playground.balls.Remove(ball);
            playground.ellipses.Remove(ball.Ellipse);
        }

        internal void MoveBall(Canvas canvas, Ball ball, TextBlock block)
        {
            int ballRadius = ball.BallRadius;
            if (ball.NextPosition == null || Vector2.Distance(ball.CurrentPosition, ball.NextPosition.Value) < 5f)
            {
                ball.NextPosition = new Vector2(
                    random.Next(0, (int)canvas.ActualWidth - ballRadius),
                    random.Next(0, (int)canvas.ActualHeight - ballRadius)
                );
            }

            Vector2 nextPosition = MoveTowards(ball.CurrentPosition, ball.NextPosition.Value, ball.Speed);
            //Vector2 nextPosition = Vector2.Lerp(ball.CurrentPosition, ball.NextPosition.Value, ball.Speed);
            Canvas.SetLeft(ball.Ellipse, nextPosition.X);
            Canvas.SetTop(ball.Ellipse, nextPosition.Y);
            ball.CurrentPosition = nextPosition;
        }

        internal void SetBalls(Canvas canvas, Playground playground, int count)
        {
            int currentBallCount = playground.balls.Count;
            if (currentBallCount > count)
            {
                for (int i = 0; i < currentBallCount - count; i++)
                {
                    // Remove one random ball
                    RemoveBall(canvas, playground);
                }
            }
            else if (currentBallCount < count)
            {
                for (int i = 0; i < count - currentBallCount; i++)
                {
                    // Create one random ball
                    CreateBall(canvas, playground);
                }
            }
        }

        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            Vector2 a = target - current;
            float magnitude = a.Length();
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                return target;
            }
            return current + a / magnitude * maxDistanceDelta;
        }
    }
}
