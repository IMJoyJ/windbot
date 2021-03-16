using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
    [Deck("Frog", "AI_Frog", "Easy")]
    public class FrogExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int CryomancerOfTheIceBarrier = 23950192;
            public const int DewdarkOfTheIceBarrier = 90311614;
            public const int SubmarineFrog = 63948258;
            public const int SwapFrog = 9126351;
            public const int FlipFlopFrog = 81278754;
            public const int Unifrog = 56052205;
            public const int Ronintoadin = 1357146;
            public const int DupeFrog = 46239604;
            public const int Tradetoad = 23408872;
            public const int TreebornFrog = 12538374;
            public const int DarkHole = 53129443;
            public const int Raigeki = 12580477;
            public const int Terraforming = 73628505;
            public const int PotOfDuality = 98645731;
            public const int Solidarity = 86780027;
            public const int Wetlands = 2084239;
            public const int FroggyForcefield = 34351849;
            public const int GravityBind = 85742772;
            public const int TheHugeRevolutionIsOver = 99188141;
        }

        public FrogExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.SpellSet, this.DefaultSpellSet);

            this.AddExecutor(ExecutorType.Activate, CardId.Solidarity, this.Solidarity);
            this.AddExecutor(ExecutorType.Activate, CardId.Terraforming, this.Terraforming);
            this.AddExecutor(ExecutorType.Activate, CardId.Wetlands, this.DefaultField);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);
            this.AddExecutor(ExecutorType.Activate, CardId.Raigeki, this.DefaultRaigeki);
            this.AddExecutor(ExecutorType.Activate, CardId.PotOfDuality, this.PotOfDuality);

            this.AddExecutor(ExecutorType.SpSummon, CardId.SwapFrog, this.SwapFrogSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.SwapFrog, this.SwapFrogActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.DupeFrog, this.DupeFrog);
            this.AddExecutor(ExecutorType.Activate, CardId.FlipFlopFrog, this.FlipFlopFrog);
            this.AddExecutor(ExecutorType.Activate, CardId.Ronintoadin, this.Ronintoadin);
            this.AddExecutor(ExecutorType.Activate, CardId.TreebornFrog, this.TreebornFrog);
            this.AddExecutor(ExecutorType.Activate, CardId.Unifrog);

            this.AddExecutor(ExecutorType.Summon, CardId.CryomancerOfTheIceBarrier, this.SummonFrog);
            this.AddExecutor(ExecutorType.Summon, CardId.DewdarkOfTheIceBarrier, this.SummonFrog);
            this.AddExecutor(ExecutorType.Summon, CardId.SubmarineFrog, this.SummonFrog);
            this.AddExecutor(ExecutorType.Summon, CardId.SwapFrog, this.SummonFrog);
            this.AddExecutor(ExecutorType.Summon, CardId.Unifrog, this.SummonFrog);
            this.AddExecutor(ExecutorType.Summon, CardId.Ronintoadin, this.SummonFrog);
            this.AddExecutor(ExecutorType.Summon, CardId.DupeFrog, this.SummonFrog);
            this.AddExecutor(ExecutorType.Summon, CardId.Tradetoad, this.SummonFrog);
            this.AddExecutor(ExecutorType.Summon, CardId.TreebornFrog, this.SummonFrog);
            this.AddExecutor(ExecutorType.Summon, CardId.FlipFlopFrog, this.SummonFrog);

            this.AddExecutor(ExecutorType.MonsterSet, CardId.FlipFlopFrog);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.DupeFrog);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.Tradetoad);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.Ronintoadin);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.TreebornFrog);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.Unifrog);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.SwapFrog);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.SubmarineFrog);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.DewdarkOfTheIceBarrier);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.CryomancerOfTheIceBarrier);

            this.AddExecutor(ExecutorType.Repos, this.FrogMonsterRepos);

            this.AddExecutor(ExecutorType.Activate, CardId.FroggyForcefield, this.DefaultTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.TheHugeRevolutionIsOver, this.DefaultTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.GravityBind, this.GravityBind);
        }

        private int m_swapFrogSummoned;
        private int m_flipFlopFrogSummoned;
        private int m_treebornFrogCount = 0;

        public override void OnNewTurn()
        {
            this.m_treebornFrogCount = 0;
            base.OnNewTurn();
        }

        private bool TreebornFrog()
        {
            this.m_treebornFrogCount++;
            return this.m_treebornFrogCount <= 5;
        }

        private bool SwapFrogSummon()
        {
            int atk = this.Card.Attack + this.GetSpellBonus();
            if (this.Util.IsAllEnemyBetterThanValue(atk, true))
            {
                return false;
            }

            this.AI.SelectCard(CardId.Ronintoadin);
            this.m_swapFrogSummoned = this.Duel.Turn;
            return true;
        }

        private bool SwapFrogActivate()
        {
            if (this.m_swapFrogSummoned != this.Duel.Turn)
            {
                return false;
            }

            this.m_swapFrogSummoned = -1;

            if (this.Bot.GetRemainingCount(CardId.Ronintoadin, 2) == 0)
            {
                return false;
            }

            this.AI.SelectCard(CardId.Ronintoadin);
            return true;
        }

        private bool DupeFrog()
        {
            this.AI.SelectCard(CardLocation.Deck);
            return true;
        }

        private bool FlipFlopFrog()
        {
            if (this.Card.IsDefense() || this.m_flipFlopFrogSummoned == this.Duel.Turn || this.Duel.Phase == DuelPhase.Main2)
            {
                this.m_flipFlopFrogSummoned = -1;
                List<ClientCard> monsters = this.Enemy.GetMonsters();
                monsters.Sort(CardContainer.CompareCardAttack);
                monsters.Reverse();
                this.AI.SelectCard(monsters);
                return true;
            }
            return false;
        }

        private bool Ronintoadin()
        {
            List<ClientCard> monsters = this.Bot.GetGraveyardMonsters();
            if (monsters.Count > 2)
            {
                if (this.GetSpellBonus() == 0)
                {
                    this.AI.SelectPosition(CardPosition.FaceUpDefence);
                }

                return true;
            }
            return false;
        }

        private bool SummonFrog()
        {
            int atk = this.Card.Attack + this.GetSpellBonus();

            if (this.Util.IsOneEnemyBetterThanValue(atk, true))
            {
                return false;
            }

            if (this.Card.IsCode(CardId.SwapFrog))
            {
                this.m_swapFrogSummoned = this.Duel.Turn;
            }

            return true;
        }

        private bool PotOfDuality()
        {
            List<int> cards = new List<int>();
            
            if (this.Util.IsOneEnemyBetter())
            {
                cards.Add(CardId.FlipFlopFrog);
            }

            if (this.Bot.SpellZone[5] == null)
            {
                cards.Add(CardId.Terraforming);
                cards.Add(CardId.Wetlands);
            }

            cards.Add(CardId.DarkHole);
            cards.Add(CardId.SwapFrog);
            cards.Add(CardId.GravityBind);

            if (cards.Count > 0)
            {
                this.AI.SelectCard(cards);
                return true;
            }

            return false;
        }

        private bool Terraforming()
        {
            if (this.Bot.HasInHand(CardId.Wetlands))
            {
                return false;
            }

            if (this.Bot.SpellZone[5] != null)
            {
                return false;
            }

            return true;
        }

        private bool Solidarity()
        {
            List<ClientCard> monsters = this.Bot.GetGraveyardMonsters();
            return monsters.Count != 0;
        }

        private bool GravityBind()
        {
            List<ClientCard> spells = this.Bot.GetSpells();
            foreach (ClientCard spell in spells)
            {
                if (spell.IsCode(CardId.GravityBind) && !spell.IsFacedown())
                {
                    return false;
                }
            }
            return true;
        }

        private bool FrogMonsterRepos()
        {
            if (this.Card.IsCode(CardId.Unifrog))
            {
                return this.Card.IsDefense();
            }

            if (this.Card.IsCode(CardId.DewdarkOfTheIceBarrier))
            {
                return this.Card.IsDefense();
            }

            bool enemyBetter = this.Util.IsOneEnemyBetterThanValue(this.Card.Attack + (this.Card.IsFacedown() ? this.GetSpellBonus() : 0), true);
            if (this.Card.Attack < 800)
            {
                enemyBetter = true;
            }

            bool result = false;
            if (this.Card.IsAttack() && enemyBetter)
            {
                result =  true;
            }

            if (this.Card.IsDefense() && !enemyBetter)
            {
                result = true;
            }

            if (!result && this.Card.IsCode(CardId.FlipFlopFrog) && this.Enemy.GetMonsterCount() > 0 && this.Card.IsFacedown())
            {
                result = true;
            }

            if (this.Card.IsCode(CardId.FlipFlopFrog) && this.Card.IsFacedown() && result)
            {
                this.m_flipFlopFrogSummoned = this.Duel.Turn;
            }

            return result;
        }

        private int GetSpellBonus()
        {
            int atk = 0;
            if (this.Bot.SpellZone[5] != null)
            {
                atk += 1200;
            }

            List<ClientCard> monsters = this.Bot.GetGraveyardMonsters();
            if (monsters.Count != 0)
            {
                foreach (ClientCard card in this.Bot.GetSpells())
                {
                    if (card.IsCode(CardId.Solidarity))
                    {
                        atk += 800;
                    }
                }
            }

            return atk;
        }
    }
}
