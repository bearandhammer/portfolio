using System.Collections.Generic;

namespace EpicQuest.Models
{
    public sealed class Necromancer : Hero
    {
        public Necromancer()
        {
            PerformSetup();
        }

        public override void PerformSetup()
        {
            Health = 10;
            CalculateDiceState();
        }

        public override string CharacterOpeningSpeech()
        {
            return base.CharacterOpeningSpeech();
        }
    }
}
