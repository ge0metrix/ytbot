using System;
using System.Threading.Tasks;
using SlackNet.Bot;
using System.Text.RegularExpressions;

namespace SlackNet.BotExample
{
    internal class PingHandler : IMessageHandler
    {
        private int _pingcount;
        private int _pongcount;

        public async Task HandleMessage(IMessage message)
        {
            
            Regex pingrex = new Regex(@"\bping\b", RegexOptions.IgnoreCase);
            Regex pongrex = new Regex(@"\bpong\b", RegexOptions.IgnoreCase);
            if (pingrex.Match(message.Text).Success)
            {
                Console.WriteLine($"Received ping from @{message.User.Name}");
                await message.ReplyWith(new BotMessage{Text = $"PONG - ping Count: {++_pingcount}"}).ConfigureAwait(false);
            }
            if (pongrex.Match(message.Text).Success)
            {
                Console.WriteLine($"Received pong from @{message.User.Name}");
                await message.ReplyWith(new BotMessage { Text = $"PING - pong count: {++_pongcount}"}).ConfigureAwait(false);
            }
         }
    }
}