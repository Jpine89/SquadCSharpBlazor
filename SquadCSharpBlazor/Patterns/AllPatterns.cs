using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquadCSharpBlazor.Patterns
{
    public class AllPatterns
    {
        /*
         * _ChatMessage
         * Will store all Chat Logs for local use.
         * Permanent local storage, but all Chatlogs will also be sent to DB
         * ----Items stored in local ChatMessage----
         * ChatAll/ChatTeam/ChatAdmin
         * Player Joins/Disconnect
         * TeamKills
         * BroadCasts
         */
        public List<string> _ChatMessage { get; set; }
        
        public List<string> _AdminBroadcast { get; set; }
        //Temporary till Database is Setup
        public List<string> _NewGame { get; set; }
        //Temporary till Database is Setup
        public List<string> _PlayerKilled { get; set; }
        //Temporary till Database is Setup
        public List<string> _PlayerWounded { get; set; }
        //Temporary till Database is Setup
        public List<string> _PlayerPosses { get; set; }
        //Temporary till Database is Setup
        public List<string> _PlayerUnPosses { get; set; }
        //Temporary till Database is Setup
        public List<string> _PlayerRevived { get; set; }
        //Temporary till Database is Setup
        public List<string> _RoundWinner { get; set; }
        //Temporary till Database is Setup
        public List<string> _SteamID { get; set; }
        //Temporary till Database is Setup
        public List<string> _ServerTic { get; set; }
        //Temporary till Database is Setup
        public List<string> _PlayerConnected { get; set; }

        public Dictionary<string, string> _AllPatterns;
        public Dictionary<string, string> setUserNameToC_ID;
        private Dictionary<string, string> userSteamToC_ID;
        public Dictionary<string, string> userSetToTeam;
        public List<string> adminInCameraList;
        public Dictionary<string, string> adminInCameraDic;
        private string C_ID;

        public AllPatterns()
        {
            C_ID = "";
            adminInCameraList = new List<string>();
            adminInCameraDic = new Dictionary<string, string>();
            userSetToTeam = new Dictionary<string, string>();
            setUserNameToC_ID = new Dictionary<string, string>();
            userSteamToC_ID = new Dictionary<string, string>();
            _AllPatterns = new Dictionary<string, string>();
            patternSetup();
        }

        private void patternSetup()
        {
            _AllPatterns.Add("playerConnected", "\\[([0-9.:-]+)]\\[[ 0-9]*]LogSquad: PostLogin: NewPlayer: BP_PlayerController_C \\/Game\\/Maps\\/[A-z]+\\/(?:Gameplay_Layers\\/)?[A-z0-9_]+.[A-z0-9_]+:PersistentLevel.BP_PlayerController_(C_[0-9]+)");
            _AllPatterns.Add("steamID", "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogEasyAntiCheatServer: \\[[0-9:]+]\\[[A-z]+]\\[EAC Server] \\[Info]\\[RegisterClient] Client: ([A-z0-9]+) PlayerGUID: ([0-9]{17}) PlayerIP: [0-9]{17} OwnerGUID: [0-9]{17} PlayerName: (.+)");
            _AllPatterns.Add("chatMessage", "\\[(ChatAll|ChatTeam|ChatSquad|ChatAdmin)] \\[SteamID:([0-9]{17})] (.+?) : (.*)");
            _AllPatterns.Add("removeUser", "\\[([0-9.:-]+)][[ 0-9]+]LogNet: UChannel::Close: [A-z0-9_ ,.=:]+ RemoteAddr: ([0-9]+):[A-z0-9_ ,.=:]+ BP_PlayerController_(C_[0-9]+)");
            _AllPatterns.Add("adminBroadcast", "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquad: ADMIN COMMAND: Message broadcasted <(.+)> from (.+)");
            _AllPatterns.Add("newGame", "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogWorld: Bringing World \\/([A-z]+)\\/Maps\\/([A-z]+)\\/(?:Gameplay_Layers\\/)?([A-z0-9_]+)");
            _AllPatterns.Add("playerDamaged", "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquad: Player:(.+) ActualDamage=([0-9.]+) from (.+) caused by ([A-z_0-9]+)_C");
            _AllPatterns.Add("playerDied", "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquadTrace: \\[DedicatedServer](?:ASQSoldier::)?Die\\(\\): Player:(.+) KillingDamage=(?:-)*([0-9.]+) from ([A-z_0-9]+) caused by ([A-z_0-9]+)_C");
            _AllPatterns.Add("playerPosses", "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquadTrace: \\[DedicatedServer](?:ASQPlayerController::)?OnPossess\\(\\): PC=(.+) Pawn=([A-z0-9_]+)_C");
            _AllPatterns.Add("playerUnPosses", "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquadTrace: \\[DedicatedServer](?:ASQPlayerController::)?OnUnPossess\\(\\): PC=(.+)");
            _AllPatterns.Add("playerRevived", "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquad: (.+) has revived (.+)\\.");
            _AllPatterns.Add("playerWounded", "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquadTrace: \\[DedicatedServer](?:ASQSoldier::)?Wound\\(\\): Player:(.+) KillingDamage=(?:-)*([0-9.]+) from ([A-z_0-9]+) caused by ([A-z_0-9]+)_C");
            _AllPatterns.Add("serverTick", "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquad: USQGameState: Server Tick Rate: ([0-9.]+)");
            _AllPatterns.Add("roundWinner", "\\[([0-9.:-]+)]\\[([ 0-9]*)]LogSquadTrace: \\[DedicatedServer]ASQGameMode::DetermineMatchWinner\\(\\): (.+) won on (.+)");
            _AllPatterns.Add("playerList","/ID: ([0-9]+) \\| SteamID: ([0-9]{17}) \\| Name: (.+) \\| Team ID: ([0-9]+) \\| Squad ID: ([0-9]+|N\\/A)" );
            _AllPatterns.Add("currentMap", "/^Current map is (.+), Next map is (.*)/");
        }

        public void matchList(string stringType, string line, string[] substring, Boolean newUser = false)
        {
            switch (stringType)
            {
                case "playerUnPosses":
                    //Add Future Logic for seeing if someone is using admin cam to cheat. 
                    if (adminInCameraDic.ContainsKey(substring[2]))
                    {
                        adminInCameraDic[substring[2]] = "Inactive";
                    }
                    break;
                case "UserJoining":
                    if (!newUser)
                    {
                        C_ID = substring[2];
                        //Adding the User C_ID to the Dictionary First, since we don't know the UserName yet. 
                        setUserNameToC_ID.Add(substring[2], "not defined");
                    }
                    else if (newUser)
                    {
                        //Now that we have the User Name, we are combining that with the C_ID. 
                        setUserNameToC_ID[C_ID] = substring[4];
                        //Game Logs use SteamID to see who leaves. 
                        //We are setting that as the Key, and it's value as the C_ID
                        userSteamToC_ID.Add(substring[3], C_ID);
                    }
                    break;
                case "removeUser":
                    if (userSteamToC_ID.ContainsKey(substring[2]))
                    {
                        if (adminInCameraDic.ContainsKey(setUserNameToC_ID[userSteamToC_ID[substring[2]]]))
                            adminInCameraDic[setUserNameToC_ID[userSteamToC_ID[substring[2]]]] = "Inactive";

                        setUserNameToC_ID.Remove(userSteamToC_ID[substring[2]]);
                        userSteamToC_ID.Remove(substring[2]);
                    }
                    else
                    {
                        //Console.WriteLine("This user: " + substring[2] + " Has decided to leave the server");
                    }
                    break;
                case "playerPosses":
                    string bp_soldier = "bp_soldier";
                    if (substring[3].ToLower().StartsWith(bp_soldier))
                    {
                        string[] teamID = substring[3].Split("_");
                        if (userSetToTeam.ContainsKey(substring[2]))
                        {
                            userSetToTeam[substring[2]] = teamID[2];
                        }
                        else
                        {
                            userSetToTeam.Add(substring[2], teamID[2]);
                        }

                    }
                    else if (substring[3].ToLower().StartsWith("cameraman"))
                    {
                        adminInCameraList.Add(line);
                        if (adminInCameraDic.ContainsKey(substring[2]))
                        {
                            adminInCameraDic[substring[2]] = "Active";
                        }
                        else
                        {
                            adminInCameraDic.Add(substring[2], "Active");
                        }
                    }
                    break;
                case "chatMessage":
                    break;
                default:
                    break;
            }
        }

        private void Add(List<string> matchType, string line, string[] substring)
        {
            matchType.Add(line);
        }
    }
}
