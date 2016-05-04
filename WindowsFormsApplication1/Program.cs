// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// Program.cs
// 2016-05-04-9:34

#region

using System;
using System.Windows.Forms;

#endregion

namespace WindowsFormsApplication1
{
    internal static class Program
    {
        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}