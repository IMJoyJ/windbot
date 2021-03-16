using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("LightswornShaddoldinosour", "AI_LightswornShaddoldinosour")]
    public class LightswornShaddoldinosour : DefaultExecutor
    {
        public class CardId
        {
            //monster
            public const int UltimateConductorTytanno = 18940556;
            public const int DogorantheMadFlameKaiju = 93332803;
            public const int GamecieltheSeaTurtleKaiju = 55063751;
            public const int RadiantheMultidimensionalKaiju = 28674152;
            public const int OvertexCoatls = 41782653;
            public const int ShaddollBeast = 3717252;
            public const int GiantRex = 80280944;
            public const int ShaddollDragon = 77723643;
            public const int FairyTailSnow = 55623480;
            public const int KeeperOfDragonicMagic = 48048590;
            public const int ShaddollSquamata = 30328508;
            public const int SouleatingOviraptor = 44335251;
            public const int Raiden = 77558536;
            public const int Lumina = 95503687;
            public const int ShaddollHedgehog = 4939890;
            public const int AshBlossom = 14558127;
            public const int GhostOgre = 59438930;
            public const int ShaddollFalco = 37445295;
            public const int MaxxC = 23434538;
            public const int PlaguespreaderZombie = 33420078;
            public const int GlowUpBulb = 67441435;

            //spell
            public const int AllureofDarkness = 1475311;
            public const int ThatGrassLooksgreener = 11110587;
            public const int HarpiesFeatherDuster = 18144506;
            public const int DoubleEvolutionPill = 38179121;
            public const int ShaddollFusion = 44394295;
            public const int PotOfAvarice = 67169062;
            public const int FoolishBurial = 81439173;
            public const int MonsterReborn = 83764718;
            public const int ChargeOfTheLightBrigade = 94886282;
            public const int InterruptedKaijuSlumber = 99330325;
            //public const int ElShaddollFusion = 6417578;

            //trap
            public const int infiniteTransience = 10045474;
            public const int LostWind = 74003290;
            public const int SinisterShadowGames = 77505534;
            public const int ShaddollCore = 4904633;

            //extra
            public const int ElShaddollShekhinaga = 74822425;
            public const int ElShaddollConstruct = 20366274;
            public const int ElShaddollGrysra = 48424886;
            public const int ElShaddollWinda = 94977269;
            public const int CrystalWingSynchroDragon = 50954680;
            public const int ScarlightRedDragon = 80666118;
            public const int Michael = 4779823;
            public const int BlackRoseMoonlightDragon = 33698022;
            public const int RedWyvern = 76547525;
            public const int CoralDragon = 42566602;
            public const int TG_WonderMagician = 98558751;
            public const int MinervaTheExalte = 30100551;
            public const int Sdulldeat = 74997493;
            public const int CrystronNeedlefiber = 50588353;
        }

        

        public LightswornShaddoldinosour(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            //counter            
            this.AddExecutor(ExecutorType.Activate, CardId.GhostOgre, this.Hand_act_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.AshBlossom, this.Hand_act_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.MaxxC, this.MaxxC);
            //first do
            this.AddExecutor(ExecutorType.Activate, CardId.HarpiesFeatherDuster, this.DefaultHarpiesFeatherDusterFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.infiniteTransience, this.DefaultBreakthroughSkill);
            this.AddExecutor(ExecutorType.Activate, CardId.ThatGrassLooksgreener);
            this.AddExecutor(ExecutorType.Summon, CardId.SouleatingOviraptor);
            this.AddExecutor(ExecutorType.Activate, CardId.SouleatingOviraptor, this.SouleatingOviraptoreff);
            this.AddExecutor(ExecutorType.Activate, CardId.AllureofDarkness, this.DefaultAllureofDarkness);
            this.AddExecutor(ExecutorType.Activate, CardId.PotOfAvarice, this.PotofAvariceeff);
            this.AddExecutor(ExecutorType.Activate, CardId.ChargeOfTheLightBrigade, this.ChargeOfTheLightBrigadeEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.FoolishBurial, this.FoolishBurialEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.InterruptedKaijuSlumber, this.InterruptedKaijuSlumbereff);
            this.AddExecutor(ExecutorType.Activate, CardId.ShaddollFusion, this.ShaddollFusioneff);
            //Normal Summon            
            this.AddExecutor(ExecutorType.Summon, CardId.Raiden);
            this.AddExecutor(ExecutorType.Activate, CardId.Raiden);
            this.AddExecutor(ExecutorType.Summon , CardId.KeeperOfDragonicMagic);
            this.AddExecutor(ExecutorType.Activate, CardId.KeeperOfDragonicMagic, this.KeeperOfDragonicMagiceff);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.ShaddollSquamata);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.GlowUpBulb);
            this.AddExecutor(ExecutorType.Summon, CardId.Lumina, this.Luminasummon);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.ShaddollHedgehog);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.ShaddollDragon);
            this.AddExecutor(ExecutorType.Summon, CardId.FairyTailSnow, this.FairyTailSnowsummon);
            this.AddExecutor(ExecutorType.Activate, CardId.FairyTailSnow, this.FairyTailSnoweff);
            this.AddExecutor(ExecutorType.Activate, CardId.Lumina, this.Luminaeff);
            //activate
            this.AddExecutor(ExecutorType.Activate, CardId.GlowUpBulb, this.GlowUpBulbeff);
            this.AddExecutor(ExecutorType.Activate, CardId.TG_WonderMagician, this.TG_WonderMagicianeff);
            this.AddExecutor(ExecutorType.Activate, CardId.CoralDragon, this.CoralDragoneff);
            this.AddExecutor(ExecutorType.Activate, CardId.RedWyvern, this.RedWyverneff);
            this.AddExecutor(ExecutorType.Activate, CardId.CrystalWingSynchroDragon, this.CrystalWingSynchroDragoneff);
            this.AddExecutor(ExecutorType.Activate, CardId.BlackRoseMoonlightDragon, this.BlackRoseMoonlightDragoneff);
            this.AddExecutor(ExecutorType.Activate, CardId.Sdulldeat, this.Sdulldeateff);
            this.AddExecutor(ExecutorType.Activate, CardId.Michael, this.Michaeleff);
            this.AddExecutor(ExecutorType.Activate, CardId.ScarlightRedDragon, this.ScarlightRedDragoneff);
            //Sp Summon

            this.AddExecutor(ExecutorType.Activate, CardId.CrystronNeedlefiber, this.CrystronNeedlefibereff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.UltimateConductorTytanno, this.UltimateConductorTytannosp);
            this.AddExecutor(ExecutorType.Activate, CardId.UltimateConductorTytanno, this.UltimateConductorTytannoeff);
            this.AddExecutor(ExecutorType.Activate, CardId.DoubleEvolutionPill, this.DoubleEvolutionPilleff);
            //extra
            this.AddExecutor(ExecutorType.SpSummon, CardId.CrystalWingSynchroDragon);
            this.AddExecutor(ExecutorType.Activate, CardId.CrystalWingSynchroDragon, this.CrystalWingSynchroDragoneff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.ScarlightRedDragon, this.ScarlightRedDragonsp);
            this.AddExecutor(ExecutorType.Activate, CardId.ScarlightRedDragon, this.ScarlightRedDragoneff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Michael, this.Michaelsp);
            this.AddExecutor(ExecutorType.Activate, CardId.Michael, this.Michaeleff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.RedWyvern, this.RedWyvernsp);
            this.AddExecutor(ExecutorType.Activate, CardId.RedWyvern, this.RedWyverneff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.MinervaTheExalte);
            this.AddExecutor(ExecutorType.Activate, CardId.MinervaTheExalte, this.MinervaTheExaltedEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.CrystronNeedlefiber, this.CrystronNeedlefibersp);
            //Kaiju
            this.AddExecutor(ExecutorType.SpSummon, CardId.GamecieltheSeaTurtleKaiju, this.GamecieltheSeaTurtleKaijusp);
            this.AddExecutor(ExecutorType.SpSummon, CardId.RadiantheMultidimensionalKaiju, this.RadiantheMultidimensionalKaijusp);
            this.AddExecutor(ExecutorType.SpSummon, CardId.DogorantheMadFlameKaiju, this.DogorantheMadFlameKaijusp);
            //Reborn
            this.AddExecutor(ExecutorType.Activate, CardId.MonsterReborn, this.Reborneff);
            //activate chain
            this.AddExecutor(ExecutorType.Activate, CardId.OvertexCoatls, this.OvertexCoatlseff);
            this.AddExecutor(ExecutorType.Activate, CardId.ShaddollCore, this.ShaddollCoreeff);
            this.AddExecutor(ExecutorType.Activate, CardId.ShaddollBeast, this.ShaddollBeasteff);
            this.AddExecutor(ExecutorType.Activate, CardId.ShaddollFalco, this.ShaddollFalcoeff);
            this.AddExecutor(ExecutorType.Activate, CardId.ShaddollDragon, this.ShaddollDragoneff);
            this.AddExecutor(ExecutorType.Activate, CardId.ShaddollHedgehog, this.ShaddollHedgehogeff);
            this.AddExecutor(ExecutorType.Activate, CardId.ShaddollSquamata, this.ShaddollSquamataeff);
            this.AddExecutor(ExecutorType.Activate, CardId.GiantRex);
            this.AddExecutor(ExecutorType.Activate, CardId.ElShaddollConstruct, this.ElShaddollConstructeff);
            this.AddExecutor(ExecutorType.Activate, CardId.ElShaddollGrysra, this.ElShaddollGrysraeff);
            this.AddExecutor(ExecutorType.Activate, CardId.ElShaddollShekhinaga, this.ElShaddollShekhinagaeff);
            this.AddExecutor(ExecutorType.Activate, CardId.ElShaddollWinda);
            //spellset          
            this.AddExecutor(ExecutorType.SpellSet, CardId.ThatGrassLooksgreener, this.SpellSetZone);
            this.AddExecutor(ExecutorType.SpellSet, this.SpellSetZone);
            //trapset
            this.AddExecutor(ExecutorType.SpellSet, CardId.LostWind);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SinisterShadowGames);
            this.AddExecutor(ExecutorType.SpellSet, CardId.ShaddollCore);
            this.AddExecutor(ExecutorType.SpellSet, CardId.infiniteTransience, this.SetIsFieldEmpty);
            //trap activate
            this.AddExecutor(ExecutorType.Activate, CardId.LostWind, this.LostWindeff);
            this.AddExecutor(ExecutorType.Activate, CardId.SinisterShadowGames, this.SinisterShadowGameseff);

            this.AddExecutor(ExecutorType.Repos, this.MonsterRepos);
        }
        public int[] all_List()
        {
            return new[]
            {
                CardId.UltimateConductorTytanno,
                CardId.DogorantheMadFlameKaiju,
                CardId.GamecieltheSeaTurtleKaiju,
                CardId.RadiantheMultidimensionalKaiju,
                CardId.OvertexCoatls,
                CardId.ShaddollBeast,
                CardId.GiantRex,
                CardId.ShaddollDragon,
                CardId.FairyTailSnow,
                CardId.KeeperOfDragonicMagic,
                CardId.ShaddollSquamata,
                CardId.SouleatingOviraptor,
                CardId.Raiden,
                CardId.Lumina,
                CardId.ShaddollHedgehog,
                CardId.AshBlossom,
                CardId.GhostOgre,
                CardId.ShaddollFalco,
                CardId.MaxxC,
                CardId.PlaguespreaderZombie,
                CardId.GlowUpBulb,

                CardId.AllureofDarkness,
                CardId.ThatGrassLooksgreener,
                CardId.HarpiesFeatherDuster,
                CardId.DoubleEvolutionPill,
                CardId.ShaddollFusion,
                CardId.PotOfAvarice,
                CardId.FoolishBurial,
                CardId.MonsterReborn,
                CardId.ChargeOfTheLightBrigade,
                CardId.InterruptedKaijuSlumber,
                //CardId.ElShaddollFusion,

                CardId.infiniteTransience,
                CardId.LostWind,
                CardId.SinisterShadowGames,
                CardId.ShaddollCore,


            };
        }
        public int[] Useless_List()
        {
            return new[]
            {
                CardId.GlowUpBulb,
                CardId.PlaguespreaderZombie,
                CardId.ChargeOfTheLightBrigade,                
                CardId.ThatGrassLooksgreener,
                CardId.HarpiesFeatherDuster,
                CardId.FairyTailSnow,
                CardId.GiantRex,
                CardId.Lumina,
                CardId.OvertexCoatls,
                CardId.InterruptedKaijuSlumber,                
                CardId.FoolishBurial,
            };
        }
        int Ultimate_ss = 0;
        int Enemy_atk = 0;
        int TG_WonderMagician_count = 0;
        bool Pillused = false;
        bool CrystronNeedlefibereff_used = false;
        bool OvertexCoatlseff_used = false;
        bool ShaddollBeast_used = false;
        bool ShaddollFalco_used = false;
        bool ShaddollSquamata_used = false;
        bool ShaddollDragon_used = false;
        bool ShaddollHedgehog_used = false;

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

        public override void OnNewPhase()
        {
            this.Enemy_atk = 0;
            IList<ClientCard> list = new List<ClientCard>();
            foreach (ClientCard monster in this.Enemy.GetMonsters())
            {
                if(monster.IsAttack())
                {
                    list.Add(monster);
                }
            }
            //if (GetTotalATK(list) / 2 >= Bot.LifePoints) return false;
            this.Enemy_atk = this.GetTotalATK(list);
            //SLogger.DebugWriteLine("++++++++++++++++++" + Enemy_atk + "++++++++++++");
        }
        public override void OnNewTurn()
        {
            this.Pillused = false;
            this.OvertexCoatlseff_used = false;
            this.CrystronNeedlefibereff_used = false;
            this.ShaddollBeast_used = false;
            this.ShaddollFalco_used = false;
            this.ShaddollSquamata_used = false;
            this.ShaddollDragon_used = false;
            this.ShaddollHedgehog_used = false;
            this.TG_WonderMagician_count = 0;
        }

        private bool Luminasummon()
        {
            if (this.Bot.Deck.Count >= 20)
            {
                return true;
            }

            IList<ClientCard> extra = this.Bot.GetMonstersInExtraZone();
            if (extra != null)
            {
                foreach (ClientCard monster in extra)
                {
                    if (!monster.HasType(CardType.Link))
                    {
                        return false;
                    }
                }
            }

            if (this.Bot.LifePoints <= 3000)
            {
                return true;
            }

            if (this.Bot.HasInGraveyard(CardId.Raiden))
            {
                return true;
            }

            return false;
        }
        private bool Luminaeff()
        {
            if (this.Bot.HasInGraveyard(CardId.Raiden))
            {
                this.AI.SelectCard(this.Useless_List());
                this.AI.SelectNextCard(CardId.Raiden);
                return true;
            }
            return false;
        }


        private bool UltimateConductorTytannoeff()
        {
            IList<int> targets = new[] {
                CardId.OvertexCoatls,
                CardId.ShaddollBeast,
                CardId.ShaddollSquamata,
                CardId.ShaddollHedgehog,
                CardId.ShaddollDragon,
                CardId.ShaddollFalco,
                CardId.GlowUpBulb,
                CardId.PlaguespreaderZombie,
                CardId.FairyTailSnow,
                CardId.KeeperOfDragonicMagic,
                CardId.DogorantheMadFlameKaiju,
                CardId.GamecieltheSeaTurtleKaiju,
                CardId.RadiantheMultidimensionalKaiju,
                CardId.GiantRex,
                CardId.ShaddollCore,
                CardId.SouleatingOviraptor,
                CardId.Raiden,
                CardId.Lumina,
                CardId.AshBlossom,
                CardId.GhostOgre,
                CardId.MaxxC,
                };

            if (this.Duel.Phase == DuelPhase.Main1)
            {
                if(this.Duel.Player==0)
                {
                    int count = 0;
                    IList<ClientCard> check = this.Enemy.GetMonsters();
                    foreach (ClientCard monster in check)
                    {
                        if (monster.Attack > 2500 || monster == this.Enemy.MonsterZone.GetDangerousMonster())
                        {
                            count++;
                        }
                    }

                    if (count==0)
                    {
                        return false;
                    }
                }               
                if (!this.Bot.HasInHand(targets))
                {
                    if(!this.Bot.HasInMonstersZone(targets))
                    {
                        return false;
                    }
                }
                this.AI.SelectCard(targets);
                return true;
            }
            if (this.Duel.Phase == DuelPhase.BattleStart)
            {
                this.AI.SelectYesNo(true);
                return true;
            }
            return false;    
            
        }

        private bool GamecieltheSeaTurtleKaijusp()
        {
            if (!this.Bot.HasInMonstersZone(CardId.UltimateConductorTytanno))
            {
                return this.DefaultKaijuSpsummon();
            }

            return false;
        }

        private bool RadiantheMultidimensionalKaijusp()
        {
            if (this.Enemy.HasInMonstersZone(CardId.GamecieltheSeaTurtleKaiju))
            {
                return true;
            }

            if (this.Bot.HasInHand(CardId.DogorantheMadFlameKaiju) && !this.Bot.HasInMonstersZone(CardId.UltimateConductorTytanno))
            {
                return this.DefaultKaijuSpsummon();
            }

            return false;
        }


        private bool DogorantheMadFlameKaijusp()
        {
            if (this.Enemy.HasInMonstersZone(CardId.GamecieltheSeaTurtleKaiju))
            {
                return true;
            }

            if (this.Enemy.HasInMonstersZone(CardId.RadiantheMultidimensionalKaiju))
            {
                return true;
            }

            return false;
        }


        private bool InterruptedKaijuSlumbereff()
        {
            if (this.Enemy.GetMonsterCount() - this.Bot.GetMonsterCount() >= 2 )
            {
                return this.DefaultInterruptedKaijuSlumber();
            }

            return false;
        }
        private bool UltimateConductorTytannosp()
        {

            this.Pillused = true;
            foreach (ClientCard card in this.Bot.GetMonsters())
            {
                if (card.IsCode(CardId.UltimateConductorTytanno) && card.IsFaceup())
                {
                    return false;
                }
            }
            this.Ultimate_ss++;
            return true;

        }

        private bool KeeperOfDragonicMagiceff()
        {
            if (this.ActivateDescription == -1)
            {
                this.AI.SelectCard(this.Useless_List());
                return true;
            }
            return true;
        }

        private bool MonsterRepos()
        {
            if (this.Card.IsCode(CardId.UltimateConductorTytanno) && this.Card.IsFacedown())
            {
                return true;
            }

            if (this.Card.IsCode(CardId.ElShaddollConstruct) && this.Card.IsFacedown())
            {
                return true;
            }

            if (this.Card.IsCode(CardId.ElShaddollConstruct) && this.Card.IsAttack())
            {
                return false;
            }

            if (this.Card.IsCode(CardId.GlowUpBulb) && this.Card.IsDefense())
            {
                return false;
            }

            if (this.Card.IsCode(CardId.ShaddollDragon) && this.Card.IsFacedown() && this.Enemy.GetMonsterCount() >= 0)
            {
                return true;
            }

            if (this.Card.IsCode(CardId.ShaddollSquamata) && this.Card.IsFacedown() && this.Enemy.GetMonsterCount() >= 0)
            {
                return true;
            }

            return base.DefaultMonsterRepos();
        }

        private bool OvertexCoatlseff()
        {
            if (this.Card.Location == CardLocation.MonsterZone)
            {
                return false;
            }

            this.OvertexCoatlseff_used = true;
            return true;
        }

        private bool DoubleEvolutionPilleff()
        {          
            foreach (ClientCard card in this.Bot.GetMonsters())
            {
                if (card.IsCode(CardId.UltimateConductorTytanno) && card.IsFaceup())
                {
                    return false;
                }
            }
            if (this.Pillused == true)
            {
                return false;
            }

            this.Pillused = true;
            IList<int> targets = new[] {
                    CardId.GiantRex,
                    CardId.DogorantheMadFlameKaiju,
                    CardId.GamecieltheSeaTurtleKaiju,
                    CardId.RadiantheMultidimensionalKaiju,
                    CardId.OvertexCoatls,
                    CardId.SouleatingOviraptor,
                    CardId.UltimateConductorTytanno,
                };
            if (this.Bot.HasInGraveyard(targets))
            {
                this.AI.SelectCard(CardId.GiantRex, CardId.DogorantheMadFlameKaiju, CardId.OvertexCoatls, CardId.GamecieltheSeaTurtleKaiju, CardId.RadiantheMultidimensionalKaiju, CardId.SouleatingOviraptor, CardId.UltimateConductorTytanno);
            }
            else
            {
                this.AI.SelectCard(CardId.GiantRex, CardId.DogorantheMadFlameKaiju, CardId.GamecieltheSeaTurtleKaiju, CardId.RadiantheMultidimensionalKaiju, CardId.OvertexCoatls, CardId.SouleatingOviraptor, CardId.UltimateConductorTytanno);
            }
            IList<int> targets2 = new[] {
                    CardId.GiantRex,
                    CardId.DogorantheMadFlameKaiju,
                    CardId.GamecieltheSeaTurtleKaiju,
                    CardId.RadiantheMultidimensionalKaiju,
                    CardId.OvertexCoatls,
                    CardId.SouleatingOviraptor,
                    CardId.UltimateConductorTytanno,
                };
            if (this.Bot.HasInGraveyard(targets))
            {
                this.AI.SelectNextCard(CardId.ShaddollBeast, CardId.ShaddollDragon, CardId.KeeperOfDragonicMagic, CardId.ShaddollSquamata, CardId.SouleatingOviraptor, CardId.Raiden, CardId.Lumina, CardId.ShaddollHedgehog, CardId.AshBlossom, CardId.GhostOgre, CardId.ShaddollFalco, CardId.MaxxC, CardId.PlaguespreaderZombie, CardId.GlowUpBulb, CardId.FairyTailSnow);
            }
            else
            {
                this.AI.SelectNextCard(CardId.ShaddollBeast, CardId.ShaddollDragon, CardId.KeeperOfDragonicMagic, CardId.ShaddollSquamata, CardId.SouleatingOviraptor, CardId.Raiden, CardId.Lumina, CardId.ShaddollHedgehog, CardId.AshBlossom, CardId.GhostOgre, CardId.ShaddollFalco, CardId.MaxxC, CardId.PlaguespreaderZombie, CardId.GlowUpBulb, CardId.FairyTailSnow);
            }

            this.AI.SelectThirdCard(new[] {
                    CardId.UltimateConductorTytanno,

                });

            return this.Enemy.GetMonsterCount() >= 1;
        }

        
        private bool FairyTailSnowsummon()
        {
            ClientCard target = this.Util.GetBestEnemyMonster(true, true);
            if(target != null)
            {
                return true;
            }            
            return false;
        }


        private bool FairyTailSnoweff()
        {

            if (this.Card.Location == CardLocation.MonsterZone)
            {
                this.AI.SelectCard(this.Util.GetBestEnemyMonster(true, true));
                return true;
            }
            else
            {
               
                int spell_count = 0;
                IList<ClientCard> grave = this.Bot.Graveyard;               
                IList<ClientCard> all = new List<ClientCard>();
                foreach (ClientCard check in grave)
                {
                    if (check.IsCode(CardId.GiantRex))
                    {
                        all.Add(check);
                    }
                }
                foreach (ClientCard check in grave)
                    {
                        if(check.HasType(CardType.Spell)||check.HasType(CardType.Trap))
                        {
                            spell_count++;
                            all.Add(check);
                        }                        
                    }
                foreach (ClientCard check in grave)
                {
                    if (check.HasType(CardType.Monster))
                    {                       
                        all.Add(check);
                    }
                }
                if (this.Util.ChainContainsCard(CardId.FairyTailSnow))
                {
                    return false;
                }

                if (this.Duel.Player == 1  && this.Duel.Phase == DuelPhase.BattleStart && this.Bot.BattlingMonster == null && this.Enemy_atk >= this.Bot.LifePoints ||
                    this.Duel.Player == 0 && this.Duel.Phase==DuelPhase.BattleStart && this.Enemy.BattlingMonster == null && this.Enemy.LifePoints<=1850
                    )
                {
                    this.AI.SelectCard(all);
                    this.AI.SelectNextCard(this.Util.GetBestEnemyMonster());
                    return true;
                }
            }
            return false;
        }


        private bool SouleatingOviraptoreff()
        {
            if (!this.OvertexCoatlseff_used && this.Bot.GetRemainingCount(CardId.OvertexCoatls, 3) > 0)
            {
                this.AI.SelectCard(CardId.OvertexCoatls);
                this.AI.SelectOption(0);
            }
            else
            {
                this.AI.SelectCard(CardId.UltimateConductorTytanno);
                this.AI.SelectOption(1);
            }
            return true;
        }

        private bool GlowUpBulbeff()
        {
            IList<ClientCard> check = this.Bot.GetMonstersInExtraZone();
            foreach (ClientCard monster in check)
            {
                if (monster.HasType(CardType.Fusion))
                {
                    return false;
                }
            }

            if (this.Bot.HasInMonstersZone(CardId.Lumina) ||
               this.Bot.HasInMonstersZone(CardId.FairyTailSnow) ||
               this.Bot.HasInMonstersZone(CardId.KeeperOfDragonicMagic) ||
               this.Bot.HasInMonstersZone(CardId.SouleatingOviraptor) ||
               this.Bot.HasInMonstersZone(CardId.GiantRex) ||
               this.Bot.HasInMonstersZone(CardId.Raiden)
               )
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;   
        }       
       
        private bool TG_WonderMagicianeff()
        {
            this.TG_WonderMagician_count++;
            return this.TG_WonderMagician_count <= 10;
        }
        private bool AllureofDarkness()
        {
            IList<ClientCard> materials = this.Bot.Hand;
           // IList<ClientCard> check = new List<ClientCard>();
            ClientCard mat = null;
            foreach (ClientCard card in materials)
            {
                if (card.HasAttribute(CardAttribute.Dark))
                {
                    mat = card;
                    break;
                }
            }
            if (mat != null)
            {
                return true;
            }
            return false;
        }
             

        private bool Reborneff()
        {
            if(this.Bot.HasInGraveyard(CardId.UltimateConductorTytanno)&& this.Ultimate_ss >0)
            {
                this.AI.SelectCard(CardId.UltimateConductorTytanno);
                return true;
            }
            if (!this.Util.IsOneEnemyBetter(true))
            {
                return false;
            }

            IList<int> targets = new[] {                    
                    CardId.ElShaddollConstruct,
                    CardId.DogorantheMadFlameKaiju,
                    CardId.GamecieltheSeaTurtleKaiju,
                    CardId.SouleatingOviraptor,
                };
            if (!this.Bot.HasInGraveyard(targets))
            {
                return false;
            }
            this.AI.SelectCard(targets);
            return true;
        }


        private bool PotofAvariceeff()
        {
            return true;
        }

        private bool MaxxC()
        {
            return this.Duel.Player == 1;
        }


        private bool SetIsFieldEmpty()
        {
            return !this.Bot.IsFieldEmpty();
        }


        private bool SpellSetZone()
        {
            return (this.Bot.GetHandCount()>6 && this.Duel.Phase==DuelPhase.Main2);
        }

        private bool ChargeOfTheLightBrigadeEffect()
        {
            if (this.Bot.HasInGraveyard(CardId.Raiden) || this.Bot.HasInHand(CardId.Raiden))
            {
                this.AI.SelectCard(CardId.Lumina);
            }
            else
            {
                this.AI.SelectCard(CardId.Raiden);
            }

            return true;
        }


        // all Shaddoll 
        private bool SinisterShadowGameseff()
        {
            if (this.Bot.HasInGraveyard(CardId.ShaddollFusion))
            {
                this.AI.SelectCard(CardId.ShaddollCore);
            }
            else
            {
                this.AI.SelectCard(new[]
                {
                    CardId.ShaddollBeast,
                });
            }

            return true;
        }


        private bool ShaddollCoreeff()
        {
            if (this.Card.Location == CardLocation.SpellZone)
            {
                
                if (this.Duel.Player == 1 && this.Bot.BattlingMonster == null && this.Duel.Phase==DuelPhase.BattleStart|| this.DefaultOnBecomeTarget())
                {
                    Logger.DebugWriteLine("+++++++++++ShaddollCoreeffdododoo++++++++++");
                    this.AI.SelectPosition(CardPosition.FaceUpDefence);
                    return true;
                }
                return false;
            }
            return true;
        }


        private bool ShaddollFusioneff()
        {
            List<ClientCard> extra_zone_check = this.Bot.GetMonstersInExtraZone();
            foreach (ClientCard extra_monster in extra_zone_check)
            {
                if (extra_monster.HasType(CardType.Xyz) || extra_monster.HasType(CardType.Fusion) || extra_monster.HasType(CardType.Synchro))
                {
                    return false;
                }
            }

            bool deck_check = false;
            List<ClientCard> monsters = this.Enemy.GetMonsters();
            foreach (ClientCard monster in monsters)
            {
                if (monster.HasType(CardType.Synchro) || monster.HasType(CardType.Fusion) || monster.HasType(CardType.Xyz) || monster.HasType(CardType.Link))
                {
                    deck_check = true;
                }
            }

            if (deck_check)
            {
                this.AI.SelectCard(
                    CardId.ElShaddollConstruct,
                    CardId.ElShaddollShekhinaga,
                    CardId.ElShaddollGrysra,
                    CardId.ElShaddollWinda
                    );
                this.AI.SelectNextCard(
                    CardId.ShaddollSquamata,
                    CardId.ShaddollBeast,
                    CardId.ShaddollHedgehog,
                    CardId.ShaddollDragon,
                    CardId.ShaddollFalco,
                    CardId.FairyTailSnow
                    );
                this.AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
            }

            if (this.Enemy.GetMonsterCount() == 0)
            {
                int dark_count = 0;
                IList<ClientCard> m0 = this.Bot.Hand;
                IList<ClientCard> m1 = this.Bot.MonsterZone;
                IList<ClientCard> all = new List<ClientCard>();
                foreach (ClientCard monster in m0)
                {
                    if (dark_count == 2)
                    {
                        break;
                    }

                    if (monster.HasAttribute(CardAttribute.Dark))
                    {
                        dark_count++;
                        all.Add(monster);
                    }
                }
                foreach (ClientCard monster in m1)
                {
                    if (dark_count == 2)
                    {
                        break;
                    }

                    if (monster != null)
                    {
                        if (monster.HasAttribute(CardAttribute.Dark))
                        {
                            dark_count++;
                            all.Add(monster);
                        }
                    }


                }
                if (dark_count == 2)
                {
                    this.AI.SelectCard(CardId.ElShaddollWinda);
                    this.AI.SelectMaterials(all);
                    this.AI.SelectPosition(CardPosition.FaceUpAttack);
                    return true;
                }
            }
            if (!this.Util.IsOneEnemyBetter())
            {
                return false;
            }

            foreach (ClientCard monster in this.Bot.Hand)
            {
                if (monster.HasAttribute(CardAttribute.Light))
                {
                    this.AI.SelectCard(CardId.ElShaddollConstruct);
                    this.AI.SelectPosition(CardPosition.FaceUpAttack);
                    return true;
                }

            }
            List<ClientCard> material_1 = this.Bot.GetMonsters();
            foreach (ClientCard monster in material_1)
            {
                if (monster == null)
                {
                    break;
                }

                if (monster.HasAttribute(CardAttribute.Light))
                {
                    this.AI.SelectCard(CardId.ElShaddollConstruct);
                    this.AI.SelectPosition(CardPosition.FaceUpAttack);
                    return true;
                }

            }
            return false;

        }

        
        private bool ElShaddollShekhinagaeff()
        {
            if (this.Card.Location != CardLocation.MonsterZone)
            {
                return true;
            }
            else
            {
                if (this.DefaultBreakthroughSkill())
                {
                    this.AI.SelectCard(
                        CardId.ShaddollBeast,
                        CardId.ShaddollSquamata,
                        CardId.ShaddollHedgehog,
                        CardId.ShaddollDragon,
                        CardId.ShaddollFalco
                        );
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


        private bool ElShaddollGrysraeff()
        {
            if (this.Card.Location != CardLocation.MonsterZone)
            {
                return true;
            }

            return true;
        }


        private bool ElShaddollConstructeff()
        {

            if (!this.ShaddollBeast_used)
            {
                this.AI.SelectCard(CardId.ShaddollBeast);
            }
            else
            {
                this.AI.SelectCard(CardId.ShaddollFalco);
            }

            return true;
        }


        private bool ShaddollSquamataeff()
        {
            this.ShaddollSquamata_used = true;
            if (this.Card.Location != CardLocation.MonsterZone)
            {
                if(this.Util.ChainContainsCard(CardId.ElShaddollConstruct))
                {
                    if (!this.Bot.HasInHand(CardId.ShaddollFusion) && this.Bot.HasInGraveyard(CardId.ShaddollFusion))
                    {
                        this.AI.SelectNextCard(CardId.ShaddollCore);
                    }

                    if (!this.ShaddollBeast_used)
                    {
                        this.AI.SelectNextCard(CardId.ShaddollBeast);
                    }
                    else if (!this.ShaddollFalco_used)
                    {
                        this.AI.SelectNextCard(CardId.ShaddollFalco);
                    }
                    else  if(!this.ShaddollHedgehog_used)
                    {
                        this.AI.SelectNextCard(CardId.ShaddollHedgehog);
                    }
                }
                else
                {
                    if (!this.Bot.HasInHand(CardId.ShaddollFusion) && this.Bot.HasInGraveyard(CardId.ShaddollFusion))
                    {
                        this.AI.SelectCard(CardId.ShaddollCore);
                    }

                    if (!this.ShaddollBeast_used)
                    {
                        this.AI.SelectCard(CardId.ShaddollBeast);
                    }
                    else if (!this.ShaddollFalco_used)
                    {
                        this.AI.SelectCard(CardId.ShaddollFalco);
                    }
                    else if (!this.ShaddollHedgehog_used)
                    {
                        this.AI.SelectCard(CardId.ShaddollHedgehog);
                    }
                }

            }
            else
            {
                if (this.Enemy.GetMonsterCount() == 0)
                {
                    return false;
                }

                ClientCard target = this.Util.GetBestEnemyMonster();
                this.AI.SelectCard(target);
            }
            return true;
        }
        

        private bool ShaddollBeasteff()
        {
            this.ShaddollBeast_used = true;
            return true;
        }


        private bool ShaddollFalcoeff()
        {
            this.ShaddollFalco_used = true;
            if (this.Card.Location != CardLocation.MonsterZone)
            {
                return true;
            }
            else
            {
                this.AI.SelectCard(
                    CardId.ElShaddollConstruct,
                    CardId.ElShaddollShekhinaga,
                    CardId.ElShaddollGrysra,
                    CardId.ElShaddollWinda,
                    CardId.ShaddollSquamata
                    );

            }
            return true;
        }


        private bool ShaddollHedgehogeff()
        {
            this.ShaddollHedgehog_used = true;
            if (this.Card.Location != CardLocation.MonsterZone)
            {
                if (this.Util.ChainContainsCard(CardId.ElShaddollConstruct))
                {
                    this.AI.SelectNextCard(
                        CardId.ShaddollFalco,
                        CardId.ShaddollSquamata,
                        CardId.ShaddollDragon
                        );

                }
                else
                {
                    this.AI.SelectCard(
                        CardId.ShaddollSquamata,
                        CardId.ShaddollDragon
                        );
                }

            }
            else
            {
                this.AI.SelectCard(
                    CardId.ShaddollFusion,
                    CardId.SinisterShadowGames
                    );
            }
            return true;
        }


        private bool ShaddollDragoneff()
        {
            this.ShaddollDragon_used = true;
            if (this.Card.Location == CardLocation.MonsterZone)
            {
                ClientCard target = this.Util.GetBestEnemyCard();
                this.AI.SelectCard(target);
                return true;
            }
            else
            {
                if (this.Enemy.GetSpellCount() == 0)
                {
                    return false;
                }

                ClientCard target = this.Util.GetBestEnemySpell();
                this.AI.SelectCard(target);
                return true;
            }
        }
        
        
        private bool LostWindeff()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                return true;
            }

            List<ClientCard> check = this.Enemy.GetMonsters();
            foreach (ClientCard m in check)
            {
                if (m.Attack>=2000)
                {
                    return this.DefaultBreakthroughSkill();
                }
            }
            return false;            
        }

        private bool FoolishBurialEffect()
        {
            if (this.Bot.GetRemainingCount(CardId.DoubleEvolutionPill, 3) > 0)
            {
                if (!this.OvertexCoatlseff_used)
                {
                    this.AI.SelectCard(new[]
                        {
                        CardId.OvertexCoatls,
                    });
                    return true;
                }
                return false;
            }
            else
            {
                this.AI.SelectCard(CardId.ShaddollSquamata, CardId.FairyTailSnow);
            }
            return true;
        }      
       
       
        public bool Hand_act_eff()
        {
            //if (Card.IsCode(CardId.Urara) && Bot.HasInHand(CardId.LockBird) && Bot.HasInSpellZone(CardId.Re)) return false;
            if (this.Card.IsCode(CardId.GhostOgre) && this.Card.Location == CardLocation.Hand && this.Bot.HasInMonstersZone(CardId.GhostOgre))
            {
                return false;
            }

            return (this.Duel.LastChainPlayer == 1);
        }
        //other extra

        private bool Michaelsp()
        {
            IList<int> targets = new[] {
                   CardId.Raiden,
                   CardId.Lumina
                };
            if (!this.Bot.HasInMonstersZone(targets))
            {
                return false;
            }

            this.AI.SelectCard(targets);
            return true;
        }
        private bool Michaeleff()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                return true;
            }

            if (this.Bot.LifePoints <= 1000)
            {
                return false;
            }

            ClientCard select = this.Util.GetBestEnemyCard();
            if (select == null)
            {
                return false;
            }

            if (select!=null)
            {

                this.AI.SelectCard(select);
                return true;                    
            }            
            return false;
        }

        private bool MinervaTheExaltedEffect()
        {
            if (this.Card.Location == CardLocation.MonsterZone)
            {
                if (this.Bot.Deck.Count <= 10)
                {
                    return false;
                }

                return true;
            }
            else
            {
                IList<ClientCard> targets = new List<ClientCard>();

                ClientCard target1 = this.Util.GetBestEnemyMonster();
                if (target1 != null)
                {
                    targets.Add(target1);
                }

                ClientCard target2 = this.Util.GetBestEnemySpell();
                if (target2 != null)
                {
                    targets.Add(target2);
                }

                foreach (ClientCard target in this.Enemy.GetMonsters())
                {
                    if (targets.Count >= 3)
                    {
                        break;
                    }

                    if (!targets.Contains(target))
                    {
                        targets.Add(target);
                    }
                }
                foreach (ClientCard target in this.Enemy.GetSpells())
                {
                    if (targets.Count >= 3)
                    {
                        break;
                    }

                    if (!targets.Contains(target))
                    {
                        targets.Add(target);
                    }
                }
                if (targets.Count == 0)
                {
                    return false;
                }

                this.AI.SelectCard(0);
                this.AI.SelectNextCard(targets);
                return true;
            }
        }


        public bool CrystronNeedlefibersp()
        {
            if (this.Bot.HasInMonstersZone(CardId.ElShaddollConstruct) ||
                this.Bot.HasInMonstersZone(CardId.ElShaddollGrysra) ||
                this.Bot.HasInMonstersZone(CardId.ElShaddollShekhinaga) ||
                this.Bot.HasInMonstersZone(CardId.ElShaddollWinda))
            {
                return false;
            }

            if (this.CrystronNeedlefibereff_used)
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.CrystronNeedlefiber))
            {
                return false;
            }

            IList<int> check = new[]
            {
                CardId.GlowUpBulb,
                CardId.FairyTailSnow,
                CardId.KeeperOfDragonicMagic,
                CardId.SouleatingOviraptor,
                CardId.GiantRex,
                CardId.Lumina,
                CardId.Raiden,

            };
            int count=0;
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.IsCode(CardId.GlowUpBulb, CardId.FairyTailSnow, CardId.KeeperOfDragonicMagic,
                    CardId.SouleatingOviraptor, CardId.GiantRex, CardId.Lumina, CardId.Raiden))
                {
                    count++;
                }
            }

            if (!this.Bot.HasInMonstersZone(CardId.GlowUpBulb) || count<2)
            {
                return false;
            }

            this.AI.SelectCard(check);
            this.AI.SelectNextCard(check);
           
            return true;
        }

        public bool CrystronNeedlefibereff()
        {
            bool DarkHole = false;
            foreach (ClientCard card in this.Enemy.GetSpells())
            {
                if (card.IsCode(53129443) && card.IsFaceup())
                {
                    DarkHole = true;
                }
            }
            if (this.Duel.Player == 0)
            {

                this.CrystronNeedlefibereff_used = true;
                this.AI.SelectCard(
                    CardId.GhostOgre,
                    CardId.GlowUpBulb,
                    CardId.PlaguespreaderZombie,
                    CardId.ShaddollFalco
                    );
                return true;
            }
            
            else if (DarkHole || this.Util.IsChainTarget(this.Card) || this.Util.GetProblematicEnemySpell() != null)
            {
                this.AI.SelectCard(CardId.TG_WonderMagician);
                return true;
            }
                
            else if (this.Duel.Player == 1 && this.Duel.Phase == DuelPhase.BattleStart && this.Util.IsOneEnemyBetterThanValue(1500, true))
            {
                this.AI.SelectCard(CardId.TG_WonderMagician);
                if (this.Util.IsOneEnemyBetterThanValue(1900, true))
                {
                    this.AI.SelectPosition(CardPosition.FaceUpDefence);
                }
                else
                {
                    this.AI.SelectPosition(CardPosition.FaceUpAttack);
                }
                return true;
            }
            return false;
        }

        private bool ScarlightRedDragonsp()
        {
            return false;
        }

        private bool ScarlightRedDragoneff()
        {
            IList<ClientCard> targets = new List<ClientCard>();
            ClientCard target1 = this.Util.GetBestEnemyMonster();
            if (target1 != null)
            {
                targets.Add(target1);
                this.AI.SelectCard(targets);
                return true;
            }
            return false;
        }


        private bool CrystalWingSynchroDragoneff()
        {
            return this.Duel.LastChainPlayer != 0;
        }

        private bool Sdulldeateff()
        {
           /* if (snake_four_s)
            {
                snake_four_s = false;
                AI.SelectCard(Useless_List());
                return true;
            }
            //if (ActivateDescription == Util.GetStringId(CardId.snake, 2)) return true;
            if (ActivateDescription == Util.GetStringId(CardId.snake, 1))
            {
                foreach (ClientCard hand in Bot.Hand)
                {
                    if (hand.IsCode(CardId.Red, CardId.Pink))
                    {
                        AI.SelectCard(hand);
                        return true;
                    }
                    if (hand.IsCode(CardId.Urara, CardId.Ghost))
                    {
                        if (Tuner_ss())
                        {
                            AI.SelectCard(hand);
                            return true;
                        }
                    }
                }
            }*/
            return false;
        }
      
        private bool BlackRoseMoonlightDragoneff()
        {
            IList<ClientCard> targets = new List<ClientCard>();
            ClientCard target1 = this.Util.GetBestEnemyMonster();
            if (target1 != null)
            {
                targets.Add(target1);
                this.AI.SelectCard(targets);
                return true;
            }
            return false;

        }

        private bool RedWyvernsp()
        {
            return false;
        }

        private bool RedWyverneff()
        {
            IList<ClientCard> check = this.Enemy.GetMonsters();
            ClientCard best = null;
            foreach (ClientCard monster in check)
            {
                if (monster.Attack >= 2400)
                {
                    best = monster;
                }
            }
            if (best != null)
            {
                this.AI.SelectCard(best);
                return true;
            }
            return false;
        }

        private bool CoralDragoneff()
        {
            if (this.Card.Location != CardLocation.MonsterZone)
            {
                return true;
            }

            IList<ClientCard> targets = new List<ClientCard>();

            ClientCard target1 = this.Util.GetBestEnemyMonster();
            if (target1 != null)
            {
                targets.Add(target1);
            }

            ClientCard target2 = this.Util.GetBestEnemySpell();
            if (target2 != null)
            {
                targets.Add(target2);
            }
            else if (this.Util.IsChainTarget(this.Card) || this.Util.GetProblematicEnemySpell() != null)
            {
                this.AI.SelectCard(targets);
                return true;
            }
            else if (this.Duel.Player == 1 && this.Duel.Phase == DuelPhase.BattleStart && this.Util.IsOneEnemyBetterThanValue(2400, true))
            {
                this.AI.SelectCard(targets);
                return true;
            }
            return false;
        }

        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (!defender.IsMonsterHasPreventActivationEffectInBattle())
            {
                if (attacker.IsCode(CardId.ElShaddollConstruct) && !attacker.IsDisabled()) // TODO: && defender.IsSpecialSummoned
                {
                    attacker.RealPower = 9999;
                }

                if (attacker.IsCode(CardId.UltimateConductorTytanno) && !attacker.IsDisabled() && defender.IsDefense())
                {
                    attacker.RealPower = 9999;
                }
            }
            return base.OnPreBattleBetween(attacker, defender);
        }

        public override bool OnSelectHand()
        {
            return true;
        }
        /*
        private bool GoblindberghSummon()
        {
            foreach (ClientCard card in Bot.Hand.GetMonsters())
            {
                if (!card.Equals(Card) && card.Level == 4)
                    return true;
            }
            return false;
        }*/


    }
}