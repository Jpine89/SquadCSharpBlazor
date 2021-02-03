using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquadCSharpBlazor.Model
{
    public class SteamUser
    {
        public Int64 steamID { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
        public int wounded { get; set; }
        public int teamkills { get; set; }
        public int revives { get; set; }

    }
}
