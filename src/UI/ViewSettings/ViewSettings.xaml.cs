using Kompano.src.Addin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<string> VisualStylesCollection {  get; set; }

        public ObservableCollection<string> DetailLevelCollection { get; set; }
        public ObservableCollection<string>  ScaleCollection  { get; set; }

        public ViewSettings()
        {
            InitializeComponent();
            Orientation1.IsChecked = true;
            App.OrientationKey = Orientation1.Name;

            //comboboxes
            VisualStylesCollection = new ObservableCollection<string> { "Hidden Line", "Shaded", "Consitent Colors", "Realistic" };
            DetailLevelCollection = new ObservableCollection<string> { "Coarse", "Medium", "Fine" };
            ScaleCollection = new ObservableCollection<string> { "1:1", "1:2", "1:5", "1:10", "1:20", "1:50", "1:100", "1:200", "1:500", "1:1000" };

            DataContext = this;

            // set to default
            cbVisualStyle.SelectedItem = VisualStylesCollection[1];
            cbDetailLevel.SelectedItem = DetailLevelCollection[2];
            cbScale.SelectedItem = ScaleCollection[0];

            

        }

        private void Orientation1_Click(object sender, RoutedEventArgs e)
        {
            if((bool)Orientation1.IsChecked)
            {
                UncheckOtherOrientations(Orientation1);
                App.OrientationKey = Orientation1.Name;

            }
            
        }

        private void Orientation2_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Orientation2.IsChecked)
            {
                UncheckOtherOrientations(Orientation2);
                App.OrientationKey = Orientation2.Name;

            }
        }

        private void Orientation3_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Orientation3.IsChecked)
            {
                UncheckOtherOrientations(Orientation3);
                App.OrientationKey = Orientation3.Name;
            }
                
        }

        private void Orientation4_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Orientation4.IsChecked)
            {
                UncheckOtherOrientations(Orientation4);
                App.OrientationKey = Orientation4.Name;
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
            this.Close();
        }

        private void cbVisualStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.VisualStyle = cbVisualStyle.SelectedItem as string;
        }

        private void cbScale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Scale = cbScale.SelectedItem as string;
        }

        private void cbDetailLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.DetailLevel = cbDetailLevel.SelectedItem as string;
        }
    }
}
