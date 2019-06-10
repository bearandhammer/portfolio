using EpicQuest.Interfaces;
using EpicQuest.Models.CharacterBase;
using System;
using System.Collections.Generic;

namespace EpicQuest.Models
{
    public abstract class Monster : CombatCharacter, ICalculateDiceState, ISupportsSetup
    {
        //public Monster()
        //    : base()
        //{
            
        //}

        public abstract void CalculateDiceState();
        public abstract void PerformSetup();
    }
}