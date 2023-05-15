using System.Collections.ObjectModel;
using Wspolbiezne.Data;

namespace Wspolbiezne.Presentation.Model
{
    public class Playground
    {
        public static ObservableCollection<Ball> ModelBalls
        {
            get { return modelBalls; }
        }

        public static ObservableCollection<Ball> modelBalls = new ObservableCollection<Ball>();

        public static int lastUpdatedBallCount = 0;
        public static int ballDiameter = 50;
    }
}
