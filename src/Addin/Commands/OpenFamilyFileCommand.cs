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

namespace Kompano.src.Addin.Commands
{
    // Attributes
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class OpenFamilyFileCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uidoc = uiApp.ActiveUIDocument;
            Document doc = uidoc.Document;
            Autodesk.Revit.ApplicationServices.Application app = uiApp.Application;

            try
            {
                string familyPath = @"C:\Users\Symon Kipkemei\Desktop\Assembly_5111-PRHS300QPRV-3_02-07-2024.rfa";

                if (!File.Exists(familyPath))
                {
                    MessageBox.Show("Family not found");
                    return Result.Failed;
                }

                //Provides access to the family doc
                Document familyDoc = app.OpenDocumentFile(familyPath);

                //Opens it in the UI interface
                uiApp.OpenAndActivateDocument(familyPath);

                MessageBox.Show("Family opened successfully");
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

