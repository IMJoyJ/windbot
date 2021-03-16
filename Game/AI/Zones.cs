namespace WindBot.Game.AI
{
    public static class Zones
    {
        public const int MonsterZone1 = 0x1,
        MonsterZone2 = 0x2,
        MonsterZone3 = 0x4,
        MonsterZone4 = 0x8,
        MonsterZone5 = 0x10,
        ExtraZone1 = 0x20,
        ExtraZone2 = 0x40,

        MonsterZones = 0x7f,
        MainMonsterZones = 0x1f,
        ExtraMonsterZones = 0x60,

        SpellZones = 0x1f,

        PendulumZones = 0x3,

        LinkedZones = 0x10000,
        NotLinkedZones = 0x20000;
    }
}