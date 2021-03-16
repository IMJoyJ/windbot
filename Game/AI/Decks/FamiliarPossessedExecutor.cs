using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using System.Linq;

namespace WindBot.Game.AI.Decks
{
    [Deck("FamiliarPossessed", "AI_FamiliarPossessed")]
    public class FamiliarPossessedExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int MetalSnake = 71197066;
            public const int InspectBoarder = 15397015;
            public const int AshBlossomAndJoyousSpring = 14558127;
            public const int GrenMajuDaEizo = 36584821;
            public const int MaxxC = 23434538;

            public const int Aussa = 31887906;
            public const int Eria = 68881650;
            public const int Wynn = 31764354;
            public const int Hiita = 4376659;
            public const int Lyna = 40542825;
            public const int Awakening = 62256492;
            public const int Unpossessed = 25704359;

            public const int NaturalExterio = 99916754;
            public const int NaturalBeast = 33198837;
            public const int SwordsmanLV7 = 37267041;
            public const int RoyalDecreel = 51452091;

            public const int HarpieFeatherDuster = 18144506;
            public const int PotOfDesires = 35261759;
            public const int PotofExtravagance = 49238328;
            public const int Scapegoat = 73915051;
            public const int MacroCosmos = 30241314;
            public const int Crackdown = 36975314;
            public const int ImperialOrder = 61740673;
            public const int SolemnWarning = 84749824;
            public const int SolemStrike = 40605147;
            public const int SolemnJudgment = 41420027;
            public const int SkillDrain = 82732705;
            public const int Mistake = 59305593;

            public const int BorreloadDragon = 31833038;
            public const int BirrelswordDragon = 85289965;
            public const int KnightmareGryphon = 65330383;
            public const int KnightmareUnicorn = 38342335;
            public const int KnightmarePhoenix = 2857636;
            public const int KnightmareCerberus = 75452921;
            public const int LinkSpider = 98978921;
            public const int Linkuriboh = 41999284;
            public const int GagagaCowboy = 12014404;

            public const int AussaP = 97661969;
            public const int EriaP = 73309655;
            public const int WynnP = 30674956;
            public const int HiitaP = 48815792;
            public const int LynaP = 9839945;

