using System;
using EpicQuest.Interfaces;

namespace EpicQuest.Utility
{
    /// <summary>
    /// Public static utility class for the
    /// EpicDungeon game.
    /// </summary>
    public static class StaticGameActions
    {
        /// <summary>
        /// Private method that shows, in principle, how combat
        /// can be handled between two ICombatant based objects.
        /// </summary>
        /// <param name="aggressor">The instigator of the combat represented by a class implementing ICombatant.</param>
        /// <param name="target">The defender represented by a class implementing ICombatant.</param>
        public static int HandleCombat(ICombatant aggressor, ICombatant target)
        {
            //Prepare values representing the dice (random) and values to keep a tally of the offence, defence and difference totals
            int difference = 0;
            Random rollDie = new Random();

            //Determine the difference between the damage/defence totals and deal damage to the target if required
            difference = aggressor.RollAttackDie() - target.RollDefenceDie();

            if (difference > 0)
            {
                target.Health -= difference;
            }

            return difference < 0 ? 0 : difference; //Only return non-negative results
        }
    }
}