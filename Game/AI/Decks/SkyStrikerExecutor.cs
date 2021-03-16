using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("SkyStriker", "AI_SkyStriker")]
    public class SkyStrikerExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int Raye = 26077387;
            public const int Kagari = 63288573;
            public const int Shizuku = 90673288;
            public const int Hayate = 8491308;
            public const int Token = 52340445;

            public const int Engage = 63166095;
            public const int HornetDrones = 52340444;
            public const int WidowAnchor = 98338152;
            public const int Afterburners = 99550630;
            public const int JammingWave = 25955749;
            public const int Multirole = 24010609;
            public const int HerculesBase = 97616504;
            public const int AreaZero = 50005218;

            public const int AshBlossom = 14558127;
            public const int GhostRabbit = 59438930;
            public const int MaxxC = 23434538;
            public const int JetSynchron = 9742784;
            public const int EffectVeiler = 97268402;

            public const int ReinforcementOfTheArmy = 32807846;
            public const int FoolishBurialGoods = 35726888;
            public const int UpstartGoblin = 70368879;
            public const int MetalfoesFusion = 73594093;
            public const int TwinTwisters = 43898403;
            public const int SolemnJudgment = 41420027;
            public const int SolemnWarning = 84749824;

            public const int HiSpeedroidChanbara = 42110604;
            public const int TopologicBomberDragon = 5821478;
            public const int TopologicTrisbaena = 72529749;
            public const int SummonSorceress = 61665245;
            public const int TroymareUnicorn = 38342335;
            public const int TroymarePhoenix = 2857636;
            public const int CrystronNeedlefiber = 50588353;
            public const int Linkuriboh = 41999284;
        }

        bool KagariSummoned = false;
        bool ShizukuSummoned = false;
        bool HayateSummoned = false;
        ClientCard WidowAnchorTarget = null;

        public SkyStrikerExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.Activate, CardId.AshBlossom, this.DefaultAshBlossomAndJoyousSpring);
            this.AddExecutor(ExecutorType.Activate, CardId.GhostRabbit, this.DefaultGhostOgreAndSnowRabbit);
            this.AddExecutor(ExecutorType.Activate, CardId.EffectVeiler, this.DefaultBreakthroughSkill);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnWarning, this.DefaultSolemnWarning);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnJudgment, this.DefaultSolemnJudgment);

            this.AddExecutor(ExecutorType.Activate, CardId.MaxxC, this.MaxxCEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.ReinforcementOfTheArmy);
            this.AddExecutor(ExecutorType.Activate, CardId.UpstartGoblin);
            this.AddExecutor(ExecutorType.Activate, CardId.FoolishBurialGoods, this.FoolishBurialGoodsEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.TwinTwisters, this.TwinTwistersEffect);

            //
            this.AddExecutor(ExecutorType.Activate, CardId.Multirole, this.MultiroleHandEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.WidowAnchor, this.WidowAnchorEffectFirst);

            this.AddExecutor(ExecutorType.Activate, CardId.Afterburners, this.AfterburnersEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.JammingWave, this.JammingWaveEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.Engage, this.EngageEffectFirst);

            this.AddExecutor(ExecutorType.Activate, CardId.HornetDrones, this.HornetDronesEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.WidowAnchor, this.WidowAnchorEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.HerculesBase, this.HerculesBaseEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.AreaZero, this.AreaZeroEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Multirole, this.MultiroleEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.Engage, this.EngageEffect);

            //
            this.AddExecutor(ExecutorType.Summon, CardId.JetSynchron, this.TunerSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.EffectVeiler, this.TunerSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.GhostRabbit, this.TunerSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.AshBlossom, this.TunerSummon);

            this.AddExecutor(ExecutorType.Activate, CardId.Raye, this.RayeEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Kagari, this.KagariSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.Kagari, this.KagariEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.CrystronNeedlefiber, this.CrystronNeedlefiberSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.CrystronNeedlefiber, this.CrystronNeedlefiberEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.SummonSorceress);
            this.AddExecutor(ExecutorType.Activate, CardId.SummonSorceress, this.SummonSorceressEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.JetSynchron, this.JetSynchronEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.HiSpeedroidChanbara);

            this.AddExecutor(ExecutorType.SpSummon, CardId.Shizuku, this.ShizukuSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.Shizuku, this.ShizukuEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Hayate, this.HayateSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.Hayate, this.HayateEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.TopologicBomberDragon, this.Util.IsTurn1OrMain2);

            this.AddExecutor(ExecutorType.Summon, CardId.Raye, this.RayeSummon);

            //
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnJudgment);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnWarning);
            this.AddExecutor(ExecutorType.SpellSet, CardId.WidowAnchor);
            this.AddExecutor(ExecutorType.SpellSet, CardId.HerculesBase);

            this.AddExecutor(ExecutorType.SpellSet, CardId.TwinTwisters, this.HandFull);
            this.AddExecutor(ExecutorType.SpellSet, CardId.HornetDrones, this.HandFull);

            //
            this.AddExecutor(ExecutorType.Activate, CardId.MetalfoesFusion);
            this.AddExecutor(ExecutorType.Activate, CardId.Multirole, this.MultiroleEPEffect);

            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
        }

        public override bool OnSelectHand()
        {
            // go first
            return true;
        }

        public override void OnNewTurn()
        {
            this.KagariSummoned = false;
            this.ShizukuSummoned = false;
            this.HayateSummoned = false;
            this.WidowAnchorTarget = null;
        }

        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (!defender.IsMonsterHasPreventActivationEffectInBattle())
            {
                if (attacker.IsCode(CardId.HiSpeedroidChanbara) && !attacker.IsDisabled())
                {
                    attacker.RealPower = attacker.RealPower + 200;
                }
            }
            return base.OnPreBattleBetween(attacker, defender);
        }

        public override bool OnSelectYesNo(int desc)
        {
            if (desc == this.Util.GetStringId(CardId.SummonSorceress, 2)) // summon to the field of opponent?
            {
                return false;
            }

            if (desc == this.Util.GetStringId(CardId.Engage, 0)) // draw card?
            {
                return true;
            }

            if (desc == this.Util.GetStringId(CardId.WidowAnchor, 0)) // get control?
            {
                return true;
            }

            if (desc == this.Util.GetStringId(CardId.JammingWave, 0)) // destroy monster?
            {
                ClientCard target = this.Util.GetBestEnemyMonster();
                if (target != null)
                {
                    this.AI.SelectCard(target);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (desc == this.Util.GetStringId(CardId.Afterburners, 0)) // destroy spell & trap?
            {
                ClientCard target = this.Util.GetBestEnemySpell();
                if (target != null)
                {
                    this.AI.SelectCard(target);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return base.OnSelectYesNo(desc);
        }

        private bool MaxxCEffect()
        {
            return this.Duel.Player == 1;
        }

        private bool TwinTwistersEffect()
        {
            if (this.Util.ChainContainsCard(CardId.TwinTwisters))
            {
                return false;
            }

            IList<ClientCard> targets = new List<ClientCard>();
            foreach (ClientCard target in this.Enemy.GetSpells())
            {
                if (target.IsFloodgate())
                {
                    targets.Add(target);
                }

                if (targets.Count >= 2)
                {
                    break;
                }
            }
            if (targets.Count < 2)
            {
                foreach (ClientCard target in this.Enemy.GetSpells())
                {
                    if (target.IsFacedown() || target.HasType(CardType.Continuous) || target.HasType(CardType.Pendulum))
                    {
                        targets.Add(target);
                    }

                    if (targets.Count >= 2)
                    {
                        break;
                    }
                }
            }
            if (targets.Count > 0)
            {
                this.AI.SelectCard(this.GetDiscardHand());
                this.AI.SelectNextCard(targets);
                return true;
            }
            return false;
        }

        private bool FoolishBurialGoodsEffect()
        {
            this.AI.SelectCard(
                CardId.MetalfoesFusion,
                CardId.WidowAnchor,
                CardId.Engage,
                CardId.HornetDrones
                );
            return true;
        }

        private bool MultiroleHandEffect()
        {
            return this.Card.Location == CardLocation.Hand;
        }

        private bool MultiroleEPEffect()
        {
            if (this.Duel.Phase != DuelPhase.End)
            {
                return false;
            }

            IList<int> targets = new[] {
                CardId.Engage,
                CardId.HornetDrones,
                CardId.WidowAnchor
            };
            this.AI.SelectCard(targets);
            this.AI.SelectNextCard(targets);
            this.AI.SelectThirdCard(targets);
            return true;
        }

        private bool AfterburnersEffect()
        {
            ClientCard target = this.Util.GetBestEnemyMonster(true, true);
            if (target != null)
            {
                this.AI.SelectCard(target);
                return true;
            }
            return false;
        }

        private bool JammingWaveEffect()
        {
            ClientCard target = null;
            foreach(ClientCard card in this.Enemy.GetSpells())
            {
                if (card.IsFacedown())
                {
                    target = card;
                    break;
                }
            }
            if (target != null)
            {
                this.AI.SelectCard(target);
                return true;
            }
            return false;
        }

        private bool WidowAnchorEffectFirst()
        {
            if (this.Util.ChainContainsCard(CardId.WidowAnchor))
            {
                return false;
            }

            ClientCard target = this.Util.GetProblematicEnemyMonster(0, true);
            if (target != null)
            {
                this.WidowAnchorTarget = target;
                this.AI.SelectCard(target);
                return true;
            }
            return false;
        }

        private bool EngageEffectFirst()
        {
            if (!this.HaveThreeSpellsInGrave())
            {
                return false;
            }

            int target = this.GetCardToSearch();
            if (target > 0)
            {
                this.AI.SelectCard(target);
            }
            else
            {
                this.AI.SelectCard(
                    CardId.Multirole,
                    CardId.AreaZero,
                    CardId.Afterburners,
                    CardId.JammingWave,
                    CardId.Raye
                    );
            }

            return true;
        }


        private bool EngageEffect()
        {
            int target = this.GetCardToSearch();
            if (target > 0)
            {
                this.AI.SelectCard(target);
            }
            else
            {
                this.AI.SelectCard(
                    CardId.Multirole,
                    CardId.AreaZero,
                    CardId.Afterburners,
                    CardId.JammingWave,
                    CardId.Raye
                    );
            }

            return true;
        }

        private bool HornetDronesEffect()
        {
            if (this.Duel.Player == 1)
            {
                return this.Duel.Phase == DuelPhase.End;
            }
            else
            {
                if (this.Duel.Phase != DuelPhase.Main1)
                {
                    return false;
                }

                if (this.Duel.CurrentChain.Count > 0)
                {
                    return false;
                }

                if (this.Bot.GetMonstersExtraZoneCount() == 0)
                {
                    return true;
                }

                if (this.Bot.HasInMonstersZone(CardId.SummonSorceress))
                {
                    return true;
                }

                if (this.Bot.HasInMonstersZone(CardId.TopologicBomberDragon) && this.Enemy.GetMonsterCount() > 1)
                {
                    return true;
                }

                if (!this.Util.IsTurn1OrMain2())
                {
                    foreach (ClientCard card in this.Bot.Hand)
                    {
                        if (card.IsTuner())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool WidowAnchorEffect()
        {
            if (this.DefaultBreakthroughSkill())
            {
                this.WidowAnchorTarget = this.Util.GetLastChainCard();
                return true;
            }

            if (!this.HaveThreeSpellsInGrave() || this.Duel.Player == 1 || this.Duel.Phase < DuelPhase.Main1 || this.Duel.Phase >= DuelPhase.Main2 || this.Util.ChainContainsCard(CardId.WidowAnchor))
            {
                return false;
            }

            ClientCard target = this.Util.GetBestEnemyMonster(true, true);
            if (target != null && !target.IsDisabled() && !target.HasType(CardType.Normal))
            {
                this.WidowAnchorTarget = target;
                this.AI.SelectCard(target);
                return true;
            }
            return false;
        }

        private bool HerculesBaseEffect()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                IList<ClientCard> targets = new List<ClientCard>();
                foreach(ClientCard card in this.Bot.GetGraveyardMonsters())
                {
                    if (card.IsCode(CardId.Hayate, CardId.Kagari, CardId.Shizuku))
                    {
                        targets.Add(card);
                    }
                }
                if (targets.Count > 0)
                {
                    this.AI.SelectCard(targets);
                    return true;
                }
            }
            else
            {
                if (this.Util.IsTurn1OrMain2())
                {
                    return false;
                }

                ClientCard bestBotMonster = this.Util.GetBestBotMonster(true);
                if (bestBotMonster != null)
                {
                    int bestPower = bestBotMonster.Attack;
                    int count = 0;
                    bool have3 = this.HaveThreeSpellsInGrave();
                    foreach (ClientCard target in this.Enemy.GetMonsters())
                    {
                        if (target.GetDefensePower() < bestPower && !target.IsMonsterInvincible())
                        {
                            count++;
                            if (count > 1 || have3)
                            {
                                this.AI.SelectCard(bestBotMonster);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool AreaZeroEffect()
        {
            if (this.Card.Location == CardLocation.Hand || this.Card.Location == CardLocation.Grave)
            {
                return true;
            }
            foreach (ClientCard target in this.Bot.GetMonsters())
            {
                if (target == this.WidowAnchorTarget && this.Duel.Phase == DuelPhase.Main2)
                {
                    this.AI.SelectCard(target);
                    return true;
                }
            }
            foreach (ClientCard target in this.Bot.GetMonsters())
            {
                if (target.IsCode(CardId.Raye) && this.Bot.GetMonstersExtraZoneCount() == 0)
                {
                    this.AI.SelectCard(target);
                    return true;
                }
            }
            foreach (ClientCard target in this.Bot.GetSpells())
            {
                if (!target.IsCode(CardId.AreaZero, CardId.Multirole, CardId.WidowAnchor) && target.IsSpell())
                {
                    this.AI.SelectCard(target);
                    return true;
                }
            }
            return false;
        }

        private bool MultiroleEffect()
        {
            if (this.Card.Location == CardLocation.SpellZone)
            {
                foreach (ClientCard target in this.Bot.GetMonsters())
                {
                    if (target == this.WidowAnchorTarget && this.Duel.Phase == DuelPhase.Main2)
                    {
                        this.AI.SelectCard(target);
                        return true;
                    }
                }
                foreach (ClientCard target in this.Bot.GetMonsters())
                {
                    if (target.IsCode(CardId.Raye) && this.Bot.GetMonstersExtraZoneCount() == 0)
                    {
                        this.AI.SelectCard(target);
                        return true;
                    }
                }
                foreach (ClientCard target in this.Bot.GetSpells())
                {
                    if (target.IsCode(CardId.AreaZero))
                    {
                        this.AI.SelectCard(target);
                        return true;
                    }
                }
                foreach (ClientCard target in this.Bot.GetSpells())
                {
                    if (!target.IsCode(CardId.Multirole, CardId.WidowAnchor) && target.IsSpell())
                    {
                        this.AI.SelectCard(target);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool RayeSummon()
        {
            if (this.Bot.GetMonstersExtraZoneCount() == 0)
            {
                return true;
            }
            return false;
        }

        private bool RayeEffect()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                return true;
            }
            if (this.Card.IsDisabled())
            {
                return false;
            }
            if (this.Util.IsChainTarget(this.Card))
            {
                this.RayeSelectTarget();
                return true;
            }
            if (this.Card.Attacked && this.Duel.Phase == DuelPhase.BattleStart)
            {
                this.RayeSelectTarget();
                return true;
            }
            if (this.Card == this.Bot.BattlingMonster && this.Duel.Player == 1)
            {
                this.RayeSelectTarget();
                return true;
            }
            if (this.Duel.Phase == DuelPhase.Main2)
            {
                this.RayeSelectTarget();
                return true;
            }
            return false;
        }

        private void RayeSelectTarget()
        {
            if (!this.KagariSummoned && this.Bot.HasInGraveyard(new[] {
                CardId.Engage,
                CardId.HornetDrones,
                CardId.WidowAnchor
            }))
            {
                this.AI.SelectCard(CardId.Kagari);
            }
            else
            {
                this.AI.SelectCard(CardId.Shizuku, CardId.Kagari, CardId.Hayate);
            }
        }

        private bool KagariSummon()
        {
            if (this.Bot.HasInGraveyard(new[] {
                CardId.Engage,
                CardId.HornetDrones,
                CardId.WidowAnchor
            }))
            {
                this.KagariSummoned = true;
                return true;
            }
            return false;
        }

        private bool KagariEffect()
        {
            if (this.EmptyMainMonsterZone() && this.Util.GetProblematicEnemyMonster() != null && this.Bot.HasInGraveyard(CardId.Afterburners))
            {
                this.AI.SelectCard(CardId.Afterburners);
            }
            else if (this.EmptyMainMonsterZone() && this.Util.GetProblematicEnemySpell() != null && this.Bot.HasInGraveyard(CardId.JammingWave))
            {
                this.AI.SelectCard(CardId.JammingWave);
            }
            else
            {
                this.AI.SelectCard(CardId.Engage, CardId.HornetDrones, CardId.WidowAnchor);
            }

            return true;
        }

        private bool ShizukuSummon()
        {
            if (this.Util.IsTurn1OrMain2())
            {
                this.ShizukuSummoned = true;
                return true;
            }
            return false;
        }

        private bool ShizukuEffect()
        {
            int target = this.GetCardToSearch();
            if (target != 0)
            {
                this.AI.SelectCard(target);
            }
            else
            {
                this.AI.SelectCard(CardId.Engage, CardId.HornetDrones, CardId.WidowAnchor);
            }

            return true;
        }

        private bool HayateSummon()
        {
            if (this.Util.IsTurn1OrMain2())
            {
                return false;
            }

            this.HayateSummoned = true;
            return true;
        }

        private bool HayateEffect()
        {
            if (!this.Bot.HasInGraveyard(CardId.Raye))
            {
                this.AI.SelectCard(CardId.Raye);
            }
            else if (!this.Bot.HasInGraveyard(CardId.HornetDrones))
            {
                this.AI.SelectCard(CardId.HornetDrones);
            }
            else if (!this.Bot.HasInGraveyard(CardId.WidowAnchor))
            {
                this.AI.SelectCard(CardId.WidowAnchor);
            }

            return true;
        }

        private bool TunerSummon()
        {
            return !this.Bot.HasInMonstersZone(new[] {
                CardId.AshBlossom,
                CardId.EffectVeiler,
                CardId.GhostRabbit,
                CardId.JetSynchron
            }) && !this.Util.IsTurn1OrMain2()
               && this.Bot.GetMonsterCount() > 0
               && this.Bot.HasInExtra(CardId.CrystronNeedlefiber);
        }

        private bool CrystronNeedlefiberSummon()
        {
            return !this.Util.IsTurn1OrMain2();
        }

        private bool CrystronNeedlefiberEffect()
        {
            this.AI.SelectCard(CardId.JetSynchron);
            return true;
        }

        private bool SummonSorceressEffect()
        {
            if (this.ActivateDescription == -1)
            {
                return false;
            }

            return true;
        }

        private bool JetSynchronEffect()
        {
            if (this.Bot.HasInMonstersZone(CardId.Raye) || this.Bot.HasInMonstersZone(CardId.CrystronNeedlefiber))
            {
                this.AI.SelectCard(this.GetDiscardHand());
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;
        }

        private bool HandFull()
        {
            return this.Bot.GetSpellCountWithoutField() < 4 && this.Bot.Hand.Count > 4;
        }

        private int GetDiscardHand()
        {
            if (this.Bot.HasInHand(CardId.MetalfoesFusion))
            {
                return CardId.MetalfoesFusion;
            }

            if (this.Bot.HasInHand(CardId.Raye) && !this.Bot.HasInGraveyard(CardId.Raye))
            {
                return CardId.Raye;
            }

            if (this.Bot.HasInHand(CardId.JetSynchron))
            {
                return CardId.JetSynchron;
            }

            if (this.Bot.HasInHand(CardId.ReinforcementOfTheArmy))
            {
                return CardId.ReinforcementOfTheArmy;
            }

            if (this.Bot.HasInHand(CardId.FoolishBurialGoods))
            {
                return CardId.FoolishBurialGoods;
            }

            return 0;
        }

        private int GetCardToSearch()
        {
            if (!this.Bot.HasInHand(CardId.HornetDrones) && this.Bot.GetRemainingCount(CardId.HornetDrones, 3) > 0)
            {
                return CardId.HornetDrones;
            }
            else if (this.Util.GetProblematicEnemyMonster() != null && this.Bot.GetRemainingCount(CardId.WidowAnchor, 3) > 0)
            {
                return CardId.WidowAnchor;
            }
            else if (this.EmptyMainMonsterZone() && this.Util.GetProblematicEnemyMonster() != null && this.Bot.GetRemainingCount(CardId.Afterburners, 1) > 0)
            {
                return CardId.Afterburners;
            }
            else if (this.EmptyMainMonsterZone() && this.Util.GetProblematicEnemySpell() != null && this.Bot.GetRemainingCount(CardId.JammingWave, 1) > 0)
            {
                return CardId.JammingWave;
            }
            else if (!this.Bot.HasInHand(CardId.Raye) && !this.Bot.HasInMonstersZone(CardId.Raye) && this.Bot.GetRemainingCount(CardId.Raye, 3) > 0)
            {
                return CardId.Raye;
            }
            else if (!this.Bot.HasInHand(CardId.WidowAnchor) && !this.Bot.HasInSpellZone(CardId.WidowAnchor) && this.Bot.GetRemainingCount(CardId.WidowAnchor, 3) > 0)
            {
                return CardId.WidowAnchor;
            }

            return 0;
        }

        private bool EmptyMainMonsterZone()
        {
            for (int i = 0; i < 5; i++)
            {
                if (this.Bot.MonsterZone[i] != null)
                {
                    return false;
                }
            }
            return true;
        }

        private bool HaveThreeSpellsInGrave()
        {
            int count = 0;
            foreach(ClientCard card in this.Bot.Graveyard)
            {
                if (card.IsSpell())
                {
                    count++;
                }
            }
            return count >= 3;
        }

        private bool DefaultNoExecutor()
        {
            foreach (CardExecutor exec in this.Executors)
            {
                if (exec.Type == this.ExecType && exec.CardId == this.Card.Id)
                {
                    return false;
                }
            }
            return true;
        }

    }
}