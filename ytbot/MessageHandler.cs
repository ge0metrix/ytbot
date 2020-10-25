using System;
using System.Threading.Tasks;
using SlackNet.Bot;

namespace SlackNet.BotExample
{
    internal class MessageHandler : IMessageHandler
    {
       
        private bool _logtofile;
        private string _logfile;
        public MessageHandler(){
            _logtofile = false;
        }
        public MessageHandler(string filePath){
            _logfile = filePath;
            _logtofile = true;
        }
        public async Task HandleMessage(IMessage message)
        {   
            if(message.Conversation.IsChannel){
                string format = "[{0:u}] [{3}] <{1}> {2}\r\n";
                string username = message.User.Profile.DisplayName;
                if (message.User.Profile.DisplayName == ""){
                    username = message.User.Name;
                }
                string logline ="";
                string[] loglines = message.Text.Split(new string[] { "\r\n", "\r", "\n" },StringSplitOptions.None);

                foreach(string line in loglines){
                    Console.WriteLine(String.Format(format, message.Timestamp, username, line, message.Conversation.Name));
                    logline = logline + String.Format(format, message.Timestamp, username, line, message.Conversation.Name);
                }
                //Console.Write(logline);
                if(_logtofile){
                    await System.IO.File.AppendAllTextAsync(_logfile,logline);
                }
            }
            
        }
    }
}