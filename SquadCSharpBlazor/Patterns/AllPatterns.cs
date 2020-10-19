using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquadCSharpBlazor.Patterns
{
    public class AllPatterns
    {
        public ChatMessage chatMessage;
        public AdminBroadcast adminBroadcast;
        public NewGame newGame;
        public PlayerConnected playerConnected;
        public PlayerDamaged playerDamaged;
        public PlayerDied playerDied;
        public PlayerPosses playerPosses;
        public PlayerRevived playerRevived;
        public PlayerUnPosses playerUnPosses;
        public PlayerWounded playerWounded;
        public RoundWinner roundWinner;
        public ServerTickRate serverTick;
        public SteamIDConnect steamID;



        public Dictionary<string, string> _RegexParseList;
        public AllPatterns()
        {
            chatMessage = new ChatMessage();
            adminBroadcast = new AdminBroadcast();
            newGame = new NewGame();
            playerConnected = new PlayerConnected();
            playerDamaged = new PlayerDamaged();
            playerDied = new PlayerDied();
            playerPosses = new PlayerPosses();
            playerUnPosses = new PlayerUnPosses();
            playerWounded = new PlayerWounded();
            roundWinner = new RoundWinner();
            serverTick = new ServerTickRate();
            steamID = new SteamIDConnect();
            regexSetup();
        }

        private void regexSetup()
        {
            _RegexParseList.Add(chatMessage._PatternType, chatMessage._Pattern);
            _RegexParseList.Add(adminBroadcast._PatternType, adminBroadcast._Pattern);
            _RegexParseList.Add(newGame._PatternType, newGame._Pattern);
            _RegexParseList.Add(playerConnected._PatternType, playerConnected._Pattern);
            _RegexParseList.Add(playerDamaged._PatternType, playerDamaged._Pattern);
            _RegexParseList.Add(playerDied._PatternType, playerDied._Pattern);
            _RegexParseList.Add(playerPosses._PatternType, playerPosses._Pattern);
            _RegexParseList.Add(playerUnPosses._PatternType, playerUnPosses._Pattern);
            _RegexParseList.Add(playerWounded._PatternType, playerWounded._Pattern);
            _RegexParseList.Add(roundWinner._PatternType, roundWinner._Pattern);
            _RegexParseList.Add(serverTick._PatternType, serverTick._Pattern);
            _RegexParseList.Add(steamID._PatternType, steamID._Pattern);

        }
        public void matchList(string key, string line, string[] substring)
        {
            switch (key)
            {
                case "ChatMessage":
                    //Console.WriteLine("Test");
                    chatMessage.Add(line, substring);
                    break;
                default:
                    Console.WriteLine("Default was Called");
                    break;
            }
        }
    }
}
