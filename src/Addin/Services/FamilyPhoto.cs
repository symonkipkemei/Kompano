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
           
            Dictionary<string, List<String>> errorLog = new Dictionary<string, List<string>>();
            List<string> warningLog = new List<string>();


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
                      
                        var (familyDoc, familyUiDoc) = FamilyFunctions.OpenFamilyFile(uiApp, app, familyPath);

                      
                        WarningCapture warningCapture = new WarningCapture(warningLog, familyPath);

                        try
                        {
                            View3D view3D = ViewFunctions.Activate3DView(familyDoc);
                            familyUiDoc.ActiveView = view3D;


                            using (Transaction trans = new Transaction(familyDoc, "Adjust 3D View"))
                            {

                                var failureHandlingOptions = trans.GetFailureHandlingOptions()
                                .SetFailuresPreprocessor(warningCapture)
                                .SetClearAfterRollback(true);

                                trans.SetFailureHandlingOptions(failureHandlingOptions);


                                trans.Start();

                                ViewFunctions.SetView3DSettings(view3D);
                                ViewFunctions.SetZoom(familyUiDoc, view3D);

                                trans.Commit();
                            }

                            // export images
                            string familyImagePath = ExportFunctions.GetFileImagePath(familyDoc, App.DestinationDirectory);
                            ImageExportOptions exportImageSettings = ExportFunctions.ExportSettings(familyImagePath);
                            familyDoc.ExportImage(exportImageSettings);

                        }

                        catch(Exception ex)
                        {
                            //error to do with 3D view type
                            AddErrorLog(errorLog,familyPath, ex.Message);
                            
                        }

                        //close the family file including those with errors

                        FamilyFunctions.CloseFamilyFile(uiApp, app, familyDoc);

                    }

                    catch(Exception ex)
                    {
                        //Error to do with versioning (File could not be opened
                        AddErrorLog(errorLog, familyPath, ex.Message);

                    }
                    count++; //Increment count for both successful and failed files
                    int percentage = (count * 100) / totalNoFiles;
                    progressHandler.UpdateProgress(percentage);

                }

                progressHandler.CloseProgressBar();

                if (!iscancelled)
                {
                    string message = $"Family photo session is complete! {count} files processed";

                    if (warningLog.Count > 0)
                    {
                        message += $"\n\nWarnings encountered:\n{string.Join("\n", warningLog)}";
                    }

                    if (errorLog.Count > 0) 
                    {
                        int errorCount = errorLog.Sum(error => error.Value.Count) ;
                        
                        message += $"\n\n {errorCount} file(s) were skipped due to errors:";

                        foreach( var error in errorLog)
                        {
                            message += $"\n\nError: {error.Key}\nFiles: {string.Join(", ", error.Value)}";

                        }
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



        public static void AddErrorLog(Dictionary<string, List<string>> errorLog, string filePath, string errorMessage)
        {
            if (!errorLog.ContainsKey(errorMessage)) 
            { 
                errorLog[errorMessage] = new List<string>();
            }

            errorLog[errorMessage].Add(filePath);
        }

    }

    public class WarningCapture : IFailuresPreprocessor
    {
        private readonly List<string> logMessages;
        private readonly string filePath;

        public WarningCapture(List<string> logMessages, string filePath)
        {
            this.logMessages = logMessages;
            this.filePath = filePath;

        }

        public FailureProcessingResult PreprocessFailures (FailuresAccessor failuresAccessor)
        {
            //capture warnings
            IList<FailureMessageAccessor> failureMessages = failuresAccessor.GetFailureMessages();
            foreach (FailureMessageAccessor failureMessage in failureMessages)
            {
                if (failureMessage.GetSeverity() == FailureSeverity.Warning)
                {
                    string warningMessage = failureMessage.GetDescriptionText();
                    logMessages.Add($"Warning for {filePath}: {warningMessage}");
                    failuresAccessor.DeleteWarning(failureMessage); // Only delete warnings
                }
            }

            return FailureProcessingResult.Continue;
        }

    }
}

         
