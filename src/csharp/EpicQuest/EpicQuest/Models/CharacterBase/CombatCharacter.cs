using EpicQuest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicQuest.Models.CharacterBase
{
    public abstract class CombatCharacter : GameCharacter, ICombatant
    {
        protected List<Die> DefenceDice { get; set; }
        protected List<Die> AttackDice { get; set; }

        public int Health { get; set; }

        public CombatCharacter()
        {

        }

        public virtual int RollAttackDie()
        {
            int damageTotal = 0;
            Random rollDie = new Random();

            //Roll attack dice for the aggressor and get a damage total
            AttackDice.ForEach(attackDie =>
            {
                damageTotal += (byte)attackDie.DieFaces[rollDie.Next(0, 5)];
            });

            return damageTotal;
        }

        public virtual int RollDefenceDie()
        {
            int defenceTotal = 0;
            Random rollDie = new Random();

            //Roll defence dice for the defender and get a defence total
            DefenceDice.ForEach(defenceDie =>
            {
                defenceTotal += (byte)defenceDie.DieFaces[rollDie.Next(0, 5)];
            });

            return defenceTotal;
        }
    }
}
