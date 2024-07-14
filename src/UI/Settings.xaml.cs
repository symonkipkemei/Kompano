using Kompano.src.Addin;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kompano.src.UI
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        public void PrimaryBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedPath = BrowseForFolder();
            if ( !string.IsNullOrEmpty(selectedPath))
            {
                PrimaryDirectoryTextBox.Text = selectedPath;
                App.PrimarySearchDirectory = selectedPath;
            }
        }


        public void DestinationBrowseButton_Click(Object sender, RoutedEventArgs e) 
        {
            string selectedPath = BrowseForFolder();
            if (!string.IsNullOrEmpty(selectedPath))
            {
                DestinationDirectoryTextBox.Text = selectedPath;
                App.DestinationDirectory = selectedPath;
            }
        }

        public String BrowseForFolder()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && !String.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    return dialog.SelectedPath;
                }

                return null;
            }

        }

        private void SaveChangesCheckBox_Click(object sender, RoutedEventArgs e)
        {
            App.SaveChanges = SaveChangesCheckBox.IsChecked ?? false;

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {


        }
    }
}
