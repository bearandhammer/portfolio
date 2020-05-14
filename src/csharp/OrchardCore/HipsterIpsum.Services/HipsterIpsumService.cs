using HipsterIpsum.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Modules.Shared.Configuration;
using Services.Shared.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HipsterIpsum.Services
{
    public class HipsterIpsumService : BaseApiService, IHipsterIpsumService
    {
        public HipsterIpsumService(IConfiguration configProvider)
            : base(configProvider)
        {
        }

        public async Task<List<string>> GetIpsumParagraphsAsync(int paragraphCount)
        {
            string apiUrl = string.Format(ConfigurationProvider.GetSection(ConfigurationKeys.ApiConfig).Get<ApiConfiguration>().HipsterIpsumApiUrl, paragraphCount);

            return await GetApiResultAsync<List<string>>(apiUrl);
        }
    }
}
