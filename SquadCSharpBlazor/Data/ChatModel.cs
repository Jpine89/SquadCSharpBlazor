using SquadCSharpBlazor.Patterns;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SquadCSharpBlazor.Data
{
    public class ChatModel
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
        //public AllPatterns regexPattern;
        public ChatModel()
        {
            //_watcher = Watcher;
            //_Chat = new List<string>();
            PageActive = true;
            firstTime = true;
            reader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            lineReturn = "";
            debugCounter = 0;
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
                    int counter = 0;
                    
                    //line = reader.ReadLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        debugCounter++;
                        if (string.IsNullOrWhiteSpace(line) | Regex.IsMatch(line, "SendOutboundMessage")) { continue; }
                        else
                        {
                            foreach (var pattern in allPatterns._stringPatterns)
                            {
                                rg = new Regex(pattern);
                                Match match = rg.Match(line);
                                if (match.Success)
                                {
                                    if (!lineReturn.Equals(match.Value))
                                    {
                                        //Console.WriteLine(match.Value);
                                        subStrings = Regex.Split(line, pattern);
                                        foreach(var test in subStrings)
                                            Console.WriteLine(test);
                                        allPatterns.matchList(allPatterns._stringTypes[counter], match.Value, subStrings);
                                        lineReturn = match.Value;
                                    }
                                    //Console.WriteLine(match.Value);

                                    //

                                }
                                counter++;
                            }
                            counter = 0;
                        }
                    }

                    //update the last max offset
                    lastMaxOffset = reader.BaseStream.Position;
                }
            }
            catch(ArgumentException e) 
            {
                Console.WriteLine("ArgumentException Caught");
                Console.WriteLine(debugCounter);
            }
            
        }

        }
}
