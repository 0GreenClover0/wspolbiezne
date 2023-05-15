using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Wspolbiezne.Data
{
    public class Ball : INotifyPropertyChanged
    {
        public Brush Brush { get; set; }

        public float X
        {
            get { return CurrentPosition.X; }
            set
            {
                Vector2 position = CurrentPosition;
                position.X = value;
                CurrentPosition = position;
                NotifyPropertyChanged();
            }
        }

        public float Y
        {
            get { return CurrentPosition.Y; }
            set
            {
                Vector2 position = CurrentPosition;
                position.Y = value;
                CurrentPosition = position;
                NotifyPropertyChanged();
            }
        }

        public Vector2 Velocity;

        public Vector2 CurrentPosition { get; set; }

        public float BallDiameter { get; set; }
        public float BallRadius { get; set; }

        public float Speed { get; set; }
        public float Mass { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
