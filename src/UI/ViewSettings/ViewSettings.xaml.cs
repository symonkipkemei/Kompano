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

        List<CheckBox> CheckBoxList = new List<CheckBox> {  };


        public ViewSettings()
        {
            InitializeComponent();

            //checkBox List
            CheckBoxList.AddRange(new List<CheckBox> { Orientation1, Orientation2, Orientation3, Orientation4 });

            //Comboboxes - Graphics Item source items
            VisualStylesCollection = new ObservableCollection<string> (App.CollectionDisplayStyles.Keys);
            ViewDetailLevelCollection = new ObservableCollection<string> (App.CollectionViewDetailLevels.Keys);
            ScaleCollection = new ObservableCollection<string> (App.CollectionScaleOptions.Keys);


            //Comboboxes - Export Item source items
            ImageQualityCollection = new ObservableCollection<string> (App.CollectionImageResolutions.Keys);
            ExportRangeCollection = new ObservableCollection<string>(App.CollectionExportRanges.Keys);
            ImageFormatCollection = new ObservableCollection<string> (App.CollectionImageTypes.Keys);

            DataContext = this;

            // set to default (Graphics)
            cbVisualStyle.SelectedItem = App.UserDisplayStyle;
            cbDetailLevel.SelectedItem = App.UserViewDetailLevel;
            cbScale.SelectedItem = App.UserScale;

            // set to default (Export settings)
            cbExportRange.SelectedItem = App.UserExportRange;
            cbRasterImageQuality.SelectedItem = App.UserImageResolution;
            cbFormat.SelectedItem = App.UserImageFileType;

            // Set to default (Isometric 3D orientation)
            SetDefaultCheckBox(App.UserOrientation3D);
        }

        public void Orientation_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.IsChecked == true)
            {
                UncheckOtherOrientations(checkBox);
                App.OrientationKey = checkBox.Name;
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
            App.UserDisplayStyle = cbVisualStyle.SelectedItem as string;
            App.UserScale = cbScale.SelectedItem as string;
            App.UserViewDetailLevel = cbDetailLevel.SelectedItem as string;
            App.UserExportRange = cbExportRange.SelectedItem as string;
            App.UserImageFileType = cbFormat.SelectedItem as string;
            App.UserImageResolution = cbRasterImageQuality.SelectedItem as string;
            App.UserOrientation3D = SelectCheckedOrientationBox();

            this.Close();
        }

        public void SetDefaultCheckBox(string orientationName)
        {
            CheckBox selectedCheckBox = CheckBoxList.FirstOrDefault(x => x.Name == orientationName);
            if (selectedCheckBox != null) { selectedCheckBox.IsChecked = true; }
         
        }

        public string SelectCheckedOrientationBox()
        {
            CheckBox selectedCheckBox = CheckBoxList.FirstOrDefault(x => x.IsChecked == true);
            if (selectedCheckBox != null) { return selectedCheckBox.Name; }
            else { return null; }
        }

    }
}
