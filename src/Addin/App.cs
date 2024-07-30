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

        public static bool IsOrientationSet { get; set; }
        public static (XYZ eyePosition, XYZ upDirection, XYZ forwardDirection) SelectedOrientation { get; set; }

        public static Dictionary<string, (XYZ eyePosition, XYZ upDirection, XYZ forwardDirection)> orientation3D { get; set; }

            
        static App()
        {
            CollectedFilePaths = new ObservableCollection<string>();

            orientation3D = new Dictionary<string, (XYZ eyePosition, XYZ upDirection, XYZ forwardDirection)>();

            orientation3D["Orientation1"] = (new XYZ(8.261403115, -3.573411012, 3.164970061), new XYZ(-0.408248290, 0.408248290, 0.816496581), new XYZ(-0.577350269, 0.577350269, -0.577350269));
            orientation3D["Orientation2"] = (new XYZ(8.261403115, 3.364421511, 3.164970061), new XYZ(-0.408248290, -0.408248290, 0.816496581), new XYZ(-0.577350269, -0.577350269, -0.577350269));
            orientation3D["Orientation3"] = (new XYZ(1.323570591, 3.364421511, 3.164970061), new XYZ(0.408248290, -0.408248290, 0.816496581), new XYZ(0.577350269, -0.577350269, -0.577350269));
            orientation3D["Orientation4"] = (new XYZ(1.323570591, -3.573411012, 3.164970061), new XYZ(0.408248290, 0.408248290, 0.816496581), new XYZ(0.577350269, 0.577350269, -0.577350269));

            IsOrientationSet = false;
        }
    }
}
