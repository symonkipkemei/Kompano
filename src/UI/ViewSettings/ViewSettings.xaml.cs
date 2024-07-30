using Kompano.src.Addin;
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

namespace Kompano.src.UI.ViewSettings
{
    /// <summary>
    /// Interaction logic for ViewSettings.xaml
    /// </summary>
    public partial class ViewSettings : Window
    {
        public ViewSettings()
        {
            InitializeComponent();
            Orientation1.IsChecked = true;
            App.SelectedOrientation = App.orientation3D[Orientation1.Name];
            App.IsOrientationSet = true;
        }

        private void Orientation1_Click(object sender, RoutedEventArgs e)
        {
            if((bool)Orientation1.IsChecked)
            {
                UncheckOtherOrientations(Orientation1);
                App.SelectedOrientation = App.orientation3D[Orientation1.Name];
                App.IsOrientationSet = true;

            }
            
        }

        private void Orientation2_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Orientation2.IsChecked)
            {
                UncheckOtherOrientations(Orientation2);
                App.SelectedOrientation = App.orientation3D[Orientation2.Name];
                App.IsOrientationSet = true;
                
            }
        }

        private void Orientation3_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Orientation3.IsChecked)
            {
                UncheckOtherOrientations(Orientation3);
                App.SelectedOrientation = App.orientation3D[Orientation3.Name];
                App.IsOrientationSet = true;
            }
                
        }

        private void Orientation4_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Orientation4.IsChecked)
            {
                UncheckOtherOrientations(Orientation4);
                App.SelectedOrientation = App.orientation3D[Orientation4.Name];
                App.IsOrientationSet = true;
            }
                
        }


        private void UncheckOtherOrientations(CheckBox selectedCheckBox)
        {
            if (selectedCheckBox != Orientation1) Orientation1.IsChecked = false;
            if (selectedCheckBox != Orientation2) Orientation2.IsChecked = false;
            if (selectedCheckBox != Orientation3) Orientation3.IsChecked = false;
            if (selectedCheckBox != Orientation4) Orientation4.IsChecked = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Orientation set to: {App.SelectedOrientation}");
            this.Close();
        }
    }
}
