using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("Phantasm", "AI_Phantasm")]
    public class PhantasmExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int MegalosmasherX = 81823360;
            public const int AshBlossom = 14558127;
            public const int EaterOfMillions = 63845230;

            public const int HarpieFeatherDuster = 18144506;
            public const int PotOfDesires = 35261759;
            public const int FossilDig = 47325505;
            public const int CardOfDemise = 59750328;
            public const int Terraforming = 73628505;
            public const int PotOfDuality = 98645731;
            public const int Scapegoat = 73915051;
            public const int PacifisThePhantasmCity = 2819435;

            public const int InfiniteImpermanence = 10045474;
            public const int PhantasmSprialBattle = 34302287;
            public const int DrowningMirrorForce = 47475363;
            public const int StarlightRoad = 58120309;
            public const int PhantasmSpiralPower = 61397885;
            public const int Metaverse = 89208725;
            public const int SeaStealthAttack = 19089195;
            public const int GozenMatch = 53334471;
            public const int SkillDrain = 82732705;
            public const int TheHugeRevolutionIsOver = 99188141;

            public const int StardustDragon = 44508094;
            public const int TopologicBomberDragon = 5821478;
            public const int BorreloadDragon = 31833038;
            public const int BorrelswordDragon = 85289965;
            public const int KnightmareGryphon = 65330383;
            public const int TopologicTrisbaena = 72529749;
            public const int SummonSorceress = 61665245;
            public const int KnightmareUnicorn = 38342335;
            public const int KnightmarePhoenix = 2857636;
            public const int KnightmareCerberus = 75452921;
            public const int CrystronNeedlefiber = 50588353;
            public const int MissusRadiant = 3987233;
            public const int LinkSpider = 98978921;
            public const int Linkuriboh = 41999284;

            public const int ElShaddollWinda = 94977269;
            public const int BrandishSkillJammingWave = 25955749;
            public const int BrandishSkillAfterburner = 99550630;
            public const int EternalSoul = 48680970;
            public const int SuperboltThunderDragon = 15291624;
        }

        public PhantasmExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            //counter
            this.AddExecutor(ExecutorType.GoToBattlePhase, this.GoToBattlePhase);
            this.AddExecutor(ExecutorType.Activate, CardId.StarlightRoad, this.PreventFeatherDustereff);
            this.AddExecutor(ExecutorType.Activate, CardId.TheHugeRevolutionIsOver, this.PreventFeatherDustereff);
            this.AddExecutor(ExecutorType.Activate, DefaultExecutor.CardId.GhostBelle, this.DefaultGhostBelleAndHauntedMansion);
            this.AddExecutor(ExecutorType.Activate, DefaultExecutor.CardId.CalledByTheGrave, this.DefaultCalledByTheGrave);
            this.AddExecutor(ExecutorType.Activate, DefaultExecutor.CardId.EffectVeiler, this.DefaultEffectVeiler);
            this.AddExecutor(ExecutorType.Activate, DefaultExecutor.CardId.InfiniteImpermanence, this.DefaultInfiniteImpermanence);
            this.AddExecutor(ExecutorType.Activate, DefaultExecutor.CardId.AshBlossom, this.DefaultAshBlossomAndJoyousSpring);
            this.AddExecutor(ExecutorType.Activate, DefaultExecutor.CardId.GhostOgreAndSnowRabbit, this.DefaultGhostOgreAndSnowRabbit);

            //trap activate  
            this.AddExecutor(ExecutorType.Activate, CardId.SeaStealthAttack, this.SeaStealthAttackeff);
            this.AddExecutor(ExecutorType.Activate, CardId.PhantasmSprialBattle, this.PhantasmSprialBattleeff);
            this.AddExecutor(ExecutorType.Activate, CardId.PhantasmSpiralPower, this.PhantasmSpiralPowereff);
            this.AddExecutor(ExecutorType.Activate, CardId.DrowningMirrorForce, this.DrowningMirrorForceeff);
            this.AddExecutor(ExecutorType.Activate, CardId.GozenMatch, this.GozenMatcheff);
            this.AddExecutor(ExecutorType.Activate, CardId.SkillDrain, this.SkillDraineff);
            this.AddExecutor(ExecutorType.Activate, CardId.Metaverse, this.Metaverseeff);
            //sp
            this.AddExecutor(ExecutorType.SpSummon, CardId.BorrelswordDragon, this.BorrelswordDragonsp);
            this.AddExecutor(ExecutorType.Activate, CardId.BorrelswordDragon, this.BorrelswordDragoneff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.MissusRadiant, this.MissusRadiantsp);
            this.AddExecutor(ExecutorType.Activate, CardId.MissusRadiant, this.MissusRadianteff);
            this.AddExecutor(ExecutorType.Activate, CardId.Linkuriboh, this.Linkuriboheff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Linkuriboh, this.Linkuribohsp);
            //first           
            this.AddExecutor(ExecutorType.Activate, CardId.HarpieFeatherDuster, this.DefaultHarpiesFeatherDusterFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.FossilDig, this.FossilDigeff);
            this.AddExecutor(ExecutorType.Activate, CardId.Terraforming, this.Terraformingeff);
            this.AddExecutor(ExecutorType.Activate, CardId.PotOfDuality, this.PotOfDualityeff);
            this.AddExecutor(ExecutorType.Activate, CardId.PotOfDesires, this.PotOfDesireseff);
            this.AddExecutor(ExecutorType.Activate, CardId.PacifisThePhantasmCity, this.PacifisThePhantasmCityeff);
            //summon
            this.AddExecutor(ExecutorType.Summon, CardId.MegalosmasherX, this.MegalosmasherXsummon);
            //sp
            this.AddExecutor(ExecutorType.SpSummon, CardId.EaterOfMillions, this.EaterOfMillionssp);
            this.AddExecutor(ExecutorType.Activate, CardId.EaterOfMillions, this.EaterOfMillionseff);
            //other

            this.AddExecutor(ExecutorType.Activate, CardId.Scapegoat, this.DefaultScapegoat);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SeaStealthAttack, this.NoSetAlreadyDone);
            this.AddExecutor(ExecutorType.SpellSet, CardId.StarlightRoad, this.StarlightRoadset);
            this.AddExecutor(ExecutorType.SpellSet, CardId.TheHugeRevolutionIsOver, this.TheHugeRevolutionIsOverset);
            this.AddExecutor(ExecutorType.SpellSet, CardId.DrowningMirrorForce);
            this.AddExecutor(ExecutorType.SpellSet, CardId.InfiniteImpermanence, this.InfiniteImpermanenceset);
            this.AddExecutor(ExecutorType.SpellSet, CardId.Scapegoat, this.NoSetAlreadyDone);
            this.AddExecutor(ExecutorType.SpellSet, CardId.GozenMatch, this.NoSetAlreadyDone);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SkillDrain, this.NoSetAlreadyDone);
            this.AddExecutor(ExecutorType.SpellSet, CardId.Metaverse);
            this.AddExecutor(ExecutorType.SpellSet, this.SpellSeteff);
            this.AddExecutor(ExecutorType.Activate, CardId.CardOfDemise, this.CardOfDemiseeff);
            this.AddExecutor(ExecutorType.Repos, this.MonsterRepos);
        }
        bool summon_used = false;
        bool CardOfDemiseeff_used = false;
        bool SeaStealthAttackeff_used = false;
        int City_count = 0;
        public override void OnNewTurn()
        {
            this.summon_used = false;
            this.CardOfDemiseeff_used = false;
            this.SeaStealthAttackeff_used = false;
            this.City_count = 0;
            base.OnNewTurn();
        }
        private bool PreventFeatherDustereff()
        {
            return this.Duel.LastChainPlayer == 1;          
        }

        private bool GoToBattlePhase()
        {           
            if (this.Enemy.GetMonsterCount() == 0)
            {
                if (this.Util.GetTotalAttackingMonsterAttack(0) >= this.Enemy.LifePoints)
                {                   
                    return true;
                }
            }
            return false;
        }

        private bool PhantasmSprialBattleeff()
        {
            if (this.DefaultOnBecomeTarget() && this.Card.Location==CardLocation.SpellZone)
            {
                this.AI.SelectCard(this.Util.GetBestEnemyCard(false,true));
                return true;
            }
            if(this.Enemy.HasInSpellZone(CardId.EternalSoul))
            {
                this.AI.SelectCard(CardId.EternalSoul);
                return this.UniqueFaceupSpell();
            }
            if(this.Bot.UnderAttack && this.Bot.BattlingMonster != null && this.Bot.BattlingMonster.IsCode(CardId.MegalosmasherX))
            {
                this.AI.SelectCard(this.Enemy.BattlingMonster);
                return this.UniqueFaceupSpell();
            }
            if (this.Bot.GetMonsterCount() > 0 && !this.Bot.HasInSpellZone(CardId.SeaStealthAttack) &&
                this.Util.IsOneEnemyBetterThanValue(2000, false) && this.Duel.Phase==DuelPhase.BattleStart)
            {
                this.AI.SelectCard(this.Util.GetBestEnemyMonster(true,true));
                return this.UniqueFaceupSpell();
            }
            if (this.Util.GetProblematicEnemyCard(9999,true)!=null)
            {
                if (this.Util.GetProblematicEnemyCard(9999, true).IsCode(CardId.ElShaddollWinda) &&
                    !this.Util.GetProblematicEnemyCard(9999, true).IsDisabled())
                {
                    return false;
                }

                this.AI.SelectCard(this.Util.GetProblematicEnemyCard(9999, true));                
                return this.UniqueFaceupSpell();
            }
            return false;
        }

        private bool PhantasmSpiralPowereff()
        {
            if (this.DefaultOnBecomeTarget() && this.Card.Location == CardLocation.SpellZone)
            {
                return true;
            }

            if (this.Duel.Player == 0 || (this.Duel.Player==1 && this.Bot.BattlingMonster!=null))
            {
                if(this.Enemy.HasInMonstersZone(CardId.ElShaddollWinda))
                {
                    this.AI.SelectCard(CardId.ElShaddollWinda);
                    return this.UniqueFaceupSpell();
                }
                if(this.Enemy.HasInMonstersZone(CardId.SuperboltThunderDragon))
                {
                    this.AI.SelectCard(CardId.SuperboltThunderDragon);
                    return this.UniqueFaceupSpell();
                }
            }            
            return this.DefaultInfiniteImpermanence() && this.UniqueFaceupSpell();
        }

        private bool DrowningMirrorForceeff()
        {
            int count = 0;
            foreach(ClientCard m in this.Enemy.GetMonsters())
            {
                if (m.IsAttack())
                {
                    count++;
                }
            }
            if (this.Util.GetTotalAttackingMonsterAttack(1) >= this.Bot.LifePoints)
            {
                return true;
            }

            return count >= 2;
        }

        private bool GozenMatcheff()
        {
            if (this.Bot.GetMonsterCount() >= 4 || this.Bot.HasInSpellZone(CardId.Scapegoat))
            {
                return false;
            }

            if (this.DefaultOnBecomeTarget())
            {
                return true;
            }

            int dark_count = 0;
            int Divine_count = 0;
            int Earth_count = 0;
            int Fire_count = 0;
            int Light_count = 0;
            int Water_count = 0;
            int Wind_count = 0;
            foreach (ClientCard m in this.Enemy.GetMonsters())
            {
                if (m.HasAttribute(CardAttribute.Dark))
                {
                    dark_count++;
                }

                if (m.HasAttribute(CardAttribute.Divine))
                {
                    Divine_count++;
                }

                if (m.HasAttribute(CardAttribute.Earth))
                {
                    Earth_count++;
                }

                if (m.HasAttribute(CardAttribute.Fire))
                {
                    Fire_count++;
                }

                if (m.HasAttribute(CardAttribute.Light))
                {
                    Light_count++;
                }

                if (m.HasAttribute(CardAttribute.Water))
                {
                    Water_count++;
                }

                if (m.HasAttribute(CardAttribute.Wind))
                {
                    Wind_count++;
                }
            }
            if (dark_count > 1)
            {
                dark_count = 1;
            }

            if (Divine_count > 1)
            {
                Divine_count = 1;
            }

            if (Earth_count > 1)
            {
                Earth_count = 1;
            }

            if (Fire_count > 1)
            {
                Fire_count = 1;
            }

            if (Light_count > 1)
            {
                Light_count = 1;
            }

            if (Water_count > 1)
            {
                Water_count = 1;
            }

            if (Wind_count > 1)
            {
                Wind_count = 1;
            }

            return ((dark_count + Divine_count + Earth_count + Fire_count + Light_count + Water_count + Wind_count) >= 2 && this.UniqueFaceupSpell());
        }

        private bool SkillDraineff()
        {
            if (this.Duel.LastChainPlayer == 1 && this.Util.GetLastChainCard().Location == CardLocation.MonsterZone)
            {
                return this.UniqueFaceupSpell();
            }

            return false;
        }

        private bool Metaverseeff()
        {
            if (this.Duel.LastChainPlayer == 0)
            {
                return false;
            }

            if (!this.Bot.HasInSpellZone(CardId.PacifisThePhantasmCity))
            {
                this.AI.SelectOption(1);
                return this.UniqueFaceupSpell();
            }
            else
            {
                this.AI.SelectOption(0);
                return this.UniqueFaceupSpell();                    
            }
        }

        private bool CardOfDemiseeff()
        {
            if (this.DefaultSpellWillBeNegated())
            {
                return false;
            }

            this.AI.SelectPlace(Zones.MonsterZone3);
            if(this.Card.Location==CardLocation.Hand)
            {
                if (this.Bot.Hand.Count <= 1 && this.Bot.GetSpellCountWithoutField() <= 3)
                {
                    this.CardOfDemiseeff_used = true;
                    return true;
                }
            }
            else
            {
                if (this.Bot.Hand.Count <= 1 && this.Bot.GetSpellCountWithoutField() <= 4)
                {
                    this.CardOfDemiseeff_used = true;
                    return true;
                }
            }
            return false;
        }

        private bool FossilDigeff()
        {
            if (this.DefaultSpellWillBeNegated())
            {
                return false;
            }

            if (this.CardOfDemiseeff_used && this.summon_used)
            {
                return false;
            }

            return true;
        }

        private bool PotOfDualityeff()
        {
            if(!this.Bot.HasInHandOrInSpellZone(CardId.PacifisThePhantasmCity) &&
                !this.Bot.HasInHandOrInSpellZone(CardId.Metaverse))
            {
                if(this.Bot.HasInGraveyard(CardId.PacifisThePhantasmCity) && !this.Bot.HasInHandOrInSpellZone(CardId.SeaStealthAttack))
                {
                    this.AI.SelectCard(
                        CardId.SeaStealthAttack,
                        CardId.PacifisThePhantasmCity,
                        CardId.Terraforming,
                        CardId.Metaverse,
                        CardId.CardOfDemise,
                        CardId.Scapegoat
                        );
                }
                else
                {
                    this.AI.SelectCard(
                        CardId.PacifisThePhantasmCity,
                        CardId.Terraforming,
                        CardId.Metaverse,
                        CardId.CardOfDemise,
                        CardId.Scapegoat
                        );
                }
                
            }
            else if(!this.Bot.HasInHandOrInSpellZone(CardId.SeaStealthAttack))
            {
                this.AI.SelectCard(
                    CardId.SeaStealthAttack,
                    CardId.CardOfDemise,
                    CardId.PotOfDesires,
                    CardId.Scapegoat
                    );
            }
            else
            {
                this.AI.SelectCard(
                    CardId.CardOfDemise,
                    CardId.PotOfDesires,
                    CardId.Scapegoat
                    );
            }
            return true;
        }
        private bool Terraformingeff()
        {
            if (this.DefaultSpellWillBeNegated())
            {
                return false;
            }

            if (this.CardOfDemiseeff_used && this.Bot.HasInSpellZone(CardId.PacifisThePhantasmCity))
            {
                return false;
            }

            return true;
        }
        
        private bool PacifisThePhantasmCityeff()
        {
            if (this.DefaultSpellWillBeNegated())
            {
                return false;
            }

            if (this.Card.Location==CardLocation.Hand)
            {
                if (this.Bot.HasInSpellZone(CardId.PacifisThePhantasmCity))
                {
                    return false;
                }

                return true;
            }
            else
            {
                if (this.City_count > 10)
                {
                    return false;
                }

                ClientCard target = null;
                foreach(ClientCard s in this.Bot.GetSpells())
                {
                    if(s.IsCode(CardId.SeaStealthAttack) && this.Card.IsFaceup())
                    {
                        target = s;
                        break;
                    }
                }
                foreach(ClientCard m in this.Bot.GetMonsters())
                {
                    if(m.HasAttribute(CardAttribute.Water))
                    {
                        if (target != null && !this.SeaStealthAttackeff_used)
                        {
                            if (this.Util.IsChainTarget(this.Card) || this.Util.IsChainTarget(target))
                            {
                                return false;
                            }
                        }
                        break;
                    }
                }
                this.City_count++;
                this.AI.SelectPlace(Zones.MonsterZone2 | Zones.MonsterZone4);
                this.AI.SelectCard(CardId.PhantasmSprialBattle);
                return true;
            }
        }

        private bool MegalosmasherXsummon()
        {
            this.AI.SelectPlace(Zones.MonsterZone2 | Zones.MonsterZone4);
            this.summon_used = true;
            return true;
        }

        private bool BorrelswordDragonsp()
        {
           
            if (!this.Bot.HasInMonstersZone(CardId.MissusRadiant))
            {
                return false;
            }

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
                if (m.IsCode(CardId.Linkuriboh, CardId.LinkSpider))
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

        private bool BorrelswordDragoneff()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.BorrelswordDragon, 0))
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
                        if (check.IsAttack() && !check.HasType(CardType.Link))
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
        private bool EaterOfMillionssp()
        {
            if (this.Bot.MonsterZone[1] == null)
            {
                this.AI.SelectPlace(Zones.MonsterZone2);
            }
            else
            {
                this.AI.SelectPlace(Zones.MonsterZone4);
            }

            if (this.Enemy.HasInMonstersZone(CardId.KnightmareGryphon, true))
            {
                return false;
            }

            if (this.Util.GetProblematicEnemyMonster() == null && this.Bot.ExtraDeck.Count < 5)
            {
                return false;
            }

            if (this.Bot.GetMonstersInMainZone().Count >= 5)
            {
                return false;
            }

            if (this.Util.IsTurn1OrMain2())
            {
                return false;
            }

            this.AI.SelectPosition(CardPosition.FaceUpAttack);
            IList<ClientCard> material_list = new List<ClientCard>();
            if(this.Bot.HasInExtra(CardId.BorreloadDragon))
            {
                this.AI.SelectCard(
                    CardId.TopologicBomberDragon,
                    CardId.TopologicTrisbaena,
                    CardId.KnightmareGryphon,
                    CardId.SummonSorceress,
                    CardId.BorreloadDragon
                    );
            }
            else 
            {               
                foreach(ClientCard m in this.Bot.ExtraDeck)
                {
                    if (material_list.Count == 5)
                    {
                        break;
                    }

                    material_list.Add(m);
                }
            }
            return true;
        }

        private bool EaterOfMillionseff()
        {
            if (this.Enemy.BattlingMonster.HasPosition(CardPosition.Attack) && (this.Bot.BattlingMonster.Attack - this.Enemy.BattlingMonster.GetDefensePower() >= this.Enemy.LifePoints))
            {
                return false;
            }

            return true;
        }

        private bool MissusRadiantsp()
        {
            IList<ClientCard> material_list = new List<ClientCard>();
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.HasAttribute(CardAttribute.Earth) && monster.Level == 1 && !monster.IsCode(CardId.EaterOfMillions))
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
            if ((this.Bot.MonsterZone[0] == null || this.Bot.MonsterZone[0].Level==1) &&
                (this.Bot.MonsterZone[2] == null || this.Bot.MonsterZone[2].Level == 1)&&
                this.Bot.MonsterZone[5] == null)
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
            this.AI.SelectCard(new[]
           {              
                CardId.MissusRadiant,
            });
            return true;
        }

        private bool Linkuribohsp()
        {
            foreach (ClientCard c in this.Bot.GetMonsters())
            {
                if (!c.IsCode(CardId.EaterOfMillions, CardId.Linkuriboh) && c.Level == 1)
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
        private bool SeaStealthAttackeff()
        {            
            if (this.DefaultOnBecomeTarget())
            {
                this.AI.SelectCard(CardId.MegalosmasherX);
                this.SeaStealthAttackeff_used = true;
                return true;
            }
            if ((this.Card.IsFacedown() && this.Bot.HasInHandOrInSpellZoneOrInGraveyard(CardId.PacifisThePhantasmCity)))
            {
                if (!this.Bot.HasInSpellZone(CardId.PacifisThePhantasmCity))
                {
                    if(this.Bot.HasInGraveyard(CardId.PacifisThePhantasmCity))
                    {
                        foreach (ClientCard s in this.Bot.GetGraveyardSpells())
                        {
                            if (s.IsCode(CardId.PacifisThePhantasmCity))
                            {
                                this.AI.SelectYesNo(true);
                                this.AI.SelectCard(s);
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (ClientCard s in this.Bot.Hand)
                        {
                            if (s.IsCode(CardId.PacifisThePhantasmCity))
                            {
                                this.AI.SelectYesNo(true);
                                this.AI.SelectCard(s);
                                break;
                            }
                        }
                    }

                }
                else
                {
                    this.AI.SelectYesNo(false);
                }

                return this.UniqueFaceupSpell();
            }
            else if(this.Card.IsFaceup())
            {
                ClientCard target = null;
                foreach(ClientCard s in this.Bot.GetSpells())
                {
                    if (s.IsCode(CardId.PacifisThePhantasmCity))
                    {
                        target = s;
                    }
                }
                if (target != null && this.Util.IsChainTarget(target))
                {
                    this.SeaStealthAttackeff_used = true;
                    return true;
                }                
                target = this.Util.GetLastChainCard();
                if(target!=null)
                {
                    if(target.IsCode(CardId.BrandishSkillAfterburner))
                    {
                        this.AI.SelectCard(CardId.MegalosmasherX);
                        this.SeaStealthAttackeff_used = true;
                        return true;
                    }
                    if(this.Enemy.GetGraveyardSpells().Count>=3 && target.IsCode(CardId.BrandishSkillJammingWave))
                    {
                        this.AI.SelectCard(CardId.MegalosmasherX);
                        this.SeaStealthAttackeff_used = true;
                        return true;
                    }
                }
            }
            return false;
        }   

        private bool PotOfDesireseff()
        {
            return this.Bot.Deck.Count >= 18;
        }       
       
        private bool StarlightRoadset()
        {
            if (this.Duel.Turn > 1 && this.Duel.Phase == DuelPhase.Main1 && this.Bot.HasAttackingMonster())
            {
                return false;
            }

            if (this.Bot.HasInSpellZone(CardId.TheHugeRevolutionIsOver))
            {
                return false;
            }

            return true;
        }

        private bool TheHugeRevolutionIsOverset()
        {
            if (this.Duel.Turn > 1 && this.Duel.Phase == DuelPhase.Main1 && this.Bot.HasAttackingMonster())
            {
                return false;
            }

            if (this.Bot.HasInSpellZone(CardId.StarlightRoad))
            {
                return false;
            }

            return true;
        }

        private bool InfiniteImpermanenceset()
        {
            return !this.Bot.IsFieldEmpty();
        }
        
        private bool NoSetAlreadyDone()
        {
            if (this.Duel.Turn > 1 && this.Duel.Phase == DuelPhase.Main1 && this.Bot.HasAttackingMonster())
            {
                return false;
            }

            if (this.Bot.HasInSpellZone(this.Card.Id))
            {
                return false;
            }

            return true;
        }

        private bool SpellSeteff()
        { 
            if (this.Card.HasType(CardType.Field))
            {
                return false;
            }

            if (this.CardOfDemiseeff_used)
            {
                return true;
            }

            if (this.Bot.HasInHandOrInSpellZone(CardId.CardOfDemise) && !this.CardOfDemiseeff_used)
            {
                int hand_spell_count = 0;
                foreach(ClientCard s in this.Bot.Hand)
                {
                    if (s.HasType(CardType.Trap) || s.HasType(CardType.Spell) && !s.HasType(CardType.Field))
                    {
                        hand_spell_count++;
                    }
                }
                int zone_count = 5 - this.Bot.GetSpellCountWithoutField();
                return zone_count- hand_spell_count >= 1;
            }
            if(this.Card.IsCode(CardId.PhantasmSprialBattle, CardId.PhantasmSpiralPower))
            {
                if (this.Bot.HasInMonstersZone(CardId.MegalosmasherX) &&
                    !this.Bot.HasInHandOrInSpellZone(CardId.PacifisThePhantasmCity) &&
                    !this.Bot.HasInHandOrInSpellZone(CardId.Metaverse))
                {
                    return true;
                }
            }
            return false;
        }

        private bool MonsterRepos()
        {
            if (this.Card.Level >= 5)
            {
                foreach (ClientCard s in this.Bot.GetSpells())
                {
                    if (s.IsFaceup() && s.IsCode(CardId.SeaStealthAttack) &&
                        this.Bot.HasInSpellZone(CardId.PacifisThePhantasmCity) &&
                        this.Card.IsAttack())
                    {
                        return false;
                    }
                }
            }
            if (this.Card.IsCode(CardId.EaterOfMillions) && !this.Card.IsDisabled() && this.Card.IsAttack())
            {
                return false;
            }

            return this.DefaultMonsterRepos();
        }

        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if(attacker.IsCode(CardId.PacifisThePhantasmCity+1) && defender.IsCode(CardId.EaterOfMillions))
            {
                if (attacker.RealPower >= defender.RealPower)
                {
                    return true;
                }
            }
            if(attacker.Level>=5)
            {
                foreach(ClientCard s in this.Bot.GetSpells())
                {
                    if (s.IsFaceup() && s.IsCode(CardId.SeaStealthAttack) && this.Bot.HasInSpellZone(CardId.PacifisThePhantasmCity))
                    { 
                        attacker.RealPower = 9999;
                        if (defender.IsCode(CardId.EaterOfMillions))
                        {
                            return true;
                        }
                    }
                       
                }
            }
            return base.OnPreBattleBetween(attacker, defender);
        }

        public override ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
        {
            for (int i = 0; i < attackers.Count; ++i)
            {
                ClientCard attacker = attackers[i];
                if (attacker.IsCode(CardId.EaterOfMillions))
                {
                    return attacker;
                }
            }
            return null;
        }

        public override bool OnSelectHand()
        {
            return true;
        }

        
    }
}
