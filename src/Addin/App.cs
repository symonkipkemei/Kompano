using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Kompano.src.Addin
{
    public static class App
    {
        public static ObservableCollection<string> CollectedFilePaths {  get; set; }

        public static string PrimarySearchDirectory {  get; set; }

        public static string DestinationDirectory { get; set;}

        public static bool SaveChanges {  get; set; }

        public static string HostRevitFile {  get; set; }

        public static string OrientationKey {  get; set; }
      
        static App()
        {
            CollectedFilePaths = new ObservableCollection<string>();
        }
    }

}
