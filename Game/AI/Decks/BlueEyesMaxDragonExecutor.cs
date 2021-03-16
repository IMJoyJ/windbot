using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("BlueEyesMaxDragon", "AI_BlueEyesMaxDragon")]
    public class BlueEyesMaxDragonExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int BlueEyesWhiteDragon = 89631139;
            public const int BlueEyesAlternativeWhiteDragon = 38517737;
            public const int DeviritualTalismandra = 80701178;
            public const int ManguOfTheTenTousandHands = 95492061;
            public const int DevirrtualCandoll = 53303460;
            public const int AshBlossom = 14558127;
            public const int MaxxC = 23434538;
            public const int BlueEyesChaosMaxDragon = 55410871;

            public const int CreatureSwap= 31036355;
            public const int TheMelodyOfAwakeningDragon = 48800175;
            public const int UpstartGoblin = 70368879;
            public const int ChaosForm = 21082832;
            public const int AdvancedRitualArt = 46052429;
            public const int CalledByTheGrave = 24224830;
            public const int Scapegoat = 73915051;
            public const int InfiniteImpermanence = 10045474;
            public const int RecklessGreed = 37576645;

            public const int BorreloadDragon = 31833038;
            public const int BirrelswordDragon = 85289965;
            public const int KnightmareGryphon = 65330383;
            public const int MissusRadiant = 3987233;            
            public const int LinkSpider = 98978921;
            public const int Linkuriboh = 41999284;

            public const int LockBird = 94145021;
            public const int Ghost = 59438930;


        }

        public BlueEyesMaxDragonExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            //counter
            this.AddExecutor(ExecutorType.Activate, CardId.AshBlossom, this.DefaultAshBlossomAndJoyousSpring);
            this.AddExecutor(ExecutorType.Activate, CardId.MaxxC, this.MaxxCeff);
            this.AddExecutor(ExecutorType.Activate, CardId.InfiniteImpermanence, this.DefaultInfiniteImpermanence);
            this.AddExecutor(ExecutorType.Activate, CardId.CalledByTheGrave, this.CalledByTheGraveeff);
            //first
            this.AddExecutor(ExecutorType.Activate, CardId.UpstartGoblin);
            this.AddExecutor(ExecutorType.Activate, CardId.BlueEyesAlternativeWhiteDragon, this.BlueEyesAlternativeWhiteDragoneff);
            this.AddExecutor(ExecutorType.Activate, CardId.CreatureSwap, this.CreatureSwapeff);
            this.AddExecutor(ExecutorType.Activate, CardId.TheMelodyOfAwakeningDragon, this.TheMelodyOfAwakeningDragoneff);
            //summon
            this.AddExecutor(ExecutorType.Summon, CardId.ManguOfTheTenTousandHands);
            this.AddExecutor(ExecutorType.Activate, CardId.ManguOfTheTenTousandHands, this.TenTousandHandseff);
            this.AddExecutor(ExecutorType.Activate, this.DeviritualCheck);
            //ritual summon
            this.AddExecutor(ExecutorType.Activate, CardId.AdvancedRitualArt);
            this.AddExecutor(ExecutorType.Activate, CardId.ChaosForm, this.ChaosFormeff);
            //sp summon
            this.AddExecutor(ExecutorType.SpSummon, CardId.MissusRadiant, this.MissusRadiantsp);
            this.AddExecutor(ExecutorType.Activate, CardId.MissusRadiant, this.MissusRadianteff);
            this.AddExecutor(ExecutorType.Activate, CardId.Linkuriboh, this.Linkuriboheff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Linkuriboh, this.Linkuribohsp);
            this.AddExecutor(ExecutorType.SpSummon, CardId.LinkSpider);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BirrelswordDragon, this.BirrelswordDragonsp);
            this.AddExecutor(ExecutorType.Activate, CardId.BirrelswordDragon, this.BirrelswordDragoneff);
            //set
            this.AddExecutor(ExecutorType.Activate, CardId.TheMelodyOfAwakeningDragon, this.TheMelodyOfAwakeningDragoneffsecond);
            this.AddExecutor(ExecutorType.SpellSet, this.SpellSet);
            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
            //
            this.AddExecutor(ExecutorType.Activate, CardId.RecklessGreed, this.RecklessGreedeff);

            this.AddExecutor(ExecutorType.Activate, CardId.Scapegoat, this.Scapegoateff);
        }
        int Talismandra_count = 0;
        int Candoll_count = 0;
        bool Talismandra_used = false;
        bool Candoll_used = false;
        int RitualArt_count = 0;
        int ChaosForm_count = 0;
        int MaxDragon_count = 0;
        int TheMelody_count = 0;
        public override void OnNewTurn()
        {
            this.Talismandra_used = false;
            this.Candoll_used = false;
            base.OnNewTurn();
        }
        private void Count_check()
        {
            this.TheMelody_count = 0;
            this.Talismandra_count = 0;
            this.Candoll_count = 0;
            this.RitualArt_count = 0;
            this.ChaosForm_count = 0;
            this.MaxDragon_count = 0;
            foreach (ClientCard check in this.Bot.Hand)
            {
                if (check.IsCode(CardId.AdvancedRitualArt))
                {
                    this.RitualArt_count++;
                }

                if (check.IsCode(CardId.ChaosForm))
                {
                    this.ChaosForm_count++;
                }

                if (check.IsCode(CardId.DevirrtualCandoll))
                {
                    this.Candoll_count++;
                }

                if (check.IsCode(CardId.DeviritualTalismandra))
                {
                    this.Talismandra_count++;
                }

                if (check.IsCode(CardId.BlueEyesChaosMaxDragon))
                {
                    this.MaxDragon_count++;
                }

                if (check.IsCode(CardId.TheMelodyOfAwakeningDragon))
                {
                    this.TheMelody_count++;
                }
            }
        }        

        private bool MaxxCeff()
        {
            return this.Duel.Player == 1;
        }

        private bool CalledByTheGraveeff()
        {
            if(this.Duel.LastChainPlayer==1)
            {
                ClientCard lastCard = this.Util.GetLastChainCard();
                if (lastCard.IsCode(CardId.MaxxC))
                {
                    this.AI.SelectCard(CardId.MaxxC);
                    if(this.Util.ChainContainsCard(CardId.TheMelodyOfAwakeningDragon))
                    {
                        this.AI.SelectNextCard(CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesAlternativeWhiteDragon);
                    }

                    return this.UniqueFaceupSpell();
                }
                if (lastCard.IsCode(CardId.LockBird))
                {
                    this.AI.SelectCard(CardId.LockBird);
                    if (this.Util.ChainContainsCard(CardId.TheMelodyOfAwakeningDragon))
                    {
                        this.AI.SelectNextCard(CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesAlternativeWhiteDragon);
                    }

                    return this.UniqueFaceupSpell();
                }
                if (lastCard.IsCode(CardId.Ghost))
                {
                    this.AI.SelectCard(CardId.Ghost);
                    if (this.Util.ChainContainsCard(CardId.TheMelodyOfAwakeningDragon))
                    {
                        this.AI.SelectNextCard(CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesAlternativeWhiteDragon);
                    }

                    return this.UniqueFaceupSpell();
                }
                if (lastCard.IsCode(CardId.AshBlossom))
                {
                    this.AI.SelectCard(CardId.AshBlossom);
                    if (this.Util.ChainContainsCard(CardId.TheMelodyOfAwakeningDragon))
                    {
                        this.AI.SelectNextCard(CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesAlternativeWhiteDragon);
                    }

                    return this.UniqueFaceupSpell();
                }
            }
            return false;
        }
        private bool BlueEyesAlternativeWhiteDragoneff()
        {
            if(this.Card.Location==CardLocation.Hand)
            {
                if (this.Duel.Turn == 1)
                {
                    return false;
                }

                return true;
            }
            else
            {
                if(this.Util.GetProblematicEnemyMonster(3000,true)!=null)
                {
                    this.AI.SelectCard(this.Util.GetProblematicEnemyMonster(3000, true));
                    return true;
                }
            }
            return false;
        }

        private bool CreatureSwapeff()
        {
            if(this.Bot.HasInMonstersZone(CardId.BlueEyesChaosMaxDragon,true) && this.Duel.Phase==DuelPhase.Main1 &&
                (this.Bot.HasInMonstersZone(CardId.DeviritualTalismandra) || this.Bot.HasInMonstersZone(CardId.DevirrtualCandoll)))
            {
                this.AI.SelectCard(CardId.DevirrtualCandoll, CardId.DeviritualTalismandra);
                return true;
            }
            return false;
        }
        private bool TheMelodyOfAwakeningDragoneff()
        {
            this.Count_check();
            if(this.TheMelody_count >=2 && this.Bot.GetRemainingCount(CardId.BlueEyesChaosMaxDragon,3)>0)
            {
                this.AI.SelectCard(CardId.TheMelodyOfAwakeningDragon);
                this.AI.SelectNextCard(CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesAlternativeWhiteDragon);
                return true;
            }
            if(this.Bot.HasInHand(CardId.BlueEyesWhiteDragon) && this.Bot.GetRemainingCount(CardId.BlueEyesChaosMaxDragon, 3) > 0)
            {
                this.AI.SelectCard(CardId.BlueEyesWhiteDragon);
                this.AI.SelectNextCard(CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesAlternativeWhiteDragon);
                return true;
            }
            return false;
        }
        private bool TheMelodyOfAwakeningDragoneffsecond()
        {
            this.Count_check();
            if (this.RitualArtCanUse() && this.Bot.GetRemainingCount(CardId.BlueEyesChaosMaxDragon, 3) > 0 &&
                !this.Bot.HasInHand(CardId.BlueEyesChaosMaxDragon) && this.Bot.Hand.Count>=3)
            {
                if(this.RitualArt_count >=2)
                {
                    foreach (ClientCard m in this.Bot.Hand)
                    {
                        if (m.IsCode(CardId.AdvancedRitualArt))
                        {
                            this.AI.SelectCard(m);
                        }
                    }
                }
                foreach(ClientCard m in this.Bot.Hand)
                {
                    if (!m.IsCode(CardId.AdvancedRitualArt))
                    {
                        this.AI.SelectCard(m);
                    }
                }
                this.AI.SelectNextCard(CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesAlternativeWhiteDragon);
                return true;
            }            
            return false;
        }
        private bool TenTousandHandseff()
        {
            this.Count_check();
            if(this.Talismandra_count >=2 && this.Bot.GetRemainingCount(CardId.BlueEyesChaosMaxDragon,3)>0)
            {
                this.AI.SelectCard(CardId.BlueEyesChaosMaxDragon);
                return true;
            }
            if(this.Candoll_count >=2 || this.MaxDragon_count >= 2)
            {
                if(this.RitualArtCanUse() && this.Bot.GetRemainingCount(CardId.AdvancedRitualArt,3)>0)
                {
                    this.AI.SelectCard(CardId.AdvancedRitualArt);
                    return true;
                }
                if(this.ChaosFormCanUse() && this.Bot.GetRemainingCount(CardId.ChaosForm,1)>0)
                {
                    this.AI.SelectCard(CardId.ChaosForm);
                    return true;
                }
            }            
            if(this.RitualArt_count + this.ChaosForm_count >=2)
            {
                this.AI.SelectCard(CardId.BlueEyesChaosMaxDragon);
                return true;
            }
            if(this.Candoll_count + this.Talismandra_count >1)
            {
                if (this.MaxDragon_count >= 1)
                {
                    if (this.RitualArtCanUse() && this.Bot.GetRemainingCount(CardId.AdvancedRitualArt, 3) > 0)
                    {
                        this.AI.SelectCard(CardId.AdvancedRitualArt);
                        return true;
                    }
                    if (this.ChaosFormCanUse() && this.Bot.GetRemainingCount(CardId.ChaosForm, 1) > 0)
                    {
                        this.AI.SelectCard(CardId.ChaosForm);
                        return true;
                    }
                }
                if(this.Bot.HasInHand(CardId.AdvancedRitualArt) || this.Bot.HasInHand(CardId.ChaosForm))
                {
                    this.AI.SelectCard(CardId.BlueEyesChaosMaxDragon);
                    return true;
                }
            }
            if (this.ChaosForm_count >= 1)
            {
                if (this.RitualArtCanUse() && this.Bot.GetRemainingCount(CardId.AdvancedRitualArt, 3) > 0)
                {
                    this.AI.SelectCard(CardId.AdvancedRitualArt);
                    return true;
                }
                if (this.ChaosFormCanUse() && this.Bot.GetRemainingCount(CardId.ChaosForm, 1) > 0)
                {
                    this.AI.SelectCard(CardId.ChaosForm);
                    return true;
                }
            }
            if (this.Talismandra_count >=1)
            {
                this.AI.SelectCard(CardId.BlueEyesChaosMaxDragon);
                return true;
            }
            if(this.MaxDragon_count >=1)
            {
                if (this.RitualArtCanUse() && this.Bot.GetRemainingCount(CardId.AdvancedRitualArt, 3) > 0)
                {
                    this.AI.SelectCard(CardId.AdvancedRitualArt);
                    return true;
                }
                if (this.ChaosFormCanUse() && this.Bot.GetRemainingCount(CardId.ChaosForm, 1) > 0)
                {
                    this.AI.SelectCard(CardId.ChaosForm);
                    return true;
                }
            }            
            if (this.RitualArtCanUse() && this.Bot.GetRemainingCount(CardId.AdvancedRitualArt, 3) > 0)
            {
                this.AI.SelectCard(CardId.AdvancedRitualArt);                
            }
            if (this.ChaosFormCanUse() && this.Bot.GetRemainingCount(CardId.ChaosForm, 1) > 0)
            {
                this.AI.SelectCard(CardId.ChaosForm);                
            }
            return true;
        }

        private bool RitualArtCanUse()
        {
            return this.Bot.GetRemainingCount(CardId.BlueEyesWhiteDragon,2)>0;
        }

        private bool ChaosFormCanUse()
        {
            ClientCard check = null;
            foreach (ClientCard m in this.Bot.GetGraveyardMonsters())
            {
                if (m.IsCode(CardId.BlueEyesAlternativeWhiteDragon, CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesWhiteDragon))
                {
                    check = m;
                }
            }
            
            foreach (ClientCard m in this.Bot.Hand)
            {
                if (m.IsCode(CardId.BlueEyesWhiteDragon))
                {
                    check = m;
                }
            }
            if (check != null)
            {
                
                return true;
            }
            return false;
        }

        private bool DeviritualCheck()
        {
            this.Count_check();
            if(this.Card.IsCode(CardId.DeviritualTalismandra, CardId.DevirrtualCandoll))
            {
                if (this.Card.Location == CardLocation.MonsterZone)
                {
                    if(this.RitualArtCanUse())
                    {
                        this.AI.SelectCard(CardId.AdvancedRitualArt);                        
                    }
                    else
                    {
                        this.AI.SelectCard(CardId.ChaosForm);
                    }
                    return true;
                }
                if(this.Card.Location==CardLocation.Hand)
                {                    
                    if(this.Card.IsCode(CardId.DevirrtualCandoll))
                    {
                        if (this.MaxDragon_count >= 2 && this.Talismandra_count >= 1 || this.Candoll_used)
                        {
                            return false;
                        }
                    }
                    if(this.Card.IsCode(CardId.DeviritualTalismandra))
                    {
                        if (this.RitualArt_count + this.ChaosForm_count >= 2 && this.Candoll_count >= 1 || this.Talismandra_used)
                        {
                            return false;
                        }

                        this.Talismandra_used = true;
                        return true;
                    }
                    if(this.RitualArtCanUse())
                    {
                        this.Candoll_used = true;
                        this.AI.SelectCard(CardId.AdvancedRitualArt);
                        return true;
                    }
                    if (this.ChaosFormCanUse())
                    {
                        this.Candoll_used = true;
                        this.AI.SelectCard(CardId.ChaosForm);
                        return true;
                    }
                    return true;
                }
            }
            return false;

        }
        private bool ChaosFormeff()
        {
            ClientCard check = null;
            foreach(ClientCard m in this.Bot.Graveyard)
            {
                if (m.IsCode(CardId.BlueEyesAlternativeWhiteDragon, CardId.BlueEyesChaosMaxDragon, CardId.BlueEyesWhiteDragon))
                {
                    check = m;
                }
            }
            
            if(check != null)
            {
                this.AI.SelectCard(CardId.BlueEyesChaosMaxDragon);
                this.AI.SelectNextCard(check);
                return true;
            }
            foreach(ClientCard m in this.Bot.Hand)
            {
                if (m.IsCode(CardId.BlueEyesWhiteDragon))
                {
                    check = m;
                }
            }           
            if (check != null)
            {
                this.AI.SelectCard(CardId.BlueEyesChaosMaxDragon);
                this.AI.SelectNextCard(check);
                return true;
            }
            return false;
        }
        private bool MissusRadiantsp()
        {
            IList<ClientCard> material_list = new List<ClientCard>();
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.HasAttribute(CardAttribute.Earth) && monster.Level == 1)
                {
                    material_list.Add(monster);
                }

                if (material_list.Count == 2)
                {
                    break;
                }
            }
            if (material_list.Count < 2)
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.MissusRadiant))
            {
                return false;
            }

            this.AI.SelectMaterials(material_list);
            if (this.Bot.MonsterZone[0] == null && this.Bot.MonsterZone[2] == null && this.Bot.MonsterZone[5] == null)
            {
                this.AI.SelectPlace(Zones.ExtraZone1);
            }
            else
            {
                this.AI.SelectPlace(Zones.ExtraZone2);
            }

            return true;
        }

        private bool MissusRadianteff()
        {
            this.AI.SelectCard(CardId.MaxxC, CardId.MissusRadiant);
            return true;
        }

        private bool Linkuribohsp()
        {
            foreach (ClientCard c in this.Bot.GetMonsters())
            {
                if (!c.IsCode(CardId.Linkuriboh) && c.Level == 1)
                {
                    this.AI.SelectMaterials(c);
                    return true;
                }
            }
            return false;
        }

        private bool Linkuriboheff()
        {
            if (this.Duel.LastChainPlayer == 0 && this.Util.GetLastChainCard().IsCode(CardId.Linkuriboh))
            {
                return false;
            }

            return true;
        }
        private bool BirrelswordDragonsp()
        {
           
            IList<ClientCard> material_list = new List<ClientCard>();
            foreach (ClientCard m in this.Bot.GetMonsters())
            {
                if (m.IsCode(CardId.MissusRadiant))
                {
                    material_list.Add(m);
                    break;
                }
            }
            foreach (ClientCard m in this.Bot.GetMonsters())
            {
                if (m.IsCode(CardId.Linkuriboh) || m.Level==1)
                {
                    material_list.Add(m);
                    if (material_list.Count == 3)
                    {
                        break;
                    }
                }
            }
            if (material_list.Count == 3)
            {
                this.AI.SelectMaterials(material_list);
                return true;
            }
            return false;
        }

        private bool BirrelswordDragoneff()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.BirrelswordDragon, 0))
            {                
                if (this.Util.IsChainTarget(this.Card) && this.Util.GetBestEnemyMonster(true, true) != null)
                {
                    this.AI.SelectCard(this.Util.GetBestEnemyMonster(true, true));
                    return true;
                }
                if (this.Duel.Player == 1 && this.Bot.BattlingMonster == this.Card)
                {
                    this.AI.SelectCard(this.Enemy.BattlingMonster);
                    return true;
                }
                if (this.Duel.Player == 1 && this.Bot.BattlingMonster != null &&
                    (this.Enemy.BattlingMonster.Attack - this.Bot.BattlingMonster.Attack) >= this.Bot.LifePoints)
                {
                    this.AI.SelectCard(this.Enemy.BattlingMonster);
                    return true;
                }
                if (this.Duel.Player == 0 && this.Duel.Phase == DuelPhase.BattleStart)
                {
                    foreach (ClientCard check in this.Enemy.GetMonsters())
                    {
                        if (check.IsAttack())
                        {
                            this.AI.SelectCard(check);
                            return true;
                        }
                    }
                }
                return false;
            }
            return true;
        }
        private bool SpellSet()
        {
            if (this.Card.IsCode(CardId.InfiniteImpermanence))
            {
                return !this.Bot.IsFieldEmpty();
            }

            if (this.Card.IsCode(CardId.RecklessGreed))
            {
                return true;
            }

            if (this.Card.IsCode(CardId.Scapegoat))
            {
                return true;
            }

            return false;
        }

        private bool RecklessGreedeff()
        {           
            int count = 0;
            foreach (ClientCard card in this.Bot.GetSpells())
            {
                if (card.IsCode(CardId.RecklessGreed))
                {
                    count++;
                }
            }            
            if (this.DefaultOnBecomeTarget())
            {
                return true;
            }

            if (this.Duel.Player==0 && this.Duel.Phase>=DuelPhase.Main1)
            {
                if (this.Bot.LifePoints <= 4000 || count>=2)
                {
                    return true;
                }
            }
            return false;
        }

        private bool Scapegoateff()
        {            
            if (this.Duel.Player == 0)
            {
                return false;
            }

            if (this.Duel.Phase == DuelPhase.End)
            {
                return true;
            }

            if (this.Duel.LastChainPlayer == 1 && this.DefaultOnBecomeTarget())
            {
                return true;
            }

            if (this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2)
            {
                int total_atk = 0;
                List<ClientCard> enemy_monster = this.Enemy.GetMonsters();
                foreach (ClientCard m in enemy_monster)
                {
                    if (m.IsAttack() && !m.Attacked)
                    {
                        total_atk += m.Attack;
                    }
                }
                if (total_atk >= this.Bot.LifePoints)
                {
                    return true;
                }
            }
            return false;
        }
        public override ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
        {           
            for (int i = 0; i < attackers.Count; ++i)
            {                
                ClientCard attacker = attackers[i];
                if (attacker.IsCode(CardId.BlueEyesChaosMaxDragon))
                {
                    Logger.DebugWriteLine(attacker.Name);
                    return attacker;
                }               
            }
            return base.OnSelectAttacker(attackers, defenders);
        }
        public override BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
        {
            if(attacker.IsCode(CardId.BlueEyesChaosMaxDragon) && !attacker.IsDisabled() &&
                this.Enemy.HasInMonstersZone(new[] {CardId.DeviritualTalismandra,CardId.DevirrtualCandoll }))
            {              
                for (int i = 0; i < defenders.Count; i++)
                {
                    ClientCard defender = defenders[i];                    
                    attacker.RealPower = attacker.Attack;
                    defender.RealPower = defender.GetDefensePower();
                    if (!this.OnPreBattleBetween(attacker, defender))
                    {
                        continue;
                    }

                    if (defender.IsCode(CardId.DevirrtualCandoll, CardId.DeviritualTalismandra))
                    {
                        return this.AI.Attack(attacker, defender);                                          
                    }                   
                }                
            }
            return base.OnSelectAttackTarget(attacker, defenders);
        }
        public override bool OnSelectHand()
        {
            return false;
        }

    }
}
