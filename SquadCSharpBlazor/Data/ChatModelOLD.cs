using SquadCSharpBlazor.Patterns;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SquadCSharpBlazor.Data
{
    public class ChatModelOLD
    {

        //private FileSystemWatcher _watcher;
        //public List<string> _Chat { get; set; }
        public Boolean PageActive { get; set; }
        public Boolean firstTime;
        //private string fileName = "G:/SquadTestServer/servers/squad_server/SquadGame/Saved/Logs/ServerLog.log";
        private string fileName = "G:/SquadTestServer/servers/squad_server/SquadGame/Saved/Logs/ChatExample.txt";
        public StreamReader reader;
        private long lastMaxOffset;
        private string lineReturn;
        private int debugCounter;
        private Boolean userJoining;
        //public AllPatterns regexPattern;
        public ChatModelOLD()
        {
            //_watcher = Watcher;
            //_Chat = new List<string>();
            PageActive = true;
            firstTime = true;
            reader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            lineReturn = "";
            debugCounter = 0;
            userJoining = false;
            //regexPattern = new AllPatterns();
        }


        public event Action OnChange;

        public void Add(string value)
        {
            //_Chat.Add(value);
            //NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public async Task StartReadFile(AllPatterns allPatterns)
        {
            try 
            {
                Regex rg;
                string[] subStrings;
                //start at the end of the file
                if (firstTime)
                {
                    lastMaxOffset = reader.BaseStream.Length;
                    firstTime = false;
                }
                //if the file size has not changed, idle
                if (reader.BaseStream.Length == lastMaxOffset)
                    Console.WriteLine("File Size currently: " + reader.BaseStream.Length, " , saved filed size: " + lastMaxOffset);
                else
                {
                    //seek to the last max offset
                    //ChatModel.reader.BaseStream.Seek(lastMaxOffset, SeekOrigin.Begin);

                    //read out of the file until the EOF
                    string line = "";                    
                    //line = reader.ReadLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        debugCounter++;
                        if (string.IsNullOrWhiteSpace(line) | Regex.IsMatch(line, "SendOutboundMessage")) { continue; }
                        else
                        {


                            foreach (var pattern in allPatterns._AllPatterns)
                            {
                                rg = new Regex(pattern.Value);
                                Match match = rg.Match(line);
                                if (match.Success)
                                {
                                    if(userJoining & Regex.IsMatch(match.Value, "LogEasyAntiCheatServer"))
                                    {
                                        subStrings = Regex.Split(line, pattern.Value);
                                        allPatterns.matchList("userJoining", match.Value, subStrings, userJoining);
                                        lineReturn = match.Value;
                                        userJoining = false;
                                        break;
                                    }
                                    else if(Regex.IsMatch(match.Value, "NewPlayer: BP_PlayerController_C"))
                                    {
                                        subStrings = Regex.Split(line, pattern.Value);
                                        allPatterns.matchList("userJoining", match.Value, subStrings);
                                        lineReturn = match.Value;
                                        userJoining = true;
                                        break;
                                    }
                                    else if (!lineReturn.Equals(match.Value))
                                    {
                                        subStrings = Regex.Split(line, pattern.Value);
                                        allPatterns.matchList(pattern.Key, match.Value, subStrings);
                                        lineReturn = match.Value;
                                        userJoining = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //update the last max offset
                    lastMaxOffset = reader.BaseStream.Position;
                }
            }
            catch(ArgumentException e) 
            {
                Console.WriteLine("ArgumentException Caught: " + e);
                Console.WriteLine(debugCounter);
            }
            
        }

        }
}
