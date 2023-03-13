using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Wspolbiezne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            Result.Text = "Wartość siły grawitacji między ciałami = " +
                PhysicsMath.CalculateGravitationalForce(
                    Convert.ToDouble(MassA.Text),
                    Convert.ToDouble(MassB.Text),
                    Convert.ToDouble(Distance.Text)
                ).ToString("E4");
        }
    }
}
