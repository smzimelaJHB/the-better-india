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
            string token = "";
            string query = "The meaning";
            var reader = new SlackMessageReader(token,query);
            var response = Task.Run(async () => await reader.SearchMessage("The meaning")).Result;
            Console.WriteLine(response);
        }
    }
}
