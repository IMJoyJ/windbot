using System;
using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using System.Linq;

namespace WindBot.Game.AI.Decks
{
    [Deck("Orcust", "AI_Orcust")]
    class OrcustExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int OrcustKnightmare = 4055337;
            public const int OrcustHarpHorror = 57835716;
            public const int OrcustCymbalSkeleton = 21441617;
            public const int WorldLegacyWorldWand = 93920420;
            public const int ThePhantomKnightsofAncientCloak = 90432163;
            public const int ThePhantomKnightsofSilentBoots = 36426778;

            public const int TrickstarCarobein = 98169343;
            public const int TrickstarCandina = 61283655;
            public const int ArmageddonKnight = 28985331;
            public const int ScrapRecycler = 4334811;
            public const int DestrudoTheLostDragonsFrisson = 5560911;
            public const int JetSynchron = 9742784;

            public const int AshBlossomJoyousSpring = 14558127;
            public const int GhostBelleHauntedMansion = 73642296;
            public const int MaxxC = 23434538;

            public const int SkyStrikerMobilizeEngage = 63166095;
            public const int SkyStrikerMechaEagleBooster = 25733157;
            public const int SkyStrikerMechaHornetDrones = 52340444;
            public const int SkyStrikerMechaHornetDronesToken = 52340445;
            public const int TrickstarLightStage = 35371948;
            public const int OrcustratedBabel = 90351981;

            public const int ReinforcementofTheArmy = 32807846;
            public const int Terraforming = 73628505;
            public const int FoolishBurial = 81439173;
            public const int CalledbyTheGrave = 24224830;

            public const int ThePhantomKnightsofShadeBrigandine = 98827725;
            public const int PhantomKnightsFogBlade = 25542642;
            public const int OrcustratedClimax = 703897;

