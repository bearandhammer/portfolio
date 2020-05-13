using DinoIpsum.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DinoIpsum.Services
{
    public class DinoIpsumService : IDinoIpsumService
    {
        public async Task<List<string>> GetIpsumParagraphsAsync(int paragraphCount)
        {
            List<List<string>> generatedParagraphs = new List<List<string>>();

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync("http://dinoipsum.herokuapp.com/api/?format=json&paragraphs=3&words=30");
                generatedParagraphs.AddRange(JsonConvert.DeserializeObject<List<List<string>>>(response));
            }

            return generatedParagraphs.Select(gp => string.Join(" ", gp)).ToList();
        }
    }
}
