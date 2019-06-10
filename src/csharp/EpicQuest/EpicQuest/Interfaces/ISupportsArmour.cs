using EpicQuest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicQuest.Interfaces
{
    public interface ISupportsArmour
    {
        Armour ChestSlot { get; set; }
        Armour HeadSlot { get; set; }
        Armour HandSlot { get; set; }
        Armour FeetSlot { get; set; }
        Armour LegSlot { get; set; }
        Armour NecklaceSlot { get; set; }
        Armour RingSlot { get; set; }
    }
}
