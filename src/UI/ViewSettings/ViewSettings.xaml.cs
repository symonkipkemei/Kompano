using Autodesk.Revit.DB;
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
        public ObservableCollection<string> ViewDetailLevelCollection { get; set; }
        public ObservableCollection<string>  ScaleCollection  { get; set; }

        public ObservableCollection <string> ImageQualityCollection { get; set; }
        public ObservableCollection<string> ExportRangeCollection { get; set; }
        public ObservableCollection<string> ImageFormatCollection { get; set; }


        public ViewSettings()
        {
            InitializeComponent();

            

            //Comboboxes - Graphics Item source items
            VisualStylesCollection = new ObservableCollection<string> (App.DisplayStyles.Keys);
            ViewDetailLevelCollection = new ObservableCollection<string> (App.ViewDetailLevels.Keys);
            ScaleCollection = new ObservableCollection<string> (App.ScaleOptions.Keys);


            //Comboboxes - Export Item source items
            ImageQualityCollection = new ObservableCollection<string> (App.ImageResolutions.Keys);
            ExportRangeCollection = new ObservableCollection<string>(App.ExportRanges.Keys);
            ImageFormatCollection = new ObservableCollection<string> (App.ImageTypes.Keys);

            DataContext = this;

            // set to default (Graphics)
            cbVisualStyle.SelectedItem = VisualStylesCollection[2];
            cbDetailLevel.SelectedItem = ViewDetailLevelCollection[2];
            cbScale.SelectedItem = ScaleCollection[0];

            // set to default (Export settings)
            cbExportRange.SelectedItem = ExportRangeCollection[0];
            cbRasterImageQuality.SelectedItem = ImageQualityCollection[2];
            cbFormat.SelectedItem = ImageFormatCollection[4];

            // Set to default (Isometric 3D orientation)
            Orientation1.IsChecked = true;
            App.OrientationKey = Orientation1.Name;
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
            App.SelectedDisplayStyle = App.DisplayStyles[cbVisualStyle.SelectedItem as string];
        }

        private void cbScale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.SelectedScale = App.ScaleOptions[cbScale.SelectedItem as string];
        }

        private void cbDetailLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.SelectedViewDetailLevel = App.ViewDetailLevels[cbDetailLevel.SelectedItem as string];
        }

        private void cbRasterImageQuality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.SelectedImageResolution = App.ImageResolutions[cbRasterImageQuality.SelectedItem as string];
        }

        private void cbExportRange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.SelectedExportRange = App.ExportRanges[cbExportRange.SelectedItem as string];
        }

        private void cbFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.SelectedImageFileType = App.ImageTypes[cbFormat.SelectedItem as string];
        }
    }
}
