using EpicQuest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicQuest.Interfaces
{
    public interface ISupportsWeapon
    {
        Weapon HeroWeapon { get; set; }
    }
}
