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

        public List<string> _playerList { get; set; }

        public PlayerListModel(IConfiguration configuration)
        {
            Configuration = configuration;
            _playerList = new List<string>();
        }

        public event Action OnChange;

        private void Add(string value)
        {
            _playerList.Add(value);
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

            //Dictionary<string, string> PlayerListDic = new Dictionary<string, string>();
            //string[] playListString = response.Split("\n");
            //foreach (string value in playListString)
            //{
            //    //"ID: 107 | SteamID: 76561198125413977 | Name: [20R] Twomad | Team ID: 2 | Squad ID: 9"
            //    //"76561198125413977" "[20R] Twomad" 
            //    //Console.WriteLine(i++ + ": " + value);
            //    i++;
            //    string[] parsedValue = value.Split("|");
            //    if (!parsedValue[0].Equals("----- Active Players -----\r"))
            //    {
            //        //value   "----- Recently Disconnected Players [Max of 15] -----\r"   string
            //        //This second if statement is to tell us when the Active PlayerList ends and the Disconencted Player List starts. 
            //        if (!parsedValue[0].Equals("----- Recently Disconnected Players [Max of 15] -----\r"))
            //        {
            //            //
            //            string[] secondParseValue = parsedValue[1].Split(": ");
            //            //Console.WriteLine((secondParseValue[1]));
            //            string[] ThirdParseValue = parsedValue[2].Split(": ");
            //            //Console.WriteLine((ThirdParseValue[1]));
            //            PlayerListDic.Add(secondParseValue[1], ThirdParseValue[1]);
            //        }
            //        else { break; }

            //    }

            //}

            //foreach (KeyValuePair<String, string> kvp in PlayerListDic)
            //{
            //    Console.WriteLine("Steam = {0}, Name = {1}", kvp.Key, kvp.Value);
            //}


        }
    }
}
