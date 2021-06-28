/*
 * Copyright 2021, Christopher F. Moran
 * 
 * This application uses the SQLite3 library for .Net, which
 * has it's own license.  Please refer to https://sqlite.org
 */

using System;
using System.Collections.Generic;
using System.Configuration;
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

                    cmd = new SQLiteCommand(con);
                    cmd.CommandText = "CREATE TABLE ENTRY (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, ENTRY_DATE TIMESTAMP NOT NULL, CALLSIGN_OUT VARCHAR(255), CALLSIGN_IN VARCHAR(255) NOT NULL, FREQUENCY DECIMAL(10,4) NOT NULL DEFAULT 1.0, MODE VARCHAR(32) NULL, POWER INTEGER NOT NULL DEFAULT 0, SIGNAL_IN VARCHAR(32) NULL, SIGNAL_OUT VARCHAR(32) NULL, CONTACT_NAME VARCHAR(255) NULL, CONTACT_LOC VARCHAR(255) NULL, COMMENT VARCHAR(255) NULL)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE INDEX IX_ENTRY_001 ON ENTRY(ENTRY_DATE)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE INDEX IX_ENTRY_002 ON ENTRY(CALLSIGN_OUT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE INDEX IX_ENTRY_003 ON ENTRY(CALLSIGN_IN)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO ENTRY(ENTRY_DATE, CALLSIGN_OUT, CALLSIGN_IN, FREQUENCY, MODE, POWER, SIGNAL_IN, SIGNAL_OUT, CONTACT_NAME, CONTACT_LOC, COMMENT) VALUES(@Date, @CallOut, @CallIn, @Freq, @Mode, @Power, @SigIn, @SigOut, @Name, @Location, @Comment)";
                    SQLiteParameter ParamEd = new SQLiteParameter("@Date", System.Data.DbType.DateTime);
                    ParamEd.Value = DateTime.UtcNow;
                    SQLiteParameter ParamCso = new SQLiteParameter("@CallOut", System.Data.DbType.String);
                    ParamCso.Value = "VK2MEI";
                    SQLiteParameter ParamCsi = new SQLiteParameter("@CallIn", System.Data.DbType.String);
                    ParamCsi.Value = "VK2DAY";
                    SQLiteParameter ParamFreq = new SQLiteParameter("@Freq", System.Data.DbType.Decimal);
                    ParamFreq.Value = Convert.ToDouble(14.123);
                    SQLiteParameter ParamMode = new SQLiteParameter("@Mode", System.Data.DbType.String);
                    ParamMode.Value = "USB";
                    SQLiteParameter ParamPower = new SQLiteParameter("@Power", System.Data.DbType.Int32);
                    ParamPower.Value = Convert.ToInt32(10);
                    SQLiteParameter ParamSi = new SQLiteParameter("@SigIn", System.Data.DbType.String);
                    ParamSi.Value = "S9R5";
                    SQLiteParameter ParamSo = new SQLiteParameter("@SigOut", System.Data.DbType.String);
                    ParamSo.Value = "S6R5";
                    SQLiteParameter ParamName = new SQLiteParameter("@Name", System.Data.DbType.String);
                    ParamName.Value = "Bob";
                    SQLiteParameter ParamLoc = new SQLiteParameter("@Location", System.Data.DbType.String);
                    ParamLoc.Value = "Hornsby";
                    SQLiteParameter ParamComment = new SQLiteParameter("@Comment", System.Data.DbType.String);
                    ParamComment.Value = "1st Contact";

                    cmd.Parameters.Add(ParamEd);
                    cmd.Parameters.Add(ParamCso);
                    cmd.Parameters.Add(ParamCsi);
                    cmd.Parameters.Add(ParamFreq);
                    cmd.Parameters.Add(ParamMode);
                    cmd.Parameters.Add(ParamPower);
                    cmd.Parameters.Add(ParamSi);
                    cmd.Parameters.Add(ParamSo);
                    cmd.Parameters.Add(ParamName);
                    cmd.Parameters.Add(ParamLoc);
                    cmd.Parameters.Add(ParamComment);

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();

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
