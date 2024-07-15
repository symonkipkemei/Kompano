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
using System.Windows;

namespace Kompano.src.Addin.Services
{
    public static class FamilyFunctions
    {
        public static void SearchRfaFiles(string directory, ObservableCollection<string> rfaFilePaths)
        {
            try
            {
                if (string.IsNullOrEmpty(directory))
                {
                    MessageBox.Show("Directory is null or empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (rfaFilePaths == null)
                {
                    MessageBox.Show("rfaFilePaths is null.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

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


        public static (Document familyDoc, UIDocument familyUiDoc) OpenFamilyFile(UIApplication uiApp, Autodesk.Revit.ApplicationServices.Application app, string familyPath)
        {
            // Open the family document
            Document familyDoc = app.OpenDocumentFile(familyPath);

            // Switch to the family document in the Revit UI
            uiApp.OpenAndActivateDocument(familyPath);
            UIDocument familyUiDoc = uiApp.ActiveUIDocument;

            return (familyDoc, familyUiDoc);
        }


        public static void CloseFamilyFile(UIApplication uiApp, Autodesk.Revit.ApplicationServices.Application app, Document familyDoc)
        {
            if (App.OpenRevitProjectDirectory != null)
            {
                uiApp.OpenAndActivateDocument(App.OpenRevitProjectDirectory);

                //close the family file
                bool saveChanges = App.SaveChanges;
                familyDoc.Close(saveChanges);

            }

        }


        public static void GetOpenRRevitProjectPath(ExternalCommandData commandData)
        {
            UIApplication uiApp = commandData.Application;

            // Get all open documents
            DocumentSet openDocs = uiApp.Application.Documents;
            var rvtDocs = openDocs.Cast<Document>().Where(d => d.PathName.EndsWith(".rvt",StringComparison.OrdinalIgnoreCase)).ToList();

            

            // Pick the first open document
            Document doc = rvtDocs.Cast<Document>().FirstOrDefault();
            MessageBox.Show($"{doc}");

            if (doc != null) 
            {
                MessageBox.Show("No Open Projects,Please open a project .rvt file before proceeding. ");
                return;
            }

            string projectPath = doc.PathName;

            // If the project is not saved, inform the user
            if (string.IsNullOrEmpty(projectPath))
            {
                MessageBox.Show("The project is not saved. Please save the project to get the path ");
                return;
            }

            App.OpenRevitProjectDirectory= projectPath;

        }
    }
}
