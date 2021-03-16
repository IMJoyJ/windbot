using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
    [Deck("Horus", "AI_Horus", "Easy")]
    public class HorusExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int AlexandriteDragon = 43096270;
            public const int LusterDragon = 11091375;
            public const int WhiteNightDragon = 79473793;
            public const int HorusTheBlackFlameDragonLv8 = 48229808;
            public const int HorusTheBlackFlameDragonLv6 = 11224103;
            public const int CyberDragon = 70095154;
            public const int AxeDragonute = 84914462;
            public const int DodgerDragon = 47013502;
            public const int GolemDragon = 9666558;
            public const int Raigeki = 12580477;
            public const int HammerShot = 26412047;
            public const int DarkHole = 53129443;
            public const int Fissure = 66788016;
            public const int StampingDestruction = 81385346;
            public const int FoolishBurial = 81439173;
            public const int MonsterReborn = 83764718;
            public const int MysticalSpaceTyphoon = 5318639;
            public const int BellowOfTheSilverDragon = 80600103;
            public const int Mountain = 50913601;
            public const int DragonsRebirth = 20638610;
            public const int MirrorForce = 44095762;
            public const int DimensionalPrison = 70342110;
        }

        public HorusExecutor(GameAI ai, Duel duel) : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.SpellSet, this.DefaultSpellSet);

            this.AddExecutor(ExecutorType.Activate, CardId.HorusTheBlackFlameDragonLv6);
            this.AddExecutor(ExecutorType.Activate, CardId.StampingDestruction, this.DefaultMysticalSpaceTyphoon);
            this.AddExecutor(ExecutorType.Activate, CardId.MysticalSpaceTyphoon, this.DefaultMysticalSpaceTyphoon);
            this.AddExecutor(ExecutorType.Activate, CardId.FoolishBurial, this.FoolishBurial);
            this.AddExecutor(ExecutorType.Activate, CardId.Mountain, this.DefaultField);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);

            this.AddExecutor(ExecutorType.SpSummon, CardId.CyberDragon);

            this.AddExecutor(ExecutorType.Activate, CardId.Raigeki, this.DefaultRaigeki);
            this.AddExecutor(ExecutorType.Activate, CardId.HammerShot, this.DefaultHammerShot);
            this.AddExecutor(ExecutorType.Activate, CardId.Fissure);

            this.AddExecutor(ExecutorType.Activate, CardId.BellowOfTheSilverDragon, this.BellowOfTheSilverDragon);
            this.AddExecutor(ExecutorType.Activate, CardId.MonsterReborn, this.MonsterReborn);

            this.AddExecutor(ExecutorType.Summon, CardId.WhiteNightDragon, this.WhiteNightDragon);
            this.AddExecutor(ExecutorType.Summon, CardId.HorusTheBlackFlameDragonLv6, this.DefaultTributeSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.AlexandriteDragon);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.AxeDragonute);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.DodgerDragon);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.GolemDragon);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.LusterDragon);

            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);

            this.AddExecutor(ExecutorType.Activate, CardId.HorusTheBlackFlameDragonLv8, this.HorusTheBlackFlameDragonLv8);
            this.AddExecutor(ExecutorType.Activate, CardId.MirrorForce, this.DefaultTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.DimensionalPrison, this.DefaultTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.DragonsRebirth, this.DragonsRebirth);
        }

        private bool FoolishBurial()
        {
            if (this.Bot.HasInGraveyard(CardId.WhiteNightDragon))
            {
                return false;
            }

            if (this.Bot.HasInHand(CardId.WhiteNightDragon))
            {
                return false;
            }

            int remaining = 2;
            foreach (ClientCard card in this.Bot.Banished)
            {
                if (card.IsCode(CardId.WhiteNightDragon))
                {
                    remaining--;
                }
            }

            if (remaining > 0)
            {
                this.AI.SelectCard(CardId.WhiteNightDragon);
                return true;
            }
            return false;
        }

        private bool BellowOfTheSilverDragon()
        {
            if (this.Duel.Player == 0 && (this.Duel.Phase == DuelPhase.Draw || this.Duel.Phase == DuelPhase.Standby))
            {
                return false;
            }

            if (this.Duel.Player == 1 && this.Duel.Phase == DuelPhase.End)
            {
                return false;
            }

            List<ClientCard> cards = new List<ClientCard>(this.Bot.Graveyard);
            cards.Sort(CardContainer.CompareCardAttack);
            for (int i = cards.Count - 1; i >= 0; --i)
            {
                ClientCard card = cards[i];
                if (card.Attack < 1000)
                {
                    return false;
                }

                if (card.IsMonster() && card.HasType(CardType.Normal))
                {
                    this.AI.SelectCard(card);
                    return true;
                }
            }
            return false;
        }

        private bool MonsterReborn()
        {
            List<ClientCard> cards = new List<ClientCard>(this.Bot.Graveyard.GetMatchingCards(card => card.IsCanRevive()));
            cards.Sort(CardContainer.CompareCardAttack);
            ClientCard selectedCard = null;
            for (int i = cards.Count - 1; i >= 0; --i)
            {
                ClientCard card = cards[i];
                if (card.Attack < 1000)
                {
                    break;
                }

                if (card.IsMonster())
                {
                    selectedCard = card;
                    break;
                }
            }
            cards = new List<ClientCard>(this.Enemy.Graveyard.GetMatchingCards(card => card.IsCanRevive()));
            cards.Sort(CardContainer.CompareCardAttack);
            for (int i = cards.Count - 1; i >= 0; --i)
            {
                ClientCard card = cards[i];
                if (card.Attack < 1000)
                {
                    break;
                }

                if (card.IsMonster() && card.HasType(CardType.Normal) && (selectedCard == null || card.Attack > selectedCard.Attack))
                {
                    selectedCard = card;
                    break;
                }
            }
            if (selectedCard != null)
            {
                this.AI.SelectCard(selectedCard);
                return true;
            }
            return false;
        }

        private bool WhiteNightDragon()
        {
            // We should summon Horus the Black Flame Dragon LV6 if he can lvlup.
            if (this.Enemy.GetMonsterCount() != 0 && !this.Util.IsAllEnemyBetterThanValue(2300 - 1, false))
            {
                foreach (ClientCard card in this.Main.SummonableCards)
                {
                    if (card.IsCode(11224103))
                    {
                        return false;
                    }
                }
            }

            return this.DefaultTributeSummon();
        }

        private bool HorusTheBlackFlameDragonLv8()
        {
            return this.Duel.LastChainPlayer == 1;
        }

        private bool DragonsRebirth()
        {
            List<ClientCard> cards = new List<ClientCard>(this.Bot.GetMonsters());
            if (cards.Count == 0)
            {
                return false;
            }

            cards.Sort(CardContainer.CompareCardAttack);
            ClientCard tributeCard = null;
            foreach (ClientCard monster in cards)
            {
                if (monster.Attack > 2000)
                {
                    return false;
                }

                if (!monster.IsFacedown() && monster.Race == (int)CardRace.Dragon)
                {
                    tributeCard = monster;
                    break;
                }
            }

            if (tributeCard == null)
            {
                return false;
            }

            cards = new List<ClientCard>(this.Bot.Hand);
            cards.AddRange(this.Bot.Graveyard);
            if (cards.Count == 0)
            {
                return false;
            }

            cards.Sort(CardContainer.CompareCardAttack);
            ClientCard summonCard = null;
            for (int i = cards.Count - 1; i >= 0; --i)
            {
                ClientCard monster = cards[i];
                if (monster.Attack < 2300)
                {
                    return false;
                }

                if (monster.Race == (int)CardRace.Dragon && !monster.IsCode(CardId.HorusTheBlackFlameDragonLv8))
                {
                    summonCard = monster;
                    break;
                }
            }

            if (summonCard == null)
            {
                return false;
            }

            this.AI.SelectCard(tributeCard);
            this.AI.SelectNextCard(summonCard);

            return true;
        }
    }
}