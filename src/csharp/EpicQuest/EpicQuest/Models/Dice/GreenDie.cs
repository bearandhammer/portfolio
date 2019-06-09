using EpicQuest.GameEnums;

namespace EpicQuest.Models.Dice
{
    public sealed class GreenDie : Die
    {
        private readonly DieFace[] dieFaces;

        public override DieFace[] DieFaces
        {
            get 
            {
                return dieFaces;
            }
        }

        public GreenDie()
        {
            dieFaces = new DieFace[6] { DieFace.Miss, DieFace.SinglePoint, DieFace.Miss, DieFace.SinglePoint, DieFace.Miss, DieFace.SinglePoint };
        }
    }
}