            // side
            public const int Raigeki = 12580477;
            public const int lightningStorm = 14532163;
            public const int CosmicCyclone = 8267140;
            public const int CalledByTheGrave = 24224830;
            public const int CrossoutDesignator = 65681983;
            public const int InfiniteImpermanence = 10045474;
        }

        public FamiliarPossessedExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // do first
            this.AddExecutor(ExecutorType.Activate, CardId.PotofExtravagance, this.PotofExtravaganceActivate);
            // burn if enemy's LP is below 800
            this.AddExecutor(ExecutorType.SpSummon, CardId.GagagaCowboy, this.GagagaCowboySummon);
            this.AddExecutor(ExecutorType.Activate, CardId.GagagaCowboy);
            //Sticker
            this.AddExecutor(ExecutorType.Activate, CardId.MacroCosmos, this.MacroCosmoseff);
            //counter
            this.AddExecutor(ExecutorType.Activate, CardId.CalledByTheGrave, this.DefaultCalledByTheGrave);
            // AddExecutor(ExecutorType.Activate, CardId.CrossoutDesignator, DefaultCalledByTheGrave);
            this.AddExecutor(ExecutorType.Activate, CardId.InfiniteImpermanence, this.DefaultInfiniteImpermanence);
            this.AddExecutor(ExecutorType.Activate, CardId.AshBlossomAndJoyousSpring, this.DefaultAshBlossomAndJoyousSpring);
            this.AddExecutor(ExecutorType.Activate, CardId.MaxxC, this.DefaultMaxxC);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnWarning, this.DefaultSolemnWarning);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemStrike, this.DefaultSolemnStrike);
            this.AddExecutor(ExecutorType.Activate, CardId.ImperialOrder, this.ImperialOrderfirst);
            this.AddExecutor(ExecutorType.Activate, CardId.ImperialOrder, this.ImperialOrdereff);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnJudgment, this.DefaultSolemnJudgment);
            this.AddExecutor(ExecutorType.Activate, CardId.SkillDrain, this.SkillDrainEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Mistake, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.Awakening);
            this.AddExecutor(ExecutorType.Activate, CardId.Unpossessed, this.UnpossessedEffect);
            //first do
            this.AddExecutor(ExecutorType.Activate, CardId.lightningStorm, this.DefaultLightingStorm);
            this.AddExecutor(ExecutorType.Activate, CardId.HarpieFeatherDuster, this.DefaultHarpiesFeatherDusterFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.CosmicCyclone, this.DefaultMysticalSpaceTyphoon);
            this.AddExecutor(ExecutorType.Activate, CardId.Raigeki, this.DefaultRaigeki);

            this.AddExecutor(ExecutorType.Activate, CardId.PotOfDesires, this.PotOfDesireseff);
            //sp
            this.AddExecutor(ExecutorType.Activate, CardId.Linkuriboh, this.Linkuriboheff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Linkuriboh, this.Linkuribohsp);
            this.AddExecutor(ExecutorType.SpSummon, CardId.KnightmareCerberus, this.Knightmaresp);
            this.AddExecutor(ExecutorType.SpSummon, CardId.KnightmarePhoenix, this.Knightmaresp);
            this.AddExecutor(ExecutorType.SpSummon, CardId.AussaP, this.AussaPsp);
            this.AddExecutor(ExecutorType.Activate, CardId.AussaP, this.AussaPeff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.EriaP, this.EriaPsp);
            this.AddExecutor(ExecutorType.Activate, CardId.EriaP, this.EriaPeff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.WynnP, this.WynnPsp);
            this.AddExecutor(ExecutorType.Activate, CardId.WynnP, this.WynnPeff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.HiitaP, this.HiitaPsp);
            this.AddExecutor(ExecutorType.Activate, CardId.HiitaP, this.HiitaPeff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.LynaP, this.LynaPsp);
            this.AddExecutor(ExecutorType.Activate, CardId.LynaP, this.LynaPeff);

            this.AddExecutor(ExecutorType.SpSummon, CardId.Linkuriboh, this.Linkuribohsp);
            this.AddExecutor(ExecutorType.SpSummon, CardId.LinkSpider);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BorreloadDragon, this.BorreloadDragonsp);
            this.AddExecutor(ExecutorType.Activate, CardId.BorreloadDragon, this.BorreloadDragoneff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BirrelswordDragon, this.BirrelswordDragonsp);
            this.AddExecutor(ExecutorType.Activate, CardId.BirrelswordDragon, this.BirrelswordDragoneff);
            // normal summon
            this.AddExecutor(ExecutorType.Summon, CardId.InspectBoarder, this.InspectBoardersummon);
            this.AddExecutor(ExecutorType.Summon, CardId.GrenMajuDaEizo, this.GrenMajuDaEizosummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BorreloadDragon, this.BorreloadDragonspsecond);

            this.AddExecutor(ExecutorType.Summon, CardId.Aussa, this.FamiliarPossessedsummon);
            this.AddExecutor(ExecutorType.Summon, CardId.Eria, this.FamiliarPossessedsummon);
            this.AddExecutor(ExecutorType.Summon, CardId.Wynn, this.FamiliarPossessedsummon);
            this.AddExecutor(ExecutorType.Summon, CardId.Hiita, this.FamiliarPossessedsummon);
            this.AddExecutor(ExecutorType.Summon, CardId.Lyna, this.FamiliarPossessedsummon);

            this.AddExecutor(ExecutorType.Activate, CardId.MetalSnake, this.MetalSnakesp);
            this.AddExecutor(ExecutorType.Activate, CardId.MetalSnake, this.MetalSnakeeff);
            //spell
            this.AddExecutor(ExecutorType.Activate, CardId.Crackdown, this.Crackdowneff);
            this.AddExecutor(ExecutorType.Activate, CardId.Scapegoat, this.DefaultScapegoat);
            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
            //set
            this.AddExecutor(ExecutorType.SpellSet, this.SpellSet);
        }

        public void SelectSTPlace(ClientCard card = null, bool avoid_Impermanence = false, List<int> avoid_list = null)
        {
            List<int> list = new List<int> { 0, 1, 2, 3, 4 };
            int n = list.Count;
            while (n-- > 1)
            {
                int index = Program._rand.Next(n + 1);
                int temp = list[index];
                list[index] = list[n];
                list[n] = temp;
            }
            foreach (int seq in list)
            {
                int zone = (int)System.Math.Pow(2, seq);
                if (this.Bot.SpellZone[seq] == null)
                {
                    if (card != null && card.Location == CardLocation.Hand && avoid_Impermanence)
                    {
                        continue;
                    }

                    if (avoid_list != null && avoid_list.Contains(seq))
                    {
                        continue;
                    }

                    this.AI.SelectPlace(zone);
                    return;
                };
            }
            this.AI.SelectPlace(0);
        }

        public bool SpellNegatable(bool isCounter = false, ClientCard target = null)
        {
            // target default set
            if (target == null)
            {
                target = this.Card;
            }
            // won't negate if not on field
            if (target.Location != CardLocation.SpellZone && target.Location != CardLocation.Hand)
            {
                return false;
            }

            // negate judge
            if (this.Enemy.HasInMonstersZone(CardId.NaturalExterio, true) && !isCounter)
            {
                return true;
            }

            if (target.IsSpell())
            {
                if (this.Enemy.HasInMonstersZone(CardId.NaturalBeast, true))
                {
                    return true;
                }

                if (this.Enemy.HasInSpellZone(CardId.ImperialOrder, true) || this.Bot.HasInSpellZone(CardId.ImperialOrder, true))
                {
                    return true;
                }

                if (this.Enemy.HasInMonstersZone(CardId.SwordsmanLV7, true) || this.Bot.HasInMonstersZone(CardId.SwordsmanLV7, true))
                {
                    return true;
                }
            }
            if (target.IsTrap())
            {
                if (this.Enemy.HasInSpellZone(CardId.RoyalDecreel, true) || this.Bot.HasInSpellZone(CardId.RoyalDecreel, true))
                {
                    return true;
                }
            }
            // how to get here?
            return false;
        }

        private bool MacroCosmoseff()
        {

            return (this.Duel.LastChainPlayer == 1 || this.Duel.LastSummonPlayer == 1 || this.Duel.Player == 0) && this.UniqueFaceupSpell();
        }

        private bool ImperialOrderfirst()
        {
            if (this.Util.GetLastChainCard() != null && this.Util.GetLastChainCard().IsCode(CardId.PotOfDesires))
            {
                return false;
            }

            return this.DefaultOnBecomeTarget() && this.Util.GetLastChainCard().HasType(CardType.Spell);
        }

        private bool ImperialOrdereff()
        {
            if (this.Util.GetLastChainCard() != null && this.Util.GetLastChainCard().IsCode(CardId.PotOfDesires))
            {
                return false;
            }

            if (this.Duel.LastChainPlayer == 1)
            {
                foreach (ClientCard check in this.Enemy.GetSpells())
                {
                    if (this.Util.GetLastChainCard() == check)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool PotOfDesireseff()
        {
            return this.Bot.Deck.Count > 14 && !this.DefaultSpellWillBeNegated();
        }

        // activate of PotofExtravagance
        public bool PotofExtravaganceActivate()
        {
            // won't activate if it'll be negate
            if (this.SpellNegatable())
            {
                return false;
            }

            this.SelectSTPlace(this.Card, true);
            this.AI.SelectOption(1);
            return true;
        }

        private bool Crackdowneff()
        {
            if (this.Util.GetOneEnemyBetterThanMyBest(true, true) != null && this.Bot.UnderAttack)
            {
                this.AI.SelectCard(this.Util.GetOneEnemyBetterThanMyBest(true, true));
            }

            return this.Util.GetOneEnemyBetterThanMyBest(true, true) != null && this.Bot.UnderAttack;
        }

        private bool SkillDrainEffect()
        {
            return (this.Bot.LifePoints > 1000) && this.DefaultUniqueTrap();
        }

        private bool UnpossessedEffect()
        {
            this.AI.SelectCard(new List<int>() {
                CardId.Lyna,
                CardId.Hiita,
                CardId.Wynn,
                CardId.Eria,
                CardId.Aussa
            });
            return true;
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

            if (this.Bot.HasInSpellZone(CardId.SkillDrain) || this.Enemy.HasInSpellZone(CardId.SkillDrain))
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

        private bool FamiliarPossessedsummon()
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

        private bool BorreloadDragonsp()
        {
            if (!(this.Bot.HasInMonstersZone(new[] { CardId.KnightmareCerberus, CardId.KnightmarePhoenix, CardId.LynaP, CardId.HiitaP, CardId.WynnP, CardId.EriaP, CardId.AussaP })))
            {
                return false;
            }

            IList<ClientCard> material_list = new List<ClientCard>();
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.IsCode(CardId.LynaP, CardId.HiitaP, CardId.WynnP, CardId.EriaP, CardId.AussaP, CardId.KnightmareCerberus, CardId.KnightmarePhoenix, CardId.LinkSpider, CardId.Linkuriboh))
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
            if (!(this.Bot.HasInMonstersZone(new[] { CardId.KnightmareCerberus, CardId.KnightmarePhoenix, CardId.LynaP, CardId.HiitaP, CardId.WynnP, CardId.EriaP, CardId.AussaP })))
            {
                return false;
            }

            IList<ClientCard> material_list = new List<ClientCard>();
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.IsCode(CardId.LynaP, CardId.HiitaP, CardId.WynnP, CardId.EriaP, CardId.AussaP, CardId.KnightmareCerberus, CardId.KnightmarePhoenix, CardId.LinkSpider, CardId.Linkuriboh))
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
            if (this.ActivateDescription == -1 && (this.Duel.Phase == DuelPhase.BattleStart || this.Duel.Phase == DuelPhase.End))
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

        private bool BirrelswordDragonsp()
        {
            IList<ClientCard> material_list = new List<ClientCard>();
            foreach (ClientCard m in this.Bot.GetMonsters())
            {
                if (m.IsCode(CardId.KnightmareCerberus, CardId.KnightmarePhoenix, CardId.LynaP, CardId.HiitaP, CardId.WynnP, CardId.EriaP, CardId.AussaP))
                {
                    material_list.Add(m);
                    break;
                }
            }
            foreach (ClientCard m in this.Bot.GetMonsters())
            {
                if (m.IsCode(CardId.Linkuriboh) || m.Level == 1)
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

        private bool MetalSnakesp()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.MetalSnake, 0) && !this.Bot.HasInMonstersZone(CardId.MetalSnake))
            {
                if (this.Duel.Player == 1 && this.Duel.Phase >= DuelPhase.BattleStart)
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
                CardId.LynaP,
                CardId.HiitaP,
                CardId.WynnP,
                CardId.EriaP,
                CardId.KnightmareGryphon
                });
                this.AI.SelectNextCard(target);
                return true;
            }
            return false;

        }

        private bool AussaPsp()
        {
            IList<ClientCard> material_list = new List<ClientCard>();
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.HasAttribute(CardAttribute.Earth) && !monster.IsCode(CardId.KnightmareCerberus, CardId.InspectBoarder, CardId.GrenMajuDaEizo, CardId.KnightmarePhoenix, CardId.LynaP, CardId.HiitaP, CardId.WynnP, CardId.EriaP, CardId.AussaP, CardId.KnightmareUnicorn, CardId.KnightmareGryphon, CardId.BorreloadDragon, CardId.BirrelswordDragon))
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

            if (this.Bot.HasInMonstersZone(CardId.AussaP))
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

        private bool AussaPeff()
        {
            this.AI.SelectCard(CardId.MaxxC, CardId.Aussa);
            return true;
        }

        private bool EriaPsp()
        {
            IList<ClientCard> material_list = new List<ClientCard>();
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.HasAttribute(CardAttribute.Water) && !monster.IsCode(CardId.KnightmareCerberus, CardId.InspectBoarder, CardId.GrenMajuDaEizo, CardId.KnightmarePhoenix, CardId.LynaP, CardId.HiitaP, CardId.WynnP, CardId.EriaP, CardId.AussaP, CardId.KnightmareUnicorn, CardId.KnightmareGryphon, CardId.BorreloadDragon, CardId.BirrelswordDragon))
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

            if (this.Bot.HasInMonstersZone(CardId.EriaP))
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

        private bool EriaPeff()
        {
            this.AI.SelectCard(CardId.Eria);
            return true;
        }

        private bool WynnPsp()
        {
            IList<ClientCard> material_list = new List<ClientCard>();
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.HasAttribute(CardAttribute.Wind) && !monster.IsCode(CardId.KnightmareCerberus, CardId.InspectBoarder, CardId.GrenMajuDaEizo, CardId.KnightmarePhoenix, CardId.LynaP, CardId.HiitaP, CardId.WynnP, CardId.EriaP, CardId.AussaP, CardId.KnightmareUnicorn, CardId.KnightmareGryphon, CardId.BorreloadDragon, CardId.BirrelswordDragon))
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

            if (this.Bot.HasInMonstersZone(CardId.WynnP))
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

        private bool WynnPeff()
        {
            this.AI.SelectCard(CardId.Wynn);
            return true;
        }

        private bool HiitaPsp()
        {
            IList<ClientCard> material_list = new List<ClientCard>();
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.HasAttribute(CardAttribute.Fire) && !monster.IsCode(CardId.KnightmareCerberus, CardId.InspectBoarder, CardId.GrenMajuDaEizo, CardId.KnightmarePhoenix, CardId.LynaP, CardId.HiitaP, CardId.WynnP, CardId.EriaP, CardId.AussaP, CardId.KnightmareUnicorn, CardId.KnightmareGryphon, CardId.BorreloadDragon, CardId.BirrelswordDragon))
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

            if (this.Bot.HasInMonstersZone(CardId.HiitaP))
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

        private bool HiitaPeff()
        {
            this.AI.SelectCard(CardId.Hiita);
            return true;
        }

        private bool LynaPsp()
        {
            IList<ClientCard> material_list = new List<ClientCard>();
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.HasAttribute(CardAttribute.Light) && !monster.IsCode(CardId.KnightmareCerberus, CardId.InspectBoarder, CardId.GrenMajuDaEizo, CardId.KnightmarePhoenix, CardId.LynaP, CardId.HiitaP, CardId.WynnP, CardId.EriaP, CardId.AussaP, CardId.KnightmareUnicorn, CardId.KnightmareGryphon, CardId.BorreloadDragon, CardId.BirrelswordDragon))
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

            if (this.Bot.HasInMonstersZone(CardId.LynaP))
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

        private bool LynaPeff()
        {
            this.AI.SelectCard(CardId.Lyna);
            return true;
        }

        private bool Linkuribohsp()
        {

            foreach (ClientCard c in this.Bot.GetMonsters())
            {
                if (c.Level == 1)
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
                if (c.Level == 1)
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

        private bool GagagaCowboySummon()
        {
            if (this.Enemy.LifePoints <= 800 || (this.Bot.GetMonsterCount() >= 4 && this.Enemy.LifePoints <= 1600))
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;
        }
        private bool SpellSet()
        {
            if (this.Card.IsCode(CardId.MacroCosmos) && this.Bot.HasInSpellZone(CardId.MacroCosmos))
            {
                return false;
            }

            if (this.Card.IsCode(CardId.Unpossessed) && this.Bot.HasInSpellZone(CardId.Unpossessed))
            {
                return false;
            }

            if (this.Card.IsCode(CardId.Crackdown) && this.Bot.HasInSpellZone(CardId.Crackdown))
            {
                return false;
            }

            if (this.Card.IsCode(CardId.SkillDrain) && this.Bot.HasInSpellZone(CardId.SkillDrain))
            {
                return false;
            }

            if (this.Card.IsCode(CardId.Mistake) && this.Bot.HasInSpellZone(CardId.Mistake))
            {
                return false;
            }

            if (this.Card.IsCode(CardId.Scapegoat))
            {
                return true;
            }

            if (this.Card.HasType(CardType.Trap))
            {
                return this.Bot.GetSpellCountWithoutField() < 4;
            }

            return false;
        }
        public override ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
        {
            for (int i = 0; i < attackers.Count; ++i)
            {
                ClientCard attacker = attackers[i];
                if (attacker.IsCode(CardId.BirrelswordDragon, CardId.BorreloadDragon))
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
