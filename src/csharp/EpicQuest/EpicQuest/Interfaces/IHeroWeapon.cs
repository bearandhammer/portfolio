using EpicQuest.GameEnums;

namespace EpicQuest.Interfaces
{
    interface IHeroWeapon : IRollsDice
    {
        HeroOffensiveItemType OffensiveItemType { get; set; }
    }
}
