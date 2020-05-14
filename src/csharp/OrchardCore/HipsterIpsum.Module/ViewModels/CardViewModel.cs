namespace HipsterIpsum.Module.ViewModels
{
    public class CardViewModel
    {
        public CardViewModel(int number, string content)
        {
            Number = number;
            Content = content;
        }

        public int Number { get; }

        public string Content { get; }
    }
}
