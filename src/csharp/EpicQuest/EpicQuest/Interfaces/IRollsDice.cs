using EpicQuest.Models;
using System.Collections.Generic;

namespace EpicQuest.Interfaces
{
    interface IRollsDice
    {
        List<Die> Dice { get; set; }
    }
}