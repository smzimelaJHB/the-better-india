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

        public SlackMessageReader(string token)
        {
            _url = "https://slack.com/api/search.messages?query=The%20meaning&pretty=1";
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<string> ReadMessage(string c, string Ts)
        {
            var postString = "channel=" + c + "&limit=1&inclusive=true&latest=" + Ts;
            var content = new StringContent(postString, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public async Task<string> SearchMessage(string _query)
        {   
            var postString = "&highlight=1&count=1";
            var content = new StringContent(postString, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            dynamic data = JObject.Parse(responseString);

            List <string> matchedUsernames = new List<string>();
            List <string> matchedChannelNames = new List<string>();
            List <string> matchedDates = new List<string>();
            List <string> matchedMessages = new List<string>();

            for (int i = 0; i < data.messages.matches.Count; i++)
            {
                matchedUsernames.Add(data.messages.matches[i].username.ToString());
                matchedChannelNames.Add(data.messages.matches[i].channel.name.ToString());
                matchedDates.Add(data.messages.matches[i].ts.ToString());
                matchedMessages.Add(data.messages.matches[i].text.ToString());
            }
            
            return "";
        } 
    }
}   