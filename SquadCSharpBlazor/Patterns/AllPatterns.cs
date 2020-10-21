using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquadCSharpBlazor.Patterns
{
    public class AllPatterns
    {

        public string[] _stringPatterns =
        {
            "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquad: ADMIN COMMAND: Message broadcasted <(.+)> from (.+)",
            "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogWorld: Bringing World \\/([A-z]+)\\/Maps\\/([A-z]+)\\/(?:Gameplay_Layers\\/)?([A-z0-9_]+)",
            "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogNet: Join succeeded: (.+)",
            "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquad: Player:(.+) ActualDamage=([0-9.]+) from (.+) caused by ([A-z_0-9]+)_C",
            "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquadTrace: \\[DedicatedServer](?:ASQSoldier::)?Die\\(\\): Player:(.+) KillingDamage=(?:-)*([0-9.]+) from ([A-z_0-9]+) caused by ([A-z_0-9]+)_C",
            "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquadTrace: \\[DedicatedServer](?:ASQPlayerController::)?OnPossess\\(\\): PC=(.+) Pawn=([A-z0-9_]+)_C",
            "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquadTrace: \\[DedicatedServer](?:ASQPlayerController::)?OnUnPossess\\(\\): PC=(.+)",
            "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquad: (.+) has revived (.+)\\.",
            "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquadTrace: \\[DedicatedServer](?:ASQSoldier::)?Wound\\(\\): Player:(.+) KillingDamage=(?:-)*([0-9.]+) from ([A-z_0-9]+) caused by ([A-z_0-9]+)_C",
            "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquadTrace: \\[DedicatedServer]ASQGameMode::DetermineMatchWinner\\(\\): (.+) won on (.+)",
            "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquad: USQGameState: Server Tick Rate: ([0-9.]+)",
            "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogEasyAntiCheatServer: \\[[0-9:]+]\\[[A-z]+]\\[EAC Server] \\[Info]\\[RegisterClient] Client: ([A-z0-9]+) PlayerGUID: ([0-9]{17}) PlayerIP: [0-9]{17} OwnerGUID: [0-9]{17} PlayerName: (.+)",
            "\\[(ChatAll|ChatTeam|ChatSquad|ChatAdmin)] \\[SteamID:([0-9]{17})] (.+?) : (.*)",
            "/ID: ([0-9]+) \\| SteamID: ([0-9]{17}) \\| Name: (.+) \\| Team ID: ([0-9]+) \\| Squad ID: ([0-9]+|N\\/A)",
            "/^Current map is (.+), Next map is (.*)/"
        };
        public string[] _stringTypes =
        {
            "adminBroadcast",
            "newGame",
            "playerConnected",
            "playerDamaged",
            "playerDied",
            "playerPosses",
            "playerUnPosses",
            "playerRevived",
            "playerWounded",
            "roundWinner",
            "serverTick",
            "steamID",
            "chatMessage",
            "playerList",
            "currentMap"
        };
        public Dictionary<string, string> _RegexParseList;
        public List<string> _ChatMessage { get; set; }
        public List<string> _AdminBroadcast { get; set; }
        public List<string> _NewGame { get; set; }
        public List<string> _PlayerKilled { get; set; }
        public List<string> _PlayerWounded { get; set; }
        public List<string> _PlayerPosses { get; set; }
        public List<string> _PlayerUnPosses { get; set; }
        public List<string> _PlayerRevived { get; set; }
        public List<string> _RoundWinner { get; set; }
        public List<string> _SteamID { get; set; }
        public List<string> _ServerTic { get; set; }

        public List<string> _PlayerConnected { get; set; }

        public AllPatterns()
        {
            _ChatMessage = new List<string>();
            _PlayerConnected = new List<string>();
            _NewGame = new List<string>();
            _AdminBroadcast = new List<string>();
            _PlayerKilled = new List<string>();
            _PlayerWounded = new List<string>();
            _PlayerPosses = new List<string>();
            _PlayerUnPosses = new List<string>();
            _PlayerRevived = new List<string>();
            _RoundWinner = new List<string>();
            _SteamID = new List<string>();
            _ServerTic = new List<string>();
        }


        public void matchList(string stringType, string line, string[] substring)
        {
            switch (stringType)
            {
                case "chatMessage":
                    //Console.WriteLine("Test");
                    Add(_ChatMessage, line, substring);
                    break;
                case "adminBroadcast":
                    //Console.WriteLine("Test");
                    Add(_AdminBroadcast, line, substring);
                    break;
                case "newGame":
                    //Console.WriteLine("Test");
                    Add(_NewGame, line, substring);
                    break;
                case "playerConnected":
                    //Console.WriteLine("Test");
                    Add(_PlayerConnected, line, substring);
                    break;
                case "playerDamaged":
                    //Console.WriteLine("Test");
                    Add(_PlayerWounded, line, substring);
                    break;
                case "playerDied":
                    //Console.WriteLine("Test");
                    Add(_PlayerKilled, line, substring);
                    break;
                case "playerPosses":
                    //Console.WriteLine("Test");
                    Add(_PlayerPosses, line, substring);
                    break;
                case "playerUnPosses":
                    //Console.WriteLine("Test");
                    Add(_PlayerUnPosses ,line, substring);
                    break;
                case "playerRevived":
                    //Console.WriteLine("Test");
                    Add(_PlayerRevived, line, substring);
                    break;
                case "playerWounded":
                    //Console.WriteLine("Test");
                    Add(_PlayerWounded, line, substring);
                    break;
                case "roundWinner":
                    //Console.WriteLine("Test");
                    Add(_RoundWinner, line, substring);
                    break;
                case "serverTick":
                    //Console.WriteLine("Test");
                    Add(_ServerTic, line, substring);
                    break;
                case "steamID":
                    //Console.WriteLine("Test");
                    Add(_SteamID, line, substring);
                    break;
                default:
                    Console.WriteLine("Default was Called");
                    break;
            }
        }
        
        private void Add(List<string> matchType, string line, string[] substring)
        {
            matchType.Add(line);
        }
    }
}
