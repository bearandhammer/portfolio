using DinoIpsum.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinoIpsum.Services
{
    public class DinoIpsumService : IDinoIpsumService
    {
        public Task<List<string>> GetIpsumParagraphs(int paragraphCount)
        {
            throw new NotImplementedException();
        }
    }
}
