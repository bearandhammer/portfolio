using Jeffsum;
using JeffSum.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JeffSum.Services.Services
{
    /// <summary>
    /// Represents a service that provides Jeff Goldlum
    /// style Ipsum content.
    /// </summary>
    public class JeffIpsumService : IJeffIpsumService
    {
        /// <inheritdoc/>
        public async Task<List<string>> GetIpsumParagraphs(int paragraphCount) => await Task.Run(() => Goldblum.ReceiveTheJeff(paragraphCount).ToList());
    }
}
