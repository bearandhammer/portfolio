using System.Collections.Generic;
using System.Linq;

namespace HipsterIpsum.Module.ViewModels
{
    public class HipsterIpsumViewModel
    {
        public HipsterIpsumViewModel(List<string> hipsterIpsumParagraphs)
        {
            Cards = hipsterIpsumParagraphs.Select((paragraph, index) => new CardViewModel(index + 1, paragraph)).ToList();
        }

        public List<CardViewModel> Cards { get; }
    }
}
