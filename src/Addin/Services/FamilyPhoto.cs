using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Kompano.src.Addin.Services
{
    public static class FamilyPhoto
    {
        public static void FamilyPhotoFunction(ExternalCommandData commandData)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uidoc = uiApp.ActiveUIDocument;
            Document activeDoc = uidoc.Document;
            Autodesk.Revit.ApplicationServices.Application app = uiApp.Application;

            // Clear previous file paths
            App.CollectedFilePaths.Clear();

            FamilyFunctions.SearchRfaFiles(App.PrimarySearchDirectory, App.CollectedFilePaths);
            int totalNoFiles = App.CollectedFilePaths.Count;
            int count = 0;

            ProgressHandler progressHandler = new ProgressHandler();

            if (totalNoFiles != 0)
            {
                progressHandler.ShowProgressBar();

                foreach (string familyPath in App.CollectedFilePaths)
                {

                    try
                    {
                        //Open the file
                        var (familyDoc, familyUiDoc) = FamilyFunctions.OpenFamilyFile(uiApp, app, familyPath);
                        View3D view3D = ViewFunctions.Activate3DView(familyDoc);
                        familyUiDoc.ActiveView = view3D;


                        using (Transaction trans = new Transaction(familyDoc, "Adjust 3D View"))
                        {
                            trans.Start();

                            ViewFunctions.SetView3DSettings(view3D);
                            ViewFunctions.SetZoom(familyUiDoc, view3D);

                            trans.Commit();
                        }

                        // export images
                        string familyImagePath = ExportFunctions.GetFileImagePath(familyDoc, App.DestinationDirectory);
                        ImageExportOptions exportImageSettings = ExportFunctions.ExportSettings(familyImagePath);
                        familyDoc.ExportImage(exportImageSettings);


                        //close the family file

                        FamilyFunctions.CloseFamilyFile(uiApp, app, familyDoc);

                        count++;
                        int percentage = (count * 100 ) / totalNoFiles;
                        progressHandler.UpdateProgress(percentage);

                    }

                    catch (Exception ex) 
                    {
                        MessageBox.Show($"Error processing {familyPath}: {ex.Message}");
                    }
                    

                }

                progressHandler.CloseProgressBar();

            }

            else
            {
                MessageBox.Show("No .rfa files found in primary directory");
                throw new FileNotFoundException("No .rfa files found in primary directory");

            }

        }
    }
}

         
