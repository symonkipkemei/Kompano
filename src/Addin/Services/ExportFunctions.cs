﻿using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kompano.src.Addin.Services
{
    public static class ExportFunctions
    {
        public static ImageExportOptions ExportSettings(string filePath)
        {
            // instanitate the image exportoptions class
            ImageExportOptions export = new ImageExportOptions();   
            export.ExportRange = App.SelectedExportRange;
            export.FilePath = filePath;
            export.ShadowViewsFileType = App.SelectedImageFileType;
            export.HLRandWFViewsFileType = App.SelectedImageFileType;
            export.ImageResolution = App.SelectedImageResolution;
            export.ZoomType = ZoomFitType.Zoom;
            export.Zoom = 100;

            return export;

        }

        public static string GetFileImagePath(Document familyDoc, string destinationDirectoryPath)
        {
            string familyName = familyDoc.Title + ".png";
            string familyImagePath = Path.Combine(destinationDirectoryPath, familyName);

            if (File.Exists(familyImagePath))
            {
                try
                {
                    File.Delete(familyImagePath);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Error Replacing the file: {ex.Message}");
                }
                
            }
           
             return familyImagePath;
        }

    }
}
