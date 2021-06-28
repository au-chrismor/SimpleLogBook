using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogBook
{
    class LogEntry
    {
        private long id;
        private DateTime entryDate;
        private String callSignOut;
        private String callSignIn;
        private float frequency;
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

        public float Frequency
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
    }
}
