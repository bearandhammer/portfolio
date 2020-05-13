using HipsterIpsum.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HipsterIpsum.Services
{
    public class HipsterIpsumService : IHipsterIpsumService
    {
        public async Task<List<string>> GetIpsumParagraphsAsync(int paragraphCount)
        {
            List<string> generatedParagraphs = new List<string>();

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync("https://hipsum.co/api/?type=hipster-centric&paras=3&start-with-lorem=1");
                generatedParagraphs.AddRange(JsonConvert.DeserializeObject<List<string>>(response));
            }

            return generatedParagraphs;
        }
    }
}
