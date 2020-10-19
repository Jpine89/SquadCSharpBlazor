using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquadCSharpBlazor.Patterns
{
    public class PlayerDamaged : PatternClass
    {
        public PlayerDamaged()
        {
            _PatternType = "playerDamaged";
            _Pattern = "\\[(ChatAll|ChatTeam|ChatSquad|ChatAdmin)] \\[SteamID:([0-9]{17})] (.+?) : (.*)";
            _StoredLists = new List<string>();


        }

        public new void Add(string line, string[] substring)
        {

        }
    }
}
