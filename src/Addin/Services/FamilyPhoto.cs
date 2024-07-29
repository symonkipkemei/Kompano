using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;



namespace Kompano.src.Addin.Services
{
    public static class FamilyPhoto
    {
        public static void FamilyPhotoFunction(ExternalCommandData commandData)
        {

            // cancellation token from the sub thread ( Progress Window)
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            //Command data
            UIApplication uiApp = commandData.Application;
            UIDocument uidoc = uiApp.ActiveUIDocument;
            Document activeDoc = uidoc.Document;
            Autodesk.Revit.ApplicationServices.Application app = uiApp.Application;

            // Clear previous file paths
            App.CollectedFilePaths.Clear();

            FamilyFunctions.SearchRfaFiles(App.PrimarySearchDirectory, App.CollectedFilePaths);
            int totalNoFiles = App.CollectedFilePaths.Count;
            int count = 0;

            // flag if cancelled
            bool iscancelled = false;

            // Hold error messages to be displayed later.
            List<string> errorLog = new List<string>();



            ProgressHandler progressHandler = new ProgressHandler(cts);

            if (totalNoFiles != 0)
            {
                progressHandler.ShowProgressBar();

                foreach (string familyPath in App.CollectedFilePaths)
                {
                    if(token.IsCancellationRequested)
                    {
                        MessageBox.Show("Photo session terminated by user", "Terminated", MessageBoxButton.OK,MessageBoxImage.Information);
                        iscancelled = true;
                        break;
                    }


                    try
                    {
                        //Open the file

                        Document familyDoc;
                        UIDocument familyUiDoc;

                        try
                        {
                            (familyDoc, familyUiDoc) = FamilyFunctions.OpenFamilyFile(uiApp, app, familyPath);

                        }

                        catch(Exception ex)
                        {
                            // likely an error to do with versioning
                            errorLog.Add($"Error opening {familyPath}: {ex.Message}");
                            continue; //next file
                        }

                            
                        View3D view3D = ViewFunctions.Activate3DView(familyDoc);
                        familyUiDoc.ActiveView = view3D;


                        using (Transaction trans = new Transaction(familyDoc, "Adjust 3D View"))
                        {
                            trans.Start();

                            ViewFunctions.SetView3DSettings(view3D);
                            ViewFunctions.SetZoom(familyUiDoc, view3D);

                            trans.Commit();
                        }

                        // export images
                        string familyImagePath = ExportFunctions.GetFileImagePath(familyDoc, App.DestinationDirectory);
                        ImageExportOptions exportImageSettings = ExportFunctions.ExportSettings(familyImagePath);
                        familyDoc.ExportImage(exportImageSettings);


                        //close the family file

                        FamilyFunctions.CloseFamilyFile(uiApp, app, familyDoc);
                    }

                    catch (Exception ex) 
                    {
                        errorLog.Add($"Error processing {familyPath}: {ex.Message} \n\n");
                    }

                    count++; //Increment count for both successful and failed files
                    int percentage = (count * 100) / totalNoFiles;
                    progressHandler.UpdateProgress(percentage);

                }

                progressHandler.CloseProgressBar();

                if (!iscancelled)
                {
                    

                    string message = "Family photo session is complete!";
                    if (errorLog.Count > 0) 
                    {
                        message += $"\n {errorLog.Count} file(s) skipped due to errors:\n" + string.Join("\n", errorLog);
                    }

                    MessageBox.Show(message, "Completed", MessageBoxButton.OK, MessageBoxImage.Information);

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

         
