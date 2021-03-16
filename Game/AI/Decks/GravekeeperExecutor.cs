using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    // NOT FINISHED YET
    [Deck("Gravekeeper", "AI_Gravekeeper", "NotFinished")]
    public class GravekeeperExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int GravekeepersOracle = 25524823;
            public const int MaleficStardustDragon = 36521459;
            public const int GravekeepersVisionary = 3825890;
            public const int GravekeepersChief = 62473983;
            public const int ThunderKingRaiOh = 71564252;
            public const int GravekeepersCommandant = 17393207;
            public const int GravekeepersAssailant = 25262697;
            public const int GravekeepersDescendant = 30213599;
            public const int GravekeepersSpy = 24317029;
            public const int GravekeepersRecruiter = 93023479;
            public const int AllureOfDarkness = 1475311;
            public const int DarkHole = 53129443;
            public const int RoyalTribute = 72405967;
            public const int GravekeepersStele = 99523325;
            public const int MysticalSpaceTyphoon = 5318639;
            public const int BookofMoon = 14087893;
            public const int HiddenTemplesOfNecrovalley = 70000776;
            public const int Necrovalley = 47355498;
            public const int BottomlessTrapHole = 29401950;
            public const int RiteOfSpirit = 30450531;
            public const int TorrentialTribute = 53582587;
            public const int DimensionalPrison = 70342110;
            public const int SolemnWarning = 84749824;
            public const int ImperialTombsOfNecrovalley = 90434657;
        }

        public GravekeeperExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.SpellSet, this.DefaultSpellSet);

            this.AddExecutor(ExecutorType.Activate, CardId.AllureOfDarkness);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);
            this.AddExecutor(ExecutorType.Activate, CardId.RoyalTribute);
            this.AddExecutor(ExecutorType.Activate, CardId.GravekeepersStele);
            this.AddExecutor(ExecutorType.Activate, CardId.MysticalSpaceTyphoon, this.DefaultMysticalSpaceTyphoon);
            this.AddExecutor(ExecutorType.Activate, CardId.BookofMoon, this.DefaultBookOfMoon);
            this.AddExecutor(ExecutorType.Activate, CardId.HiddenTemplesOfNecrovalley, this.HiddenTemplesOfNecrovalleyEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Necrovalley, this.NecrovalleyActivate);

            this.AddExecutor(ExecutorType.Activate, CardId.BottomlessTrapHole, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnWarning, this.DefaultSolemnWarning);
            this.AddExecutor(ExecutorType.Activate, CardId.DimensionalPrison, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.RiteOfSpirit, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.ImperialTombsOfNecrovalley, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.TorrentialTribute, this.DefaultTorrentialTribute);

            this.AddExecutor(ExecutorType.Summon, CardId.GravekeepersOracle);
            this.AddExecutor(ExecutorType.SpSummon, CardId.MaleficStardustDragon, this.MaleficStardustDragonSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.GravekeepersVisionary);
            this.AddExecutor(ExecutorType.Summon, CardId.GravekeepersChief);
            this.AddExecutor(ExecutorType.Summon, CardId.ThunderKingRaiOh);
            this.AddExecutor(ExecutorType.Summon, CardId.GravekeepersCommandant, this.GravekeepersCommandantSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.GravekeepersAssailant);
            this.AddExecutor(ExecutorType.Summon, CardId.GravekeepersDescendant);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.GravekeepersSpy);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.GravekeepersRecruiter);

            this.AddExecutor(ExecutorType.Activate, CardId.GravekeepersOracle);
            this.AddExecutor(ExecutorType.Activate, CardId.GravekeepersVisionary);
            this.AddExecutor(ExecutorType.Activate, CardId.GravekeepersChief);
            this.AddExecutor(ExecutorType.Activate, CardId.GravekeepersCommandant, this.GravekeepersCommandantEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.GravekeepersAssailant, this.GravekeepersAssailantEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.GravekeepersDescendant, this.GravekeepersDescendantEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.GravekeepersSpy, this.SearchForDescendant);
            this.AddExecutor(ExecutorType.Activate, CardId.GravekeepersRecruiter, this.SearchForDescendant);

            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
        }

        private bool HiddenTemplesOfNecrovalleyEffect()
        {
            if (this.Card.Location == CardLocation.Hand && this.Bot.HasInSpellZone((int)this.Card.Id))
            {
                return false;
            }

            return true;
        }

        private bool NecrovalleyActivate()
        {
            if (this.Bot.SpellZone[5] != null)
            {
                return false;
            }

            return true;
        }

        private bool MaleficStardustDragonSummon()
        {
            if (this.Bot.SpellZone[5] != null)
            {
                return true;
            }

            return false;
        }

        private bool GravekeepersCommandantEffect()
        {
            if (!this.Bot.HasInHand(CardId.Necrovalley) && !this.Bot.HasInSpellZone(CardId.Necrovalley))
            {
                return true;
            }

            return false;
        }

        private bool GravekeepersCommandantSummon()
        {
            return !this.GravekeepersCommandantEffect();
        }

        private bool GravekeepersAssailantEffect()
        {
            if (!this.Card.IsAttack())
            {
                return false;
            }

            foreach (ClientCard card in this.Enemy.GetMonsters())
            {
                if (card.IsDefense() && card.Defense > 1500 && card.Attack < 1500 || card.Attack > 1500 && card.Defense < 1500)
                {
                    return true;
                }
            }
            return false;
        }

        private bool GravekeepersDescendantEffect()
        {
            int bestatk = this.Bot.GetMonsters().GetHighestAttackMonster().Attack;
            if (this.Util.IsOneEnemyBetterThanValue(bestatk, true))
            {
                this.AI.SelectCard(this.Enemy.GetMonsters().GetHighestAttackMonster());
                return true;
            }
            return false;
        }

        private bool SearchForDescendant()
        {
            this.AI.SelectCard(CardId.GravekeepersDescendant);
            return true;
        }
    }
}