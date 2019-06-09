using EpicQuest.Models;
using System.Collections.Generic;
namespace EpicQuest.Interfaces
{
    public interface ICombatant
    {
        int Health { get; set; }
        int RollDefenceDie();
        int RollAttackDie();
    }
}
