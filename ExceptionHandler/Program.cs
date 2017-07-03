using System;
using System.Windows.Forms;

namespace ExceptionHandler
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Set the application to handle the exception mode: ThreadException  
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //Handle UI thread exception  
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //Handle non-UI thread exceptions  
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
            }
        }
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs ex)
        {
            LogHelper.LogError(ex.Exception);
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs ex)
        {
            LogHelper.LogError(ex.ExceptionObject as Exception);
        }
    }
}
