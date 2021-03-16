using System;
using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using System.Linq;

namespace WindBot.Game.AI.Decks
{
    [Deck("Level VIII", "AI_Level8")]
    class Level8Executor : DefaultExecutor
    {
        public class CardId
        {
            public const int AngelTrumpeter = 87979586;
            public const int ScrapGolem = 82012319;
            public const int PhotonThrasher = 65367484;
            public const int WorldCarrotweightChampion = 44928016;
            public const int RaidenHandofTheLightsworn = 77558536;
            public const int ScrapBeast = 19139516;
            public const int PerformageTrickClown = 67696066;
            public const int MaskedChameleon = 53573406;
            public const int Goblindbergh = 25259669;
            public const int WhiteRoseDragon = 12213463;
            public const int RedRoseDragon = 26118970;
            public const int ScrapRecycler = 4334811;
            public const int MechaPhantomBeastOLion = 72291078;
            public const int MechaPhantomBeastOLionToken = 72291079;
            public const int JetSynchron = 9742784;

            public const int UnexpectedDai = 911883;
            public const int Raigeki = 12580477;
            public const int HarpiesFeatherDuster = 18144506;
            public const int ReinforcementofTheArmy = 32807846;
            public const int FoolishBurial = 81439173;
            public const int MonsterReborn = 83764718;
            public const int ChargeofTheLightBrigade = 94886282;
            public const int CalledbyTheGrave = 24224830;
            public const int SolemnStrike = 40605147;

            public const int WhiteAuraBihamut = 89907227;
            public const int BorreloadSavageDragon = 27548199;
            public const int CrystalWingSynchroDragon = 50954680;
            public const int ScarlightRedDragonArchfiend = 80666118;
            public const int PSYFramelordOmega = 74586817;
            public const int ScrapDragon = 76774528;
            public const int BlackRoseMoonlightDragon = 33698022;
            public const int ShootingRiserDragon = 68431965;
            public const int CoralDragon = 42566602;
            public const int GardenRoseMaiden = 53325667;
            public const int Number41BagooskaTheTerriblyTiredTapir = 90590303;
            public const int MekkKnightCrusadiaAstram = 21887175;
            public const int ScrapWyvern = 47363932;
            public const int CrystronNeedlefiber = 50588353;
        }

        public Level8Executor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.Activate, CardId.CalledbyTheGrave, this.DefaultCalledByTheGrave);
            this.AddExecutor(ExecutorType.Activate, CardId.Raigeki);
            this.AddExecutor(ExecutorType.Activate, CardId.HarpiesFeatherDuster);

            this.AddExecutor(ExecutorType.Repos, CardId.Number41BagooskaTheTerriblyTiredTapir, this.MonsterRepos);

            this.AddExecutor(ExecutorType.Activate, CardId.CrystalWingSynchroDragon, this.CrystalWingSynchroDragonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.BorreloadSavageDragon, this.BorreloadSavageDragonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.ScrapGolem, this.ScrapGolemEffect);

            // empty field
            this.AddExecutor(ExecutorType.Activate, CardId.UnexpectedDai, this.UnexpectedDaiFirst);
            this.AddExecutor(ExecutorType.SpSummon, CardId.PhotonThrasher, this.PhotonThrasherSummonFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.UnexpectedDai);
            this.AddExecutor(ExecutorType.SpSummon, CardId.PhotonThrasher);

            // 
            this.AddExecutor(ExecutorType.Activate, CardId.ReinforcementofTheArmy, this.ReinforcementofTheArmyEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.FoolishBurial, this.FoolishBurialEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.MonsterReborn, this.DefaultCallOfTheHaunted);

            // scrap combo
            this.AddExecutor(ExecutorType.Summon, CardId.ScrapRecycler, this.ScrapRecyclerSummonFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.ScrapRecycler, this.ScrapRecyclerEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.MechaPhantomBeastOLion, this.MechaPhantomBeastOLionEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.ScrapWyvern, this.ScrapWyvernSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.ScrapWyvern, this.ScrapWyvernEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.JetSynchron, this.JetSynchronEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.CrystronNeedlefiber, this.CrystronNeedlefiberSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.CrystronNeedlefiber, this.CrystronNeedlefiberEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.MekkKnightCrusadiaAstram, this.MekkKnightCrusadiaAstramSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.MekkKnightCrusadiaAstram, this.MekkKnightCrusadiaAstramEffect);

