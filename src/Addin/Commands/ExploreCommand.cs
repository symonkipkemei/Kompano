using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompano.src.Addin.Services;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Kompano.src.Addin.Commands
{
    // Attributes
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class ExploreCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // Command data
                UIApplication uiApp = commandData.Application;
                UIDocument uidoc = uiApp.ActiveUIDocument;
                Document activeDoc = uidoc.Document;
                Autodesk.Revit.ApplicationServices.Application app = uiApp.Application;

                // Get the active view
                View activeView = activeDoc.ActiveView;

                // Ensure the active view is a 3D view
                if (activeView is View3D view3D)
                {
                    // Get the current orientation of the 3D view
                    ViewOrientation3D orientation = view3D.GetOrientation();

                    // Retrieve the eye position, up direction, and forward direction
                    XYZ eyePosition = orientation.EyePosition;
                    XYZ upDirection = orientation.UpDirection;
                    XYZ forwardDirection = orientation.ForwardDirection;

                    // Create the message string
                    string infoMessage = $"Eye Position: {eyePosition}\n" +
                                         $"Up Direction: {upDirection}\n" +
                                         $"Forward Direction: {forwardDirection}";

                    // Show MessageBox with options
                    var result = MessageBox.Show(infoMessage, "View Orientation", MessageBoxButton.OKCancel, MessageBoxImage.Information);

                    // If Cancel is clicked, copy the text to the clipboard
                    if (result == MessageBoxResult.Cancel)
                    {
                        Clipboard.SetText(infoMessage);
                        MessageBox.Show("Text copied to clipboard!", "Copied", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("The active view is not a 3D view.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                message = $"Message: {ex.Message} \nStackTrace: {ex.StackTrace}";
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return Result.Failed;
            }

            return Result.Succeeded;
        }
    }
}
