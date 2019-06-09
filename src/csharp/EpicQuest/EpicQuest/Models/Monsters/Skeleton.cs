using EpicQuest.Interfaces;
using EpicQuest.Models.Dice;
using System.Collections.Generic;
using System;

namespace EpicQuest.Models
{
    public sealed class Skeleton : Monster, IRoomContent
    {
        public Skeleton()
        {
            Health = 6;
            CalculateDiceState();
        }

        public override void PerformSetup()
        {
            Health = 6;
            CalculateDiceState();
        }

        public override void CalculateDiceState()
        {
            DefenceDice = new List<Die>(new Die[] { new BlueDie(), new GreenDie() });
            AttackDice = new List<Die>(new Die[] { new GreenDie(), new GreenDie() });
        }

        public override string CharacterOpeningSpeech()
        {
            return base.CharacterOpeningSpeech();
        }
    }
}