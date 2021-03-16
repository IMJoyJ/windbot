using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("ChainBurn", "AI_ChainBurn")]
    public class ChainBurnExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int SandaionTheTimelord = 33015627;
            public const int MichionTimelord = 7733560;
            public const int Mathematician = 41386308;
            public const int DiceJar = 3549275;
            public const int CardcarD = 45812361;
            public const int BattleFader = 19665973;
            public const int AbouluteKingBackJack = 60990740;

            public const int PotOfDesires = 35261759;
            public const int CardOfDemise = 59750328;
            public const int PotOfDuality = 98645731;
            public const int ChainStrike = 91623717;

            public const int Waboku = 12607053;
            public const int SecretBlast = 18252559;
            public const int JustDesserts = 24068492;
            public const int SectetBarrel = 27053506;
            public const int OjamaTrio = 29843091;
            public const int ThreateningRoar = 36361633;
            public const int Ceasefire = 36468556;
            public const int RecklessGreed = 37576645;
            public const int MagicCylinder = 62279055;
            public const int BalanceOfJudgment = 67443336;
            public const int BlazingMirrorForce = 75249652;
            public const int RingOfDestruction = 83555666;
            public const int AccuulatedFortune = 98444741;

            public const int Linkuriboh = 41999284;
            public const int HarpiesFeatherDuster = 18144506;
        }

        public ChainBurnExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            //first add
            this.AddExecutor(ExecutorType.Activate, CardId.PotOfDesires);
            this.AddExecutor(ExecutorType.Activate, CardId.PotOfDuality, this.PotOfDualityeff);
            //normal summon
            this.AddExecutor(ExecutorType.Summon, CardId.MichionTimelord, this.MichionTimelordsummon);
            this.AddExecutor(ExecutorType.Summon, CardId.SandaionTheTimelord, this.SandaionTheTimelord_summon);
            this.AddExecutor(ExecutorType.Summon, CardId.Mathematician);
            this.AddExecutor(ExecutorType.Activate, CardId.Mathematician, this.Mathematicianeff);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.DiceJar);
            this.AddExecutor(ExecutorType.Activate, CardId.DiceJar);
            this.AddExecutor(ExecutorType.Summon, CardId.CardcarD);
            this.AddExecutor(ExecutorType.Summon, CardId.AbouluteKingBackJack, this.AbouluteKingBackJacksummon);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.AbouluteKingBackJack);

            this.AddExecutor(ExecutorType.Activate, CardId.MichionTimelord);
            this.AddExecutor(ExecutorType.Activate, CardId.SandaionTheTimelord, this.SandaionTheTimelordeff);
            // Set traps
            this.AddExecutor(ExecutorType.SpellSet, CardId.Waboku);
            this.AddExecutor(ExecutorType.SpellSet, CardId.ThreateningRoar);
            this.AddExecutor(ExecutorType.SpellSet, CardId.BlazingMirrorForce);
            this.AddExecutor(ExecutorType.SpellSet, CardId.OjamaTrio, this.OjamaTrioset);
            this.AddExecutor(ExecutorType.SpellSet, this.BrunSpellSet);
            //afer set
            this.AddExecutor(ExecutorType.Activate, CardId.CardcarD);
            this.AddExecutor(ExecutorType.Activate, CardId.CardOfDemise, this.CardOfDemiseeff);
            //activate trap
            this.AddExecutor(ExecutorType.Activate, CardId.BalanceOfJudgment, this.BalanceOfJudgmenteff);
            this.AddExecutor(ExecutorType.Activate, CardId.AccuulatedFortune);
            //battle
            this.AddExecutor(ExecutorType.Activate, CardId.BlazingMirrorForce, this.BlazingMirrorForceeff);
            this.AddExecutor(ExecutorType.Activate, CardId.MagicCylinder, this.MagicCylindereff);
            this.AddExecutor(ExecutorType.Activate, CardId.ThreateningRoar, this.ThreateningRoareff);
            this.AddExecutor(ExecutorType.Activate, CardId.Waboku, this.Wabokueff);
            this.AddExecutor(ExecutorType.Activate, CardId.BattleFader, this.BattleFadereff);
            this.AddExecutor(ExecutorType.Activate, CardId.RingOfDestruction, this.Ring_act);
            //chain            
            this.AddExecutor(ExecutorType.Activate, CardId.JustDesserts, this.JustDessertseff);
            this.AddExecutor(ExecutorType.Activate, CardId.Ceasefire, this.Ceasefireeff);
            this.AddExecutor(ExecutorType.Activate, CardId.SecretBlast, this.SecretBlasteff);
            this.AddExecutor(ExecutorType.Activate, CardId.SectetBarrel, this.SectetBarreleff);
            this.AddExecutor(ExecutorType.Activate, CardId.RecklessGreed, this.RecklessGreedeff);
            this.AddExecutor(ExecutorType.Activate, CardId.OjamaTrio, this.OjamaTrioeff);
            this.AddExecutor(ExecutorType.Activate, CardId.AbouluteKingBackJack, this.AbouluteKingBackJackeff);
            this.AddExecutor(ExecutorType.Activate, CardId.ChainStrike, this.ChainStrikeeff);
            //sp
            this.AddExecutor(ExecutorType.SpSummon, CardId.Linkuriboh);
            this.AddExecutor(ExecutorType.Activate, CardId.Linkuriboh, this.Linkuriboheff);
            this.AddExecutor(ExecutorType.Repos, this.MonsterRepos);

        }
        public int[] all_List()
        {
            return new[]
            {
                CardId.SandaionTheTimelord,
                CardId.MichionTimelord,
                CardId.Mathematician,
                CardId.DiceJar,
                CardId.CardcarD,
                CardId.BattleFader,
                CardId.AbouluteKingBackJack,

                CardId.PotOfDesires,
                CardId.CardOfDemise,
                CardId.PotOfDuality,
                CardId.ChainStrike,

                CardId.Waboku,
                CardId.SecretBlast,
                CardId.JustDesserts,
                CardId.OjamaTrio,
                CardId.SectetBarrel,
                CardId.ThreateningRoar,
                CardId.Ceasefire,
                CardId.RecklessGreed,
                CardId.MagicCylinder,
                CardId.BalanceOfJudgment,
                CardId.BlazingMirrorForce,
                CardId.RingOfDestruction,
                CardId.AccuulatedFortune,
    };
        }
        public int[] AbouluteKingBackJack_List_1()
        {
            return new[] {
            CardId.BlazingMirrorForce,
            CardId.Waboku,
            CardId.ThreateningRoar,
            CardId.MagicCylinder,
            CardId.RingOfDestruction,
            CardId.RecklessGreed,
            CardId.SecretBlast,
            CardId.JustDesserts,
            CardId.OjamaTrio,
            CardId.SectetBarrel,
            CardId.Ceasefire,
            CardId.BalanceOfJudgment,
            CardId.AccuulatedFortune,
        };
    }
        public int[] AbouluteKingBackJack_List_2()
        {
            return new[] {
            CardId.MichionTimelord,
            CardId.SandaionTheTimelord,
            CardId.PotOfDesires,
            CardId.Mathematician,
            CardId.DiceJar,
            CardId.CardcarD,
            CardId.BattleFader,
            CardId.BlazingMirrorForce,
            CardId.Waboku,
            CardId.ThreateningRoar,
            CardId.MagicCylinder,
            CardId.RingOfDestruction,
            CardId.RecklessGreed,
            CardId.SecretBlast,
            CardId.JustDesserts,
            CardId.OjamaTrio,
            CardId.SectetBarrel,
            CardId.Ceasefire,
            CardId.BalanceOfJudgment,
            CardId.AccuulatedFortune,
        };
        }
        public int[] now_List()
        {
            return new[]
            {

                CardId.Waboku,
                CardId.SecretBlast,
                CardId.JustDesserts,
                CardId.SectetBarrel,
                CardId.ThreateningRoar,
                CardId.Ceasefire,
                CardId.RecklessGreed,
                CardId.RingOfDestruction,


    };
        }
        public int[] pot_list()
        {
            return new[]
            {
                CardId.PotOfDesires,                
                CardId.MichionTimelord,
                CardId.SandaionTheTimelord,
                CardId.BattleFader,

                CardId.Waboku,
                CardId.ThreateningRoar,
                CardId.MagicCylinder,
                CardId.BlazingMirrorForce,
                CardId.RingOfDestruction,
    };
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
        public bool Has_prevent_list_0(int id)
        {
            return (
                    id == CardId.Waboku ||
                    id == CardId.ThreateningRoar||
                    id == CardId.MagicCylinder||
                    id == CardId.BlazingMirrorForce||
                    id == CardId.RingOfDestruction
                   );
        }
        public bool Has_prevent_list_1(int id)
        {
            return (id == CardId.SandaionTheTimelord ||
                    id == CardId.BattleFader ||
                    id == CardId.MichionTimelord
                   );
        }
        bool no_sp = false;
        bool one_turn_kill = false;
        bool one_turn_kill_1 = false;
        int expected_blood = 0;
        bool prevent_used = false;
        int preventcount = 0;        
        bool OjamaTrioused = false;        
        bool OjamaTrioused_draw = false;
        bool OjamaTrioused_do = false;
        bool drawfirst = false;
        bool Linkuribohused = true;
        bool Timelord_check = false;
        int Waboku_count = 0;
        int Roar_count = 0;
        int strike_count = 0;        
        int greed_count = 0;
        int blast_count = 0;
        int barrel_count = 0;
        int just_count = 0;
        int Ojama_count = 0;
        int HasAccuulatedFortune = 0;

        public override bool OnSelectHand()
        {
            return true;
        }

        public override void OnNewTurn()
        {
            if (this.Bot.HasInHand(CardId.SandaionTheTimelord) || this.Bot.HasInHand(CardId.MichionTimelord))
            {
                Logger.DebugWriteLine("2222222222222222SandaionTheTimelord");
            }

            this.no_sp = false;
            this.prevent_used = false;
            this.Linkuribohused = true;
            this.Timelord_check = false;
        }
        public override void OnNewPhase()
        {
            this.preventcount = 0;
            this.OjamaTrioused = false;

            IList<ClientCard> trap = this.Bot.GetSpells();
            IList<ClientCard> monster = this.Bot.GetMonsters();

            foreach (ClientCard card in trap)
            {
                if (this.Has_prevent_list_0(card.Id))
                {
                    this.preventcount++;                    
                }
            }
            foreach (ClientCard card in monster)
            {
                if (this.Has_prevent_list_1(card.Id))
                {
                    this.preventcount++;                   
                }
            }
            foreach (ClientCard card in monster)
            {
                if (this.Bot.HasInMonstersZone(CardId.SandaionTheTimelord) ||
                    this.Bot.HasInMonstersZone(CardId.MichionTimelord))
                {
                    this.prevent_used = true;
                    this.Timelord_check = true;
                }
            }
            if(this.prevent_used && this.Timelord_check)
            {
                if (!this.Bot.HasInMonstersZone(CardId.SandaionTheTimelord) ||
                    !this.Bot.HasInMonstersZone(CardId.MichionTimelord))
                {
                    this.prevent_used = false;
                }
            }
            this.expected_blood = 0;
            this.one_turn_kill = false;
            this.one_turn_kill_1 = false;
            this.OjamaTrioused_draw = false;
            this.OjamaTrioused_do = false;
            this.drawfirst = false;
            this.HasAccuulatedFortune = 0;
            this.strike_count = 0;
            this.greed_count = 0;
            this.blast_count = 0;
            this.barrel_count = 0;
            this.just_count = 0;
            this.Waboku_count = 0;
            this.Roar_count = 0;
            this.Ojama_count = 0;

            IList<ClientCard> check = this.Bot.GetSpells();
            foreach (ClientCard card in check)
            {
                if (card.IsCode(CardId.AccuulatedFortune))
                {
                    this.HasAccuulatedFortune++;
                }
            }
            foreach (ClientCard card in check)
            {
                if (card.IsCode(CardId.SecretBlast))
                {
                    this.blast_count++;
                }
            }
            foreach (ClientCard card in check)
            {
                if (card.IsCode(CardId.SectetBarrel))
                {
                    this.barrel_count++;
                }
            }
            foreach (ClientCard card in check)
            {
                if (card.IsCode(CardId.JustDesserts))
                {
                    this.just_count++;
                }
            }
            foreach (ClientCard card in check)
            {
                if (card.IsCode(CardId.ChainStrike))
                {
                    this.strike_count++;
                }
            }
            foreach (ClientCard card in this.Bot.GetSpells())
            {
                if (card.IsCode(CardId.RecklessGreed))
                {
                    this.greed_count++;
                }
            }
            foreach (ClientCard card in check)
            {
                if (card.IsCode(CardId.Waboku))
                {
                    this.Waboku_count++;
                }
            }
            foreach (ClientCard card in check)
            {
                if (card.IsCode(CardId.ThreateningRoar))
                {
                    this.Roar_count++;
                }
            }
            this.expected_blood = (this.Enemy.GetMonsterCount() * 500 * this.just_count + this.Enemy.GetFieldHandCount() * 200 * this.barrel_count + this.Enemy.GetFieldCount() * 300 * this.blast_count);           
            if (this.Enemy.LifePoints <= this.expected_blood && this.Duel.Player == 1)
            {
                Logger.DebugWriteLine(" one_turn_kill");
                this.one_turn_kill = true;
            }
            this.expected_blood = 0;
            if (this.greed_count >= 2)
            {
                this.greed_count = 1;
            }

            if (this.blast_count >= 2)
            {
                this.blast_count = 1;
            }

            if (this.just_count >= 2)
            {
                this.just_count = 1;
            }

            if (this.barrel_count >= 2)
            {
                this.barrel_count = 1;
            }

            if (this.Waboku_count >= 2)
            {
                this.Waboku_count = 1;
            }

            if (this.Roar_count >= 2)
            {
                this.Roar_count = 1;
            }

            int currentchain = 0;
            if (this.OjamaTrioused_do)
            {
                currentchain = this.Duel.CurrentChain.Count + this.blast_count + this.just_count + this.barrel_count + this.Waboku_count + this.Waboku_count + this.Roar_count + this.greed_count + this.Ojama_count;
            }
            else
            {
                currentchain = this.Duel.CurrentChain.Count + this.blast_count + this.just_count + this.barrel_count + this.Waboku_count + this.Waboku_count + this.greed_count + this.Roar_count;
            }
            //if (currentchain >= 3 && Duel.Player == 1) drawfirst = true;
            if (this.Bot.HasInSpellZone(CardId.ChainStrike))
            {
                if (this.strike_count == 1)
                {
                    if (this.OjamaTrioused_do)
                    {
                        this.expected_blood = ((this.Enemy.GetMonsterCount() + 3) * 500 * this.just_count + this.Enemy.GetFieldHandCount() * 200 * this.barrel_count + this.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1) * 400);
                    }
                    else
                    {
                        this.expected_blood = (this.Enemy.GetMonsterCount() * 500 * this.just_count + this.Enemy.GetFieldHandCount() * 200 * this.barrel_count + this.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1) * 400);
                    }
                }
                else
                {
                    if (this.OjamaTrioused_do)
                    {
                        this.expected_blood = ((this.Enemy.GetMonsterCount() + 3) * 500 * this.just_count + this.Enemy.GetFieldHandCount() * 200 * this.barrel_count + this.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1 + currentchain + 2) * 400);
                    }
                    else
                    {
                        this.expected_blood = (this.Enemy.GetMonsterCount() * 500 * this.just_count + this.Enemy.GetFieldHandCount() * 200 * this.barrel_count + this.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1 + currentchain + 2) * 400);
                    }
                }
                if (!this.one_turn_kill && this.Enemy.LifePoints <= this.expected_blood && this.Duel.Player == 1)
                {
                    Logger.DebugWriteLine(" %%%%%%%%%%%%%%%%%one_turn_kill_1");
                    this.one_turn_kill_1 = true;
                    this.OjamaTrioused = true;
                }
            }
            }


        private bool must_chain()
        {
            if (this.Util.IsChainTarget(this.Card))
            {
                return true;
            }

            foreach (ClientCard card in this.Enemy.GetSpells())
            {
                if (card.IsCode(CardId.HarpiesFeatherDuster)&&card.IsFaceup())
                {
                    return true;
                }
            }
            return false;
        }
        private bool OjamaTrioset()
        {
            if (this.Bot.HasInSpellZone(CardId.OjamaTrio))
            {
                return false;
            }

            return true;
        }
        private bool BrunSpellSet()
        {
            if (this.Card.IsCode(CardId.OjamaTrio) && this.Bot.HasInSpellZone(CardId.OjamaTrio))
            {
                return false;
            }

            return (this.Card.IsTrap() || this.Card.HasType(CardType.QuickPlay)) && this.Bot.GetSpellCountWithoutField() < 5;
        }
        private bool MichionTimelordsummon()
        {
            if (this.Duel.Turn == 1)
            {
                return false;
            }

            return true;
        }

        private bool SandaionTheTimelord_summon()
        {
            Logger.DebugWriteLine("&&&&&&&&&SandaionTheTimelord_summon");
            return true;            
        }
        private bool AbouluteKingBackJacksummon()
        {
            return !this.no_sp;
        }
        private bool AbouluteKingBackJackeff()
        {
            if (this.ActivateDescription == -1)
            {
                this.AI.SelectCard(this.AbouluteKingBackJack_List_1());
                this.AI.SelectNextCard(this.AbouluteKingBackJack_List_2());
            }

            return true;

        }
        private bool PotOfDualityeff()
        {
            this.no_sp = true;

            this.AI.SelectCard(this.pot_list());
            return true;
        }
        
        private bool ThreateningRoareff()
        {
            if (this.one_turn_kill_1)
            {
                return this.UniqueFaceupSpell();
            }

            if (this.drawfirst)
            {
                return true;
            }

            if (this.DefaultOnBecomeTarget())
            {
                this.prevent_used = true;
                return true;
            }
            if (this.prevent_used || this.Duel.Phase != DuelPhase.BattleStart)
            {
                return false;
            }

            this.prevent_used = true;
            return this.DefaultUniqueTrap();
        }
        private bool SandaionTheTimelordeff()
        {
            Logger.DebugWriteLine("***********SandaionTheTimelordeff");
            return true;
        }
        private bool Wabokueff()
        {
            if (this.one_turn_kill_1)
            {
                return this.UniqueFaceupSpell();
            }

            if (this.drawfirst)
            {
                this.Linkuribohused = false;
                return true;
            }                
            if (this.DefaultOnBecomeTarget())
            {
                this.Linkuribohused = false;
                this.prevent_used = true;
                return true;
            }            
            if (this.prevent_used || this.Duel.Player == 0|| this.Duel.Phase!=DuelPhase.BattleStart)
            {
                return false;
            }

            this.prevent_used = true;
            this.Linkuribohused = false;
            return this.DefaultUniqueTrap();
        }
        private bool BattleFadereff()
        {
            if (this.Util.ChainContainsCard(CardId.BlazingMirrorForce) || this.Util.ChainContainsCard(CardId.MagicCylinder))
            {
                return false;
            }

            if (this.prevent_used || this.Duel.Player == 0)
            {
                return false;
            }

            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            this.prevent_used = true;
            return true;
        }

        private bool BlazingMirrorForceeff()
        {
            if (this.prevent_used)
            {
                return false;
            }

            IList<ClientCard> list = new List<ClientCard>();
            foreach (ClientCard monster in this.Enemy.GetMonsters())
            {
                if (monster.IsAttack())
                {
                    list.Add(monster);
                }
            }
            if (this.GetTotalATK(list) / 2 >= this.Bot.LifePoints)
            {
                return false;
            }

            Logger.DebugWriteLine("!!!!!!!!BlazingMirrorForceeff" + this.GetTotalATK(list) / 2);
            if (this.GetTotalATK(list) / 2 >= this.Enemy.LifePoints)
            {
                return this.DefaultUniqueTrap();
            }

            if (this.GetTotalATK(list) < 3000)
            {
                return false;
            }

            this.prevent_used = true;
            return this.DefaultUniqueTrap();
                        
        }

        private bool MagicCylindereff()
        {
            if (this.prevent_used)
            {
                return false;
            }

            if (this.Bot.LifePoints <= this.Enemy.BattlingMonster.Attack)
            {
                return this.DefaultUniqueTrap();
            }

            if (this.Enemy.LifePoints <= this.Enemy.BattlingMonster.Attack)
            {
                return this.DefaultUniqueTrap();
            }

            if (this.Enemy.BattlingMonster.Attack>1800)
            {
                return this.DefaultUniqueTrap();
            }

            return false;
        }
        public bool Ring_act()
        {
            if (this.Duel.LastChainPlayer == 0 && this.Util.GetLastChainCard() != null )
            {
                return false;
            }

            ClientCard target = this.Util.GetProblematicEnemyMonster();
            if (target == null && this.Util.IsChainTarget(this.Card))
            {
                target = this.Util.GetBestEnemyMonster(true, true);
            }
            if (target != null)
            {
                if (this.Bot.LifePoints <= target.Attack)
                {
                    return false;
                }

                this.AI.SelectCard(target);
                return true;
            }
            return false;
        }
        private bool RecklessGreedeff()
        {
            if (this.one_turn_kill_1)
            {
                return this.UniqueFaceupSpell();
            }

            int count=0;
            foreach (ClientCard card in this.Bot.GetSpells())
            {
                if (card.IsCode(CardId.RecklessGreed))
                {
                    count++;
                }
            }           
            bool Demiseused = this.Util.ChainContainsCard(CardId.CardOfDemise);
            if (this.drawfirst)
            {
                return this.UniqueFaceupSpell();
            }

            if (this.DefaultOnBecomeTarget() && count > 1)
            {
                return true;
            }

            if (Demiseused)
            {
                return false;
            }

            if (count > 1)
            {
                return true;
            }

            if (this.Bot.LifePoints <= 3000)
            {
                return true;
            }

            if (this.Bot.GetHandCount() <1 && this.Duel.Player==0 && this.Duel.Phase!=DuelPhase.Standby)
            {
                return true;
            }

            return false;
        }
        private bool SectetBarreleff()
        {
            if (this.DefaultOnBecomeTarget())
            {
                return true;
            }

            if (this.Duel.Player == 0)
            {
                return false;
            }

            if (this.drawfirst)
            {
                return true;
            }

            if (this.one_turn_kill_1)
            {
                return this.UniqueFaceupSpell();
            }

            if (this.one_turn_kill)
            {
                return true;
            }

            if (this.DefaultOnBecomeTarget())
            {
                return true;
            }

            int count = this.Enemy.GetFieldHandCount();
            int monster_count = this.Enemy.GetMonsterCount() - this.Enemy.GetMonstersExtraZoneCount();
            if (this.Enemy.LifePoints < count * 200)
            {
                return true;
            }

            if (this.Bot.HasInSpellZone(CardId.OjamaTrio) && monster_count <= 2 && monster_count >= 1)
            {
                if (count + 3 >= 8)
                {
                    this.OjamaTrioused = true;
                    return true;
                }
            }
            if (count >= 8)
            {
                return true;
            }

            return false;
        }
        private bool SecretBlasteff()
        {
            if (this.DefaultOnBecomeTarget())
            {
                return true;
            }

            if (this.Duel.Player == 0)
            {
                return false;
            }

            if (this.drawfirst)
            {
                return this.UniqueFaceupSpell();
            }

            if (this.one_turn_kill_1)
            {
                return this.UniqueFaceupSpell();
            }

            if (this.one_turn_kill)
            {
                return true;
            }

            int count = this.Enemy.GetFieldCount();
            int monster_count = this.Enemy.GetMonsterCount() - this.Enemy.GetMonstersExtraZoneCount();
            if (this.Enemy.LifePoints < count * 300)
            {
                return true;
            }

            if (this.Bot.HasInSpellZone(CardId.OjamaTrio) && monster_count <= 2 && monster_count >= 1 )
            {
                if(count+3>=5)
                {
                    this.OjamaTrioused = true;
                    return true;
                }
            }
            if (count >= 5)
            {
                return true;
            }

            return false;

        }
        
        private bool OjamaTrioeff()
        {
            return this.OjamaTrioused || this.OjamaTrioused_draw;
        }
        private bool JustDessertseff()
        {
            if (this.DefaultOnBecomeTarget())
            {
                return true;
            }

            if (this.Duel.Player == 0)
            {
                return false;
            }

            if (this.drawfirst)
            {
                return this.UniqueFaceupSpell();
            }

            if (this.one_turn_kill_1)
            {
                return this.UniqueFaceupSpell();
            }

            if (this.one_turn_kill)
            {
                return true;
            }

            int count = this.Enemy.GetMonsterCount()-this.Enemy.GetMonstersExtraZoneCount();
            if (this.Enemy.LifePoints <= count * 500)
            {
                return true;
            }

            if (this.Bot.HasInSpellZone(CardId.OjamaTrio) && count <= 2 && count >= 1)
            {
                this.OjamaTrioused = true;
                return true;
            }
            if (count >= 3)
            {
                return true;
            }

            return false;
        }
        private bool ChainStrikeeff()
        {
            if (this.one_turn_kill)
            {
                return true;
            }

            if (this.one_turn_kill_1)
            {
                return true;
            }

            if (this.drawfirst)
            {
                return true;
            }

            if (this.DefaultOnBecomeTarget())
            {
                return true;
            }

            int chain = this.Duel.CurrentChain.Count;
            if (this.strike_count >= 2 && chain >= 2)
            {
                return true;
            }

            if (this.Enemy.LifePoints <= (chain + 1) * 400)
            {
                return true;
            }

            if (this.Duel.CurrentChain.Count >= 3)
            {
                return true;
            }

            return false;
        }

        private bool BalanceOfJudgmenteff()
        {
            if (this.DefaultOnBecomeTarget())
            {
                return true;
            }

            int count = (this.Enemy.GetFieldCount() - this.Bot.GetFieldHandCount());
            if ( count>= 2)
            {
                return true;
            }

            return false;
        }
        private bool CardOfDemiseeff()
        {
            foreach (ClientCard card in this.Bot.GetMonsters())
            {
                if (card.IsCode(CardId.CardcarD) && card.IsFaceup())
                {
                    return false;
                }
            }
            if (this.Bot.GetHandCount() == 1 && this.Bot.GetSpellCountWithoutField() <= 3)
            {
                this.no_sp = true;
                return true;
            }
            return false;
        }
        private bool Mathematicianeff()
        {
            if (this.Card.Location == CardLocation.MonsterZone)
            {
                this.AI.SelectCard(CardId.AbouluteKingBackJack);
                return true;
            }
            return true;

        }
        private bool DiceJarfacedown()
        {

            foreach (ClientCard card in this.Bot.GetMonsters())

            {
                if (card.IsCode(CardId.DiceJar) && card.IsFacedown())
                {
                    return true;
                }

                break;
            }
            return false;
        }
        private bool Ceasefireeff()
        {
            if (this.Enemy.GetMonsterCount() >= 3)
            {
                return true;
            }

            if (this.DiceJarfacedown())
            {
                return false;
            }

            if ((this.Bot.GetMonsterCount() + this.Enemy.GetMonsterCount()) >= 4)
            {
                return true;
            }

            return false;
        }
        private bool Linkuriboheff()
        {
            IList<ClientCard> newlist = new List<ClientCard>();
            foreach (ClientCard newmonster in this.Enemy.GetMonsters())
            {
                if (newmonster.IsAttack())
                {
                    newlist.Add(newmonster);
                }
            }
            if (!this.Linkuribohused)
            {
                return false;
            }

            if (this.Enemy.BattlingMonster!=null)
            {
                if (this.Enemy.BattlingMonster.Attack > 1800 && this.Bot.HasInSpellZone(CardId.MagicCylinder))
                {
                    return false;
                }
            }
            if (this.GetTotalATK(newlist) / 2 >= this.Bot.LifePoints && this.Bot.HasInSpellZone(CardId.BlazingMirrorForce))
            {
                return true;
            }

            if (this.GetTotalATK(newlist) / 2 >= this.Enemy.LifePoints && this.Bot.HasInSpellZone(CardId.BlazingMirrorForce))
            {
                return false;
            }

            if (this.Util.GetLastChainCard() == null)
            {
                return true;
            }

            if (this.Util.GetLastChainCard().IsCode(CardId.Linkuriboh))
            {
                return false;
            }

            return true;
        }
        public bool MonsterRepos()
        {
            if (this.Card.IsFacedown() && !this.Card.IsCode(CardId.DiceJar))
            {
                return true;
            }

            return base.DefaultMonsterRepos();
        }
        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (attacker.IsCode(CardId.Linkuriboh) && defender.IsFacedown())
            {
                return false;
            }

            if (attacker.IsCode(CardId.SandaionTheTimelord) && !attacker.IsDisabled())
            {
                attacker.RealPower = 9999;
                return true;
            }
            if(attacker.IsCode(CardId.MichionTimelord) && !attacker.IsDisabled())
            {
                attacker.RealPower = 9999;
                return true;
            }
            return base.OnPreBattleBetween(attacker,defender);
        }

        public override void OnChaining(int player, ClientCard card)
        {
            this.expected_blood = 0;
            this.one_turn_kill = false;
            this.one_turn_kill_1 = false;
            this.OjamaTrioused_draw = false;
            this.OjamaTrioused_do = false;
            this.drawfirst = false;
            this.HasAccuulatedFortune = 0;
            this.strike_count = 0;
            this.greed_count = 0;
            this.blast_count = 0;
            this.barrel_count = 0;
            this.just_count = 0;
            this.Waboku_count = 0;
            this.Roar_count = 0;
            this.Ojama_count = 0;

            IList<ClientCard> check = this.Bot.GetSpells();
            foreach (ClientCard card1 in check)
            {
                if (card1.IsCode(CardId.AccuulatedFortune))
                {
                    this.HasAccuulatedFortune++;
                }
            }
            foreach (ClientCard card1 in check)
            {
                if (card1.IsCode(CardId.SecretBlast))
                {
                    this.blast_count++;
                }
            }
            foreach (ClientCard card1 in check)
            {
                if (card1.IsCode(CardId.SectetBarrel))
                {
                    this.barrel_count++;
                }
            }
            foreach (ClientCard card1 in check)
            {
                if (card1.IsCode(CardId.JustDesserts))
                {
                    this.just_count++;
                }
            }
            foreach (ClientCard card1 in check)
            {
                if (card1.IsCode(CardId.ChainStrike))
                {
                    this.strike_count++;
                }
            }
            foreach (ClientCard card1 in this.Bot.GetSpells())
            {
                if (card1.IsCode(CardId.RecklessGreed))
                {
                    this.greed_count++;
                }
            }
            foreach (ClientCard card1 in check)
            {
                if (card1.IsCode(CardId.Waboku))
                {
                    this.Waboku_count++;
                }
            }
            foreach (ClientCard card1 in check)
            {
                if (card1.IsCode(CardId.ThreateningRoar))
                {
                    this.Roar_count++;
                }
            }
            /*if (Bot.HasInSpellZone(CardId.OjamaTrio) && Enemy.GetMonsterCount() <= 2 && Enemy.GetMonsterCount() >= 1)
            {
                if (HasAccuulatedFortune > 0) OjamaTrioused_draw = true;
            }*/
            if (this.Bot.HasInSpellZone(CardId.OjamaTrio) && (this.Enemy.GetMonsterCount() - this.Enemy.GetMonstersExtraZoneCount()) <= 2 && 
                (this.Enemy.GetMonsterCount() - this.Enemy.GetMonstersExtraZoneCount()) >= 1)
            {
                this.OjamaTrioused_do = true;
            }
            this.expected_blood = (this.Enemy.GetMonsterCount() * 500 * this.just_count + this.Enemy.GetFieldHandCount() * 200 * this.barrel_count + this.Enemy.GetFieldCount() * 300 * this.blast_count);
            if (this.Enemy.LifePoints <= this.expected_blood && this.Duel.Player == 1)
            {
                Logger.DebugWriteLine(" %%%%%%%%%%%%%%%%%one_turn_kill");
                this.one_turn_kill = true;
            }
            this.expected_blood = 0;
            if (this.greed_count >= 2)
            {
                this.greed_count = 1;
            }

            if (this.blast_count >= 2)
            {
                this.blast_count = 1;
            }

            if (this.just_count >= 2)
            {
                this.just_count = 1;
            }

            if (this.barrel_count >= 2)
            {
                this.barrel_count = 1;
            }

            if (this.Waboku_count >= 2)
            {
                this.Waboku_count = 1;
            }

            if (this.Roar_count >= 2)
            {
                this.Roar_count = 1;
            }

            int currentchain = 0;
            if (this.OjamaTrioused_do)
            {
                currentchain = this.Duel.CurrentChain.Count + this.blast_count + this.just_count + this.barrel_count + this.Waboku_count + this.Waboku_count + this.Roar_count + this.greed_count + this.Ojama_count;
            }
            else
            {
                currentchain = this.Duel.CurrentChain.Count + this.blast_count + this.just_count + this.barrel_count + this.Waboku_count + this.Waboku_count + this.greed_count + this.Roar_count ;
            }
            //if (currentchain >= 3 && Duel.Player == 1) drawfirst = true;
            if (this.Bot.HasInSpellZone(CardId.ChainStrike))
            {
                if (this.strike_count == 1)
                {
                    if (this.OjamaTrioused_do)
                    {
                        this.expected_blood = ((this.Enemy.GetMonsterCount() + 3) * 500 * this.just_count + this.Enemy.GetFieldHandCount() * 200 * this.barrel_count + this.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1) * 400);
                    }
                    else
                    {
                        this.expected_blood = (this.Enemy.GetMonsterCount() * 500 * this.just_count + this.Enemy.GetFieldHandCount() * 200 * this.barrel_count + this.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1) * 400);
                    }
                }
                else
                {
                    if (this.OjamaTrioused_do)
                    {
                        this.expected_blood = ((this.Enemy.GetMonsterCount() + 3) * 500 * this.just_count + this.Enemy.GetFieldHandCount() * 200 * this.barrel_count + this.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1 + currentchain + 2) * 400);
                    }
                    else
                    {
                        this.expected_blood = (this.Enemy.GetMonsterCount() * 500 * this.just_count + this.Enemy.GetFieldHandCount() * 200 * this.barrel_count + this.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1 + currentchain + 2) * 400);
                    }
                }
                if (!this.one_turn_kill && this.Enemy.LifePoints <= this.expected_blood && this.Duel.Player == 1)
                {
                    Logger.DebugWriteLine(" %%%%%%%%%%%%%%%%%one_turn_kill_1");
                    this.one_turn_kill_1 = true;
                    this.OjamaTrioused = true;
                }
            }
            base.OnChaining(player, card);           
        }
    }
}