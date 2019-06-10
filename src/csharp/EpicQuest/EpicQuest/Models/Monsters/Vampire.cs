using EpicQuest.Interfaces;
using EpicQuest.Models.Dice;
using System.Collections.Generic;
using System;

namespace EpicQuest.Models.Monsters
{
    public sealed class Vampire : Monster, IRoomContent
    {
        public Vampire()
        {
            PerformSetup();
        }

        public override void PerformSetup()
        {
            Health = 8;
            CalculateDiceState();
        }

        public override void CalculateDiceState()
        {        
            DefenceDice = new List<Die>(new Die[] { new BlueDie(), new BlueDie() });
            AttackDice = new List<Die>(new Die[] { new BlueDie(), new GreenDie() });
        }

        public override string CharacterOpeningSpeech()
        {
            return base.CharacterOpeningSpeech();
        }
    }
}
