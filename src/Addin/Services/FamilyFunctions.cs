using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kompano.src.Addin.Services
{
    public static class FamilyFunctions
    {
        public static void SearchRfaFiles(string directory, ObservableCollection<string> rfaFilePaths)
        {
            try
            {
                // Search for .rfa files in the current directory
                foreach (string file in Directory.GetFiles(directory, "*.rfa"))
                {
                    rfaFilePaths.Add(file);
                }

                // Recursively search subdirectories
                foreach (string subDirectory in Directory.GetDirectories(directory))
                {
                    SearchRfaFiles(subDirectory, rfaFilePaths);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Handle or log unauthorized access exceptions if needed
            }
        }


        public static Document OpenFamilyFile(UIApplication uiApp, Application app, string familyPath)
        {
            // Open the family document
            Document familyDoc = app.OpenDocumentFile(familyPath);

            // Switch to the family document in the Revit UI
            uiApp.OpenAndActivateDocument(familyPath);

            return familyDoc;
        }

    }
}