            //
            this.AddExecutor(ExecutorType.Activate, CardId.ChargeofTheLightBrigade);

            // other summon
            this.AddExecutor(ExecutorType.Summon, CardId.Goblindbergh, this.GoblindberghSummonFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.Goblindbergh, this.GoblindberghEffect);
            this.AddExecutor(ExecutorType.Summon, CardId.MaskedChameleon, this.MaskedChameleonSummonFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.MaskedChameleon, this.MaskedChameleonEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.WhiteRoseDragon);
            this.AddExecutor(ExecutorType.Summon, CardId.WhiteRoseDragon, this.WhiteRoseDragonSummonFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.WhiteRoseDragon, this.WhiteRoseDragonEffect);

            //
            this.AddExecutor(ExecutorType.Summon, CardId.RaidenHandofTheLightsworn, this.L4TunerSummonFirst);
            this.AddExecutor(ExecutorType.Summon, CardId.ScrapBeast, this.L4TunerSummonFirst);
            this.AddExecutor(ExecutorType.Summon, CardId.AngelTrumpeter, this.L4TunerSummonFirst);
            this.AddExecutor(ExecutorType.Summon, CardId.MaskedChameleon, this.L4TunerSummonFirst);

            this.AddExecutor(ExecutorType.Summon, CardId.PerformageTrickClown, this.L4NonTunerSummonFirst);
            this.AddExecutor(ExecutorType.Summon, CardId.Goblindbergh, this.L4NonTunerSummonFirst);
            this.AddExecutor(ExecutorType.Summon, CardId.WorldCarrotweightChampion, this.L4NonTunerSummonFirst);
            this.AddExecutor(ExecutorType.Summon, CardId.WhiteRoseDragon, this.L4NonTunerSummonFirst);

            this.AddExecutor(ExecutorType.Summon, CardId.RedRoseDragon, this.OtherTunerSummonFirst);
            this.AddExecutor(ExecutorType.Summon, CardId.JetSynchron, this.OtherTunerSummonFirst);
            this.AddExecutor(ExecutorType.Summon, CardId.MechaPhantomBeastOLion, this.OtherTunerSummonFirst);

            this.AddExecutor(ExecutorType.Summon, CardId.RaidenHandofTheLightsworn);
            this.AddExecutor(ExecutorType.Summon, CardId.Goblindbergh);
            this.AddExecutor(ExecutorType.Summon, CardId.ScrapBeast);
            this.AddExecutor(ExecutorType.Summon, CardId.PerformageTrickClown);
            this.AddExecutor(ExecutorType.Summon, CardId.AngelTrumpeter);
            this.AddExecutor(ExecutorType.Summon, CardId.WorldCarrotweightChampion);
            this.AddExecutor(ExecutorType.Summon, CardId.MaskedChameleon);
            this.AddExecutor(ExecutorType.Summon, CardId.WhiteRoseDragon);

            this.AddExecutor(ExecutorType.Summon, CardId.RedRoseDragon);
            this.AddExecutor(ExecutorType.Summon, CardId.JetSynchron);
            this.AddExecutor(ExecutorType.Summon, CardId.MechaPhantomBeastOLion);

            this.AddExecutor(ExecutorType.Summon, CardId.ScrapRecycler);

            this.AddExecutor(ExecutorType.Activate, CardId.RedRoseDragon);
            this.AddExecutor(ExecutorType.Activate, CardId.RaidenHandofTheLightsworn);
            this.AddExecutor(ExecutorType.Activate, CardId.PerformageTrickClown, this.PerformageTrickClownEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.WorldCarrotweightChampion, this.WorldCarrotweightChampionEffect);

