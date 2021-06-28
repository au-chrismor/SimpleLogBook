using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;


namespace SimpleLogBook
{
    public partial class Form1 : Form
    {
//        private String WorkDir = String.Empty;
        public Form1()
        {
            InitializeComponent();
            Utils utils = new Utils();
            this.toolStripStatusLabel1.Text = utils.GetDataFile();
            this.FillDataGrid();

        }

        private void FillDataGrid()
        {
            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataReader rdr = null;

            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(this.dataGridView1.Font, FontStyle.Bold);
            this.dataGridView1.Columns.Add("EntryDate", "Date");
            this.dataGridView1.Columns.Add("CallSignIn", "Callsign");
            this.dataGridView1.Columns.Add("Frequency", "Freq");
            this.dataGridView1.Columns.Add("Mode", "Mode");
            this.dataGridView1.Columns.Add("Power", "Power");
            this.dataGridView1.Columns.Add("Receive", "Receive");
            this.dataGridView1.Columns.Add("Transmit", "Transmit");
            this.dataGridView1.Columns.Add("Name", "Name");
            this.dataGridView1.Columns.Add("Location", "Location");
            this.dataGridView1.Columns.Add("Comment", "Comment");

            this.dataGridView1.ReadOnly = true;

            Utils util = new Utils();
            conn = util.OpenDatabase();

            cmd = new SQLiteCommand(conn);
            cmd.CommandText = "SELECT * FROM ENTRY ORDER BY ENTRY_DATE";

                        rdr = cmd.ExecuteReader();
                        if(rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataGridViewRow newRow = new DataGridViewRow();
                                newRow.CreateCells(this.dataGridView1);
                                newRow.Cells[0].Value = rdr["ENTRY_DATE"].ToString();
                                newRow.Cells[1].Value = rdr["CALLSIGN_IN"].ToString();
                                newRow.Cells[2].Value = rdr["FREQUENCY"].ToString();
                                newRow.Cells[3].Value = rdr["MODE"].ToString();
                                newRow.Cells[4].Value = rdr["POWER"].ToString();
                                newRow.Cells[5].Value = rdr["SIGNAL_IN"].ToString();
                                newRow.Cells[6].Value = rdr["SIGNAL_OUT"].ToString();
                                newRow.Cells[7].Value = rdr["CONTACT_NAME"].ToString();
                                newRow.Cells[8].Value = rdr["CONTACT_LOC"].ToString();
                                newRow.Cells[9].Value = rdr["COMMENT"].ToString();
                                this.dataGridView1.Rows.Add(newRow);
                            }
                            this.dataGridView1.Refresh();
                        }
            
            conn.Close();
            conn.Dispose();

            this.dataGridView1.Refresh();
        }
    }
}
