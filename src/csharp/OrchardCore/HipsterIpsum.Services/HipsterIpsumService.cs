using HipsterIpsum.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Modules.Shared.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HipsterIpsum.Services
{
    public class HipsterIpsumService : IHipsterIpsumService
    {
        private readonly IConfiguration configurationProvider;

        public HipsterIpsumService(IConfiguration configProvider)
        {
            configurationProvider = configProvider;
        }

        public async Task<List<string>> GetIpsumParagraphsAsync(int paragraphCount)
        {
            string apiUrl = string.Format(configurationProvider.GetSection(ConfigurationKeys.ApiConfig).Get<ApiConfiguration>().HipsterIpsumApiUrl, paragraphCount);

            List<string> generatedParagraphs = new List<string>();

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(apiUrl);
                generatedParagraphs.AddRange(JsonConvert.DeserializeObject<List<string>>(response));
            }

            return generatedParagraphs;
        }
    }
}
