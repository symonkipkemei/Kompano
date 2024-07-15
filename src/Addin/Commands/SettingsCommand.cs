using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompano.src.Addin.Services;
using System.Collections.ObjectModel;

namespace Kompano.src.Addin.Commands
{
    // Attributes
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class SettingsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uidoc = uiApp.ActiveUIDocument;
            Document familyDoc = uidoc.Document;
            Autodesk.Revit.ApplicationServices.Application app = uiApp.Application;

            try
            {
                // export images

                string destinationDirectory = @"C:\Users\Symon Kipkemei\Desktop\Photos";
                string familyImagePath = ExportFunctions.GetFileImagePath(familyDoc, destinationDirectory);



                using (Transaction trans = new Transaction(familyDoc, "Adjust 3D View Parameters"))
                {
                    trans.Start();

                    //Adjust 3D settings
                    View3D view3D = ViewFunctions.Activate3DView(familyDoc);
                    ViewFunctions.SetView3DSettings(uiApp, view3D);
                    ViewFunctions.SetZoom(uiApp, view3D);

                    trans.Commit();
                }

                ImageExportOptions exportImageSettings = ExportFunctions.ExportSettings(familyImagePath);
                familyDoc.ExportImage(exportImageSettings);

            }

            catch (Exception ex)
            {
                message = $"Message: {ex.Message} \nStackTrace: {ex.StackTrace}";
                MessageBox.Show(message);
                return Result.Failed;
            }

            return Result.Succeeded;
        }
    }
}
