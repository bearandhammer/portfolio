using EpicQuest.GameEnums;

namespace EpicQuest.Models.Dice
{
    public sealed class BlueDie : Die
    {
        private readonly DieFace[] dieFaces;

        public override DieFace[] DieFaces
        {
            get 
            {
                return dieFaces;
            }
        }

        public BlueDie()
        {
            dieFaces = new DieFace[6] { DieFace.SinglePoint, DieFace.DoublePoint, DieFace.Miss, DieFace.Miss, DieFace.DoublePoint, DieFace.Miss };
        }
    }
}