            public const int BorreloadSavageDragon = 27548199;
            public const int ShootingRiserDragon = 68431965;
            public const int SheorcustDingirsu = 93854893;
            public const int BorrelswordDragon = 85289965;
            public const int LongirsuTheOrcustOrchestrator = 76145142;
            public const int ThePhantomKnightsofRustyBardiche = 26692769;
            public const int KnightmarePhoenix = 2857636;
            public const int GalateaTheOrcustAutomaton = 30741503;
            public const int CrystronNeedlefiber = 50588353;
            public const int SkyStrikerAceKagari = 63288573;
            public const int KnightmareMermaid = 3679218;
            public const int SalamangreatAlmiraj = 60303245;
        }

        public OrcustExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.Activate, CardId.SkyStrikerMechaEagleBooster, this.EagleBoosterEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.OrcustratedClimax, this.ClimaxEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.MaxxC, this.DefaultMaxxC);
            this.AddExecutor(ExecutorType.Activate, CardId.AshBlossomJoyousSpring, this.DefaultAshBlossomAndJoyousSpring);
            this.AddExecutor(ExecutorType.Activate, CardId.GhostBelleHauntedMansion, this.DefaultGhostBelleAndHauntedMansion);
            this.AddExecutor(ExecutorType.Activate, CardId.CalledbyTheGrave, this.DefaultCalledByTheGrave);

            this.AddExecutor(ExecutorType.Activate, CardId.Terraforming, this.TerraformingEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.ReinforcementofTheArmy, this.ReinforcementofTheArmyEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.FoolishBurial, this.FoolishBurialEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.TrickstarLightStage, this.LightStageEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.SkyStrikerMobilizeEngage, this.EngageEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.SkyStrikerMechaHornetDrones, this.DronesEffectFirst);
            this.AddExecutor(ExecutorType.SpSummon, CardId.SkyStrikerAceKagari);
            this.AddExecutor(ExecutorType.Activate, CardId.SkyStrikerAceKagari);

            this.AddExecutor(ExecutorType.SpSummon, CardId.KnightmareMermaid, this.KnightmareMermaidSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.KnightmareMermaid, this.KnightmareMermaidEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.TrickstarCarobein, this.CarobeinSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.TrickstarCarobein);

            this.AddExecutor(ExecutorType.SpellSet, CardId.ThePhantomKnightsofShadeBrigandine);

            this.AddExecutor(ExecutorType.Summon, CardId.ArmageddonKnight, this.ArmageddonKnightSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.ArmageddonKnight, this.ArmageddonKnightEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.ScrapRecycler, this.ScrapRecyclerSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.ScrapRecycler, this.ScrapRecyclerEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.SkyStrikerMechaHornetDrones, this.DronesEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.JetSynchron, this.JetSynchronSummon);

            this.AddExecutor(ExecutorType.Activate, CardId.DestrudoTheLostDragonsFrisson, this.DestrudoSummon);

            this.AddExecutor(ExecutorType.SpSummon, CardId.CrystronNeedlefiber, this.NeedlefiberSummonFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.CrystronNeedlefiber, this.NeedlefiberEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.ShootingRiserDragon, this.ShootingRiserDragonEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.TrickstarCandina, this.CandinaSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.TrickstarCandina, this.CandinaEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.JetSynchron, this.OneCardComboSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.ThePhantomKnightsofAncientCloak, this.OneCardComboSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.ThePhantomKnightsofSilentBoots, this.OneCardComboSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.SalamangreatAlmiraj, this.AlmirajSummon);

            this.AddExecutor(ExecutorType.Activate, CardId.ThePhantomKnightsofShadeBrigandine, this.ShadeBrigandineSummonFirst);

            this.AddExecutor(ExecutorType.SpSummon, CardId.KnightmarePhoenix, this.KnightmarePhoenixSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.KnightmarePhoenix, this.KnightmarePhoenixEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.GalateaTheOrcustAutomaton, this.GalateaSummonFirst);

            this.AddExecutor(ExecutorType.Activate, CardId.JetSynchron, this.JetSynchronEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.OrcustKnightmare, this.OrcustKnightmareEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.OrcustHarpHorror, this.HarpHorrorEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.WorldLegacyWorldWand, this.WorldWandEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.ThePhantomKnightsofAncientCloak, this.AncientCloakEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.ThePhantomKnightsofRustyBardiche, this.RustyBardicheSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.ThePhantomKnightsofRustyBardiche, this.RustyBardicheEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.OrcustCymbalSkeleton, this.CymbalSkeletonEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.GalateaTheOrcustAutomaton, this.GalateaEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.SheorcustDingirsu, this.SheorcustDingirsuSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.SheorcustDingirsu, this.SheorcustDingirsuEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.ThePhantomKnightsofSilentBoots, this.SilentBootsSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.ThePhantomKnightsofShadeBrigandine, this.ShadeBrigandineSummonSecond);

            this.AddExecutor(ExecutorType.SpSummon, CardId.BorreloadSavageDragon);
            this.AddExecutor(ExecutorType.Activate, CardId.BorreloadSavageDragon, this.BorreloadSavageDragonEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.GalateaTheOrcustAutomaton, this.GalateaSummonSecond);

            this.AddExecutor(ExecutorType.Activate, CardId.ThePhantomKnightsofSilentBoots, this.SilentBootsEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.GhostBelleHauntedMansion, this.TunerSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.AshBlossomJoyousSpring, this.TunerSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.OrcustCymbalSkeleton, this.OtherSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.OrcustHarpHorror, this.OtherSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.ThePhantomKnightsofAncientCloak, this.LinkMaterialSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.MaxxC, this.LinkMaterialSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.ThePhantomKnightsofSilentBoots, this.LinkMaterialSummon);

            this.AddExecutor(ExecutorType.SpSummon, CardId.CrystronNeedlefiber, this.NeedlefiberSummonSecond);

            this.AddExecutor(ExecutorType.SpSummon, CardId.BorrelswordDragon, this.BorrelswordDragonSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.BorrelswordDragon, this.BorrelswordDragonEffect);

            this.AddExecutor(ExecutorType.SpellSet, CardId.PhantomKnightsFogBlade);
            this.AddExecutor(ExecutorType.Activate, CardId.PhantomKnightsFogBlade, this.FogBladeEffect);
            this.AddExecutor(ExecutorType.SpellSet, CardId.OrcustratedClimax);

            this.AddExecutor(ExecutorType.Activate, CardId.OrcustratedBabel, this.BabelEffect);

            this.AddExecutor(ExecutorType.Repos, this.MonsterRepos);
        }

        private bool NormalSummoned = false;
        private bool SheorcustDingirsuSummoned = false;
        private bool HarpHorrorUsed = false;
        private bool CymbalSkeletonUsed = false;
        private bool BorrelswordDragonUsed = false;
        private ClientCard RustyBardicheTarget = null;
        private int ShootingRiserDragonCount = 0;

        private readonly int[] HandCosts = new[]
        {
            CardId.OrcustCymbalSkeleton,
            CardId.OrcustKnightmare,
            CardId.DestrudoTheLostDragonsFrisson,
            CardId.WorldLegacyWorldWand,
            CardId.OrcustHarpHorror,
            CardId.ThePhantomKnightsofAncientCloak,
            CardId.ThePhantomKnightsofSilentBoots,
            CardId.JetSynchron,
            CardId.TrickstarLightStage,
            CardId.SkyStrikerMobilizeEngage,
            CardId.Terraforming,
            CardId.ReinforcementofTheArmy,
            CardId.MaxxC,
            CardId.GhostBelleHauntedMansion
        };

        public override bool OnSelectHand()
        {
            // go first
            return true;
        }

        public override void OnNewTurn()
        {
            this.NormalSummoned = false;
            this.SheorcustDingirsuSummoned = false;
            this.HarpHorrorUsed = false;
            this.CymbalSkeletonUsed = false;
            this.BorrelswordDragonUsed = false;
            this.RustyBardicheTarget = null;
            this.ShootingRiserDragonCount = 0;
        }

        public override void OnChainEnd()
        {
            this.RustyBardicheTarget = null;
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
                if (cardId == CardId.KnightmarePhoenix || cardId == CardId.CrystronNeedlefiber)
                {
                    ClientCard b = this.Bot.MonsterZone.GetFirstMatchingCard(card => card.Id == CardId.BorreloadSavageDragon);
                    int zone = (1 << (b?.Sequence ?? 0)) & available;
                    if (zone > 0)
                    {
                        return zone;
                    }
                }
                if ((available & Zones.MonsterZone1) > 0)
                {
                    return Zones.MonsterZone1;
                }

                if ((available & Zones.MonsterZone2) > 0)
                {
                    return Zones.MonsterZone2;
                }

                if ((available & Zones.MonsterZone3) > 0)
                {
                    return Zones.MonsterZone3;
                }

                if ((available & Zones.MonsterZone4) > 0)
                {
                    return Zones.MonsterZone4;
                }

                if ((available & Zones.MonsterZone5) > 0)
                {
                    return Zones.MonsterZone5;
                }
            }
            if (location == CardLocation.MonsterZone)
            {
                if (cardId == CardId.SheorcustDingirsu)
                {
                    ClientCard l = this.Bot.MonsterZone.GetFirstMatchingCard(card => card.Id == CardId.ThePhantomKnightsofRustyBardiche);
                    int zones = (l?.GetLinkedZones() ?? 0) & available;
                    if ((zones & Zones.MonsterZone5) > 0)
                    {
                        return Zones.MonsterZone5;
                    }

                    if ((zones & Zones.MonsterZone4) > 0)
                    {
                        return Zones.MonsterZone4;
                    }

                    if ((zones & Zones.MonsterZone3) > 0)
                    {
                        return Zones.MonsterZone3;
                    }

                    if ((zones & Zones.MonsterZone2) > 0)
                    {
                        return Zones.MonsterZone2;
                    }

                    if ((zones & Zones.MonsterZone1) > 0)
                    {
                        return Zones.MonsterZone1;
                    }
                }
                if (cardId == CardId.GalateaTheOrcustAutomaton)
                {
                    int zones = this.Bot.GetLinkedZones() & available;
                    if ((zones & Zones.MonsterZone1) > 0)
                    {
                        return Zones.MonsterZone1;
                    }

                    if ((zones & Zones.MonsterZone3) > 0)
                    {
                        return Zones.MonsterZone3;
                    }

                    if ((zones & Zones.MonsterZone2) > 0)
                    {
                        return Zones.MonsterZone2;
                    }

                    if ((zones & Zones.MonsterZone4) > 0)
                    {
                        return Zones.MonsterZone4;
                    }

                    if ((zones & Zones.MonsterZone5) > 0)
                    {
                        return Zones.MonsterZone5;
                    }
                }
                if (cardId == CardId.KnightmarePhoenix)
                {
                    if ((this.Enemy.MonsterZone[5]?.HasLinkMarker(CardLinkMarker.Top) ?? false) && (available & Zones.MonsterZone4) > 0)
                    {
                        return Zones.MonsterZone4;
                    }

                    if ((this.Enemy.MonsterZone[6]?.HasLinkMarker(CardLinkMarker.Top) ?? false) && (available & Zones.MonsterZone2) > 0)
                    {
                        return Zones.MonsterZone2;
                    }
                }

                if ((available & Zones.ExtraZone2) > 0)
                {
                    return Zones.ExtraZone2;
                }

                if ((available & Zones.ExtraZone1) > 0)
                {
                    return Zones.ExtraZone1;
                }

                if ((available & Zones.MonsterZone2) > 0)
                {
                    return Zones.MonsterZone2;
                }

                if ((available & Zones.MonsterZone4) > 0)
                {
                    return Zones.MonsterZone4;
                }

                if ((available & Zones.MonsterZone1) > 0)
                {
                    return Zones.MonsterZone1;
                }

                if ((available & Zones.MonsterZone5) > 0)
                {
                    return Zones.MonsterZone5;
                }

                if ((available & Zones.MonsterZone3) > 0)
                {
                    return Zones.MonsterZone3;
                }
            }
            return 0;
        }

        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (!defender.IsMonsterHasPreventActivationEffectInBattle())
            {
                if (attacker.IsCode(CardId.TrickstarCandina) && this.Bot.HasInHand(CardId.TrickstarCarobein))
                {
                    attacker.RealPower = attacker.RealPower + 1800;
                }

                if (attacker.IsCode(CardId.BorrelswordDragon) && !attacker.IsDisabled() && !this.BorrelswordDragonUsed)
                {
                    attacker.RealPower = attacker.RealPower + defender.GetDefensePower() / 2;
                    defender.RealPower = defender.RealPower - defender.GetDefensePower() / 2;
                }
            }
            return base.OnPreBattleBetween(attacker, defender);
        }

        private bool TerraformingEffect()
        {
            this.AI.SelectCard(CardId.TrickstarLightStage);
            return true;
        }

        private bool ReinforcementofTheArmyEffect()
        {
            this.AI.SelectCard(CardId.ArmageddonKnight);
            return true;
        }

        private bool FoolishBurialEffect()
        {
            this.AI.SelectCard(new[] {
                CardId.DestrudoTheLostDragonsFrisson,
                CardId.JetSynchron,
                CardId.OrcustHarpHorror,
                CardId.OrcustCymbalSkeleton
            });
            return true;
        }

        private bool LightStageEffect()
        {
            if (this.Card.Location == CardLocation.Hand || this.Card.IsFacedown())
            {
                ClientCard field = this.Bot.GetFieldSpellCard();
                if ((field?.IsCode(CardId.OrcustratedBabel) ?? false) && this.Bot.GetMonsterCount() > 1)
                {
                    return false;
                }

                if ((field?.IsCode(CardId.TrickstarLightStage) ?? false) && this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.TrickstarCandina) && this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.TrickstarCarobein))
                {
                    return false;
                }

                this.AI.SelectYesNo(true);
                if (this.Bot.HasInHandOrHasInMonstersZone(CardId.TrickstarCandina))
                {
                    this.AI.SelectCard(CardId.TrickstarCarobein);
                }
                else
                {
                    this.AI.SelectCard(CardId.TrickstarCandina);
                }

                return true;
            }
            ClientCard target = this.Enemy.SpellZone.GetFirstMatchingCard(card => card.IsFacedown());
            this.AI.SelectCard(target);
            return true;
        }

        private bool CarobeinSummon()
        {
            if (this.Bot.HasInMonstersZone(CardId.TrickstarCandina))
            {
                // TODO: beat mode
                return this.Bot.HasInExtra(CardId.KnightmarePhoenix);
            }
            else
            {
                return !this.NormalSummoned && this.Bot.Hand.IsExistingMatchingCard(card => card.Level <= 4);
            }
        }

        private bool EngageEffect()
        {
            bool needProtect = false;
            if (this.Bot.HasInHand(CardId.ArmageddonKnight))
            {
                needProtect = true;
            }
            else if (this.Bot.HasInHandOrInGraveyard(CardId.DestrudoTheLostDragonsFrisson) && this.Bot.Hand.IsExistingMatchingCard(card => card.Level <= 4))
            {
                needProtect = true;
            }
            else if (this.Bot.HasInHand(CardId.TrickstarCandina))
            {
                needProtect = true;
            }

            if (needProtect)
            {
                this.AI.SelectCard(CardId.SkyStrikerMechaEagleBooster);
            }
            else
            {
                this.AI.SelectCard(CardId.SkyStrikerMechaHornetDrones);
            }

            this.AI.SelectYesNo(true);
            return true;
        }

        private bool DronesEffectFirst()
        {
            return this.Bot.GetMonsterCount() == 0;
        }

        private bool DronesEffect()
        {
            return !this.Bot.HasInHand(CardId.ArmageddonKnight) && !this.Bot.HasInHand(CardId.TrickstarCandina);
        }

        private bool CandinaSummon()
        {
            this.NormalSummoned = true;
            return true;
        }

        private bool CandinaEffect()
        {
            this.AI.SelectCard(CardId.TrickstarLightStage);
            return true;
        }

        private bool ArmageddonKnightSummon()
        {
            this.NormalSummoned = true;
            return true;
        }

        private bool ArmageddonKnightEffect()
        {
            this.AI.SelectCard(new[] {
                CardId.DestrudoTheLostDragonsFrisson,
                CardId.OrcustHarpHorror
            });
            return true;
        }

        private bool ScrapRecyclerSummon()
        {
            this.NormalSummoned = true;
            return true;
        }

        private bool ScrapRecyclerEffect()
        {
            this.AI.SelectCard(new[] {
                CardId.JetSynchron,
                CardId.OrcustHarpHorror
            });
            return true;
        }

        private bool JetSynchronSummon()
        {
            if (this.Bot.GetMonsterCount() > 0)
            {
                this.NormalSummoned = true;
                return true;
            }
            return false;
        }

        private bool JetSynchronEffect()
        {
            this.AI.SelectCard(this.HandCosts);
            return true;
        }

        private bool AlmirajSummon()
        {
            if (this.Bot.GetMonsterCount() > 1)
            {
                return false;
            }

            ClientCard mat = this.Bot.GetMonsters().First();
            if (mat.IsCode(new[] {
                CardId.JetSynchron,
                CardId.ThePhantomKnightsofAncientCloak,
                CardId.ThePhantomKnightsofSilentBoots
            }))
            {
                this.AI.SelectMaterials(mat);
                return true;
            }
            return false;
        }

        private bool DestrudoSummon()
        {
            return this.Bot.GetMonsterCount() < 3 && this.Bot.HasInExtra(new[] { CardId.CrystronNeedlefiber, CardId.KnightmarePhoenix });
        }

        private bool NeedlefiberSummonFirst()
        {
            if (!this.Bot.HasInExtra(CardId.BorreloadSavageDragon))
            {
                return false;
            }

            if (!this.Bot.HasInHand(CardId.JetSynchron) && this.Bot.GetRemainingCount(CardId.JetSynchron, 1) == 0)
            {
                return false;
            }

            int[] matids = new[] {
                CardId.DestrudoTheLostDragonsFrisson,
                CardId.AshBlossomJoyousSpring,
                CardId.GhostBelleHauntedMansion,
                CardId.SkyStrikerMechaHornetDronesToken,
                CardId.TrickstarCarobein,
                CardId.SkyStrikerAceKagari,
                CardId.ScrapRecycler,
                CardId.ArmageddonKnight,
                CardId.TrickstarCandina,
                CardId.OrcustHarpHorror,
                CardId.OrcustCymbalSkeleton,
                CardId.ThePhantomKnightsofAncientCloak,
                CardId.ThePhantomKnightsofSilentBoots
            };
            if (this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsCode(matids)) >= 2)
            {
                this.AI.SelectMaterials(matids);
                return true;
            }
            return false;
        }

        private bool NeedlefiberSummonSecond()
        {
            IList<ClientCard> selected = new List<ClientCard>();

            ClientCard tuner = this.Bot.MonsterZone.GetFirstMatchingFaceupCard(card => card.IsCode(new[]
            {
                CardId.DestrudoTheLostDragonsFrisson,
                CardId.AshBlossomJoyousSpring,
                CardId.GhostBelleHauntedMansion,
                CardId.JetSynchron
            }));
            if (tuner != null)
            {
                selected.Add(tuner);
            }

            int[] matids = new[] {
                CardId.SkyStrikerMechaHornetDronesToken,
                CardId.ThePhantomKnightsofShadeBrigandine,
                CardId.SkyStrikerAceKagari,
                CardId.ScrapRecycler,
                CardId.ArmageddonKnight,
                CardId.OrcustHarpHorror,
                CardId.OrcustCymbalSkeleton,
                CardId.ThePhantomKnightsofAncientCloak,
                CardId.ThePhantomKnightsofSilentBoots
            };

            IList<ClientCard> mats = this.Bot.MonsterZone.GetMatchingCards(card => card.Attack <= 1700);

            for (int i = 0; i < matids.Length && selected.Count < 2; i++)
            {
                ClientCard c = mats.GetFirstMatchingFaceupCard(card => card.IsCode(matids[i]));
                if (c != null)
                {
                    selected.Add(c);
                    if (selected.Count == 2 && this.Util.GetBotAvailZonesFromExtraDeck(selected) == 0)
                    {
                        selected.Remove(c);
                    }
                }
            }

            if (selected.Count == 2)
            {
                this.AI.SelectMaterials(selected);
                return true;
            }
            return false;
        }

        private bool NeedlefiberEffect()
        {
            this.AI.SelectCard(CardId.JetSynchron);
            return true;
        }

        private bool ShootingRiserDragonEffect()
        {
            if (this.ActivateDescription == -1 || (this.ActivateDescription == this.Util.GetStringId(CardId.ShootingRiserDragon, 0)))
            {
                if (this.Bot.MonsterZone.IsExistingMatchingCard(card => card.Level == 3 && card.IsFaceup() && !card.IsTuner()) && this.Bot.GetRemainingCount(CardId.MaxxC, 3) > 0)
                {
                    this.AI.SelectCard(CardId.MaxxC);
                }
                else if (this.Bot.MonsterZone.IsExistingMatchingCard(card => card.Level == 4 && card.IsFaceup() && !card.IsTuner()))
                {
                    this.AI.SelectCard(new[] {
                        CardId.ThePhantomKnightsofAncientCloak,
                        CardId.ThePhantomKnightsofSilentBoots,
                        CardId.ScrapRecycler,
                        CardId.OrcustCymbalSkeleton,
                        CardId.AshBlossomJoyousSpring,
                        CardId.GhostBelleHauntedMansion
                    });
                }
                else if (this.Bot.MonsterZone.IsExistingMatchingCard(card => card.Level == 5 && card.IsFaceup() && !card.IsTuner()))
                {
                    this.AI.SelectCard(new[] {
                        CardId.OrcustHarpHorror,
                        CardId.ArmageddonKnight,
                        CardId.TrickstarCandina
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
                if (this.Duel.LastChainPlayer == 0)
                {
                    return false;
                }

                this.ShootingRiserDragonCount++;
                return this.ShootingRiserDragonCount <= 10;
            }
        }

        private bool KnightmarePhoenixSummon()
        {
            if (!this.KnightmareMermaidSummon())
            {
                return false;
            }

            if (!this.Bot.HasInExtra(CardId.KnightmareMermaid))
            {
                return false;
            }

            int[] firstMats = new[] {
                CardId.JetSynchron,
                CardId.CrystronNeedlefiber,
                CardId.SkyStrikerMechaHornetDronesToken,
                CardId.ThePhantomKnightsofShadeBrigandine,
                CardId.ScrapRecycler,
                CardId.SkyStrikerAceKagari,
                CardId.ArmageddonKnight,
                CardId.TrickstarCandina,
                CardId.TrickstarCarobein
            };
            if (this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsCode(firstMats)) >= 2)
            {
                this.AI.SelectMaterials(firstMats);
                return true;
            }
            int[] secondMats = new[] {
                CardId.OrcustCymbalSkeleton,
                CardId.OrcustHarpHorror,
                CardId.DestrudoTheLostDragonsFrisson,
                CardId.JetSynchron,
                CardId.AshBlossomJoyousSpring,
                CardId.GhostBelleHauntedMansion,
                CardId.ThePhantomKnightsofSilentBoots,
                CardId.ThePhantomKnightsofAncientCloak,
                CardId.MaxxC,
                CardId.SalamangreatAlmiraj
            };
            int[] mats = firstMats.Concat(secondMats).ToArray();
            if (this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsCode(mats)) >= 2)
            {
                this.AI.SelectMaterials(mats);
                return true;
            }
            return false;
        }

        private bool KnightmarePhoenixEffect()
        {
            int costcount = this.Bot.Hand.GetMatchingCardsCount(card => card.IsCode(this.HandCosts));
            ClientCard target = this.Enemy.SpellZone.GetFloodgate();
            ClientCard anytarget = this.Enemy.SpellZone.GetFirstMatchingCard(card => !card.OwnTargets.Any(cont => cont.IsCode(CardId.TrickstarLightStage)));
            if ((costcount > 1 && anytarget != null) || (this.Bot.GetHandCount() > 1 && target != null))
            {
                this.AI.SelectCard(this.HandCosts);
                if (target == null)
                {
                    target = anytarget;
                }

                this.AI.SelectNextCard(target);
                return true;
            }
            return false;
        }

        private bool KnightmareMermaidSummon()
        {
            if (this.Bot.GetHandCount() == 0)
            {
                return false;
            }

            if (this.Bot.GetRemainingCount(CardId.OrcustKnightmare, 2) == 0)
            {
                return false;
            }

            this.AI.SelectPlace(Zones.ExtraMonsterZones);
            return true;
        }

        private bool KnightmareMermaidEffect()
        {
            this.AI.SelectCard(this.HandCosts);
            return true;
        }

        private bool GalateaSummonFirst()
        {
            // only summon with Mermaid and Orcust Knightmare
            IList<ClientCard> mats = this.Bot.MonsterZone.GetMatchingCards(card => card.IsCode(CardId.KnightmareMermaid, CardId.OrcustKnightmare));
            if (mats.Count >= 2)
            {
                this.AI.SelectMaterials(mats);
                return true;
            }
            return false;
        }

        private bool OrcustKnightmareEffect()
        {
            if (!this.Bot.HasInGraveyard(CardId.OrcustHarpHorror))
            {
                this.AI.SelectCard(this.Util.GetBestBotMonster());
                this.AI.SelectNextCard(CardId.OrcustHarpHorror);
                return true;
            }
            else if (!this.Bot.HasInGraveyard(CardId.WorldLegacyWorldWand) && this.Bot.GetRemainingCount(CardId.WorldLegacyWorldWand, 1) > 0)
            {
                this.AI.SelectCard(CardId.GalateaTheOrcustAutomaton);
                this.AI.SelectNextCard(CardId.WorldLegacyWorldWand);
                return true;
            }
            else if (!this.Bot.HasInGraveyard(CardId.OrcustCymbalSkeleton) && this.Bot.GetRemainingCount(CardId.OrcustCymbalSkeleton, 1) > 0 && this.Bot.HasInGraveyard(CardId.SheorcustDingirsu) && !this.SheorcustDingirsuSummoned)
            {
                this.AI.SelectCard(CardId.GalateaTheOrcustAutomaton);
                this.AI.SelectNextCard(CardId.OrcustCymbalSkeleton);
                return true;
            }
            return false;
        }

        private bool HarpHorrorEffect()
        {
            this.HarpHorrorUsed = true;
            this.AI.SelectCard(CardId.OrcustCymbalSkeleton);
            return true;
        }

        private bool WorldWandEffect()
        {
            this.AI.SelectCard(CardId.OrcustCymbalSkeleton);
            return true;
        }

        private bool RustyBardicheSummon()
        {
            //if (Bot.GetRemainingCount(CardId.ThePhantomKnightsofAncientCloak, 1) == 0 && Bot.GetRemainingCount(CardId.ThePhantomKnightsofSilentBoots, 1) == 0)
            //    return false;
            //if (Bot.GetRemainingCount(CardId.ThePhantomKnightsofShadeBrigandine, 1) == 0 && Bot.GetRemainingCount(CardId.PhantomKnightsFogBlade, 2) == 0)
            //    return false;
            IList<ClientCard> mats = this.Bot.MonsterZone.GetMatchingCards(card => card.IsCode(CardId.GalateaTheOrcustAutomaton));
            ClientCard mat2 = this.Bot.MonsterZone.GetMatchingCards(card => card.IsCode(CardId.OrcustCymbalSkeleton)).FirstOrDefault();
            if (mat2 != null)
            {
                mats.Add(mat2);
            }

            this.AI.SelectMaterials(mats);
            this.AI.SelectPlace(Zones.ExtraMonsterZones);
            return true;
        }

        private bool RustyBardicheEffect()
        {
            if (this.ActivateDescription == -1 || this.ActivateDescription == this.Util.GetStringId(CardId.ThePhantomKnightsofRustyBardiche, 0))
            {
                ClientCard target = this.GetFogBladeTarget();
                if (target == null)
                {
                    target = this.Util.GetBestEnemyCard(false, true);
                }

                if (target == null)
                {
                    return false;
                }

                this.RustyBardicheTarget = target;
                this.AI.SelectCard(target);
                return true;
            }
            else
            {
                this.AI.SelectCard(CardId.ThePhantomKnightsofAncientCloak);
                if (this.Bot.HasInMonstersZone(CardId.JetSynchron) && !this.Bot.MonsterZone.IsExistingMatchingCard(card => card.Level == 4))
                {
                    this.AI.SelectNextCard(CardId.ThePhantomKnightsofShadeBrigandine);
                }
                else
                {
                    this.AI.SelectNextCard(CardId.PhantomKnightsFogBlade);
                }

                return true;
            }
        }

        private ClientCard GetFogBladeTarget()
        {
            return this.Enemy.MonsterZone.GetFirstMatchingCard(card => card.OwnTargets.Any(cont => cont.IsCode(CardId.PhantomKnightsFogBlade)));
        }

        private bool CymbalSkeletonEffect()
        {
            int[] botTurnTargets = new[] { CardId.GalateaTheOrcustAutomaton, CardId.SheorcustDingirsu };
            int[] emenyTurnTargets = new[] { CardId.SheorcustDingirsu, CardId.GalateaTheOrcustAutomaton };
            if (this.Duel.Player == 0 && this.Bot.HasInGraveyard(CardId.GalateaTheOrcustAutomaton) && !this.Bot.HasInMonstersZone(CardId.GalateaTheOrcustAutomaton) && this.Bot.HasInExtra(CardId.SheorcustDingirsu) && !this.SheorcustDingirsuSummoned)
            {
                this.AI.SelectCard(botTurnTargets);
                this.CymbalSkeletonUsed = true;
                return true;
            }
            else if (this.Duel.Player == 0 && this.Bot.HasInGraveyard(CardId.SheorcustDingirsu) && !this.SheorcustDingirsuSummoned)
            {
                this.AI.SelectCard(emenyTurnTargets);
                this.SheorcustDingirsuSummoned = true;
                this.CymbalSkeletonUsed = true;
                return true;
            }
            if (this.Duel.Player == 1 && this.Bot.HasInGraveyard(CardId.SheorcustDingirsu) && !this.SheorcustDingirsuSummoned &&
                (this.Util.GetProblematicEnemyCard() != null || this.Duel.Phase == DuelPhase.End))
            {
                this.AI.SelectCard(emenyTurnTargets);
                this.CymbalSkeletonUsed = true;
                this.SheorcustDingirsuSummoned = true;
                return true;
            }
            return false;
        }

        private bool SheorcustDingirsuSummon()
        {
            this.SheorcustDingirsuSummoned = true;
            return true;
        }

        private bool SheorcustDingirsuEffect()
        {
            if (this.ActivateDescription == 96)
            {
                // TODO: more FogBlade lost target
                if ((this.Duel.Phase == DuelPhase.Main1 || this.Duel.Phase == DuelPhase.Main2) && this.Duel.CurrentChain.Count == 0)
                {
                    return false;
                }

                this.AI.SelectCard(CardId.OrcustCymbalSkeleton);
                return true;
            }
            ClientCard target;
            target = this.GetFogBladeTarget();
            if (target != null && target != this.RustyBardicheTarget)
            {
                this.AI.SelectOption(0);
                this.AI.SelectCard(target);
                return true;
            }
            target = this.Util.GetProblematicEnemyMonster();
            if (target != null && target != this.RustyBardicheTarget)
            {
                this.AI.SelectOption(0);
                this.AI.SelectCard(target);
                return true;
            }
            target = this.Util.GetProblematicEnemySpell();
            if (target != null && target != this.RustyBardicheTarget)
            {
                this.AI.SelectOption(0);
                this.AI.SelectCard(target);
                return true;
            }
            if (this.Bot.HasInBanished(CardId.OrcustCymbalSkeleton))
            {
                this.AI.SelectOption(1);
                this.AI.SelectCard(CardId.OrcustCymbalSkeleton);
                return true;
            }
            target = this.Enemy.MonsterZone.GetFirstMatchingCard(card => card != this.RustyBardicheTarget) ?? this.Enemy.SpellZone.GetFirstMatchingCard(card => card != this.RustyBardicheTarget);
            if (target != null)
            {
                this.AI.SelectOption(0);
                this.AI.SelectCard(target);
                return true;
            }
            this.AI.SelectOption(1);
            //AI.SelectCard(); any card
            return true;
        }

        private bool AncientCloakEffect()
        {
            if (this.Bot.HasInMonstersZone(CardId.SalamangreatAlmiraj) && this.Bot.HasInExtra(CardId.KnightmarePhoenix))
            {
                this.AI.SelectCard(CardId.ThePhantomKnightsofShadeBrigandine);
            }
            else
            {
                this.AI.SelectCard(CardId.ThePhantomKnightsofSilentBoots);
            }

            return true;
        }

        private bool SilentBootsSummon()
        {
            return true;
        }

        private bool SilentBootsEffect()
        {
            if (this.Bot.HasInMonstersZone(CardId.SalamangreatAlmiraj) && this.Bot.HasInExtra(CardId.KnightmarePhoenix))
            {
                this.AI.SelectCard(CardId.ThePhantomKnightsofShadeBrigandine);
            }
            else
            {
                this.AI.SelectCard(CardId.PhantomKnightsFogBlade);
            }

            return true;
        }

        private bool ShadeBrigandineSummonSecond()
        {
            if (this.DefaultOnBecomeTarget())
            {
                return true;
            }

            return (this.Bot.HasInMonstersZone(CardId.SalamangreatAlmiraj) && this.Bot.HasInExtra(CardId.KnightmarePhoenix)) ||
                (this.Bot.HasInMonstersZone(CardId.JetSynchron) && this.Bot.HasInMonstersZone(CardId.ThePhantomKnightsofSilentBoots));
        }

        private bool GalateaSummonSecond()
        {
            if (!this.Util.IsTurn1OrMain2())
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.GalateaTheOrcustAutomaton))
            {
                return false;
            }

            IList<ClientCard> selected = new List<ClientCard>();

            if (!this.Bot.HasInGraveyard(CardId.SheorcustDingirsu))
            {
                ClientCard sheorcustDingirsu = this.Bot.MonsterZone.GetFirstMatchingFaceupCard(card => card.IsCode(CardId.SheorcustDingirsu));
                if (sheorcustDingirsu != null)
                {
                    selected.Add(sheorcustDingirsu);
                }
            }

            int[] matids = new[] {
                CardId.OrcustKnightmare,
                CardId.ThePhantomKnightsofSilentBoots,
                CardId.ThePhantomKnightsofAncientCloak,
                CardId.OrcustCymbalSkeleton,
                CardId.OrcustHarpHorror,
                CardId.ScrapRecycler,
                CardId.CrystronNeedlefiber,
                CardId.SkyStrikerAceKagari,
                CardId.KnightmareMermaid,
                CardId.ArmageddonKnight
            };

            IList<ClientCard> mats = this.Bot.MonsterZone.GetMatchingCards(card => card.Level > 0 && card.Level <= 7);

            for (int i = 0; i < matids.Length && selected.Count < 2; i++)
            {
                ClientCard c = mats.GetFirstMatchingFaceupCard(card => card.IsCode(matids[i]));
                if (c != null)
                {
                    selected.Add(c);
                    if (selected.Count == 2 && this.Util.GetBotAvailZonesFromExtraDeck(selected) == 0)
                    {
                        selected.Remove(c);
                    }
                }
            }

            if (selected.Count == 2)
            {
                this.AI.SelectMaterials(selected);
                return true;
            }

            return false;
        }

        private bool GalateaEffect()
        {
            if (this.Duel.Player == 0)
            {
                this.AI.SelectCard(CardId.OrcustKnightmare);
                this.AI.SelectNextCard(CardId.OrcustratedBabel);
            }
            if (this.Duel.Player == 1)
            {
                this.AI.SelectCard(CardId.OrcustKnightmare);
                this.AI.SelectNextCard(CardId.OrcustratedClimax);
            }
            return true;
        }

        private bool BorrelswordDragonSummon()
        {
            if (this.Util.IsTurn1OrMain2())
            {
                return false;
            }

            List<ClientCard> mats = this.Bot.MonsterZone.GetMatchingCards(card => card.IsFaceup() && card.HasType(CardType.Effect) && card.Attack <= 2000).ToList();
            mats.Sort(CardContainer.CompareCardAttack);
            mats.Reverse();

            int link = 0;
            bool doubleused = false;
            IList<ClientCard> selected = new List<ClientCard>();
            foreach (ClientCard card in mats)
            {
                selected.Add(card);
                if (!doubleused && card.LinkCount == 2)
                {
                    doubleused = true;
                    link += 2;
                }
                else
                {
                    link++;
                }

                if (link >= 4)
                {
                    break;
                }
            }

            if (link >= 4 && this.Util.GetBotAvailZonesFromExtraDeck(selected) > 0)
            {
                this.AI.SelectMaterials(selected);
                return true;
            }
            return false;
        }

        private bool BorrelswordDragonEffect()
        {
            if (this.ActivateDescription == -1 || this.ActivateDescription == this.Util.GetStringId(CardId.BorrelswordDragon, 1))
            {
                this.BorrelswordDragonUsed = true;
                return true;
            }
            else
            {
                if (this.Duel.Player == 0 && (this.Duel.Turn == 1 || this.Duel.Phase >= DuelPhase.Main2))
                {
                    return false;
                }
                ClientCard target = this.Bot.MonsterZone.GetFirstMatchingCard(card => card.IsAttack() && !card.HasType(CardType.Link) && card.Attacked && !card.IsShouldNotBeTarget());
                if (target != null)
                {
                    this.AI.SelectCard(target);
                    return true;
                }
                if (!this.Bot.MonsterZone.IsExistingMatchingCard(card => card.IsAttack() && !card.HasType(CardType.Link)))
                {
                    target = this.Enemy.MonsterZone.GetFirstMatchingCard(card => card.IsAttack() && !card.HasType(CardType.Link) && !card.IsShouldNotBeTarget());
                    if (target != null)
                    {
                        this.AI.SelectCard(target);
                        return true;
                    }
                }
                return false;
            }
        }

        private bool BabelEffect()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                IList<ClientCard> costCards = this.Bot.Hand.GetMatchingCards(card => card.IsCode(this.HandCosts));
                if (costCards.Count > 0)
                {
                    this.AI.SelectCard(this.HandCosts);
                    return true;
                }
                return false;
            }
            return this.Bot.HasInMonstersZoneOrInGraveyard(new[] {
                CardId.OrcustCymbalSkeleton,
                CardId.OrcustHarpHorror,
                CardId.OrcustKnightmare,
                CardId.GalateaTheOrcustAutomaton,
                CardId.LongirsuTheOrcustOrchestrator,
                CardId.SheorcustDingirsu
            });
        }

        private bool ShadeBrigandineSummonFirst()
        {
            return this.Bot.GetMonsterCount() < 2;
        }

        private bool OneCardComboSummon()
        {
            if (this.Bot.HasInExtra(CardId.SalamangreatAlmiraj) && this.Bot.HasInExtra(new[] { CardId.CrystronNeedlefiber, CardId.KnightmarePhoenix }) && this.Bot.GetMonsterCount() < 3)
            {
                this.NormalSummoned = true;
                return true;
            }
            return false;
        }

        private bool LinkMaterialSummon()
        {
            if (this.Bot.HasInExtra(CardId.KnightmarePhoenix) && this.Bot.GetMonsterCount() > 0 && this.Bot.GetMonsterCount() < 3)
            {
                this.NormalSummoned = true;
                return true;
            }
            return false;
        }

        private bool TunerSummon()
        {
            if (this.Bot.HasInExtra(new[] { CardId.CrystronNeedlefiber, CardId.KnightmarePhoenix }) && this.Bot.GetMonsterCount() > 0 && this.Bot.GetMonsterCount() < 3)
            {
                this.NormalSummoned = true;
                return true;
            }
            return false;
        }

        private bool OtherSummon()
        {
            this.NormalSummoned = true;
            return true;
        }

        private bool BorreloadSavageDragonEffect()
        {
            if (this.Duel.CurrentChain.Count == 0)
            {
                this.AI.SelectCard(new[] { CardId.KnightmarePhoenix, CardId.CrystronNeedlefiber });
                return true;
            }
            else
            {
                return true;
            }
        }

        private bool FogBladeEffect()
        {
            if (this.Card.Location == CardLocation.SpellZone)
            {
                return !this.Util.HasChainedTrap(0) && this.DefaultDisableMonster();
            }
            else if (this.Bot.HasInGraveyard(CardId.ThePhantomKnightsofRustyBardiche) || this.Bot.GetMonsterCount() < 2)
            {
                this.AI.SelectCard(CardId.ThePhantomKnightsofRustyBardiche);
                return true;
            }
            return false;
        }

        private bool ClimaxEffect()
        {
            if (this.Card.Location == CardLocation.SpellZone)
            {
                return this.Duel.LastChainPlayer == 1;
            }
            else if (this.Duel.Phase == DuelPhase.End)
            {
                ClientCard target = null;
                target = this.Bot.Banished.GetFirstMatchingFaceupCard(card=>card.IsCode(CardId.OrcustCymbalSkeleton));
                if (target == null)
                {
                    target = this.Bot.Banished.GetFirstMatchingFaceupCard(card => card.IsCode(CardId.OrcustHarpHorror));
                }

                if (target != null)
                {
                    this.AI.SelectCard(target);
                    return true;
                }
                if(!this.Bot.HasInHand(CardId.OrcustHarpHorror) && this.Bot.GetRemainingCount(CardId.OrcustHarpHorror, 2) > 1)
                {
                    this.AI.SelectCard(CardId.OrcustHarpHorror);
                    return true;
                }
            }
            return false;
        }

        private bool EagleBoosterEffect()
        {
            if (this.Duel.LastChainPlayer != 1)
            {
                return false;
            }

            ClientCard target = this.Bot.GetMonstersInExtraZone().GetFirstMatchingCard(
                card => this.Duel.CurrentChain.Contains(card) || card.IsCode(CardId.KnightmareMermaid));
            if (target != null)
            {
                this.AI.SelectCard(target);
                return true;
            }
            return false;
        }

        private bool MonsterRepos()
        {
            if (this.Card.IsFacedown())
            {
                return true;
            }

            return this.DefaultMonsterRepos();
        }
    }
}
