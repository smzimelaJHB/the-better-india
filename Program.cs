using System;
using System.Threading.Tasks;

namespace Slack_App
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = "xoxb-4629455833220-4658671317777-9czZkjAbZAeu2olb078oZBO1";
            var writer = new SlackMessageWriter(token);
            Task.WaitAll(writer.WriteMessage("Hello from C#!"));
        }
    }
}
