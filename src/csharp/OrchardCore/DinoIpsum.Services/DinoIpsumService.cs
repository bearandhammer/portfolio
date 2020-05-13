using DinoIpsum.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Modules.Shared.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DinoIpsum.Services
{
    public class DinoIpsumService : IDinoIpsumService
    {
        private readonly IConfiguration configurationProvider;

        public DinoIpsumService(IConfiguration configProvider)
        {
            configurationProvider = configProvider;
        }

        public async Task<List<string>> GetIpsumParagraphsAsync(int paragraphCount)
        {
            string apiUrl = string.Format(configurationProvider.GetSection(ConfigurationKeys.ApiConfig).Get<ApiConfiguration>().DinoIpsumApiUrl, paragraphCount);

            List<List<string>> generatedParagraphs = new List<List<string>>();

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(apiUrl);
                generatedParagraphs.AddRange(JsonConvert.DeserializeObject<List<List<string>>>(response));
            }

            return generatedParagraphs.Select(gp => string.Join(" ", gp)).ToList();
        }
    }
}
