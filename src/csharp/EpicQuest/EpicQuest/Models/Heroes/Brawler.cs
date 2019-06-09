using EpicQuest.Models.Dice;
using System.Collections.Generic;
using System;

namespace EpicQuest.Models
{
    /// <summary>
    /// Implementation of the Brawler hero type.
    /// </summary>
    public sealed class Brawler : Hero
    {
        public Brawler()
        {
            PerformSetup();
        }

        public override void PerformSetup()
        {
            Health = 14;
            CalculateDiceState();
        }

        public override string CharacterOpeningSpeech()
        {
            return base.CharacterOpeningSpeech();
        }
    }
}