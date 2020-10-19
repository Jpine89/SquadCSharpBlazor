using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SquadCSharpBlazor.Data
{
    public class ChatModel
    {

        //private FileSystemWatcher _watcher;
        public List<string> _Chat { get; set; }
        public Boolean PageActive { get; set; }
        public Boolean firstTime;
        private string fileName = "G:/SquadTestServer/servers/squad_server/SquadGame/Saved/Logs/ServerLog.log";
        public StreamReader reader;
        public ChatModel()
        {
            //_watcher = Watcher;
            _Chat = new List<string>();
            PageActive = true;
            firstTime = true;
            reader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }


        public event Action OnChange;

        public void Add(string value)
        {
            _Chat.Add(value);
            //NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public async Task StartReadFile()
        {
            string fileName = "G:/SquadTestServer/servers/squad_server/SquadGame/Saved/Logs/SquadGame.log";
            using (StreamReader reader = new StreamReader(new FileStream(fileName,
                     FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                //start at the end of the file
                long lastMaxOffset = reader.BaseStream.Length;

                while (PageActive)
                {
                    System.Threading.Thread.Sleep(100);

                    //if the file size has not changed, idle
                    if (reader.BaseStream.Length == lastMaxOffset)
                        continue;

                    //seek to the last max offset
                    reader.BaseStream.Seek(lastMaxOffset, SeekOrigin.Begin);

                    //read out of the file until the EOF
                    string line = "";
                    while ((line = reader.ReadLine()) != null && reader.ReadLine() != "")
                        //Console.WriteLine(line);
                        Add(line);

                    //update the last max offset
                    lastMaxOffset = reader.BaseStream.Position;
                }
            }
        }

        }
}
