using EpicQuest.Interfaces;
using EpicQuest.Models.Dice;
using System.Collections.Generic;
using System;

namespace EpicQuest.Models
{
    public sealed class Kobold : Monster, IRoomContent
    {
        public Kobold()
            : base()
        {
            PerformSetup();
        }

        public override void PerformSetup()
        {
            Health = 4;
            CalculateDiceState();
        }

        public override void CalculateDiceState()
        {
            DefenceDice = new List<Die>(new Die[] { new GreenDie() });
            AttackDice = new List<Die>(new Die[] { new GreenDie() });
        }

        public override string CharacterOpeningSpeech()
        {
            return base.CharacterOpeningSpeech();
        }
    }
}