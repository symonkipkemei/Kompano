using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.UI;

namespace Kompano.src.Addin
{
    public static class App
    {
        public static ObservableCollection<string> CollectedFilePaths { get; set; }
        public static string PrimarySearchDirectory { get; set; }
        public static string DestinationDirectory { get; set; }
        public static bool SaveChanges { get; set; }
        public static string HostRevitFile { get; set; }
        public static string OrientationKey { get; set; }

        // settings for Graphics & Export
        public static DisplayStyle SelectedDisplayStyle => CollectionDisplayStyles[UserDisplayStyle];
        public static ViewDetailLevel SelectedViewDetailLevel => CollectionViewDetailLevels[UserViewDetailLevel];
        public static int SelectedScale => CollectionScaleOptions[UserScale];
        public static ImageFileType SelectedImageFileType => CollectionImageTypes[UserImageFileType];
        public static ImageResolution SelectedImageResolution => CollectionImageResolutions[UserImageResolution];
        public static ExportRange SelectedExportRange => CollectionExportRanges[UserExportRange];
        public static (XYZ eyePosition, XYZ upDirection, XYZ forwardDirection) SelectedOrientation3D => CollectionOrientation3D[UserOrientation3D];

        // User Selection stored when saved
        public static string UserDisplayStyle { get; set; }
        public static string UserViewDetailLevel { get; set; }
        public static string UserScale { get; set; }
        public static string UserImageFileType { get; set; }
        public static string UserImageResolution { get; set; }
        public static string UserExportRange { get; set; }
        public static string UserOrientation3D { get; set; }

        //Collections
        public static Dictionary<string, DisplayStyle> CollectionDisplayStyles { get; set; }
        public static Dictionary<string, ViewDetailLevel> CollectionViewDetailLevels { get; set; }
        public static Dictionary<string, int> CollectionScaleOptions { get; set; }
        public static Dictionary<string, ImageFileType> CollectionImageTypes { get; set; }
        public static Dictionary<string, ImageResolution> CollectionImageResolutions { get; set; }
        public static Dictionary<string, ExportRange> CollectionExportRanges { get; set; }
        public static Dictionary<string, (XYZ eyePosition, XYZ upDirection, XYZ forwardDirection)> CollectionOrientation3D { get; set; }

        static App()
        {
            CollectedFilePaths = new ObservableCollection<string>();

            // Graphics style collection initiated
            CollectionDisplayStyles = new Dictionary<string, DisplayStyle>
            {
                {"Wireframe", DisplayStyle.Wireframe },
                {"Hidden Line", DisplayStyle.HLR},
                {"Shading", DisplayStyle.Shading },
                {"Shading With Edges", DisplayStyle.ShadingWithEdges },
                {"Rendering", DisplayStyle.Rendering},
                {"Realistic", DisplayStyle.Realistic},
                {"Flat Colors", DisplayStyle.FlatColors },
                {"Realistic With Edges", DisplayStyle.RealisticWithEdges},
            };

            CollectionViewDetailLevels = new Dictionary<string, ViewDetailLevel>
            {
                {"Coarse", ViewDetailLevel.Coarse },
                {"Medium", ViewDetailLevel.Medium},
                {"Fine",ViewDetailLevel.Fine},
            };

            CollectionScaleOptions = new Dictionary<string, int>
            {
                {"1:1",1 },{"1:2",2 },{"1:5",5 },{"1:10",10 }, {"1:20",20 },{"1:50",50 },{"1:100",100 },{"1:200",200 },{"1:500",500 },{"1:1000",1000 },
            };
            // Export styles collection initiated

            CollectionImageTypes = new Dictionary<string, ImageFileType>
            {
                { "BMP", ImageFileType.BMP},
                {"JPEG LossLess", ImageFileType.JPEGLossless },
                {"JPEG Medium", ImageFileType.JPEGMedium},
                {"JPEG Smallest",ImageFileType.JPEGSmallest},
                {"PNG",ImageFileType.PNG},
                {"TARGA",ImageFileType.TARGA},
                {"TIFF",ImageFileType.TIFF},
            };

            CollectionImageResolutions = new Dictionary<string, ImageResolution>
            {
                { "DPI 72", ImageResolution.DPI_72 },
                { "DPI 150", ImageResolution.DPI_150 },
                { "DPI 300", ImageResolution.DPI_300 },
                { "DPI 600", ImageResolution.DPI_600 },
            };

            CollectionExportRanges = new Dictionary<string, ExportRange>
            {
                {"Current View", ExportRange.CurrentView },
                {"Visible Region of Current View", ExportRange.VisibleRegionOfCurrentView},

            };

            // 3D orientations

            CollectionOrientation3D = new Dictionary<string, (XYZ eyePosition, XYZ upDirection, XYZ forwardDirection)>

            {   //Orientation Numbered from Front Isometric- Anticlockwise ( Front,Rights, Back, Left)
                {"Orientation1", (new XYZ(8.261403115, -3.573411012, 3.164970061), new XYZ(-0.408248290, 0.408248290, 0.816496581), new XYZ(-0.577350269, 0.577350269, -0.577350269)) },
                {"Orientation2",  (new XYZ(8.261403115, 3.364421511, 3.164970061), new XYZ(-0.408248290, -0.408248290, 0.816496581), new XYZ(-0.577350269, -0.577350269, -0.577350269))},
                {"Orientation3", (new XYZ(1.323570591, 3.364421511, 3.164970061), new XYZ(0.408248290, -0.408248290, 0.816496581), new XYZ(0.577350269, -0.577350269, -0.577350269)) },
                {"Orientation4", (new XYZ(1.323570591, -3.573411012, 3.164970061), new XYZ(0.408248290, 0.408248290, 0.816496581), new XYZ(0.577350269, 0.577350269, -0.577350269)) },

            };

            SetDefaultSettings();

        }

        public static void SetDefaultSettings()
        {
            // Default Options set/saved
            UserDisplayStyle = CollectionDisplayStyles.Keys.ToList()[2];
            UserScale = CollectionScaleOptions.Keys.ToList()[0];
            UserViewDetailLevel = CollectionViewDetailLevels.Keys.ToList()[2];
            UserExportRange = CollectionExportRanges.Keys.ToList()[0];
            UserImageFileType = CollectionImageTypes.Keys.ToList()[4];
            UserImageResolution = CollectionImageResolutions.Keys.ToList()[2];
            UserOrientation3D = CollectionOrientation3D.Keys.ToList()[0];
        }

    }
}
