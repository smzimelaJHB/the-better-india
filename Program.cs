using System;
using System.Threading.Tasks;

namespace Slack_App
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = "xoxb-4629455833220-4658671317777-kLdleXZEg6n3IwPgxCidvx7w";
            var writer = new SlackMessageWriter(token);
            Task.WaitAll(writer.WriteMessage("C# test message-scanner app"));
        }
    }
}
