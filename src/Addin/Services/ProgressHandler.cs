using Kompano.src.UI.ProgressWindow;


using System.Windows;
using System.Threading;
using System.Windows.Threading;
using Autodesk.Revit.DB;


namespace Kompano.src.Addin.Services
{
    internal class ProgressHandler
    {

        private ProgressWindow progressWindow;
        private Thread uiThread;
        private bool isWindowOpen = false;

        public void ShowProgressBar()
        {
            uiThread = new Thread(() =>
            {
                progressWindow = new ProgressWindow();
                progressWindow.Closed += (s, e) =>

                {
                    isWindowOpen = false; // Mark window as closed
                    Dispatcher.CurrentDispatcher.InvokeShutdown();
                };

                isWindowOpen = true; // Mark window as open
                progressWindow.Show();

                // Keep WPF open
                Dispatcher.Run();
            });

            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.IsBackground = true;
            uiThread.Start();

        }

        public void UpdateProgress(int progress) 
        {
            if (isWindowOpen) 
            {
                progressWindow.Dispatcher.Invoke(() => progressWindow.UpdateProgress(progress), DispatcherPriority.Background);
            }
        }

        public void CloseProgressBar()
        {
            if (progressWindow != null && isWindowOpen)
            {
                progressWindow.Dispatcher.Invoke(() => progressWindow.Close());
            }

            uiThread.Join();
        }
    }
}
