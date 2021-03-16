using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
    [Deck("Salamangreat", "AI_Salamangreat")]
    class SalamangreatExecutor : DefaultExecutor
    {
        bool foxyPopEnemySpell = false;
        bool wasGazelleSummonedThisTurn = false;
        bool wasFieldspellUsedThisTurn = false;
        bool wasWolfSummonedUsingItself = false;
        int sunlightPosition = 0;
        bool wasVeilynxSummonedThisTurn = false;
        bool falcoHitGY = false;
        List<int> CombosInHand;
        readonly List<int> Impermanence_list = new List<int>();
        public class CardId
        {
            public const int JackJaguar = 56003780;
            public const int EffectVeiler = 97268402;
            public const int LadyDebug = 16188701;
            public const int Foxy = 94620082;
            public const int Gazelle = 26889158;
            public const int Fowl = 89662401;
            public const int Falco = 20618081;
            public const int Spinny = 52277807;
            public const int MaxxC = 23434538;
            public const int AshBlossom = 14558127;

            public const int FusionOfFire = 25800447;
            public const int Circle = 52155219;
            public const int HarpieFeatherDuster = 18144507;
            public const int FoolishBurial = 81439174;
            public const int Sanctuary = 1295111;
            public const int CalledByTheGrave = 24224830;
            public const int SalamangreatRage = 14934922;
            public const int SalamangreatRoar = 51339637;
            public const int Impermanence = 10045474;
            public const int SolemnJudgment = 41420027;
            public const int SolemnStrike = 40605147;

            public const int SalamangreatVioletChimera = 37261776;
            public const int ExcitionKnight = 46772449;
            public const int MirageStallio = 87327776;
            public const int SunlightWolf = 87871125;
            public const int Borrelload = 31833038;
            public const int HeatLeo = 41463181;
            public const int Veilynx = 14812471;
            public const int Charmer = 48815792;
            public const int KnightmarePheonix = 2857636;
            public const int Borrelsword = 85289965;

            public const int GO_SR = 59438930;
            public const int DarkHole = 53129443;
            public const int NaturalBeast = 33198837;
            public const int SwordsmanLV7 = 37267041;
            public const int RoyalDecreel = 51452091;
            public const int Anti_Spell = 58921041;
            public const int Hayate = 8491308;
            public const int Raye = 26077387;
            public const int Drones_Token = 52340445;
            public const int Iblee = 10158145;
            public const int ImperialOrder = 61740673;
            public const int TornadoDragon = 6983839;
        }

        readonly List<int> Combo_cards = new List<int>()
        {
            CardId.Spinny,
            CardId.JackJaguar,
            CardId.Fowl,
            CardId.Foxy,
            CardId.Falco,
            CardId.Circle,
            CardId.Gazelle,
            CardId.FoolishBurial
        };
        readonly List<int> normal_counter = new List<int>
        {
            53262004, 98338152, 32617464, 45041488, CardId.SolemnStrike,
            61257789, 23440231, 27354732, 12408276, 82419869, CardId.Impermanence,
            49680980, 18621798, 38814750, 17266660, 94689635,CardId.AshBlossom,
            74762582, 75286651, 4810828,  44665365, 21123811, DefaultExecutor.CardId.CrystalWingSynchroDragon,
            82044279, 82044280, 79606837, 10443957, 1621413,
            90809975, 8165596,  9753964,  53347303, 88307361, DefaultExecutor.CardId.GamecieltheSeaTurtleKaiju,
            5818294,  2948263,  6150044,  26268488, 51447164, DefaultExecutor.CardId.JizukirutheStarDestroyingKaiju,
            97268402
        };
        readonly List<int> should_not_negate = new List<int>
        {
            81275020, 28985331
        };
        readonly List<int> salamangreat_links = new List<int>
        {
            CardId.HeatLeo,
            CardId.SunlightWolf,
            CardId.Veilynx
        };
        readonly List<int> JackJaguarTargets = new List<int>
        {
            CardId.SunlightWolf,
            CardId.MirageStallio,
            CardId.HeatLeo
        };
        readonly List<int> salamangreat_combopieces = new List<int>
        {
            CardId.Gazelle,
            CardId.Spinny,
            CardId.JackJaguar,
            CardId.Foxy,
            CardId.Circle,
            CardId.Falco
        };
        readonly List<int> WolfMaterials = new List<int>
        {
            CardId.Veilynx,
            CardId.JackJaguar,
            CardId.Falco,
            CardId.Foxy,
            CardId.MirageStallio,
            CardId.Gazelle
        };
        readonly List<int> salamangreat_spellTrap = new List<int>
        {
            CardId.SalamangreatRoar,
            CardId.SalamangreatRage,
            CardId.Circle,
            CardId.Sanctuary
        };
        private bool falcoUsedReturnST;
        private bool wasStallioActivated;
        private bool wasWolfActivatedThisTurn;

        bool JackJaguarActivatedThisTurn = false;
        bool FoxyActivatedThisTurn = false;

        public SalamangreatExecutor(GameAI ai, Duel duel) : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.Activate, CardId.HarpieFeatherDuster);
            this.AddExecutor(ExecutorType.Activate, CardId.MaxxC, this.G_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.CalledByTheGrave, this.Called_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.AshBlossom, this.Hand_act_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.EffectVeiler, this.DefaultBreakthroughSkill);
            this.AddExecutor(ExecutorType.Activate, CardId.Impermanence, this.Impermanence_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.SalamangreatRoar, this.SolemnJudgment_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, this.SolemnStrike_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnJudgment, this.SolemnJudgment_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.Sanctuary, this.Sanctuary_activate);

            this.AddExecutor(ExecutorType.Activate, CardId.Charmer);
            this.AddExecutor(ExecutorType.Activate, CardId.SunlightWolf, this.Wolf_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.LadyDebug, this.Fadydebug_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.Foxy, this.Foxy_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.Falco, this.Falco_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.Circle, this.Circle_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.Borrelsword, this.Borrelsword_eff);
            this.AddExecutor(ExecutorType.Activate, CardId.Gazelle, this.Gazelle_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.Spinny, this.Spinny_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.MirageStallio, this.Stallio_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.Veilynx);

            this.AddExecutor(ExecutorType.Activate, CardId.JackJaguar, this.JackJaguar_activate);
            this.AddExecutor(ExecutorType.Summon, CardId.LadyDebug);
            this.AddExecutor(ExecutorType.Summon, CardId.Foxy);
            this.AddExecutor(ExecutorType.Summon, CardId.Spinny);
            this.AddExecutor(ExecutorType.Summon, CardId.JackJaguar);
            this.AddExecutor(ExecutorType.Summon, CardId.Gazelle);
            this.AddExecutor(ExecutorType.Summon, CardId.Fowl);
            this.AddExecutor(ExecutorType.Activate, CardId.Spinny, this.Spinny_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.HeatLeo, this.DefaultMysticalSpaceTyphoon);

            this.AddExecutor(ExecutorType.SpSummon, CardId.Borrelsword, this.Borrelsword_ss);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Veilynx, this.Veilynx_summon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.MirageStallio, this.Stallio_summon);
            this.AddExecutor(ExecutorType.Activate, CardId.MirageStallio, this.Stallio_activate);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Charmer, this.Charmer_summon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.SunlightWolf, this.SunlightWolf_summon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.HeatLeo, this.HeatLeo_summon);

            this.AddExecutor(ExecutorType.SpSummon, CardId.TornadoDragon);
            this.AddExecutor(ExecutorType.Activate, CardId.TornadoDragon, this.DefaultMysticalSpaceTyphoon);

            this.AddExecutor(ExecutorType.Activate, CardId.SalamangreatRage, this.Rage_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.Fowl, this.Fowl_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.SunlightWolf, this.Wolf_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.Gazelle, this.Gazelle_activate);
            this.AddExecutor(ExecutorType.Activate, CardId.FoolishBurial, this.FoolishBurial_activate);

            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
            this.AddExecutor(ExecutorType.SpellSet, this.SpellSet);

        }

        public int get_Wolf_linkzone()
        {
            ClientCard WolfInExtra = this.Bot.GetMonstersInExtraZone().Where(x => x.Id == CardId.SunlightWolf).ToList().FirstOrDefault(x => x.Id == CardId.SunlightWolf);
            if (WolfInExtra != null)
            {
                int zone = WolfInExtra.Position;
                if (zone == 5)
                {
                    return 1;
                }

                if (zone == 6)
                {
                    return 3;
                }
            }
            return -1;
        }

        private bool Charmer_summon()
        {
            if (this.Duel.Phase != DuelPhase.Main1)
            {
                return false;
            }

            if (this.Duel.Turn == 1)
            {
                return false;
            }

            if (this.Enemy.Graveyard.Where(x => x.Attribute == (int)CardAttribute.Fire).Count() > 0
                && (this.Bot.GetMonstersInExtraZone().Count == 0
                || this.Bot.GetMonstersInExtraZone().Where(x =>
                (x.Id == CardId.Veilynx
                || x.Id == CardId.MirageStallio)
                && x.Owner == 0).Count() == 1))
            {
                List<ClientCard> material_list = new List<ClientCard>();
                List<ClientCard> bot_monster = this.Bot.GetMonsters();
                bot_monster.Sort(CardContainer.CompareCardAttack);
                //bot_monster.Reverse();
                int link_count = 0;
                foreach (ClientCard card in bot_monster)
                {
                    if (card.IsFacedown())
                    {
                        continue;
                    }

                    if (!material_list.Contains(card) && card.LinkCount < 2)
                    {
                        material_list.Add(card);
                        link_count += (card.HasType(CardType.Link)) ? card.LinkCount : 1;
                        if (link_count >= 4)
                        {
                            break;
                        }
                    }
                }
                if (link_count >= 3)
                {
                    this.AI.SelectCard(CardId.Veilynx);
                    return true;
                }
            }
            return false;
        }

        private bool HeatLeo_summon()
        {
            if (this.Duel.Turn == 1)
            {
                return false;
            }

            if (this.Duel.Phase != DuelPhase.Main1)
            {
                return false;
            }

            if (this.wasWolfSummonedUsingItself && this.Bot.GetMonsters().Count() <= 3)
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

            if (this.Enemy.GetSpells().Where(x => x.IsFloodgate()).Count() > 0)
            {
                List<ClientCard> material_list = new List<ClientCard>();
                List<ClientCard> bot_monster = this.Bot.GetMonsters();
                bot_monster.Sort(CardContainer.CompareCardAttack);
                //bot_monster.Reverse();
                int link_count = 0;

                foreach (ClientCard card in bot_monster)
                {
                    if (card.IsFacedown())
                    {
                        continue;
                    }

                    if (!material_list.Contains(card) && card.LinkCount < 2)
                    {
                        material_list.Add(card);
                        link_count += (card.HasType(CardType.Link)) ? card.LinkCount : 1;
                        if (link_count >= 3)
                        {
                            break;
                        }
                    }
                }
                if (link_count >= 3)
                {
                    this.AI.SelectMaterials(material_list);
                    return true;
                }
            }
            return false;
        }

        private bool Stallio_summon()
        {
            if (!this.wasStallioActivated)
            {
                this.AI.SelectMaterials(CardId.Spinny);
                return true;
            }
            return false;
        }

        private bool SunlightWolf_summon()
        {
            if (this.Bot.HasInMonstersZone(CardId.SunlightWolf))
            {
                if (this.wasWolfSummonedUsingItself)
                {
                    return false;
                }

                if (!this.wasFieldspellUsedThisTurn && this.Bot.HasInGraveyard(this.salamangreat_spellTrap) || this.Bot.HasInHandOrInSpellZone(CardId.SalamangreatRage))
                {
                    this.AI.SelectOption(1);
                    this.AI.SelectMaterials(new List<int>() {
                        CardId.SunlightWolf,
                        CardId.Veilynx,
                        CardId.JackJaguar,
                        CardId.Gazelle
                    });
                    this.wasWolfSummonedUsingItself = true;
                    this.AI.SelectPlace(this.sunlightPosition);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            this.wasWolfSummonedUsingItself = false;
            if (this.Bot.HasInMonstersZone(CardId.Veilynx))
            {

                if (this.Bot.HasInMonstersZone(CardId.MirageStallio)
                    && this.Bot.HasInMonstersZone(CardId.Veilynx)
                    && this.Bot.HasInMonstersZone(CardId.Gazelle))
                {
                    this.AI.SelectCard(CardId.Veilynx);
                    this.AI.SelectNextCard(CardId.MirageStallio);
                }
                else
                {
                    this.AI.SelectCard(this.WolfMaterials);
                    this.AI.SelectNextCard(this.WolfMaterials);
                }
                this.sunlightPosition = this.SelectSetPlace(new List<int>() { CardId.Veilynx }, true);

                this.AI.SelectPlace(this.sunlightPosition);
            }
            return true;
        }

        private bool Wolf_activate()
        {
            this.wasWolfActivatedThisTurn = true;
            this.AI.SelectCard(new List<int>() {
                CardId.Gazelle,
                CardId.SalamangreatRoar,
                CardId.SalamangreatRage,
                CardId.Foxy,
                CardId.AshBlossom,
                CardId.Fowl,
                CardId.SunlightWolf,
                CardId.Veilynx,
                CardId.HeatLeo,
                CardId.Spinny
            });
            return true;
        }

        private bool Stallio_activate()
        {
            if (this.Card.Location == CardLocation.MonsterZone)
            {
                this.wasStallioActivated = true;
                if (!this.wasGazelleSummonedThisTurn)
                {
                    this.AI.SelectCard(CardId.Gazelle, CardId.Spinny);
                    this.AI.SelectNextCard(CardId.Gazelle);
                    return true;
                }
                if (!this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.JackJaguar))
                {
                    this.AI.SelectCard(CardId.Gazelle);
                    this.AI.SelectNextCard(CardId.JackJaguar);
                    return true;
                }
                if (!this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.Falco) && this.FalcoToGY(true))
                {
                    this.AI.SelectCard(CardId.Gazelle);
                    this.AI.SelectNextCard(CardId.Falco);
                    return true;
                }
                this.AI.SelectCard(CardId.Gazelle);
                return true;
            }
            else
            {
                if (this.Util.GetBestEnemyMonster(canBeTarget: true) != null)
                {
                    this.AI.SelectCard(this.Util.GetBestEnemyMonster(canBeTarget: true));
                    return true;
                }
            }
            return false;
        }

        private bool Veilynx_summon()
        {
            if (this.wasStallioActivated && this.wasWolfActivatedThisTurn)
            {
                return false;
            }
            if ((this.wasStallioActivated
                && !this.wasWolfActivatedThisTurn)
                ||
                (!this.wasStallioActivated
                && this.wasWolfActivatedThisTurn))
            {
                return false;
            }
            if (this.Bot.HasInHand(CardId.Gazelle)
                && !this.wasGazelleSummonedThisTurn
                && !this.Bot.HasInGraveyard(CardId.JackJaguar)
                && this.Bot.GetMonstersInMainZone().Where(x => x.Level == 3).Count() <= 1
                || (this.Bot.HasInMonstersZone(CardId.SunlightWolf)
                && !this.Bot.HasInSpellZoneOrInGraveyard(CardId.Sanctuary)
                && !this.wasWolfSummonedUsingItself))
            {

                var monsters = this.Bot.GetMonstersInMainZone();
                if (this.Bot.HasInMonstersZone(CardId.Veilynx) && monsters.Count == 2)
                {
                    return false;
                }

                monsters.Sort(CardContainer.CompareCardLevel);
                monsters.Reverse();
                this.AI.SelectMaterials(monsters);
                return true;
            }
            if (!this.Bot.HasInMonstersZone(CardId.Veilynx)
                &&
                this.Bot.GetMonstersInMainZone().Count >= 3
                &&
                (this.Bot.GetMonstersInExtraZone().Where(x => x.Owner == 0).Count() == 0))
            {
                var monsters = this.Bot.GetMonstersInMainZone();
                monsters.Sort(CardContainer.CompareCardLevel);
                monsters.Reverse();
                this.AI.SelectMaterials(monsters);
                return true;
            }


            if (this.CombosInHand.Where(x => x != CardId.Foxy).Where(x => x != CardId.Spinny).Count() == 0
                && this.Bot.HasInHand(CardId.Spinny))
            {
                if (this.Bot.HasInMonstersZone(CardId.Gazelle) && this.Bot.HasInMonstersZone(CardId.SunlightWolf))
                {
                    this.AI.SelectMaterials(CardId.Gazelle);
                    return true;
                }
                if (!this.wasVeilynxSummonedThisTurn)
                {
                    this.wasVeilynxSummonedThisTurn = true;
                    return true;
                }
            }

            return false;
        }

        private bool JackJaguar_activate()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                if (this.Bot.HasInGraveyard(this.JackJaguarTargets)
                    || this.Bot.Graveyard.Where(x => x.Id == CardId.Veilynx).Count() >= 2
                    || (!this.Bot.HasInGraveyard(this.salamangreat_spellTrap)
                    && this.Bot.HasInMonstersZone(CardId.SunlightWolf)
                    && this.Bot.HasInGraveyard(CardId.Gazelle)
                    && !this.Bot.HasInHand(CardId.Gazelle)))
                {
                    this.JackJaguarActivatedThisTurn = true;
                    if (this.Bot.Graveyard.Where(x => x.Id == CardId.Veilynx).Count() >= 2
                        && this.Bot.Graveyard.Select(x => x.Id).Intersect(this.JackJaguarTargets).Count() == 0)
                    {
                        this.AI.SelectCard(CardId.Veilynx);
                        return true;
                    }
                    this.AI.SelectCard(this.JackJaguarTargets);
                    return true;
                }
            }
            return false;
        }

        private bool Fowl_activate()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                return this.Bot.HasInMonstersZone(CardId.JackJaguar) && this.JackJaguarActivatedThisTurn;
            }
            return false;
        }

        private bool Spinny_activate()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                if (this.Bot.HasInGraveyard(CardId.Foxy) && !this.FoxyActivatedThisTurn)
                {
                    return false;
                }

                if (this.CombosInHand.Where(x => x != CardId.Foxy).Where(x => x != CardId.Spinny).Count() == 0)
                {
                    return false;
                }

                if (!this.Bot.HasInMonstersZoneOrInGraveyard(CardId.Spinny)
                    && this.Util.GetBestBotMonster(true) != null
                    && !(this.Bot.GetMonsters().Count == 1
                    && this.Bot.HasInMonstersZone(CardId.Spinny)))
                {
                    this.AI.SelectCard(this.Util.GetBestBotMonster(true));
                    return true;
                }
            }
            return true;
        }

        private bool Falco_activate()
        {
            if (!this.falcoUsedReturnST && this.falcoHitGY)
            {
                if (this.Bot.HasInGraveyard(this.salamangreat_spellTrap))
                {
                    this.falcoUsedReturnST = true;
                    this.AI.SelectCard(this.salamangreat_spellTrap);
                    return true;
                }
            }
            return false;
        }

        private bool Gazelle_activate()
        {
            this.wasGazelleSummonedThisTurn = true;
            if (!this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.Spinny))
            {
                this.AI.SelectCard(CardId.Spinny);
                return true;
            }
            if (!this.Bot.HasInSpellZoneOrInGraveyard(CardId.SalamangreatRoar))
            {
                this.AI.SelectCard(CardId.SalamangreatRoar);
                return true;
            }
            if (!this.Bot.HasInSpellZoneOrInGraveyard(CardId.SalamangreatRage))
            {
                this.AI.SelectCard(CardId.SalamangreatRage);
                return true;
            }
            if (!this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.JackJaguar))
            {
                this.AI.SelectCard(CardId.JackJaguar);
                return true;
            }
            if (!this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.Foxy))
            {
                this.AI.SelectCard(CardId.Foxy);
                return true;
            }
            if (!this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.Falco))
            {
                this.AI.SelectCard(CardId.Falco);
                return true;
            }
            return true;
        }

        private bool Foxy_activate()
        {
            if (this.Card.Location == CardLocation.MonsterZone)
            {

                if (this.CombosInHand.Where(x => x != CardId.Foxy).Where(x => x != CardId.Spinny).Count() == 0 && this.Bot.HasInHand(CardId.Spinny))
                {
                    return false;
                }
                this.AI.SelectCard(this.salamangreat_combopieces);
                this.FoxyActivatedThisTurn = true;
                return true;
            }
            else
            {
                if (this.Bot.HasInHand(CardId.Spinny) || this.FalcoToGY(false))
                {
                    if (this.Bot.HasInHand(CardId.Spinny) && !this.Bot.HasInGraveyard(CardId.Spinny))
                    {
                        this.AI.SelectCard(CardId.Spinny);
                    }
                    else
                    {
                        if (this.FalcoToGY(false))
                        {
                            this.AI.SelectCard(CardId.Falco);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (this.Util.GetBestEnemySpell(true) != null)
                    {
                        this.AI.SelectNextCard(this.Util.GetBestEnemySpell(true));
                        this.foxyPopEnemySpell = true;
                    }
                    this.FoxyActivatedThisTurn = true;
                    return true;
                }
                return false;
            }
        }

        private bool FalcoToGY(bool FromDeck)
        {
            if (FromDeck && this.Bot.Deck.ContainsCardWithId(CardId.Falco))
            {
                if (this.Bot.HasInGraveyard(this.salamangreat_spellTrap))
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (this.Bot.HasInHand(CardId.Falco) && this.Bot.HasInGraveyard(this.salamangreat_spellTrap))
                {
                    return true;
                }
                return false;
            }
        }
        private bool Fadydebug_activate()
        {
            if (!this.Bot.HasInHand(CardId.Gazelle))
            {
                this.AI.SelectCard(CardId.Gazelle);
                return true;
            }
            if (!this.Bot.HasInHandOrInGraveyard(CardId.Spinny))
            {
                this.AI.SelectCard(CardId.Spinny);
                return true;
            }
            if (!this.Bot.HasInHand(CardId.Foxy))
            {
                this.AI.SelectCard(CardId.Foxy);
                return true;
            }
            return true;
        }

        private bool Circle_activate()
        {
            var x = this.ActivateDescription;
            if (this.ActivateDescription == this.Util.GetStringId(CardId.Circle, 0) || this.ActivateDescription == 0)
            {
                this.AI.SelectOption(0);
                if (!this.Bot.HasInHand(CardId.Gazelle))
                {
                    this.AI.SelectCard(CardId.Gazelle);
                    return true;
                }
                if (!this.Bot.HasInHandOrInGraveyard(CardId.Spinny))
                {
                    this.AI.SelectCard(CardId.Spinny);
                    return true;
                }
                if (!this.Bot.HasInHand(CardId.Foxy))
                {
                    this.AI.SelectCard(CardId.Foxy);
                    return true;
                }
                if (!this.Bot.HasInHand(CardId.Fowl))
                {
                    this.AI.SelectCard(CardId.Fowl);
                    return true;
                }
                if (!this.Bot.HasInHand(CardId.JackJaguar))
                {
                    this.AI.SelectCard(CardId.JackJaguar);
                    return true;
                }
                if (!this.Bot.HasInHand(CardId.Falco))
                {
                    this.AI.SelectCard(CardId.Falco);
                    return true;
                }
                return false;
            }

            return false;
        }

        private bool FoolishBurial_activate()
        {
            if (this.FalcoToGY(true) && this.Bot.HasInHandOrInGraveyard(CardId.Spinny))
            {
                this.AI.SelectCard(CardId.Falco);
                return true;
            }
            this.AI.SelectCard(CardId.Spinny, CardId.JackJaguar, CardId.Foxy);
            return true;
        }

        private bool Sanctuary_activate()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                return true;
            }
            return false;
        }

        private bool Rage_activate()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.SalamangreatRage, 1))
            {
                this.AI.SelectCard(this.salamangreat_links);
                this.AI.SelectOption(1);
                IList<ClientCard> targets = new List<ClientCard>();

                ClientCard target1 = this.Util.GetBestEnemyMonster(canBeTarget: true);
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
                    if (targets.Count >= 2)
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
                    if (targets.Count >= 2)
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

                this.AI.SelectNextCard(targets);
                return true;
            }
            else
            {
                if (this.Util.GetProblematicEnemyCard(canBeTarget: true) != null)
                {
                    if (this.Util.GetBestBotMonster(true) != null)
                    {
                        this.AI.SelectCard(this.Util.GetProblematicEnemyCard(this.Util.GetBestBotMonster(true).Attack, canBeTarget: true));
                    }
                    else
                    {
                        this.AI.SelectCard(this.Util.GetProblematicEnemyCard(canBeTarget: true));
                    }

                    return true;
                }
            }
            return false;
        }

        public bool G_activate()
        {
            return (this.Duel.Player == 1);
        }
        public bool Hand_act_eff()
        {
            return (this.Duel.LastChainPlayer == 1);
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
                    this.AI.SelectCard(m);
                    return true;
                }
            }

            ClientCard LastChainCard = this.Util.GetLastChainCard();

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
                if ((this_seq * that_seq >= 0 && this_seq + that_seq == 4)
                    || (this.Util.IsChainTarget(this.Card))
                    || (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.IsCode(DefaultExecutor.CardId.HarpiesFeatherDuster)))
                {
                    List<ClientCard> enemy_monsters = this.Enemy.GetMonsters();
                    enemy_monsters.Sort(CardContainer.CompareCardAttack);
                    enemy_monsters.Reverse();
                    foreach (ClientCard card in enemy_monsters)
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
            if ((LastChainCard == null || LastChainCard.Controller != 1 || LastChainCard.Location != CardLocation.MonsterZone
                || LastChainCard.IsDisabled() || LastChainCard.IsShouldNotBeTarget() || LastChainCard.IsShouldNotBeSpellTrapTarget()))
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
            return !this.Util.IsChainTargetOnly(this.Card)
                    &&
                    !(this.Duel.Player == 0
                    && this.Duel.LastChainPlayer == -1)
                    && this.DefaultTrap() && this.spell_trap_activate(true);
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

        public bool Should_counter()
        {
            if (this.Duel.CurrentChain.Count < 2)
            {
                return true;
            }

            ClientCard self_card = this.Duel.CurrentChain[this.Duel.CurrentChain.Count - 2];
            if (self_card?.Controller != 0
                || !(self_card.Location == CardLocation.MonsterZone || self_card.Location == CardLocation.SpellZone))
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
        public int SelectSTPlace(ClientCard card = null, bool avoid_Impermanence = false)
        {
            List<int> list = new List<int>
            {
                0,
                1,
                2,
                3,
                4
            };
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

        public override bool OnSelectYesNo(int desc)
        {
            if (desc == this.Util.GetStringId(CardId.Sanctuary, 0))
            {
                this.wasFieldspellUsedThisTurn = true;
            }
            if (desc == this.Util.GetStringId(CardId.Foxy, 3))
            {
                return this.foxyPopEnemySpell;
            }
            return base.OnSelectYesNo(desc);
        }

        public override void OnNewTurn()
        {
            this.FoxyActivatedThisTurn = false;
            this.JackJaguarActivatedThisTurn = false;
            this.wasWolfActivatedThisTurn = false;
            this.wasStallioActivated = false;
            this.falcoUsedReturnST = false;
            this.CombosInHand = this.Bot.Hand.Select(h => h.Id).Intersect(this.Combo_cards).ToList();
            this.wasFieldspellUsedThisTurn = false;
            this.wasGazelleSummonedThisTurn = false;
            base.OnNewTurn();
        }

        public override bool OnSelectHand()
        {
            return true;
        }

        public bool SpellSet()
        {
            if (this.Card.Id == CardId.Circle)
            {
                return false;
            }
            if (this.Duel.Phase == DuelPhase.Main1 && this.Bot.HasAttackingMonster() && this.Duel.Turn > 1)
            {
                return false;
            }

            if (this.Card.IsCode(CardId.SolemnStrike) && this.Bot.LifePoints <= 1500)
            {
                return false;
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
                    }
                    else
                    {
                        this.AI.SelectPlace(this.SelectSetPlace(avoid_list));
                        return true;
                    }
                }
                else
                {
                    this.AI.SelectPlace(this.SelectSTPlace());
                }
                return true;
            }
            return false;
        }

        public bool Called_activate()
        {
            if (!this.DefaultUniqueTrap())
            {
                return false;
            }

            if (this.Duel.Player == 1)
            {
                ClientCard target = this.Enemy.MonsterZone.GetShouldBeDisabledBeforeItUseEffectMonster();
                if (target != null && this.Enemy.HasInGraveyard(target.Id))
                {
                    this.AI.SelectCard(target.Id);
                    return true;
                }
            }

            ClientCard LastChainCard = this.Util.GetLastChainCard();

            if (LastChainCard != null
                && LastChainCard.Controller == 1
                && (LastChainCard.Location == CardLocation.Grave
                || LastChainCard.Location == CardLocation.Hand
                || LastChainCard.Location == CardLocation.MonsterZone
                || LastChainCard.Location == CardLocation.Removed)
                && !LastChainCard.IsDisabled() && !LastChainCard.IsShouldNotBeTarget()
                && !LastChainCard.IsShouldNotBeSpellTrapTarget()
                && this.Enemy.HasInGraveyard(LastChainCard.Id))
            {
                this.AI.SelectCard(LastChainCard.Id);
                return true;
            }

            if (this.Bot.BattlingMonster != null && this.Enemy.BattlingMonster != null)
            {
                if (!this.Enemy.BattlingMonster.IsDisabled() && this.Enemy.BattlingMonster.IsCode(DefaultExecutor.CardId.EaterOfMillions) && this.Enemy.HasInGraveyard(DefaultExecutor.CardId.EaterOfMillions))
                {
                    this.AI.SelectCard(this.Enemy.BattlingMonster.Id);
                    return true;
                }
            }

            if (this.Duel.Phase == DuelPhase.BattleStart && this.Duel.Player == 1 &&
                this.Enemy.HasInMonstersZone(DefaultExecutor.CardId.NumberS39UtopiaTheLightning, true) && this.Enemy.HasInGraveyard(DefaultExecutor.CardId.NumberS39UtopiaTheLightning))
            {
                this.AI.SelectCard(DefaultExecutor.CardId.NumberS39UtopiaTheLightning);
                return true;
            }

            return false;
        }

        public bool Borrelsword_ss()
        {
            if (this.Duel.Phase != DuelPhase.Main1)
            {
                return false;
            }

            if (this.Duel.Turn == 1)
            {
                return false;
            }

            if (this.wasStallioActivated)
            {
                return false;
            }

            List<ClientCard> material_list = new List<ClientCard>();
            List<ClientCard> bot_monster = this.Bot.GetMonsters();
            bot_monster.Sort(CardContainer.CompareCardAttack);
            //bot_monster.Reverse();
            int link_count = 0;
            foreach (ClientCard card in bot_monster)
            {
                if (card.IsFacedown())
                {
                    continue;
                }

                if (!material_list.Contains(card) && card.LinkCount < 3)
                {
                    material_list.Add(card);
                    link_count += (card.HasType(CardType.Link)) ? card.LinkCount : 1;
                }
            }
            if (link_count >= 4)
            {
                if (link_count > 4 && material_list.Where(x => x.Id == CardId.MirageStallio).Count() > 0)
                {
                    material_list.Remove(material_list.First(x => x.Id == CardId.MirageStallio));
                }
                this.AI.SelectMaterials(material_list);
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
                foreach (ClientCard card in enemy_list)
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
        public override void OnChainEnd()
        {
            if (!this.falcoHitGY && !this.falcoUsedReturnST && this.Bot.HasInGraveyard(CardId.Falco))
            {
                this.falcoHitGY = true;
            }
            else if (!this.Bot.HasInGraveyard(CardId.Falco))
            {
                this.falcoHitGY = false;
            }
            base.OnChainEnd();
        }

        public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
        {
            if (player == 0)
            {
                if (location == CardLocation.MonsterZone)
                {
                    if (this.Bot.GetMonstersInExtraZone().Where(x => x.Id == CardId.SunlightWolf).Count() > 1)
                    {
                        for (int i = 0; i < 7; ++i)
                        {
                            if (this.Bot.MonsterZone[i] != null && this.Bot.MonsterZone[i].IsCode(CardId.SunlightWolf))
                            {
                                int next_index = this.get_Wolf_linkzone();
                                if (next_index != -1 && (available & (int)(System.Math.Pow(2, next_index))) > 0)
                                {
                                    return (int)(System.Math.Pow(2, next_index));
                                }
                            }
                        }
                    }
                }
            }
            return base.OnSelectPlace(cardId, player, location, available);
        }
        public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
        {
            if (this.Util.IsTurn1OrMain2()
                &&
                (cardId == CardId.Gazelle
                || cardId == CardId.Spinny
                || cardId == CardId.Foxy))
            {
                return CardPosition.FaceUpDefence;
            }
            return 0;
        }

        public int SelectSetPlace(List<int> avoid_list = null, bool avoid = true)
        {
            List<int> list = new List<int>
            {
                5,
                6
            };
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

                if (this.Bot.MonsterZone[seq] == null || !avoid)
                {
                    if (avoid)
                    {
                        if (avoid_list != null && avoid_list.Contains(seq))
                        {
                            continue;
                        }

                        return zone;
                    }
                    else
                    {
                        if (avoid_list != null && avoid_list.Contains(seq))
                        {
                            return list.First(x => x == seq);
                        }
                        continue;
                    }

                };
            }
            return 0;
        }
        public override BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
        {
            foreach (ClientCard defender in defenders)
            {
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

                if (attacker.RealPower > defender.RealPower || (attacker.RealPower > defender.RealPower && attacker.IsLastAttacker && defender.IsAttack()))
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
    }
}
