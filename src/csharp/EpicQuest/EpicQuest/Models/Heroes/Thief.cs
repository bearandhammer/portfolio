using System.Collections.Generic;

namespace EpicQuest.Models
{
    public sealed class Thief : Hero
    {
        public Thief()
        {
            PerformSetup();
        }

        public override void PerformSetup()
        {
            Health = 12;
            CalculateDiceState();
        }

        public override string CharacterOpeningSpeech()
        {
            return base.CharacterOpeningSpeech();
        }
    }
}
