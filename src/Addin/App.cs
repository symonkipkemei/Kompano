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

        // settings for Graphics
        public static DisplayStyle SelectedDisplayStyle {  get; set; }
        public static ViewDetailLevel SelectedViewDetailLevel { get; set; }
        public static int SelectedScale {  get; set; }

        

        //Export settings

        public static ImageFileType SelectedImageFileType { get; set; }
        public static ImageResolution SelectedImageResolution { get; set; }
        public static ExportRange SelectedExportRange { get; set; }

        //Selections
        public static Dictionary<string, DisplayStyle> DisplayStyles { get; set; }
        public static Dictionary<string, ViewDetailLevel> ViewDetailLevels { get; set; }
        public static Dictionary<string, int> ScaleOptions { get; set; }

        public static Dictionary<string, ImageFileType> ImageTypes { get; set; }
        public static Dictionary<string, ImageResolution> ImageResolutions { get; set; }

        public static Dictionary<string, ExportRange> ExportRanges { get; set; }
      
        static App()
        {
            CollectedFilePaths = new ObservableCollection<string>();

            // Graphics style initiation
            DisplayStyles = new Dictionary<string, DisplayStyle>
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

            ViewDetailLevels = new Dictionary<string, ViewDetailLevel>
            {
                {"Coarse", ViewDetailLevel.Coarse },
                {"Medium", ViewDetailLevel.Medium},
                {"Fine",ViewDetailLevel.Fine},
            };

            ScaleOptions = new Dictionary<string, int>
            {
                {"1:1",1 },{"1:2",2 },{"1:5",5 },{"1:10",10 }, {"1:20",20 },{"1:50",50 },{"1:100",100 },{"1:200",200 },{"1:500",500 },{"1:1000",1000 },
            };
            // Export styles initiation

            ImageTypes = new Dictionary<string, ImageFileType>
            {
                { "BMP", ImageFileType.BMP},
                {"JPEG LossLess", ImageFileType.JPEGLossless },
                {"JPEG Medium", ImageFileType.JPEGMedium},
                {"JPEG Smallest",ImageFileType.JPEGSmallest},
                {"PNG",ImageFileType.PNG},
                {"TARGA",ImageFileType.TARGA},
                {"TIFF",ImageFileType.TIFF},
            };

            ImageResolutions = new Dictionary<string, ImageResolution>
            {
                { "DPI 72", ImageResolution.DPI_72 },
                { "DPI 150", ImageResolution.DPI_150 },
                { "DPI 300", ImageResolution.DPI_300 },
                { "DPI 600", ImageResolution.DPI_600 },
            };

            ExportRanges = new Dictionary<string, ExportRange>
            {
                {"Current View", ExportRange.CurrentView },
                {"Visible Region of Current View", ExportRange.VisibleRegionOfCurrentView},

            };
                

        }
    }

}
