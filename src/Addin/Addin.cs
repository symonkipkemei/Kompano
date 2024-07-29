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

            //Button data for about 

            PushButtonData data1 = new PushButtonData("About", "About", assembly, "Kompano.src.Addin.Commands.AboutCommand");
            PushButton button1 = ribbonPanel.AddItem(data1) as PushButton;
            button1.ToolTip = "Info about this tool";
            Uri uri1 = new Uri("pack://application:,,,/Kompano;component/src/Addin/Resources/KompanoProfileIcon.png");
            BitmapImage image1 = new BitmapImage(uri1);
            button1.LargeImage = image1;


            //Button data for Photograph Families command
            PushButtonData data2 = new PushButtonData("Photograph families", "Photograph RVT \n families/projects", assembly, "Kompano.src.Addin.Commands.FamilyPhotoCommand");
            PushButton button2 = ribbonPanel.AddItem(data2) as PushButton;
            button2.ToolTip = "Auto photograph Revit families from your directory/sub-directories";
            Uri uri2 = new Uri("pack://application:,,,/Kompano;component/src/Addin/Resources/KompanoPhotographIcon.png");
            BitmapImage image2 = new BitmapImage(uri2);
            button2.LargeImage = image2;

            //Button data for Settings Command
            PushButtonData data3 = new PushButtonData("settings", "Settings", assembly, "Kompano.src.Addin.Commands.SettingsCommand");
            PushButton button3 = ribbonPanel.AddItem(data3) as PushButton;
            button3.ToolTip = "Adjust settings for 3D orientation, graphics style and export settings to your preference.";
            Uri uri3 = new Uri("pack://application:,,,/Kompano;component/src/Addin/Resources/KompanoPhotographIcon.png");
            BitmapImage image3 = new BitmapImage(uri3);
            button3.LargeImage = image3;



            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
