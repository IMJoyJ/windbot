using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("DarkMagician", "AI_DarkMagician")]
    public class DarkMagicianExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int DarkMagician = 46986414;
            public const int GrinderGolem = 75732622;
            public const int MagicianOfLllusion = 35191415;
            public const int ApprenticeLllusionMagician = 30603688;
            public const int WindwitchGlassBell = 71007216;
            public const int MagiciansRod = 7084129;
            public const int WindwitchIceBell = 43722862;
            public const int AshBlossom = 14558127;
            public const int SpellbookMagicianOfProphecy = 14824019;
            public const int MaxxC = 23434538;
            public const int WindwitchSnowBell = 70117860;

            public const int TheEyeOfTimaeus = 1784686;
            public const int DarkMagicAttack = 2314238;
            public const int SpellbookOfKnowledge = 23314220;
            public const int UpstartGoblin = 70368879;
            public const int SpellbookOfSecrets = 89739383;
            public const int DarkMagicInheritance = 41735184;
            public const int LllusionMagic = 73616671;
            //public const int Scapegoat = 73915051;
            public const int DarkMagicalCircle = 47222536;
            public const int WonderWand = 67775894;
            public const int MagicianNavigation = 7922915;
            public const int EternalSoul = 48680970;
            public const int SolemnStrike = 40605147;

            public const int DarkMagicianTheDragonKnight = 41721210;
            public const int CrystalWingSynchroDragon = 50954680;
            public const int OddEyesWingDragon = 58074177;
            public const int ClearWingFastDragon = 90036274;
            public const int WindwitchWinterBell = 14577226;
            public const int OddEyesAbsoluteDragon = 16691074;
            public const int Dracossack = 22110647;
            public const int BigEye = 80117527;
            public const int TroymarePhoenix = 2857636;
            public const int TroymareCerberus = 75452921;
            public const int ApprenticeWitchling = 71384012;
            public const int VentriloauistsClaraAndLucika = 1482001;
            /*public const int EbonLllusionMagician = 96471335;
            public const int BorreloadDragon = 31833038;
            public const int SaryujaSkullDread = 74997493;
            public const int Hidaruma = 64514892;
            public const int AkashicMagician = 28776350;
            public const int SecurityDragon = 99111753;
            public const int LinkSpider = 98978921;
            public const int Linkuriboh = 41999284;*/

            public const int HarpiesFeatherDuster = 18144506;
            public const int ElShaddollWinda = 94977269;
            public const int DarkHole = 53129443;
            public const int Ultimate = 86221741;
            public const int LockBird = 94145021;
            public const int Ghost = 59438930;
            public const int GiantRex = 80280944;
            public const int UltimateConductorTytanno = 18940556;
            public const int SummonSorceress = 61665245;
            public const int CrystronNeedlefiber = 50588353;
            public const int FirewallDragon = 5043010;
            public const int JackKnightOfTheLavenderDust = 28692962;
            public const int JackKnightOfTheCobaltDepths = 92204263;
            public const int JackKnightOfTheCrimsonLotus = 56809158;
            public const int JackKnightOfTheGoldenBlossom = 29415459;
            public const int JackKnightOfTheVerdantGale = 66022706;
            public const int JackKnightOfTheAmberShade = 93020401;
            public const int JackKnightOfTheAzureSky = 20537097;
            public const int MekkKnightMorningStar = 72006609;
            public const int JackKnightOfTheWorldScar = 38502358;
            public const int WhisperOfTheWorldLegacy = 62530723;
            public const int TrueDepthsOfTheWorldLegacy = 98935722;
            public const int KeyToTheWorldLegacy = 2930675;
        }

        public DarkMagicianExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            //counter
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, this.SolemnStrikeeff);
            this.AddExecutor(ExecutorType.Activate, CardId.AshBlossom, this.ChainEnemy);
            this.AddExecutor(ExecutorType.Activate, CardId.CrystalWingSynchroDragon, this.CrystalWingSynchroDragoneff);
            this.AddExecutor(ExecutorType.Activate, CardId.MaxxC, this.MaxxCeff);
            //AddExecutor(ExecutorType.Activate, CardId.Scapegoat,Scapegoateff);
            //first do
            this.AddExecutor(ExecutorType.Activate, CardId.UpstartGoblin, this.UpstartGoblineff);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkMagicalCircle, this.DarkMagicalCircleeff);
            this.AddExecutor(ExecutorType.Activate, CardId.SpellbookOfSecrets, this.SpellbookOfSecreteff);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkMagicInheritance, this.DarkMagicInheritanceeff);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkMagicAttack, this.DarkMagicAttackeff);
            //trap set
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnStrike);
            this.AddExecutor(ExecutorType.SpellSet, CardId.MagicianNavigation, this.MagicianNavigationset);
            this.AddExecutor(ExecutorType.SpellSet, CardId.EternalSoul, this.EternalSoulset);
            /*AddExecutor(ExecutorType.SpellSet, CardId.Scapegoat, Scapegoatset);            
            //sheep
            AddExecutor(ExecutorType.SpSummon, CardId.Hidaruma, Hidarumasp);
            AddExecutor(ExecutorType.SpSummon, CardId.Linkuriboh, Linkuribohsp);
            AddExecutor(ExecutorType.Activate, CardId.Linkuriboh, Linkuriboheff);
            AddExecutor(ExecutorType.SpSummon, CardId.LinkSpider, Linkuribohsp);
            AddExecutor(ExecutorType.SpSummon, CardId.BorreloadDragon, BorreloadDragonsp);
            AddExecutor(ExecutorType.SpSummon, CardId.BorreloadDragon, BorreloadDragoneff);*/
            //plan A             
            this.AddExecutor(ExecutorType.Activate, CardId.WindwitchIceBell, this.WindwitchIceBelleff);
            this.AddExecutor(ExecutorType.Activate, CardId.WindwitchGlassBell, this.WindwitchGlassBelleff);
            this.AddExecutor(ExecutorType.Activate, CardId.WindwitchSnowBell, this.WindwitchSnowBellsp);
            this.AddExecutor(ExecutorType.SpSummon, CardId.WindwitchWinterBell, this.WindwitchWinterBellsp);
            this.AddExecutor(ExecutorType.Activate, CardId.WindwitchWinterBell, this.WindwitchWinterBelleff);

            this.AddExecutor(ExecutorType.SpSummon, CardId.CrystalWingSynchroDragon, this.CrystalWingSynchroDragonsp);
            // if fail
            this.AddExecutor(ExecutorType.SpSummon, CardId.ClearWingFastDragon, this.ClearWingFastDragonsp);
            this.AddExecutor(ExecutorType.Activate, CardId.ClearWingFastDragon, this.ClearWingFastDragoneff);
            // plan B
            //AddExecutor(ExecutorType.Activate, CardId.GrinderGolem, GrinderGolemeff);
            // AddExecutor(ExecutorType.SpSummon, CardId.Linkuriboh, Linkuribohsp);
            //AddExecutor(ExecutorType.SpSummon, CardId.LinkSpider, LinkSpidersp);
            //AddExecutor(ExecutorType.SpSummon, CardId.AkashicMagician);
            //plan C
            this.AddExecutor(ExecutorType.SpSummon, CardId.OddEyesAbsoluteDragon, this.OddEyesAbsoluteDragonsp);
            this.AddExecutor(ExecutorType.Activate, CardId.OddEyesAbsoluteDragon, this.OddEyesAbsoluteDragoneff);
            this.AddExecutor(ExecutorType.Activate, CardId.OddEyesWingDragon);
            //summon
            this.AddExecutor(ExecutorType.Summon, CardId.WindwitchGlassBell, this.WindwitchGlassBellsummonfirst);
            this.AddExecutor(ExecutorType.Summon, CardId.SpellbookMagicianOfProphecy, this.SpellbookMagicianOfProphecysummon);
            this.AddExecutor(ExecutorType.Activate, CardId.SpellbookMagicianOfProphecy, this.SpellbookMagicianOfProphecyeff);
            this.AddExecutor(ExecutorType.Summon, CardId.MagiciansRod, this.MagiciansRodsummon);
            this.AddExecutor(ExecutorType.Activate, CardId.MagiciansRod, this.MagiciansRodeff);
            this.AddExecutor(ExecutorType.Summon, CardId.WindwitchGlassBell, this.WindwitchGlassBellsummon);
            //activate
            this.AddExecutor(ExecutorType.Activate, CardId.LllusionMagic, this.LllusionMagiceff);
            this.AddExecutor(ExecutorType.SpellSet, CardId.LllusionMagic, this.LllusionMagicset);
            this.AddExecutor(ExecutorType.Activate, CardId.SpellbookOfKnowledge, this.SpellbookOfKnowledgeeff);
            this.AddExecutor(ExecutorType.Activate, CardId.WonderWand, this.WonderWandeff);
            this.AddExecutor(ExecutorType.Activate, CardId.TheEyeOfTimaeus, this.TheEyeOfTimaeuseff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.ApprenticeLllusionMagician, this.ApprenticeLllusionMagiciansp);
            this.AddExecutor(ExecutorType.Activate, CardId.ApprenticeLllusionMagician, this.ApprenticeLllusionMagicianeff);
            //other thing                     
            this.AddExecutor(ExecutorType.Activate, CardId.MagicianOfLllusion);
            this.AddExecutor(ExecutorType.Activate, CardId.MagicianNavigation, this.MagicianNavigationeff);
            this.AddExecutor(ExecutorType.Activate, CardId.EternalSoul, this.EternalSouleff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BigEye, this.BigEyesp);
            this.AddExecutor(ExecutorType.Activate, CardId.BigEye, this.BigEyeeff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Dracossack, this.Dracossacksp);
            this.AddExecutor(ExecutorType.Activate, CardId.Dracossack, this.Dracossackeff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.ApprenticeWitchling, this.ApprenticeWitchlingsp);
            this.AddExecutor(ExecutorType.Activate, CardId.ApprenticeWitchling, this.ApprenticeWitchlingeff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.VentriloauistsClaraAndLucika, this.VentriloauistsClaraAndLucikasp);
            this.AddExecutor(ExecutorType.Repos, this.MonsterRepos);
        }
        private void EternalSoulSelect()
        {
            this.AI.SelectPosition(CardPosition.FaceUpAttack);
            /*
            if (Enemy.HasInMonstersZone(CardId.MekkKnightMorningStar))
            {
                int MekkKnightZone = 0;
                int BotZone = 0;
                for (int i = 0; i <= 6; i++)
                {
                    if (Enemy.MonsterZone[i] != null && Enemy.MonsterZone[i].IsCode(CardId.MekkKnightMorningStar))
                    {
                        MekkKnightZone = i;
                        break;
                    }
                }
                if (Bot.MonsterZone[GetReverseColumnMainZone(MekkKnightZone)] == null)
                {
                    BotZone = GetReverseColumnMainZone(MekkKnightZone);
                    AI.SelectPlace(ReverseZoneTo16bit(BotZone));
                }
                else
                {
                    for (int i = 0; i <= 6; i++)
                    {
                        if (!NoJackKnightColumn(i))
                        {
                            if (Bot.MonsterZone[GetReverseColumnMainZone(i)] == null)
                            {
                                AI.SelectPlace(ReverseZoneTo16bit(GetReverseColumnMainZone(i)));
                                break;
                            }
                        }
                    }
                }                
                Logger.DebugWriteLine("******************MekkKnightMorningStar");
            }
            else
            {
                for (int i = 0; i <= 6; i++)
                {
                    if (!NoJackKnightColumn(i))
                    {
                        if (Bot.MonsterZone[GetReverseColumnMainZone(i)] == null)
                        {
                            AI.SelectPlace(ReverseZoneTo16bit(GetReverseColumnMainZone(i)));
                            break;
                        } 
                    }
                }
            }
            */
        }

        readonly int attackerzone = -1;
        readonly int defenderzone = -1;
        bool Secret_used = false;
        bool plan_A = false;
        bool plan_C = false;
        int maxxc_done = 0;
        int lockbird_done = 0;
        int ghost_done = 0;
        bool maxxc_used = false;
        bool lockbird_used = false;
        bool ghost_used = false;
        bool WindwitchGlassBelleff_used = false;
        int ApprenticeLllusionMagician_count = 0;
        bool Spellbook_summon = false;
        bool Rod_summon = false;
        bool GlassBell_summon = false;
        bool magician_sp = false;
        bool soul_used = false;
        bool big_attack = false;
        bool big_attack_used = false;
        bool CrystalWingSynchroDragon_used = false;
        public override void OnNewPhase()
        {
            //Util.UpdateLinkedZone();
            //Logger.DebugWriteLine("Zones.CheckLinkedPointZones= " + Zones.CheckLinkedPointZones);
            //Logger.DebugWriteLine("Zones.CheckMutualEnemyZoneCount= " + Zones.CheckMutualEnemyZoneCount);
            this.plan_C = false;
            this.ApprenticeLllusionMagician_count = 0;
            foreach (ClientCard count in this.Bot.GetMonsters())
            {
                if (count.IsCode(CardId.ApprenticeLllusionMagician) && count.IsFaceup())
                {
                    this.ApprenticeLllusionMagician_count++;
                }
            }
            foreach (ClientCard dangerous in this.Enemy.GetMonsters())
            {
                if (dangerous != null && dangerous.IsShouldNotBeTarget() && dangerous.Attack > 2500 &&
                    !this.Bot.HasInHandOrHasInMonstersZone(CardId.ApprenticeLllusionMagician))
                {
                    this.plan_C = true;
                    Logger.DebugWriteLine("*********dangerous = " + dangerous.Id);
                }
            }
            if (this.Bot.HasInHand(CardId.SpellbookMagicianOfProphecy) &&
              this.Bot.HasInHand(CardId.MagiciansRod) &&
              this.Bot.HasInHand(CardId.WindwitchGlassBell))
            {
                if (this.Bot.HasInHand(CardId.SpellbookOfKnowledge) ||
                    this.Bot.HasInHand(CardId.WonderWand))
                {
                    this.Rod_summon = true;
                }
                else
                {
                    this.Spellbook_summon = true;
                }
            }
            else if
                 (this.Bot.HasInHand(CardId.SpellbookMagicianOfProphecy) &&
                 this.Bot.HasInHand(CardId.MagiciansRod))
            {
                if (this.Bot.HasInSpellZone(CardId.EternalSoul) &&
                    !(this.Bot.HasInHand(CardId.DarkMagician) || this.Bot.HasInHand(CardId.DarkMagician)))
                {
                    this.Rod_summon = true;
                }
                else if (this.Bot.HasInHand(CardId.SpellbookOfKnowledge) ||
                    this.Bot.HasInHand(CardId.WonderWand))
                {
                    this.Rod_summon = true;
                }
                else
                {
                    this.Spellbook_summon = true;
                }
            }
            else if
                  (this.Bot.HasInHand(CardId.SpellbookMagicianOfProphecy) &&
                 this.Bot.HasInHand(CardId.WindwitchGlassBell))
            {
                if (this.plan_A)
                {
                    this.Rod_summon = true;
                }
                else
                {
                    this.GlassBell_summon = true;
                }
            }
            else if
                  (this.Bot.HasInHand(CardId.MagiciansRod) &&
                 this.Bot.HasInHand(CardId.WindwitchGlassBell))
            {
                if (this.plan_A)
                {
                    this.Rod_summon = true;
                }
                else
                {
                    this.GlassBell_summon = true;
                }
            }
            else
            {
                this.Spellbook_summon = true;
                this.Rod_summon = true;
                this.GlassBell_summon = true;
            }
        }
        public override void OnNewTurn()
        {
            this.CrystalWingSynchroDragon_used = false;
            this.Secret_used = false;
            this.maxxc_used = false;
            this.lockbird_used = false;
            this.ghost_used = false;
            this.WindwitchGlassBelleff_used = false;
            this.Spellbook_summon = false;
            this.Rod_summon = false;
            this.GlassBell_summon = false;
            this.magician_sp = false;
            this.big_attack = false;
            this.big_attack_used = false;
            this.soul_used = false;
        }
        public int GetTotalATK(IList<ClientCard> list)
        {

            int atk = 0;
            foreach (ClientCard c in list)
            {
                if (c == null)
                {
                    continue;
                }

                atk += c.Attack;
            }
            return atk;
        }

        private bool WindwitchIceBelleff()
        {
            if (this.lockbird_used)
            {
                return false;
            }

            if (this.Enemy.HasInMonstersZone(CardId.ElShaddollWinda))
            {
                return false;
            }

            if (this.maxxc_used)
            {
                return false;
            }

            if (this.WindwitchGlassBelleff_used)
            {
                return false;
            }
            //AI.SelectPlace(Zones.z2, 1);
            if (this.Bot.GetRemainingCount(CardId.WindwitchGlassBell, 2) >= 1)
            {
                this.AI.SelectCard(CardId.WindwitchGlassBell);
            }
            else if (this.Bot.HasInHand(CardId.WindwitchGlassBell))
            {
                this.AI.SelectCard(CardId.WindwitchSnowBell);
            }

            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }

        private bool WindwitchGlassBelleff()
        {
            if (this.Bot.HasInMonstersZone(CardId.WindwitchIceBell))
            {
                int ghost_count = 0;
                foreach (ClientCard check in this.Enemy.Graveyard)
                {
                    if (check.IsCode(CardId.Ghost))
                    {
                        ghost_count++;
                    }
                }
                if (ghost_count != this.ghost_done)
                {
                    this.AI.SelectCard(CardId.WindwitchIceBell);
                }
                else
                {
                    this.AI.SelectCard(CardId.WindwitchSnowBell);
                }
            }
            else
            {
                this.AI.SelectCard(CardId.WindwitchIceBell);
            }

            this.WindwitchGlassBelleff_used = true;
            return true;
        }

        private bool WindwitchSnowBellsp()
        {
            if (this.maxxc_used)
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.WindwitchIceBell) &&
                this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell))
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;
        }

        private bool WindwitchWinterBellsp()
        {
            if (this.maxxc_used)
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.WindwitchIceBell) &&
                 this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell) &&
                 this.Bot.HasInMonstersZone(CardId.WindwitchSnowBell))
            {
                //AI.SelectPlace(Zones.z5, Zones.ExtraMonsterZones);
                this.AI.SelectCard(CardId.WindwitchIceBell, CardId.WindwitchGlassBell);
                this.AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
            }

            return false;
        }

        private bool WindwitchWinterBelleff()
        {
            this.AI.SelectCard(CardId.WindwitchGlassBell);
            return true;
        }

        private bool ClearWingFastDragonsp()
        {
            if (this.Bot.HasInMonstersZone(CardId.WindwitchIceBell) &&
                this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell))
            {

                this.AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
            }

            return false;
        }

        private bool ClearWingFastDragoneff()
        {
            if (this.Card.Location == CardLocation.MonsterZone)
            {
                if (this.Duel.Player == 1)
                {
                    return this.DefaultTrap();
                }

                return true;
            }
            return false;
        }
        private bool CrystalWingSynchroDragonsp()
        {
            if (this.Bot.HasInMonstersZone(CardId.WindwitchSnowBell) && this.Bot.HasInMonstersZone(CardId.WindwitchWinterBell))
            {
                //AI.SelectPlace(Zones.z5, Zones.ExtraMonsterZones);
                this.plan_A = true;
                return true;
            }
            return false;
        }

        /* private bool GrinderGolemeff()
         {

             if (plan_A) return false;
             AI.SelectPosition(CardPosition.FaceUpDefence);
             if (Bot.GetMonstersExtraZoneCount() == 0)
                 return true;
             if (Bot.HasInMonstersZone(CardId.AkashicMagician) ||
                 Bot.HasInMonstersZone(CardId.SecurityDragon))
                 return true;
             return false;
         }*/

        /*  private bool Linkuribohsp()
          {
              if (Bot.HasInMonstersZone(CardId.GrinderGolem + 1))
              {
                  AI.SelectCard(CardId.GrinderGolem + 1);
                  return true;
              }
              return false;
          }

          private bool LinkSpidersp()
          {
              if(Bot.HasInMonstersZone(CardId.GrinderGolem+1))
              {
                  AI.SelectCard(CardId.GrinderGolem + 1);
                  return true;
              }
              return false;
          }*/

        private bool OddEyesAbsoluteDragonsp()
        {
            if (this.plan_C)
            {
                return true;
            }
            return false;
        }

        private bool OddEyesAbsoluteDragoneff()
        {
            Logger.DebugWriteLine("OddEyesAbsoluteDragonef 1");
            if (this.Card.Location == CardLocation.MonsterZone/*ActivateDescription == Util.GetStringId(CardId.OddEyesAbsoluteDragon, 0)*/)
            {
                Logger.DebugWriteLine("OddEyesAbsoluteDragonef 2");
                return this.Duel.Player == 1;
            }
            else if (this.Card.Location == CardLocation.Grave/*ActivateDescription == Util.GetStringId(CardId.OddEyesAbsoluteDragon, 0)*/)
            {
                Logger.DebugWriteLine("OddEyesAbsoluteDragonef 3");
                this.AI.SelectCard(CardId.OddEyesWingDragon);
                return true;
            }
            return false;
        }

        private bool SolemnStrikeeff()
        {
            if (this.Bot.LifePoints > 1500 && this.Duel.LastChainPlayer == 1)
            {
                return true;
            }

            return false;
        }

        private bool ChainEnemy()
        {
            if (this.Util.GetLastChainCard() != null &&
                this.Util.GetLastChainCard().IsCode(CardId.UpstartGoblin))
            {
                return false;
            }

            return this.Duel.LastChainPlayer == 1;
        }

        private bool CrystalWingSynchroDragoneff()
        {
            if (this.Duel.LastChainPlayer == 1)
            {
                this.CrystalWingSynchroDragon_used = true;
                return true;
            }
            return false;
        }

        private bool MaxxCeff()
        {
            return this.Duel.Player == 1;
        }
        /*
        private bool Scapegoatset()
        {
            if (Bot.HasInSpellZone(CardId.Scapegoat)) return false;
            return (Bot.GetMonsterCount() - Bot.GetMonstersExtraZoneCount()) < 2;
        }

        public bool Scapegoateff()
        {
            if (Duel.Player == 0) return false;           
            if (DefaultOnBecomeTarget() && !Enemy.HasInMonstersZone(CardId.UltimateConductorTytanno))
            {
                Logger.DebugWriteLine("*************************sheepeff");
                return true;
            }
            if (Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon)) return false;     
            if(Duel.Phase == DuelPhase.End)
            {
                Logger.DebugWriteLine("*************************sheepeff");
                return true;
            }
            if (Duel.Phase > DuelPhase.Main1 && Duel.Phase < DuelPhase.Main2)
            {
                
                int total_atk = 0;
                List<ClientCard> enemy_monster = Enemy.GetMonsters();
                foreach (ClientCard m in enemy_monster)
                {
                    if (m.IsAttack() && !m.Attacked) total_atk += m.Attack;
                }
                if (total_atk >= Bot.LifePoints && !Enemy.HasInMonstersZone(CardId.UltimateConductorTytanno)) return true;
            }
            return false;
        }
        
        private bool Hidarumasp()
        {
            if (!Bot.HasInMonstersZone(CardId.Scapegoat + 1)) return false;
            if(Bot.MonsterZone[5]==null)
            {
                AI.SelectCard(new[] { CardId.Scapegoat + 1, CardId.Scapegoat + 1 });               
                return true;
            }
            if (Bot.MonsterZone[6] == null)
            {
                AI.SelectCard(new[] { CardId.Scapegoat + 1, CardId.Scapegoat + 1 });                
                return true;
            }
            return false;

        }

        private bool Linkuribohsp()
        {
            foreach (ClientCard c in Bot.GetMonsters())
            {
                if (!c.IsCode(CardId.WindwitchSnowBell) && c.Level == 1 && !c.IsCode(CardId.LinkSpider) && !c.IsCode(CardId.Linkuriboh))
                {
                    AI.SelectCard(c);
                    return true;
                }
            }
            return false;
        }


        private bool Linkuriboheff()
        {
            if (Duel.LastChainPlayer == 0 && Util.GetLastChainCard().IsCode(CardId.Linkuriboh)) return false;
            if (Bot.HasInMonstersZone(CardId.WindwitchSnowBell)) return false;
            return true;
        }
        private bool BorreloadDragonsp()
        {
            if(Bot.HasInMonstersZone(CardId.Hidaruma)&&
                Bot.HasInMonstersZone(CardId.Linkuriboh))
            {
                AI.SelectCard(new[] { CardId.Hidaruma, CardId.Linkuriboh, CardId.LinkSpider ,CardId.Linkuriboh});
                return true;
            }
            
            return false;
        }

       
        private bool BorreloadDragoneff()
        {
            if (ActivateDescription == -1)
            {
                ClientCard enemy_monster = Enemy.BattlingMonster;
                if (enemy_monster != null && enemy_monster.HasPosition(CardPosition.Attack))
                {
                    return enemy_monster.Attack > 2000;
                }
                return true;
            };
            ClientCard BestEnemy = Util.GetBestEnemyMonster(true,true);            
            if (BestEnemy == null || BestEnemy.HasPosition(CardPosition.FaceDown)) return false;
            AI.SelectCard(BestEnemy);
            return true;  
        }*/
        private bool EternalSoulset()
        {
            if (this.Bot.GetHandCount() > 6)
            {
                return true;
            }

            if (!this.Bot.HasInSpellZone(CardId.EternalSoul))
            {
                return true;
            }

            return false;
        }

        private bool EternalSouleff()
        {
            IList<ClientCard> grave = this.Bot.Graveyard;
            IList<ClientCard> magician = new List<ClientCard>();
            foreach (ClientCard check in grave)
            {
                if (check.IsCode(CardId.DarkMagician))
                {
                    magician.Add(check);
                }
            }
            if (this.Util.IsChainTarget(this.Card) && this.Bot.GetMonsterCount() == 0)
            {
                this.AI.SelectYesNo(false);
                return true;
            }
            if (this.Util.ChainCountPlayer(0) > 0)
            {
                return false;
            }

            if (this.Enemy.HasInSpellZone(CardId.HarpiesFeatherDuster) && this.Card.IsFacedown())
            {
                return false;
            }

            foreach (ClientCard target in this.Duel.ChainTargets)
            {
                if ((target.IsCode(CardId.DarkMagician, CardId.DarkMagicianTheDragonKnight))
                    && this.Card.IsFacedown())
                {
                    this.AI.SelectYesNo(false);
                    return true;
                }
            }

            if (this.Enemy.HasInSpellZone(CardId.DarkHole) && this.Card.IsFacedown() &&
                (this.Bot.HasInMonstersZone(CardId.DarkMagician) || this.Bot.HasInMonstersZone(CardId.DarkMagicianTheDragonKnight)))
            {
                this.AI.SelectYesNo(false);
                return true;
            }
            if (this.Bot.HasInGraveyard(CardId.DarkMagicianTheDragonKnight) &&
                !this.Bot.HasInMonstersZone(CardId.DarkMagicianTheDragonKnight) && !this.plan_C)
            {
                this.EternalSoulSelect();
                this.AI.SelectCard(CardId.DarkMagicianTheDragonKnight);
                return true;
            }
            if (this.Duel.Player == 1 && this.Bot.HasInSpellZone(CardId.DarkMagicalCircle) &&
                (this.Enemy.HasInMonstersZone(CardId.SummonSorceress) || this.Enemy.HasInMonstersZone(CardId.FirewallDragon)))
            {
                this.soul_used = true;
                this.magician_sp = true;
                this.EternalSoulSelect();
                this.AI.SelectCard(magician);
                return true;
            }
            if (this.Duel.Player == 1 && this.Duel.Phase == DuelPhase.BattleStart && this.Enemy.GetMonsterCount() > 0)
            {
                if (this.Card.IsFacedown() && this.Bot.HasInMonstersZone(CardId.VentriloauistsClaraAndLucika))
                {
                    this.AI.SelectYesNo(false);
                    return true;
                }
                if (this.Card.IsFacedown() &&
                (this.Bot.HasInMonstersZone(CardId.DarkMagician) || this.Bot.HasInMonstersZone(CardId.DarkMagicianTheDragonKnight)))
                {
                    this.AI.SelectYesNo(false);
                    return true;
                }
                if (this.Bot.HasInGraveyard(CardId.DarkMagicianTheDragonKnight) ||
                    this.Bot.HasInGraveyard(CardId.DarkMagician))
                {
                    this.soul_used = true;
                    this.magician_sp = true;
                    this.EternalSoulSelect();
                    this.AI.SelectCard(magician);
                    return true;
                }
                if (this.Bot.HasInHand(CardId.DarkMagician))
                {
                    this.soul_used = true;
                    this.magician_sp = true;
                    this.AI.SelectCard(CardId.DarkMagician);
                    this.EternalSoulSelect();
                    return true;
                }
            }
            if (this.Duel.Player == 0 && this.Duel.Phase == DuelPhase.Main1)
            {
                if (this.Bot.HasInHand(CardId.DarkMagicalCircle) && !this.Bot.HasInSpellZone(CardId.DarkMagicalCircle))
                {
                    return false;
                }

                if (this.Bot.HasInGraveyard(CardId.DarkMagicianTheDragonKnight) ||
                    this.Bot.HasInGraveyard(CardId.DarkMagician))
                {
                    this.soul_used = true;
                    this.magician_sp = true;
                    this.AI.SelectCard(magician);
                    this.EternalSoulSelect();
                    return true;
                }
                if (this.Bot.HasInHand(CardId.DarkMagician))
                {
                    this.soul_used = true;
                    this.magician_sp = true;
                    this.AI.SelectCard(CardId.DarkMagician);
                    this.EternalSoulSelect();
                    return true;
                }
            }
            if (this.Duel.Phase == DuelPhase.End)
            {
                if (this.Card.IsFacedown() && this.Bot.HasInMonstersZone(CardId.VentriloauistsClaraAndLucika))
                {
                    this.AI.SelectYesNo(false);
                    return true;
                }
                if (this.Bot.HasInGraveyard(CardId.DarkMagicianTheDragonKnight) ||
                    this.Bot.HasInGraveyard(CardId.DarkMagician))
                {
                    this.soul_used = true;
                    this.magician_sp = true;
                    this.AI.SelectCard(magician);
                    this.EternalSoulSelect();
                    return true;
                }
                if (this.Bot.HasInHand(CardId.DarkMagician))
                {
                    this.soul_used = true;
                    this.magician_sp = true;
                    this.AI.SelectCard(CardId.DarkMagician);
                    this.EternalSoulSelect();
                    return true;
                }
                return true;
            }
            return false;
        }

        private bool MagicianNavigationset()
        {
            if (this.Bot.GetHandCount() > 6)
            {
                return true;
            }

            if (this.Bot.HasInSpellZone(CardId.LllusionMagic))
            {
                return true;
            }

            if (this.Bot.HasInHand(CardId.DarkMagician) && !this.Bot.HasInSpellZone(CardId.MagicianNavigation))
            {
                return true;
            }

            return false;
        }

        private bool MagicianNavigationeff()
        {
            bool spell_act = false;
            IList<ClientCard> spell = new List<ClientCard>();
            if (this.Duel.LastChainPlayer == 1)
            {
                foreach (ClientCard check in this.Enemy.GetSpells())
                {
                    if (this.Util.GetLastChainCard() == check)
                    {
                        spell.Add(check);
                        spell_act = true;
                        break;
                    }
                }
            }
            bool soul_faceup = false;
            foreach (ClientCard check in this.Bot.GetSpells())
            {
                if (check.IsCode(CardId.EternalSoul) && check.IsFaceup())
                {
                    soul_faceup = true;
                }
            }
            if (this.Card.Location == CardLocation.Grave && spell_act)
            {
                Logger.DebugWriteLine("**********************Navigationeff***********");
                this.AI.SelectCard(spell);
                return true;
            }
            if (this.Util.IsChainTarget(this.Card))
            {
                this.AI.SelectPlace(Zones.MonsterZone1 | Zones.MonsterZone5);
                this.AI.SelectCard(CardId.DarkMagician);
                ClientCard check = this.Util.GetOneEnemyBetterThanValue(2500, true);
                if (check != null)
                {
                    this.AI.SelectNextCard(CardId.ApprenticeLllusionMagician, CardId.DarkMagician, CardId.MagicianOfLllusion);
                }
                else
                {
                    this.AI.SelectNextCard(CardId.ApprenticeLllusionMagician, CardId.DarkMagician, CardId.MagicianOfLllusion);
                }

                this.magician_sp = true;
                return this.UniqueFaceupSpell();
            }
            if (this.DefaultOnBecomeTarget() && !soul_faceup)
            {
                this.AI.SelectPlace(Zones.MonsterZone1 | Zones.MonsterZone5);
                this.AI.SelectCard(CardId.DarkMagician);
                ClientCard check = this.Util.GetOneEnemyBetterThanValue(2500, true);
                if (check != null)
                {
                    this.AI.SelectNextCard(CardId.ApprenticeLllusionMagician, CardId.DarkMagician, CardId.MagicianOfLllusion);
                }
                else
                {
                    this.AI.SelectNextCard(CardId.ApprenticeLllusionMagician, CardId.DarkMagician, CardId.MagicianOfLllusion);
                }

                this.magician_sp = true;
                return true;
            }
            if (this.Duel.Player == 0 && this.Card.Location == CardLocation.SpellZone && !this.maxxc_used && this.Bot.HasInHand(CardId.DarkMagician))
            {
                this.AI.SelectPlace(Zones.MonsterZone1 | Zones.MonsterZone5);
                this.AI.SelectCard(CardId.DarkMagician);
                ClientCard check = this.Util.GetOneEnemyBetterThanValue(2500, true);
                if (check != null)
                {
                    this.AI.SelectNextCard(CardId.ApprenticeLllusionMagician, CardId.DarkMagician, CardId.MagicianOfLllusion);
                }
                else
                {
                    this.AI.SelectNextCard(CardId.ApprenticeLllusionMagician, CardId.DarkMagician, CardId.MagicianOfLllusion);
                }

                this.magician_sp = true;
                return this.UniqueFaceupSpell();
            }
            if (this.Duel.Player == 1 && this.Bot.HasInSpellZone(CardId.DarkMagicalCircle) &&
                (this.Enemy.HasInMonstersZone(CardId.SummonSorceress) || this.Enemy.HasInMonstersZone(CardId.FirewallDragon))
                && this.Card.Location == CardLocation.SpellZone)
            {
                this.AI.SelectPlace(Zones.MonsterZone1 | Zones.MonsterZone5);
                this.AI.SelectCard(CardId.DarkMagician);
                ClientCard check = this.Util.GetOneEnemyBetterThanValue(2500, true);
                if (check != null)
                {
                    this.AI.SelectNextCard(CardId.ApprenticeLllusionMagician, CardId.DarkMagician, CardId.MagicianOfLllusion);
                }
                else
                {
                    this.AI.SelectNextCard(CardId.ApprenticeLllusionMagician, CardId.DarkMagician, CardId.MagicianOfLllusion);
                }

                this.magician_sp = true;
                return this.UniqueFaceupSpell();
            }
            if (this.Enemy.GetFieldCount() > 0 &&
                (this.Duel.Phase == DuelPhase.BattleStart || this.Duel.Phase == DuelPhase.End) &&
                this.Card.Location == CardLocation.SpellZone && !this.maxxc_used)
            {
                this.AI.SelectPlace(Zones.MonsterZone1 | Zones.MonsterZone5);
                this.AI.SelectCard(CardId.DarkMagician);
                ClientCard check = this.Util.GetOneEnemyBetterThanValue(2500, true);
                if (check != null)
                {
                    this.AI.SelectNextCard(CardId.ApprenticeLllusionMagician, CardId.DarkMagician, CardId.MagicianOfLllusion);
                }
                else
                {
                    this.AI.SelectNextCard(CardId.ApprenticeLllusionMagician, CardId.DarkMagician, CardId.MagicianOfLllusion);
                }

                this.magician_sp = true;
                return this.UniqueFaceupSpell();
            }

            return false;
        }
        private bool DarkMagicalCircleeff()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                //AI.SelectPlace(Zones.z2, 2);
                if (this.Bot.LifePoints <= 4000)
                {
                    return true;
                }

                return this.UniqueFaceupSpell();
            }
            else
            {
                if (this.magician_sp)
                {
                    this.AI.SelectCard(this.Util.GetBestEnemyCard(false, true));
                    if (this.Util.GetBestEnemyCard(false, true) != null)
                    {
                        Logger.DebugWriteLine("*************SelectCard= " + this.Util.GetBestEnemyCard(false, true).Id);
                    }

                    this.magician_sp = false;
                }
            }
            return true;
        }
        private bool LllusionMagicset()
        {
            if (this.Bot.GetMonsterCount() >= 1 &&
                !(this.Bot.GetMonsterCount() == 1 && this.Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon)) &&
                !(this.Bot.GetMonsterCount() == 1 && this.Bot.HasInMonstersZone(CardId.ClearWingFastDragon)) &&
                !(this.Bot.GetMonsterCount() == 1 && this.Bot.HasInMonstersZone(CardId.VentriloauistsClaraAndLucika)))
            {
                return true;
            }

            return false;
        }
        private bool LllusionMagiceff()
        {
            if (this.lockbird_used)
            {
                return false;
            }

            if (this.Duel.LastChainPlayer == 0)
            {
                return false;
            }

            ClientCard target = null;
            bool soul_exist = false;
            //AI.SelectPlace(Zones.z2, 2);
            foreach (ClientCard m in this.Bot.GetSpells())
            {
                if (m.IsCode(CardId.EternalSoul) && m.IsFaceup())
                {
                    soul_exist = true;
                }
            }
            if (!this.soul_used && soul_exist)
            {
                if (this.Bot.HasInMonstersZone(CardId.MagiciansRod))
                {
                    this.AI.SelectCard(CardId.MagiciansRod);
                    this.AI.SelectNextCard(CardId.DarkMagician, CardId.DarkMagician);
                    return true;
                }
            }
            if (this.Duel.Player == 0)
            {
                int ghost_count = 0;
                foreach (ClientCard check in this.Enemy.Graveyard)
                {
                    if (check.IsCode(CardId.Ghost))
                    {
                        ghost_count++;
                    }
                }
                if (ghost_count != this.ghost_done)
                {
                    if (this.Duel.CurrentChain.Count >= 2 && this.Util.GetLastChainCard().IsCode(0))
                    {
                        this.AI.SelectCard(CardId.MagiciansRod);
                        this.AI.SelectNextCard(CardId.DarkMagician, CardId.DarkMagician);
                        return true;
                    }
                }
                int count = 0;
                foreach (ClientCard m in this.Bot.GetMonsters())
                {
                    if (this.Util.IsChainTarget(m))
                    {
                        count++;
                        target = m;
                        Logger.DebugWriteLine("************IsChainTarget= " + target.Id);
                        break;
                    }
                }
                if (count == 0)
                {
                    return false;
                }

                if ((target.IsCode(CardId.WindwitchGlassBell, CardId.WindwitchIceBell)) &&
                    this.Bot.HasInMonstersZone(CardId.WindwitchIceBell) &&
                    this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell))
                {
                    return false;
                }

                this.AI.SelectCard(target);
                this.AI.SelectNextCard(CardId.DarkMagician, CardId.DarkMagician);
                return true;
            }

            if (this.Bot.HasInMonstersZone(CardId.MagiciansRod) || this.Bot.HasInMonstersZone(CardId.SpellbookMagicianOfProphecy))
            {
                this.AI.SelectCard(CardId.MagiciansRod, CardId.SpellbookMagicianOfProphecy);
                this.AI.SelectNextCard(CardId.DarkMagician, CardId.DarkMagician);
                return true;
            }
            if (this.Duel.Player == 1 && this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell))
            {
                this.AI.SelectCard(CardId.WindwitchGlassBell);
                this.AI.SelectNextCard(CardId.DarkMagician, CardId.DarkMagician);
                return true;
            }
            if (this.Duel.Player == 1 && this.Bot.HasInMonstersZone(CardId.WindwitchIceBell))
            {
                this.AI.SelectCard(CardId.WindwitchIceBell);
                this.AI.SelectNextCard(CardId.DarkMagician, CardId.DarkMagician);
                return true;
            }
            if (this.Duel.Player == 1 && this.Bot.HasInMonstersZone(CardId.WindwitchSnowBell))
            {
                this.AI.SelectCard(CardId.WindwitchSnowBell);
                this.AI.SelectNextCard(CardId.DarkMagician, CardId.DarkMagician);
                return true;
            }
            if (this.Duel.Player == 1 && this.Bot.HasInMonstersZone(CardId.SpellbookMagicianOfProphecy))
            {
                this.AI.SelectCard(CardId.SpellbookMagicianOfProphecy);
                this.AI.SelectNextCard(CardId.DarkMagician, CardId.DarkMagician);
                return true;
            }
            if (this.Duel.Player == 1 && this.Bot.HasInMonstersZone(CardId.ApprenticeLllusionMagician) &&
               (this.Bot.HasInSpellZone(CardId.EternalSoul) || this.Bot.HasInSpellZone(CardId.MagicianNavigation)))
            {
                this.AI.SelectCard(CardId.ApprenticeLllusionMagician);
                this.AI.SelectNextCard(CardId.DarkMagician, CardId.DarkMagician);
                return true;
            }

            if ((this.Bot.GetRemainingCount(CardId.DarkMagician, 3) > 1 || this.Bot.HasInGraveyard(CardId.DarkMagician)) &&
                this.Bot.HasInSpellZone(CardId.MagicianNavigation) &&
                (this.Bot.HasInMonstersZone(CardId.DarkMagician) || this.Bot.HasInMonstersZone(CardId.ApprenticeLllusionMagician)) &&
                this.Duel.Player == 1 && !this.Bot.HasInHand(CardId.DarkMagician))
            {
                this.AI.SelectCard(CardId.DarkMagician, CardId.ApprenticeLllusionMagician);
                this.AI.SelectNextCard(CardId.DarkMagician, CardId.DarkMagician);
                return true;
            }
            return false;
        }
        private bool SpellbookMagicianOfProphecyeff()
        {
            Logger.DebugWriteLine("*********Secret_used= " + this.Secret_used);
            if (this.Secret_used)
            {
                this.AI.SelectCard(CardId.SpellbookOfKnowledge);
            }
            else
            {
                this.AI.SelectCard(CardId.SpellbookOfSecrets, CardId.SpellbookOfKnowledge);
            }

            return true;
        }

        private bool TheEyeOfTimaeuseff()
        {
            //AI.SelectPlace(Zones.z2, 2);
            return true;
        }

        private bool UpstartGoblineff()
        {
            //AI.SelectPlace(Zones.z2, 2);
            return true;
        }
        private bool SpellbookOfSecreteff()
        {
            if (this.lockbird_used)
            {
                return false;
            }
            //AI.SelectPlace(Zones.z2, 2);
            this.Secret_used = true;
            if (this.Bot.HasInHand(CardId.SpellbookMagicianOfProphecy))
            {
                this.AI.SelectCard(CardId.SpellbookOfKnowledge);
            }
            else
            {
                this.AI.SelectCard(CardId.SpellbookMagicianOfProphecy);
            }

            return true;
        }

        private bool SpellbookOfKnowledgeeff()
        {
            int count = 0;
            foreach (ClientCard check in this.Bot.GetMonsters())
            {
                if (!check.IsCode(CardId.CrystalWingSynchroDragon))
                {
                    count++;
                }
            }
            Logger.DebugWriteLine("%%%%%%%%%%%%%%%%SpellCaster= " + count);
            if (this.lockbird_used)
            {
                return false;
            }

            if (this.Bot.HasInSpellZone(CardId.LllusionMagic) && count < 2)
            {
                return false;
            }
            //AI.SelectPlace(Zones.z2, 2);
            if (this.Bot.HasInMonstersZone(CardId.SpellbookMagicianOfProphecy) ||
                this.Bot.HasInMonstersZone(CardId.MagiciansRod) ||
                this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell) ||
                this.Bot.HasInMonstersZone(CardId.WindwitchIceBell))
            {
                this.AI.SelectCard(CardId.SpellbookMagicianOfProphecy, CardId.MagiciansRod, CardId.WindwitchGlassBell);
                return true;
            }
            if (this.Bot.HasInMonstersZone(CardId.ApprenticeLllusionMagician) && this.Bot.GetSpellCount() < 2 && this.Duel.Phase == DuelPhase.Main2)
            {
                this.AI.SelectCard(CardId.ApprenticeLllusionMagician);
                return true;
            }
            if (this.Bot.HasInMonstersZone(CardId.DarkMagician) &&
                    this.Bot.HasInSpellZone(CardId.EternalSoul) && this.Duel.Phase == DuelPhase.Main2)
            {
                this.AI.SelectCard(CardId.DarkMagician);
                return true;
            }
            return false;
        }

        private bool WonderWandeff()
        {
            if (this.lockbird_used)
            {
                return false;
            }

            int count = 0;
            foreach (ClientCard check in this.Bot.GetMonsters())
            {
                if (!check.IsCode(CardId.CrystalWingSynchroDragon))
                {
                    count++;
                }
            }
            Logger.DebugWriteLine("%%%%%%%%%%%%%%%%SpellCaster= " + count);
            if (this.Card.Location == CardLocation.Hand)
            {
                if (this.Bot.HasInSpellZone(CardId.LllusionMagic) && count < 2)
                {
                    return false;
                }
                //AI.SelectPlace(Zones.z2, 2);
                if (this.Bot.HasInMonstersZone(CardId.SpellbookMagicianOfProphecy) ||
                this.Bot.HasInMonstersZone(CardId.MagiciansRod) ||
                this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell) ||
                this.Bot.HasInMonstersZone(CardId.WindwitchIceBell))
                {
                    this.AI.SelectCard(
                        CardId.SpellbookMagicianOfProphecy,
                        CardId.MagiciansRod,
                        CardId.WindwitchGlassBell,
                        CardId.WindwitchIceBell
                        );
                    return this.UniqueFaceupSpell();
                }

                if (this.Bot.HasInMonstersZone(CardId.DarkMagician) &&
                    this.Bot.HasInSpellZone(CardId.EternalSoul) && this.Duel.Phase == DuelPhase.Main2)
                {
                    this.AI.SelectCard(CardId.DarkMagician);
                    return this.UniqueFaceupSpell();
                }
                if (this.Bot.HasInMonstersZone(CardId.ApprenticeLllusionMagician) && this.Bot.GetSpellCount() < 2 && this.Duel.Phase == DuelPhase.Main2)
                {
                    this.AI.SelectCard(CardId.ApprenticeLllusionMagician);
                    return this.UniqueFaceupSpell();
                }
                if (this.Bot.HasInMonstersZone(CardId.ApprenticeLllusionMagician) && this.Bot.GetHandCount() <= 3 && this.Duel.Phase == DuelPhase.Main2)
                {
                    this.AI.SelectCard(CardId.ApprenticeLllusionMagician);
                    return this.UniqueFaceupSpell();
                }
            }
            else
            {
                if (this.Duel.Turn != 1)
                {
                    if (this.Duel.Phase == DuelPhase.Main1 && this.Enemy.GetSpellCountWithoutField() == 0 &&
                    this.Util.GetBestEnemyMonster(true, true) == null)
                    {
                        return false;
                    }

                    if (this.Duel.Phase == DuelPhase.Main1 && this.Enemy.GetSpellCountWithoutField() == 0 &&
                        this.Util.GetBestEnemyMonster().IsFacedown())
                    {
                        return true;
                    }

                    if (this.Duel.Phase == DuelPhase.Main1 && this.Enemy.GetSpellCountWithoutField() == 0 &&
                        this.Util.GetBestBotMonster(true) != null &&
                        this.Util.GetBestBotMonster(true).Attack > this.Util.GetBestEnemyMonster(true).Attack)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private bool ApprenticeLllusionMagiciansp()
        {
            //AI.SelectPlace(Zones.z2, 1);
            if (this.Bot.HasInHand(CardId.DarkMagician) && !this.Bot.HasInSpellZone(CardId.MagicianNavigation))
            {
                if (this.Bot.GetRemainingCount(CardId.DarkMagician, 3) > 0)
                {
                    this.AI.SelectCard(CardId.DarkMagician);
                    this.AI.SelectPosition(CardPosition.FaceUpAttack);
                    return true;
                }
                return false;
            }
            if ((this.Bot.HasInHand(CardId.SpellbookOfSecrets) ||
                  this.Bot.HasInHand(CardId.DarkMagicAttack)))
            {
                this.AI.SelectPosition(CardPosition.FaceUpAttack);
                this.AI.SelectCard(CardId.SpellbookOfSecrets, CardId.DarkMagicAttack);
                return true;
            }
            if (this.Bot.HasInMonstersZone(CardId.ApprenticeLllusionMagician))
            {
                return false;
            }

            int count = 0;
            foreach (ClientCard check in this.Bot.Hand)
            {
                if (check.IsCode(CardId.WonderWand))
                {
                    count++;
                }
            }
            if (count >= 2)
            {
                this.AI.SelectPosition(CardPosition.FaceUpAttack);
                this.AI.SelectCard(CardId.WonderWand);
                return true;
            }
            if(!this.Bot.HasInHandOrInSpellZone(CardId.EternalSoul) &&
                this.Bot.HasInHandOrInSpellZone(CardId.MagicianNavigation)&&
                !this.Bot.HasInHand(CardId.DarkMagician) && this.Bot.GetHandCount()>2&&
                this.Bot.GetMonsterCount()==0)
            {
                this.AI.SelectPosition(CardPosition.FaceUpAttack);
                this.AI.SelectCard(CardId.MagicianOfLllusion, CardId.ApprenticeLllusionMagician, CardId.TheEyeOfTimaeus, CardId.DarkMagicInheritance, CardId.WonderWand);
                return true;
            }
            if (!this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.DarkMagician))
            {
                if (this.Bot.HasInHandOrInSpellZone(CardId.LllusionMagic) && this.Bot.GetMonsterCount() >= 1)
                {
                    return false;
                }

                this.AI.SelectPosition(CardPosition.FaceUpAttack);
                int Navigation_count = 0;
                foreach (ClientCard Navigation in this.Bot.Hand)
                {
                    if (Navigation.IsCode(CardId.MagicianNavigation))
                    {
                        Navigation_count++;
                    }
                }
                if (Navigation_count >= 2)
                {
                    this.AI.SelectCard(CardId.MagicianNavigation);
                    return true;
                }
                this.AI.SelectCard(CardId.MagicianOfLllusion, CardId.ApprenticeLllusionMagician, CardId.TheEyeOfTimaeus, CardId.DarkMagicInheritance, CardId.WonderWand);
                return true;
            }
            return false;
        }
        private bool ApprenticeLllusionMagicianeff()
        {
            if (this.Util.ChainContainsCard(CardId.ApprenticeLllusionMagician))
            {
                return false;
            }

            if (this.Duel.Phase == DuelPhase.Battle ||
                this.Duel.Phase == DuelPhase.BattleStart ||
                this.Duel.Phase == DuelPhase.BattleStep ||
                this.Duel.Phase == DuelPhase.Damage ||
                this.Duel.Phase == DuelPhase.DamageCal
               )
            {
                if (this.ActivateDescription == -1)
                {
                    Logger.DebugWriteLine("ApprenticeLllusionMagicianadd");
                    return true;
                }
                if (this.Card.IsDisabled())
                {
                    return false;
                }

                if ((this.Bot.BattlingMonster == null))
                {
                    return false;
                }

                if ((this.Enemy.BattlingMonster == null))
                {
                    return false;
                }

                if (this.Bot.BattlingMonster.Attack < this.Enemy.BattlingMonster.Attack)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        private bool SpellbookMagicianOfProphecysummon()
        {
            //AI.SelectPlace(Zones.z2, 1);
            if (this.lockbird_used)
            {
                return false;
            }

            if (this.Spellbook_summon)
            {
                if (this.Secret_used)
                {
                    this.AI.SelectCard(CardId.SpellbookOfKnowledge);
                }
                else
                {
                    this.AI.SelectCard(CardId.SpellbookOfSecrets, CardId.SpellbookOfKnowledge);
                }

                return true;
            }
            return false;
        }
        private bool MagiciansRodsummon()
        {
            if (this.lockbird_used)
            {
                return false;
            }
            //AI.SelectPlace(Zones.z2, 1);
            if (this.Rod_summon)
            {
                return true;
            }

            return true;
        }

        private bool DarkMagicAttackeff()
        {
            //AI.SelectPlace(Zones.z1, 2);
            return this.DefaultHarpiesFeatherDusterFirst();
        }
        private bool DarkMagicInheritanceeff()
        {
            if (this.lockbird_used)
            {
                return false;
            }

            IList<ClientCard> grave = this.Bot.Graveyard;
            IList<ClientCard> spell = new List<ClientCard>();
            int count = 0;
            foreach (ClientCard check in grave)
            {
                if (this.Card.HasType(CardType.Spell))
                {
                    spell.Add(check);
                    count++;
                }
            }
            if (count >= 2)
            {
                //AI.SelectPlace(Zones.z2, 2);
                this.AI.SelectCard(spell);
                if (this.Bot.HasInHandOrInSpellZone(CardId.EternalSoul) && this.Bot.HasInHandOrInSpellZone(CardId.DarkMagicalCircle))
                {
                    if (this.Bot.GetRemainingCount(CardId.DarkMagician, 3) >= 2 && !this.Bot.HasInHandOrInSpellZoneOrInGraveyard(CardId.LllusionMagic))
                    {
                        this.AI.SelectNextCard(CardId.LllusionMagic);
                        return true;
                    }
                }

                if (this.Bot.HasInHand(CardId.ApprenticeLllusionMagician) &&
                  (!this.Bot.HasInHandOrInSpellZone(CardId.EternalSoul) || !this.Bot.HasInHandOrInSpellZone(CardId.MagicianNavigation)))
                {
                    this.AI.SelectNextCard(CardId.MagicianNavigation);
                    return true;
                }
                if (this.Bot.HasInHandOrInSpellZone(CardId.EternalSoul) && !this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.DarkMagician) &&
                    !this.Bot.HasInHandOrInSpellZoneOrInGraveyard(CardId.LllusionMagic))
                {
                    this.AI.SelectNextCard(CardId.LllusionMagic);
                    return true;
                }

                if (this.Bot.HasInHandOrInSpellZone(CardId.MagicianNavigation) &&
                    !this.Bot.HasInHand(CardId.DarkMagician) &&
                    !this.Bot.HasInHandOrInSpellZone(CardId.EternalSoul) &&
                    this.Bot.GetRemainingCount(CardId.LllusionMagic, 1) > 0)
                {
                    this.AI.SelectNextCard(CardId.LllusionMagic);
                    return true;
                }

                if ((this.Bot.HasInHandOrInSpellZone(CardId.EternalSoul) || this.Bot.HasInHandOrInSpellZone(CardId.MagicianNavigation)) &&
                    !this.Bot.HasInHandOrInSpellZone(CardId.DarkMagicalCircle))
                {
                    this.AI.SelectNextCard(CardId.DarkMagicalCircle);
                    return true;
                }
                if (this.Bot.HasInHandOrInSpellZone(CardId.DarkMagicalCircle))
                {
                    if (this.Bot.HasInGraveyard(CardId.MagicianNavigation))
                    {
                        this.AI.SelectNextCard(CardId.EternalSoul, CardId.MagicianNavigation, CardId.DarkMagicalCircle);
                    }
                    else
                    {
                        this.AI.SelectNextCard(CardId.EternalSoul, CardId.MagicianNavigation, CardId.DarkMagicalCircle);
                    }

                    return true;
                }
                if (this.Bot.HasInGraveyard(CardId.MagicianNavigation))
                {
                    this.AI.SelectNextCard(CardId.EternalSoul, CardId.DarkMagicalCircle, CardId.MagicianNavigation);
                }
                else
                {
                    this.AI.SelectNextCard(CardId.MagicianNavigation, CardId.DarkMagicalCircle, CardId.EternalSoul);
                }

                return true;
            }
            return false;
        }
        private bool MagiciansRodeff()
        {
            if (this.Card.Location == CardLocation.MonsterZone)
            {
                if (this.Bot.HasInHandOrInSpellZone(CardId.EternalSoul) && this.Bot.HasInHandOrInSpellZone(CardId.DarkMagicalCircle))
                {
                    if (this.Bot.GetRemainingCount(CardId.DarkMagician, 3) >= 2 && this.Bot.GetRemainingCount(CardId.LllusionMagic, 1) > 0)
                    {
                        this.AI.SelectCard(CardId.LllusionMagic);
                        return true;
                    }
                }

                if (this.Bot.HasInHand(CardId.ApprenticeLllusionMagician) &&
                  !this.Bot.HasInHandOrInSpellZone(CardId.MagicianNavigation) &&
                  this.Bot.GetRemainingCount(CardId.MagicianNavigation, 3) > 0)
                {
                    this.AI.SelectCard(CardId.MagicianNavigation);
                    return true;
                }

                if (this.Bot.HasInHandOrInSpellZone(CardId.EternalSoul) &&
                    !this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.DarkMagician) &&
                    this.Bot.GetRemainingCount(CardId.LllusionMagic, 1) > 0)
                {
                    this.AI.SelectCard(CardId.LllusionMagic);
                    return true;
                }

                if (this.Bot.HasInHandOrInSpellZone(CardId.MagicianNavigation) &&
                    !this.Bot.HasInHand(CardId.DarkMagician) &&
                    !this.Bot.HasInHandOrInSpellZone(CardId.EternalSoul) &&
                    this.Bot.GetRemainingCount(CardId.LllusionMagic, 1) > 0)
                {
                    this.AI.SelectCard(CardId.LllusionMagic);
                    return true;
                }
                if (!this.Bot.HasInHandOrInSpellZone(CardId.EternalSoul) &&
                    this.Bot.HasInHandOrInSpellZone(CardId.DarkMagicalCircle) &&
                    this.Bot.HasInHandOrInSpellZone(CardId.MagicianNavigation) &&
                    this.Bot.GetRemainingCount(CardId.EternalSoul, 3) > 0)
                {
                    this.AI.SelectCard(CardId.EternalSoul);
                    return true;
                }
                if ((this.Bot.HasInHandOrInSpellZone(CardId.EternalSoul) || this.Bot.HasInHandOrInSpellZone(CardId.MagicianNavigation)) &&
                    !this.Bot.HasInHandOrInSpellZone(CardId.DarkMagicalCircle) &&
                    this.Bot.GetRemainingCount(CardId.DarkMagicalCircle, 3) > 0)
                {
                    this.AI.SelectCard(CardId.DarkMagicalCircle);
                    return true;
                }
                if (!this.Bot.HasInHandOrInSpellZone(CardId.EternalSoul) &&
                    !this.Bot.HasInHandOrInSpellZone(CardId.MagicianNavigation))
                {
                    if (this.Bot.HasInHand(CardId.DarkMagician) &&
                        !this.Bot.HasInGraveyard(CardId.MagicianNavigation) &&
                        this.Bot.GetRemainingCount(CardId.MagicianNavigation, 3) > 0
                        )
                    {
                        this.AI.SelectCard(CardId.MagicianNavigation);
                    }
                    else if (!this.Bot.HasInHandOrInSpellZone(CardId.DarkMagicalCircle))
                    {
                        this.AI.SelectCard(CardId.DarkMagicalCircle);
                    }
                    else
                    {
                        this.AI.SelectCard(CardId.EternalSoul);
                    }

                    return true;
                }
                if (!this.Bot.HasInHand(CardId.MagicianNavigation))
                {
                    this.AI.SelectCard(CardId.MagicianNavigation);
                    return true;
                }
                if (!this.Bot.HasInHand(CardId.DarkMagicalCircle))
                {
                    this.AI.SelectCard(CardId.DarkMagicalCircle);
                    return true;
                }
                if (!this.Bot.HasInHand(CardId.EternalSoul))
                {
                    this.AI.SelectCard(CardId.EternalSoul);
                    return true;
                }
                this.AI.SelectCard(CardId.LllusionMagic, CardId.EternalSoul, CardId.DarkMagicalCircle, CardId.MagicianNavigation);
                return true;
            }
            else
            {
                if (this.Bot.HasInMonstersZone(CardId.VentriloauistsClaraAndLucika))
                {
                    this.AI.SelectCard(CardId.VentriloauistsClaraAndLucika);
                    return true;
                }
                int Enemy_atk = 0;
                IList<ClientCard> list = new List<ClientCard>();
                foreach (ClientCard monster in this.Enemy.GetMonsters())
                {
                    if (monster.IsAttack())
                    {
                        list.Add(monster);
                    }
                }
                Enemy_atk = this.GetTotalATK(list);
                int bot_atk = 0;
                IList<ClientCard> list_1 = new List<ClientCard>();
                foreach (ClientCard monster in this.Bot.GetMonsters())
                {
                    if (this.Util.GetWorstBotMonster(true) != null)
                    {
                        if (monster.IsAttack() && monster.Id != this.Util.GetWorstBotMonster(true).Id)
                        {
                            list_1.Add(monster);
                        }
                    }
                }
                bot_atk = this.GetTotalATK(list);
                if (this.Bot.HasInHand(CardId.MagiciansRod))
                {
                    return false;
                }

                if (this.Bot.HasInMonstersZone(CardId.ApprenticeWitchling) && this.Bot.GetMonsterCount() == 1 && this.Bot.HasInSpellZone(CardId.EternalSoul))
                {
                    return false;
                }

                if (this.Bot.LifePoints <= (Enemy_atk - bot_atk) &&
                    this.Bot.GetMonsterCount() > 1)
                {
                    return false;
                }

                if ((this.Bot.LifePoints - Enemy_atk <= 1000) &&
                    this.Bot.GetMonsterCount() == 1)
                {
                    return false;
                }

                this.AI.SelectCard(CardId.VentriloauistsClaraAndLucika, CardId.SpellbookMagicianOfProphecy, CardId.WindwitchGlassBell, CardId.WindwitchIceBell, CardId.MagiciansRod, CardId.DarkMagician, CardId.MagicianOfLllusion);
                return true;
            }
        }

        private bool WindwitchGlassBellsummonfirst()
        {
            if (this.Bot.HasInMonstersZone(CardId.WindwitchIceBell) &&
                this.Bot.HasInMonstersZone(CardId.WindwitchSnowBell) &&
                !this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell))
            {
                return true;
            }

            return false;
        }
        private bool WindwitchGlassBellsummon()
        {
            if (this.lockbird_used)
            {
                return false;
            }

            if (!this.plan_A && (this.Bot.HasInGraveyard(CardId.WindwitchGlassBell) || this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell)))
            {
                return false;
            }
            //AI.SelectPlace(Zones.z2, 1);
            if (this.GlassBell_summon && this.Bot.HasInMonstersZone(CardId.WindwitchIceBell) &&
                !this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell))
            {
                return true;
            }

            if (this.WindwitchGlassBelleff_used)
            {
                return false;
            }

            if (this.GlassBell_summon)
            {
                return true;
            }

            return false;
        }
        private bool BigEyesp()
        {
            if (this.plan_C)
            {
                return false;
            }

            if (this.Util.IsOneEnemyBetterThanValue(2500, false) &&
                !this.Bot.HasInHandOrHasInMonstersZone(CardId.ApprenticeLllusionMagician))
            {
                //AI.SelectPlace(Zones.z5, Zones.ExtraMonsterZones);
                this.AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
            }
            return false;
        }

        private bool BigEyeeff()
        {
            ClientCard target = this.Util.GetBestEnemyMonster(false, true);
            if (target != null && target.Attack >= 2500)
            {
                this.AI.SelectCard(CardId.DarkMagician);
                this.AI.SelectNextCard(target);
                return true;
            }
            return false;

        }
        private bool Dracossacksp()
        {
            if (this.plan_C)
            {
                return false;
            }

            if (this.Util.IsOneEnemyBetterThanValue(2500, false) &&
                !this.Bot.HasInHandOrHasInMonstersZone(CardId.ApprenticeLllusionMagician))
            {
                //AI.SelectPlace(Zones.z5, Zones.ExtraMonsterZones);
                this.AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
            }
            return false;
        }

        private bool Dracossackeff()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.Dracossack, 0))
            {
                this.AI.SelectCard(CardId.DarkMagician);
                return true;

            }
            ClientCard target = this.Util.GetBestEnemyCard(false, true);
            if (target != null)
            {
                this.AI.SelectCard(CardId.Dracossack + 1);
                this.AI.SelectNextCard(target);
                return true;
            }
            return false;
        }

        private bool ApprenticeWitchlingsp()
        {
            int rod_count = 0;
            foreach (ClientCard rod in this.Bot.GetMonsters())
            {
                if (rod.IsCode(CardId.MagiciansRod))
                {
                    rod_count++;
                }
            }
            if (rod_count >= 2)
            {
                this.AI.SelectCard(CardId.MagiciansRod, CardId.MagiciansRod);
                return true;
            }
            if (this.Bot.HasInMonstersZone(CardId.DarkMagician) &&
                this.Bot.HasInMonstersZone(CardId.MagiciansRod) &&
                (this.Bot.HasInSpellZone(CardId.EternalSoul) || this.Bot.GetMonsterCount() >= 4)
                && this.Duel.Phase == DuelPhase.Main2)
            {
                if (rod_count >= 2)
                {
                    this.AI.SelectCard(CardId.MagiciansRod, CardId.MagiciansRod);
                }
                else
                {
                    this.AI.SelectCard(CardId.MagiciansRod, CardId.DarkMagician);
                }

                return true;
            }
            if (this.Bot.HasInMonstersZone(CardId.MagiciansRod) &&
                this.Bot.HasInMonstersZone(CardId.ApprenticeLllusionMagician) &&
                (this.Bot.HasInSpellZone(CardId.EternalSoul) || this.Bot.HasInSpellZone(CardId.MagicianNavigation))
                && this.Duel.Phase == DuelPhase.Main2)
            {
                if (rod_count >= 2)
                {
                    this.AI.SelectCard(CardId.MagiciansRod, CardId.MagiciansRod);
                }
                else
                {
                    this.AI.SelectCard(CardId.MagiciansRod, CardId.DarkMagician);
                }

                return true;
            }
            return false;
        }

        private bool ApprenticeWitchlingeff()
        {
            this.AI.SelectCard(CardId.MagiciansRod, CardId.DarkMagician, CardId.ApprenticeLllusionMagician);
            return true;
        }
        public override bool OnSelectHand()
        {
            return true;
        }

        private bool VentriloauistsClaraAndLucikasp()
        {
            if (this.Bot.HasInSpellZone(CardId.LllusionMagic))
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.MagiciansRod) && !this.Bot.HasInGraveyard(CardId.MagiciansRod) &&
                (this.Bot.HasInSpellZone(CardId.EternalSoul) || this.Bot.HasInSpellZone(CardId.MagicianNavigation)))
            {
                this.AI.SelectCard(CardId.MagiciansRod);
                return true;
            }
            return false;
        }
        public override void OnChaining(int player, ClientCard card)
        {
            base.OnChaining(player, card);
        }


        public override void OnChainEnd()
        {
            /*if (Enemy.MonsterZone[5] != null)
            {
                Logger.DebugWriteLine("%%%%%%%%%%%%%%%%Enemy.MonsterZone[5].LinkMarker= " + Enemy.MonsterZone[5].LinkMarker);
                Logger.DebugWriteLine("%%%%%%%%%%%%%%%%Enemy.MonsterZone[5].LinkLevel= " + Enemy.MonsterZone[5].LinkLevel);                
            }
                
            if (Enemy.MonsterZone[6] != null)
            {
                Logger.DebugWriteLine("%%%%%%%%%%%%%%%%Enemy.MonsterZone[6].LinkMarker= " + Enemy.MonsterZone[6].LinkMarker);
                Logger.DebugWriteLine("%%%%%%%%%%%%%%%%Enemy.MonsterZone[6].LinkLevel= " + Enemy.MonsterZone[6].LinkLevel);
            }
            for (int i = 0; i < 6; i++)
            {
                if (Enemy.MonsterZone[i] != null)
                    Logger.DebugWriteLine("++++++++MONSTER ZONE[" + i + "]= " + Enemy.MonsterZone[i].Attack);
            }
            for (int i = 0; i < 6; i++)
            {
                if (Bot.MonsterZone[i] != null)
                    Logger.DebugWriteLine("++++++++MONSTER ZONE[" + i + "]= " + Bot.MonsterZone[i].Id);
            }
            for (int i = 0; i < 4; i++)
            {
                if (Bot.SpellZone[i] != null)
                    Logger.DebugWriteLine("++++++++SpellZone[" + i + "]= " + Bot.SpellZone[i].Id);
            }*/
            if (this.Util.ChainContainsCard(CardId.MaxxC))
            {
                this.maxxc_used = true;
            }

            if ((this.Duel.CurrentChain.Count >= 1 && this.Util.GetLastChainCard().Id == 0) ||
                (this.Duel.CurrentChain.Count == 2 && !this.Util.ChainContainPlayer(0) && this.Duel.CurrentChain[0].Id == 0))
            {
                Logger.DebugWriteLine("current chain = " + this.Duel.CurrentChain.Count);
                Logger.DebugWriteLine("******last chain card= " + this.Util.GetLastChainCard().Id);
                int maxxc_count = 0;
                foreach (ClientCard check in this.Enemy.Graveyard)
                {
                    if (check.IsCode(CardId.MaxxC))
                    {
                        maxxc_count++;
                    }
                }
                if (maxxc_count != this.maxxc_done)
                {
                    Logger.DebugWriteLine("************************last chain card= " + this.Util.GetLastChainCard().Id);
                    this.maxxc_used = true;
                }               
                int lockbird_count = 0;
                foreach (ClientCard check in this.Enemy.Graveyard)
                {
                    if (check.IsCode(CardId.LockBird))
                    {
                        lockbird_count++;
                    }
                }
                if (lockbird_count != this.lockbird_done)
                {
                    Logger.DebugWriteLine("************************last chain card= " + this.Util.GetLastChainCard().Id);
                    this.lockbird_used = true;
                }
                int ghost_count = 0;
                foreach (ClientCard check in this.Enemy.Graveyard)
                {
                    if (check.IsCode(CardId.Ghost))
                    {
                        ghost_count++;
                    }
                }
                if (ghost_count != this.ghost_done)
                {
                    Logger.DebugWriteLine("************************last chain card= " + this.Util.GetLastChainCard().Id);
                    this.ghost_used = true;
                }
                if (this.ghost_used && this.Util.ChainContainsCard(CardId.WindwitchGlassBell))
                {
                    this.AI.SelectCard(CardId.WindwitchIceBell);
                    Logger.DebugWriteLine("***********WindwitchGlassBell*********************");
                }

            }
            foreach (ClientCard dangerous in this.Enemy.GetMonsters())
            {
                if (dangerous != null && dangerous.IsShouldNotBeTarget() &&
                    (dangerous.Attack > 2500 || dangerous.Defense > 2500) &&
                    !this.Bot.HasInHandOrHasInMonstersZone(CardId.ApprenticeLllusionMagician))
                {
                    this.plan_C = true;
                    Logger.DebugWriteLine("*********dangerous = " + dangerous.Id);
                }
            }
            int count = 0;
            foreach (ClientCard check in this.Enemy.Graveyard)
            {
                if (check.IsCode(CardId.MaxxC))
                {
                    count++;
                }
            }
            this.maxxc_done = count;
            count = 0;
            foreach (ClientCard check in this.Enemy.Graveyard)
            {
                if (check.IsCode(CardId.LockBird))
                {
                    count++;
                }
            }
            this.lockbird_done = count;
            count = 0;
            foreach (ClientCard check in this.Enemy.Graveyard)
            {
                if (check.IsCode(CardId.Ghost))
                {
                    count++;
                }
            }
            this.ghost_done = count;
            base.OnChainEnd();
        }


        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            /*
            if (Enemy.HasInMonstersZone(CardId.MekkKnightMorningStar))
            {
                attackerzone = -1;
                defenderzone = -1;
                for (int a = 0; a <= 6; a++)
                    for (int b = 0; b <= 6; b++)
                    {
                        if (Bot.MonsterZone[a] != null && Enemy.MonsterZone[b] != null &&
                            SameMonsterColumn(a, b) &&
                            Bot.MonsterZone[a].IsCode(attacker.Id) && Enemy.MonsterZone[b].IsCode(defender.Id))
                        {
                            attackerzone = a;
                            defenderzone = b;
                        }
                    }
                Logger.DebugWriteLine("**********attack_zone= " + attackerzone + "  defender_zone= " + defenderzone);
                if (!SameMonsterColumn(attackerzone, defenderzone) && IsJackKnightMonster(defenderzone))
                {
                    Logger.DebugWriteLine("**********cant attack ");
                    return false;
                }
            }
            */
            //Logger.DebugWriteLine("@@@@@@@@@@@@@@@@@@@ApprenticeLllusionMagician= " + ApprenticeLllusionMagician_count);            
            if (this.Bot.HasInSpellZone(CardId.OddEyesWingDragon))
            {
                this.big_attack = true;
            }

            if (this.Duel.Player == 0 && this.Bot.GetMonsterCount() >= 2 && this.plan_C)
            {
                Logger.DebugWriteLine("*********dangerous********************* ");
                if (attacker.IsCode(CardId.OddEyesAbsoluteDragon, CardId.OddEyesWingDragon))
                {
                    attacker.RealPower = 9999;
                }
            }
            if ((attacker.IsCode(CardId.DarkMagician, CardId.MagiciansRod, CardId.BigEye, CardId.ApprenticeWitchling)) &&
                this.Bot.HasInHandOrHasInMonstersZone(CardId.ApprenticeLllusionMagician))
            {
                attacker.RealPower += 2000;
            }
            if (attacker.IsCode(CardId.ApprenticeLllusionMagician) && this.ApprenticeLllusionMagician_count >= 2)
            {
                attacker.RealPower += 2000;
            }
            if (attacker.IsCode(CardId.DarkMagician, CardId.DarkMagicianTheDragonKnight) && this.Bot.HasInSpellZone(CardId.EternalSoul))
            {
                return true;
            }
            if (attacker.IsCode(CardId.CrystalWingSynchroDragon))
            {
                if (defender.Level >= 5)
                {
                    attacker.RealPower = 9999;
                }

                if (this.CrystalWingSynchroDragon_used == false)
                {
                    return true;
                }
            }
            if (!this.big_attack_used && this.big_attack)
            {
                attacker.RealPower = 9999;
                this.big_attack_used = true;
                return true;
            }
            if (attacker.IsCode(CardId.ApprenticeLllusionMagician))
            {
                Logger.DebugWriteLine("@@@@@@@@@@@@@@@@@@@ApprenticeLllusionMagician= " + attacker.RealPower);
            }

            if (this.Bot.HasInSpellZone(CardId.EternalSoul) && attacker.IsCode(CardId.DarkMagician, CardId.DarkMagicianTheDragonKnight, CardId.MagicianOfLllusion))
            {
                return true;
            }

            return base.OnPreBattleBetween(attacker, defender);
        }
        /*
        public override BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
        {
            for (int i = 0; i < defenders.Count; ++i)
            {
                ClientCard defender = defenders[i];
                if (Enemy.HasInMonstersZone(CardId.MekkKnightMorningStar))
                {
                    for (int b = 0; b <= 6; b++)
                    {
                        if (Enemy.MonsterZone[b] != null &&
                            SameMonsterColumn(attackerzone, b) &&
                            Bot.MonsterZone[attackerzone].IsCode(attacker.Id) && Enemy.MonsterZone[b].IsCode(defender.Id))
                        {
                            defenderzone = b;
                        }
                    }
                    if (defenderzone == -1)
                    {
                        Logger.DebugWriteLine("**********firstattackerzone= " + attackerzone + "  firstTargetzone= " + defenderzone);

                        return null;
                    }
                }
            }
            defenderzone = -1;
            return base.OnSelectAttackTarget(attacker,defenders);
        }
        */
        /*
        public override ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
        {            
            for (int i = 0; i < attackers.Count; ++i)
            {                
                ClientCard attacker = attackers[i];
                for(int j = 0;j < defenders.Count;++j)
                {
                    ClientCard defender = defenders[j];
                    if (Enemy.HasInMonstersZone(CardId.MekkKnightMorningStar))
                    {
                        attackerzone = -1;
                        defenderzone = -1;
                        for(int a = 0;a <= 6;a++)
                            for(int b = 0;b <= 6;b++)
                            {
                                if (Bot.MonsterZone[a] != null && Enemy.MonsterZone[b]!=null &&
                                    SameMonsterColumn(a,b) && 
                                    Bot.MonsterZone[a].IsCode(attacker.Id) && Enemy.MonsterZone[b].IsCode(defender.Id))
                                {
                                    attackerzone = a;
                                    defenderzone = b;
                                }
                            }
                           
                        if (defenderzone != -1)                            
                            {                                
                                Logger.DebugWriteLine("**********firstattackerzone= " + attackerzone + "  firstdefenderzone= " + defenderzone);
                                return attacker;
                            }                       
                    }
                }                
            }
            return base.OnSelectAttacker(attackers,defenders);
        }
        */
        public bool MonsterRepos()
        {
            if (this.Bot.HasInMonstersZone(CardId.OddEyesWingDragon) ||
                this.Bot.HasInSpellZone(CardId.OddEyesWingDragon) ||
                this.Bot.HasInMonstersZone(CardId.OddEyesAbsoluteDragon))
            {
                if (this.Card.IsAttack())
                {
                    return false;
                }
            }
            if (this.Bot.HasInMonstersZone(CardId.ApprenticeLllusionMagician) || (this.Bot.HasInHand(CardId.ApprenticeLllusionMagician)))
            {
                if (this.Card.IsAttack())
                {
                    return false;
                }
            }
            if (this.Card.IsFacedown())
            {
                return true;
            }

            return base.DefaultMonsterRepos();
        }

    }
}