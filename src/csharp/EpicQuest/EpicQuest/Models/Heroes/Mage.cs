using System.Collections.Generic;

namespace EpicQuest.Models
{
    public sealed class Mage : Hero
    {
        public Mage()
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
