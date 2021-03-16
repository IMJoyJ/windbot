using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("ST1732", "AI_ST1732")]
    public class ST1732Executor : DefaultExecutor
    {
        public class CardId
        {
            public const int Digitron = 32295838;
            public const int Bitron = 36211150;
            public const int DualAssembloom = 7445307;
            public const int BootStagguard = 70950698;
            public const int Linkslayer = 35595518;
            public const int RAMClouder = 9190563;
            public const int ROMCloudia = 44956694;
            public const int BalancerLord = 8567955;
            public const int Backlinker = 71172240;
            public const int Kleinant = 45778242;
            public const int Draconnet = 62706865;
            public const int DotScaper = 18789533;

            public const int MindControl = 37520316;
            public const int DarkHole = 53129443;
            public const int MonsterReborn = 83764718;
            public const int MysticalSpaceTyphoon = 5318639;
            public const int CosmicCyclone = 8267140;
            public const int BookOfMoon = 14087893;
            public const int CynetBackdoor = 43839002;
            public const int MoonMirrorShield = 19508728;
            public const int CynetUniverse = 61583217;
            public const int BottomlessTrapHole = 29401950;
            public const int MirrorForce = 44095762;
            public const int TorrentialTribute = 53582587;
            public const int RecodedAlive = 70238111;
            public const int DimensionalBarrier = 83326048;
            public const int CompulsoryEvacuationDevice = 94192409;
            public const int SolemnStrike = 40605147;

            public const int DecodeTalker = 1861629;
            public const int EncodeTalker = 6622715;
            public const int TriGateWizard = 32617464;
            public const int Honeybot = 34472920;
            public const int BinarySorceress = 79016563;
            public const int LinkSpider = 98978921;

            public const int StagToken = 70950699;
        }

        bool BalancerLordUsed = false;

        public ST1732Executor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.Activate, CardId.CosmicCyclone, this.DefaultCosmicCyclone);
            this.AddExecutor(ExecutorType.Activate, CardId.MysticalSpaceTyphoon, this.DefaultMysticalSpaceTyphoon);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);
            this.AddExecutor(ExecutorType.Activate, CardId.BookOfMoon, this.DefaultBookOfMoon);

            this.AddExecutor(ExecutorType.Activate, CardId.CynetUniverse, this.CynetUniverseEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.Linkslayer);
            this.AddExecutor(ExecutorType.Activate, CardId.Linkslayer, this.LinkslayerEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.LinkSpider);
            this.AddExecutor(ExecutorType.Activate, CardId.LinkSpider);

            this.AddExecutor(ExecutorType.Activate, CardId.MindControl, this.MindControlEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Backlinker);
            this.AddExecutor(ExecutorType.Activate, CardId.Backlinker, this.BacklinkerEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.BootStagguard, this.BootStagguardEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.MonsterReborn, this.MonsterRebornEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.MoonMirrorShield, this.MoonMirrorShieldEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.CynetBackdoor, this.CynetBackdoorEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.RecodedAlive);

            this.AddExecutor(ExecutorType.Summon, CardId.BalancerLord, this.BalancerLordSummon);

            this.AddExecutor(ExecutorType.Summon, CardId.ROMCloudia, this.ROMCloudiaSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.ROMCloudia, this.ROMCloudiaEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.Draconnet, this.DraconnetSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.Draconnet, this.DraconnetEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.Kleinant);
            this.AddExecutor(ExecutorType.Activate, CardId.Kleinant, this.KleinantEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.RAMClouder);
            this.AddExecutor(ExecutorType.Activate, CardId.RAMClouder, this.RAMClouderEffect);

            this.AddExecutor(ExecutorType.SummonOrSet, CardId.DotScaper);
            this.AddExecutor(ExecutorType.Activate, CardId.DotScaper, this.DotScaperEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.BalancerLord);
            this.AddExecutor(ExecutorType.Summon, CardId.ROMCloudia);
            this.AddExecutor(ExecutorType.Summon, CardId.Draconnet);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.Backlinker);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.Digitron);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.Bitron);

            this.AddExecutor(ExecutorType.Activate, CardId.BalancerLord, this.BalancerLordEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.DecodeTalker, this.LinkSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.DecodeTalker);

            this.AddExecutor(ExecutorType.SpSummon, CardId.TriGateWizard, this.LinkSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.TriGateWizard);

            this.AddExecutor(ExecutorType.SpSummon, CardId.EncodeTalker, this.LinkSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.EncodeTalker);

            this.AddExecutor(ExecutorType.SpSummon, CardId.Honeybot, this.LinkSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BinarySorceress, this.LinkSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.BinarySorceress);

            this.AddExecutor(ExecutorType.SpellSet, CardId.CynetBackdoor, this.DefaultSpellSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.RecodedAlive, this.DefaultSpellSet);

            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnStrike, this.DefaultSpellSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.CompulsoryEvacuationDevice, this.DefaultSpellSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.DimensionalBarrier, this.DefaultSpellSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.TorrentialTribute, this.DefaultSpellSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.MirrorForce, this.DefaultSpellSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.BottomlessTrapHole, this.DefaultSpellSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.BookOfMoon, this.DefaultSpellSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.CosmicCyclone, this.DefaultSpellSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.MysticalSpaceTyphoon, this.DefaultSpellSet);

            this.AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, this.DefaultSolemnStrike);
            this.AddExecutor(ExecutorType.Activate, CardId.CompulsoryEvacuationDevice, this.DefaultCompulsoryEvacuationDevice);
            this.AddExecutor(ExecutorType.Activate, CardId.DimensionalBarrier, this.DefaultDimensionalBarrier);
            this.AddExecutor(ExecutorType.Activate, CardId.TorrentialTribute, this.DefaultTorrentialTribute);
            this.AddExecutor(ExecutorType.Activate, CardId.MirrorForce, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.BottomlessTrapHole, this.DefaultUniqueTrap);

            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
        }

        public override bool OnSelectHand()
        {
            // go second
            return false;
        }

        public override void OnNewTurn()
        {
            // reset
            this.BalancerLordUsed = false;
        }

        public override int OnSelectOption(IList<int> options)
        {
            // put Moon Mirror Shield to the bottom of deck
            return options.Count == 2 ? 1 : 0;
        }

        public override bool OnSelectYesNo(int desc)
        {
            if (desc == 210) // Continue selecting? (Link Summoning)
            {
                return false;
            }

            if (desc == 31) // Direct Attack?
            {
                return true;
            }

            return base.OnSelectYesNo(desc);
        }

        private bool LinkslayerEffect()
        {
            IList<ClientCard> targets = this.Enemy.GetSpells();
            if (targets.Count > 0)
            {
                this.AI.SelectCard(
                    CardId.DualAssembloom,
                    CardId.Bitron,
                    CardId.Digitron,
                    CardId.RecodedAlive
                    );
                this.AI.SelectNextCard(targets);
                return true;
            }
            return false;
        }

        private bool MindControlEffect()
        {
            ClientCard target = this.Util.GetBestEnemyMonster();
            if (target != null)
            {
                this.AI.SelectCard(target);
                return true;
            }
            return false;
        }

        private bool BacklinkerEffect()
        {
            return (this.Bot.MonsterZone[5] == null) && (this.Bot.MonsterZone[6] == null);
        }

        private bool BootStagguardEffect()
        {
            if (this.Card.Location != CardLocation.Hand)
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
            }

            return true;
        }

        private bool MonsterRebornEffect()
        {
            IList<int> targets = new[] {
                    CardId.DecodeTalker,
                    CardId.EncodeTalker,
                    CardId.TriGateWizard,
                    CardId.BinarySorceress,
                    CardId.Honeybot,
                    CardId.DualAssembloom,
                    CardId.BootStagguard,
                    CardId.BalancerLord,
                    CardId.ROMCloudia,
                    CardId.Linkslayer,
                    CardId.RAMClouder,
                    CardId.Backlinker,
                    CardId.Kleinant
                };
            if (!this.Bot.HasInGraveyard(targets))
            {
                return false;
            }
            this.AI.SelectCard(targets);
            return true;
        }

        private bool MoonMirrorShieldEffect()
        {
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                this.AI.SelectCard(monster);
                return true;
            }
            return false;
        }

        private bool CynetUniverseEffect()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                return this.DefaultField();
            }

            foreach (ClientCard card in this.Enemy.Graveyard)
            {
                if (card.IsMonster())
                {
                    this.AI.SelectCard(card);
                    return true;
                }
            }
            return false;
        }

        private bool CynetBackdoorEffect()
        {
            if (!(this.Duel.Player == 0 && this.Duel.Phase == DuelPhase.Main2) &&
                !(this.Duel.Player == 1 && (this.Duel.Phase == DuelPhase.BattleStart || this.Duel.Phase == DuelPhase.End)))
            {
                return false;
            }
            if (!this.UniqueFaceupSpell())
            {
                return false;
            }

            bool selected = false;
            foreach (ClientCard monster in this.Bot.GetMonstersInExtraZone())
            {
                if (monster.Attack > 1000)
                {
                    this.AI.SelectCard(monster);
                    selected = true;
                    break;
                }
            }
            if (!selected)
            {
                List<ClientCard> monsters = this.Bot.GetMonsters();
                foreach (ClientCard monster in monsters)
                {
                    if (monster.IsCode(CardId.BalancerLord))
                    {
                        this.AI.SelectCard(monster);
                        selected = true;
                        break;
                    }
                }
                if (!selected)
                {
                    foreach (ClientCard monster in monsters)
                    {
                        if (monster.Attack >= 1700)
                        {
                            this.AI.SelectCard(monster);
                            selected = true;
                            break;
                        }
                    }
                }
            }
            if (selected)
            {
                this.AI.SelectNextCard(
                    CardId.ROMCloudia,
                    CardId.BalancerLord,
                    CardId.Kleinant,
                    CardId.Draconnet,
                    CardId.Backlinker
                    );
                return true;
            }
            return false;
        }

        private bool BalancerLordSummon()
        {
            return !this.BalancerLordUsed;
        }

        private bool BalancerLordEffect()
        {
            if (this.Card.Location == CardLocation.Removed)
            {
                return true;
            }

            bool hastarget = this.Bot.HasInHand(new[] {
                    CardId.Draconnet,
                    CardId.Kleinant,
                    CardId.BalancerLord,
                    CardId.ROMCloudia,
                    CardId.RAMClouder,
                    CardId.DotScaper
                });
            if (hastarget && !this.BalancerLordUsed)
            {
                this.BalancerLordUsed = true;
                return true;
            }
            return false;
        }

        private bool ROMCloudiaSummon()
        {
            return this.Bot.HasInGraveyard(new[] {
                    CardId.BootStagguard,
                    CardId.BalancerLord,
                    CardId.Kleinant,
                    CardId.Linkslayer,
                    CardId.Draconnet,
                    CardId.RAMClouder
                });
        }

        private bool ROMCloudiaEffect()
        {
            if (this.Card.Location == CardLocation.MonsterZone)
            {
                this.AI.SelectCard(
                    CardId.BootStagguard,
                    CardId.BalancerLord,
                    CardId.Kleinant,
                    CardId.Linkslayer,
                    CardId.Draconnet,
                    CardId.RAMClouder
                    );
                return true;
            }
            else
            {
                this.AI.SelectCard(
                    CardId.BalancerLord,
                    CardId.Kleinant,
                    CardId.RAMClouder,
                    CardId.DotScaper
                    );
                return true;
            }
        }

        private bool DraconnetSummon()
        {
            return this.Bot.GetRemainingCount(CardId.Digitron, 1) > 0
                || this.Bot.GetRemainingCount(CardId.Bitron, 1) > 0;
        }

        private bool DraconnetEffect()
        {
            this.AI.SelectCard(CardId.Bitron);
            return true;
        }

        private bool KleinantEffect()
        {
            IList<int> targets = new[] {
                CardId.DualAssembloom,
                CardId.Bitron,
                CardId.Digitron,
                CardId.DotScaper
            };
            foreach (ClientCard monster in this.Bot.Hand)
            {
                if (monster.IsCode(targets))
                {
                    this.AI.SelectCard(targets);
                    return true;
                }
            }
            IList<int> targets2 = new[] {
                CardId.StagToken,
                CardId.Bitron,
                CardId.Digitron,
                CardId.DotScaper
            };
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.IsCode(targets2))
                {
                    this.AI.SelectCard(targets2);
                    return true;
                }
            }
            return false;
        }

        private bool RAMClouderEffect()
        {
            this.AI.SelectCard(
                CardId.StagToken,
                CardId.Bitron,
                CardId.Digitron,
                CardId.DotScaper,
                CardId.Draconnet,
                CardId.Backlinker,
                CardId.RAMClouder
                );
            this.AI.SelectNextCard(
                CardId.DecodeTalker,
                CardId.EncodeTalker,
                CardId.TriGateWizard,
                CardId.BinarySorceress,
                CardId.Honeybot,
                CardId.DualAssembloom,
                CardId.BootStagguard,
                CardId.BalancerLord,
                CardId.ROMCloudia,
                CardId.Linkslayer,
                CardId.RAMClouder
                );
            return true;
        }

        private bool DotScaperEffect()
        {
            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }

        private bool LinkSummon()
        {
            return (this.Util.IsTurn1OrMain2() || this.Util.IsOneEnemyBetter())
                && this.Util.GetBestAttack(this.Bot) < this.Card.Attack;
        }
    }
}