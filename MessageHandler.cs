using System;
using System.Threading.Tasks;
using SlackNet.Bot;
using System.Text.Json;
using System.Text.Json.Serialization;


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
                string logline = String.Format(format, message.Timestamp, message.User.Name, message.Text, message.Conversation.Name);
                Console.WriteLine(logline);
                if(_logtofile){
                    await System.IO.File.AppendAllTextAsync(_logfile,logline);
                }
            }
            
        }
    }
}