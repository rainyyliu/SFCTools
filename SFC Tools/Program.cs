using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SFC_Tools
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
           // Application.SetCompatibleTextRenderingDefault(false);
            SFCStartup sfcs = new SFCStartup();
            frmMain frmIni = new frmMain();
            Application.Run(frmIni);
            //Application.Run(new frmMain());
        }
    }
}
