using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleLogBook
{
    public partial class Form1 : Form
    {
        private String WorkDir = String.Empty;
        public Form1()
        {
            InitializeComponent();
            Utils utils = new Utils();
            WorkDir = utils.GetDataDir();
            utils.CreateDataDir(WorkDir);

            this.toolStripStatusLabel1.Text = WorkDir;

            utils.CreateDatabaseFile();
        }
    }
}
