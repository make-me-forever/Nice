using System;
using System.Windows.Forms;
using System.Threading;

namespace Nice
{
    static class Program
    {
        //public static Thread th_wnd, th_task;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //th_wnd = new Thread(thread_wnd);
            //th_wnd.Name = "thread_wnd";
            //th_wnd.Start();
            //th_task = new Thread(thread_task);
            //th_task.Name = "thread_task";
            //th_task.Start();

            MainPage main = new MainPage();
            Application.Run(main);

            main.Close();
            Application.Exit();
            System.Environment.Exit(0);
            Application.ExitThread(); 
        }

        //static void thread_wnd()
        //{

        //    while(true)
        //    {
        //        Console.Write("wnd\t\r\n");
        //        //Thread.Sleep(1000);
        //        Console.Write(th_task.Name + " is status : " + th_wnd.ThreadState + ".\r\n");
        //        th_task.Resume();
        //        Console.Write("task\t" + th_task.Name + " is status : " + th_wnd.ThreadState + ".\r\n");
        //        th_wnd.Suspend();
        //    }

        //}

        //static void thread_task()
        //{
        //    while(true)
        //    {
        //        Console.Write("task\t\r\n");
        //        //Thread.Sleep(1000);
        //        //th_task.Suspend();
        //        //th_wnd.Start();
        //        Console.Write(th_wnd.Name + " is status : " + th_wnd.ThreadState + ".\r\n");
        //        th_wnd.Resume();
        //        Console.Write(th_wnd.Name + " is status : " + th_wnd.ThreadState + ".\r\n");
        //        th_task.Suspend();
        //    }
        //}
    }
}
