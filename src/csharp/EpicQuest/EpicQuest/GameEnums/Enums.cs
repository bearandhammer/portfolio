namespace EpicQuest.GameEnums
{
    #region Debug Enum

    /// <summary>
    /// Public enum containing values denoting
    /// the debug message types available (i.e. for use
    /// in extension methods, etc).
    /// </summary>
    public enum DebugMsgType : byte
    {
        MethodEntered = 1,
        MethodLeft = 2
    }

    #endregion Debug Enum

    #region Game Enums

    /// <summary>
    /// Public enum representing the various
    /// Game Actions that result from user input.
    /// </summary>
    public enum GameAction : byte
    {
        ExitGame = 1,
        Invalid = 2,
        BrawlerChosen = 3,
        ClericChosen = 4,
        MageChosen = 5,
        NecromancerChosen = 6,
        ThiefChosen = 7
    }

    /// <summary>
    /// Public enum representing the various Hero
    /// Types available to the player when building a party.
    /// </summary>
    public enum HeroClass : byte
    {
        Brawler = 1,
        Cleric = 2,
        Mage = 3,
        Necromancer = 4,
        Thief = 5,
        Unspecified = 6
    }

    /// <summary>
    /// 
    /// </summary>
    public enum HeroOffensiveItemType : byte
    {
        Fists = 1,
        RustyDagger = 2,
        RustyMace = 3,
        RustyShortSword = 4,
        RustyBastardSword = 5,
        SilverHexDagger = 6,
        MagicMissile = 7,
        FlameTorch = 8,
        Freeze = 9,
        FineBarbedWireMace = 10,
        JaggedSword = 11,
        Disolve = 12,
        LegendaryBastardSword = 13
    }

    /// <summary>
    /// 
    /// </summary>
    public enum HeroDefensiveItemType : byte
    {
        //Level one
        ChestRags = 1,
        FeetRags = 2,
        HandRags = 3,
        HeadRags = 4,
        LegRags = 5,
        StringNeckace = 6,
        BronzeRing = 7,
        ClothTunic = 8,
        LeatherHelm = 9,
        LeatherGloves = 10,
        LeatherChestPiece = 11,
        LeatherBoots = 12,
        LeatherPants = 13,
        //Level two
        SilverNecklace = 14,
        SilverRing = 15,
        NoviceMagicianRobe = 16,
        NoviceRobeOfSkulls = 17,
        ChainMailHelm = 18,
        ChainMailGloves = 19,
        ChainMailChestPiece = 21,
        ChainMailBoots = 22,
        ChainMailPants = 23,
        //Level three
        GoldNecklace = 24,
        GoldRing = 25,
        AdeptMagicianDragonSkinRobe = 26,
        AdeptRobeOfDemonSkulls = 27,
        MasterworkPlateHelm = 28,
        MasterworkPlateGloves = 29,
        MasterworkPlateChestPiece = 30,
        MasterworkPlateBoots = 31,
        MasterworkPlatePants = 32,
        //Level four
        DragonSkinRobes = 33,
        DragonSkinChestPiece = 34,
        //Level five
        BloodStainedMithralRobe = 35,
        BloodStainedMithralArmour = 36,
        None = 37
    }

    /// <summary>
    /// 
    /// </summary>
    public enum HeroDefensiveItemSlotType : byte
    {
        Head = 1,
        Chest = 2,
        Hands = 3,
        Legs = 4,
        Feet = 5,
        Necklace = 6,
        Ring = 7,
        None = 8
    }

    /// <summary>
    /// 
    /// </summary>
    public enum DieFace : byte
    {
        Miss = 0,
        SinglePoint = 1,
        DoublePoint = 2,
        TriplePoint = 3,
        CriticalPoint = 4
    }

    #endregion Game Enums
}