            // extra monsters
            this.AddExecutor(ExecutorType.SpSummon, CardId.BorreloadSavageDragon, this.BorreloadSavageDragonSummon);

            this.AddExecutor(ExecutorType.SpSummon, CardId.ScrapDragon, this.ScrapDragonSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.ScrapDragon, this.ScrapDragonEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.CrystalWingSynchroDragon);

            this.AddExecutor(ExecutorType.SpSummon, CardId.ScarlightRedDragonArchfiend, this.DefaultScarlightRedDragonArchfiendSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.ScarlightRedDragonArchfiend, this.DefaultScarlightRedDragonArchfiendEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.PSYFramelordOmega);
            this.AddExecutor(ExecutorType.Activate, CardId.PSYFramelordOmega, this.PSYFramelordOmegaEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.WhiteAuraBihamut);
            this.AddExecutor(ExecutorType.Activate, CardId.WhiteAuraBihamut);

            this.AddExecutor(ExecutorType.SpSummon, CardId.GardenRoseMaiden);
            this.AddExecutor(ExecutorType.Activate, CardId.GardenRoseMaiden);

            this.AddExecutor(ExecutorType.SpSummon, CardId.CoralDragon);
            this.AddExecutor(ExecutorType.Activate, CardId.CoralDragon, this.CoralDragonEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.ShootingRiserDragon, this.ShootingRiserDragonSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.ShootingRiserDragon, this.ShootingRiserDragonEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.BlackRoseMoonlightDragon);

            this.AddExecutor(ExecutorType.SpSummon, CardId.Number41BagooskaTheTerriblyTiredTapir, this.Number41BagooskaTheTerriblyTiredTapirSummon);

            this.AddExecutor(ExecutorType.Summon, CardId.ScrapGolem, this.ScrapGolemSummon);

