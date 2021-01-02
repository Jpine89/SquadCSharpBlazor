using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquadCSharpBlazor.Model
{
    public class PlayerList
    {
        public Int64 steamID { get; set; }
        public string userName { get; set; }
        public int connected { get; set; }
    }
}
