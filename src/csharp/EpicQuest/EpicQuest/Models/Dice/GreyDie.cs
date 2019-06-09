using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpicQuest.GameEnums;

namespace EpicQuest.Models.Dice
{
    public sealed class GreyDie : Die
    {
        private readonly DieFace[] dieFaces;

        public override DieFace[] DieFaces
        {
            get
            {
                return dieFaces;
            }
        }

        public GreyDie()
        {
            dieFaces = new DieFace[6] { DieFace.Miss, DieFace.Miss, DieFace.Miss, DieFace.Miss, DieFace.Miss, DieFace.SinglePoint };
        }
    }
}
