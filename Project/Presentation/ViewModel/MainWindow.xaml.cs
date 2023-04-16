using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wspolbiezne;
using Wspolbiezne.Data;
using Wspolbiezne.Logic;
using Wspolbiezne.Presentation.Model;

namespace Wspolbiezne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BallManager ballManager;
        private Playground playground;
        private static readonly Regex numbersOnly = new Regex("^[0-9]+$");
        private Stopwatch timer = new Stopwatch();

        public MainWindow()
        {
            ballManager = new BallManager();
            playground = new Playground();
            InitializeComponent();

            timer.Start();

            CompositionTarget.Rendering += Update;
        }

        private void AddSphereButton_Click(object sender, RoutedEventArgs e)
        {
            ballManager.CreateBall(BallPlayground, playground);
        }

        private void RemoveSphereButton_Click(object sender, RoutedEventArgs e)
        {
            ballManager.RemoveBall(BallPlayground, playground);
        }

        private void OnBallCountChanged(object sender, TextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(BallCount.Text))
                return;

            ballManager.SetBalls(BallPlayground, playground, int.Parse(BallCount.Text));
        }

        private void OnBallCountKeydown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void OnBallCountInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !numbersOnly.IsMatch(e.Text);
            base.OnPreviewTextInput(e);
        }

        private void Update(object sender, EventArgs e)
        {
            float deltaTime = timer.ElapsedMilliseconds * 0.01f;

            Parallel.For(0, playground.balls.Count, i =>
            {
                ballManager.MoveBall((int)BallPlayground.ActualWidth, (int)BallPlayground.ActualHeight, playground.balls[i], deltaTime);
            });

            for (int i = 0; i < playground.balls.Count; i++)
            {
                ballManager.Render(playground.balls[i]);
            }

            if (playground.lastUpdatedBallCount != playground.balls.Count)
            {
                BallCount.Text = playground.balls.Count.ToString();
                playground.lastUpdatedBallCount = playground.balls.Count;
            }

            timer.Restart();
        }
    }
}
