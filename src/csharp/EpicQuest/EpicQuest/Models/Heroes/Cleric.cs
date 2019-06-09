using System;
using System.Collections.Generic;

namespace EpicQuest.Models
{
    public sealed class Cleric : Hero
    {
        public Cleric()
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
