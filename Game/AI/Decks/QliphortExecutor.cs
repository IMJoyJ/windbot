using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("Qliphort", "AI_Qliphort")]
    public class QliphortExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int Scout = 65518099;
            public const int Stealth = 13073850;
            public const int Shell = 90885155;
            public const int Helix = 37991342;
            public const int Carrier = 91907707;

            public const int DarkHole = 53129443;
            public const int CardOfDemise = 59750328;
            public const int SummonersArt = 79816536;
            public const int PotOfDuality = 98645731;
            public const int Saqlifice = 17639150;

            public const int MirrorForce = 44095762;
            public const int TorrentialTribute = 53582587;
            public const int DimensionalBarrier = 83326048;
            public const int CompulsoryEvacuationDevice = 94192409;
            public const int VanitysEmptiness = 5851097;
            public const int SkillDrain = 82732705;
            public const int SolemnStrike = 40605147;
            public const int TheHugeRevolutionIsOver = 99188141;
        }

        bool CardOfDemiseUsed = false;
        readonly IList<int> LowScaleCards = new[]
        {
            CardId.Stealth,
            CardId.Carrier
        };
        readonly IList<int> HighScaleCards = new[]
        {
            CardId.Scout,
            CardId.Shell,
            CardId.Helix
        };

        public QliphortExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {

            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);
            this.AddExecutor(ExecutorType.Activate, CardId.SummonersArt);

            this.AddExecutor(ExecutorType.Activate, CardId.Scout, this.ScoutActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.Scout, this.ScoutEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.Stealth, this.ScaleActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.Shell, this.ScaleActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.Helix, this.ScaleActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.Carrier, this.ScaleActivate);

            this.AddExecutor(ExecutorType.Summon, this.NormalSummon);
            this.AddExecutor(ExecutorType.SpSummon);

            this.AddExecutor(ExecutorType.Activate, CardId.Saqlifice, this.SaqlificeEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.Stealth, this.StealthEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Helix, this.HelixEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Carrier, this.CarrierEffect);

            this.AddExecutor(ExecutorType.SpellSet, CardId.SkillDrain, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.VanitysEmptiness, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.DimensionalBarrier, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.TorrentialTribute, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnStrike, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.MirrorForce, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.CompulsoryEvacuationDevice, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.TheHugeRevolutionIsOver, this.TrapSetUnique);

            this.AddExecutor(ExecutorType.SpellSet, CardId.Saqlifice, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SkillDrain, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.VanitysEmptiness, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.DimensionalBarrier, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.TorrentialTribute, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnStrike, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.MirrorForce, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.CompulsoryEvacuationDevice, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.TheHugeRevolutionIsOver, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.DarkHole, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SummonersArt, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.PotOfDuality, this.TrapSetWhenZoneFree);

            this.AddExecutor(ExecutorType.Activate, CardId.PotOfDuality, this.PotOfDualityEffect);
            this.AddExecutor(ExecutorType.SpellSet, CardId.CardOfDemise);
            this.AddExecutor(ExecutorType.Activate, CardId.CardOfDemise, this.CardOfDemiseEffect);

            this.AddExecutor(ExecutorType.SpellSet, CardId.Saqlifice, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SkillDrain, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.VanitysEmptiness, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.DimensionalBarrier, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.TorrentialTribute, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnStrike, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.MirrorForce, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.CompulsoryEvacuationDevice, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.TheHugeRevolutionIsOver, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.DarkHole, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SummonersArt, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.PotOfDuality, this.CardOfDemiseAcivated);

            this.AddExecutor(ExecutorType.Activate, CardId.TheHugeRevolutionIsOver, this.DefaultTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, this.DefaultSolemnStrike);
            this.AddExecutor(ExecutorType.Activate, CardId.SkillDrain, this.SkillDrainEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.VanitysEmptiness, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.CompulsoryEvacuationDevice, this.DefaultCompulsoryEvacuationDevice);
            this.AddExecutor(ExecutorType.Activate, CardId.DimensionalBarrier, this.DefaultDimensionalBarrier);
            this.AddExecutor(ExecutorType.Activate, CardId.MirrorForce, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.TorrentialTribute, this.DefaultTorrentialTribute);

            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
        }

        public override bool OnSelectHand()
        {
            return true;
        }

        public override void OnNewTurn()
        {
            this.CardOfDemiseUsed = false;
        }

        public override IList<ClientCard> OnSelectPendulumSummon(IList<ClientCard> cards, int max)
        {
            Logger.DebugWriteLine("OnSelectPendulumSummon");
            // select the last cards

            IList<ClientCard> selected = new List<ClientCard>();
            for (int i = 1; i <= max; ++i)
            {
                ClientCard card = cards[cards.Count - i];
                if (!card.IsCode(CardId.Scout) || (card.Location == CardLocation.Extra && !this.Duel.IsNewRule))
                {
                    selected.Add(card);
                }
            }
            if (selected.Count == 0)
            {
                selected.Add(cards[cards.Count - 1]);
            }

            return selected;
        }

        private bool NormalSummon()
        {
            if (this.Card.IsCode(CardId.Scout))
            {
                return false;
            }

            if (this.Card.Level < 8)
            {
                this.AI.SelectOption(1);
            }

            return true;
        }

        private bool SkillDrainEffect()
        {
            return (this.Bot.LifePoints > 1000) && this.DefaultUniqueTrap();
        }

        private bool PotOfDualityEffect()
        {
            this.AI.SelectCard(
                CardId.Scout,
                CardId.SkillDrain,
                CardId.VanitysEmptiness,
                CardId.DimensionalBarrier,
                CardId.Stealth,
                CardId.Shell,
                CardId.Helix,
                CardId.Carrier,
                CardId.SolemnStrike,
                CardId.CardOfDemise
                );
            return !this.ShouldPendulum();
        }

        private bool CardOfDemiseEffect()
        {
            if (this.Util.IsTurn1OrMain2() && !this.ShouldPendulum())
            {
                this.CardOfDemiseUsed = true;
                return true;
            }
            return false;
        }

        private bool TrapSetUnique()
        {
            foreach (ClientCard card in this.Bot.GetSpells())
            {
                if (card.IsCode(this.Card.Id))
                {
                    return false;
                }
            }
            return this.TrapSetWhenZoneFree();
        }

        private bool TrapSetWhenZoneFree()
        {
            return this.Bot.GetSpellCountWithoutField() < 4;
        }

        private bool CardOfDemiseAcivated()
        {
            return this.CardOfDemiseUsed;
        }

        private bool SaqlificeEffect()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                ClientCard l = this.Util.GetPZone(0, 0);
                ClientCard r = this.Util.GetPZone(0, 1);
                if (l == null && r == null)
                {
                    this.AI.SelectCard(CardId.Scout);
                }
            }
            return true;
        }

        private bool ScoutActivate()
        {
            if (this.Card.Location != CardLocation.Hand)
            {
                return false;
            }

            ClientCard l = this.Util.GetPZone(0, 0);
            ClientCard r = this.Util.GetPZone(0, 1);
            if (l == null && r == null)
            {
                return true;
            }

            if (l == null && r.RScale != this.Card.LScale)
            {
                return true;
            }

            if (r == null && l.LScale != this.Card.RScale)
            {
                return true;
            }

            return false;
        }

        private bool ScaleActivate()
        {
            if (!this.Card.HasType(CardType.Pendulum) || this.Card.Location != CardLocation.Hand)
            {
                return false;
            }

            int count = 0;
            foreach (ClientCard card in this.Bot.Hand.GetMonsters())
            {
                if (!this.Card.Equals(card))
                {
                    count++;
                }
            }
            foreach (ClientCard card in this.Bot.ExtraDeck.GetFaceupPendulumMonsters())
            {
                count++;
            }
            ClientCard l = this.Util.GetPZone(0, 0);
            ClientCard r = this.Util.GetPZone(0, 1);
            if (l == null && r == null)
            {
                if (this.CardOfDemiseUsed)
                {
                    return true;
                }

                bool pair = false;
                foreach (ClientCard card in this.Bot.Hand.GetMonsters())
                {
                    if (card.RScale != this.Card.LScale)
                    {
                        pair = true;
                        count--;
                        break;
                    }
                }
                return pair && count>1;
            }
            if (l == null && r.RScale != this.Card.LScale)
            {
                return count > 1 || this.CardOfDemiseUsed;
            }

            if (r == null && l.LScale != this.Card.RScale)
            {
                return count > 1 || this.CardOfDemiseUsed;
            }

            return false;
        }

        private bool ScoutEffect()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                return false;
            }

            int count = 0;
            int handcount = 0;
            int fieldcount = 0;
            foreach (ClientCard card in this.Bot.Hand.GetMonsters())
            {
                count++;
                handcount++;
            }
            foreach (ClientCard card in this.Bot.MonsterZone.GetMonsters())
            {
                fieldcount++;
            }
            foreach (ClientCard card in this.Bot.ExtraDeck.GetFaceupPendulumMonsters())
            {
                count++;
            }
            if (count>0 && !this.Bot.HasInHand(this.LowScaleCards))
            {
                this.AI.SelectCard(this.LowScaleCards);
            }
            else if (handcount>0 || fieldcount>0)
            {
                this.AI.SelectCard(CardId.Saqlifice, CardId.Shell, CardId.Helix);
            }
            else
            {
                this.AI.SelectCard(this.HighScaleCards);
            }
            return this.Bot.LifePoints > 800;
        }

        private bool StealthEffect()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                return false;
            }

            ClientCard target = this.Util.GetBestEnemyCard();
            if (target != null)
            {
                this.AI.SelectCard(target);
                return true;
            }
            return false;
        }

        private bool CarrierEffect()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                return false;
            }

            ClientCard target = this.Util.GetBestEnemyMonster();
            if (target != null)
            {
                this.AI.SelectCard(target);
                return true;
            }
            return false;
        }

        private bool HelixEffect()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                return false;
            }

            ClientCard target = this.Util.GetBestEnemySpell();
            if (target != null)
            {
                this.AI.SelectCard(target);
                return true;
            }
            return false;
        }

        private bool ShouldPendulum()
        {
            ClientCard l = this.Util.GetPZone(0, 0);
            ClientCard r = this.Util.GetPZone(0, 1);
            if (l != null && r != null && l.LScale != r.RScale)
            {
                int count = 0;
                foreach (ClientCard card in this.Bot.Hand.GetMonsters())
                {
                    count++;
                }
                foreach (ClientCard card in this.Bot.ExtraDeck.GetFaceupPendulumMonsters())
                {
                    count++;
                }
                return count > 1;
            }
            return false;
        }

    }
}