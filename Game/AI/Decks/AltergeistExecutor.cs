using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("Altergeist", "AI_Altergeist")]
    public class AltergeistExecutor : DefaultExecutor
    {

        public class CardId
        {
            public const int Kunquery = 52927340;
            public const int Marionetter = 53143898;
            public const int Multifaker = 42790071;
            public const int AB_JS = 14558127;
            public const int GO_SR = 59438930;
            public const int GR_WC = 62015408;
            public const int GB_HM = 73642296;
            public const int Silquitous = 89538537;
            public const int MaxxC = 23434538;
            public const int Meluseek = 25533642;
            public const int OneForOne = 2295440;
            public const int PotofDesires = 35261759;
            public const int PotofIndulgence = 49238328;
            public const int Impermanence = 10045474;
            public const int WakingtheDragon = 10813327;
            public const int EvenlyMatched = 15693423;
            public const int Storm = 23924608;
            public const int Manifestation = 35146019;
            public const int Protocol = 27541563;
            public const int Spoofing = 53936268;
            public const int ImperialOrder = 61740673;
            public const int SolemnStrike = 40605147;
            public const int SolemnJudgment = 41420027;
            public const int NaturalExterio = 99916754;
            public const int UltimateFalcon = 86221741;
            public const int Borrelsword = 85289965;
            public const int FWD = 05043010;
            public const int TripleBurstDragon = 49725936;
            public const int HeavymetalfoesElectrumite = 24094258;
            public const int Isolde = 59934749;
            public const int Hexstia = 1508649;
            public const int Needlefiber = 50588353;
            public const int Kagari = 63288573;
            public const int Shizuku = 90673288;
            public const int Linkuriboh = 41999284;
            public const int Anima = 94259633;

            public const int SecretVillage = 68462976;

            public const int DarkHole = 53129443;
            public const int NaturalBeast = 33198837;
            public const int SwordsmanLV7 = 37267041;
            public const int RoyalDecreel = 51452091;
            public const int Anti_Spell = 58921041;
            public const int Hayate = 8491308;
            public const int Raye = 26077387;
            public const int Drones_Token = 52340445;
            public const int Iblee = 10158145;
        }

        readonly List<int> Impermanence_list = new List<int>();
        bool Multifaker_ssfromhand = false;
        bool Multifaker_ssfromdeck = false;
        bool Marionetter_reborn = false;
        bool Hexstia_searched = false;
        bool Meluseek_searched = false;
        bool summoned = false;
        bool Silquitous_bounced = false;
        bool Silquitous_recycled = false;
        bool ss_other_monster = false;
        readonly List<ClientCard> attacked_Meluseek = new List<ClientCard>();
        readonly List<int> SkyStrike_list = new List<int> {
            CardId.Raye, CardId.Hayate, CardId.Kagari, CardId.Shizuku,
            21623008, 25955749, 63166095, 99550630,
            25733157, 51227866, CardId.Drones_Token-1,98338152,
            24010609, 97616504, 50005218
        };
        readonly List<int> cards_improper = new List<int>
        {
            0,CardId.WakingtheDragon, CardId.SolemnStrike, CardId.Spoofing,   CardId.OneForOne, CardId.PotofDesires,
            CardId.Manifestation, CardId.SecretVillage, CardId.ImperialOrder,   DefaultExecutor.CardId.HarpiesFeatherDuster, CardId.GR_WC,
            CardId.Protocol, CardId.SolemnJudgment, CardId.Storm, CardId.GO_SR, CardId.Silquitous,
            CardId.MaxxC,  CardId.Impermanence, CardId.Meluseek,   CardId.AB_JS, CardId.Kunquery,
            CardId.Marionetter, CardId.Multifaker
        };
        readonly List<int> normal_counter = new List<int>
        {
            53262004, 98338152, 32617464, 45041488, CardId.SolemnStrike,
            61257789, 23440231, 27354732, 12408276, 82419869, CardId.Impermanence,
            49680980, 18621798, 38814750, 17266660, 94689635,CardId.AB_JS,
            74762582, 75286651, 4810828,  44665365, 21123811, DefaultExecutor.CardId.CrystalWingSynchroDragon,
            82044279, 82044280, 79606837, 10443957, 1621413,  CardId.Protocol,
            90809975, 8165596,  9753964,  53347303, 88307361, DefaultExecutor.CardId.GamecieltheSeaTurtleKaiju,
            5818294,  2948263,  6150044,  26268488, 51447164, DefaultExecutor.CardId.JizukirutheStarDestroyingKaiju,
            97268402
        };
        readonly List<int> should_not_negate = new List<int>
        {
            81275020, 28985331
        };

        public AltergeistExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // negate
            this.AddExecutor(ExecutorType.Activate, DefaultExecutor.CardId.ChickenGame, this.ChickenGame);
            this.AddExecutor(ExecutorType.Repos, this.EvenlyMatched_Repos);

            this.AddExecutor(ExecutorType.Activate, CardId.MaxxC, this.G_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.Anti_Spell, this.Anti_Spell_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.PotofIndulgence, this.PotofIndulgence_activate);

            this.AddExecutor(ExecutorType.Activate, this.field_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.SecretVillage, this.SecretVillage_activate);

            this.AddExecutor(ExecutorType.Activate, CardId.Hexstia, this.Hexstia_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.NaturalExterio, this.NaturalExterio_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.TripleBurstDragon, this.TripleBurstDragon_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.ImperialOrder, this.ImperialOrder_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, this.SolemnStrike_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnJudgment, this.SolemnJudgment_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.Protocol, this.Protocol_negate_better);
            this.AddExecutor(ExecutorType.Activate, CardId.Impermanence, this.Impermanence_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.Protocol, this.Protocol_negate);
            this.AddExecutor(ExecutorType.Activate, CardId.Protocol, this.Protocol_activate_not_use);
            this.AddExecutor(ExecutorType.Activate, CardId.AB_JS, this.Hand_act_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.GB_HM, this.Hand_act_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.GO_SR, this.Hand_act_eff);

            this.AddExecutor(ExecutorType.Activate, CardId.GR_WC, this.GR_WC_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.WakingtheDragon, this.WakingtheDragon_eff);

            // clear
            this.AddExecutor(ExecutorType.Activate, CardId.EvenlyMatched, this.EvenlyMatched_activate);
            this.AddExecutor(ExecutorType.Activate, DefaultExecutor.CardId.HarpiesFeatherDuster, this.Feather_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.Storm, this.Storm_activate);

            this.AddExecutor(ExecutorType.Activate, CardId.Meluseek, this.Meluseek_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Silquitous, this.Silquitous_eff);

            this.AddExecutor(ExecutorType.Activate, CardId.Borrelsword, this.Borrelsword_eff);

            // spsummon
            this.AddExecutor(ExecutorType.Activate, CardId.Multifaker, this.Multifaker_handss);
            this.AddExecutor(ExecutorType.Activate, CardId.Manifestation, this.Manifestation_eff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Anima, this.Anima_ss);
            this.AddExecutor(ExecutorType.Activate, CardId.Anima);
            this.AddExecutor(ExecutorType.Activate, CardId.Needlefiber, this.Needlefiber_eff);

            // effect
            this.AddExecutor(ExecutorType.Activate, CardId.Spoofing, this.Spoofing_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Kunquery, this.Kunquery_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Multifaker, this.Multifaker_deckss);

            // summon
            this.AddExecutor(ExecutorType.SpSummon, CardId.Hexstia, this.Hexstia_ss);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Linkuriboh, this.Linkuriboh_ss);
            this.AddExecutor(ExecutorType.Activate, CardId.Linkuriboh, this.Linkuriboh_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Marionetter, this.Marionetter_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.OneForOne, this.OneForOne_activate);
            this.AddExecutor(ExecutorType.Summon, CardId.Meluseek, this.Meluseek_summon);
            this.AddExecutor(ExecutorType.Summon, CardId.Marionetter, this.Marionetter_summon);
            this.AddExecutor(ExecutorType.Summon, CardId.GR_WC, this.tuner_summon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Needlefiber, this.Needlefiber_ss);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Borrelsword, this.Borrelsword_ss);
            this.AddExecutor(ExecutorType.SpSummon, CardId.TripleBurstDragon, this.TripleBurstDragon_ss);

            // normal
            this.AddExecutor(ExecutorType.Activate, CardId.PotofDesires, this.PotofDesires_activate);
            this.AddExecutor(ExecutorType.Summon, CardId.Silquitous, this.Silquitous_summon);
            this.AddExecutor(ExecutorType.Summon, CardId.Multifaker, this.Multifaker_summon);
            this.AddExecutor(ExecutorType.Repos, this.MonsterRepos);
            this.AddExecutor(ExecutorType.Summon, this.MonsterSummon);
            this.AddExecutor(ExecutorType.MonsterSet, this.MonsterSet);
            this.AddExecutor(ExecutorType.SpellSet, this.SpellSet);
        }

        public bool EvenlyMatched_ready()
        {
            if (this.Bot.HasInHand(CardId.EvenlyMatched) && this.Bot.GetSpellCount() == 0)
            {
                if (this.Duel.Phase < DuelPhase.Main2 && this.Enemy.GetFieldCount() >= 3
                    && this.Bot.HasInMonstersZone(CardId.Iblee))
                {
                    return true;
                }
            }
            return false;
        }

        public bool has_altergeist_left()
        {
            return (this.Bot.GetRemainingCount(CardId.Marionetter, 3) > 0
                || this.Bot.GetRemainingCount(CardId.Multifaker, 2) > 0
                || this.Bot.GetRemainingCount(CardId.Meluseek,3) > 0
                || this.Bot.GetRemainingCount(CardId.Silquitous,2) > 0
                || this.Bot.GetRemainingCount(CardId.Kunquery,1) > 0);
        }

        public bool EvenlyMatched_Repos()
        {
            if (this.EvenlyMatched_ready())
            {
                return (!this.Card.HasPosition(CardPosition.Attack));
            }
            return false;
        }

        public bool isAltergeist(int id)
        {
            return (id == CardId.Marionetter || id == CardId.Hexstia || id == CardId.Protocol
                || id == CardId.Multifaker || id == CardId.Meluseek || id == CardId.Kunquery
                || id == CardId.Manifestation || id == CardId.Silquitous);
        }

        public bool isAltergeist(ClientCard card)
        {
            return card.IsCode(CardId.Marionetter, CardId.Hexstia, CardId.Protocol, CardId.Multifaker, CardId.Meluseek,
                CardId.Kunquery, CardId.Manifestation, CardId.Silquitous);
        }

        public int GetSequence(ClientCard card)
        {
            if (this.Card.Location != CardLocation.MonsterZone)
            {
                return -1;
            }

            for (int i = 0; i < 7; ++i)
            {
                if (this.Bot.MonsterZone[i] == card)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool trap_can_activate(int id)
        {
            if (id == CardId.WakingtheDragon || id == CardId.EvenlyMatched)
            {
                return false;
            }

            if (id == CardId.SolemnStrike && this.Bot.LifePoints <= 1500)
            {
                return false;
            }

            return true;
        }

        public bool Should_counter()
        {
            if (this.Duel.CurrentChain.Count < 2)
            {
                return true;
            }

            if (!this.Protocol_activing())
            {
                return true;
            }

            ClientCard self_card = this.Duel.CurrentChain[this.Duel.CurrentChain.Count - 2];
            if (self_card?.Controller != 0
                || !(self_card.Location == CardLocation.MonsterZone || self_card.Location == CardLocation.SpellZone)
                || !this.isAltergeist(self_card))
            {
                return true;
            }

            ClientCard enemy_card = this.Duel.CurrentChain[this.Duel.CurrentChain.Count - 1];
            if (enemy_card?.Controller != 1
                || !enemy_card.IsCode(this.normal_counter))
            {
                return true;
            }

            return false;
        }

        public bool Should_activate_Protocol()
        {
            if (this.Duel.CurrentChain.Count < 2)
            {
                return false;
            }

            if (this.Protocol_activing())
            {
                return false;
            }

            ClientCard self_card = this.Duel.CurrentChain[this.Duel.CurrentChain.Count - 2];
            if (self_card?.Controller != 0
                || !(self_card.Location == CardLocation.MonsterZone || self_card.Location == CardLocation.SpellZone)
                || !this.isAltergeist(self_card))
            {
                return false;
            }

            ClientCard enemy_card = this.Duel.CurrentChain[this.Duel.CurrentChain.Count - 1];
            if (enemy_card?.Controller != 1
                || !enemy_card.IsCode(this.normal_counter))
            {
                return false;
            }

            return true;
        }

        public bool is_should_not_negate()
        {
            ClientCard last_card = this.Util.GetLastChainCard();
            if (last_card != null
                && last_card.Controller == 1 && last_card.IsCode(this.should_not_negate))
            {
                return true;
            }

            return false;
        }
        
        public bool Multifaker_can_ss()
        {
            foreach (ClientCard sp in this.Bot.GetSpells())
            {
                if (sp.IsTrap() && sp.IsFacedown() && this.trap_can_activate(sp.Id))
                {
                    return true;
                }
            }
            foreach (ClientCard h in this.Bot.Hand)
            {
                if (h.IsTrap() && this.trap_can_activate(h.Id))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Multifaker_candeckss()
        {
            return (!this.Multifaker_ssfromdeck && !this.ss_other_monster);
        }

        public bool Protocol_activing()
        {
            foreach(ClientCard card in this.Bot.GetSpells())
            {
                if (card.IsCode(CardId.Protocol) && card.IsFaceup() && !card.IsDisabled() && !this.Duel.CurrentChain.Contains(card))
                {
                    return true;
                }
            }
            return false;
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

        public int SelectSTPlace(ClientCard card=null, bool avoid_Impermanence = false)
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
                    if (card != null && card.Location == CardLocation.Hand && avoid_Impermanence && this.Impermanence_list.Contains(seq))
                    {
                        continue;
                    }

                    return zone;
                };
            }
            return 0;
        }

        public int SelectSetPlace(List<int> avoid_list=null)
        {
            List<int> list = new List<int>();
            list.Add(0);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
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
                    if (avoid_list != null && avoid_list.Contains(seq))
                    {
                        continue;
                    }

                    return zone;
                };
            }
            return 0;
        }

        public bool spell_trap_activate(bool isCounter = false, ClientCard target = null)
        {
            if (target == null)
            {
                target = this.Card;
            }

            if (target.Location != CardLocation.SpellZone && target.Location != CardLocation.Hand)
            {
                return true;
            }

            if (this.Enemy.HasInMonstersZone(CardId.NaturalExterio, true) && !this.Bot.HasInHandOrHasInMonstersZone(CardId.GO_SR) && !isCounter && !this.Bot.HasInSpellZone(CardId.SolemnStrike))
            {
                return false;
            }

            if (target.IsSpell())
            {
                if (this.Enemy.HasInMonstersZone(CardId.NaturalBeast, true) && !this.Bot.HasInHandOrHasInMonstersZone(CardId.GO_SR) && !isCounter && !this.Bot.HasInSpellZone(CardId.SolemnStrike))
                {
                    return false;
                }

                if (this.Enemy.HasInSpellZone(CardId.ImperialOrder, true) || this.Bot.HasInSpellZone(CardId.ImperialOrder, true))
                {
                    return false;
                }

                if (this.Enemy.HasInMonstersZone(CardId.SwordsmanLV7, true) || this.Bot.HasInMonstersZone(CardId.SwordsmanLV7, true))
                {
                    return false;
                }

                return true;
            }
            if (target.IsTrap())
            {
                if (this.Enemy.HasInSpellZone(CardId.RoyalDecreel, true) || this.Bot.HasInSpellZone(CardId.RoyalDecreel, true))
                {
                    return false;
                }

                return true;
            }
            // how to get here?
            return false;
        }

        public void RandomSort(List<ClientCard> list)
        {

            int n = list.Count;
            while (n-- > 1)
            {
                int index = Program._rand.Next(n + 1);
                ClientCard temp = list[index];
                list[index] = list[n];
                list[n] = temp;
            }
        }

        public int get_Hexstia_linkzone(int zone)
        {
            if (zone >= 0 && zone < 4)
            {
                return zone + 1;
            }

            if (zone == 5)
            {
                return 1;
            }

            if (zone == 6)
            {
                return 3;
            }

            return -1;
        }

        public bool get_linked_by_Hexstia(int place)
        {
            if (place == 0)
            {
                return false;
            }

            if (place == 2 || place == 4)
            {
                int last_place = place - 1;
                return (this.Bot.MonsterZone[last_place] != null && this.Bot.MonsterZone[last_place].IsCode(CardId.Hexstia));
            }
            if (place == 1 || place == 3)
            {
                int last_place_1, last_place_2;
                if (place == 1)
                {
                    last_place_1 = 0;
                    last_place_2 = 5;
                } else
                {
                    last_place_1 = 2;
                    last_place_2 = 6;
                }
                if (this.Bot.MonsterZone[last_place_1] != null && this.Bot.MonsterZone[last_place_1].IsCode(CardId.Hexstia))
                {
                    return true;
                }

                if (this.Bot.MonsterZone[last_place_2] != null && this.Bot.MonsterZone[last_place_2].IsCode(CardId.Hexstia))
                {
                    return true;
                }

                return false;
            }
            return false;
        }

        public ClientCard GetFloodgate_Alter(bool canBeTarget = false, bool is_bounce = true)
        {
            foreach (ClientCard card in this.Enemy.GetSpells())
            {
                if (card != null && card.IsFloodgate() && card.IsFaceup() &&
                    !card.IsCode(CardId.Anti_Spell, CardId.ImperialOrder)
                    && (!is_bounce || card.IsTrap())
                    && (!canBeTarget || !card.IsShouldNotBeTarget()))
                {
                    return card;
                }
            }
            return null;
        }

        public ClientCard GetProblematicEnemyCard_Alter(bool canBeTarget = false, bool is_bounce = true)
        {
            ClientCard card = this.Enemy.MonsterZone.GetFloodgate(canBeTarget);
            if (card != null)
            {
                return card;
            }

            card = this.GetFloodgate_Alter(canBeTarget, is_bounce);
            if (card != null)
            {
                return card;
            }

            card = this.Enemy.MonsterZone.GetDangerousMonster(canBeTarget);
            if (card != null
                && (this.Duel.Player == 0 || (this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2)))
            {
                return card;
            }

            card = this.Enemy.MonsterZone.GetInvincibleMonster(canBeTarget);
            if (card != null)
            {
                return card;
            }

            List<ClientCard> enemy_monsters = this.Enemy.GetMonsters();
            enemy_monsters.Sort(CardContainer.CompareCardAttack);
            enemy_monsters.Reverse();
            foreach(ClientCard target in enemy_monsters)
            {
                if (target.HasType(CardType.Fusion) || target.HasType(CardType.Ritual) || target.HasType(CardType.Synchro) || target.HasType(CardType.Xyz) || (target.HasType(CardType.Link) && target.LinkCount >= 2) )
                {
                    if (target.IsCode(CardId.Kagari, CardId.Shizuku))
                    {
                        continue;
                    }

                    if (!canBeTarget || !(target.IsShouldNotBeTarget() || target.IsShouldNotBeMonsterTarget()))
                    {
                        return target;
                    }
                }
            }

            return null;
        }

        public ClientCard GetBestEnemyCard_random()
        {
            // monsters
            ClientCard card = this.Util.GetProblematicEnemyMonster(0, true);
            if (card != null)
            {
                return card;
            }

            if (this.Util.GetOneEnemyBetterThanMyBest() != null)
            {
                card = this.Enemy.MonsterZone.GetHighestAttackMonster(true);
                if (card != null)
                {
                    return card;
                }
            }

            // spells
            List<ClientCard> enemy_spells = this.Enemy.GetSpells();
            this.RandomSort(enemy_spells);
            foreach(ClientCard sp in enemy_spells)
            {
                if (sp.IsFaceup() && !sp.IsDisabled())
                {
                    return sp;
                }
            }
            if (enemy_spells.Count > 0)
            {
                return enemy_spells[0];
            }

            List<ClientCard> monsters = this.Enemy.GetMonsters();
            if (monsters.Count > 0)
            {
                this.RandomSort(monsters);
                return monsters[0];
            }

            return null;
        }

        public bool bot_can_s_Meluseek()
        {
            if (this.Duel.Player != 0)
            {
                return false;
            }

            foreach (ClientCard card in this.Bot.GetMonsters())
            {
                if (card.IsCode(CardId.Meluseek) && !card.IsDisabled() && !card.Attacked)
                {
                    return true;
                }
            }
            if (this.Bot.HasInMonstersZone(CardId.Meluseek))
            {
                return true;
            }

            if (this.Bot.HasInMonstersZone(CardId.Marionetter) && !this.Marionetter_reborn && this.Bot.HasInGraveyard(CardId.Meluseek))
            {
                return true;
            }

            if (!this.summoned
                && (this.Bot.HasInHand(CardId.Meluseek)
                || (this.Bot.HasInHand(CardId.Marionetter) && this.Bot.HasInGraveyard(CardId.Meluseek)))
                )
            {
                return true;
            }

            return false;
        }

        public bool SpellSet()
        {
            if (this.Duel.Phase == DuelPhase.Main1 && this.Bot.HasAttackingMonster() && this.Duel.Turn > 1)
            {
                return false;
            }

            if (this.Card.IsCode(CardId.EvenlyMatched) && !this.Bot.HasInHandOrInSpellZone(CardId.Spoofing)
                && !this.Bot.HasInHandOrInSpellZone(CardId.Protocol) && !this.Bot.HasInHandOrInSpellZone(CardId.ImperialOrder))
            {
                return false;
            }

            if (this.Card.IsCode(CardId.EvenlyMatched) && this.Bot.HasInSpellZone(CardId.EvenlyMatched))
            {
                return false;
            }

            if (this.Card.IsCode(CardId.SolemnStrike) && this.Bot.LifePoints <= 1500)
            {
                return false;
            }

            if (this.Card.IsCode(CardId.Spoofing) && this.Bot.HasInSpellZone(CardId.Spoofing))
            {
                return false;
            }

            if (this.Card.IsCode(CardId.Manifestation) && this.Bot.HasInHandOrInSpellZone(CardId.Spoofing))
            {
                bool can_activate = false;
                foreach(ClientCard g in this.Bot.GetGraveyardMonsters())
                {
                    if (g.IsMonster() && this.isAltergeist(g))
                    {
                        can_activate = true;
                        break;
                    }
                }
                Logger.DebugWriteLine("Manifestation_set: " + can_activate.ToString());
                if (!can_activate)
                {
                    return false;
                }
            }
            if ((this.Card.IsTrap() || this.Card.HasType(CardType.QuickPlay)))
            {
                List<int> avoid_list = new List<int>();
                int Impermanence_set = 0;
                for (int i = 0; i < 5; ++i)
                {
                    if (this.Enemy.SpellZone[i] != null && this.Enemy.SpellZone[i].IsFaceup() && this.Bot.SpellZone[4 - i] == null)
                    {
                        avoid_list.Add(4 - i);
                        Impermanence_set += (int)System.Math.Pow(2, 4 - i);
                    }
                }
                if (this.Bot.HasInHand(CardId.Impermanence))
                {
                    if (this.Card.IsCode(CardId.Impermanence))
                    {
                        this.AI.SelectPlace(Impermanence_set);
                        return true;
                    } else
                    {
                        this.AI.SelectPlace(this.SelectSetPlace(avoid_list));
                        return true;
                    }
                } else
                {
                    this.AI.SelectPlace(this.SelectSTPlace());
                }
                return true;
            }
            else if (this.Enemy.HasInSpellZone(CardId.Anti_Spell, true) || this.Bot.HasInSpellZone(CardId.Anti_Spell, true))
            {
                if (this.Card.IsSpell() && (!this.Card.IsCode(CardId.OneForOne) || this.Bot.GetRemainingCount(CardId.Meluseek,3) > 0))
                {
                    this.AI.SelectPlace(this.SelectSTPlace());
                    return true;
                }
            }
            return false;
        }

        public bool field_activate()
        {
            if (this.Card.HasPosition(CardPosition.FaceDown) && this.Card.HasType(CardType.Field) && this.Card.Location == CardLocation.SpellZone)
            {
                // field spells that forbid other fields' activate
                return !this.Card.IsCode(71650854, 78082039);
            }
            return false;
        }

        public bool ChickenGame()
        {
            Logger.DebugWriteLine("Use override");
            if (!this.spell_trap_activate())
            {
                return false;
            }

            if (this.Bot.LifePoints <= 1000)
            {
                return false;
            }

            if (this.Bot.LifePoints - 1000 <= this.Enemy.LifePoints && this.ActivateDescription == this.Util.GetStringId(DefaultExecutor.CardId.ChickenGame, 0))
            {
                return true;
            }
            if (this.Bot.LifePoints - 1000 > this.Enemy.LifePoints && this.ActivateDescription == this.Util.GetStringId(DefaultExecutor.CardId.ChickenGame, 1))
            {
                return true;
            }
            return false;
        }

        public bool Anti_Spell_activate()
        {
            foreach(ClientCard card in this.Bot.GetSpells())
            {
                if (card.IsCode(CardId.Anti_Spell) && card.IsFaceup() && this.Duel.LastChainPlayer == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool SecretVillage_activate()
        {
            if (!this.spell_trap_activate())
            {
                return false;
            }

            if (this.Bot.SpellZone[5] != null && this.Bot.SpellZone[5].IsFaceup() && this.Bot.SpellZone[5].IsCode(CardId.SecretVillage) && this.Bot.SpellZone[5].Disabled==0)
            {
                return false;
            }

            if (this.Multifaker_can_ss() && this.Bot.HasInHand(CardId.Multifaker))
            {
                return true;
            }

            foreach (ClientCard card in this.Bot.GetMonsters())
            {
                if (card != null && card.IsFaceup() && (card.Race & (int)CardRace.SpellCaster) != 0 && !card.IsCode(CardId.Meluseek))
                {
                    return true;
                }
            }
            return false;   
        }

        public bool G_activate()
        {
            return (this.Duel.Player == 1);
        }

        public bool NaturalExterio_eff()
        {
            if (this.Duel.LastChainPlayer != 0)
            {
                this.AI.SelectCard(
                    DefaultExecutor.CardId.HarpiesFeatherDuster,
                    CardId.PotofDesires,
                    CardId.OneForOne,
                    CardId.GO_SR,
                    CardId.AB_JS,
                    CardId.GR_WC,
                    CardId.MaxxC,
                    CardId.Spoofing,
                    CardId.SolemnJudgment,
                    CardId.SolemnStrike,
                    CardId.ImperialOrder,
                    CardId.Spoofing,
                    CardId.Storm,
                    CardId.EvenlyMatched,
                    CardId.WakingtheDragon,
                    CardId.Impermanence,
                    CardId.Marionetter
                    );
                return true;
            }
            return false;
        }

        public bool SolemnStrike_activate()
        {
            if (!this.Should_counter())
            {
                return false;
            }

            return (this.DefaultSolemnStrike() && this.spell_trap_activate(true));
        }

        public bool SolemnJudgment_activate()
        {
            if (this.Util.IsChainTargetOnly(this.Card) && (this.Bot.HasInHand(CardId.Multifaker) || this.Multifaker_candeckss()))
            {
                return false;
            }

            if (!this.Should_counter())
            {
                return false;
            }

            if ((this.DefaultSolemnJudgment() && this.spell_trap_activate(true)))
            {
                ClientCard target = this.Util.GetLastChainCard();
                if (target != null && !target.IsMonster() && !this.spell_trap_activate(false, target))
                {
                    return false;
                }

                return true;
            }
            return false;
        }

        public bool Impermanence_activate()
        {
            if (!this.Should_counter())
            {
                return false;
            }

            if (!this.spell_trap_activate())
            {
                return false;
            }
            // negate before effect used
            foreach (ClientCard m in this.Enemy.GetMonsters())
            {
                if (m.IsMonsterShouldBeDisabledBeforeItUseEffect() && !m.IsDisabled() && this.Duel.LastChainPlayer != 0)
                {
                    if (this.Card.Location == CardLocation.SpellZone)
                    {
                        for (int i = 0; i < 5; ++ i)
                        {
                            if (this.Bot.SpellZone[i] == this.Card)
                            {
                                this.Impermanence_list.Add(i);
                                break;
                            }
                        }
                    }
                    if (this.Card.Location == CardLocation.Hand)
                    {
                        this.AI.SelectPlace(this.SelectSTPlace(this.Card, true));
                    }
                    this.AI.SelectCard(m);
                    return true;
                }
            }

            ClientCard LastChainCard = this.Util.GetLastChainCard();

            if (LastChainCard == null
                && !(this.Duel.Player == 1 && this.Duel.Phase > DuelPhase.Main2 && this.Bot.HasInHand(CardId.Multifaker) && this.Multifaker_candeckss() && !this.Multifaker_ssfromhand))
            {
                return false;
            }
            // negate spells
            if (this.Card.Location == CardLocation.SpellZone)
            {
                int this_seq = -1;
                int that_seq = -1;
                for (int i = 0; i < 5; ++i)
                {
                    if (this.Bot.SpellZone[i] == this.Card)
                    {
                        this_seq = i;
                    }

                    if (LastChainCard != null
                        && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.SpellZone && this.Enemy.SpellZone[i] == LastChainCard)
                    {
                        that_seq = i;
                    }
                    else if (this.Duel.Player == 0 && this.Util.GetProblematicEnemySpell() != null
                        && this.Enemy.SpellZone[i] != null && this.Enemy.SpellZone[i].IsFloodgate())
                    {
                        that_seq = i;
                    }
                }
                if ( (this_seq * that_seq >= 0 && this_seq + that_seq == 4)
                    || (this.Util.IsChainTarget(this.Card))
                    || (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.IsCode(DefaultExecutor.CardId.HarpiesFeatherDuster))
                    || (this.Duel.Player == 1 && this.Duel.Phase > DuelPhase.Main2 && this.Bot.HasInHand(CardId.Multifaker) && this.Multifaker_candeckss() && !this.Multifaker_ssfromhand))
                {
                    List<ClientCard> enemy_monsters = this.Enemy.GetMonsters();
                    enemy_monsters.Sort(CardContainer.CompareCardAttack);
                    enemy_monsters.Reverse();
                    foreach(ClientCard card in enemy_monsters)
                    {
                        if (card.IsFaceup() && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeSpellTrapTarget())
                        {
                            this.AI.SelectCard(card);
                            this.Impermanence_list.Add(this_seq);
                            return true;
                        }
                    }
                }
            }
            if ( (LastChainCard == null || LastChainCard.Controller != 1 || LastChainCard.Location != CardLocation.MonsterZone
                || LastChainCard.IsDisabled() || LastChainCard.IsShouldNotBeTarget() || LastChainCard.IsShouldNotBeSpellTrapTarget())
                && !(this.Duel.Player == 1 && this.Duel.Phase > DuelPhase.Main2 && this.Bot.HasInHand(CardId.Multifaker) && this.Multifaker_candeckss() && !this.Multifaker_ssfromhand) )
            {
                return false;
            }
            // negate monsters
            if (this.is_should_not_negate() && LastChainCard.Location == CardLocation.MonsterZone)
            {
                return false;
            }

            if (this.Card.Location == CardLocation.SpellZone)
            {
                for (int i = 0; i < 5; ++i)
                {
                    if (this.Bot.SpellZone[i] == this.Card)
                    {
                        this.Impermanence_list.Add(i);
                        break;
                    }
                }
            }
            if (this.Card.Location == CardLocation.Hand)
            {
                this.AI.SelectPlace(this.SelectSTPlace(this.Card, true));
            }
            if (LastChainCard != null)
            {
                this.AI.SelectCard(LastChainCard);
            }
            else
            {
                List<ClientCard> enemy_monsters = this.Enemy.GetMonsters();
                enemy_monsters.Sort(CardContainer.CompareCardAttack);
                enemy_monsters.Reverse();
                foreach (ClientCard card in enemy_monsters)
                {
                    if (card.IsFaceup() && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeSpellTrapTarget())
                    {
                        this.AI.SelectCard(card);
                        return true;
                    }
                }
            }
            return true;
        }

        public bool Hand_act_eff()
        {
            if (this.Card.IsCode(CardId.AB_JS) && this.Util.GetLastChainCard().HasSetcode(0x11e) && this.Util.GetLastChainCard().Location == CardLocation.Hand) // Danger! archtype hand effect
            {
                return false;
            }

            if (this.Card.IsCode(CardId.GO_SR) && this.Card.Location == CardLocation.Hand && this.Bot.HasInMonstersZone(CardId.GO_SR))
            {
                return false;
            }

            return (this.Duel.LastChainPlayer == 1);
        }

        public bool WakingtheDragon_eff()
        {
            if (this.Bot.HasInExtra(CardId.NaturalExterio) && !this.Multifaker_ssfromdeck)
            {
                bool has_skystriker = false;
                foreach(ClientCard card in this.Enemy.Graveyard)
                {
                    if (card != null && card.IsCode(this.SkyStrike_list))
                    {
                        has_skystriker = true;
                        break;
                    }
                }
                if (!has_skystriker)
                {
                    foreach (ClientCard card in this.Enemy.GetSpells())
                    {
                        if (card != null && card.IsCode(this.SkyStrike_list))
                        {
                            has_skystriker = true;
                            break;
                        }
                    }
                }
                if (!has_skystriker)
                {
                    foreach (ClientCard card in this.Enemy.GetSpells())
                    {
                        if (card != null && card.IsCode(this.SkyStrike_list))
                        {
                            has_skystriker = true;
                            break;
                        }
                    }
                }
                if (has_skystriker)
                {
                    this.AI.SelectCard(CardId.NaturalExterio);
                    this.ss_other_monster = true;
                    return true;
                }
            }
            IList<int> ex_list = new[] {
                CardId.UltimateFalcon,
                CardId.Borrelsword,
                CardId.NaturalExterio,
                CardId.FWD,
                CardId.TripleBurstDragon,
                CardId.HeavymetalfoesElectrumite,
                CardId.Isolde,
                CardId.Hexstia,
                CardId.Needlefiber,
                CardId.Multifaker,
                CardId.Kunquery
            };
            foreach(int id in ex_list)
            {
                if (this.Bot.HasInExtra(id))
                {
                    if (!this.isAltergeist(id))
                    {
                        if (this.Multifaker_ssfromdeck)
                        {
                            continue;
                        }

                        this.ss_other_monster = true;
                    }
                    Logger.DebugWriteLine(id.ToString());
                    this.AI.SelectCard(id);
                    return true;
                }
            }
            return true;
        }

        public bool GR_WC_activate()
        {
            int warrior_count = 0;
            int pendulum_count = 0;
            int link_count = 0;
            int altergeis_count = 0;
            bool has_skystriker_acer = false;
            bool has_tuner = false;
            bool has_level_1 = false;
            foreach (ClientCard card in this.Enemy.MonsterZone)
            {
                if (card == null)
                {
                    continue;
                }

                if (card.IsCode(CardId.Kagari, CardId.Shizuku, CardId.Hayate, CardId.Raye, CardId.Drones_Token))
                {
                    has_skystriker_acer = true;
                }

                if (card.HasType(CardType.Pendulum))
                {
                    pendulum_count ++;
                }

                if ((card.Race & (int)CardRace.Warrior) != 0)
                {
                    warrior_count ++;
                }

                if (card.IsTuner() && (this.Enemy.GetMonsterCount() >= 2))
                {
                    has_tuner = true;
                }

                if (this.isAltergeist(card))
                {
                    altergeis_count++;
                }

                if (!card.HasType(CardType.Link) && !card.HasType(CardType.Xyz) && card.Level == 1)
                {
                    has_level_1 = true;
                }

                link_count += (card.HasType(CardType.Link) ? card.LinkCount : 1);
            }
            if (has_skystriker_acer)
            {
                if (!this.Enemy.HasInBanished(CardId.Kagari) && this.Bot.HasInExtra(CardId.Kagari))
                {
                    this.AI.SelectCard(CardId.Kagari);
                    return true;
                } else if (!this.Enemy.HasInBanished(CardId.Shizuku) && this.Bot.HasInExtra(CardId.Shizuku))
                {
                    this.AI.SelectCard(CardId.Shizuku);
                    return true;
                }
            }
            if (pendulum_count >= 2 && !(this.Enemy.HasInMonstersZoneOrInGraveyard(CardId.HeavymetalfoesElectrumite) || this.Enemy.HasInBanished(CardId.HeavymetalfoesElectrumite)) && this.Bot.HasInExtra(CardId.HeavymetalfoesElectrumite))
            {
                this.AI.SelectCard(CardId.HeavymetalfoesElectrumite);
                return true;
            }
            if (warrior_count >= 2 && !(this.Enemy.HasInMonstersZoneOrInGraveyard(CardId.Isolde) || this.Enemy.HasInBanished(CardId.Isolde)) && this.Bot.HasInExtra(CardId.Isolde))
            {
                this.AI.SelectCard(CardId.Isolde);
                return true;
            }
            if (has_tuner && !this.Enemy.HasInBanished(CardId.Needlefiber) && this.Bot.HasInExtra(CardId.Needlefiber) && !this.Enemy.HasInMonstersZone(CardId.Needlefiber))
            {
                this.AI.SelectCard(CardId.Needlefiber);
                return true;
            }
            if (has_level_1 && !this.Enemy.HasInHandOrInMonstersZoneOrInGraveyard(CardId.Linkuriboh) && !this.Enemy.HasInBanished(CardId.Linkuriboh) && this.Bot.HasInExtra(CardId.Linkuriboh))
            {
                this.AI.SelectCard(CardId.Linkuriboh);
                return true;
            }
            if (altergeis_count > 0 && !this.Enemy.HasInBanished(CardId.Hexstia) && this.Bot.HasInExtra(CardId.Hexstia))
            {
                this.AI.SelectCard(CardId.Hexstia);
                return true;
            }
            if (link_count >= 4)
            {
                if ((this.Bot.HasInMonstersZone(CardId.UltimateFalcon) || this.Bot.HasInMonstersZone(CardId.NaturalExterio)) && !(this.Enemy.HasInMonstersZoneOrInGraveyard(CardId.Borrelsword) || this.Enemy.HasInBanished(CardId.Borrelsword)) && this.Bot.HasInExtra(CardId.Borrelsword))
                {
                    this.AI.SelectCard(CardId.Borrelsword);
                    return true;
                }
                if (!(this.Enemy.HasInMonstersZoneOrInGraveyard(CardId.FWD) || this.Enemy.HasInBanished(CardId.FWD)) && this.Bot.HasInExtra(CardId.FWD))
                {
                    this.AI.SelectCard(CardId.FWD);
                    return true;
                }
            }

            return false;
        }

        public bool ImperialOrder_activate()
        {
            if (!this.Card.HasPosition(CardPosition.FaceDown))
            {
                return true;
            }

            foreach (ClientCard card in this.Enemy.GetSpells())
            {
                if (card.IsSpell() && this.spell_trap_activate())
                {
                    return true;
                }
            }
            if (this.Duel.Player == 1 && this.Duel.Phase > DuelPhase.Main2 && this.Bot.HasInHand(CardId.Multifaker) && (!this.Multifaker_ssfromhand && this.Multifaker_candeckss()))
            {
                return true;
            }

            return false;
        }

        public bool EvenlyMatched_activate()
        {
            if (!this.spell_trap_activate())
            {
                return false;
            }

            return true;

            // use after ToBattle fix
            int bot_count = this.Bot.GetFieldCount();
            if (this.Card.Location == CardLocation.Hand)
            {
                bot_count += 1;
            }

            int enemy_count = this.Enemy.GetFieldCount();
            if (enemy_count - bot_count < 2)
            {
                return false;
            }

            if (this.Card.Location == CardLocation.Hand)
            {
                this.AI.SelectPlace(this.SelectSTPlace(this.Card, true));
            }

            return true;
        }

        public bool Feather_activate()
        {
            if (!this.spell_trap_activate())
            {
                return false;
            }

            if (this.Util.GetProblematicEnemySpell() != null)
            {
                this.AI.SelectPlace(this.SelectSTPlace(this.Card, true));
                return true;
            }
            // activate when more than 2 cards
            if (this.Enemy.GetSpellCount() <= 1)
            {
                return false;
            }

            this.AI.SelectPlace(this.SelectSTPlace(this.Card, true));
            return true;
        }

        public bool Storm_activate()
        {
            if (!this.spell_trap_activate())
            {
                return false;
            }

            List<ClientCard> select_list = new List<ClientCard>();
            int activate_immediately = 0;
            List<ClientCard> spells = this.Enemy.GetSpells();
            this.RandomSort(spells);
            foreach(ClientCard card in spells)
            {
                if (card != null)
                {
                    if (card.IsFaceup())
                    {
                        if (card.HasType(CardType.Equip) || card.HasType(CardType.Pendulum) || card.HasType(CardType.Field) || card.HasType(CardType.Continuous))
                        {
                            select_list.Add(card);
                            activate_immediately += 1;
                        }
                    }
                }
            }
            foreach (ClientCard card in spells)
            {
                if (card != null && card.IsFacedown())
                {
                    select_list.Add(card);
                }
            }
            foreach (ClientCard card in spells)
            {
                if (card != null && card.IsFaceup() && !select_list.Contains(card))
                {
                    select_list.Add(card);
                }
            }
            if (this.Duel.Phase == DuelPhase.End 
                || activate_immediately >= 2 
                || (this.Util.IsChainTarget(this.Card) 
                    || (this.Util.GetLastChainCard() != null && this.Util.GetLastChainCard().Controller == 1 && this.Util.GetLastChainCard().IsCode(DefaultExecutor.CardId.HarpiesFeatherDuster))))
            {
                if (select_list.Count > 0)
                {
                    this.AI.SelectCard(select_list);
                    return true;
                }
            }
            return false;
        }

        public bool Kunquery_eff()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                if (this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2)
                {
                    if (this.Util.ChainContainsCard(CardId.Linkuriboh))
                    {
                        return false;
                    }

                    if (this.Bot.BattlingMonster == null || (this.Enemy.BattlingMonster.Attack >= this.Bot.BattlingMonster.GetDefensePower()) || this.Enemy.BattlingMonster.IsMonsterDangerous())
                    {
                        this.AI.SelectPosition(CardPosition.FaceUpDefence);
                        return true;
                    }
                }
                return false;
            } else
            {
                ClientCard target = this.GetProblematicEnemyCard_Alter(true, false);
                if (target != null)
                {
                    this.AI.SelectCard(target);
                    return true;
                }
                List<ClientCard> spells = this.Enemy.GetSpells();
                this.RandomSort(spells);
                foreach(ClientCard card in spells)
                {
                    if (card.IsFaceup() && !card.IsDisabled())
                    {
                        this.AI.SelectCard(card);
                        return true;
                    }
                }
                List<ClientCard> monsters = this.Enemy.GetMonsters();
                this.RandomSort(monsters);
                foreach (ClientCard card in monsters)
                {
                    if (card.IsFaceup() && !card.IsDisabled() 
                        && !(card.IsShouldNotBeMonsterTarget() || card.IsShouldNotBeTarget()))
                    {
                        this.AI.SelectCard(card);
                        return true;
                    }
                }
                return false;
            }
        }

        public bool Marionetter_eff()
        {
            if (this.ActivateDescription == -1)
            {
                if (!this.Bot.HasInHandOrInSpellZone(CardId.Protocol) && this.Bot.GetRemainingCount(CardId.Protocol,2) > 0)
                {
                    this.AI.SelectCard(CardId.Protocol, CardId.Manifestation);
                    this.AI.SelectPlace(this.SelectSetPlace());
                    return true;
                } else
                {
                    this.AI.SelectCard(CardId.Manifestation, CardId.Protocol);
                    this.AI.SelectPlace(this.SelectSetPlace());
                    return true;
                }
            }
            else
            {
                if (this.Card.IsDisabled() && !this.Protocol_activing())
                {
                    return false;
                }

                int next_card = 0;
                bool choose_other = false;
                bool can_choose_other = false;
                foreach(ClientCard card in this.Bot.GetSpells())
                {
                    if (card.IsFaceup() && this.isAltergeist(card))
                    {
                        can_choose_other = true;
                        break;
                    }
                }
                if (!can_choose_other){
                    foreach(ClientCard card in this.Bot.GetMonsters())
                    {
                        if (card.IsFaceup() && card != this.Card && this.isAltergeist(card))
                        {
                            can_choose_other = true;
                        }
                    }
                }
                if (!this.Util.IsTurn1OrMain2())
                {
                    ClientCard self_best = this.Util.GetBestBotMonster();
                    ClientCard enemy_best = this.Util.GetProblematicEnemyCard(self_best.Attack, true);
                    ClientCard enemy_target = this.GetProblematicEnemyCard_Alter(true,false);

                    if ((enemy_best != null || enemy_target != null)
                        && this.Bot.HasInGraveyard(CardId.Meluseek))
                    {
                        next_card = CardId.Meluseek;
                    }
                    else if (this.Enemy.GetMonsterCount() <= 1 && this.Bot.HasInGraveyard(CardId.Meluseek) && this.Enemy.GetFieldCount() > 0)
                    {
                        next_card = CardId.Meluseek;
                    }
                    else if (this.Bot.HasInGraveyard(CardId.Hexstia) && this.Util.GetProblematicEnemySpell() == null && this.Util.GetOneEnemyBetterThanValue(3100, true) == null && can_choose_other)
                    {
                        next_card = CardId.Hexstia;
                        choose_other = (this.Util.GetOneEnemyBetterThanMyBest(true) != null);
                    }
                }
                else
                {
                    if (!this.Meluseek_searched && !this.Bot.HasInMonstersZone(CardId.Meluseek) && this.Bot.HasInGraveyard(CardId.Meluseek))
                    {
                        if (this.Multifaker_candeckss() && this.Bot.HasInGraveyard(CardId.Multifaker) && this.Bot.GetRemainingCount(CardId.Meluseek,3) > 0)
                        {
                            next_card = CardId.Multifaker;
                        } else
                        {
                            next_card = CardId.Meluseek;
                        }
                    }
                    else if (this.Multifaker_candeckss() && this.Bot.HasInGraveyard(CardId.Multifaker) && this.has_altergeist_left())
                    {
                        next_card = CardId.Multifaker;
                    }
                    else if (this.Bot.HasInGraveyard(CardId.Hexstia))
                    {
                        next_card = CardId.Hexstia;
                        choose_other = !(this.Bot.GetMonsterCount() > 1 || this.Bot.HasInHand(CardId.Multifaker));
                    }
                    else if (this.Bot.HasInGraveyard(CardId.Silquitous))
                    {
                        int alter_count = 0;
                        foreach (ClientCard card in this.Bot.Hand)
                        {
                            if (this.isAltergeist(card) && (card.IsTrap() || (!this.summoned && card.IsMonster())))
                            {
                                alter_count ++;
                            }
                        }
                        foreach (ClientCard s in this.Bot.GetSpells())
                        {
                            if (this.isAltergeist(s))
                            {
                                alter_count++;
                            }
                        }
                        foreach(ClientCard m in this.Bot.GetMonsters())
                        {
                            if (this.isAltergeist(m) && m != this.Card)
                            {
                                alter_count++;
                            }
                        }
                        if (alter_count > 0)
                        {
                            next_card = CardId.Silquitous;
                        }
                    }
                }
                if (next_card != 0)
                {
                    int Protocol_count = 0;
                    foreach (ClientCard h in this.Bot.Hand)
                    {
                        if (h.IsCode(CardId.Protocol))
                        {
                            Protocol_count++;
                        }
                    }
                    foreach (ClientCard s in this.Bot.GetSpells())
                    {
                        if (s.IsCode(CardId.Protocol))
                        {
                            Protocol_count += (s.IsFaceup() ? 11 : 1);
                        }
                    }
                    if (Protocol_count >= 12)
                    {
                        this.AI.SelectCard(CardId.Protocol);
                        this.AI.SelectNextCard(next_card);
                        this.Marionetter_reborn = true;
                        if (next_card == CardId.Meluseek && this.Util.IsTurn1OrMain2())
                        {
                            this.AI.SelectPosition(CardPosition.FaceUpDefence);
                        }

                        return true;
                    }
                    List<ClientCard> list = this.Bot.GetMonsters();
                    list.Sort(CardContainer.CompareCardAttack);
                    foreach (ClientCard card in list)
                    {
                        if (this.isAltergeist(card) && !(choose_other && card == this.Card))
                        {
                            this.AI.SelectCard(card);
                            this.AI.SelectNextCard(next_card);
                            if (next_card == CardId.Meluseek && this.Util.IsTurn1OrMain2())
                            {
                                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                            }

                            this.Marionetter_reborn = true;
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                };
            }
            return false;
        }

        public bool Hexstia_eff()
        {
            if (this.Card.Location == CardLocation.MonsterZone && this.Duel.LastChainPlayer != 0 && (this.Protocol_activing() || !this.Card.IsDisabled()))
            {
                ClientCard target =  this.Util.GetLastChainCard();
                if (target != null && !this.spell_trap_activate(false, target))
                {
                    return false;
                }

                if (!this.Should_counter())
                {
                    return false;
                }
                // check
                int this_seq = this.GetSequence(this.Card);
                if (this_seq != -1)
                {
                    this_seq = this.get_Hexstia_linkzone(this_seq);
                }

                if (this_seq != -1)
                {
                    ClientCard linked_card = this.Bot.MonsterZone[this_seq];
                    if (linked_card != null && linked_card.IsCode(CardId.Hexstia))
                    {
                        int next_seq = this.get_Hexstia_linkzone(this_seq);
                        if (next_seq != -1 && this.Bot.MonsterZone[next_seq] != null && this.isAltergeist(this.Bot.MonsterZone[next_seq].Id))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            if (this.ActivateDescription == this.Util.GetStringId(CardId.Hexstia,0))
            {
                return false;
            }

            if (this.Enemy.HasInSpellZone(82732705) && this.Bot.GetRemainingCount(CardId.Protocol,3) > 0 && !this.Bot.HasInHandOrInSpellZone(CardId.Protocol))
            {
                this.AI.SelectCard(CardId.Protocol);
                return true;
            }
            if (this.Duel.Player == 0 && !this.summoned && this.Bot.GetRemainingCount(CardId.Marionetter, 3) > 0)
            {
                this.AI.SelectCard(CardId.Marionetter);
                return true;
            }
            if (!this.Bot.HasInHandOrHasInMonstersZone(CardId.Multifaker) && this.Bot.GetRemainingCount(CardId.Multifaker, 2) > 0 && this.Multifaker_can_ss())
            {
                this.AI.SelectCard(CardId.Multifaker);
                return true;
            }
            if (!this.Bot.HasInHand(CardId.Marionetter) && this.Bot.GetRemainingCount(CardId.Marionetter,3) > 0)
            {
                this.AI.SelectCard(CardId.Marionetter);
                return true;
            }
            if (!this.Bot.HasInHandOrInSpellZone(CardId.Manifestation) && this.Bot.GetRemainingCount(CardId.Manifestation,2) > 0)
            {
                this.AI.SelectCard(CardId.Manifestation);
                return true;
            }
            if (!this.Bot.HasInHandOrInSpellZone(CardId.Protocol) && this.Bot.GetRemainingCount(CardId.Protocol, 2) > 0)
            {
                this.AI.SelectCard(CardId.Protocol);
                return true;
            }
            this.AI.SelectCard(
                CardId.Meluseek,
                CardId.Kunquery,
                CardId.Marionetter,
                CardId.Multifaker,
                CardId.Manifestation,
                CardId.Protocol,
                CardId.Silquitous
                );
            return true;
        }

        public bool Meluseek_eff()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.Meluseek,0)
                || (this.ActivateDescription == -1 && this.Card.Location == CardLocation.MonsterZone))
            {
                this.attacked_Meluseek.Add(this.Card);
                ClientCard target = this.GetProblematicEnemyCard_Alter(true);
                if (target != null)
                {
                    this.AI.SelectCard(target);
                    return true;
                }
                target = this.Util.GetOneEnemyBetterThanMyBest(true, true);
                if (target != null)
                {
                    this.AI.SelectCard(target);
                    return true;
                }
                List<ClientCard> targets = this.Enemy.GetSpells();
                this.RandomSort(targets);
                if (targets.Count > 0)
                {
                    this.AI.SelectCard(targets[0]);
                    return true;
                }
                target = this.GetBestEnemyCard_random();
                if (target != null)
                {
                    this.AI.SelectCard(target);
                    return true;
                }
            } else
            {
                if (this.Duel.Player == 1)
                {
                    if (!this.Bot.HasInHandOrHasInMonstersZone(CardId.Multifaker) && this.Bot.GetRemainingCount(CardId.Multifaker, 2) > 0 && this.Multifaker_candeckss() && this.Multifaker_can_ss())
                    {
                        foreach(ClientCard set_card in this.Bot.GetSpells())
                        {
                            if (set_card.IsFacedown() && !set_card.IsCode(CardId.WakingtheDragon))
                            {
                                this.AI.SelectCard(CardId.Multifaker);
                                return true;
                            }
                        }
                    }
                    if (this.Bot.GetRemainingCount(CardId.Marionetter, 3) > 0)
                    {
                        this.AI.SelectCard(CardId.Marionetter);
                        return true;
                    }
                }
                else
                {
                    if (!this.summoned && !this.Bot.HasInHand(CardId.Marionetter) && this.Bot.GetRemainingCount(CardId.Marionetter, 3) > 0)
                    {
                        this.AI.SelectCard(CardId.Marionetter);
                        return true;
                    }
                    if (!this.Bot.HasInHandOrHasInMonstersZone(CardId.Multifaker) && this.Bot.GetRemainingCount(CardId.Multifaker, 2) > 0 && this.Multifaker_can_ss())
                    {
                        this.AI.SelectCard(CardId.Multifaker);
                        return true;
                    }
                    if (!this.Bot.HasInHand(CardId.Marionetter) && this.Bot.GetRemainingCount(CardId.Marionetter,3) > 0)
                    {
                        this.AI.SelectCard(CardId.Marionetter);
                        return true;
                    }
                }
                this.AI.SelectCard(
                    CardId.Kunquery,
                    CardId.Marionetter,
                    CardId.Multifaker,
                    CardId.Silquitous
                    );
                return true;
            }
            return false;
        }

        public bool Multifaker_handss()
        {
            if (!this.Multifaker_candeckss() || this.Card.Location != CardLocation.Hand)
            {
                return false;
            }

            this.Multifaker_ssfromhand = true;
            if (this.Duel.Player != 0 && this.Util.GetOneEnemyBetterThanMyBest() != null)
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
            }

            return true;
        }

        public bool Multifaker_deckss()
        {
            if (this.Card.Location != CardLocation.Hand)
            {
                ClientCard Silquitous_target = this.GetProblematicEnemyCard_Alter(true);
                if (this.Duel.Player == 1 && this.Duel.Phase >= DuelPhase.Main2 && this.GetProblematicEnemyCard_Alter(true) == null && this.Bot.GetRemainingCount(CardId.Meluseek,3) > 0)
                {
                    this.AI.SelectCard(CardId.Meluseek);
                    this.Multifaker_ssfromdeck = true;
                    return true;
                }
                else if (!this.Silquitous_bounced && !this.Bot.HasInMonstersZone(CardId.Silquitous) && this.Bot.GetRemainingCount(CardId.Silquitous,2) > 0
                    && !(this.Duel.Player == 0 && Silquitous_target==null))
                {
                    this.AI.SelectCard(CardId.Silquitous);
                    this.Multifaker_ssfromdeck = true;
                    return true;
                }
                else if (!this.Meluseek_searched && !this.Bot.HasInMonstersZone(CardId.Meluseek) && this.Bot.GetRemainingCount(CardId.Meluseek, 3) > 0)
                {
                    this.AI.SelectCard(CardId.Meluseek);
                    this.Multifaker_ssfromdeck = true;
                    return true;
                }
                else if (this.Bot.GetRemainingCount(CardId.Kunquery,1) > 0)
                {
                    this.AI.SelectCard(CardId.Kunquery);
                    this.Multifaker_ssfromdeck = true;
                    return true;
                } else
                {
                    this.AI.SelectCard(CardId.Marionetter);
                    this.Multifaker_ssfromdeck = true;
                    return true;
                }
            }
            return false;
        }

        public bool Silquitous_eff()
        {
            if (this.ActivateDescription != this.Util.GetStringId(CardId.Silquitous,0))
            {
                if (!this.Bot.HasInHandOrInSpellZone(CardId.Manifestation) && this.Bot.HasInGraveyard(CardId.Manifestation))
                {
                    this.AI.SelectCard(CardId.Manifestation);
                } else
                {
                    this.AI.SelectCard(CardId.Protocol);
                }
                this.Silquitous_recycled = true;
                return true;
            }
            else {
                ClientCard bounce_self = null;
                int Protocol_count = 0;
                ClientCard faceup_Protocol = null;
                ClientCard faceup_Manifestation = null;
                ClientCard selected_target = null;
                foreach (ClientCard spell in this.Bot.GetSpells())
                {
                    if (spell.IsCode(CardId.Protocol))
                    {
                        if (spell.IsFaceup())
                        {
                            faceup_Protocol = spell;
                            Protocol_count += 11;
                        } else
                        {
                            Protocol_count++;
                        }
                    }
                    if (spell.IsCode(CardId.Manifestation) && spell.IsFaceup())
                    {
                        faceup_Manifestation = spell;
                    }

                    if (this.Duel.LastChainPlayer != 0 && this.Util.IsChainTarget(spell) && spell.IsFaceup() && this.isAltergeist(spell))
                    {
                        selected_target = spell;
                    }
                }
                if (Protocol_count >= 12)
                {
                    bounce_self = faceup_Protocol;
                } else if (this.Duel.Player == 0 && faceup_Protocol != null)
                {
                    bounce_self = faceup_Protocol;
                } else if (faceup_Manifestation != null)
                {
                    bounce_self = faceup_Manifestation;
                }
                ClientCard faceup_Multifaker = null;
                ClientCard faceup_monster = null;
                List<ClientCard> monster_list = this.Bot.GetMonsters();
                monster_list.Sort(CardContainer.CompareCardAttack);
                foreach(ClientCard card in monster_list)
                {
                    if (card.IsFaceup() && this.isAltergeist(card) && card != this.Card)
                    {
                        if (this.Duel.LastChainPlayer != 0 && this.Util.IsChainTarget(card) && card.IsFaceup())
                        {
                            selected_target = card;
                        }
                        if (faceup_Multifaker == null && card.IsCode(CardId.Multifaker))
                        {
                            faceup_Multifaker = card;
                        }

                        if (faceup_monster == null && !card.IsCode(CardId.Hexstia))
                        {
                            faceup_monster = card;
                        }
                    }
                }
                if (bounce_self == null)
                {
                    if (selected_target != null && selected_target != this.Card)
                    {
                        bounce_self = selected_target;
                    }
                    else if (faceup_Multifaker != null)
                    {
                        bounce_self = faceup_Multifaker;
                    }
                    else
                    {
                        bounce_self = faceup_monster;
                    }
                }

                ClientCard card_should_bounce_immediately = this.GetProblematicEnemyCard_Alter(true);
                if (card_should_bounce_immediately != null && this.Duel.LastChainPlayer != 0 && !this.bot_can_s_Meluseek())
                {
                    Logger.DebugWriteLine("Silquitous: dangerous");
                    this.AI.SelectCard(bounce_self);
                    this.AI.SelectNextCard(card_should_bounce_immediately);
                    return true;
                }
                if (this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2)
                {
                    if (this.Duel.LastChainPlayer != 0)
                    {
                        Logger.DebugWriteLine("Silquitous: battle");
                        if (this.Util.ChainContainsCard(CardId.Linkuriboh) || this.Bot.HasInHand(CardId.Kunquery))
                        {
                            return false;
                        }

                        if (this.Enemy.BattlingMonster != null && this.Bot.BattlingMonster != null && this.Enemy.BattlingMonster.GetDefensePower() >= this.Bot.BattlingMonster.GetDefensePower())
                        {
                            if (this.Bot.HasInMonstersZone(CardId.Kunquery))
                            {
                                this.AI.SelectCard(CardId.Kunquery);
                            }
                            else
                            {
                                this.AI.SelectCard(bounce_self);
                            }

                            List<ClientCard> enemy_list = this.Enemy.GetMonsters();
                            enemy_list.Sort(CardContainer.CompareCardAttack);
                            enemy_list.Reverse();
                            foreach(ClientCard target in enemy_list)
                            {
                                if (target.IsAttack() && !target.IsShouldNotBeMonsterTarget() && !target.IsShouldNotBeTarget())
                                {
                                    this.AI.SelectNextCard(target);
                                    return true;
                                }
                            }
                            this.AI.SelectNextCard(this.Enemy.BattlingMonster);
                            return true;
                        }
                    }
                } 
                else if (this.Duel.Phase > DuelPhase.Main2)
                {
                    if (this.Duel.LastChainPlayer != 0)
                    {
                        Logger.DebugWriteLine("Silquitous: end");
                        ClientCard enemy_card = this.GetBestEnemyCard_random();
                        if (enemy_card != null)
                        {
                            this.AI.SelectCard(bounce_self);
                            this.AI.SelectNextCard(enemy_card);
                            return true;
                        }
                    }
                } else if (this.Duel.Player == 0)
                {
                    Logger.DebugWriteLine("Silquitous: orenoturn");
                    if (this.Duel.Phase < DuelPhase.Main2 && this.summoned && bounce_self.IsMonster())
                    {
                        return false;
                    }

                    ClientCard enemy_card = this.GetBestEnemyCard_random();
                    if (enemy_card != null)
                    {
                        Logger.DebugWriteLine("Silquitous decide:" + bounce_self?.Name);
                        this.AI.SelectCard(bounce_self);
                        this.AI.SelectNextCard(enemy_card);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool Manifestation_eff()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                if (this.Util.ChainContainsCard(CardId.Silquitous))
                {
                    return false;
                }

                if (!this.Bot.HasInHandOrInSpellZone(CardId.Protocol) && !this.Util.ChainContainsCard(CardId.Protocol))
                {
                    this.AI.SelectCard(CardId.Protocol);
                    return true;
                }
                return false;
            }
            else
            {
                if (this.Util.ChainContainsCard(CardId.Manifestation) || this.Util.ChainContainsCard(CardId.Spoofing))
                {
                    return false;
                }

                if (this.Duel.LastChainPlayer == 0 && !(this.Util.GetLastChainCard() != null && this.Util.GetLastChainCard().IsCode(CardId.Hexstia)))
                {
                    return false;
                }

                if (this.Bot.HasInMonstersZone(CardId.Hexstia))
                {
                    bool has_position = false;
                    for (int i = 0; i < 7; ++i)
                    {
                        ClientCard target = this.Bot.MonsterZone[i];
                        if (target != null && target.IsCode(CardId.Hexstia))
                        {
                            int next_id = this.get_Hexstia_linkzone(i);
                            if (next_id != -1)
                            {
                                ClientCard next_card = this.Bot.MonsterZone[next_id];
                                if (next_card == null)
                                {
                                    has_position = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!has_position)
                    {
                        return false;
                    }
                }

                if (this.Enemy.HasInMonstersZone(94977269) && this.Bot.HasInGraveyard(CardId.Silquitous))
                {
                    this.AI.SelectCard(CardId.Silquitous);
                    return true;
                }

                if (this.Multifaker_candeckss() && this.Bot.HasInGraveyard(CardId.Multifaker) && this.has_altergeist_left())
                {
                    if (this.Bot.HasInHand(CardId.Multifaker) && this.Bot.HasInGraveyard(CardId.Silquitous) && this.Bot.GetRemainingCount(CardId.Silquitous,2) == 0)
                    {
                        this.AI.SelectCard(CardId.Silquitous);
                        return true;
                    } else
                    {
                        this.AI.SelectCard(CardId.Multifaker);
                        return true;
                    }
                }
                List<int> choose_list = new List<int>();
                choose_list.Add(CardId.Hexstia);
                choose_list.Add(CardId.Silquitous);
                choose_list.Add(CardId.Meluseek);
                choose_list.Add(CardId.Marionetter);
                choose_list.Add(CardId.Kunquery);
                foreach(int id in choose_list)
                {
                    if (this.Bot.HasInGraveyard(id)){
                        if (id == CardId.Kunquery
                            && (!this.Bot.HasInHand(CardId.Multifaker) || !this.Multifaker_candeckss()))
                        {
                            continue;
                        }

                        this.AI.SelectCard(id);
                        return true;
                    }
                }

            }
            return false;
        }

        public bool Protocol_negate_better()
        {
            // skip if no one of enemy's monsters is better
            if (this.ActivateDescription == this.Util.GetStringId(CardId.Protocol, 1))
            {
                if (this.Util.GetOneEnemyBetterThanMyBest(true) == null)
                {
                    return false;
                }
            }
            return this.Protocol_negate();
        }

        public bool Protocol_negate()
        {
            // negate
            if (this.ActivateDescription == this.Util.GetStringId(CardId.Protocol, 1) && (!this.Card.IsDisabled() || this.Protocol_activing()))
            {
                if (!this.Should_counter())
                {
                    return false;
                }

                if (this.is_should_not_negate())
                {
                    return false;
                }

                if (this.Should_activate_Protocol())
                {
                    return false;
                }

                foreach (ClientCard card in this.Bot.GetSpells())
                {
                    if (card.IsCode(CardId.Protocol) && card.IsFaceup() && card != this.Card
                        && (this.Card.IsFacedown() || !this.Card.IsDisabled()))
                    {
                        this.AI.SelectCard(card);
                        return true;
                    }
                }
                if (this.Bot.HasInMonstersZone(CardId.Hexstia))
                {
                    for (int i = 0; i < 7; ++i)
                    {
                        ClientCard target = this.Bot.MonsterZone[i];
                        if (target != null && this.isAltergeist(target) && target.IsFaceup())
                        {
                            if (target.IsCode(CardId.Hexstia))
                            {
                                int next_index = this.get_Hexstia_linkzone(i);
                                if (next_index != -1 && this.Bot.MonsterZone[next_index] != null && this.Bot.MonsterZone[next_index].IsFaceup() && this.isAltergeist(this.Bot.MonsterZone[next_index].Id))
                                {
                                    continue;
                                }
                            }
                            if (!this.get_linked_by_Hexstia(i))
                            {
                                Logger.DebugWriteLine("negate_index: " + i.ToString());
                                this.AI.SelectCard(target);
                                return true;
                            }
                        }
                    }
                }
                List<int> cost_list = new List<int>();
                if (this.Util.ChainContainsCard(CardId.Manifestation))
                {
                    cost_list.Add(CardId.Manifestation);
                }

                if (!this.Card.IsDisabled())
                {
                    cost_list.Add(CardId.Protocol);
                }

                cost_list.Add(CardId.Multifaker);
                cost_list.Add(CardId.Marionetter);
                cost_list.Add(CardId.Kunquery);
                if (this.Meluseek_searched)
                {
                    cost_list.Add(CardId.Meluseek);
                }

                if (this.Silquitous_bounced)
                {
                    cost_list.Add(CardId.Silquitous);
                }

                for (int i = 0; i < 7; ++i)
                {
                    ClientCard card = this.Bot.MonsterZone[i];
                    if (card != null && card.IsCode(CardId.Hexstia))
                    {
                        int nextzone = this.get_Hexstia_linkzone(i);
                        if (nextzone != -1)
                        {
                            ClientCard linkedcard = this.Bot.MonsterZone[nextzone];
                            if (linkedcard == null || !this.isAltergeist(linkedcard))
                            {
                                cost_list.Add(CardId.Hexstia);
                            }
                        } else
                        {
                            cost_list.Add(CardId.Hexstia);
                        }
                    }
                }
                if (!this.Silquitous_bounced)
                {
                    cost_list.Add(CardId.Silquitous);
                }

                if (!this.Meluseek_searched)
                {
                    cost_list.Add(CardId.Meluseek);
                }

                if (!this.Util.ChainContainsCard(CardId.Manifestation))
                {
                    cost_list.Add(CardId.Manifestation);
                }

                this.AI.SelectCard(cost_list);
                return true;
            }
            return false;
        }

        public bool Protocol_activate_not_use()
        {
            if (this.Util.GetLastChainCard() != null && this.Util.GetLastChainCard().Controller == 0 && this.Util.GetLastChainCard().IsTrap())
            {
                return false;
            }

            if (this.ActivateDescription != this.Util.GetStringId(CardId.Protocol, 1))
            {
                if (this.Util.IsChainTarget(this.Card) && this.Card.IsFacedown())
                {
                    return true;
                }

                if (this.Should_activate_Protocol())
                {
                    return true;
                }

                if (!this.Multifaker_ssfromhand && this.Multifaker_candeckss() && (this.Bot.HasInHand(CardId.Multifaker) || this.Bot.HasInSpellZone(CardId.Spoofing)))
                {
                    if (!this.Bot.HasInMonstersZone(CardId.Hexstia))
                    {
                        return true;
                    }

                    for (int i = 0; i < 7; ++i)
                    {
                        if (i == 4)
                        {
                            continue;
                        }

                        if (this.Bot.MonsterZone[i] != null && this.Bot.MonsterZone[i].IsCode(CardId.Hexstia))
                        {
                            int next_id = this.get_Hexstia_linkzone(i);
                            if (next_id != -1)
                            {
                                if (this.Bot.MonsterZone[next_id] == null)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                int can_bounce = 0;
                bool should_disnegate = false;
                foreach(ClientCard card in this.Bot.GetMonsters())
                {
                    if (this.isAltergeist(card))
                    {
                        if (card.IsCode(CardId.Silquitous) && card.IsFaceup() && !this.Silquitous_bounced)
                        {
                            can_bounce += 10;
                        }
                        else if (card.IsFaceup() && !card.IsCode(CardId.Hexstia))
                        {
                            can_bounce++;
                        }

                        if (card.IsDisabled() && !this.Protocol_activing())
                        {
                            should_disnegate = true;
                        }
                    }
                }
                if (can_bounce == 10 || should_disnegate)
                {
                    return true;
                }

                if (this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2 && this.Bot.HasInHand(CardId.Kunquery) && this.Util.GetOneEnemyBetterThanMyBest() != null)
                {
                    return true;
                }
            }
            return false;
        }

        public void Spoofing_select(IList<int> list)
        {
            foreach(ClientCard card in this.Duel.CurrentChain)
            {
                if (card != null
                    && card.Location == CardLocation.SpellZone && card.Controller == 0 && card.IsFaceup())
                {
                    if (card.IsCode(CardId.Manifestation))
                    {
                        this.AI.SelectCard(card);
                        return;
                    }
                }
            }
            foreach (ClientCard card in this.Bot.Hand)
            {
                foreach (int id in list)
                {
                    if (card.IsCode(id) && !(id == CardId.Multifaker && this.Util.GetLastChainCard() == card))
                    {
                        this.AI.SelectCard(card);
                        return;
                    }
                }
            }
            foreach(ClientCard card in this.Bot.GetSpells())
            {
                foreach (int id in list)
                {
                    if (card.IsFaceup() && card.IsCode(id))
                    {
                        this.AI.SelectCard(card);
                        return;
                    }
                }
            }
            foreach (ClientCard card in this.Bot.GetMonsters())
            {
                foreach (int id in list)
                {
                    if (card.IsFaceup() && card.IsCode(id))
                    {
                        this.AI.SelectCard(card);
                        return;
                    }
                }
            }
            this.AI.SelectCard((ClientCard)null);
        }

        public bool Spoofing_eff()
        {
            if (this.Util.ChainContainsCard(CardId.Spoofing))
            {
                return false;
            }

            if (this.Card.IsDisabled())
            {
                return false;
            }

            if (!this.Util.ChainContainPlayer(0) && !this.Multifaker_ssfromhand && this.Multifaker_candeckss() && this.Bot.HasInHand(CardId.Multifaker) && this.Card.HasPosition(CardPosition.FaceDown))
            {
                this.AI.SelectYesNo(false);
                return true;
            }
            bool has_cost = false;
            bool can_ss_Multifaker = this.Multifaker_can_ss() || this.Card.IsFacedown();
            // cost check(not select)
            if (this.Card.IsFacedown())
            {
                foreach(ClientCard card in this.Bot.Hand)
                {
                    if (this.isAltergeist(card))
                    {
                        has_cost = true;
                        break;
                    }
                }
                if (!has_cost)
                {
                    foreach(ClientCard card in this.Bot.GetSpells())
                    {
                        if (this.isAltergeist(card) && card.IsFaceup())
                        {
                            has_cost = true;
                            break;
                        }
                    }
                }
                if (!has_cost)
                {
                    foreach(ClientCard card in this.Bot.GetMonsters())
                    {
                        if (this.isAltergeist(card) && card.IsFaceup())
                        {
                            has_cost = true;
                            break;
                        }
                    }
                }
                if (!has_cost)
                {
                    foreach (ClientCard card in this.Bot.GetSpells())
                    {
                        if (this.isAltergeist(card) && card.IsFaceup())
                        {
                            has_cost = true;
                            break;
                        }
                    }
                }
                if (!has_cost)
                {
                    return false;
                }
            }
            if (this.Duel.Player == 1)
            {
                if (!this.Multifaker_ssfromhand && this.Multifaker_candeckss() && !this.Bot.HasInHand(CardId.Multifaker) && can_ss_Multifaker)
                {
                    if (this.Bot.HasInHand(CardId.Silquitous))
                    {
                        foreach (ClientCard card in this.Bot.Hand)
                        {
                            if (card.IsCode(CardId.Silquitous))
                            {
                                this.AI.SelectCard(card);
                                this.AI.SelectNextCard(CardId.Multifaker, CardId.Kunquery);
                                return true;
                            }
                        }
                    }
                    else
                    {
                        this.Spoofing_select(new[]
                        {
                            CardId.Silquitous,
                            CardId.Manifestation,
                            CardId.Kunquery,
                            CardId.Marionetter,
                            CardId.Multifaker,
                            CardId.Protocol,
                            CardId.Meluseek
                        });
                        this.AI.SelectNextCard(
                            CardId.Multifaker,
                            CardId.Marionetter,
                            CardId.Meluseek,
                            CardId.Kunquery,
                            CardId.Silquitous
                            );
                        return true;
                    }
                }
            }
            else
            {
                ClientCard self_best = this.Util.GetBestBotMonster();
                int best_atk = self_best == null ? 0 : self_best.Attack;
                ClientCard enemy_best = this.Util.GetProblematicEnemyCard(best_atk, true);
                ClientCard enemy_target = this.GetProblematicEnemyCard_Alter(true, false);

                if (!this.Multifaker_ssfromhand && this.Multifaker_candeckss() && can_ss_Multifaker)
                {
                    this.Spoofing_select(new[]{
                        CardId.Silquitous,
                        CardId.Manifestation,
                        CardId.Kunquery,
                        CardId.Marionetter,
                        CardId.Multifaker,
                        CardId.Protocol,
                        CardId.Meluseek
                    });
                    this.AI.SelectNextCard(
                        CardId.Multifaker,
                        CardId.Marionetter,
                        CardId.Meluseek,
                        CardId.Kunquery,
                        CardId.Silquitous
                        );
                }
                else if (!this.summoned && !this.Bot.HasInGraveyard(CardId.Meluseek) && this.Bot.GetRemainingCount(CardId.Meluseek,3) > 0 && !this.Bot.HasInHand(CardId.Meluseek)
                    && (enemy_best != null || enemy_target != null) )
                {
                    if (this.Bot.HasInHand(CardId.Silquitous))
                    {
                        foreach (ClientCard card in this.Bot.Hand)
                        {
                            if (card.IsCode(CardId.Silquitous))
                            {
                                this.AI.SelectCard(card);
                                this.AI.SelectNextCard(
                                    CardId.Meluseek,
                                    CardId.Marionetter
                                    );
                                return true;
                            }
                        }
                    }
                    else
                    {
                        this.Spoofing_select(new[]
                        {
                            CardId.Silquitous,
                            CardId.Manifestation,
                            CardId.Kunquery,
                            CardId.Multifaker,
                            CardId.Protocol,
                            CardId.Meluseek,
                            CardId.Marionetter,
                        });
                        this.AI.SelectNextCard(
                            CardId.Meluseek,
                            CardId.Marionetter,
                            CardId.Multifaker,
                            CardId.Kunquery
                            );
                        return true;
                    }
                }
                else if (!this.summoned && !this.Bot.HasInHand(CardId.Marionetter) && this.Bot.GetRemainingCount(CardId.Marionetter,3) > 0)
                {
                    if (this.Bot.HasInHand(CardId.Silquitous))
                    {
                        foreach (ClientCard card in this.Bot.Hand)
                        {
                            if (card.IsCode(CardId.Silquitous))
                            {
                                this.AI.SelectCard(card);
                                this.AI.SelectNextCard(
                                    CardId.Marionetter,
                                    CardId.Meluseek
                                    );
                                return true;
                            }
                        }
                    }
                    else
                    {
                        this.Spoofing_select(new[]
                        {
                            CardId.Silquitous,
                            CardId.Manifestation,
                            CardId.Kunquery,
                            CardId.Multifaker,
                            CardId.Protocol,
                            CardId.Meluseek,
                            CardId.Marionetter,
                        });
                        this.AI.SelectNextCard(
                            CardId.Marionetter,
                            CardId.Meluseek,
                            CardId.Multifaker,
                            CardId.Kunquery
                            );
                        return true;
                    }
                }
            }
            // target protect
            bool go = false;
            foreach(ClientCard card in this.Bot.GetSpells())
            {
                if ( (this.Util.ChainContainsCard(DefaultExecutor.CardId.HarpiesFeatherDuster) || this.Util.IsChainTarget(card)) 
                    && card.IsFaceup() && this.Duel.LastChainPlayer != 0 && this.isAltergeist(card))
                {
                    this.AI.SelectCard(card);
                    go = true;
                    break;
                }
            }
            if (!go)
            {
                foreach (ClientCard card in this.Bot.GetMonsters())
                {
                    if ( (this.Util.IsChainTarget(card) || this.Util.ChainContainsCard(CardId.DarkHole) || (!this.Protocol_activing() && card.IsDisabled()))
                        && card.IsFaceup() && this.Duel.LastChainPlayer != 0 && this.isAltergeist(card))
                    {
                        Logger.DebugWriteLine("Spoofing target:" + card?.Name);
                        this.AI.SelectCard(card);
                        go = true;
                        break;
                    }
                }
            }
            if (go)
            {
                this.AI.SelectNextCard(
                    CardId.Marionetter,
                    CardId.Meluseek,
                    CardId.Multifaker,
                    CardId.Kunquery
                    );
                return true;
            }
            return false;
        }

        public bool OneForOne_activate()
        {
            if (!this.spell_trap_activate())
            {
                return false;
            }

            if (!this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.Meluseek) && !this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.Multifaker))
            {
                this.AI.SelectCard(
                    CardId.GR_WC,
                    CardId.MaxxC,
                    CardId.Kunquery,
                    CardId.GO_SR
                    );
                if (this.Util.IsTurn1OrMain2())
                {
                    this.AI.SelectPosition(CardPosition.FaceUpDefence);
                }

                return true;
            }
            if (!this.summoned && !this.Meluseek_searched && !this.Bot.HasInHand(CardId.Marionetter))
            {
                this.AI.SelectCard(
                    CardId.GR_WC,
                    CardId.MaxxC,
                    CardId.Kunquery,
                    CardId.GO_SR
                    );
                return true;
            }
            return false;
        }

        public bool Meluseek_summon()
        {
            if (this.EvenlyMatched_ready())
            {
                return false;
            }

            if (this.Bot.HasInHand(CardId.Marionetter) && this.Bot.HasInGraveyard(CardId.Meluseek) && !this.Marionetter_reborn)
            {
                return false;
            }

            this.summoned = true;
            return true;
        }

        public bool Marionetter_summon()
        {
            if (this.EvenlyMatched_ready())
            {
                return false;
            }

            this.summoned = true;
            return true;
        }

        public bool Silquitous_summon()
        {
            if (this.EvenlyMatched_ready())
            {
                return false;
            }

            bool can_summon = false;
            if (this.Enemy.GetMonsterCount() == 0 && this.Enemy.LifePoints <= 800)
            {
                return true;
            }

            foreach (ClientCard card in this.Bot.Hand)
            {
                if (this.isAltergeist(card) && card.IsTrap())
                {
                    can_summon = true;
                    break;
                }
            }
            foreach(ClientCard card in this.Bot.GetMonstersInMainZone())
            {
                if (this.isAltergeist(card))
                {
                    can_summon = true;
                    break;
                }
            }
            foreach(ClientCard card in this.Bot.GetSpells())
            {
                if (this.isAltergeist(card))
                {
                    can_summon = true;
                    break;
                }
            }
            if (can_summon)
            {
                this.summoned = true;
                return true;
            } else
            {
                return false;
            }
        }

        public bool Multifaker_summon()
        {
            if (this.EvenlyMatched_ready())
            {
                return false;
            }

            if (this.Enemy.GetMonsterCount() == 0 && this.Enemy.LifePoints <= 1200)
            {
                return true;
            }

            if (this.Bot.HasInMonstersZone(CardId.Silquitous) || this.Bot.HasInHandOrInSpellZone(CardId.Spoofing))
            {
                this.summoned = true;
                return true;
            }
            return false;
        }

        public bool PotofDesires_activate()
        {
            if (this.Bot.Deck.Count > 15 && this.spell_trap_activate())
            {
                this.AI.SelectPlace(this.SelectSTPlace(this.Card, true));
                return true;
            }
            return false;
        }

        public bool PotofIndulgence_activate()
        {
            if (!this.spell_trap_activate())
            {
                return false;
            }

            if (!this.Bot.HasInGraveyard(CardId.Linkuriboh) && !this.Bot.HasInGraveyard(CardId.Hexstia))
            {
                int important_count = 0;
                foreach (ClientCard card in this.Bot.ExtraDeck)
                {
                    if (card.Id == CardId.Linkuriboh || card.Id == CardId.Hexstia)
                    {
                        important_count++;
                    }
                }
                if (important_count > 0)
                {
                    this.AI.SelectPlace(this.SelectSTPlace(this.Card, true));
                    this.AI.SelectOption(1);
                    return true;
                }
                return false;
            }
            this.AI.SelectPlace(this.SelectSTPlace(this.Card, true));
            this.AI.SelectOption(1);
            return true;
        }

        public bool Anima_ss()
        {
            if (this.Duel.Phase != DuelPhase.Main2)
            {
                return false;
            }

            ClientCard card_ex_left = this.Enemy.MonsterZone[6];
            ClientCard card_ex_right = this.Enemy.MonsterZone[5];
            if (card_ex_left != null && card_ex_left.HasLinkMarker((int)CardLinkMarker.Top))
            {
                ClientCard self_card_1 = this.Bot.MonsterZone[1];
                if (self_card_1 == null)
                {
                    this.AI.SelectMaterials(CardId.Meluseek);
                    this.AI.SelectPlace(Zones.MonsterZone2);
                    this.ss_other_monster = true;
                    return true;
                } else if (self_card_1.IsCode(CardId.Meluseek))
                {
                    this.AI.SelectMaterials(self_card_1);
                    this.AI.SelectPlace(Zones.MonsterZone2);
                    this.ss_other_monster = true;
                    return true;
                }
            }
            if (card_ex_right != null && card_ex_right.HasLinkMarker((int)CardLinkMarker.Top))
            {
                ClientCard self_card_2 = this.Bot.MonsterZone[3];
                if (self_card_2 == null)
                {
                    this.AI.SelectMaterials(CardId.Meluseek);
                    this.AI.SelectPlace(Zones.MonsterZone4);
                    this.ss_other_monster = true;
                    return true;
                }
                else if (self_card_2.IsCode(CardId.Meluseek))
                {
                    this.AI.SelectMaterials(self_card_2);
                    this.AI.SelectPlace(Zones.MonsterZone4);
                    this.ss_other_monster = true;
                    return true;
                }
            }
            ClientCard card_left = this.Enemy.MonsterZone[3];
            ClientCard card_right = this.Enemy.MonsterZone[1];
            if (card_left != null && card_left.IsFacedown())
            {
                card_left = null;
            }

            if (card_right != null && card_right.IsFacedown())
            {
                card_right = null;
            }

            if (card_left != null && (card_left.IsShouldNotBeMonsterTarget() || card_left.IsShouldNotBeTarget()))
            {
                card_left = null;
            }

            if (card_right != null && (card_right.IsShouldNotBeMonsterTarget() || card_right.IsShouldNotBeTarget()))
            {
                card_right = null;
            }

            if (this.Enemy.MonsterZone[6] != null)
            {
                card_left = null;
            }

            if (this.Enemy.MonsterZone[5] != null)
            {
                card_right = null;
            }

            if (card_left == null && card_right != null)
            {
                if (this.Bot.MonsterZone[6] == null)
                {
                    this.AI.SelectMaterials(CardId.Meluseek);
                    this.AI.SelectPlace(Zones.ExtraZone2);
                    this.ss_other_monster = true;
                    return true;
                }
            }
            if (card_left != null && card_right == null)
            {
                if (this.Bot.MonsterZone[5] == null)
                {
                    this.AI.SelectMaterials(CardId.Meluseek);
                    this.AI.SelectPlace(Zones.ExtraZone1);
                    this.ss_other_monster = true;
                    return true;
                }
            }
            if (card_left != null && card_right != null && this.Bot.GetMonstersExtraZoneCount() == 0)
            {
                int selection = 0;
                if (card_left.IsFloodgate() && !card_right.IsFloodgate())
                {
                    selection = Zones.ExtraZone1;
                }
                else if (!card_left.IsFloodgate() && card_right.IsFloodgate())
                {
                    selection = Zones.ExtraZone2;
                }
                else
                {
                    if (card_left.GetDefensePower() >= card_right.GetDefensePower())
                    {
                        selection = Zones.ExtraZone1;
                    }
                    else
                    {
                        selection = Zones.ExtraZone2;
                    }
                }
                this.AI.SelectPlace(selection);
                this.AI.SelectMaterials(CardId.Meluseek);
                this.ss_other_monster = true;
                return true;
            }
            return false;
        }

        public bool Linkuriboh_ss()
        {
            if (this.Bot.GetMonstersExtraZoneCount() > 0)
            {
                return false;
            }

            if (this.Util.IsTurn1OrMain2() && !this.Meluseek_searched)
            {
                this.AI.SelectPlace(Zones.ExtraZone1);
                this.ss_other_monster = true;
                return true;
            }
            return false;
        }

        public bool Linkuriboh_eff()
        {
            if (this.Util.ChainContainsCard(CardId.Linkuriboh))
            {
                return false;
            }

            if (this.Util.ChainContainsCard(CardId.Multifaker))
            {
                return false;
            }

            if (this.Duel.Player == 1)
            {
                if (this.Card.Location == CardLocation.Grave)
                {
                    this.AI.SelectCard(new[] { CardId.Meluseek });
                    this.ss_other_monster = true;
                    return true;
                } else
                {
                    if (this.Card.IsDisabled() && !this.Enemy.HasInSpellZone(82732705, true))
                    {
                        return false;
                    }

                    ClientCard enemy_card = this.Enemy.BattlingMonster;
                    if (enemy_card == null)
                    {
                        return false;
                    }

                    ClientCard self_card = this.Bot.BattlingMonster;
                    if (self_card == null)
                    {
                        return (!enemy_card.IsCode(CardId.Hayate));
                    }

                    return (enemy_card.Attack > self_card.GetDefensePower());
                }
            }
            else
            {
                if (!this.summoned && !this.Bot.HasInHand(CardId.Marionetter) && !this.Meluseek_searched && (this.Duel.Phase == DuelPhase.Main1 || this.Duel.Phase == DuelPhase.Main2))
                {
                    this.AI.SelectCard(new[] { CardId.Meluseek });
                    this.ss_other_monster = true;
                    this.AI.SelectPlace(Zones.MonsterZone1 | Zones.MonsterZone5);
                    return true;
                }
                else if (this.Util.IsTurn1OrMain2())
                {
                    this.AI.SelectCard(new[] { CardId.Meluseek });
                    this.ss_other_monster = true;
                    this.AI.SelectPlace(Zones.MonsterZone1 | Zones.MonsterZone5);
                    return true;
                }
                else if (this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2)
                {
                    if (this.Duel.Player != 0 || this.attacked_Meluseek.Count == 0 || this.Enemy.GetMonsterCount() > 0)
                    {
                        return false;
                    }

                    foreach (ClientCard card in this.attacked_Meluseek)
                    {
                        if (card != null && card.Location == CardLocation.MonsterZone)
                        {

                            this.ss_other_monster = true;
                            this.AI.SelectPlace(Zones.MonsterZone1 | Zones.MonsterZone5);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool Hexstia_ss()
        {
            List<ClientCard> targets = new List<ClientCard>();
            List<ClientCard> list = this.Bot.GetMonsters();
            list.Sort(CardContainer.CompareCardAttack);
            //list.Reverse();
            bool Meluseek_selected = false;
            bool Silquitous_selected = false;
            bool Hexstia_selected = false;
            int altergeist_count = 0;
            foreach (ClientCard card in list)
            {
                if (this.isAltergeist(card))
                {
                    altergeist_count++;
                }

                if (card.IsCode(CardId.Meluseek) && targets.Count < 2 && card.IsFaceup())
                {
                    if ((!this.Meluseek_searched || !Meluseek_selected) && (!this.summoned || this.Duel.Phase == DuelPhase.Main2))
                    {
                        Meluseek_selected = true;
                        targets.Add(card);
                    }
                }
                else if (card.IsCode(CardId.Silquitous) && targets.Count < 2 && card.IsFaceup() && !this.Bot.HasInGraveyard(CardId.Silquitous))
                {
                    if (!this.Silquitous_recycled || !Silquitous_selected)
                    {
                        Silquitous_selected = true;
                        targets.Add(card);
                    }
                }
                else if (card.IsCode(CardId.Hexstia) && targets.Count < 2 && card.IsFaceup())
                {
                    if ((!this.Hexstia_searched || !Hexstia_selected) && !this.summoned && !this.Bot.HasInHand(CardId.Marionetter) && this.Bot.GetRemainingCount(CardId.Marionetter, 3) > 0)
                    {
                        Hexstia_selected = true;
                        targets.Add(card);
                    }
                }
                else if (this.isAltergeist(card) && targets.Count < 2 && card.IsFaceup())
                {
                    targets.Add(card);
                }
                else if (card.IsCode(CardId.Silquitous) && targets.Count < 2 && card.IsFaceup())
                {
                    if (!this.Silquitous_recycled || !Silquitous_selected)
                    {
                        Silquitous_selected = true;
                        targets.Add(card);
                    }
                }
            }
            if (targets.Count >= 2)
            {
                if (this.Duel.Phase < DuelPhase.Main2)
                {
                    if (this.GetTotalATK(targets) >= 1500 && (this.summoned || (!Meluseek_selected && !Hexstia_selected)))
                    {
                        return false;
                    }
                }
                bool can_have_Multifaker = (this.Bot.HasInHand(CardId.Multifaker) 
                    || (this.Bot.GetRemainingCount(CardId.Multifaker, 2) > 0 
                        && ( (Meluseek_selected && !this.Meluseek_searched) 
                            || (Hexstia_selected && !this.Hexstia_searched) )));
                if (can_have_Multifaker && this.Multifaker_can_ss())
                {
                    altergeist_count++;
                }

                if (this.Bot.HasInHandOrInSpellZone(CardId.Manifestation))
                {
                    altergeist_count++;
                }

                Logger.DebugWriteLine("Multifaker_ss_check: count = " + altergeist_count.ToString());
                if (altergeist_count <= 2)
                {
                    return false;
                }

                this.AI.SelectMaterials(targets);
                return true;
            }
            return false;
        }

        public bool TripleBurstDragon_eff()
        {
            if (this.ActivateDescription != this.Util.GetStringId(CardId.TripleBurstDragon,0))
            {
                return false;
            }

            return (this.Duel.LastChainPlayer != 0);
        }

        public bool TripleBurstDragon_ss()
        {
            if (!this.Enemy.HasInGraveyard(CardId.Raye))
            {
                ClientCard self_best = this.Util.GetBestBotMonster(true);
                int self_power = (self_best != null) ? self_best.Attack : 0;
                ClientCard enemy_best = this.Util.GetBestEnemyMonster(true);
                int enemy_power = (enemy_best != null) ? enemy_best.GetDefensePower() : 0;
                if (enemy_power <= self_power)
                {
                    return false;
                }

                Logger.DebugWriteLine("Three: enemy: " + enemy_power.ToString() + ", bot: " + self_power.ToString());
                if (enemy_power >= 2401)
                {
                    return false;
                }
            };
            foreach (ClientCard card in this.Bot.GetMonstersInExtraZone())
            {
                if (!card.HasType(CardType.Link))
                {
                    return false;
                }
            }
            int link_count = 0;
            if (this.Enemy.HasInMonstersZone(CardId.Shizuku) && this.Enemy.GetGraveyardSpells().Count >= 9)
            {
                return false;
            }

            List<ClientCard> list = new List<ClientCard>();
            if (this.Bot.HasInMonstersZone(CardId.Needlefiber))
            {
                foreach(ClientCard card in this.Bot.GetMonsters())
                {
                    if (card.IsCode(CardId.Needlefiber))
                    {
                        list.Add(card);
                        link_count += 2;
                    }
                }
            }
            List<ClientCard> monsters = this.Bot.GetMonsters();
            monsters.Sort(CardContainer.CompareCardAttack);
            //monsters.Reverse();
            foreach(ClientCard card in monsters)
            {
                if (!list.Contains(card) && card.LinkCount < 3)
                {
                    list.Add(card);
                    link_count += (card.HasType(CardType.Link) ? card.LinkCount : 1);
                    if (link_count >= 3)
                    {
                        break;
                    }
                }
            }
            if (link_count >= 3)
            {
                this.AI.SelectMaterials(list);
                this.ss_other_monster = true;
                return true;
            }
            return false;

        }

        public bool Needlefiber_ss()
        {
            if (!this.Enemy.HasInGraveyard(CardId.Raye))
            {
                ClientCard self_best = this.Util.GetBestBotMonster(true);
                int self_power = (self_best != null) ? self_best.Attack : 0;
                ClientCard enemy_best = this.Util.GetBestEnemyMonster(true);
                int enemy_power = (enemy_best != null) ? enemy_best.GetDefensePower() : 0;
                if (enemy_power < self_power)
                {
                    return false;
                }

                if (this.Bot.GetMonsterCount() <= 2 && enemy_power >= 2401)
                {
                    return false;
                }
            }
            foreach(ClientCard card in this.Bot.GetMonstersInExtraZone())
            {
                if (!card.HasType(CardType.Link))
                {
                    return false;
                }
            }
            List<ClientCard> material_list = new List<ClientCard>();
            List<ClientCard> monsters = this.Bot.GetMonsters();
            monsters.Sort(CardContainer.CompareCardAttack);
            //monsters.Reverse();
            foreach(ClientCard t in monsters)
            {
                if (t.IsTuner())
                {
                    material_list.Add(t);
                    break;
                }
            }
            foreach(ClientCard m in monsters)
            {
                if (!material_list.Contains(m) && m.LinkCount <= 2)
                {
                    material_list.Add(m);
                    if (material_list.Count >= 2)
                    {
                        break;
                    }
                }
            }
            this.AI.SelectMaterials(material_list);
            this.ss_other_monster = true;
            return true;
        }

        public bool Needlefiber_eff()
        {
            this.AI.SelectCard(
                CardId.GR_WC,
                CardId.GO_SR,
                CardId.AB_JS
                );
            return true;
        }

        public bool Borrelsword_ss()
        {
            if (this.Duel.Phase != DuelPhase.Main1)
            {
                return false;
            }

            ClientCard self_best = this.Util.GetBestBotMonster(true);
            int self_power = (self_best != null) ? self_best.Attack : 0;
            ClientCard enemy_best = this.Util.GetBestEnemyMonster(true);
            int enemy_power = (enemy_best != null) ? enemy_best.GetDefensePower() : 0;
            if (enemy_power < self_power)
            {
                return false;
            }

            foreach (ClientCard card in this.Bot.GetMonstersInExtraZone())
            {
                if (!card.HasType(CardType.Link))
                {
                    return false;
                }
            }

            List<ClientCard> material_list = new List<ClientCard>();
            List<ClientCard> bot_monster = this.Bot.GetMonsters();
            bot_monster.Sort(CardContainer.CompareCardAttack);
            //bot_monster.Reverse();
            int link_count = 0;
            foreach(ClientCard card in bot_monster)
            {
                if (card.IsFacedown())
                {
                    continue;
                }

                if (!material_list.Contains(card) && card.LinkCount < 3)
                {
                    material_list.Add(card);
                    link_count += (card.HasType(CardType.Link)) ? card.LinkCount : 1;
                    if (link_count >= 4)
                    {
                        break;
                    }
                }
            }
            if (link_count >= 4)
            {
                this.AI.SelectMaterials(material_list);
                this.ss_other_monster = true;
                return true;
            }
            return false;
        }

        public bool Borrelsword_eff()
        {
            if (this.ActivateDescription == -1)
            {
                return true;
            }
            else if ((this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2) || this.Util.IsChainTarget(this.Card))
                {
                    List<ClientCard> enemy_list = this.Enemy.GetMonsters();
                    enemy_list.Sort(CardContainer.CompareCardAttack);
                    enemy_list.Reverse();
                    foreach(ClientCard card in enemy_list)
                    {
                        if (card.HasPosition(CardPosition.Attack) && !card.HasType(CardType.Link))
                        {
                        this.AI.SelectCard(card);
                            return true;
                        }
                    }
                    List<ClientCard> bot_list = this.Bot.GetMonsters();
                    bot_list.Sort(CardContainer.CompareCardAttack);
                    //bot_list.Reverse();
                    foreach (ClientCard card in bot_list)
                    {
                        if (card.HasPosition(CardPosition.Attack) && !card.HasType(CardType.Link))
                        {
                        this.AI.SelectCard(card);
                            return true;
                        }
                    }
                }
            return false;
        }

        public bool tuner_summon()
        {
            if (this.EvenlyMatched_ready())
            {
                return false;
            }

            foreach (ClientCard card in this.Bot.GetMonstersInExtraZone())
            {
                if (card != null && !card.HasType(CardType.Link))
                {
                    return false;
                }
            }
            if (!this.Enemy.HasInGraveyard(CardId.Raye))
            {
                ClientCard self_best = this.Util.GetBestBotMonster(true);
                int self_power = (self_best != null) ? self_best.Attack : 0;
                ClientCard enemy_best = this.Util.GetBestEnemyMonster(true);
                int enemy_power = (enemy_best != null) ? enemy_best.GetDefensePower() : 0;
                Logger.DebugWriteLine("Tuner: enemy: " + enemy_power.ToString() + ", bot: " + self_power.ToString());
                if (enemy_power < self_power || enemy_power == 0)
                {
                    return false;
                }

                int real_count = (this.Bot.HasInExtra(CardId.Needlefiber)) ? this.Bot.GetMonsterCount() + 2 : this.Bot.GetMonsterCount() + 1;
                if ((real_count <= 3 && enemy_power >= 2400)
                    || !(this.Bot.HasInExtra(CardId.TripleBurstDragon) || this.Bot.HasInExtra(CardId.Borrelsword)) )
                {
                    return false;
                }
            }
            if (this.Multifaker_ssfromdeck)
            {
                return false;
            }

            foreach (ClientCard card in this.Bot.GetMonsters())
            {
                if (card.IsFaceup())
                {
                    this.summoned = true;
                    this.AI.SelectPlace(Zones.ExtraZone1);
                    return true;
                }
            }
            return false;
        }

        public override ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
        {
            List<ClientCard> Meluseek_list = new List<ClientCard>();
            for (int i = 0; i < attackers.Count; ++i)
            {
                ClientCard attacker = attackers[i];
                if (attacker.IsCode(CardId.Meluseek) && !attacker.IsDisabled())
                {
                    if (this.Enemy.GetMonsterCount() > 0)
                    {
                        return attacker;
                    }
                    // Meluseek attack first even in direct attack
                    else
                    {
                        Meluseek_list.Add(attacker);
                    }
                }
                if (attacker.IsCode(CardId.Borrelsword) && !attacker.IsDisabled())
                {
                    return attacker;
                }
            }
            if (Meluseek_list.Count > 0)
            {
                foreach(ClientCard card in Meluseek_list)
                {
                    attackers.Remove(card);
                    attackers.Add(card);
                }
            }
            return null;
        }

        public override bool OnSelectHand()
        {
            // go first
            return true;
        }

        public override void OnNewTurn()
        {
            this.Multifaker_ssfromhand = false;
            this.Multifaker_ssfromdeck = false;
            this.Marionetter_reborn = false;
            this.Hexstia_searched = false;
            this.Meluseek_searched = false;
            this.summoned = false;
            this.Silquitous_bounced = false;
            this.Silquitous_recycled = false;
            this.ss_other_monster = false;
            this.Impermanence_list.Clear();
            this.attacked_Meluseek.Clear();
        }

        public bool MonsterRepos()
        {
            if (this.Card.Attack == 0)
            {
                return (this.Card.IsAttack());
            }

            if (this.Card.IsCode(CardId.Meluseek) || this.Bot.HasInMonstersZone(CardId.Meluseek))
            {
                return this.Card.HasPosition(CardPosition.Defence);
            }
           
            if (this.isAltergeist(this.Card) && this.Bot.HasInHandOrInSpellZone(CardId.Protocol) && this.Card.IsFacedown())
            {
                return true;
            }

            bool enemyBetter = this.Util.IsAllEnemyBetter(true);
            if (this.Card.IsAttack() && enemyBetter)
            {
                return true;
            }

            if (this.Card.IsDefense() && !enemyBetter)
            {
                return true;
            }

            return false;
        }

        public bool MonsterSet()
        {
            if (this.Util.GetOneEnemyBetterThanMyBest() == null && this.Bot.GetMonsterCount() > 0)
            {
                return false;
            }

            if (this.Card.Level > 4)
            {
                return false;
            }

            int rest_lp = this.Bot.LifePoints;
            int count = this.Bot.GetMonsterCount();
            List<ClientCard> list = this.Enemy.GetMonsters();
            list.Sort(CardContainer.CompareCardAttack);
            foreach(ClientCard card in list)
            {
                if (!card.HasPosition(CardPosition.Attack))
                {
                    continue;
                }

                if (count-- > 0)
                {
                    continue;
                }

                rest_lp -= card.Attack;
            }
            if (rest_lp < 1700)
            {
                this.AI.SelectPosition(CardPosition.FaceDownDefence);
                return true;
            }
            return false;
        }

        public bool MonsterSummon()
        {
            if (this.Enemy.GetMonsterCount() + this.Bot.GetMonsterCount() > 0)
            {
                return false;
            }

            return this.Card.Attack >= this.Enemy.LifePoints;
        }

        public override BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
        {
            if (this.EvenlyMatched_ready())
            {
                List<ClientCard> enemy_m = this.Enemy.GetMonsters();
                enemy_m.Sort(CardContainer.CompareCardAttack);
                //enemy_m.Reverse();
                foreach (ClientCard e_card in enemy_m)
                {
                    if (e_card.HasPosition(CardPosition.Attack))
                    {
                        return this.AI.Attack(attacker, e_card);
                    }
                }
            }
            for (int i = 0; i < defenders.Count; ++i)
            {
                ClientCard defender = defenders[i];
                attacker.RealPower = attacker.Attack;
                defender.RealPower = defender.GetDefensePower();
                if (attacker.IsCode(CardId.Borrelsword) && !attacker.IsDisabled())
                {
                    return this.AI.Attack(attacker, defender);
                }

                if (!this.OnPreBattleBetween(attacker, defender))
                {
                    continue;
                }

                if (attacker.RealPower == defender.RealPower && this.Bot.GetMonsterCount() < this.Enemy.GetMonsterCount())
                {
                    continue;
                }

                if (attacker.RealPower > defender.RealPower || (attacker.RealPower >= defender.RealPower && attacker.IsLastAttacker && defender.IsAttack()))
                {
                    return this.AI.Attack(attacker, defender);
                }
            }

            if (attacker.CanDirectAttack && (this.Enemy.GetMonsterCount() == 0 || !attacker.IsDisabled()))
            {
                return this.AI.Attack(attacker, null);
            }

            return null;
        }

        public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
        {

            int HIINT_TOGRAVE = 504;
            if (max == 1 && cards[0].Location == CardLocation.Deck 
                && this.Util.GetLastChainCard() != null && this.Util.GetLastChainCard().IsCode(23002292) && this.Bot.GetRemainingCount(CardId.WakingtheDragon,1) > 0)
            {
                IList<ClientCard> result = new List<ClientCard>();
                foreach (ClientCard card in cards)
                {
                    if (card.IsCode(CardId.WakingtheDragon))
                    {
                        result.Add(card);
                        this.AI.SelectPlace(this.SelectSetPlace());
                        break;
                    }
                }
                if (result.Count > 0)
                {
                    return result;
                }
            }
            else if (this.Util.GetLastChainCard() != null && this.Util.GetLastChainCard().IsCode(CardId.EvenlyMatched) && this.Duel.LastChainPlayer != 0)
            {
                Logger.DebugWriteLine("EvenlyMatched: min=" + min.ToString() + ", max=" + max.ToString());
            }
            else if (cards[0].Location == CardLocation.Hand && cards[cards.Count - 1].Location == CardLocation.Hand
                && (hint == 501 || hint == HIINT_TOGRAVE) && min == max)
            {
                if (this.Duel.LastChainPlayer == 0 && this.Util.GetLastChainCard().IsCode(CardId.OneForOne))
                {
                    return null;
                }

                Logger.DebugWriteLine("Hand drop except OneForOne");
                int todrop = min;
                IList<ClientCard> result = new List<ClientCard>();
                IList<ClientCard> ToRemove = new List<ClientCard>(cards);
                // throw redundance
                List<int> record = new List<int>();
                foreach(ClientCard card in ToRemove)
                {
                    if (card?.Id != 0 && !record.Contains(card.Id))
                    {
                        record.Add(card.Id);
                    }
                    else
                    {
                        result.Add(card);
                        if (--todrop <= 0)
                        {
                            break;
                        }
                    }
                }
                if (todrop <= 0)
                {
                    return result;
                }

                foreach (ClientCard card in result)
                {
                    ToRemove.Remove(card);
                }
                // throw improper
                foreach (int throw_id in this.cards_improper)
                {
                    foreach(ClientCard card in ToRemove)
                    {
                        if (card.IsCode(throw_id))
                        {
                            result.Add(card);
                            if (--todrop <= 0)
                            {
                                return result;
                            }
                        }
                    }
                }
                // throw all??
                return null;
            }
            return null;
        }

        public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
        {
            if (this.Util.IsTurn1OrMain2()
                && (cardId == CardId.Meluseek || cardId == CardId.Silquitous))
            {
                return CardPosition.FaceUpDefence;
            }
            return 0;
        }

        public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
        {
            if (player == 0)
            {
                if (location == CardLocation.SpellZone)
                {
                    // unfinished
                }
                else if (location == CardLocation.MonsterZone)
                {
                    if(cardId == CardId.Linkuriboh)
                    {
                        if ((Zones.ExtraZone1 & available) > 0)
                        {
                            return Zones.ExtraZone1;
                        }

                        if ((Zones.ExtraZone2 & available) > 0)
                        {
                            return Zones.ExtraZone2;
                        }

                        for (int i = 4; i >= 0; --i)
                        {
                            if (this.Bot.MonsterZone[i] == null)
                            {
                                int place = (int)System.Math.Pow(2, i);
                                return place;
                            }
                        }
                    }
                    if (this.isAltergeist(cardId))
                    {
                        if (this.Bot.HasInMonstersZone(CardId.Hexstia))
                        {
                            for (int i = 0; i < 7; ++i)
                            {
                                if (i == 4)
                                {
                                    continue;
                                }

                                if (this.Bot.MonsterZone[i] != null && this.Bot.MonsterZone[i].IsCode(CardId.Hexstia))
                                {
                                    int next_index = this.get_Hexstia_linkzone(i);
                                    if (next_index != -1 && (available & (int)(System.Math.Pow(2, next_index))) > 0)
                                    {
                                        return (int)(System.Math.Pow(2, next_index));
                                    }
                                }
                            }
                        }
                        if (cardId == CardId.Hexstia)
                        {
                            // ex zone
                            if ((Zones.ExtraZone1 & available) > 0 && this.Bot.MonsterZone[1] != null && this.isAltergeist(this.Bot.MonsterZone[1].Id))
                            {
                                return Zones.ExtraZone1;
                            }

                            if ((Zones.ExtraZone2 & available) > 0 && this.Bot.MonsterZone[3] != null && this.isAltergeist(this.Bot.MonsterZone[3].Id))
                            {
                                return Zones.ExtraZone2;
                            }

                            if ( ((Zones.ExtraZone2 & available) > 0 && this.Bot.MonsterZone[3] != null && !this.isAltergeist(this.Bot.MonsterZone[3].Id))
                                || ((Zones.ExtraZone1 & available) > 0 && this.Bot.MonsterZone[1] == null) )
                            {
                                return Zones.ExtraZone1;
                            }

                            if (((Zones.ExtraZone1 & available) > 0 && this.Bot.MonsterZone[1] != null && !this.isAltergeist(this.Bot.MonsterZone[1].Id))
                                || ((Zones.ExtraZone2 & available) > 0 && this.Bot.MonsterZone[3] == null))
                            {
                                return Zones.ExtraZone2;
                            }
                            // main zone
                            for (int i = 1; i < 5; ++i)
                            {
                                if (this.Bot.MonsterZone[i] != null && this.isAltergeist(this.Bot.MonsterZone[i].Id))
                                {
                                    if ((available & (int)System.Math.Pow(2, i - 1)) > 0)
                                    {
                                        return (int)System.Math.Pow(2, i - 1);
                                    }
                                }
                            }
                        }
                        // 1 or 3
                        if ((Zones.MonsterZone2 & available) > 0)
                        {
                            return Zones.MonsterZone2;
                        }

                        if ((Zones.MonsterZone4 & available) > 0)
                        {
                            return Zones.MonsterZone4;
                        }
                    }
                }
            }
            return base.OnSelectPlace(cardId, player, location, available);
        }
    }
}