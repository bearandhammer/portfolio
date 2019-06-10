using EpicQuest.GameEnums;

namespace EpicQuest.Interfaces
{
    interface IHeroArmour : IRollsDice
    {
        HeroDefensiveItemType DefensiveItemType { get; set; }
        HeroDefensiveItemSlotType DefensiveItemTypeSlot { get; }
    }
}
