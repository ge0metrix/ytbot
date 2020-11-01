using SlackNet.Bot;
using System;
using System.Threading.Tasks;

namespace SlackNet.BotExample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try{
                Console.WriteLine(args[0]);

                await Run(args[0],args[1]).ConfigureAwait(false);
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

            }
        }

        private static async Task Run(string token, string logfile)
        {
            using(var bot = new SlackBot(token)){

                bot.AddHandler(new PingHandler());
                //bot.AddHandler(new MessageHandler());
                bot.AddHandler(new MessageHandler(logfile));
                bot.AddHandler(new AnalyticsHandler());
                
                await bot.Connect().ConfigureAwait(false);
                Console.WriteLine("Connected");
                await WaitForKeyPress().ConfigureAwait(false);
            }
        }

        private static async Task WaitForKeyPress()
        {
            Console.WriteLine("Press any key to disconnect...");
            while (!Console.KeyAvailable)
                await Task.Delay(250).ConfigureAwait(false);
            Console.ReadKey();
        }
    }
}