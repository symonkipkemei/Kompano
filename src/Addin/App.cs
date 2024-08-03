﻿using System;
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
        public static string VisualStyle {  get; set; }

        public static string Scale {  get; set; }

        public static string DetailLevel { get; set; }

        //Export settings

        public static string ImageFormat { get; set; }
        public static string ImageQuality { get; set; }
        public static string ExportFormat { get; set; }

      
        static App()
        {
            CollectedFilePaths = new ObservableCollection<string>();
        }
    }

}
