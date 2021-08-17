/*
 * Copyright 2021, Christopher F. Moran
 * 
 * This application uses the SQLite3 library for .Net, which
 * has it's own license.  Please refer to https://sqlite.org
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.Specialized;
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
            String DataDir;

            if (this.UserAppDataStore())
            {
                String ProgDataDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                DataDir = ProgDataDirectory + "\\" + "SimpleLogBook";
            }
            else
            {
                DataDir = Directory.GetCurrentDirectory();
            }
            return DataDir;
        }

        public String GetDataFile()
        {
            String DataFile = this.GetDataDir() + "\\" + "Logbook.db";
            return DataFile;
        }

        public string GetConnectionString()
        { 
            return @"Data Source=" + this.GetDataFile() + "; Version=3; FailIfMissing=False; ForeignKeys=True";
        }

        public SQLiteConnection OpenDatabase()
        {
            SQLiteConnection conn = null;
            conn = new SQLiteConnection(this.GetConnectionString());
            conn.Open();

            return conn;
        }

        public Boolean CreateDatabaseFile()
        {
            Boolean ret = true;
            String ConnectionString = null;

            // At the moment, this does nothing.  But one day we will use the settings in this module ONLY
            // if an invalid file path is present.
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            if(settings != null)        // Reading app.config succeeded
            {
                if (settings.Count == 0) // But there's no valid entry
                    ConnectionString = this.GetConnectionString();

            }
            else
            {
                MessageBox.Show("settings is NULL", "ConnectionString", MessageBoxButtons.OK);
                ConnectionString = this.GetConnectionString();
            }

            
            SQLiteConnection con = null;
            SQLiteCommand cmd = null;

            if (!File.Exists(this.GetDataFile()))
            {
                try
                {
                    con = this.OpenDatabase();

                    String myCallSign = ConfigurationManager.AppSettings.Get("MyCallSign");

                    cmd = new SQLiteCommand(con);
                    cmd.CommandText = "CREATE TABLE ENTRY (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, ENTRY_DATE DATE NOT NULL, CALLSIGN_OUT VARCHAR(255), CALLSIGN_IN VARCHAR(255) NOT NULL, FREQUENCY DECIMAL(10,4) NOT NULL DEFAULT 1.0, MODE VARCHAR(32) NULL, POWER INTEGER NOT NULL DEFAULT 0, SIGNAL_IN VARCHAR(32) NULL, SIGNAL_OUT VARCHAR(32) NULL, CONTACT_NAME VARCHAR(255) NULL, CONTACT_LOC VARCHAR(255) NULL, COMMENT VARCHAR(255) NULL)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE INDEX IX_ENTRY_001 ON ENTRY(ENTRY_DATE)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE INDEX IX_ENTRY_002 ON ENTRY(CALLSIGN_OUT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE INDEX IX_ENTRY_003 ON ENTRY(CALLSIGN_IN)";
                    cmd.ExecuteNonQuery();

                    LogEntry le = new LogEntry(myCallSign);
                    le.EntryDate = DateTime.UtcNow.Date;
                    le.CallSignOut = myCallSign;
                    le.CallSignIn = "VK2DAY";
                    le.Frequency = Convert.ToDouble(14.123);
                    le.Mode = "USB";
                    le.Power = Convert.ToInt32(10);
                    le.SignalIn = "S9R5";
                    le.SignalOut = "S6R5";
                    le.RemoteName = "Rod";
                    le.RemoteLocation = "Hornsby";
                    le.Comments = "1st Contact";
                    le.Save();

                    le.EntryDate = DateTime.UtcNow.Date;
                    le.CallSignOut = myCallSign;
                    le.CallSignIn = "VK2AOR";
                    le.Frequency = Convert.ToDouble(7.456);
                    le.Mode = "USB";
                    le.Power = Convert.ToInt32(10);
                    le.SignalIn = "S8R5";
                    le.SignalOut = "S5R4";
                    le.RemoteName = "Bob";
                    le.RemoteLocation = "Gosford";
                    le.Comments = "Not a real entry";
                    le.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error Connecting to database engine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return ret;
        }

        public String GetMyCallSign()
        {
            String CallSign = ConfigurationManager.AppSettings.Get("MyCallSign");
            return CallSign;
        }

        public Boolean UserAppDataStore()
        {
            Boolean ret = false;
            ret = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("StoreInUserAppData"));
            return ret;
        }
    }
}
