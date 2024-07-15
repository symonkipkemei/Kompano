using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Kompano.src.Addin
{
    public class Addin : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            // Add your initialization code here

            // Ribbon Panel
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("Kompano");
            string assembly = Assembly.GetExecutingAssembly().Location;


            //Button data for About command
            PushButtonData data1 = new PushButtonData("FamilyPhoto", "FamilyPhoto", assembly, "Kompano.src.Addin.Commands.FamilyPhotoCommand");
            PushButton button1 = ribbonPanel.AddItem(data1) as PushButton;
            button1.ToolTip = "Info about this tool";
            Uri uri1 = new Uri("pack://application:,,,/Autojenzi;component/src/Addin/Resources/familyPhoto.png");
            BitmapImage image1 = new BitmapImage(uri1);
            button1.LargeImage = image1;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
