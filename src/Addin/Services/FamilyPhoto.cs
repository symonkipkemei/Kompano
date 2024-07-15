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

            if (App.CollectedFilePaths.Count != 0)
            {
                foreach (string familyPath in App.CollectedFilePaths)
                {
                    //Open the file
                    var (familyDoc, familyUiDoc) = FamilyFunctions.OpenFamilyFile(uiApp, app, familyPath);
                    View3D view3D = ViewFunctions.Activate3DView(familyDoc);
                    familyUiDoc.ActiveView = view3D;


                    using (Transaction trans = new Transaction(familyDoc, "Adjust 3D View "))
                    {
                        trans.Start("Adjust 3D");
              
                        //Allow the preview to load, delay by 30s
                        // Thread.Sleep(30000);

                        ViewFunctions.SetView3DSettings(view3D);
                        

                        trans.Commit();
                    }

                    using (Transaction trans = new Transaction(familyDoc, "Adjust Zoom "))
                    {
                        trans.Start("Adjust Zoom");
                        ViewFunctions.SetZoom(familyUiDoc, view3D);

                        trans.Commit();
                    }



                   // export images
                    string familyImagePath = ExportFunctions.GetFileImagePath(familyDoc, App.DestinationDirectory);
                    ImageExportOptions exportImageSettings = ExportFunctions.ExportSettings(familyImagePath);
                    familyDoc.ExportImage(exportImageSettings);


                    //close the family file
                    
                    //FamilyFunctions.CloseFamilyFile(uiApp,app,familyDoc);
                }

            }

            else
            {
                MessageBox.Show("No .rfa files found in primary directory");
                throw new FileNotFoundException("No .rfa files found in primary directory");

            }

        }
    }
}

         
