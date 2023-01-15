using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Slack_App
{
    class SlackMessageReader
    {
        private readonly string _url;
        private readonly HttpClient _client;

        public SlackMessageReader(string token)
        {
            _url = "https://slack.com/api/conversations.list";
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<string> ReadMessage(string channel)
        {
            var postObject = new { channel = "#the-better-india", limit = 1 };
            var json = JsonConvert.SerializeObject(postObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}