using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace SimpleLogBook
{
    class Utils
    {
        public void CreateDataDir(String BaseDir, String DirName)
        {
            try
            {
                String DirPath;
                DirPath = BaseDir + "\\" + DirName;

                if (!Directory.Exists(DirPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(DirPath);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error creating Data Directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CreateDataDir(String DirName)
        {
            try
            {
                 if (!Directory.Exists(DirName))
                {
                    DirectoryInfo di = Directory.CreateDirectory(DirName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error creating Data Directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public String GetDataDir()
        {
            String ProgDataDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            String DataDir = ProgDataDirectory + "\\" + "SimpleLogBook";

            return DataDir;
        }

        public Boolean CreateDatabaseFile()
        {
            Boolean ret = true;
            String DataFile = this.GetDataDir() + "\\" + "Logbook.db";
            String ConnectionString = @"Data Source=" + DataFile + "; Version=3; FailIfMissing=False; ForeignKeys=True";
            SQLiteConnection con = null;
            SQLiteCommand cmd = null;

            if (!File.Exists(DataFile))
            {
                try
                {
                    con = new SQLiteConnection(ConnectionString);
                    con.Open();

                    cmd = new SQLiteCommand(con);
                    cmd.CommandText = "CREATE TABLE ENTRY (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, ENTRY_DATE TIMESTAMP NOT NULL, CALLSIGN_OUT VARCHAR(255), CALLSIGN_IN VARCHAR(255) NOT NULL, FREQUENCY DECIMAL(10,4) NOT NULL DEFAULT 1.0, MODE VARCHAR(32) NULL, POWER INTEGER NOT NULL DEFAULT 0, SIGNAL_IN VARCHAR(32) NULL, SIGNAL_OUT VARCHAR(32) NULL, CONTACT_NAME VARCHAR(255) NULL, CONTACT_LOC VARCHAR(255) NULL, COMMENT VARCHAR(255) NULL)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE INDEX IX_ENTRY_001 ON ENTRY(ENTRY_DATE)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE INDEX IX_ENTRY_002 ON ENTRY(CALLSIGN_OUT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE INDEX IX_ENTRY_003 ON ENTRY(CALLSIGN_IN)";
                    cmd.ExecuteNonQuery();

                    con.Close();
                    con.Dispose();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error Connecting to database engine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return ret;
        }
    }
}
