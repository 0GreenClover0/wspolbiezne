using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Wspolbiezne.Logic;
using Wspolbiezne.Presentation.Model;

namespace Wspolbiezne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Canvas BallPlayground;
        private BallManager ballManager;
        private static readonly Regex numbersOnly = new Regex("^[0-9]+$");

        public MainWindow()
        {
            ballManager = new BallManager();
            InitializeComponent();
            CompositionTarget.Rendering += ballManager.Update;
        }

        private void BallPlayground_Loaded(object sender, RoutedEventArgs e)
        {
            BallPlayground = (Canvas)sender;
            ballManager.CanvasWidth = BallPlayground.ActualWidth;
            ballManager.CanvasHeight = BallPlayground.ActualHeight;
        }

        private void AddSphereButton_Click(object sender, RoutedEventArgs e)
        {
            ballManager.CreateBall(BallPlayground);
        }

        private void RemoveSphereButton_Click(object sender, RoutedEventArgs e)
        {
            ballManager.RemoveBall();
        }

        private void OnBallCountChanged(object sender, TextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(BallCount.Text))
                return;

            ballManager.SetBalls(BallPlayground, int.Parse(BallCount.Text));

            if (Playground.lastUpdatedBallCount != Playground.ModelBalls.Count)
            {
                BallCount.Text = Playground.ModelBalls.Count.ToString();
                Playground.lastUpdatedBallCount = Playground.ModelBalls.Count;
            }
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
    }
}