            this.AddExecutor(ExecutorType.SpellSet, CardId.CalledbyTheGrave);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnStrike);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, this.DefaultSolemnStrike);
            this.AddExecutor(ExecutorType.Repos, this.MonsterRepos);
        }

        private bool BeastOLionUsed = false;
        private bool JetSynchronUsed = false;
        private bool ScrapWyvernUsed = false;
        private bool MaskedChameleonUsed = false;
        private int ShootingRiserDragonCount = 0;

        private int[] HandCosts = new[]
        {
            CardId.PerformageTrickClown,
            CardId.JetSynchron,
            CardId.MechaPhantomBeastOLion,
            CardId.ScrapGolem,
            CardId.WorldCarrotweightChampion
        };

        private int[] L4NonTuners = new[]
        {
            CardId.PhotonThrasher,
            CardId.WorldCarrotweightChampion,
            CardId.PerformageTrickClown,
            CardId.Goblindbergh,
            CardId.WhiteRoseDragon
        };

        private int[] L4Tuners = new[]
        {
            CardId.RaidenHandofTheLightsworn,
            CardId.ScrapBeast,
            CardId.AngelTrumpeter,
            CardId.MaskedChameleon
        };

        public override void OnNewTurn()
        {
            this.BeastOLionUsed = false;
            this.JetSynchronUsed = false;
            this.ScrapWyvernUsed = false;
            this.MaskedChameleonUsed = false;
            this.ShootingRiserDragonCount = 0;
        }

        public override void OnChainEnd()
        {
            
        }

        public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
        {
            YGOSharp.OCGWrapper.NamedCard cardData = YGOSharp.OCGWrapper.NamedCard.Get(cardId);
            if (cardData != null)
            {
                if (cardData.Attack <= 1000)
                {
                    return CardPosition.FaceUpDefence;
                }
            }
            return 0;
        }

        public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
        {
            if (location == CardLocation.SpellZone)
            {
                if (cardId == CardId.MekkKnightCrusadiaAstram || cardId == CardId.ScrapWyvern || cardId == CardId.CrystronNeedlefiber)
                {
                    ClientCard b = this.Bot.MonsterZone.GetFirstMatchingCard(card => card.Id == CardId.BorreloadSavageDragon);
                    int zone = (1 << (b?.Sequence ?? 0)) & available;
                    if (zone > 0)
                    {
                        return zone;
                    }
                }
                if ((available & Zones.MonsterZone5) > 0)
                {
                    return Zones.MonsterZone5;
                }

                if ((available & Zones.MonsterZone4) > 0)
                {
                    return Zones.MonsterZone4;
                }

                if ((available & Zones.MonsterZone3) > 0)
                {
                    return Zones.MonsterZone3;
                }

                if ((available & Zones.MonsterZone2) > 0)
                {
                    return Zones.MonsterZone2;
                }

                if ((available & Zones.MonsterZone1) > 0)
                {
                    return Zones.MonsterZone1;
                }
            }
            if (location == CardLocation.MonsterZone)
            {
                if ((available & Zones.ExtraZone2) > 0)
                {
                    return Zones.ExtraZone2;
                }

                if ((available & Zones.ExtraZone1) > 0)
                {
                    return Zones.ExtraZone1;
                }

                if ((available & Zones.MonsterZone1) > 0)
                {
                    return Zones.MonsterZone1;
                }

                if ((available & Zones.MonsterZone3) > 0)
                {
                    return Zones.MonsterZone3;
                }

                if ((available & Zones.MonsterZone5) > 0)
                {
                    return Zones.MonsterZone5;
                }

                if ((available & Zones.MonsterZone2) > 0)
                {
                    return Zones.MonsterZone2;
                }

                if ((available & Zones.MonsterZone4) > 0)
                {
                    return Zones.MonsterZone4;
                }
            }
            return 0;
        }

        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (!defender.IsMonsterHasPreventActivationEffectInBattle())
            {
                if (!attacker.IsDisabled() && (attacker.IsCode(CardId.MekkKnightCrusadiaAstram) && defender.IsSpecialSummoned
                                            || attacker.IsCode(CardId.CrystalWingSynchroDragon) && defender.Level>=5))
                {
                    attacker.RealPower = attacker.RealPower + defender.Attack;
                }
            }
            return base.OnPreBattleBetween(attacker, defender);
        }

        private bool UnexpectedDaiFirst()
        {
            if (this.Bot.HasInHand(CardId.ScrapRecycler))
            {
                return true;
            }

            if (this.Bot.HasInHand(new[] {
                CardId.WorldCarrotweightChampion,
                CardId.PerformageTrickClown,
                CardId.Goblindbergh,
                CardId.WhiteRoseDragon
            }))
            {
                return true;
            }

            return false;
        }

        private bool PhotonThrasherSummonFirst()
        {
            if (this.Bot.HasInHand(CardId.ScrapRecycler))
            {
                return true;
            }

            if (this.Bot.HasInHand(this.L4Tuners))
            {
                return true;
            }

            return false;
        }

        private bool ReinforcementofTheArmyEffect()
        {
            if (this.Bot.GetMonsterCount() == 0 && this.PhotonThrasherSummonFirst() && !this.Bot.HasInHand(CardId.PhotonThrasher))
            {
                this.AI.SelectCard(CardId.PhotonThrasher);
                return true;
            }
            if (this.GoblindberghSummonFirst() && !this.Bot.HasInHand(CardId.Goblindbergh))
            {
                this.AI.SelectCard(CardId.Goblindbergh);
                return true;
            }
            this.AI.SelectCard(new[] {
                CardId.Goblindbergh,
                CardId.RaidenHandofTheLightsworn,
                CardId.PhotonThrasher
            });
            return true;
        }

        private bool FoolishBurialEffect()
        {
            if (!this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.ScrapRecycler))
            {
                this.AI.SelectCard(CardId.ScrapRecycler);
                return true;
            }
            if (this.L4NonTunerSummonFirst() && this.Bot.GetRemainingCount(CardId.PerformageTrickClown, 1) > 0)
            {
                this.AI.SelectCard(CardId.PerformageTrickClown);
                return true;
            }
            this.AI.SelectCard(new[] {
                CardId.JetSynchron,
                CardId.MechaPhantomBeastOLion,
                CardId.ScrapBeast,
                CardId.PhotonThrasher
            });
            return true;
        }

        private bool ScrapRecyclerSummonFirst()
        {
            return this.Bot.GetRemainingCount(CardId.ScrapGolem, 2) > 0 && this.Bot.GetRemainingCount(CardId.MechaPhantomBeastOLion, 2) > 0 && this.Bot.GetRemainingCount(CardId.JetSynchron, 2) > 0;
        }

        private bool ScrapRecyclerEffect()
        {
            if ((this.Bot.HasInMonstersZone(CardId.ScrapGolem) && !this.JetSynchronUsed) || this.BeastOLionUsed)
            {
                this.AI.SelectCard(new[] { CardId.JetSynchron, CardId.MechaPhantomBeastOLion });
            }
            else
            {
                this.AI.SelectCard(new[] { CardId.MechaPhantomBeastOLion, CardId.JetSynchron });
            }
            return true;
        }

        private bool MechaPhantomBeastOLionEffect()
        {
            if (this.ActivateDescription == -1)
            {
                this.BeastOLionUsed = true;
                return true;
            }
            // todo: need tuner check
            return !this.BeastOLionUsed;
        }

        private bool ScrapWyvernSummon()
        {
            if (this.ScrapWyvernUsed || this.MaskedChameleonUsed || this.Bot.HasInMonstersZone(CardId.ScrapWyvern))
            {
                return false;
            }

            if (!this.Bot.HasInMonstersZone(new[] {
                CardId.ScrapBeast,
                CardId.ScrapGolem,
                CardId.ScrapRecycler,
            }) || !this.Bot.HasInMonstersZoneOrInGraveyard(CardId.ScrapRecycler))
            {
                return false;
            }

            this.AI.SelectMaterials(new[] {
                CardId.MechaPhantomBeastOLionToken,
                CardId.PhotonThrasher,
                CardId.Goblindbergh,
                CardId.AngelTrumpeter,
                CardId.PerformageTrickClown,
                CardId.WorldCarrotweightChampion,
                CardId.WhiteRoseDragon,
                CardId.ScrapBeast,
                CardId.ScrapGolem,
                CardId.ScrapRecycler
            });
            return true;
        }

        private bool ScrapWyvernEffect()
        {
            if(this.ActivateDescription != -1)
            {
                int[] targets = new[]
                {
                    CardId.ScrapRecycler,
                    CardId.ScrapBeast,
                    CardId.ScrapGolem,
                    CardId.ScrapDragon
                };
                this.AI.SelectCard(targets);
                this.AI.SelectNextCard(targets);
                this.ScrapWyvernUsed = true;
                return true;
            }
            else
            {
                this.AI.SelectCard(new[]
                {
                    CardId.ScrapGolem,
                    CardId.ScrapBeast
                });
                ClientCard target = this.Util.GetBestEnemyCard();
                if (target != null)
                {
                    this.AI.SelectNextCard(target);
                }
                else
                {
                    this.AI.SelectNextCard(new[]
                    {
                        CardId.CalledbyTheGrave,
                        CardId.PhotonThrasher,
                        CardId.PerformageTrickClown,
                        CardId.MechaPhantomBeastOLionToken,
                        CardId.WorldCarrotweightChampion,
                        CardId.WhiteRoseDragon,
                        CardId.Goblindbergh,
                        CardId.AngelTrumpeter,
                        CardId.ScrapWyvern
                    });
                }

                return true;
            }
        }

        private bool ScrapGolemEffect()
        {
            if (this.Bot.GetMonstersInMainZone().Count == 5)
            {
                return false;
            }

            this.AI.SelectCard(CardId.ScrapRecycler);
            this.AI.SelectOption(0);
            return true;
        }

        private bool JetSynchronEffect()
        {
            if (!this.Bot.HasInMonstersZone(CardId.BlackRoseMoonlightDragon)
                && this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsFaceup() && card.Level >= 2 && card.Level <= 5) < 2)
            {
                return false;
            }

            this.AI.SelectCard(this.HandCosts);
            return true;
        }

        private bool CrystronNeedlefiberSummon()
        {
            if (this.MaskedChameleonUsed)
            {
                return false;
            }

            int nonTunerCount = this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsFaceup() && !card.IsTuner());
            if (this.Bot.GetMonsterCount() < 3 || nonTunerCount == 0)
            {
                return false;
            }

            if (nonTunerCount == 1)
            {
                this.AI.SelectMaterials(new[] {
                    CardId.JetSynchron,
                    CardId.MechaPhantomBeastOLion,
                    CardId.AngelTrumpeter,
                    CardId.RaidenHandofTheLightsworn,
                    CardId.ScrapBeast,
                    CardId.MaskedChameleon,

                    CardId.PerformageTrickClown,
                    CardId.MechaPhantomBeastOLionToken,
                    CardId.ScrapRecycler,
                    CardId.WhiteRoseDragon,
                    CardId.PhotonThrasher,
                    CardId.Goblindbergh,
                    CardId.WorldCarrotweightChampion
                });
            }
            else
            {
                this.AI.SelectMaterials(new[] {
                    CardId.MechaPhantomBeastOLionToken,
                    CardId.ScrapRecycler,
                    CardId.WhiteRoseDragon,
                    CardId.PhotonThrasher,
                    CardId.Goblindbergh,
                    CardId.WorldCarrotweightChampion,
                    CardId.PerformageTrickClown,

                    CardId.JetSynchron,
                    CardId.MechaPhantomBeastOLion,
                    CardId.AngelTrumpeter,
                    CardId.RaidenHandofTheLightsworn,
                    CardId.ScrapBeast,
                    CardId.MaskedChameleon
                });
            }
            return true;
        }

        private bool CrystronNeedlefiberEffect()
        {
            if (this.Duel.Player == 0)
            {
                this.AI.SelectCard(CardId.RedRoseDragon);
                return true;
            }
            else
            {
                if (this.Bot.HasInExtra(CardId.ShootingRiserDragon) && this.Bot.MonsterZone.IsExistingMatchingCard(card => card.Level >= 3 && card.Level <= 5 && card.IsFaceup() && !card.IsTuner()))
                {
                    this.AI.SelectCard(CardId.ShootingRiserDragon);
                    return true;
                }
                if (this.Util.IsOneEnemyBetterThanValue(1500, true) || this.DefaultOnBecomeTarget())
                {
                    this.AI.SelectCard(CardId.CoralDragon);
                    return true;
                }
                return false;
            }
        }

        private bool MekkKnightCrusadiaAstramSummon()
        {
            int[] matcodes = new[] {
                CardId.ScrapWyvern,
                CardId.CrystronNeedlefiber
            };
            if (this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsCode(matcodes)) < 2)
            {
                return false;
            }

            this.AI.SelectMaterials(matcodes);
            return true;
        }

        private bool MekkKnightCrusadiaAstramEffect()
        {
            if (this.Card.Location == CardLocation.MonsterZone)
            {
                return true;
            }
            else
            {
                ClientCard target = this.Util.GetBestEnemyCard();
                if (target == null)
                {
                    return false;
                }

                this.AI.SelectCard(target);
                return true;
            }
        }

        private bool GoblindberghSummonFirst()
        {
            if (this.Bot.HasInHand(this.L4Tuners))
            {
                return true;
            }

            return false;
        }

        private bool GoblindberghEffect()
        {
            this.AI.SelectCard(this.L4Tuners);
            return true;
        }

        private bool MaskedChameleonSummonFirst()
        {
            if (this.Bot.HasInGraveyard(new[] {
                CardId.PhotonThrasher,
                CardId.WorldCarrotweightChampion,
                CardId.Goblindbergh
            }))
            {
                return true;
            }

            return false;
        }

        private bool MaskedChameleonEffect()
        {
            if (this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsFaceup() && !card.IsTuner()) == 0)
            {
                this.MaskedChameleonUsed = true;
                this.AI.SelectCard(this.L4NonTuners);
                return true;
            }
            return false;
        }

        private bool WhiteRoseDragonSummonFirst()
        {
            if (this.Bot.HasInGraveyard(new[] {
                CardId.RedRoseDragon
            }))
            {
                return true;
            }

            return false;
        }

        private bool WhiteRoseDragonEffect()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                if (this.Bot.GetRemainingCount(CardId.WorldCarrotweightChampion, 1) > 0)
                {
                    this.AI.SelectCard(CardId.WorldCarrotweightChampion);
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool L4TunerSummonFirst()
        {
            return this.Bot.HasInMonstersZone(this.L4NonTuners, faceUp: true);
        }

        private bool L4NonTunerSummonFirst()
        {
            return this.Bot.HasInMonstersZone(this.L4Tuners, faceUp: true);
        }

        private bool OtherTunerSummonFirst()
        {
            return this.Bot.HasInMonstersZone(this.L4NonTuners, faceUp: true);
        }

        private bool PerformageTrickClownEffect()
        {
            if (this.Bot.LifePoints <= 1000)
            {
                return false;
            }

            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }

        private bool WorldCarrotweightChampionEffect()
        {
            return !this.Bot.HasInMonstersZone(this.L4NonTuners);
        }

        private bool ScrapGolemSummon()
        {
            return this.Bot.GetMonsterCount() <= 2 && this.Bot.HasInMonstersZoneOrInGraveyard(CardId.ScrapRecycler);
        }

        private bool BorreloadSavageDragonSummon()
        {
            if (!this.Bot.HasInGraveyard(new[] {
                CardId.ScrapWyvern,
                CardId.CrystronNeedlefiber,
                CardId.MekkKnightCrusadiaAstram
            }))
            {
                return false;
            }

            return true;
        }

        private bool BorreloadSavageDragonEffect()
        {
            if (this.ActivateDescription == -1)
            {
                this.AI.SelectCard(new[] { CardId.MekkKnightCrusadiaAstram, CardId.CrystronNeedlefiber, CardId.ScrapWyvern });
                return true;
            }
            else
            {
                return true;
            }
        }

        private bool ScrapDragonSummon()
        {
            if (this.Util.GetProblematicEnemyCard(3000) != null)
            {
                return true;
            }
            if (this.Bot.HasInGraveyard(new[] { CardId.ScrapBeast, CardId.ScrapRecycler, CardId.ScrapGolem, CardId.ScrapWyvern }))
            {
                return true;
            }
            return false;
        }

        private bool ScrapDragonEffect()
        {
            ClientCard invincible = this.Util.GetProblematicEnemyCard(3000);
            if (invincible == null && !this.Util.IsOneEnemyBetterThanValue(2800 - 1, false))
            {
                return false;
            }

            List<ClientCard> monsters = this.Enemy.GetMonsters();
            monsters.Sort(CardContainer.CompareCardAttack);

            ClientCard destroyCard = invincible;
            if (destroyCard == null)
            {
                for (int i = monsters.Count - 1; i >= 0; --i)
                {
                    if (monsters[i].IsAttack())
                    {
                        destroyCard = monsters[i];
                        break;
                    }
                }
            }

            if (destroyCard == null)
            {
                return false;
            }

            this.AI.SelectCard(new[] {
                CardId.CalledbyTheGrave,
                CardId.MechaPhantomBeastOLionToken,
                CardId.ScrapRecycler,
                CardId.WhiteRoseDragon,
                CardId.PhotonThrasher,
                CardId.Goblindbergh,
                CardId.WorldCarrotweightChampion,
                CardId.PerformageTrickClown,
                CardId.JetSynchron,
                CardId.MechaPhantomBeastOLion,
                CardId.AngelTrumpeter,
                CardId.RaidenHandofTheLightsworn,
                CardId.ScrapBeast,
                CardId.MaskedChameleon
            });
            this.AI.SelectNextCard(destroyCard);

            return true;
        }

        private bool CrystalWingSynchroDragonEffect()
        {
            return this.Duel.LastChainPlayer != 0;
        }

        private bool PSYFramelordOmegaEffect()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                // todo
                return false;
            }
            if (this.Duel.Player == 0)
            {
                return this.DefaultOnBecomeTarget();
            }
            if (this.Duel.Player == 1)
            {
                if (this.Duel.Phase == DuelPhase.Standby)
                {
                    if (this.Bot.HasInBanished(CardId.JetSynchron) && !this.Bot.HasInGraveyard(CardId.JetSynchron))
                    {
                        this.AI.SelectCard(CardId.JetSynchron);
                        return true;
                    }
                    if (this.Bot.HasInBanished(CardId.CrystronNeedlefiber))
                    {
                        this.AI.SelectCard(CardId.CrystronNeedlefiber);
                        return true;
                    }
                }
                else
                {
                    if (this.Enemy.MonsterZone.GetMatchingCards(card => card.IsAttack()).Sum(card => card.Attack) >= this.Bot.LifePoints)
                    {
                        return false;
                    }

                    return true;// DefaultOnBecomeTarget() || Util.IsOneEnemyBetterThanValue(2800, true);
                }
            }
            return false;
        }

        private bool CoralDragonEffect()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                return true;
            }

            ClientCard target = this.Util.GetProblematicEnemyCard(canBeTarget: true);
            if (target != null)
            {
                this.AI.SelectCard(this.HandCosts);
                this.AI.SelectNextCard(target);
                return true;
            }
            return false;
        }

        private bool ShootingRiserDragonSummon()
        {
            return this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsFaceup() && !card.IsTuner()) >= 2;
        }

        private bool ShootingRiserDragonEffect()
        {
            if (this.ActivateDescription == -1 || (this.ActivateDescription == this.Util.GetStringId(CardId.ShootingRiserDragon, 0)))
            {
                int targetLevel = 8;

                if (this.Bot.MonsterZone.IsExistingMatchingCard(card => card.Level == targetLevel - 5 && card.IsFaceup() && !card.IsTuner()) && this.Bot.GetRemainingCount(CardId.MechaPhantomBeastOLion, 2) > 0)
                {
                    this.AI.SelectCard(CardId.MechaPhantomBeastOLion);
                }
                else if (this.Bot.MonsterZone.IsExistingMatchingCard(card => card.Level == targetLevel - 4 && card.IsFaceup() && !card.IsTuner()))
                {
                    this.AI.SelectCard(new[] {
                        CardId.ScrapRecycler,
                        CardId.RedRoseDragon
                    });
                }
                else if (this.Bot.MonsterZone.IsExistingMatchingCard(card => card.Level == targetLevel - 3 && card.IsFaceup() && !card.IsTuner()))
                {
                    this.AI.SelectCard(new[] {
                        CardId.ScrapBeast,
                        CardId.PhotonThrasher,
                        CardId.Goblindbergh,
                        CardId.WorldCarrotweightChampion,
                        CardId.WhiteRoseDragon,
                        CardId.RaidenHandofTheLightsworn,
                        CardId.AngelTrumpeter,
                        CardId.PerformageTrickClown,
                        CardId.MaskedChameleon
                    });
                }
                else
                {
                    this.FoolishBurialEffect();
                }
                return true;
            }
            else
            {
                if (this.Duel.LastChainPlayer == 0 || this.ShootingRiserDragonCount >= 10)
                {
                    return false;
                }

                this.ShootingRiserDragonCount++;
                this.AI.SelectCard(new[] {
                    CardId.BlackRoseMoonlightDragon,
                    CardId.ScrapDragon,
                    CardId.PSYFramelordOmega
                });
                return true;
            }
        }

        private bool Number41BagooskaTheTerriblyTiredTapirSummon()
        {
            if (!this.Util.IsTurn1OrMain2())
            {
                return false;
            }

            if (this.Bot.GetMonsterCount() > 3)
            {
                return false;
            }

            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }

        private bool MonsterRepos()
        {
            if (this.Card.IsFacedown())
            {
                return true;
            }

            if (this.Card.IsCode(CardId.Number41BagooskaTheTerriblyTiredTapir) && this.Card.IsDefense())
            {
                return this.Card.Overlays.Count == 0;
            }

            return this.DefaultMonsterRepos();
        }
    }
}
