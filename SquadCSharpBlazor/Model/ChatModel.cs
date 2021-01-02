using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquadCSharpBlazor.Model
{
    public class ChatModel
    {
        public Int64 steamID { get; set; }
        public string chatType { get; set; }
        public string message { get; set; }

    }
}
