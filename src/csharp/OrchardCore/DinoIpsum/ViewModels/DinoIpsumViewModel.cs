using System.Collections.Generic;

namespace DinoIpsum.Module.ViewModels
{
    public class DinoIpsumViewModel
    {
        public DinoIpsumViewModel(List<string> dinoIpsumParagraphs)
        {
            DinoParagraphs = dinoIpsumParagraphs;
        }

        public List<string> DinoParagraphs { get; }
    }
}
