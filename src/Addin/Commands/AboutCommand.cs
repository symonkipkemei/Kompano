using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Kompano.src.UI.About;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kompano.src.Addin.Commands
{


    // Attributes
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class AboutCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                About about = new About();
                about.ShowDialog();

            }
            catch (Exception ex)
            {
                message = $"Message: {ex.Message}\nStackTrace: {ex.StackTrace}";
                MessageBox.Show(message);
                return Result.Failed;
            }

            return Result.Succeeded;
        }
    }
}
