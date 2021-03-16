using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    // NOT FINISHED YET
    [Deck("Nekroz", "AI_Nekroz", "NotFinished")]
    public class NekrozExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int DancePrincess = 52738610;
            public const int ThousandHands = 23401839;
            public const int TenThousandHands = 95492061;
            public const int Shurit = 90307777;
            public const int MaxxC = 23434538;
            public const int DecisiveArmor = 88240999;
            public const int Trishula = 52068432;
            public const int Valkyrus = 25857246;
            public const int Gungnir = 74122412;
            public const int Brionac = 26674724;
            public const int Unicore = 89463537;
            public const int Clausolas = 99185129;
            public const int PhantomOfChaos = 30312361;

            public const int DarkHole = 53129443;
            public const int ReinforcementOfTheArmy = 32807846;
            public const int TradeIn = 38120068;
            public const int PreparationOfRites = 96729612;
            public const int Mirror = 14735698;
            public const int Kaleidoscope = 51124303;
            public const int Cycle = 97211663;
            public const int MysticalSpaceTyphoon = 5318639;
            public const int RoyalDecree = 51452091;
            public const int EvilswarmExcitonKnight = 46772449;
            public const int HeraldOfTheArcLight = 79606837;
        }

        List<int> NekrozRituelCard = new List<int>();
        List<int> NekrozSpellCard = new List<int>();

        public NekrozExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.NekrozRituelCard.Add(CardId.Clausolas);
            this.NekrozRituelCard.Add(CardId.Unicore);
            this.NekrozRituelCard.Add(CardId.DecisiveArmor);
            this.NekrozRituelCard.Add(CardId.Brionac);
            this.NekrozRituelCard.Add(CardId.Trishula);
            this.NekrozRituelCard.Add(CardId.Gungnir);
            this.NekrozRituelCard.Add(CardId.Valkyrus);

            this.NekrozSpellCard.Add(CardId.Mirror);
            this.NekrozSpellCard.Add(CardId.Kaleidoscope);
            this.NekrozSpellCard.Add(CardId.Cycle);

            this.AddExecutor(ExecutorType.SpellSet, this.DefaultSpellSet);
            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);

            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);
            this.AddExecutor(ExecutorType.Activate, CardId.ReinforcementOfTheArmy, this.ReinforcementOfTheArmyEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.TradeIn);
            this.AddExecutor(ExecutorType.Activate, CardId.PreparationOfRites);
            this.AddExecutor(ExecutorType.Activate, CardId.Mirror);
            this.AddExecutor(ExecutorType.Activate, CardId.Kaleidoscope);
            this.AddExecutor(ExecutorType.Activate, CardId.Cycle);
            this.AddExecutor(ExecutorType.Activate, CardId.MysticalSpaceTyphoon, this.DefaultMysticalSpaceTyphoon);
            this.AddExecutor(ExecutorType.Activate, CardId.RoyalDecree);

            this.AddExecutor(ExecutorType.SummonOrSet, CardId.DancePrincess, this.DancePrincessSummon);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.Shurit, this.ShuritSet);
            this.AddExecutor(ExecutorType.Summon, CardId.ThousandHands, this.ThousandHandsSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.TenThousandHands, this.TenThousandHandsSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.PhantomOfChaos, this.PhantomOfChaosSummon);

            this.AddExecutor(ExecutorType.Activate, CardId.Unicore, this.UnicoreEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.DecisiveArmor, this.DecisiveArmorEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Valkyrus, this.ValkyrusEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Gungnir, this.GungnirEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Brionac, this.BrionacEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Clausolas, this.ClausolasEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Trishula);
            this.AddExecutor(ExecutorType.Activate, CardId.EvilswarmExcitonKnight, this.DefaultEvilswarmExcitonKnightEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.PhantomOfChaos, this.PhantomOfChaosEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.MaxxC);
            this.AddExecutor(ExecutorType.Activate, CardId.ThousandHands, this.ThousandHandsEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.TenThousandHands, this.BrionacEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.HeraldOfTheArcLight);
            this.AddExecutor(ExecutorType.Activate, CardId.Shurit);

            this.AddExecutor(ExecutorType.SpSummon, CardId.Trishula);
            this.AddExecutor(ExecutorType.SpSummon, CardId.DecisiveArmor);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Valkyrus);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Gungnir);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Brionac);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Unicore);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Clausolas);
            this.AddExecutor(ExecutorType.SpSummon, CardId.EvilswarmExcitonKnight, this.DefaultEvilswarmExcitonKnightSummon);
        }

        private bool ThousandHandsSummon()
        {
            if (!this.Bot.HasInHand(this.NekrozRituelCard) || this.Bot.HasInHand(CardId.Shurit) || !this.Bot.HasInHand(this.NekrozSpellCard))
            {
                return true;
            }

            foreach (ClientCard Card in this.Bot.Hand)
            {
                if (Card != null && Card.IsCode(CardId.Kaleidoscope) && !this.Bot.HasInHand(CardId.Unicore))
                {
                    return true;
                }
                else if (Card.IsCode(CardId.Trishula) || Card.IsCode(CardId.DecisiveArmor) && !this.Bot.HasInHand(CardId.Mirror) || !this.Bot.HasInHand(CardId.Shurit))
                {
                    return true;
                }
            }

            return false;
        }

        private bool ReinforcementOfTheArmyEffect()
        {
            if (!this.Bot.HasInGraveyard(CardId.Shurit) && !this.Bot.HasInHand(CardId.Shurit))
            {
                this.AI.SelectCard(CardId.Shurit);
                return true;
            }
            return false;
        }

        private bool TenThousandHandsSummon()
        {
                if (!this.Bot.HasInHand(CardId.ThousandHands) || !this.Bot.HasInHand(CardId.Shurit))
            {
                return true;
            }

            return false;
        }

        private bool DancePrincessSummon()
        {
            if (!this.Bot.HasInHand(CardId.ThousandHands) && !this.Bot.HasInHand(CardId.TenThousandHands))
            {
                return true;
            }

            return false;
        }

        private bool PhantomOfChaosSummon()
        {
            if (this.Bot.HasInGraveyard(CardId.Shurit) && this.Bot.HasInHand(this.NekrozSpellCard) && this.Bot.HasInHand(this.NekrozRituelCard))
            {
                return true;
            }

            return false;
        }

        private bool PhantomOfChaosEffect()
        {
            this.AI.SelectCard(CardId.Shurit);
            return true;
        }

        private bool ShuritSet()
        {
            if (!this.Bot.HasInHand(CardId.ThousandHands) && !this.Bot.HasInHand(CardId.TenThousandHands) && !this.Bot.HasInHand(CardId.DancePrincess))
            {
                return true;
            }

            return false;
        }

        private bool DecisiveArmorEffect()
        {
            if (this.Util.IsAllEnemyBetterThanValue(3300, true))
            {
                this.AI.SelectCard(CardId.DecisiveArmor);
                return true;
            }
            return false;
        }

        private bool ValkyrusEffect()
        {
            if (this.Duel.Phase == DuelPhase.Battle)
            {
                return true;
            }

            return false;
        }

        private bool GungnirEffect()
        {
            if (this.Util.IsOneEnemyBetter(true) && this.Duel.Phase == DuelPhase.Main1)
            {
                this.AI.SelectCard(this.Enemy.GetMonsters().GetHighestAttackMonster());
                return true;
            }
            return false;
        }

        private bool BrionacEffect()
        {
            if (!this.Bot.HasInHand(CardId.Shurit))
            {
                this.AI.SelectCard(CardId.Shurit);
                return true;
            }
            else if (!this.Bot.HasInHand(this.NekrozSpellCard))
            {
                this.AI.SelectCard(CardId.Mirror);
                return true;
            }
            else if (this.Util.IsOneEnemyBetterThanValue(3300, true) && !this.Bot.HasInHand(CardId.Trishula))
            {
                this.AI.SelectCard(CardId.Trishula);
                return true;
            }
            else if (this.Util.IsAllEnemyBetterThanValue(2700,true) && !this.Bot.HasInHand(CardId.DecisiveArmor))
            {
                this.AI.SelectCard(CardId.DecisiveArmor);
                return true;
            }
            else if (this.Bot.HasInHand(CardId.Unicore) && !this.Bot.HasInHand(CardId.Kaleidoscope))
            {
                this.AI.SelectCard(CardId.Kaleidoscope);
                return true;
            }
            else if (!this.Bot.HasInHand(CardId.Unicore) && this.Bot.HasInHand(CardId.Kaleidoscope))
            {
                this.AI.SelectCard(CardId.Unicore);
                return true;
            }
            return true;
        }

        private bool ThousandHandsEffect()
        {
            if (this.Util.IsOneEnemyBetterThanValue(3300, true) && !this.Bot.HasInHand(CardId.Trishula))
            {
                this.AI.SelectCard(CardId.Trishula);
                return true;
            }
            else if (this.Util.IsAllEnemyBetterThanValue(2700, true) && !this.Bot.HasInHand(CardId.DecisiveArmor))
            {
                this.AI.SelectCard(CardId.DecisiveArmor);
                return true;
            }
            else if (!this.Bot.HasInHand(CardId.Unicore) && this.Bot.HasInHand(CardId.Kaleidoscope))
            {
                this.AI.SelectCard(CardId.Unicore);
                return true;
            }
            return true;
        }

        private bool UnicoreEffect()
        {
            if (this.Bot.HasInGraveyard(CardId.Shurit))
            {
                this.AI.SelectCard(CardId.Shurit);
                return true;
            }
            return false;
        }

        private bool ClausolasEffect()
        {
            if (!this.Bot.HasInHand(this.NekrozSpellCard))
            {
                this.AI.SelectCard(CardId.Mirror);
                return true;
            }
            return false;
        }

        private bool IsTheLastPossibility()
        {
            if (!this.Bot.HasInHand(CardId.DecisiveArmor) && !this.Bot.HasInHand(CardId.Trishula))
            {
                return true;
            }

            return false;
        }

        private bool SelectNekrozWhoInvoke()
        {
            List<int> NekrozCard = new List<int>();
            try
            {
                foreach (ClientCard card in this.Bot.Hand)
                {
                    if (card != null && card.IsCode(this.NekrozRituelCard))
                    {
                        NekrozCard.Add(card.Id);
                    }
                }

                foreach (int Id in NekrozCard)
                {
                    if (Id == CardId.Trishula && this.Util.IsAllEnemyBetterThanValue(2700, true) && this.Bot.HasInHand(CardId.DecisiveArmor))
                    {
                        this.AI.SelectCard(CardId.Trishula);
                        return true;
                    }
                    else if (Id == CardId.DecisiveArmor)
                    {
                        this.AI.SelectCard(CardId.DecisiveArmor);
                        return true;
                    }
                    else if (Id == CardId.Unicore && this.Bot.HasInHand(CardId.Kaleidoscope) && !this.Bot.HasInGraveyard(CardId.Shurit))
                    {
                        this.AI.SelectCard(CardId.Unicore);
                        return true;
                    }
                    else if (Id == CardId.Valkyrus)
                    {
                        if (this.IsTheLastPossibility())
                        {
                            this.AI.SelectCard(CardId.Valkyrus);
                            return true;
                        }
                    }
                    else if (Id == CardId.Gungnir)
                    {
                        if (this.IsTheLastPossibility())
                        {
                            this.AI.SelectCard(CardId.Gungnir);
                            return true;
                        }
                    }
                    else if (Id == CardId.Clausolas)
                    {
                        if (this.IsTheLastPossibility())
                        {
                            this.AI.SelectCard(CardId.Clausolas);
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            { return false; }
        }
    }
}
