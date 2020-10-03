using CoreRCON;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SquadCSharpBlazor.Data
{
    public class PlayerListModel
    {
        private readonly IConfiguration Configuration;
        public string player { get; set; }

        public List<string> playerList { get; set; }

        public PlayerListModel(IConfiguration configuration)
        {
            Configuration = configuration;
            playerList = new List<string>();
        }

        public event Action OnChange;

        private void Add(string value)
        {
            playerList.Add(value);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();


        public async void loadRconAsync()
        {
            var rcon = new RCON(IPAddress.Parse(Configuration["IP"]), Convert.ToUInt16(Configuration["Port"]), Configuration["Pass"], 10000, true);
            await rcon.ConnectAsync();
            string response = await rcon.SendCommandAsync("ListPlayers");
            string[] playListString = response.Split("\n");
            foreach (string value in playListString)
            {
                Add(value);
            }
        }
    }
}
