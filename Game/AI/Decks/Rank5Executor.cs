using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
    [Deck("Rank V", "AI_Rank5")]
    public class Rank5Executor : DefaultExecutor
    {
        public class CardId
        {
            public const int MistArchfiend = 28601770;
            public const int CyberDragon = 70095154;
            public const int ZWEagleClaw = 29353756;
            public const int SolarWindJammer = 33911264;
            public const int QuickdrawSynchron = 20932152;
            public const int WindUpSoldier = 12299841;
            public const int StarDrawing = 24610207;
            public const int ChronomalyGoldenJet = 88552992;

            public const int InstantFusion = 1845204;
            public const int DoubleSummon = 43422537;
            public const int MysticalSpaceTyphoon = 5318639;
            public const int BookOfMoon = 14087893;
            public const int XyzUnit = 13032689;
            public const int XyzReborn = 26708437;
            public const int MirrorForce = 44095762;
            public const int TorrentialTribute = 53582587;
            public const int XyzVeil = 96457619;

            public const int PanzerDragon = 72959823;
            public const int GaiaDragonTheThunderCharger = 91949988;
            public const int CyberDragonInfinity = 10443957;
            public const int TirasKeeperOfGenesis = 31386180;
            public const int Number61Volcasaurus = 29669359;
            public const int SharkFortress = 50449881;
            public const int CyberDragonNova = 58069384;
        }

        private bool NormalSummoned = false;
        private bool InstantFusionUsed = false;
        private bool DoubleSummonUsed = false;
        private bool CyberDragonInfinitySummoned = false;
        private bool Number61VolcasaurusUsed = false;

        public Rank5Executor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // Quick spells
            this.AddExecutor(ExecutorType.Activate, CardId.BookOfMoon, this.DefaultBookOfMoon);
            this.AddExecutor(ExecutorType.Activate, CardId.MysticalSpaceTyphoon, this.DefaultMysticalSpaceTyphoon);

            // Cyber Dragon Infinity first
            this.AddExecutor(ExecutorType.SpSummon, CardId.CyberDragonNova, this.CyberDragonNovaSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.CyberDragonNova, this.CyberDragonNovaEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.CyberDragonInfinity, this.CyberDragonInfinitySummon);
            this.AddExecutor(ExecutorType.Activate, CardId.CyberDragonInfinity, this.CyberDragonInfinityEffect);

            // Level 5 monsters without side effects
            this.AddExecutor(ExecutorType.SpSummon, CardId.CyberDragon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.ZWEagleClaw);
            this.AddExecutor(ExecutorType.Summon, CardId.ChronomalyGoldenJet, this.NormalSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.ChronomalyGoldenJet, this.ChronomalyGoldenJetEffect);
            this.AddExecutor(ExecutorType.Summon, CardId.StarDrawing, this.NormalSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.WindUpSoldier, this.NormalSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.WindUpSoldier, this.WindUpSoldierEffect);

            // XYZ Monsters: Summon
            this.AddExecutor(ExecutorType.SpSummon, CardId.Number61Volcasaurus, this.Number61VolcasaurusSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.Number61Volcasaurus, this.Number61VolcasaurusEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.TirasKeeperOfGenesis);
            this.AddExecutor(ExecutorType.Activate, CardId.TirasKeeperOfGenesis, this.TirasKeeperOfGenesisEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.SharkFortress);
            this.AddExecutor(ExecutorType.Activate, CardId.SharkFortress);

            this.AddExecutor(ExecutorType.SpSummon, CardId.GaiaDragonTheThunderCharger, this.GaiaDragonTheThunderChargerSummon);


            // Level 5 monsters with side effects
            this.AddExecutor(ExecutorType.SpSummon, CardId.SolarWindJammer, this.SolarWindJammerSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.QuickdrawSynchron, this.QuickdrawSynchronSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.MistArchfiend, this.MistArchfiendSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.InstantFusion, this.InstantFusionEffect);

            // Useful spells
            this.AddExecutor(ExecutorType.Activate, CardId.DoubleSummon, this.DoubleSummonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.XyzUnit, this.XyzUnitEffect);


            this.AddExecutor(ExecutorType.Activate, CardId.XyzReborn, this.XyzRebornEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.PanzerDragon, this.PanzerDragonEffect);

            // Reposition
            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);

            // Set and activate traps
            this.AddExecutor(ExecutorType.SpellSet, this.DefaultSpellSet);

            this.AddExecutor(ExecutorType.Activate, CardId.XyzVeil, this.XyzVeilEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.TorrentialTribute, this.DefaultTorrentialTribute);
            this.AddExecutor(ExecutorType.Activate, CardId.MirrorForce, this.DefaultTrap);
        }

        public override bool OnSelectHand()
        {
            return false;
        }

        public override void OnNewTurn()
        {
            this.NormalSummoned = false;
            this.InstantFusionUsed = false;
            this.DoubleSummonUsed = false;
            this.CyberDragonInfinitySummoned = false;
            this.Number61VolcasaurusUsed = false;
        }

        public override IList<ClientCard> OnSelectXyzMaterial(IList<ClientCard> cards, int min, int max)
        {
            IList<ClientCard> result = this.Util.SelectPreferredCards(new[] {
                CardId.MistArchfiend,
                CardId.PanzerDragon,
                CardId.SolarWindJammer,
                CardId.StarDrawing
            }, cards, min, max);
            return this.Util.CheckSelectCount(result, cards, min, max);
        }

        private bool NormalSummon()
        {
            this.NormalSummoned = true;
            return true;
        }

        private bool SolarWindJammerSummon()
        {
            if (!this.NeedLV5())
            {
                return false;
            }

            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }

        private bool QuickdrawSynchronSummon()
        {
            if (!this.NeedLV5())
            {
                return false;
            }

            this.AI.SelectCard(
                CardId.QuickdrawSynchron,
                CardId.ZWEagleClaw,
                CardId.SolarWindJammer,
                CardId.CyberDragon,
                CardId.MistArchfiend,
                CardId.WindUpSoldier,
                CardId.StarDrawing,
                CardId.ChronomalyGoldenJet
                );
            return true;
        }

        private bool MistArchfiendSummon()
        {
            if (!this.NeedLV5())
            {
                return false;
            }

            this.AI.SelectOption(1);
            this.NormalSummoned = true;
            return true;
        }

        private bool InstantFusionEffect()
        {
            if (!this.NeedLV5())
            {
                return false;
            }

            this.InstantFusionUsed = true;
            return true;
        }

        private bool NeedLV5()
        {
            if (this.HaveOtherLV5OnField())
            {
                return true;
            }

            if (this.Util.GetBotAvailZonesFromExtraDeck() == 0)
            {
                return false;
            }

            int lv5Count = 0;
            foreach (ClientCard card in this.Bot.Hand)
            {
                if (card.IsCode(CardId.SolarWindJammer) && this.Bot.GetMonsterCount() == 0)
                {
                    ++lv5Count;
                }

                if (card.IsCode(CardId.InstantFusion) && !this.InstantFusionUsed)
                {
                    ++lv5Count;
                }

                if (card.IsCode(CardId.QuickdrawSynchron) && this.Bot.Hand.ContainsMonsterWithLevel(4))
                {
                    ++lv5Count;
                }

                if (card.IsCode(CardId.MistArchfiend) && !this.NormalSummoned)
                {
                    ++lv5Count;
                }

                if (card.IsCode(CardId.DoubleSummon) && this.DoubleSummonEffect())
                {
                    ++lv5Count;
                }
            }
            if (lv5Count >= 2)
            {
                return true;
            }

            return false;
        }

        private bool WindUpSoldierEffect()
        {
            return this.HaveOtherLV5OnField();
        }

        private bool ChronomalyGoldenJetEffect()
        {
            return this.Card.Level == 4;
        }

        private bool DoubleSummonEffect()
        {
            if (!this.NormalSummoned || this.DoubleSummonUsed)
            {
                return false;
            }

            if (this.Bot.HasInHand(new[]
                {
                    CardId.WindUpSoldier,
                    CardId.StarDrawing,
                    CardId.ChronomalyGoldenJet,
                    CardId.MistArchfiend
                }))
            {
                this.NormalSummoned = false;
                this.DoubleSummonUsed = true;
                return true;
            }
            return false;
        }

        private bool CyberDragonNovaSummon()
        {
            return !this.CyberDragonInfinitySummoned;
        }

        private bool CyberDragonNovaEffect()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.CyberDragonNova, 0))
            {
                return true;
            }
            else if (this.Card.Location == CardLocation.Grave)
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CyberDragonInfinitySummon()
        {
            this.CyberDragonInfinitySummoned = true;
            return true;
        }

        private bool CyberDragonInfinityEffect()
        {
            if (this.Duel.CurrentChain.Count > 0)
            {
                return this.Duel.LastChainPlayer == 1;
            }
            else
            {
                ClientCard bestmonster = null;
                foreach (ClientCard monster in this.Enemy.GetMonsters())
                {
                    if (monster.IsAttack() && (bestmonster == null || monster.Attack >= bestmonster.Attack))
                    {
                        bestmonster = monster;
                    }
                }
                if (bestmonster != null)
                {
                    this.AI.SelectCard(bestmonster);
                    return true;
                }
            }
            return false;
        }

        private bool Number61VolcasaurusSummon()
        {
            return this.Util.IsOneEnemyBetterThanValue(2000, false);
        }

        private bool Number61VolcasaurusEffect()
        {
            ClientCard target = this.Util.GetProblematicEnemyMonster(2000);
            if (target != null)
            {
                this.AI.SelectCard(CardId.CyberDragon);
                this.AI.SelectNextCard(target);
                this.Number61VolcasaurusUsed = true;
                return true;
            }
            return false;
        }

        private bool TirasKeeperOfGenesisEffect()
        {
            ClientCard target = this.Util.GetProblematicEnemyCard();
            if (target == null)
            {
                target = this.Util.GetBestEnemyCard();
            }

            if (target != null)
            {
                this.AI.SelectCard(target);
            }
            return true;
        }

        private bool GaiaDragonTheThunderChargerSummon()
        {
            if (this.Number61VolcasaurusUsed && this.Bot.HasInMonstersZone(CardId.Number61Volcasaurus))
            {
                this.AI.SelectCard(CardId.Number61Volcasaurus);
                return true;
            }
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.HasType(CardType.Xyz) && !monster.HasXyzMaterial())
                {
                    this.AI.SelectCard(monster);
                    return true;
                }
            }
            return false;
        }

        private bool XyzRebornEffect()
        {
            if (!this.UniqueFaceupSpell())
            {
                return false;
            }

            this.AI.SelectCard(
                CardId.CyberDragonInfinity,
                CardId.CyberDragonNova,
                CardId.TirasKeeperOfGenesis,
                CardId.SharkFortress,
                CardId.Number61Volcasaurus
                );
            return true;
        }

        private bool XyzUnitEffect()
        {
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.HasType(CardType.Xyz))
                {
                    this.AI.SelectCard(monster);
                    return true;
                }
            }
            return false;
        }

        private bool PanzerDragonEffect()
        {
            ClientCard target = this.Util.GetBestEnemyCard();
            if (target != null)
            {
                this.AI.SelectCard(target);
                return true;
            }
            return false;
        }

        private bool XyzVeilEffect()
        {
            if (!this.UniqueFaceupSpell())
            {
                return false;
            }

            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.HasType(CardType.Xyz))
                {
                    return true;
                }
            }
            return false;
        }

        private bool HaveOtherLV5OnField()
        {
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.HasType(CardType.Monster) &&
                    !monster.HasType(CardType.Xyz) &&
                    this.Util.GetBotAvailZonesFromExtraDeck(monster) > 0 &&
                    (monster.Level == 5
                    || monster.IsCode(CardId.StarDrawing)
                    || monster.IsCode(CardId.WindUpSoldier) && !monster.Equals(this.Card)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
