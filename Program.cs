using System;
using System.Threading.Tasks;

namespace Slack_App
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = "xoxb-4629455833220-4658671317777-hbLNjOwapo3OLSNoJPrKbZD6";
            var writer = new SlackMessageWriter(token);
            var reader = new SlackMessageReader(token);

            Task.WaitAll(writer.WriteMessage("C# test message-scanner app"));
            var response = Task.Run(async () => await reader.ReadMessage("C04JHEXM4A0","15712345.001500")).Result;
            Console.WriteLine(response);
        }
    }
}
