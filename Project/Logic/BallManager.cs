﻿using System;
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
using Wspolbiezne.Data;

namespace Wspolbiezne.Logic
{
    public class BallManager
    {
        private readonly Random random = new Random();

        public void CreateBall(Canvas canvas, Playground playground)
        {
            int ballRadius = playground.ballRadius;
            Brush brush = new SolidColorBrush(Color.FromRgb(
                    (byte)random.Next(1, 255),
                    (byte)random.Next(1, 255),
                    (byte)random.Next(1, 255)
                )
            );
            brush.Freeze();

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
            ball.Speed = 20f;

            // Random direction in X axis in range -1, 1. Y axis will be complementary to 1 (max speed)
            float directionX = (float)(random.NextDouble() + (-1) * random.Next(0, 2));

            float directionY;
            if (random.Next(0, 2) == 0)
                directionY = (float)Math.Sqrt(1 - directionX * directionX);
            else
                directionY = (float)Math.Sqrt(1 - directionX * directionX) * -1f;

            ball.Direction = new Vector2(directionX, directionY);

            playground.balls.Add(ball);
            playground.ellipses.Add(ellipse);
            canvas.Children.Add(ellipse);
        }

        public void RemoveBall(Canvas canvas, Playground playground)
        {
            if (playground.ellipses.Count == 0)
                return;

            Ball ball = playground.balls.ElementAt(random.Next(0, playground.balls.Count));
            canvas.Children.Remove(ball.Ellipse);
            playground.balls.Remove(ball);
            playground.ellipses.Remove(ball.Ellipse);
        }

        public void MoveBall(int width, int height, Ball ball, float deltaTime)
        {
            int ballRadius = ball.BallRadius;

            ball.NextPosition = ball.CurrentPosition + ball.Direction;

            Vector2 nextPosition = MoveTowards(ball.CurrentPosition, ball.NextPosition.Value, ball.Speed * deltaTime);

            if (nextPosition.X >= width - ballRadius || nextPosition.X < 0f)
                ball.Direction.X =  -ball.Direction.X;

            if (nextPosition.Y >= height - ballRadius || nextPosition.Y < 0f)
                ball.Direction.Y = -ball.Direction.Y;

            ball.CurrentPosition = nextPosition;
        }

        public void SetBalls(Canvas canvas, Playground playground, int count)
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

        public void Render(Ball ball)
        {
            Canvas.SetLeft(ball.Ellipse, ball.CurrentPosition.X);
            Canvas.SetTop(ball.Ellipse, ball.CurrentPosition.Y);
        }
    }
}
