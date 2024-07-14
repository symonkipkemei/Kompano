using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kompano.src.Addin.Services
{
    public static class FamilyPhoto
    {
        public static void FamilyPhotoFunction(ExternalCommandData commandData)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uidoc = uiApp.ActiveUIDocument;
            Document doc = uidoc.Document;
            Autodesk.Revit.ApplicationServices.Application app = uiApp.Application;

            // Clear previous file paths
            App.CollectedFilePaths.Clear();

            FamilyFunctions.SearchRfaFiles(App.PrimarySearchDirectory, App.CollectedFilePaths);

            if (App.CollectedFilePaths.Count != 0)
            {
                foreach (string familyPath in App.CollectedFilePaths)
                {
                    //Open the file
                    Document familyDoc = FamilyFunctions.OpenFamilyFile(uiApp, app, familyPath);

                    using (Transaction trans = new Transaction(familyDoc, "Adjust 3D View Parameters"))
                    {
                        trans.Start();

                        //Adjust 3D settings
                        View3D view3D = ViewFunctions.GetOrCreate3DView(familyDoc);
                        ViewFunctions.SetView3DSettings(uiApp, view3D);
                        ViewFunctions.SetZoom(uiApp, view3D);

                        trans.Commit();
                    }


                    // export images
                    string familyImagePath = ExportFunctions.GetFileImagePath(familyDoc, App.PrimarySearchDirectory);
                    ImageExportOptions exportImageSettings = ExportFunctions.ExportSettings(familyImagePath);
                    familyDoc.ExportImage(exportImageSettings);


                    //close the family file
                    bool saveChanges = App.SaveChanges;
                    familyDoc.Close(saveChanges);

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

         
