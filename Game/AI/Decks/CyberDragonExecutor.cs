using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    // NOT FINISHED YET
    [Deck("CyberDragon", "AI_CyberDragon", "NotFinished")]
    public class CyberDragonExecutor : DefaultExecutor
    {
        bool PowerBondUsed = false;

        public class CardId
        {
            public const int CyberLaserDragon = 4162088;
            public const int CyberBarrierDragon = 68774379;
            public const int CyberDragon = 70095154;
            public const int CyberDragonDrei = 59281922;
            public const int CyberPhoenix = 3370104;
            public const int ArmoredCybern = 67159705;
            public const int ProtoCyberDragon = 26439287;
            public const int CyberKirin = 76986005;
            public const int CyberDragonCore = 23893227;
            public const int CyberValley = 3657444;
            public const int Raigeki = 12580477;
            public const int DarkHole = 53129443;
            public const int DifferentDimensionCapsule = 11961740;
            public const int Polymerization = 24094653;
            public const int PowerBond = 37630732;
            public const int EvolutionBurst = 52875873;
            public const int PhotonGeneratorUnit = 66607691;
            public const int DeFusion = 95286165;
            public const int BottomlessTrapHole = 29401950;
            public const int MirrorForce = 44095762;
            public const int AttackReflectorUnit = 91989718;
            public const int CyberneticHiddenTechnology = 92773018;
            public const int CallOfTheHaunted = 97077563;
            public const int SevenToolsOfTheBandit = 3819470;
            public const int CyberTwinDragon = 74157028;
            public const int CyberEndDragon = 1546123;
            public const int CyberDragonNova = 58069384;
        }

        public CyberDragonExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.SpellSet, CardId.DeFusion);

            this.AddExecutor(ExecutorType.Activate, CardId.DifferentDimensionCapsule, this.Capsule);
            this.AddExecutor(ExecutorType.Activate, CardId.Raigeki, this.DefaultRaigeki);
            this.AddExecutor(ExecutorType.Activate, CardId.Polymerization, this.PolymerizationEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.PowerBond, this.PowerBondEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.EvolutionBurst, this.EvolutionBurstEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);
            this.AddExecutor(ExecutorType.Activate, CardId.PhotonGeneratorUnit);
            this.AddExecutor(ExecutorType.Activate, CardId.DeFusion, this.DeFusionEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.BottomlessTrapHole, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.MirrorForce, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.AttackReflectorUnit);
            this.AddExecutor(ExecutorType.Activate, CardId.SevenToolsOfTheBandit, this.DefaultTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.CallOfTheHaunted, this.DefaultCallOfTheHaunted);

            this.AddExecutor(ExecutorType.SummonOrSet, CardId.CyberDragonDrei, this.NoCyberDragonSpsummon);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.CyberPhoenix, this.NoCyberDragonSpsummon);
            this.AddExecutor(ExecutorType.Summon, CardId.CyberValley, this.NoCyberDragonSpsummon);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.CyberDragonCore, this.NoCyberDragonSpsummon);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.ArmoredCybern, this.ArmoredCybernSet);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.ProtoCyberDragon, this.ProtoCyberDragonSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.CyberKirin, this.CyberKirinSummon);

            this.AddExecutor(ExecutorType.SpSummon, CardId.CyberDragon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.CyberEndDragon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.CyberTwinDragon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.CyberBarrierDragon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.CyberLaserDragon);

            this.AddExecutor(ExecutorType.Activate, CardId.CyberBarrierDragon);
            this.AddExecutor(ExecutorType.Activate, CardId.CyberLaserDragon);
            this.AddExecutor(ExecutorType.Activate, CardId.CyberDragonDrei);
            this.AddExecutor(ExecutorType.Activate, CardId.CyberPhoenix);
            this.AddExecutor(ExecutorType.Activate, CardId.CyberKirin);
            this.AddExecutor(ExecutorType.Activate, CardId.ArmoredCybern, this.ArmoredCybernEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.CyberValley);

            this.AddExecutor(ExecutorType.SpellSet, this.DefaultSpellSet);
            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
        }

        private bool CyberDragonInHand()  { return this.Bot.HasInHand(CardId.CyberDragon); }
        private bool CyberDragonInGraveyard()  { return this.Bot.HasInGraveyard(CardId.CyberDragon); }
        private bool CyberDragonInMonsterZone() { return this.Bot.HasInMonstersZone(CardId.CyberDragon); }
        private bool CyberDragonIsBanished() { return this.Bot.HasInBanished(CardId.CyberDragon); }

        private bool Capsule()
        {
            IList<int> SelectedCard = new List<int>();
            SelectedCard.Add(CardId.PowerBond);
            SelectedCard.Add(CardId.DarkHole);
            SelectedCard.Add(CardId.Raigeki);
            this.AI.SelectCard(SelectedCard);
            return true;
        }

        private bool PolymerizationEffect()
        {
            if (this.Bot.GetCountCardInZone(this.Bot.MonsterZone, CardId.CyberDragon) + this.Bot.GetCountCardInZone(this.Bot.MonsterZone, CardId.ProtoCyberDragon) + this.Bot.GetCountCardInZone(this.Bot.MonsterZone, CardId.CyberDragonDrei) + this.Bot.GetCountCardInZone(this.Bot.MonsterZone, CardId.CyberDragonDrei) + this.Bot.GetCountCardInZone(this.Bot.Hand, CardId.CyberDragon) >= 3)
            {
                this.AI.SelectCard(CardId.CyberEndDragon);
            }
            else
            {
                this.AI.SelectCard(CardId.CyberTwinDragon);
            }

            return true;
        }

        private bool PowerBondEffect()
        {
            this.PowerBondUsed = true;
            if (this.Bot.GetCountCardInZone(this.Bot.MonsterZone, CardId.CyberDragon) + this.Bot.GetCountCardInZone(this.Bot.MonsterZone, CardId.ProtoCyberDragon) + this.Bot.GetCountCardInZone(this.Bot.Hand, CardId.CyberDragon) + this.Bot.GetCountCardInZone(this.Bot.Graveyard, CardId.CyberDragon) + this.Bot.GetCountCardInZone(this.Bot.Hand, CardId.CyberDragonCore) + this.Bot.GetCountCardInZone(this.Bot.Graveyard, CardId.CyberDragonCore) + this.Bot.GetCountCardInZone(this.Bot.Graveyard, CardId.CyberDragonDrei) + this.Bot.GetCountCardInZone(this.Bot.MonsterZone, CardId.CyberDragonDrei) >= 3)
            {
                this.AI.SelectCard(CardId.CyberEndDragon);
            }
            else
            {
                this.AI.SelectCard(CardId.CyberTwinDragon);
            }

            return true;
        }

        private bool EvolutionBurstEffect()
        {
            ClientCard bestMy = this.Bot.GetMonsters().GetHighestAttackMonster();
            if (bestMy == null || !this.Util.IsOneEnemyBetterThanValue(bestMy.Attack, false))
            {
                return false;
            }
            else
            {
                this.AI.SelectCard(this.Enemy.MonsterZone.GetHighestAttackMonster());
            }

            return true;
        }

        private bool NoCyberDragonSpsummon()
        {
            if (this.CyberDragonInHand() && (this.Bot.GetMonsterCount() == 0 && this.Enemy.GetMonsterCount() != 0))
            {
                return false;
            }

            return true;
        }

        private bool ArmoredCybernSet()
        {
            if (this.CyberDragonInHand() && (this.Bot.GetMonsterCount() == 0 && this.Enemy.GetMonsterCount() != 0) || (this.Bot.HasInHand(CardId.CyberDragonDrei) || this.Bot.HasInHand(CardId.CyberPhoenix)) && !this.Util.IsOneEnemyBetterThanValue(1800,true))
            {
                return false;
            }

            return true;
        }

        private bool ProtoCyberDragonSummon()
        {
            if (this.Bot.GetCountCardInZone(this.Bot.Hand, CardId.CyberDragon) + this.Bot.GetCountCardInZone(this.Bot.MonsterZone, CardId.CyberDragon) + this.Bot.GetCountCardInZone(this.Bot.MonsterZone, CardId.CyberDragonCore) >= 1 && this.Bot.HasInHand(CardId.Polymerization) || this.Bot.GetCountCardInZone(this.Bot.Hand, CardId.CyberDragon) + this.Bot.GetCountCardInZone(this.Bot.MonsterZone, CardId.CyberDragon) + this.Bot.GetCountCardInZone(this.Bot.Graveyard, CardId.CyberDragon) + this.Bot.GetCountCardInZone(this.Bot.Graveyard, CardId.CyberDragonCore) >= 1 && this.Bot.HasInHand(CardId.PowerBond))
            {
                return true;
            }

            if (this.CyberDragonInHand() && (this.Bot.GetMonsterCount() == 0 && this.Enemy.GetMonsterCount() != 0) || (this.Bot.HasInHand(CardId.CyberDragonDrei) || this.Bot.HasInHand(CardId.CyberPhoenix)) && !this.Util.IsOneEnemyBetterThanValue(1800, true))
            {
                return false;
            }

            return true;
        }

        private bool CyberKirinSummon()
        {
            return this.PowerBondUsed;
        }

        private bool ArmoredCybernEffect()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                return true;
            }
            else if (this.Card.Location == CardLocation.SpellZone)
            {
                if (this.Util.IsOneEnemyBetterThanValue(this.Bot.GetMonsters().GetHighestAttackMonster().Attack, true))
                {
                    if (this.ActivateDescription == this.Util.GetStringId(CardId.ArmoredCybern, 2))
                    {
                        return true;
                    }
                }

                return false;
            }
            return false;
        }

        private bool DeFusionEffect()
        {
            if (this.Duel.Phase == DuelPhase.Battle)
            {
                if (!this.Bot.HasAttackingMonster())
                {
                    return true;
                }
            }
            return false;
        }
    }
}