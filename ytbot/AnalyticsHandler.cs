using System;
using System.Threading.Tasks;
using SlackNet.Bot;
using Azure;
using Azure.AI.TextAnalytics;
namespace SlackNet.BotExample
{
    internal class AnalyticsHandler : IMessageHandler
    {
        private static readonly AzureKeyCredential credentials = new AzureKeyCredential("<TOKEN>");
        private static readonly Uri endpoint = new Uri("<ENDPOINT>>");
        public async Task HandleMessage(IMessage message)
        {   
            var client = new TextAnalyticsClient(endpoint, credentials);
            await Analyze(client, message.Text);
        }
        static async Task Analyze(TextAnalyticsClient client, string text)
        {
            DocumentSentiment sentiment = await client.AnalyzeSentimentAsync(text);
            Console.WriteLine($"Sentiment: {sentiment.ConfidenceScores.Positive}:{sentiment.ConfidenceScores.Neutral}:{sentiment.ConfidenceScores.Negative} - {sentiment.Sentiment}");

            var keywords = await client.ExtractKeyPhrasesAsync(text);
            // Printing key phrases
            Console.WriteLine("Key phrases:");

            foreach (string keyphrase in keywords.Value)
            {
                Console.WriteLine($"\t{keyphrase}");
            }
        }
    }
}