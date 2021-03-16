using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("Trickstar", "AI_Trickstar")]
    public class TrickstarExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int White = 98169343;
            public const int BF = 9929398;
            public const int Yellow = 61283655;
            public const int Red = 35199656;
            public const int Urara = 14558127;
            public const int Ghost = 59438930;
            public const int Pink = 98700941;
            public const int MG = 23434538;
            public const int Tuner = 67441435;
            public const int Eater = 63845230;
            public const int LockBird = 94145021;

            public const int Feather = 18144506;
            public const int Galaxy = 5133471;
            public const int Pot = 35261759;
            public const int Trans = 73628505;
            public const int Sheep = 73915051;
            public const int Crown = 22159429;
            public const int Stage = 35371948;
            public const int GraveCall = 24224830;
            public const int DarkHole = 53129443;

            public const int Re = 21076084;
            public const int Ring = 83555666;
            public const int Strike = 40605147;
            public const int Warn = 84749824;
            public const int Awaken = 10813327;

            public const int Linkuri = 41999284;
            public const int Linkspi = 98978921;
            public const int SafeDra = 99111753;
            public const int Crystal = 50588353;
            public const int Phoneix = 2857636;
            public const int Unicorn = 38342335;
            public const int Snake = 74997493;
            public const int Borrel = 31833038;
            public const int TG = 98558751;

            public const int Beelze = 34408491;
            public const int Abyss = 9753964;
            public const int Exterio = 99916754;
            public const int Ultimate = 86221741;
            public const int Cardian = 87460579;

            public const int Missus = 3987233;
        }

        public int getLinkMarker(int id)
        {
            if (id == CardId.Borrel || id == CardId.Snake)
            {
                return 4;
            }
            else if (id == CardId.Abyss || id == CardId.Beelze || id == CardId.Exterio || id == CardId.Ultimate || id == CardId.Cardian)
            {
                return 5;
            }
            else if (id == CardId.Unicorn)
            {
                return 3;
            }
            else if (id == CardId.Crystal || id == CardId.Phoneix || id == CardId.SafeDra || id == CardId.Missus)
            {
                return 2;
            }

            return 1;
        }

        bool NormalSummoned = false;
        ClientCard stage_locked = null;
        bool pink_ss = false;
        bool snake_four_s = false;
        bool tuner_eff_used = false;
        bool crystal_eff_used = false;
        int red_ss_count = 0;
        bool white_eff_used = false;
        bool lockbird_useful = false;
        bool lockbird_used = false;
        int GraveCall_id = 0;
        int GraveCall_count = 0;
        readonly List<int> SkyStrike_list = new List<int> {
            26077387, 8491308, 63288573, 90673288,
            21623008, 25955749, 63166095, 99550630,
            25733157, 51227866, 52340444,98338152,
            24010609, 97616504, 50005218
        };

        public TrickstarExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // counter
            this.AddExecutor(ExecutorType.Activate, CardId.MG, this.G_act);
            this.AddExecutor(ExecutorType.Activate, CardId.Strike, this.DefaultSolemnStrike);
            this.AddExecutor(ExecutorType.Activate, CardId.Warn, this.DefaultSolemnWarning);
            this.AddExecutor(ExecutorType.Activate, CardId.Awaken, this.Awaken_ss);
            this.AddExecutor(ExecutorType.Activate, CardId.Urara, this.Hand_act_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Ghost, this.Hand_act_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Ring, this.Ring_act);
            this.AddExecutor(ExecutorType.Activate, CardId.Abyss, this.Abyss_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Exterio, this.Exterio_counter);
            this.AddExecutor(ExecutorType.Activate, CardId.Cardian);
            this.AddExecutor(ExecutorType.Activate, CardId.GraveCall, this.GraveCall_eff);

            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DarkHole_eff);

            // spell clean
            this.AddExecutor(ExecutorType.Activate, this.field_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.Stage, this.Stage_Lock);
            this.AddExecutor(ExecutorType.Activate, CardId.Feather, this.Feather_Act);
            this.AddExecutor(ExecutorType.Activate, CardId.Stage, this.Stage_act);
            this.AddExecutor(ExecutorType.Activate, CardId.Galaxy, this.GalaxyCyclone);
            this.AddExecutor(ExecutorType.Activate, CardId.TG, this.TG_eff);

            this.AddExecutor(ExecutorType.Activate, CardId.Tuner, this.Tuner_eff);
            this.AddExecutor(ExecutorType.SpellSet, this.Five_Rainbow);

            // ex ss
            this.AddExecutor(ExecutorType.SpSummon, CardId.Borrel, this.Borrel_ss);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Missus, this.Missus_ss);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Phoneix, this.Phoneix_ss);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Snake, this.Snake_ss);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Crystal, this.Crystal_ss);
            this.AddExecutor(ExecutorType.SpSummon, CardId.SafeDra, this.Safedragon_ss);
            this.AddExecutor(ExecutorType.Activate, CardId.SafeDra, this.DefaultCompulsoryEvacuationDevice);
            this.AddExecutor(ExecutorType.Activate, CardId.Linkuri, this.Linkuri_eff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Linkuri, this.Linkuri_ss);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Unicorn, this.Unicorn_ss);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Linkspi);

            // ex_monster act
            this.AddExecutor(ExecutorType.Activate, CardId.Beelze);
            this.AddExecutor(ExecutorType.Activate, CardId.Missus, this.Missus_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Crystal, this.Crystal_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Phoneix, this.Phoneix_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Unicorn, this.Unicorn_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Snake, this.Snake_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Borrel, this.Borrel_eff);

            // normal act
            this.AddExecutor(ExecutorType.Activate, CardId.Trans);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BF, this.BF_pos);
            this.AddExecutor(ExecutorType.Activate, CardId.BF, this.BF_pos);
            this.AddExecutor(ExecutorType.Activate, CardId.Sheep, this.Sheep_Act);
            this.AddExecutor(ExecutorType.Activate, CardId.Eater, this.Eater_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.LockBird, this.LockBird_act);

            // ts
            this.AddExecutor(ExecutorType.Activate, CardId.Pink, this.Pink_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Re, this.Reincarnation);
            this.AddExecutor(ExecutorType.Activate, CardId.Red, this.Red_ss);
            this.AddExecutor(ExecutorType.Activate, CardId.Yellow, this.Yellow_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.White, this.White_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Crown, this.Crown_eff);
            this.AddExecutor(ExecutorType.Summon, CardId.Yellow, this.Yellow_sum);
            this.AddExecutor(ExecutorType.Summon, CardId.Red, this.Red_sum);
            this.AddExecutor(ExecutorType.Summon, CardId.Pink, this.Pink_sum);

            // normal
            this.AddExecutor(ExecutorType.SpSummon, CardId.Eater, this.Eater_ss);
            this.AddExecutor(ExecutorType.Summon, CardId.Tuner, this.Tuner_ns);
            this.AddExecutor(ExecutorType.Summon, CardId.Urara, this.Tuner_ns);
            this.AddExecutor(ExecutorType.Summon, CardId.Ghost, this.Tuner_ns);
            this.AddExecutor(ExecutorType.Activate, CardId.Pot, this.Pot_Act);
            this.AddExecutor(ExecutorType.Repos, this.MonsterRepos);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.Red);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.Pink);
            this.AddExecutor(ExecutorType.SpellSet, this.SpellSet);
        }

        public bool Five_Rainbow()
        {
            if (this.Enemy.HasInSpellZone(19619755,true) || this.Bot.HasInSpellZone(19619755, true))
            {
                if (this.Card.HasType(CardType.Field))
                {
                    return false;
                }

                bool has_setcard = false;
                for (int i = 0; i < 5; ++i)
                {
                    ClientCard sp = this.Bot.SpellZone[i];
                    if (sp != null && sp.HasPosition(CardPosition.FaceDown))
                    {
                        has_setcard = true;
                        break;
                    }
                }
                if (has_setcard)
                {
                    return false;
                }

                this.AI.SelectPlace(this.SelectSTPlace());
                return true;
            }
            return false;
        }

        public int SelectSTPlace()
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
            foreach(int seq in list)
            {
                int zone = (int)System.Math.Pow(2, seq);
                if (this.Bot.SpellZone[seq] == null)
                {
                    return zone;
                }
            }
            return 0;
        }

        public bool SpellSet()
        {
            if (this.Card.IsCode(CardId.Sheep) && this.Bot.HasInSpellZone(CardId.Sheep))
            {
                return false;
            }

            if (this.DefaultSpellSet())
            {
                this.AI.SelectPlace(this.SelectSTPlace());
                return true;
            } else if (this.Enemy.HasInSpellZone(58921041,true) || this.Bot.HasInSpellZone(58921041, true))
            {
                if (this.Card.IsCode(CardId.Stage))
                {
                    return !this.Bot.HasInSpellZone(CardId.Stage);
                }

                if (this.Card.IsSpell())
                {
                    this.AI.SelectPlace(this.SelectSTPlace());
                    return true;
                }
            }
            return false;
        }

        public bool IsTrickstar(int id)
        {
            return (id == CardId.Yellow || id == CardId.Red || id == CardId.Pink || id == CardId.White || id == CardId.Stage || id == CardId.Re || id == CardId.Crown);
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

        public bool spell_trap_activate()
        {
            if (this.Card.Location != CardLocation.SpellZone && this.Card.Location != CardLocation.Hand)
            {
                return true;
            }

            if (this.Enemy.HasInMonstersZone(CardId.Exterio,true) && !this.Bot.HasInHandOrHasInMonstersZone(CardId.Ghost))
            {
                return false;
            }

            if (this.Card.IsSpell())
            {
                if (this.Enemy.HasInMonstersZone(33198837, true) && !this.Bot.HasInHandOrHasInMonstersZone(CardId.Ghost))
                {
                    return false;
                }

                if (this.Enemy.HasInSpellZone(61740673, true) || this.Bot.HasInSpellZone(61740673,true))
                {
                    return false;
                }

                if (this.Enemy.HasInMonstersZone(37267041, true) || this.Bot.HasInMonstersZone(37267041, true))
                {
                    return false;
                }

                return true;
            }
            if (this.Card.IsTrap())
            {
                if (this.Enemy.HasInSpellZone(51452091, true) || this.Bot.HasInSpellZone(51452091, true))
                {
                    return false;
                }

                if (this.Enemy.HasInSpellZone(51452091, true) || this.Bot.HasInSpellZone(51452091, true))
                {
                    return false;
                }

                return true;
            }
            // how to get here?
            return false;
        }

        public int[] Useless_List()
        {
            return new[]
            {
                CardId.Tuner,
                CardId.Awaken,
                CardId.Crown,
                CardId.Pink,
                CardId.Pot,
                CardId.BF,
                CardId.White,
                CardId.Trans,
                CardId.Galaxy,
                CardId.Feather,
                CardId.Sheep,
                CardId.Re,
                CardId.Red,
                CardId.Yellow,
                CardId.Eater,
                CardId.MG,
                CardId.Ghost,
                CardId.Urara,
                CardId.Stage,
                CardId.Ring,
                CardId.Warn,
                CardId.Strike
            };
        }

        public int GetTotalATK(IList<ClientCard> list)
        {
            int atk = 0;
            foreach(ClientCard c in list)
            {
                if (c == null)
                {
                    continue;
                }

                atk += c.Attack;
            }
            return atk;
        }

        public bool Awaken_ss()
        {
            // judge whether can ss from exdeck
            bool judge = (this.Bot.ExtraDeck.Count > 0);
            if (this.Enemy.GetMonstersExtraZoneCount() > 1)
            {
                judge = false; // exlink
            }

            if (this.Bot.GetMonstersExtraZoneCount() >= 1)
            {
                foreach (ClientCard card in this.Bot.GetMonstersInExtraZone())
                {
                    if (this.getLinkMarker(card.Id) == 5)
                    {
                        judge = false;
                    }
                }
            }
            // can ss from exdeck
            if (judge)
            {
                bool fornextss = this.Util.ChainContainsCard(CardId.Awaken);
                IList<ClientCard> ex = this.Bot.ExtraDeck;
                ClientCard ex_best = null;
                foreach (ClientCard ex_card in ex)
                {
                    if (!fornextss)
                    {
                        if (this.Bot.HasInExtra(CardId.Exterio))
                        {
                            bool has_skystriker = false;
                            foreach (ClientCard card in this.Enemy.Graveyard)
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
                                this.AI.SelectCard(CardId.Exterio);
                                return true;
                            } else
                            {
                                if (ex_best == null || ex_card.Attack > ex_best.Attack)
                                {
                                    ex_best = ex_card;
                                }
                            }
                        }
                        else
                        {
                            if (ex_best == null || ex_card.Attack > ex_best.Attack)
                            {
                                ex_best = ex_card;
                            }
                        }
                    } 
                    else
                    {
                        if (this.getLinkMarker(ex_card.Id) != 5 && (ex_best == null || ex_card.Attack > ex_best.Attack))
                        {
                            ex_best = ex_card;
                        }
                    }
                }
                if (ex_best != null)
                {
                    this.AI.SelectCard(ex_best);
                }
            }
            if (!judge || this.Util.ChainContainsCard(CardId.Awaken))
            {
                // cannot ss from exdeck or have more than 1 grass in chain
                int[] secondselect = new[]
                {
                    CardId.Borrel,
                    CardId.Ultimate,
                    CardId.Abyss,
                    CardId.Cardian,
                    CardId.Exterio,
                    CardId.Ghost,
                    CardId.White,
                    CardId.Red,
                    CardId.Yellow,
                    CardId.Pink
                };
                if (!this.Util.ChainContainsCard(CardId.Awaken))
                {
                    if (!judge && this.Bot.GetRemainingCount(CardId.Ghost, 2) > 0)
                    {
                        this.AI.SelectCard(CardId.Ghost);
                        this.AI.SelectPosition(CardPosition.FaceUpDefence);
                    }
                    else
                    {
                        this.AI.SelectCard(secondselect);
                    }
                }
                else
                {
                    if (!judge)
                    {
                        this.AI.SelectCard(secondselect);
                    }

                    this.AI.SelectNextCard(secondselect);
                    this.AI.SelectThirdCard(secondselect);
                }
            }
            return true;
        }

        public bool Abyss_eff()
        {
            // tuner ss
            if (this.ActivateDescription == -1)
            {
                this.AI.SelectCard(CardId.Ghost, CardId.TG, CardId.Tuner, CardId.Urara, CardId.BF);
                return true;
            };
            // counter
            if (!this.Enemy.HasInMonstersZone(CardId.Ghost) || this.Enemy.GetHandCount() <= 1)
            {
                ClientCard tosolve = this.Util.GetProblematicEnemyCard();
                if (tosolve == null)
                {
                    if (this.Duel.LastChainPlayer == 1 && this.Util.GetLastChainCard() != null)
                    {
                        ClientCard target = this.Util.GetLastChainCard();
                        if (target.HasPosition(CardPosition.FaceUp) && (target.Location == CardLocation.MonsterZone || target.Location == CardLocation.SpellZone))
                        {
                            tosolve = target;
                        }
                    }
                }
                if (tosolve != null)
                {
                    this.AI.SelectCard(tosolve);
                    return true;
                }
            }
            return false;
        }

        public void RandomSort(List<ClientCard> list) {

            int n = list.Count;
            while(n-- > 1)
            {
                int index = Program._rand.Next(n + 1);
                ClientCard temp = list[index];
                list[index] = list[n];
                list[n] = temp;
            }
        }

        public bool Stage_Lock()
        {
            if (this.Card.Location != CardLocation.SpellZone)
            {
                return false;
            }

            List<ClientCard> spells = this.Enemy.GetSpells();
            this.RandomSort(spells);
            if (spells.Count == 0)
            {
                return false;
            }

            foreach (ClientCard card in spells)
            {
                if (card.IsFacedown())
                {
                    this.AI.SelectCard(card);
                    this.stage_locked = card;
                    return true;
                }
            }
            return false;
        }

        public bool GalaxyCyclone()
        {
            if (!this.Bot.HasInSpellZone(CardId.Stage))
            {
                this.stage_locked = null;
            }

            List<ClientCard> spells = this.Enemy.GetSpells();
            if (spells.Count == 0)
            {
                return false;
            }

            ClientCard selected = null;
            if (this.Card.Location == CardLocation.Grave)
            {
                selected = this.Util.GetBestEnemySpell(true);
            }
            else
            {
                if (!this.spell_trap_activate())
                {
                    return false;
                }

                foreach (ClientCard card in spells)
                {
                    if (card.IsFacedown() && card != this.stage_locked)
                    {
                        selected = card;
                        break;
                    }
                }
            }
            if (selected == null)
            {
                return false;
            }

            this.AI.SelectCard(selected);
            this.AI.SelectPlace(this.SelectSTPlace());
            return true;
        }

        public bool BF_pos()
        {
            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }

        public bool Feather_Act()
        {
            if (!this.spell_trap_activate())
            {
                return false;
            }

            if (this.Util.GetProblematicEnemySpell() != null)
            {
                List<ClientCard> grave = this.Bot.GetGraveyardSpells();
                foreach (ClientCard self_card in grave)
                {
                    if (self_card.IsCode(CardId.Galaxy))
                    {
                        return false;
                    }
                }
                this.AI.SelectPlace(this.SelectSTPlace());
                return true;
            }
            // activate when more than 2 cards
            if (this.Enemy.GetSpellCount() <= 1)
            {
                return false;
            }

            this.AI.SelectPlace(this.SelectSTPlace());
            return true;
        }

        public bool Sheep_Act()
        {
            if (!this.spell_trap_activate())
            {
                return false;
            }

            if (this.Duel.Player == 0)
            {
                return false;
            }

            if (this.Duel.Phase == DuelPhase.End)
            {
                return true;
            }

            if (this.Duel.LastChainPlayer == 1 && (this.Util.IsChainTarget(this.Card) || (this.Util.GetLastChainCard().IsCode(CardId.Feather) && !this.Bot.HasInSpellZone(CardId.Awaken))))
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

        public bool Stage_act()
        {
            if (this.Card.Location == CardLocation.SpellZone && this.Card.HasPosition(CardPosition.FaceUp))
            {
                return false;
            }

            if (!this.spell_trap_activate())
            {
                return false;
            }

            if (!this.NormalSummoned)
            {
                if (!this.Bot.HasInHand(CardId.Yellow))
                {
                    this.AI.SelectCard(CardId.Yellow, CardId.Pink, CardId.Red, CardId.White);
                    this.stage_locked = null;
                    return true;
                }
                else if (this.Enemy.LifePoints <= 1000 && this.Bot.GetRemainingCount(CardId.Pink,1) > 0)
                {
                    this.AI.SelectCard(CardId.Pink, CardId.Yellow, CardId.Red, CardId.White);
                    this.stage_locked = null;
                    return true;
                }
                else if (this.Bot.HasInHand(CardId.Yellow) && !this.Bot.HasInHand(CardId.Red))
                {
                    this.AI.SelectCard(CardId.Red, CardId.Pink, CardId.Yellow, CardId.White);
                    this.stage_locked = null;
                    return true;
                }
                else if (this.Enemy.GetMonsterCount() > 0 && this.Util.GetBestEnemyMonster().Attack >= this.Util.GetBestAttack(this.Bot))
                {
                    this.AI.SelectCard(CardId.White, CardId.Yellow, CardId.Pink, CardId.Red);
                    this.stage_locked = null;
                    return true;
                }
                else if (!this.Bot.HasInSpellZone(CardId.Stage))
                {
                    this.AI.SelectCard(CardId.Yellow, CardId.Pink, CardId.Red, CardId.White);
                    this.stage_locked = null;
                    return true;
                }
                return false;
            }

            if (this.NormalSummoned)
            {
                if (this.Enemy.LifePoints <= 1000 && !this.pink_ss && this.Bot.GetRemainingCount(CardId.Pink,1) > 0)
                {
                    this.AI.SelectCard(CardId.Pink, CardId.Yellow, CardId.Red, CardId.White);
                    this.stage_locked = null;
                    return true;
                }
                if (this.Enemy.GetMonsterCount() > 0 && this.Util.GetBestEnemyMonster().Attack >= this.Util.GetBestAttack(this.Bot) && !this.Bot.HasInHand(CardId.White))
                {
                    this.AI.SelectCard(CardId.White, CardId.Yellow, CardId.Pink, CardId.Red);
                    this.stage_locked = null;
                    return true;
                }
                if (this.Bot.HasInMonstersZone(CardId.Yellow) && !this.Bot.HasInHand(CardId.Red))
                {
                    this.AI.SelectCard(CardId.Red, CardId.Pink, CardId.Yellow, CardId.White);
                    this.stage_locked = null;
                    return true;
                }
                this.AI.SelectCard(CardId.Yellow, CardId.Pink, CardId.Red, CardId.White);
                this.stage_locked = null;
                return true;
            }
            this.stage_locked = null;
            return false;
        }

        public bool Pot_Act()
        {
            if (!this.spell_trap_activate())
            {
                return false;
            }

            if (this.Bot.Deck.Count > 15)
            {
                this.AI.SelectPlace(this.SelectSTPlace());
                return true;
            }
            return false;
        }

        public bool Hand_act_eff()
        {
            if (this.GraveCall_count > 0 && this.GraveCall_id == this.Card.Id)
            {
                return false;
            }

            if (this.Card.IsCode(CardId.Urara) && this.Util.GetLastChainCard().HasSetcode(0x11e) && this.Util.GetLastChainCard().Location == CardLocation.Hand) // Danger! archtype hand effect
            {
                return false;
            }

            if (this.Card.IsCode(CardId.Urara) && this.Bot.HasInHand(CardId.LockBird) && this.Bot.HasInSpellZone(CardId.Re))
            {
                return false;
            }

            if (this.Card.IsCode(CardId.Ghost) && this.Card.Location == CardLocation.Hand && this.Bot.HasInMonstersZone(CardId.Ghost))
            {
                return false;
            }

            return (this.Duel.LastChainPlayer == 1);
        }

        public bool Exterio_counter()
        {
            if (this.Duel.LastChainPlayer == 1)
            {
                this.AI.SelectCard(this.Useless_List());
                return true;
            }
            return false;
        }

        public bool G_act()
        {
            return (this.Duel.Player == 1 && !(this.GraveCall_count > 0 && this.GraveCall_id == this.Card.Id));
        }

        public bool Pink_eff()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                if ((this.Enemy.LifePoints <= 1000 && this.Bot.HasInSpellZone(CardId.Stage))
                || this.Enemy.LifePoints <= 800
                || (!this.NormalSummoned && this.Bot.HasInGraveyard(CardId.Red))
                )
                {
                    this.pink_ss = true;
                    return true;
                }
                else if (this.Enemy.GetMonsterCount() > 0 && (this.Util.GetBestEnemyMonster().Attack - 800 >= this.Bot.LifePoints))
                {
                    return false;
                }

                this.pink_ss = true;
                return true;
            }
            else if (this.Card.Location == CardLocation.Onfield)
            {
                if (!this.NormalSummoned && this.Bot.HasInGraveyard(CardId.Yellow))
                {
                    this.AI.SelectCard(CardId.Yellow, CardId.Red, CardId.White);
                    return true;
                } else
                {
                    this.AI.SelectCard(CardId.Red, CardId.Yellow, CardId.White);
                    return true;
                }
            }
            return true;
        }

        public bool Eater_ss()
        {
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
            if (this.Bot.SpellZone[5] != null && !this.Bot.SpellZone[5].IsCode(CardId.Stage))
            {
                targets.Add(this.Bot.SpellZone[5]);
            }
            if (this.Bot.SpellZone[5] != null && this.Bot.SpellZone[5].IsCode(CardId.Stage) && this.Bot.HasInHand(CardId.Stage))
            {
                targets.Add(this.Bot.SpellZone[5]);
            }
            foreach (ClientCard e_c in this.Bot.ExtraDeck)
            {
                targets.Add(e_c);
                if (targets.Count >= 5)
                {
                    this.AI.SelectCard(targets);
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

        public bool Eater_eff()
        {
            if (this.Enemy.BattlingMonster.HasPosition(CardPosition.Attack) && (this.Bot.BattlingMonster.Attack - this.Enemy.BattlingMonster.GetDefensePower() >= this.Enemy.LifePoints))
            {
                return false;
            }

            return true;
        }

        public void Red_SelectPos(ClientCard return_card = null)
        {
            int self_power = (this.Bot.HasInHand(CardId.White) && !this.white_eff_used) ? 3200 : 1600;
            if (this.Duel.Player == 0)
            {
                List<ClientCard> monster_list = this.Bot.GetMonsters();
                monster_list.Sort(CardContainer.CompareCardAttack);
                monster_list.Reverse();
                foreach(ClientCard card in monster_list)
                {
                    if (this.IsTrickstar(card.Id) && card != return_card && card.HasPosition(CardPosition.Attack))
                    {
                        int this_power = (this.Bot.HasInHand(CardId.White) && !this.white_eff_used) ? (card.RealPower + card.Attack) : card.RealPower;
                        if (this_power >= self_power)
                        {
                            self_power = this_power;
                        }
                    } else if (card.RealPower >= self_power)
                    {
                        self_power = card.RealPower;
                    }
                }
            }
            ClientCard bestenemy = this.Util.GetOneEnemyBetterThanValue(self_power, true);
            if (bestenemy != null)
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
            }
            else
            {
                this.AI.SelectPosition(CardPosition.FaceUpAttack);
            }

            return;
        }

        public bool Red_ss()
        {
            if (this.red_ss_count >= 6)
            {
                return false;
            }

            if ((this.Util.ChainContainsCard(CardId.DarkHole) || this.Util.ChainContainsCard(99330325) || this.Util.ChainContainsCard(53582587)) && this.Util.ChainContainsCard(CardId.Red))
            {
                return false;
            }

            if (this.Duel.LastChainPlayer == 0 && this.Util.GetLastChainCard().IsCode(CardId.Red))
            {
                foreach (ClientCard m in this.Bot.GetMonsters())
                {
                    if (this.Util.IsChainTarget(m) && this.IsTrickstar(m.Id))
                    {
                        this.red_ss_count += 1;
                        this.AI.SelectCard(m);
                        this.Red_SelectPos();
                        return true;
                    }
                }
            }
            if (this.Duel.LastChainPlayer == 1)
            {
                return true;
            }

            if (this.Duel.Player == 0)
            {
                if (this.Util.IsTurn1OrMain2())
                {
                    return false;
                }

                if (this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2)
                {
                    List<ClientCard> self_m = this.Bot.GetMonsters();
                    ClientCard tosolve_enemy = this.Util.GetOneEnemyBetterThanMyBest();
                    foreach (ClientCard c in self_m)
                    {
                        if (this.IsTrickstar(c.Id) && !c.IsCode(CardId.Red))
                        {
                            if (c.Attacked)
                            {
                                this.AI.SelectCard(c);
                                this.Red_SelectPos(c);
                                this.red_ss_count += 1;
                                return true;
                            }
                            if (c.IsCode(CardId.Pink))
                            {
                                return false;
                            }

                            if (tosolve_enemy != null)
                            {
                                if (this.Bot.HasInHand(CardId.White) && c.Attack + c.BaseAttack < tosolve_enemy.Attack)
                                {
                                    if (tosolve_enemy.Attack > 3200)
                                    {
                                        this.AI.SelectPosition(CardPosition.FaceUpDefence);
                                    }

                                    this.AI.SelectCard(c);
                                    this.Red_SelectPos(c);
                                    this.red_ss_count += 1;
                                    return true;
                                }
                                if (!this.Bot.HasInHand(CardId.White) && tosolve_enemy.Attack <= 3200 && c.IsCode(CardId.White))
                                {
                                    this.AI.SelectCard(c);
                                    this.Red_SelectPos(c);
                                    this.red_ss_count += 1;
                                    return true;
                                }
                                if (!this.Bot.HasInHand(CardId.White) && c.Attack < tosolve_enemy.Attack)
                                {
                                    if (!c.Attacked)
                                    {
                                        ClientCard badatk = this.Enemy.GetMonsters().GetLowestAttackMonster();
                                        ClientCard baddef = this.Enemy.GetMonsters().GetLowestDefenseMonster();
                                        int enemy_power = 99999;
                                        if (badatk != null && badatk.Attack <= enemy_power)
                                        {
                                            enemy_power = badatk.Attack;
                                        }

                                        if (baddef != null && baddef.Defense <= enemy_power)
                                        {
                                            enemy_power = baddef.Defense;
                                        }

                                        if (c.Attack > enemy_power)
                                        {
                                            return false;
                                        }
                                    }
                                    if (tosolve_enemy.Attack > 1600)
                                    {
                                        this.AI.SelectPosition(CardPosition.FaceUpDefence);
                                    }

                                    this.AI.SelectCard(c);
                                    this.Red_SelectPos(c);
                                    this.red_ss_count += 1;
                                    return true;
                                }
                            }
                        }
                    }
                }
            } else
            {
                if (this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2)
                {
                    if (this.Util.GetOneEnemyBetterThanMyBest() != null)
                    {
                        List<ClientCard> self_monster = this.Bot.GetMonsters();
                        self_monster.Sort(CardContainer.CompareDefensePower);
                        foreach(ClientCard card in self_monster)
                        {
                            if (this.IsTrickstar(card.Id) && !card.IsCode(CardId.Red))
                            {
                                this.AI.SelectCard(card);
                                this.Red_SelectPos(card);
                                this.red_ss_count += 1;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool Yellow_eff()
        {
            if (!this.Bot.HasInHand(CardId.Stage) && !this.Bot.HasInSpellZone(CardId.Stage) && this.Bot.GetRemainingCount(CardId.Stage, 3) > 0)
            {
                this.AI.SelectCard(CardId.Stage, CardId.Red, CardId.White, CardId.Pink, CardId.Re, CardId.Crown, CardId.Yellow);
                return true;
            }
            if (this.Enemy.LifePoints <= 1000)
            {
                if (this.Bot.GetRemainingCount(CardId.Pink, 1) > 0 && !this.pink_ss)
                {
                    this.AI.SelectCard(CardId.Pink, CardId.Stage, CardId.Red, CardId.White, CardId.Re, CardId.Crown, CardId.Yellow);
                    return true;
                }
                else if (this.Bot.HasInGraveyard(CardId.Pink) && this.Bot.GetRemainingCount(CardId.Crown, 1) > 0)
                {
                    this.AI.SelectCard(CardId.Crown, CardId.Pink, CardId.Re, CardId.Stage, CardId.Red, CardId.White, CardId.Yellow);
                    return true;
                }
            }
            if (this.Enemy.GetMonsterCount() == 0 && !this.Util.IsTurn1OrMain2())
            {
                if (this.Bot.HasInGraveyard(CardId.Red) && this.Bot.GetRemainingCount(CardId.Pink, 1) > 0 && !this.pink_ss)
                {
                    this.AI.SelectCard(CardId.Pink, CardId.Red, CardId.White, CardId.Re, CardId.Stage, CardId.Crown, CardId.Yellow);
                }
                else if (this.Bot.HasInGraveyard(CardId.Pink) && this.Bot.HasInGraveyard(CardId.Red) && this.Bot.GetRemainingCount(CardId.Ring, 1) > 0)
                {
                    this.AI.SelectCard(CardId.Crown, CardId.Red, CardId.White, CardId.Re, CardId.Stage, CardId.Pink, CardId.Yellow);
                }
                else if (this.Bot.GetRemainingCount(CardId.White, 2) > 0 && this.Enemy.LifePoints <= 4000)
                {
                    this.AI.SelectCard(CardId.White, CardId.Red, CardId.Pink, CardId.Re, CardId.Stage, CardId.Crown, CardId.Yellow);
                }
                else if (this.Bot.HasInGraveyard(CardId.White) && this.Bot.GetRemainingCount(CardId.Crown, 1) > 0)
                {
                    this.AI.SelectCard(CardId.Crown, CardId.Red, CardId.Pink, CardId.Re, CardId.Stage, CardId.White, CardId.Yellow);
                }
                else
                {
                    this.AI.SelectCard(CardId.Red, CardId.Pink, CardId.Re, CardId.Crown, CardId.Stage, CardId.White, CardId.Yellow);
                }
                return true;
            }
            if (this.Util.GetProblematicEnemyMonster() != null)
            {
                int power = this.Util.GetProblematicEnemyMonster().GetDefensePower();
                if (power >= 1800 && power <= 3600 && this.Bot.GetRemainingCount(CardId.White, 2) > 0 && !this.Bot.HasInHand(CardId.White))
                {
                    this.AI.SelectCard(CardId.White, CardId.Red, CardId.Pink, CardId.Re, CardId.Stage, CardId.Crown, CardId.Yellow);
                }
                else
                {
                    this.AI.SelectCard(CardId.Red, CardId.Pink, CardId.Re, CardId.Crown, CardId.Stage, CardId.White, CardId.Yellow);
                }
                return true;
            }
            if ((this.Bot.HasInHand(CardId.Red) || this.Bot.HasInHand(CardId.Stage) || this.Bot.HasInHand(CardId.Yellow)) && this.Bot.GetRemainingCount(CardId.Re,1) > 0) {
                this.AI.SelectCard(CardId.Re, CardId.Red, CardId.White, CardId.Crown, CardId.Pink, CardId.Stage, CardId.Yellow);
                return true;
            }
            this.AI.SelectCard(CardId.Red, CardId.Pink, CardId.Re, CardId.Crown, CardId.Stage, CardId.White, CardId.Yellow);
            return true;
        }

        public bool White_eff()
        {
            if (this.Duel.Phase >= DuelPhase.Main2)
            {
                return false;
            }

            if (this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2)
            {
                if (this.Bot.BattlingMonster == null || this.Enemy.BattlingMonster == null || !this.IsTrickstar(this.Bot.BattlingMonster.Id) || this.Bot.BattlingMonster.HasPosition(CardPosition.Defence))
                {
                    return false;
                }

                if (this.Bot.BattlingMonster.Attack <= this.Enemy.BattlingMonster.RealPower && this.Bot.BattlingMonster.Attack + this.Bot.BattlingMonster.BaseAttack >= this.Enemy.BattlingMonster.RealPower)
                {
                    this.white_eff_used = true;
                    return true;
                }
                return false; 
            } else
            {
                if (this.Enemy.GetMonsterCount() == 0 && !this.Util.IsTurn1OrMain2()) {
                    this.white_eff_used = true;
                    return true;
                }
                else if (this.Enemy.GetMonsterCount() != 0)
                {
                    ClientCard tosolve = this.Util.GetBestEnemyMonster(true);
                    ClientCard self_card = this.Bot.GetMonsters().GetHighestAttackMonster();
                    if (tosolve == null || self_card == null || (tosolve != null && self_card != null && !this.IsTrickstar(self_card.Id)))
                    {
                        if (this.Enemy.GetMonsters().GetHighestAttackMonster()== null ||
                            this.Enemy.GetMonsters().GetHighestDefenseMonster() == null ||
                            this.Enemy.GetMonsters().GetHighestAttackMonster().GetDefensePower() < 2000 ||
                            this.Enemy.GetMonsters().GetHighestDefenseMonster().GetDefensePower() < 2000)
                        {
                            this.white_eff_used = true;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (tosolve != null && self_card != null && this.IsTrickstar(self_card.Id) && !tosolve.IsMonsterHasPreventActivationEffectInBattle())
                    {
                        int defender_power = tosolve.GetDefensePower();
                        Logger.DebugWriteLine("battle check 0:" + this.Duel.Phase.ToString());
                        Logger.DebugWriteLine("battle check 1:" + self_card.Attack.ToString());
                        Logger.DebugWriteLine("battle check 2:" + (self_card.Attack+self_card.BaseAttack).ToString());
                        Logger.DebugWriteLine("battle check 3:" + defender_power.ToString());
                        if (self_card.Attack <= defender_power && self_card.Attack + self_card.BaseAttack >= defender_power)
                        {
                            return false;
                        }
                        else if (defender_power <= 2000)
                        {
                            this.white_eff_used = true;
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public bool LockBird_act()
        {
            if (this.Duel.Player == 0 || this.lockbird_used)
            {
                return false;
            }

            this.lockbird_useful = true;
            if (this.Bot.HasInSpellZone(CardId.Re))
            {
                if (this.Util.ChainContainsCard(CardId.Re))
                {
                    this.lockbird_used = true;
                }

                return this.Util.ChainContainsCard(CardId.Re);
            }
            this.lockbird_used = true;
            return true;
        }

        public bool Reincarnation()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                return this.Ts_reborn();
            }

            if (!this.spell_trap_activate())
            {
                return false;
            }

            if (this.Bot.HasInHand(CardId.LockBird))
            {
                if (this.lockbird_useful || this.Util.IsChainTarget(this.Card) || (this.Duel.Player == 1 && this.Util.ChainContainsCard(CardId.Feather))) {
                    this.lockbird_useful = false;
                    return true;
                }
                return false;
            }
            return true;
        }

        public bool Crown_eff()
        {
            if (this.Card.Location == CardLocation.Hand || (this.Card.Location == CardLocation.SpellZone && this.Card.HasPosition(CardPosition.FaceDown)))
            {
                if (!this.spell_trap_activate())
                {
                    return false;
                }

                if (this.Duel.Phase <= DuelPhase.Main1 && this.Ts_reborn())
                {
                    this.AI.SelectPlace(this.SelectSTPlace());
                    return true;
                }
                return false;
            }
            if (this.Bot.HasInHand(CardId.Pink) && this.GraveCall_id != CardId.Pink)
            {
                this.AI.SelectCard(CardId.Pink);
                return true;
            }
            if (this.Enemy.GetMonsterCount() == 0)
            {
                foreach(ClientCard hand in this.Bot.Hand)
                {
                    if (hand.IsMonster() && this.IsTrickstar(hand.Id))
                    {
                        if (hand.Attack >= this.Enemy.LifePoints)
                        {
                            return true;
                        }

                        if (!hand.IsCode(CardId.Yellow))
                        {
                            if (this.Util.GetOneEnemyBetterThanValue(hand.Attack, false) == null)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool Ts_reborn()
        {
            if (this.Util.IsTurn1OrMain2())
            {
                return false;
            }

            if (this.Duel.Player == 0 && this.Enemy.LifePoints <= 1000)
            {
                this.AI.SelectCard(CardId.Pink);
                return true;
            }
            bool can_summon = (this.Duel.Player == 0 && this.NormalSummoned);
            if (can_summon)
            {
                if ((this.Duel.Phase < DuelPhase.Main2) && this.Bot.HasInGraveyard(CardId.Pink))
                {
                    this.AI.SelectCard(CardId.Pink);
                    return true;
                }
                else
                {
                    this.AI.SelectCard(CardId.Red, CardId.White, CardId.Yellow, CardId.Pink);
                    return true;
                }
            }
            else
            {

                this.AI.SelectCard(CardId.Red, CardId.White, CardId.Yellow, CardId.Pink);
                return true;
            }
        }

        public bool Yellow_sum()
        {
            this.NormalSummoned = true;
            return true;
        }

        public bool Red_sum()
        {
            if ((this.Enemy.GetMonsterCount() == 0 && this.Enemy.LifePoints <= 1800) || (this.Duel.Turn == 1 && this.Bot.HasInHand(CardId.Re)))
            {
                this.NormalSummoned = true;
                return true;
            }
            return false;
        }

        public bool Pink_sum()
        {
            if (this.Enemy.LifePoints <= 1000)
            {
                this.NormalSummoned = true;
                return true;
            }
            else if (!this.Util.IsTurn1OrMain2() && (this.Bot.HasInGraveyard(CardId.Yellow) || this.Bot.HasInGraveyard(CardId.Red)))
            {
                this.NormalSummoned = true;
                return true;
            }
            return false;
        }

        public bool Tuner_ns()
        {
            if ((this.Card.IsCode(CardId.Tuner) && this.Bot.HasInExtra(CardId.Crystal) && !this.tuner_eff_used) || this.Tuner_ss())
            {
                this.NormalSummoned = true;
                return true;
            }
            return false;
        }

        public bool Tuner_ss()
        {
            if (this.crystal_eff_used || this.Bot.HasInMonstersZone(CardId.Crystal))
            {
                return false;
            }

            if (this.Bot.GetMonsterCount() == 0 || !this.Bot.HasInExtra(CardId.Crystal))
            {
                return false;
            }

            if (this.Card.IsCode(CardId.Ghost) && this.Bot.GetRemainingCount(CardId.Ghost, 2) <= 0)
            {
                return false;
            }

            int count = 0;
            if (!this.Card.IsCode(CardId.Urara))
            {
                count += 1;
            }

            foreach (ClientCard hand in this.Bot.Hand)
            {
                if (hand.IsCode(this.Card.Id))
                {
                    count += 1;
                }
            }
            if (count < 2)
            {
                return false;
            }

            foreach (ClientCard m in this.Bot.GetMonsters())
            {
                if (!m.IsCode(CardId.Eater) && this.getLinkMarker(m.Id) <= 2)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Tuner_eff()
        {
            this.tuner_eff_used = true;
            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }

        public bool Ring_act()
        {
            if (this.Duel.LastChainPlayer == 0 && this.Util.GetLastChainCard() != null && this.Util.GetLastChainCard().IsCode(CardId.Ghost))
            {
                return false;
            }

            if (!this.spell_trap_activate())
            {
                return false;
            }

            ClientCard target = this.Util.GetProblematicEnemyMonster();
            if (target == null && this.Util.IsChainTarget(this.Card))
            {
                target = this.Util.GetBestEnemyMonster();
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

        public bool Linkuri_ss()
        {
            foreach(ClientCard c in this.Bot.GetMonsters())
            {
                if (!c.IsCode(CardId.Eater, CardId.Linkuri, CardId.Linkspi) && c.Level == 1)
                {
                    this.AI.SelectCard(c);
                    return true;
                }
            }
            return false;
        }

        public bool Linkuri_eff()
        {
            if (this.Duel.LastChainPlayer == 0 && this.Util.GetLastChainCard().IsCode(CardId.Linkuri))
            {
                return false;
            }

            this.AI.SelectCard(CardId.Tuner, CardId.BF + 1);
            return true;
        }

        public bool Crystal_ss()
        {
            if (this.crystal_eff_used)
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.BF) && this.Bot.HasInMonstersZone(CardId.BF + 1))
            {
                this.AI.SelectCard(CardId.BF, CardId.BF + 1);
                return true;
            }
            foreach(ClientCard extra_card in this.Bot.GetMonstersInExtraZone())
            {
                if (this.getLinkMarker(extra_card.Id) >= 5)
                {
                    return false;
                }
            }
            IList<ClientCard> targets = new List<ClientCard>();
            foreach(ClientCard t_check in this.Bot.GetMonsters())
            {
                if (t_check.IsFacedown())
                {
                    continue;
                }

                if (t_check.IsCode(CardId.BF, CardId.Tuner, CardId.Urara, CardId.Ghost))
                {
                    targets.Add(t_check);
                    break;
                }
            }
            if (targets.Count == 0)
            {
                return false;
            }

            List<ClientCard> m_list = new List<ClientCard>(this.Bot.GetMonsters());
            m_list.Sort(CardContainer.CompareCardAttack);
            foreach (ClientCard e_check in m_list)
            {
                if (e_check.IsFacedown())
                {
                    continue;
                }

                if (targets[0] != e_check && this.getLinkMarker(e_check.Id) <= 2 && !e_check.IsCode(CardId.Eater, CardId.Crystal))
                {
                    targets.Add(e_check);
                    break;
                }
            }
            if (targets.Count <= 1)
            {
                return false;
            }

            this.AI.SelectMaterials(targets);
            return true;
        }

        public bool Crystal_eff()
        {
            if (this.Duel.Player == 0)
            {
                this.crystal_eff_used = true;
                this.AI.SelectCard(CardId.Tuner, CardId.Ghost, CardId.Urara);
                return true;
            }
            else if (this.Util.IsChainTarget(this.Card) || this.Util.GetProblematicEnemySpell() != null)
            {
                return true;
            }
            else if (this.Duel.Player == 1 && this.Duel.Phase == DuelPhase.BattleStart && this.Util.IsOneEnemyBetterThanValue(1500,true)) {
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

        public bool TG_eff()
        {
            if (this.Card.Location != CardLocation.MonsterZone)
            {
                return true;
            }

            ClientCard target = this.Util.GetProblematicEnemySpell();
            IList<ClientCard> list = new List<ClientCard>();
            if (target != null)
            {
                list.Add(target);
            }

            foreach (ClientCard spells in this.Enemy.GetSpells())
            {
                if (spells != null && !list.Contains(spells))
                {
                    list.Add(spells);
                }
            }
            this.AI.SelectCard(list);
            return true;
        }

        public bool Safedragon_ss()
        {
            if (this.Util.IsTurn1OrMain2())
            {
                return false;
            }

            ClientCard m = this.Util.GetProblematicEnemyMonster();
            foreach(ClientCard ex_m in this.Bot.GetMonstersInExtraZone())
            {
                if (this.getLinkMarker(ex_m.Id) >= 4)
                {
                    return false;
                }
            }
            if ((m == null || m.HasPosition(CardPosition.FaceDown)) && this.Enemy.LifePoints <= 1100)
            {
                if (this.Enemy.GetMonsterCount() == 0 && this.Duel.Phase < DuelPhase.Battle)
                {
                    IList<ClientCard> list = new List<ClientCard>();
                    foreach(ClientCard monster in this.Bot.GetMonsters())
                    {
                        if (this.getLinkMarker(monster.Id) <= 2)
                        {
                            list.Add(monster);
                        }

                        if (list.Count == 2)
                        {
                            break;
                        }
                    }
                    if (list.Count == 2 && this.GetTotalATK(list) <= 1100)
                    {
                        this.AI.SelectMaterials(list);
                        return true;
                    }
                    return false;
                }
            }
            ClientCard ex_1 = this.Bot.MonsterZone[5];
            ClientCard ex_2 = this.Bot.MonsterZone[6];
            ClientCard ex = null;
            if (ex_1 != null && ex_1.Controller == 0)
            {
                ex = ex_1;
            }

            if (ex_2 != null && ex_2.Controller == 0)
            {
                ex = ex_2;
            }

            if (ex == null)
            {
                return false;
            }

            if (!ex.HasLinkMarker(2))
            {
                return false;
            }

            IList<ClientCard> targets = new List<ClientCard>();
            foreach (ClientCard s_m in this.Bot.GetMonsters())
            {
                if (s_m.IsCode(CardId.Eater))
                {
                    continue;
                }

                if (s_m != this.Bot.MonsterZone[5] && s_m != this.Bot.MonsterZone[6])
                {
                    targets.Add(s_m);
                }

                if (targets.Count == 2)
                {
                    break;
                }
            }
            if (targets.Count == 2)
            {
                this.AI.SelectMaterials(targets);
                return true;
            }
            return false;
        }

        public bool Phoneix_ss()
        {
            ClientCard m = this.Util.GetProblematicEnemySpell();
            if (m == null)
            {
                if (this.Enemy.GetMonsterCount() == 0 && this.Enemy.LifePoints <= 1900 && this.Duel.Phase == DuelPhase.Main1) 
                {
                    IList<ClientCard> m_list = new List<ClientCard>();
                    List<ClientCard> list = new List<ClientCard>(this.Bot.GetMonsters());
                    list.Sort(CardContainer.CompareCardAttack);
                    foreach(ClientCard monster in list)
                    {
                        if (this.getLinkMarker(monster.Id) == 1 && monster.IsFaceup())
                        {
                            m_list.Add(monster);
                        }

                        if (m_list.Count == 2)
                        {
                            break;
                        }
                    }
                    if (m_list.Count == 2 && this.GetTotalATK(m_list) <= 1900)
                    {
                        this.AI.SelectMaterials(m_list);
                        return true;
                    }
                }
                return false;
            }
            if (this.Bot.Hand.Count == 0)
            {
                return false;
            }

            IList<ClientCard> targets = new List<ClientCard>();
            List<ClientCard> main_list = new List<ClientCard>(this.Bot.GetMonstersInMainZone());
            main_list.Sort(CardContainer.CompareCardAttack);
            foreach (ClientCard s_m in main_list)
            {
                if (s_m.IsFacedown())
                {
                    continue;
                }

                if ((!s_m.IsCode(CardId.Eater) || (s_m.IsCode(CardId.Eater) && s_m.IsDisabled())) && !targets.ContainsCardWithId(s_m.Id))
                {
                    targets.Add(s_m);
                };
                if (targets.Count == 2)
                {
                    break;
                }
            }
            if (targets.Count < 2)
            {
                foreach (ClientCard s_m in this.Bot.GetMonstersInExtraZone())
                {
                    if (s_m.IsFacedown())
                    {
                        continue;
                    }

                    if (!s_m.IsCode(CardId.Eater) && !targets.ContainsCardWithId(s_m.Id))
                    {
                        targets.Add(s_m);
                    };
                    if (targets.Count == 2)
                    {
                        break;
                    }
                }
            }
            if (targets.Count < 2)
            {
                return false;
            }

            this.AI.SelectMaterials(targets);
            return true;
        }

        public bool Phoneix_eff()
        {
            this.AI.SelectCard(this.Useless_List());
            ClientCard target = this.Util.GetProblematicEnemySpell();
            if (target != null)
            {
                this.AI.SelectNextCard(target);
            } else
            {
                List<ClientCard> spells = this.Enemy.GetSpells();
                this.RandomSort(spells);
                foreach (ClientCard card in spells)
                {
                    if ((card != this.stage_locked || card.HasPosition(CardPosition.FaceUp)) && !(card.IsShouldNotBeTarget() || card.IsShouldNotBeMonsterTarget()))
                    {
                        this.AI.SelectNextCard(card);
                    }
                }
            }
            return true;
        }

        public bool Unicorn_ss() {
            ClientCard m = this.Util.GetProblematicEnemyCard();
            int link_count = 0;
            if (m == null)
            {
                if (this.Enemy.GetMonsterCount() == 0 && this.Enemy.LifePoints <= 2200 && this.Duel.Phase == DuelPhase.Main1)
                {
                    IList<ClientCard> m_list = new List<ClientCard>();
                    List<ClientCard> _sort_list = new List<ClientCard>(this.Bot.GetMonsters());
                    _sort_list.Sort(CardContainer.CompareCardAttack);
                    foreach(ClientCard monster in _sort_list)
                    {
                        if (this.getLinkMarker(monster.Id) == 2)
                        {
                            link_count += 2;
                            m_list.Add(monster);
                        } else if (this.getLinkMarker(monster.Id) == 1 && monster.IsFaceup())
                        {
                            link_count += 1;
                            m_list.Add(monster);
                        }
                        if (link_count >= 3)
                        {
                            break;
                        }
                    }
                    if (link_count >= 3 && this.GetTotalATK(m_list) <= 2200)
                    {
                        this.AI.SelectMaterials(m_list);
                        return true;
                    }
                }
                return false;
            }
            if (this.Bot.Hand.Count == 0)
            {
                return false;
            }

            IList<ClientCard> targets = new List<ClientCard>();
            List<ClientCard> sort_list = this.Bot.GetMonsters();
            sort_list.Sort(CardContainer.CompareCardAttack);
            foreach (ClientCard s_m in sort_list)
            {
                if ((!s_m.IsCode(CardId.Eater) || (s_m.IsCode(CardId.Eater) && m.IsMonsterHasPreventActivationEffectInBattle())) && this.getLinkMarker(s_m.Id) <= 2 && s_m.IsFaceup())
                {
                    if (!targets.ContainsCardWithId(s_m.Id))
                    {
                        targets.Add(s_m);
                        link_count += this.getLinkMarker(s_m.Id);
                    }
                    if (link_count >= 3)
                    {
                        break;
                    }
                }
            }
            if (link_count < 3)
            {
                return false;
            }

            this.AI.SelectMaterials(targets);
            return true;
        }

        public bool Unicorn_eff()
        {
            ClientCard m = this.Util.GetProblematicEnemyCard();
            if (m == null)
            {
                return false;
            }
            // avoid cards that cannot target.
            this.AI.SelectCard(this.Useless_List());
            IList<ClientCard> enemy_list = new List<ClientCard>();
            if (!m.IsShouldNotBeMonsterTarget() && !m.IsShouldNotBeTarget())
            {
                enemy_list.Add(m);
            }

            foreach (ClientCard enemy in this.Enemy.GetMonstersInExtraZone())
            {
                if (enemy != null && !enemy_list.Contains(enemy) && !enemy.IsShouldNotBeMonsterTarget() && !enemy.IsShouldNotBeTarget())
                {
                    enemy_list.Add(enemy);
                }
            }
            foreach (ClientCard enemy in this.Enemy.GetMonstersInMainZone())
            {
                if (enemy != null && !enemy_list.Contains(enemy) && !enemy.IsShouldNotBeMonsterTarget() && !enemy.IsShouldNotBeTarget())
                {
                    enemy_list.Add(enemy);
                }
            }
            foreach (ClientCard enemy in this.Enemy.GetSpells())
            {
                if (enemy != null && !enemy_list.Contains(enemy) && !enemy.IsShouldNotBeMonsterTarget() && !enemy.IsShouldNotBeTarget())
                {
                    enemy_list.Add(enemy);
                }
            }
            if (enemy_list.Count > 0)
            {
                this.AI.SelectNextCard(enemy_list);
                return true;
            } else
            {
                return false;
            }
        }

        public bool Snake_ss()
        {
            IList<ClientCard> targets = new List<ClientCard>();
            // exzone fo first
            foreach (ClientCard e_m in this.Bot.GetMonstersInExtraZone())
            {
                if (e_m.Attack < 1900 && !targets.ContainsCardWithId(e_m.Id) && e_m.IsFaceup())
                {
                    targets.Add(e_m);
                }
            }
            List<ClientCard> sort_main_list = new List<ClientCard>(this.Bot.GetMonstersInMainZone());
            sort_main_list.Sort(CardContainer.CompareCardAttack);
            foreach (ClientCard m in sort_main_list)
            {
                if (m.Attack < 1900 && !targets.ContainsCardWithId(m.Id) && m.IsFaceup())
                {
                    targets.Add(m);
                }
                if (targets.Count >= 4)
                {
                    if (this.Enemy.LifePoints <= this.GetTotalATK(targets) && this.Enemy.GetMonsterCount() == 0)
                    {
                        return false;
                    }

                    this.AI.SelectMaterials(targets);
                    this.AI.SelectYesNo(true);
                    this.snake_four_s = true;
                    return true;
                }
            }
            return false;
        }

        public bool Snake_eff()
        {
            if (this.snake_four_s)
            {
                this.snake_four_s = false;
                this.AI.SelectCard(this.Useless_List());
                return true;
            }
            //if (ActivateDescription == Util.GetStringId(CardId.Snake, 2)) return true;
            if (this.ActivateDescription == this.Util.GetStringId(CardId.Snake, 1))
            {
                foreach(ClientCard hand in this.Bot.Hand)
                {
                    if (hand.IsCode(CardId.Red, CardId.Pink))
                    {
                        this.AI.SelectCard(hand);
                        return true;
                    }
                    if (hand.IsCode(CardId.Urara, CardId.Ghost))
                    {
                        if (this.Tuner_ss())
                        {
                            this.AI.SelectCard(hand);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool Missus_ss()
        {
            IList<ClientCard> material_list = new List<ClientCard>();
            foreach(ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.HasAttribute(CardAttribute.Earth) && this.getLinkMarker(monster.Id) == 1)
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

            if (this.Enemy.GetMonsterCount() == 0 || this.Util.GetProblematicEnemyMonster(2000) == null)
            {
                this.AI.SelectMaterials(material_list);
                return true;
            } else if (this.Util.GetProblematicEnemyMonster(2000) != null && this.Bot.HasInExtra(CardId.Borrel) && !this.Bot.HasInMonstersZone(CardId.Missus))
            {
                this.AI.SelectMaterials(material_list);
                return true;
            }
            return false;
        }

        public bool Missus_eff()
        {
            this.AI.SelectCard(CardId.MG, CardId.Missus, CardId.Snake);
            return true;
        }

        public bool Borrel_ss()
        {
            bool already_link2 = false;
            IList<ClientCard> material_list = new List<ClientCard>();
            if (this.Util.GetProblematicEnemyMonster(2000) == null)
            {
                Logger.DebugWriteLine("***borrel:null");
            }
            else
            {
                Logger.DebugWriteLine("***borrel:" + (this.Util.GetProblematicEnemyMonster(2000).Name ?? "unknown"));
            }

            if (this.Util.GetProblematicEnemyMonster(2000) != null || (this.Enemy.GetMonsterCount() == 0 && this.Duel.Phase == DuelPhase.Main1 && this.Enemy.LifePoints <= 3000))
            {
                foreach(ClientCard e_m in this.Bot.GetMonstersInExtraZone())
                {
                    if (this.getLinkMarker(e_m.Id) < 3)
                    {
                        if (this.getLinkMarker(e_m.Id) == 2)
                        {
                            already_link2 = true;
                        }

                        material_list.Add(e_m);
                    }
                }
                List<ClientCard> sort_list = new List<ClientCard>(this.Bot.GetMonstersInMainZone());
                sort_list.Sort(CardContainer.CompareCardAttack);

                foreach(ClientCard m in sort_list)
                {
                    if (this.getLinkMarker(m.Id) < 3)
                    {
                        if (this.getLinkMarker(m.Id) == 2 && !already_link2)
                        {
                            already_link2 = true;
                            material_list.Add(m);
                        } else if (!m.IsCode(CardId.Sheep + 1, CardId.Eater))
                        {
                            material_list.Add(m);
                        }
                        if ((already_link2 && material_list.Count == 3) || (!already_link2 && material_list.Count == 4))
                        {
                            break;
                        }
                    }
                }
                if ((already_link2 && material_list.Count == 3) || (!already_link2 && material_list.Count == 4))
                {
                    if (this.Enemy.GetMonsterCount() == 0 && this.Duel.Phase == DuelPhase.Main1 && this.Enemy.LifePoints <= 3000)
                    {
                        if (this.GetTotalATK(material_list) >= 3000)
                        {
                            return false;
                        }
                    }
                    this.AI.SelectMaterials(material_list);
                    return true;
                }
            }
            return false;
        }

        public bool Borrel_eff()
        {
            if (this.ActivateDescription == -1) {
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

        // unfinished.
        public bool GraveCall_eff()
        {
            if (!this.spell_trap_activate())
            {
                return false;
            }

            if (this.Duel.LastChainPlayer == 1)
            {
                if (this.Util.GetLastChainCard().IsMonster() && this.Enemy.HasInGraveyard(this.Util.GetLastChainCard().Id))
                {
                    this.GraveCall_id = this.Util.GetLastChainCard().Id;
                    this.GraveCall_count = 2;
                    this.AI.SelectCard(this.GraveCall_id);
                    return true;
                }
            }
            return false;
        }

        public bool DarkHole_eff()
        {
            if (!this.spell_trap_activate())
            {
                return false;
            }

            if (this.Bot.GetMonsterCount() == 0)
            {
                
                int bestPower = -1;
                foreach (ClientCard hand in this.Bot.Hand)
                {
                    if (hand.IsMonster() && hand.Attack > bestPower)
                    {
                        bestPower = hand.Attack;
                    }
                }
                int bestenemy = -1;
                foreach (ClientCard enemy in this.Enemy.GetMonsters())
                {
                    if (enemy.IsMonsterDangerous())
                    {
                        this.AI.SelectPlace(this.SelectSTPlace());
                        return true;
                    }
                    if (enemy.IsFaceup() && (enemy.GetDefensePower() > bestenemy))
                    {
                        bestenemy = enemy.GetDefensePower();
                    }
                }
                if (bestPower <= bestenemy)
                {
                    this.AI.SelectPlace(this.SelectSTPlace());
                    return true;
                }
            }
            return false;
        }

        public bool IsAllEnemyBetter()
        {
            int bestPower = -1;
            for (int i = 0; i < 7; ++i)
            {
                ClientCard card = this.Bot.MonsterZone[i];
                if (card == null || card.Data == null)
                {
                    continue;
                }

                int newPower = card.Attack;
                if (this.IsTrickstar(card.Id) && this.Bot.HasInHand(CardId.White) && !this.white_eff_used)
                {
                    newPower += card.RealPower;
                }

                if (newPower > bestPower)
                {
                    bestPower = newPower;
                }
            }
            return this.Util.IsAllEnemyBetterThanValue(bestPower,true);
        }

        public bool MonsterRepos()
        {
            if (this.Card.IsCode(CardId.Eater))
            {
                return (!this.Card.HasPosition(CardPosition.Attack));
            }

            if (this.IsTrickstar(this.Card.Id) && !this.white_eff_used && this.Bot.HasInHand(CardId.White) && this.Card.IsAttack() && this.Duel.Phase == DuelPhase.Main1)
            {
                return false;
            }

            if (this.Card.IsFaceup() && this.Card.IsDefense() && this.Card.Attack == 0)
            {
                return false;
            }

            if (this.Card.IsCode(CardId.Pink))
            {
                if ((this.Bot.HasInSpellZone(CardId.Stage, true) && this.Enemy.LifePoints <= 1000) || (!this.Bot.HasInSpellZone(CardId.Stage, true) && this.Enemy.LifePoints <= 800))
                {
                    return !this.Card.HasPosition(CardPosition.Attack);
                }
            }

            bool enemyBetter = this.IsAllEnemyBetter();

            if (this.Card.IsAttack() && enemyBetter)
            {
                return true;
            }

            if (this.Card.IsDefense() && !enemyBetter && this.Card.Attack >= this.Card.Defense)
            {
                return true;
            }

            return false;
        }

        public override bool OnSelectHand()
        {
            // go first
            return true;
        }

        public override void OnNewTurn()
        {
            this.NormalSummoned = false;
            this.stage_locked = null;
            this.pink_ss = false;
            this.snake_four_s = false;
            this.crystal_eff_used = false;
            this.red_ss_count = 0;
            this.white_eff_used = false;
            this.lockbird_useful = false;
            this.lockbird_used = false;
            if (this.GraveCall_count > 0)
            {
                if (--this.GraveCall_count <= 0)
                {
                    this.GraveCall_id = 0;
                }                
            }
        }

        public override BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
        {
            ClientCard lowestattack = null;
            for (int i = defenders.Count - 1; i >= 0; --i)
            {
                ClientCard defender = defenders[i];
                if (defender.HasPosition(CardPosition.Attack) && !defender.IsMonsterDangerous() && (lowestattack == null || defender.Attack < lowestattack.Attack))
                {
                    lowestattack = defender;
                }
            }
            if (lowestattack != null && attacker.Attack - lowestattack.Attack >= this.Enemy.LifePoints)
            {
                return this.AI.Attack(attacker, lowestattack);
            }

            for (int i = 0; i < defenders.Count; ++i)
            {
                ClientCard defender = defenders[i];

                attacker.RealPower = attacker.Attack;
                defender.RealPower = defender.GetDefensePower();

                if (!defender.IsMonsterHasPreventActivationEffectInBattle() && !attacker.IsDisabled())
                {
                    if ((attacker.IsCode(CardId.Eater) && !defender.HasType(CardType.Token)) || attacker.IsCode(CardId.Borrel))
                    {
                        return this.AI.Attack(attacker, defender);
                    }

                    if ((attacker.IsCode(CardId.Ultimate, CardId.Cardian)) && attacker.RealPower > defender.RealPower)
                    {
                        return this.AI.Attack(attacker, defender);
                    }
                }

                if (!this.OnPreBattleBetween(attacker, defender))
                {
                    continue;
                }

                if (attacker.RealPower > defender.RealPower || (attacker.RealPower >= defender.RealPower && attacker.IsLastAttacker && defender.IsAttack()))
                {
                    return this.AI.Attack(attacker, defender);
                }
            }

            if (attacker.CanDirectAttack)
            {
                return this.AI.Attack(attacker, null);
            }

            return null;
        }

        public override ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
        {
            for (int i = 0; i < attackers.Count; ++i)
            {
                ClientCard attacker = attackers[i];
                if (attacker.IsCode(CardId.Borrel, CardId.Eater))
                {
                    return attacker;
                }
            }
            return null;
        }

        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (!defender.IsMonsterHasPreventActivationEffectInBattle())
            {
                if (this.IsTrickstar(attacker.Id) && this.Bot.HasInHand(CardId.White) && !this.white_eff_used)
                {
                    attacker.RealPower = attacker.RealPower + attacker.Attack;
                }
            }
            return base.OnPreBattleBetween(attacker, defender);
        }
    }
}
