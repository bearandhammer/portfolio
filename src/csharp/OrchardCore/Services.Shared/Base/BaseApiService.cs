using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services.Shared.Base
{
    public abstract class BaseApiService
    {
        public BaseApiService(IConfiguration configProvider)
        {
            ConfigurationProvider = configProvider;
        }

        protected IConfiguration ConfigurationProvider { get; }

        protected virtual async Task<T> GetApiResultAsync<T>(string apiUrl)
        {
            T requestedReturnValue;

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(apiUrl);
                requestedReturnValue = JsonConvert.DeserializeObject<T>(response);
            }

            return requestedReturnValue;
        }
    }
}
