using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquadCSharpBlazor.Data
{
    public class PlayerListModel
    {
        public string player { get; set; }

        public List<string> playerList { get; set; }

        public PlayerListModel()
        {
            playerList = new List<string>();
        }

        public event Action OnChange;

        public void Add(string value)
        {
            playerList.Add(value);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
