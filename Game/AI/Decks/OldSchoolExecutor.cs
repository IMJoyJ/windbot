using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("OldSchool", "AI_OldSchool", "Easy")]
    public class OldSchoolExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int AncientGearGolem = 83104731;
            public const int Frostosaurus = 6631034;
            public const int AlexandriteDragon = 43096270;
            public const int GeneWarpedWarwolf = 69247929;
            public const int GearGolemTheMovingFortress = 30190809;
            public const int EvilswarmHeliotrope = 77542832;
            public const int LusterDragon = 11091375;
            public const int InsectKnight = 35052053;
            public const int ArchfiendSoldier = 49881766;

            public const int HeavyStorm = 19613556;
            public const int DarkHole = 53129443;
            public const int Raigeki = 12580477;
            public const int HammerShot = 26412047;
            public const int Fissure = 66788016;
            public const int SwordsOfRevealingLight = 72302403;
            public const int DoubleSummon = 43422537;

            public const int MirrorForce = 44095762;
            public const int DimensionalPrison = 70342110;

        }

        public OldSchoolExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.Activate, CardId.HeavyStorm, this.DefaultHeavyStorm);
            this.AddExecutor(ExecutorType.SpellSet, this.DefaultSpellSet);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);
            this.AddExecutor(ExecutorType.Activate, CardId.Raigeki, this.DefaultRaigeki);
            this.AddExecutor(ExecutorType.Activate, CardId.HammerShot, this.DefaultHammerShot);
            this.AddExecutor(ExecutorType.Activate, CardId.Fissure);
            this.AddExecutor(ExecutorType.Activate, CardId.SwordsOfRevealingLight, this.SwordsOfRevealingLight);
            this.AddExecutor(ExecutorType.Activate, CardId.DoubleSummon, this.DoubleSummon);

            this.AddExecutor(ExecutorType.Summon, CardId.AncientGearGolem, this.DefaultTributeSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.Frostosaurus, this.DefaultTributeSummon);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.AlexandriteDragon);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.GeneWarpedWarwolf);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.GearGolemTheMovingFortress);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.EvilswarmHeliotrope);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.LusterDragon);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.InsectKnight);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.ArchfiendSoldier);

            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);

            this.AddExecutor(ExecutorType.Activate, CardId.MirrorForce, this.DefaultTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.DimensionalPrison, this.DefaultTrap);
        }

        private int _lastDoubleSummon;

        private bool DoubleSummon()
        {
            if (this._lastDoubleSummon == this.Duel.Turn)
            {
                return false;
            }

            if (this.Main.SummonableCards.Count == 0)
            {
                return false;
            }

            if (this.Main.SummonableCards.Count == 1 && this.Main.SummonableCards[0].Level < 5)
            {
                bool canTribute = false;
                foreach (ClientCard handCard in this.Bot.Hand)
                {
                    if (handCard.IsMonster() && handCard.Level > 4 && handCard.Level < 6)
                    {
                        canTribute = true;
                    }
                }
                if (!canTribute)
                {
                    return false;
                }
            }

            int monsters = 0;
            foreach (ClientCard handCard in this.Bot.Hand)
            {
                if (handCard.IsMonster())
                {
                    monsters++;
                }
            }
            if (monsters <= 1)
            {
                return false;
            }

            this._lastDoubleSummon = this.Duel.Turn;
            return true;
        }

        private bool SwordsOfRevealingLight()
        {
            foreach (ClientCard handCard in this.Enemy.GetMonsters())
            {
                if (handCard.IsFacedown())
                {
                    return true;
                }
            }
            return this.Util.IsOneEnemyBetter(true);
        }
    }
}