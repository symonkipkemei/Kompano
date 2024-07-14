using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kompano.src.Addin
{
    public class Addin : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            // Add your initialization code here

            // Example button creation code to open Settings window
            PushButtonData buttonData = new PushButtonData("SettingsButton", "Settings", Assembly.GetExecutingAssembly().Location, "Kompano.src.Addin.Commands.FamilyPhotoCommand");
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("My Tools");
            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
