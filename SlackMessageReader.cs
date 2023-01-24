using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Slack_App
{
    class SlackMessageReader
    {
        private readonly string _url;
        private readonly HttpClient _client;

        public SlackMessageReader(string token,string query)
        {
            _url = "https://slack.com/api/search.messages?query="+query+"&pretty=1";
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<string> SearchMessage(string _query)
        {   
            var postString = "&highlight=1&count=1";
            var content = new StringContent(postString, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            dynamic data = JObject.Parse(responseString);
            WriteToCSV(data);
            return "Done";
        } 

        public void WriteToCSV(dynamic data){

            //initialize lists
            List <string> matchedUsernames = new List<string>();
            List <string> matchedChannelNames = new List<string>();
            List <string> matchedDates = new List<string>();
            List <string> matchedMessages = new List<string>();

            //add data to lists
            for (int i = 0; i < data.messages.matches.Count; i++)
            {
                matchedUsernames.Add(data.messages.matches[i].username.ToString());
                matchedChannelNames.Add(data.messages.matches[i].channel.name.ToString());
                matchedDates.Add(new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds((double)data.messages.matches[i].ts).ToLocalTime().ToString());
                matchedMessages.Add(data.messages.matches[i].text.ToString());

            }

            //write to csv
            var csv = new StringBuilder();
            var Headers = new string[] { "Username", "ChannelName", "Date", "Message" };
            var newLine = string.Join(",", Headers);
            csv.AppendLine(newLine+Environment.NewLine);

            //add data to csv
            for (int i = 0; i < matchedUsernames.Count; i++)
            {
                newLine = string.Join(",", matchedUsernames[i], matchedChannelNames[i], matchedDates[i], matchedMessages[i]);
                csv.AppendLine(newLine);
            }

            //write to file
            File.WriteAllText("data.csv", csv.ToString());
            Console.WriteLine(csv.ToString());
        }
    }
}   