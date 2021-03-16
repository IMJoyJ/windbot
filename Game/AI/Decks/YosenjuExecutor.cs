using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("Yosenju", "AI_Yosenju")]
    public class YosenjuExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int YosenjuKama1 = 65247798;
            public const int YosenjuKama2 = 92246806;
            public const int YosenjuKama3 = 28630501;
            public const int YosenjuTsujik = 25244515;

            public const int HarpiesFeatherDuster = 18144507;
            public const int DarkHole = 53129443;
            public const int CardOfDemise = 59750328;
            public const int PotOfDuality = 98645731;
            public const int CosmicCyclone = 8267140;
            public const int QuakingMirrorForce = 40838625;
            public const int DrowningMirrorForce = 47475363;
            public const int StarlightRoad = 58120309;
            public const int VanitysEmptiness = 5851097;
            public const int MacroCosmos = 30241314;
            public const int SolemnStrike = 40605147;
            public const int SolemnWarning = 84749824;
            public const int SolemnJudgment = 41420027;
            public const int MagicDrain = 59344077;

            public const int StardustDragon = 44508094;
            public const int NumberS39UtopiatheLightning = 56832966;
            public const int NumberS39UtopiaOne = 86532744;
            public const int DarkRebellionXyzDragon = 16195942;
            public const int Number39Utopia = 84013237;
            public const int Number103Ragnazero = 94380860;
            public const int BrotherhoodOfTheFireFistTigerKing = 96381979;
            public const int Number106GiantHand = 63746411;
            public const int CastelTheSkyblasterMusketeer = 82633039;
            public const int DiamondDireWolf = 95169481;
            public const int LightningChidori = 22653490;
            public const int EvilswarmExcitonKnight = 46772449;
            public const int AbyssDweller = 21044178;
            public const int GagagaCowboy = 12014404;
        }

        bool CardOfDemiseUsed = false;

        public YosenjuExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // do the end phase effect of Card Of Demise before Yosenjus return to hand
            this.AddExecutor(ExecutorType.Activate, CardId.CardOfDemise, this.CardOfDemiseEPEffect);

            // burn if enemy's LP is below 800
            this.AddExecutor(ExecutorType.SpSummon, CardId.GagagaCowboy, this.GagagaCowboySummon);
            this.AddExecutor(ExecutorType.Activate, CardId.GagagaCowboy);

            this.AddExecutor(ExecutorType.Activate, CardId.HarpiesFeatherDuster, this.DefaultHarpiesFeatherDusterFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.CosmicCyclone, this.DefaultCosmicCyclone);
            this.AddExecutor(ExecutorType.Activate, CardId.HarpiesFeatherDuster);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);

            this.AddExecutor(ExecutorType.Activate, CardId.PotOfDuality, this.PotOfDualityEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.YosenjuKama1, this.HaveAnotherYosenjuWithSameNameInHand);
            this.AddExecutor(ExecutorType.Summon, CardId.YosenjuKama2, this.HaveAnotherYosenjuWithSameNameInHand);
            this.AddExecutor(ExecutorType.Summon, CardId.YosenjuKama3, this.HaveAnotherYosenjuWithSameNameInHand);
            this.AddExecutor(ExecutorType.Summon, CardId.YosenjuKama1);
            this.AddExecutor(ExecutorType.Summon, CardId.YosenjuKama2);
            this.AddExecutor(ExecutorType.Summon, CardId.YosenjuKama3);
            this.AddExecutor(ExecutorType.Summon, CardId.YosenjuTsujik);

            this.AddExecutor(ExecutorType.Activate, CardId.YosenjuKama1, this.YosenjuEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.YosenjuKama2, this.YosenjuEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.YosenjuKama3, this.YosenjuEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.YosenjuTsujik, this.YosenjuEffect);

            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnJudgment, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnStrike, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnWarning, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.MacroCosmos, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.VanitysEmptiness, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.MagicDrain, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.DrowningMirrorForce, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.QuakingMirrorForce, this.TrapSetUnique);
            this.AddExecutor(ExecutorType.SpellSet, CardId.StarlightRoad, this.TrapSetUnique);

            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnJudgment, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnStrike, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnWarning, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.MacroCosmos, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.VanitysEmptiness, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.MagicDrain, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.DrowningMirrorForce, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.QuakingMirrorForce, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.StarlightRoad, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.HarpiesFeatherDuster, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.DarkHole, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.PotOfDuality, this.TrapSetWhenZoneFree);
            this.AddExecutor(ExecutorType.SpellSet, CardId.CosmicCyclone, this.TrapSetWhenZoneFree);

            this.AddExecutor(ExecutorType.SpellSet, CardId.CardOfDemise);
            this.AddExecutor(ExecutorType.Activate, CardId.CardOfDemise, this.CardOfDemiseEffect);

            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnJudgment, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnStrike, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnWarning, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.MacroCosmos, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.VanitysEmptiness, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.MagicDrain, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.DrowningMirrorForce, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.QuakingMirrorForce, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.StarlightRoad, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.HarpiesFeatherDuster, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.DarkHole, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.PotOfDuality, this.CardOfDemiseAcivated);
            this.AddExecutor(ExecutorType.SpellSet, CardId.CosmicCyclone, this.CardOfDemiseAcivated);

            this.AddExecutor(ExecutorType.SpSummon, CardId.EvilswarmExcitonKnight, this.DefaultEvilswarmExcitonKnightSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.EvilswarmExcitonKnight, this.DefaultEvilswarmExcitonKnightEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.DarkRebellionXyzDragon, this.DarkRebellionXyzDragonSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkRebellionXyzDragon, this.DarkRebellionXyzDragonEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.Number39Utopia, this.DefaultNumberS39UtopiaTheLightningSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.NumberS39UtopiaOne);
            this.AddExecutor(ExecutorType.SpSummon, CardId.NumberS39UtopiatheLightning);
            this.AddExecutor(ExecutorType.Activate, CardId.NumberS39UtopiatheLightning, this.DefaultNumberS39UtopiaTheLightningEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.StardustDragon, this.DefaultStardustDragonEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.StarlightRoad, this.DefaultTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.MagicDrain);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnWarning, this.DefaultSolemnWarning);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, this.DefaultSolemnStrike);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnJudgment, this.DefaultSolemnJudgment);
            this.AddExecutor(ExecutorType.Activate, CardId.MacroCosmos, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.VanitysEmptiness, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.DrowningMirrorForce, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.QuakingMirrorForce, this.DefaultUniqueTrap);

            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
        }

        public override bool OnSelectHand()
        {
            // go first
            return true;
        }

        public override void OnNewTurn()
        {
            this.CardOfDemiseUsed = false;
        }

        public override bool OnSelectYesNo(int desc)
        {
            // Yosenju Kama 2 shouldn't attack directly at most times
            if (this.Card == null)
            {
                return true;
            }
            // Logger.DebugWriteLine(Card.Name);
            if (this.Card.IsCode(CardId.YosenjuKama2))
            {
                return this.Card.ShouldDirectAttack;
            }
            else
            {
                return true;
            }
        }

        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (!defender.IsMonsterHasPreventActivationEffectInBattle())
            {
                if (attacker.Attribute == (int)CardAttribute.Wind && this.Bot.HasInHand(CardId.YosenjuTsujik))
                {
                    attacker.RealPower = attacker.RealPower + 1000;
                }
            }
            return base.OnPreBattleBetween(attacker, defender);
        }

        public override IList<ClientCard> OnSelectXyzMaterial(IList<ClientCard> cards, int min, int max)
        {
            IList<ClientCard> result = this.Util.SelectPreferredCards(CardId.YosenjuTsujik, cards, min, max);
            return this.Util.CheckSelectCount(result, cards, min, max);
        }

        private bool PotOfDualityEffect()
        {
            if (this.CardOfDemiseUsed)
            {
                this.AI.SelectCard(
                    CardId.StarlightRoad,
                    CardId.MagicDrain,
                    CardId.SolemnJudgment,
                    CardId.VanitysEmptiness,
                    CardId.HarpiesFeatherDuster,
                    CardId.DrowningMirrorForce,
                    CardId.QuakingMirrorForce,
                    CardId.SolemnStrike,
                    CardId.SolemnWarning,
                    CardId.MacroCosmos,
                    CardId.CardOfDemise
                    );
            }
            else
            {
                this.AI.SelectCard(
                    CardId.YosenjuKama3,
                    CardId.YosenjuKama1,
                    CardId.YosenjuKama2,
                    CardId.StarlightRoad,
                    CardId.MagicDrain,
                    CardId.VanitysEmptiness,
                    CardId.HarpiesFeatherDuster,
                    CardId.DrowningMirrorForce,
                    CardId.QuakingMirrorForce,
                    CardId.SolemnStrike,
                    CardId.SolemnJudgment,
                    CardId.SolemnWarning,
                    CardId.MacroCosmos,
                    CardId.CardOfDemise
                    );
            }
            return true;
        }

        private bool CardOfDemiseEffect()
        {
            if (this.Util.IsTurn1OrMain2())
            {
                this.CardOfDemiseUsed = true;
                return true;
            }
            return false;
        }

        private bool HaveAnotherYosenjuWithSameNameInHand()
        {
            foreach (ClientCard card in this.Bot.Hand.GetMonsters())
            {
                if (!card.Equals(this.Card) && card.IsCode(this.Card.Id))
                {
                    return true;
                }
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

        private bool YosenjuEffect()
        {
            // Don't activate the return to hand effect first
            if (this.Duel.Phase == DuelPhase.End)
            {
                return false;
            }

            this.AI.SelectCard(CardId.YosenjuKama1, CardId.YosenjuKama2, CardId.YosenjuKama3);
            return true;
        }

        private bool CardOfDemiseEPEffect()
        {
            // do the end phase effect of Card Of Demise before Yosenjus return to hand
            return this.Duel.Phase == DuelPhase.End;
        }

        private bool GagagaCowboySummon()
        {
            if (this.Enemy.LifePoints <= 800 || (this.Bot.GetMonsterCount()>=4 && this.Enemy.LifePoints <= 1600))
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;
        }

        private bool DarkRebellionXyzDragonSummon()
        {
            int selfBestAttack = this.Util.GetBestAttack(this.Bot);
            int oppoBestAttack = this.Util.GetBestAttack(this.Enemy);
            return selfBestAttack <= oppoBestAttack;
        }

        private bool DarkRebellionXyzDragonEffect()
        {
            int oppoBestAttack = this.Util.GetBestAttack(this.Enemy);
            ClientCard target = this.Util.GetOneEnemyBetterThanValue(oppoBestAttack, true);
            if (target != null)
            {
                this.AI.SelectCard(0);
                this.AI.SelectNextCard(target);
            }
            return true;
        }
    }
}