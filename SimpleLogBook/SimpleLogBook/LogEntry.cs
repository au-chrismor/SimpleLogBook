using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace SimpleLogBook
{
    class LogEntry
    {
        private long id;
        private DateTime entryDate;
        private String callSignOut;
        private String callSignIn;
        private Double frequency;
        private String mode;
        private Int32 power;
        private String signalOut;
        private String signalIn;
        private String remoteName;
        private String remoteLocation;
        private String comments;

        public String CallSignIn
        {
            get { return this.callSignIn; }
            set { this.callSignIn = value; }
        }

        public String CallSignOut
        {
            get { return this.callSignOut; }
            set { this.callSignOut = value; }
        }

        public String Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public DateTime EntryDate
        {
            get { return this.entryDate; }
            set { this.entryDate = value; }
        }

        public Double Frequency
        {
            get { return this.frequency; }
            set { this.frequency = value; }
        }

        public long Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public String Mode
        {
            get { return this.mode; }
            set { this.mode = value; }
        }

        public Int32 Power
        {
            get { return this.power; }
            set { this.power = value; }
        }

        public String RemoteLocation
        {
            get { return this.remoteLocation; }
            set { this.remoteLocation = value; }
        }

        public String RemoteName
        {
            get { return this.remoteName; }
            set { this.remoteName = value; }
        }

        public String SignalIn
        {
            get { return this.signalIn; }
            set { this.signalIn = value; }
        }

        public String SignalOut
        {
            get { return this.signalOut; }
            set { this.signalOut = value; }
        }

        public LogEntry() { }

        public LogEntry(String MyCallSign)
        {
            this.callSignOut = MyCallSign;
        }

        public Boolean Save()
        {
            Boolean ret = true;

            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            Utils util = new Utils();

            try
            {
                conn = util.OpenDatabase();
                cmd = new SQLiteCommand(conn);
                cmd.CommandText = "INSERT INTO ENTRY(ENTRY_DATE, CALLSIGN_OUT, CALLSIGN_IN, FREQUENCY, MODE, POWER, SIGNAL_IN, SIGNAL_OUT, CONTACT_NAME, CONTACT_LOC, COMMENT) VALUES(@Date, @CallOut, @CallIn, @Freq, @Mode, @Power, @SigIn, @SigOut, @Name, @Location, @Comment)";
                SQLiteParameter ParamEd = new SQLiteParameter("@Date", System.Data.DbType.DateTime);
                ParamEd.Value = DateTime.UtcNow;
                SQLiteParameter ParamCso = new SQLiteParameter("@CallOut", System.Data.DbType.String);
                ParamCso.Value = util.GetMyCallSign();
                SQLiteParameter ParamCsi = new SQLiteParameter("@CallIn", System.Data.DbType.String);
                ParamCsi.Value = this.CallSignIn;
                SQLiteParameter ParamFreq = new SQLiteParameter("@Freq", System.Data.DbType.Decimal);
                ParamFreq.Value = this.Frequency;
                SQLiteParameter ParamMode = new SQLiteParameter("@Mode", System.Data.DbType.String);
                ParamMode.Value = this.Mode;
                SQLiteParameter ParamPower = new SQLiteParameter("@Power", System.Data.DbType.Int32);
                ParamPower.Value = this.Power;
                SQLiteParameter ParamSi = new SQLiteParameter("@SigIn", System.Data.DbType.String);
                ParamSi.Value = this.SignalIn;
                SQLiteParameter ParamSo = new SQLiteParameter("@SigOut", System.Data.DbType.String);
                ParamSo.Value = this.SignalOut;
                SQLiteParameter ParamName = new SQLiteParameter("@Name", System.Data.DbType.String);
                ParamName.Value = this.RemoteName;
                SQLiteParameter ParamLoc = new SQLiteParameter("@Location", System.Data.DbType.String);
                ParamLoc.Value = this.RemoteLocation;
                SQLiteParameter ParamComment = new SQLiteParameter("@Comment", System.Data.DbType.String);
                ParamComment.Value = this.Comments;

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



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error saving record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if(conn.State == System.Data.ConnectionState.Open)
                {
                    // Abort and roll back
                    conn.Cancel();
                    ret = false;
                }
            }
            finally
            {
                try
                {
                    cmd.Dispose();

                    conn.Close();
                    conn.Dispose();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error saving record - finalize", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ret = false;
                }
            }

            return ret;
        }
    }
}
