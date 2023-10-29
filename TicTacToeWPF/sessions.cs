using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeWPF
{
    internal class sessions
    {
        public int id { get; set; }

        private string sessionname, userwins, userlosses, timesession;
        
        public string Sessionname
        {
            get { return sessionname; }
            set { sessionname = value; }
        }

        public string Userwins
        {
            get { return userwins; }
            set { userwins = value; }
        }

        public string Userlosses
        {
            get { return userlosses; }
            set { userlosses = value; }
        }

        public string Timesession
        {
            get { return timesession; }
            set { timesession = value; }
        }
        public sessions() { }

        public sessions(string sessionname, string userwins, string userlosses, string timesession) {
            this.sessionname = sessionname;
            this.userwins = userwins;
            this.userlosses = userlosses;
            this.timesession = timesession;
        }

        
    }
}
