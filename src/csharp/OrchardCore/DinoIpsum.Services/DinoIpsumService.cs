using DinoIpsum.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Modules.Shared.Configuration;
using Newtonsoft.Json;
using Services.Shared.Base;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DinoIpsum.Services
{
    public class DinoIpsumService : BaseApiService, IDinoIpsumService
    {
        public DinoIpsumService(IConfiguration configProvider)
            : base(configProvider)
        {

        }

        public async Task<List<string>> GetIpsumParagraphsAsync(int paragraphCount)
        {
            string apiUrl = string.Format(ConfigurationProvider.GetSection(ConfigurationKeys.ApiConfig).Get<ApiConfiguration>().DinoIpsumApiUrl, paragraphCount);

            List<List<string>> generatedParagraphs = await GetApiResultAsync<List<List<string>>>(apiUrl);

            return generatedParagraphs.Select(gp => string.Join(" ", gp)).ToList();
        }
    }
}
