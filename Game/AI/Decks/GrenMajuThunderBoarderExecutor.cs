using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("GrenMajuThunderBoarder", "AI_GrenMajuThunderBoarder")]
    public class GrenMajuThunderBoarderExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int MetalSnake = 71197066;
            public const int InspectBoarder = 15397015;
            public const int ThunderKingRaiOh = 71564252;
            public const int AshBlossomAndJoyousSpring =14558127;
            public const int GhostReaperAndWinterCherries = 62015408;
            public const int GrenMajuDaEizo = 36584821;
            public const int MaxxC = 23434538;
            public const int EaterOfMillions = 63845230;

            public const int HarpieFeatherDuster = 18144506;
            public const int PotOfDesires = 35261759;
            public const int CardOfDemise = 59750328;
            public const int UpstartGoblin = 70368879;
            public const int PotOfDuality = 98645731;
            public const int Scapegoat = 73915051;
            public const int MoonMirrorShield = 19508728;
            public const int InfiniteImpermanence = 10045474;
            public const int WakingTheDragon = 10813327;
            public const int EvenlyMatched = 15693423;
            public const int HeavyStormDuster = 23924608;
            public const int DrowningMirrorForce = 47475363;
            public const int MacroCosmos = 30241314;
            public const int Crackdown = 36975314;
            public const int AntiSpellFragrance = 58921041;
            public const int ImperialOrder = 61740673;
            public const int PhatomKnightsSword = 61936647;
            public const int UnendingNightmare= 69452756;
            public const int SolemnWarning = 84749824;
            public const int SolemStrike= 40605147;
            public const int SolemnJudgment = 41420027;
            public const int DarkBribe = 77538567;

            public const int RaidraptorUltimateFalcon = 86221741;
            public const int BorreloadDragon = 31833038;
            public const int BirrelswordDragon = 85289965;
            public const int FirewallDragon = 5043010;
            public const int NingirsuTheWorldChaliceWarrior = 30194529;
            public const int TopologicTrisbaena = 72529749;
            public const int KnightmareUnicorn = 38342335;
            public const int KnightmarePhoenix = 2857636;
            public const int HeavymetalfoesElectrumite= 24094258;
            public const int KnightmareCerberus = 75452921;
            public const int CrystronNeedlefiber = 50588353;
            public const int MissusRadiant= 3987233;
            public const int BrandishMaidenKagari= 63288573;
            public const int LinkSpider = 98978921;
            public const int Linkuriboh = 41999284;

            public const int KnightmareGryphon = 65330383;
        }

        public GrenMajuThunderBoarderExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.GoToBattlePhase, this.GoToBattlePhase);
            this.AddExecutor(ExecutorType.Activate, CardId.EvenlyMatched, this.EvenlyMatchedeff);
            //Sticker
            this.AddExecutor(ExecutorType.Activate, CardId.MacroCosmos, this.MacroCosmoseff);
            this.AddExecutor(ExecutorType.Activate, CardId.AntiSpellFragrance, this.AntiSpellFragranceeff);
            //counter
            this.AddExecutor(ExecutorType.Activate, CardId.AshBlossomAndJoyousSpring, this.DefaultAshBlossomAndJoyousSpring);
            this.AddExecutor(ExecutorType.Activate, CardId.MaxxC, this.DefaultMaxxC);
            this.AddExecutor(ExecutorType.Activate, CardId.InfiniteImpermanence, this.DefaultInfiniteImpermanence);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnWarning, this.DefaultSolemnWarning);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemStrike, this.DefaultSolemnStrike);
            this.AddExecutor(ExecutorType.Activate, CardId.ImperialOrder, this.ImperialOrderfirst);
            this.AddExecutor(ExecutorType.Activate, CardId.HeavyStormDuster, this.HeavyStormDustereff);
            this.AddExecutor(ExecutorType.Activate, CardId.UnendingNightmare, this.UnendingNightmareeff);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkBribe, this.DarkBribeeff);
            this.AddExecutor(ExecutorType.Activate, CardId.ImperialOrder, this.ImperialOrdereff);
            this.AddExecutor(ExecutorType.Activate, CardId.ThunderKingRaiOh, this.ThunderKingRaiOheff);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnJudgment, this.DefaultSolemnJudgment);
            this.AddExecutor(ExecutorType.Activate, CardId.DrowningMirrorForce, this.DrowningMirrorForceeff);
            //first do
            this.AddExecutor(ExecutorType.Activate, CardId.UpstartGoblin, this.UpstartGoblineff);
            this.AddExecutor(ExecutorType.Activate, CardId.HarpieFeatherDuster, this.DefaultHarpiesFeatherDusterFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.PotOfDuality, this.PotOfDualityeff);
            this.AddExecutor(ExecutorType.Activate, CardId.PotOfDesires, this.PotOfDesireseff);
            this.AddExecutor(ExecutorType.Activate, CardId.CardOfDemise, this.CardOfDemiseeff);
            //sp
            this.AddExecutor(ExecutorType.Activate, CardId.Linkuriboh, this.Linkuriboheff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Linkuriboh, this.Linkuribohsp);
            this.AddExecutor(ExecutorType.SpSummon, CardId.KnightmareCerberus, this.Knightmaresp);
            this.AddExecutor(ExecutorType.SpSummon, CardId.KnightmarePhoenix, this.Knightmaresp);
            this.AddExecutor(ExecutorType.SpSummon, CardId.MissusRadiant, this.MissusRadiantsp);
            this.AddExecutor(ExecutorType.Activate, CardId.MissusRadiant, this.MissusRadianteff);

            this.AddExecutor(ExecutorType.SpSummon, CardId.Linkuriboh, this.Linkuribohsp);
            this.AddExecutor(ExecutorType.SpSummon, CardId.LinkSpider);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BorreloadDragon, this.BorreloadDragonsp);
            this.AddExecutor(ExecutorType.Activate, CardId.BorreloadDragon, this.BorreloadDragoneff);
            this.AddExecutor(ExecutorType.Activate, CardId.EaterOfMillions, this.EaterOfMillionseff);
            this.AddExecutor(ExecutorType.Activate, CardId.WakingTheDragon, this.WakingTheDragoneff);
            // normal summon
            this.AddExecutor(ExecutorType.Summon, CardId.InspectBoarder, this.InspectBoardersummon);
            this.AddExecutor(ExecutorType.Summon, CardId.GrenMajuDaEizo, this.GrenMajuDaEizosummon);
            this.AddExecutor(ExecutorType.Summon, CardId.ThunderKingRaiOh, this.ThunderKingRaiOhsummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BorreloadDragon, this.BorreloadDragonspsecond);
            this.AddExecutor(ExecutorType.SpSummon, CardId.EaterOfMillions, this.EaterOfMillionssp);

            this.AddExecutor(ExecutorType.Activate, CardId.MetalSnake, this.MetalSnakesp);
            this.AddExecutor(ExecutorType.Activate, CardId.MetalSnake, this.MetalSnakeeff);
            //spell
            this.AddExecutor(ExecutorType.Activate, CardId.Crackdown, this.Crackdowneff);
            this.AddExecutor(ExecutorType.Activate, CardId.MoonMirrorShield, this.MoonMirrorShieldeff);
            this.AddExecutor(ExecutorType.Activate, CardId.Scapegoat, this.DefaultScapegoat);
            this.AddExecutor(ExecutorType.Activate, CardId.PhatomKnightsSword, this.PhatomKnightsSwordeff);
            this.AddExecutor(ExecutorType.Repos, this.MonsterRepos);
            //set
            this.AddExecutor(ExecutorType.SpellSet, this.SpellSet);
        }
        bool CardOfDemiseeff_used = false;        
        bool eater_eff = false;
        public override void OnNewTurn()
        {
            this.eater_eff = false;
            this.CardOfDemiseeff_used = false;
        }

        public override void OnNewPhase()
        {
            foreach (ClientCard check in this.Bot.GetMonsters())
            {
                if (check.HasType(CardType.Fusion) || check.HasType(CardType.Xyz) ||
                    check.HasType(CardType.Synchro) || check.HasType(CardType.Link) ||
                    check.HasType(CardType.Ritual))
                {
                    this.eater_eff = true;
                    break;
                }
            }
            foreach (ClientCard check in this.Enemy.GetMonsters())
            {
                if (check.HasType(CardType.Fusion) || check.HasType(CardType.Xyz) ||
                    check.HasType(CardType.Synchro) || check.HasType(CardType.Link) ||
                    check.HasType(CardType.Ritual))
                {
                    this.eater_eff = true;
                    break;
                }
            }
            base.OnNewPhase();
        }

        private bool GoToBattlePhase()
        {
            return this.Bot.HasInHand(CardId.EvenlyMatched) && this.Duel.Turn >= 2 && this.Enemy.GetFieldCount() >= 2 && this.Bot.GetFieldCount() == 0;
        }
 
        private bool MacroCosmoseff()
        {
           
            return (this.Duel.LastChainPlayer == 1 || this.Duel.LastSummonPlayer == 1 || this.Duel.Player == 0) && this.UniqueFaceupSpell();
        }

        private bool AntiSpellFragranceeff()
        {
           
            int spell_count = 0;
            foreach(ClientCard check in this.Bot.Hand)
            {
                if (check.HasType(CardType.Spell))
                {
                    spell_count++;
                }
            }
            if (spell_count >= 2)
            {
                return false;
            }

            return this.Duel.Player == 1 && this.UniqueFaceupSpell();
        }
 
        private bool EvenlyMatchedeff()
        {
            return this.Enemy.GetFieldCount()- this.Bot.GetFieldCount() > 1;
        }
 
        private bool HeavyStormDustereff()
        {
            IList<ClientCard> targets = new List<ClientCard>();
            foreach (ClientCard check in this.Enemy.GetSpells())
            {
                if (check.HasType(CardType.Continuous) || check.HasType(CardType.Field))
                {
                    targets.Add(check);
                }
            }
            if (this.Util.GetPZone(1, 0) != null && this.Util.GetPZone(1, 0).Type == 16777218)
            {
                targets.Add(this.Util.GetPZone(1, 0));
                
            }
            if (this.Util.GetPZone(1, 1) != null && this.Util.GetPZone(1, 1).Type == 16777218)
            {
                targets.Add(this.Util.GetPZone(1, 1));               
            }
            foreach (ClientCard check in this.Enemy.GetSpells())
            {
                if (!check.HasType(CardType.Continuous) && !check.HasType(CardType.Field))
                {
                    targets.Add(check);
                }
            }
            if(this.DefaultOnBecomeTarget())
            {
                this.AI.SelectCard(targets);
                return true;
            }
            int count = 0;
            foreach(ClientCard check in this.Enemy.GetSpells())
            {
                if (check.Type == 16777218)
                {
                    count++;
                }
            }
            if(this.Util.GetLastChainCard()!=null && 
                (this.Util.GetLastChainCard().HasType(CardType.Continuous)||
                this.Util.GetLastChainCard().HasType(CardType.Field) || count==2) &&
                this.Duel.LastChainPlayer==1)               
                {
                this.AI.SelectCard(targets);
                return true;
                }
            return false;
        }
        private bool UnendingNightmareeff()
        {
            if (this.Card.IsDisabled()){
                return false;
            }
            ClientCard card = null;
            foreach(ClientCard check in this.Enemy.GetSpells())
            {
                if (check.HasType(CardType.Continuous) || check.HasType(CardType.Field))
                {
                    card = check;
                }

                break;
            }
            int count = 0;
            foreach (ClientCard check in this.Enemy.GetSpells())
            {
                if (check.Type == 16777218)
                {
                    count++;
                }
            }
            if(count==2)
            {
                if (this.Util.GetPZone(1, 1) != null && this.Util.GetPZone(1, 1).Type == 16777218)
                {
                    card= this.Util.GetPZone(1, 1);
                }
            }
                
            if (card!=null && this.Bot.LifePoints>1000)
            {
                this.AI.SelectCard(card);
                return true;
            }
            return false;
        }

        private bool DarkBribeeff()
        {
            if (this.Util.GetLastChainCard()!=null && this.Util.GetLastChainCard().IsCode(CardId.UpstartGoblin))
            {
                return false;
            }

            return true;

        }
        private bool ImperialOrderfirst()
        {
            if (this.Util.GetLastChainCard() != null && this.Util.GetLastChainCard().IsCode(CardId.UpstartGoblin))
            {
                return false;
            }

            return this.DefaultOnBecomeTarget() && this.Util.GetLastChainCard().HasType(CardType.Spell);
        }

        private bool ImperialOrdereff()
        {
            if (this.Util.GetLastChainCard() != null && this.Util.GetLastChainCard().IsCode(CardId.UpstartGoblin))
            {
                return false;
            }

            if (this.Duel.LastChainPlayer == 1)
            {
                foreach(ClientCard check in this.Enemy.GetSpells())
                {
                    if (this.Util.GetLastChainCard() == check)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool DrowningMirrorForceeff()
        {
            if(this.Enemy.GetMonsterCount() ==1)
            {
                if(this.Enemy.BattlingMonster.Attack- this.Bot.LifePoints>=1000)
                {
                    return this.DefaultUniqueTrap();
                }
            }
            if (this.Util.GetTotalAttackingMonsterAttack(1) >= this.Bot.LifePoints)
            {
                return this.DefaultUniqueTrap();
            }

            if (this.Enemy.GetMonsterCount() >= 2)
            {
                return this.DefaultUniqueTrap();
            }

            return false;
        }
        private bool UpstartGoblineff()
        {         
            return !this.DefaultSpellWillBeNegated();
        }

        private bool PotOfDualityeff()
        {
            if (this.DefaultSpellWillBeNegated())
            {
                return false;
            }

            int count = 0;
            if (this.Bot.GetMonsterCount() > 0)
            {
                count = 1;
            }

            foreach (ClientCard card in this.Bot.Hand)
            {
                if (card.HasType(CardType.Monster))
                {
                    count++;
                }
            }
            if(this.Util.GetBestEnemyMonster()!=null && this.Util.GetBestEnemyMonster().Attack>=1900)
            {
                this.AI.SelectCard(
                    CardId.EaterOfMillions,
                    CardId.PotOfDesires,
                    CardId.GrenMajuDaEizo,
                    CardId.InspectBoarder,
                    CardId.ThunderKingRaiOh,
                    CardId.Scapegoat,
                    CardId.SolemnJudgment,
                    CardId.SolemnWarning,
                    CardId.SolemStrike,
                    CardId.InfiniteImpermanence
                    );
            }

            if (count == 0)
            {
                this.AI.SelectCard(
                    CardId.PotOfDesires,
                    CardId.InspectBoarder,
                    CardId.ThunderKingRaiOh,
                    CardId.EaterOfMillions,
                    CardId.GrenMajuDaEizo,
                    CardId.Scapegoat
                    );
            }
            else
            {
                this.AI.SelectCard(
                    CardId.PotOfDesires,
                    CardId.CardOfDemise,
                    CardId.SolemnJudgment,
                    CardId.SolemnWarning,
                    CardId.SolemStrike,
                    CardId.InfiniteImpermanence,
                    CardId.Scapegoat
                    );
            }
            return true;
        }

        private bool PotOfDesireseff()
        {
            if (this.CardOfDemiseeff_used)
            {
                return false;
            }

            return this.Bot.Deck.Count > 14 && !this.DefaultSpellWillBeNegated();
        }

        private bool CardOfDemiseeff()
        {          
            if (this.Bot.Hand.Count == 1 && this.Bot.GetSpellCountWithoutField() <= 3 && !this.DefaultSpellWillBeNegated())
            {
                this.CardOfDemiseeff_used = true;
                return true;
            }
            return false;
        }

        private bool Crackdowneff()
        {
            if (this.Util.GetOneEnemyBetterThanMyBest(true, true) != null && this.Bot.UnderAttack)
            {
                this.AI.SelectCard(this.Util.GetOneEnemyBetterThanMyBest(true, true));
            }

            return this.Util.GetOneEnemyBetterThanMyBest(true, true) != null && this.Bot.UnderAttack;
        }

        private bool MoonMirrorShieldeff()
        {
            if(this.Card.Location==CardLocation.Hand)
            {
                if (this.Bot.GetMonsterCount() == 0)
                {
                    return false;
                }

                return !this.DefaultSpellWillBeNegated();
            }
            if(this.Card.Location==CardLocation.Grave)
            {
                return true;
            }
            return false;
        }

        private bool PhatomKnightsSwordeff()
        {
            if (this.Card.IsFaceup())
            {
                return true;
            }

            if (this.Duel.Phase==DuelPhase.BattleStart && this.Bot.BattlingMonster!=null && this.Enemy.BattlingMonster!=null)
            {                
                if (this.Bot.BattlingMonster.Attack + 800 >= this.Enemy.BattlingMonster.GetDefensePower())
                {
                    this.AI.SelectCard(this.Bot.BattlingMonster);
                    return this.DefaultUniqueTrap();
                }                
            }
            return false;
        }
        private bool InspectBoardersummon()
        {           
            if (this.Bot.MonsterZone[0] == null)
            {
                this.AI.SelectPlace(Zones.MonsterZone1);
            }
            else
            {
                this.AI.SelectPlace(Zones.MonsterZone5);
            }

            return true;
        }
        private bool GrenMajuDaEizosummon()
        {
            if (this.Duel.Turn == 1)
            {
                return false;
            }

            if (this.Bot.MonsterZone[0] == null)
            {
                this.AI.SelectPlace(Zones.MonsterZone1);
            }
            else
            {
                this.AI.SelectPlace(Zones.MonsterZone5);
            }

            return this.Bot.Banished.Count >= 6;
        }

        private bool ThunderKingRaiOhsummon()
        { 
            if (this.Bot.MonsterZone[0] == null)
            {
                this.AI.SelectPlace(Zones.MonsterZone1);
            }
            else
            {
                this.AI.SelectPlace(Zones.MonsterZone5);
            }

            return true;
        }

        private bool ThunderKingRaiOheff()
        {
           if(this.Duel.SummoningCards.Count > 0)
           {
                foreach(ClientCard m in this.Duel.SummoningCards)
                {
                    if (m.Attack >= 1900)
                    {
                        return true;
                    }
                }
           }            
            return false;
        }

        private bool BorreloadDragonsp()
        {
            if (!(this.Bot.HasInMonstersZone(CardId.MissusRadiant) || this.Bot.HasInMonstersZone(new[] { CardId.KnightmareCerberus, CardId.KnightmarePhoenix })))
            {
                return false;
            }

            IList<ClientCard> material_list = new List<ClientCard>();
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.IsCode(CardId.MissusRadiant, CardId.KnightmareCerberus, CardId.KnightmarePhoenix, CardId.LinkSpider, CardId.Linkuriboh))
                {
                    material_list.Add(monster);
                }

                if (material_list.Count == 3)
                {
                    break;
                }
            }
            if (material_list.Count >= 3)
            {
                this.AI.SelectMaterials(material_list);
                return true;
            }
            return false;
        }
        private bool BorreloadDragonspsecond()
        {
            if (!(this.Bot.HasInMonstersZone(CardId.MissusRadiant) || this.Bot.HasInMonstersZone(new[] { CardId.KnightmareCerberus,CardId.KnightmarePhoenix })))
            {
                return false;
            }

            IList<ClientCard> material_list = new List<ClientCard>();
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.IsCode(CardId.MissusRadiant, CardId.KnightmareCerberus, CardId.KnightmarePhoenix, CardId.LinkSpider, CardId.Linkuriboh))
                {
                    material_list.Add(monster);
                }

                if (material_list.Count == 3)
                {
                    break;
                }
            }
            if (material_list.Count >= 3)
            {
                this.AI.SelectMaterials(material_list);
                return true;
            }
            return false;
        }
        public bool BorreloadDragoneff()
        {
            if (this.ActivateDescription == -1 && (this.Duel.Phase==DuelPhase.BattleStart|| this.Duel.Phase==DuelPhase.End))
            {
                ClientCard enemy_monster = this.Enemy.BattlingMonster;
                if (enemy_monster != null && enemy_monster.HasPosition(CardPosition.Attack))
                {
                    return (this.Card.Attack - enemy_monster.Attack < this.Enemy.LifePoints);
                }
                return true;
            };
            ClientCard BestEnemy = this.Util.GetBestEnemyMonster(true);
            ClientCard WorstBot = this.Bot.GetMonsters().GetLowestAttackMonster();
            if (BestEnemy == null || BestEnemy.HasPosition(CardPosition.FaceDown))
            {
                return false;
            }

            if (WorstBot == null || WorstBot.HasPosition(CardPosition.FaceDown))
            {
                return false;
            }

            if (BestEnemy.Attack >= WorstBot.RealPower)
            {
                this.AI.SelectCard(BestEnemy);
                return true;
            }
            return false;
        }

        private bool EaterOfMillionssp()
        {
            if (this.Bot.MonsterZone[0] == null)
            {
                this.AI.SelectPlace(Zones.MonsterZone1);
            }
            else
            {
                this.AI.SelectPlace(Zones.MonsterZone5);
            }

            if (this.Enemy.HasInMonstersZone(CardId.KnightmareGryphon, true))
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.InspectBoarder) && !this.eater_eff)
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
            IList<ClientCard> targets = new List<ClientCard>();            
            foreach (ClientCard e_c in this.Bot.ExtraDeck)
            {                
                targets.Add(e_c);                
                if (targets.Count >= 5)
                {
                    this.AI.SelectCard(targets);
                    /*AI.SelectCard(new[] {
                        CardId.BingirsuTheWorldChaliceWarrior,
                        CardId.TopologicTrisbaena,
                        CardId.KnightmareCerberus,
                        CardId.KnightmarePhoenix,
                        CardId.KnightmareUnicorn,
                        CardId.BrandishMaidenKagari,
                        CardId.HeavymetalfoesElectrumite,
                        CardId.CrystronNeedlefiber,
                        CardId.FirewallDragon,
                        CardId.BirrelswordDragon,
                        CardId.RaidraptorUltimateFalcon,
                    });*/

                    this.AI.SelectPlace(Zones.MonsterZone5 | Zones.MonsterZone1);
                    return true;
                }
            }
            Logger.DebugWriteLine("*** Eater use up the extra deck.");
            foreach (ClientCard s_c in this.Bot.GetSpells())
            {
                targets.Add(s_c);
                if (targets.Count >= 5)
                {
                    this.AI.SelectCard(targets);
                    return true;
                }
            }
            return false;
        }

        private bool EaterOfMillionseff()
        {
            if (this.Enemy.BattlingMonster.HasPosition(CardPosition.Attack) && (this.Bot.BattlingMonster.Attack - this.Enemy.BattlingMonster.GetDefensePower() >= this.Enemy.LifePoints))
            {
                return false;
            }

            return true;
        }

        private bool WakingTheDragoneff()
        {
            this.AI.SelectCard(new[] { CardId.RaidraptorUltimateFalcon });
            return true;
        }

        private bool MetalSnakesp()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.MetalSnake, 0) && !this.Bot.HasInMonstersZone(CardId.MetalSnake))
            {
                if(this.Duel.Player == 1 && this.Duel.Phase >= DuelPhase.BattleStart )
                {
                    return this.Bot.Deck.Count >= 12;
                }

                if (this.Duel.Player == 0 && this.Duel.Phase >= DuelPhase.Main1)
                {
                    return this.Bot.Deck.Count >= 12;
                }
            }              
            return false;
        }

        private bool MetalSnakeeff()
        {
            ClientCard target = this.Util.GetOneEnemyBetterThanMyBest(true, true);
            if (this.ActivateDescription == this.Util.GetStringId(CardId.MetalSnake, 1) && target != null)
            {
                this.AI.SelectCard(new[]
                {                    
                    CardId.HeavymetalfoesElectrumite,
                    CardId.BrandishMaidenKagari,
                    CardId.CrystronNeedlefiber,
                    CardId.RaidraptorUltimateFalcon,
                    CardId.NingirsuTheWorldChaliceWarrior
                });
                this.AI.SelectNextCard(target);
                return true;
            }
            return false;    
            
        }
        private bool MissusRadiantsp()
        {                       
            IList<ClientCard> material_list = new List<ClientCard>();
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.HasAttribute(CardAttribute.Earth) && monster.Level==1 && !monster.IsCode(CardId.EaterOfMillions))
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
                if (!c.IsCode(CardId.EaterOfMillions, CardId.Linkuriboh) && c.Level==1)
                {
                    this.AI.SelectMaterials(c);
                    return true;
                }
            }
            return false;
        }

        private bool Knightmaresp()
        {
            int[] firstMats = new[] {
              CardId.KnightmareCerberus,
              CardId.KnightmarePhoenix
            };
            if (this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsCode(firstMats)) >= 1)
            {
                return false;
            }

            foreach (ClientCard c in this.Bot.GetMonsters())
            {
                if (!c.IsCode(CardId.EaterOfMillions) && c.Level == 1)
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
        private bool MonsterRepos()
        {
            if (this.Card.IsCode(CardId.EaterOfMillions) && this.Card.IsAttack())
            {
                return false;
            }

            return this.DefaultMonsterRepos();
        }

        private bool SpellSet()
        {
            int count = 0;
            foreach(ClientCard check in this.Bot.Hand)
            {
                if (check.IsCode(CardId.CardOfDemise))
                {
                    count++;
                }
            }
            if (count == 2 && this.Bot.Hand.Count == 2 && this.Bot.GetSpellCountWithoutField() <= 2)
            {
                return true;
            }

            if (this.Card.IsCode(CardId.MacroCosmos) && this.Bot.HasInSpellZone(CardId.MacroCosmos))
            {
                return false;
            }

            if (this.Card.IsCode(CardId.AntiSpellFragrance) && this.Bot.HasInSpellZone(CardId.AntiSpellFragrance))
            {
                return false;
            }

            if (this.CardOfDemiseeff_used)
            {
                return true;
            }

            if (this.Card.IsCode(CardId.EvenlyMatched) && (this.Enemy.GetFieldCount() - this.Bot.GetFieldCount()) < 0)
            {
                return false;
            }

            if (this.Card.IsCode(CardId.AntiSpellFragrance) && this.Bot.HasInSpellZone(CardId.AntiSpellFragrance))
            {
                return false;
            }

            if (this.Card.IsCode(CardId.MacroCosmos) && this.Bot.HasInSpellZone(CardId.MacroCosmos))
            {
                return false;
            }

            if (this.Duel.Turn > 1 && this.Duel.Phase == DuelPhase.Main1 && this.Bot.HasAttackingMonster())
            {
                return false;
            }

            if (this.Card.IsCode(CardId.InfiniteImpermanence))
            {
                return this.Bot.GetFieldCount() > 0 && this.Bot.GetSpellCountWithoutField() < 4;
            }

            if (this.Card.IsCode(CardId.Scapegoat))
            {
                return true;
            }

            if (this.Card.HasType(CardType.Trap))
            {
                return this.Bot.GetSpellCountWithoutField() < 4;
            }

            if (this.Bot.HasInSpellZone(CardId.AntiSpellFragrance,true))
            {
                if (this.Card.IsCode(CardId.UpstartGoblin, CardId.PotOfDesires, CardId.PotOfDuality))
                {
                    return true;
                }

                if (this.Card.IsCode(CardId.CardOfDemise) && this.Bot.HasInSpellZone(CardId.CardOfDemise))
                {
                    return false;
                }

                if (this.Card.HasType(CardType.Spell))
                {
                    return this.Bot.GetSpellCountWithoutField() < 4;
                }
            }
            return false;
        }
        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (attacker.IsCode(DefaultExecutor.CardId.EaterOfMillions) && (this.Bot.HasInMonstersZone(CardId.InspectBoarder) && this.eater_eff) && !attacker.IsDisabled())
            {
                attacker.RealPower = 9999;
                return true;
            }
            if (attacker.IsCode(DefaultExecutor.CardId.EaterOfMillions) && !this.Bot.HasInMonstersZone(CardId.InspectBoarder) && !attacker.IsDisabled())
            {
                attacker.RealPower = 9999;
                return true;
            }            
            return base.OnPreBattleBetween(attacker, defender);
        }
        public override ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
        {
            for (int i = 0; i < attackers.Count; ++i)
            {
                ClientCard attacker = attackers[i];
                if (attacker.IsCode(CardId.BirrelswordDragon, CardId.EaterOfMillions))
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
