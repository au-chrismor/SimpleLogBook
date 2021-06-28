using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleLogBook
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            String WorkDir = String.Empty;

            Utils utils = new Utils(); 
            WorkDir = utils.GetDataDir();
            utils.CreateDataDir(WorkDir);
            utils.CreateDatabaseFile();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
