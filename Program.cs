using System;
using System.Threading.Tasks;
using Jering.Javascript.NodeJS;
using Microsoft.Extensions.DependencyInjection;

namespace Slack_App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string token = "xoxp-4629455833220-4639625795521-4662937656563-04dec08ea91e20c10be5f04eac558d72";
            var reader = new SlackMessageReader(token);
            var response2 = Task.Run(async () => await reader.SearchMessage("The meaning")).Result;
            Console.WriteLine(response2);
        }
    }
}
