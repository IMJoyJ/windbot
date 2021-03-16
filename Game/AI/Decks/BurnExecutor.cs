using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("Burn", "AI_Burn", "Easy")]
    public class BurnExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int LavaGolem = 102380;
            public const int ReflectBounder = 2851070;
            public const int FencingFireFerret = 97396380;
            public const int BlastSphere = 26302522;
            public const int Marshmallon = 31305911;
            public const int SpiritReaper = 23205979;
            public const int NaturiaBeans = 44789585;
            public const int ThunderShort = 20264508;
            public const int Ookazi = 19523799;
            public const int GoblinThief = 45311864;
            public const int TremendousFire = 46918794;
            public const int SwordsOfRevealingLight = 72302403;
            public const int SupremacyBerry = 98380593;
            public const int ChainEnergy = 79323590;
            public const int DarkRoomofNightmare = 85562745;
            public const int PoisonOfTheOldMan = 8842266;
            public const int OjamaTrio = 29843091;
            public const int Ceasefire = 36468556;
            public const int MagicCylinder = 62279055;
            public const int MinorGoblinOfficial = 1918087;
            public const int ChainBurst = 48276469;
            public const int SkullInvitation = 98139712;
        }

        public BurnExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // Set traps
            this.AddExecutor(ExecutorType.SpellSet, this.DefaultSpellSet);

            // Activate Spells
            this.AddExecutor(ExecutorType.Activate, CardId.DarkRoomofNightmare);
            this.AddExecutor(ExecutorType.Activate, CardId.Ookazi);
            this.AddExecutor(ExecutorType.Activate, CardId.GoblinThief);
            this.AddExecutor(ExecutorType.Activate, CardId.TremendousFire);
            this.AddExecutor(ExecutorType.Activate, CardId.SwordsOfRevealingLight, this.SwordsOfRevealingLight);
            this.AddExecutor(ExecutorType.Activate, CardId.SupremacyBerry, this.SupremacyBerry);
            this.AddExecutor(ExecutorType.Activate, CardId.PoisonOfTheOldMan, this.PoisonOfTheOldMan);
            this.AddExecutor(ExecutorType.Activate, CardId.ThunderShort, this.ThunderShort);

            // Hello, my name is Lava Golem
            this.AddExecutor(ExecutorType.SpSummon, CardId.LavaGolem, this.LavaGolem);

            // Set an invincible monster
            this.AddExecutor(ExecutorType.MonsterSet, CardId.Marshmallon, this.SetInvincibleMonster);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.SpiritReaper, this.SetInvincibleMonster);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.BlastSphere);

            // Set other monsters
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.FencingFireFerret);
            this.AddExecutor(ExecutorType.Summon, CardId.ReflectBounder);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.NaturiaBeans);

            // We're a coward
            this.AddExecutor(ExecutorType.Repos, this.ReposEverything);

            // Chain traps
            this.AddExecutor(ExecutorType.Activate, CardId.MagicCylinder, this.DefaultTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.Ceasefire, this.Ceasefire);
            this.AddExecutor(ExecutorType.Activate, CardId.OjamaTrio);
            this.AddExecutor(ExecutorType.Activate, CardId.MinorGoblinOfficial);
            this.AddExecutor(ExecutorType.Activate, CardId.ChainBurst);
            this.AddExecutor(ExecutorType.Activate, CardId.SkullInvitation);
            this.AddExecutor(ExecutorType.Activate, CardId.ChainEnergy);
        }

        public override bool OnSelectHand()
        {
            return true;
        }

        private bool SwordsOfRevealingLight()
        {
            int count = this.Bot.SpellZone.GetCardCount(CardId.SwordsOfRevealingLight);
            return count == 0;
        }

        private bool SupremacyBerry()
        {
            return this.Bot.LifePoints < this.Enemy.LifePoints;
        }

        private bool PoisonOfTheOldMan()
        {
            this.AI.SelectOption(1);
            return true;
        }

        private bool ThunderShort()
        {
            return this.Enemy.GetMonsterCount() >= 3;
        }

        private bool SetInvincibleMonster()
        {
            foreach (ClientCard card in this.Bot.GetMonsters())
            {
                if (card.IsCode(CardId.Marshmallon, CardId.SpiritReaper))
                {
                    return false;
                }
            }
            return true;
        }

        private bool LavaGolem()
        {
            bool found = false;
            foreach (ClientCard card in this.Enemy.GetMonsters())
            {
                if (card.Attack > 2000)
                {
                    found = true;
                }
            }
            return found;
        }

        private bool Ceasefire()
        {
            return this.Bot.GetMonsterCount() + this.Enemy.GetMonsterCount() >= 3;
        }

        private bool ReposEverything()
        {
            if (this.Card.IsCode(CardId.ReflectBounder))
            {
                return this.Card.IsDefense();
            }

            if (this.Card.IsCode(CardId.FencingFireFerret))
            {
                return this.DefaultMonsterRepos();
            }

            if (this.Card.IsAttack())
            {
                return true;
            }

            return false;
        }
    }
}