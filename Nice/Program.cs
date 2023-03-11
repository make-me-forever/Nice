using System;
using System.Windows.Forms;

namespace Nice
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainPage main = new MainPage();
            Application.Run(main);

            Application.Exit(); 
            main.Close(); 
            System.Environment.Exit(0);
            Application.ExitThread(); 
        }
    }
}
