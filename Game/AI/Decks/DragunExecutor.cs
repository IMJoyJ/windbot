using System;
using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using System.Linq;

namespace WindBot.Game.AI.Decks
{
    [Deck("Dragun", "AI_Dragun")]
    class DragunExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int DarkMagician = 46986414;
            public const int RedEyesBDragon = 74677422;
            public const int RedEyesWyvern = 67300516;
            public const int TourGuideFromTheUnderworld = 10802915;
            public const int Sangan = 26202165;
            public const int CrusadiaArboria = 91646304;
            public const int AshBlossomJoyousSpring = 14558127;
            public const int MechaPhantomBeastOLion = 72291078;
            public const int MechaPhantomBeastOLionToken = 72291079;
            public const int MaxxC = 23434538;
            public const int MagiciansSouls = 97631303;

            public const int InstantFusion = 1845204;
            public const int RedEyesFusion = 6172122;
            public const int MagicalizedFusion = 11827244;
            public const int HarpiesFeatherDuster = 18144506;
            public const int FoolishBurial = 81439173;
            public const int MonsterReborn = 83764718;
            public const int RedEyesInsight = 92353449;
            public const int CalledbyTheGrave = 24224830;
            public const int InfiniteImpermanence = 10045474;
            public const int SolemnStrike = 40605147;

            public const int DragunofRedEyes = 37818794;
            public const int SeaMonsterofTheseus = 96334243;
            public const int ThousandEyesRestrict = 63519819;
            public const int CrystronHalqifibrax = 50588353;
            public const int PredaplantVerteAnaconda = 70369116;
            public const int LinkSpider = 98978921;
            public const int ImdukTheWorldChaliceDragon = 31226177;
            public const int SalamangreatAlmiraj = 60303245;
        }

        public DragunExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // counter
            this.AddExecutor(ExecutorType.Activate, CardId.AshBlossomJoyousSpring, this.DefaultAshBlossomAndJoyousSpring);
            this.AddExecutor(ExecutorType.Activate, CardId.CalledbyTheGrave, this.DefaultCalledByTheGrave);
            this.AddExecutor(ExecutorType.Activate, CardId.InfiniteImpermanence, this.DefaultInfiniteImpermanence);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, this.DefaultSolemnStrike);
            this.AddExecutor(ExecutorType.Activate, CardId.DragunofRedEyes, this.DragunofRedEyesCounter);

            this.AddExecutor(ExecutorType.Activate, CardId.MaxxC, this.DefaultMaxxC);
            this.AddExecutor(ExecutorType.Activate, CardId.HarpiesFeatherDuster);

            this.AddExecutor(ExecutorType.Activate, CardId.DragunofRedEyes, this.DragunofRedEyesDestroy);
            this.AddExecutor(ExecutorType.Activate, CardId.ThousandEyesRestrict, this.ThousandEyesRestrictEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.RedEyesInsight, this.RedEyesInsightEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.RedEyesFusion, this.RedEyesFusionEffect);

            this.AddExecutor(ExecutorType.Repos, this.MonsterRepos);

            this.AddExecutor(ExecutorType.Summon, CardId.TourGuideFromTheUnderworld, this.TourGuideFromTheUnderworldSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.TourGuideFromTheUnderworld, this.TourGuideFromTheUnderworldEffect);
            this.AddExecutor(ExecutorType.Summon, CardId.Sangan, this.SanganSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.Sangan, this.SanganEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.MechaPhantomBeastOLion);
            this.AddExecutor(ExecutorType.Activate, CardId.MechaPhantomBeastOLion, this.MechaPhantomBeastOLionEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.SalamangreatAlmiraj, this.SalamangreatAlmirajSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.ImdukTheWorldChaliceDragon, this.ImdukTheWorldChaliceDragonSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.LinkSpider, this.LinkSpiderSummon);

            this.AddExecutor(ExecutorType.SpSummon, CardId.CrusadiaArboria);

            this.AddExecutor(ExecutorType.Activate, CardId.InstantFusion, this.InstantFusionEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.RedEyesWyvern);
            this.AddExecutor(ExecutorType.Summon, CardId.CrusadiaArboria, this.SummonForMaterial);
            this.AddExecutor(ExecutorType.Summon, CardId.AshBlossomJoyousSpring, this.SummonForMaterial);
            this.AddExecutor(ExecutorType.Summon, CardId.MaxxC, this.SummonForMaterial);

            this.AddExecutor(ExecutorType.Activate, CardId.FoolishBurial, this.FoolishBurialEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.MonsterReborn, this.MonsterRebornEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.MagiciansSouls, this.MagiciansSoulsEffect);
            this.AddExecutor(ExecutorType.Summon, CardId.MagiciansSouls, this.SummonForMaterial);

            this.AddExecutor(ExecutorType.SpSummon, CardId.CrystronHalqifibrax, this.CrystronNeedlefiberSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.CrystronHalqifibrax, this.CrystronNeedlefiberEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.PredaplantVerteAnaconda, this.PredaplantVerteAnacondaSummon);

            this.AddExecutor(ExecutorType.Activate, CardId.MagicalizedFusion, this.MagicalizedFusionEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.PredaplantVerteAnaconda, this.PredaplantVerteAnacondaEffect);

            this.AddExecutor(ExecutorType.SpellSet, CardId.InfiniteImpermanence, this.TrapSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnStrike, this.TrapSet);

            this.AddExecutor(ExecutorType.MonsterSet, CardId.Sangan);

        }

        private bool BeastOLionUsed = false;
        private bool RedEyesFusionUsed = false;
        public override bool OnSelectHand()
        {
            // go first
            return true;
        }

        public override void OnNewTurn()
        {
            this.BeastOLionUsed = false;
            this.RedEyesFusionUsed = false;
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
            if (location == CardLocation.MonsterZone)
            {
                return available & ~this.Bot.GetLinkedZones();
            }
            return 0;
        }

        private bool DragunofRedEyesCounter()
        {
            if (this.ActivateDescription != -1 && this.ActivateDescription != this.Util.GetStringId(CardId.DragunofRedEyes, 1))
            {
                return false;
            }

            if (this.Duel.LastChainPlayer != 1)
            {
                return false;
            }

            this.AI.SelectCard(new[] {
                CardId.RedEyesWyvern,
                CardId.MechaPhantomBeastOLion
            });
            return true;
        }

        private bool DragunofRedEyesDestroy()
        {
            if (this.ActivateDescription == -1 || this.ActivateDescription == this.Util.GetStringId(CardId.DragunofRedEyes, 1))
            {
                return false;
            }

            this.AI.SelectCard(this.Util.GetBestEnemyMonster());
            return true;
        }

        private bool ThousandEyesRestrictEffect()
        {
            this.AI.SelectCard(this.Util.GetBestEnemyMonster());
            return true;
        }

        private bool RedEyesInsightEffect()
        {
            if (this.Bot.HasInHand(CardId.RedEyesFusion))
            {
                return false;
            }

            if (this.Bot.GetRemainingCount(CardId.RedEyesWyvern, 1) == 0 && this.Bot.GetRemainingCount(CardId.RedEyesBDragon, 2) == 1 && !this.Bot.HasInHand(CardId.RedEyesBDragon))
            {
                return false;
            }

            this.AI.SelectCard(CardId.RedEyesWyvern);
            return true;
        }

        private bool RedEyesFusionEffect()
        {
            if (this.Bot.HasInMonstersZone(new[] { CardId.DragunofRedEyes, CardId.RedEyesBDragon }))
            { // you don't want to use DragunofRedEyes which is treated as RedEyesBDragon as fusion material
                if (this.Util.GetBotAvailZonesFromExtraDeck() == 0)
                {
                    return false;
                }

                if (this.Bot.GetRemainingCount(CardId.RedEyesBDragon, 2) == 0 && !this.Bot.HasInHand(CardId.RedEyesBDragon))
                {
                    return false;
                }
            }
            this.AI.SelectMaterials(CardLocation.Deck);
            this.RedEyesFusionUsed = true;
            return true;
        }

        private bool TourGuideFromTheUnderworldSummon()
        {
            if (this.Bot.GetRemainingCount(CardId.TourGuideFromTheUnderworld, 2) == 0 && this.Bot.GetRemainingCount(CardId.Sangan, 2) == 0)
            {
                return false;
            }

            return true;
        }

        private bool TourGuideFromTheUnderworldEffect()
        {
            this.AI.SelectCard(CardId.Sangan);
            return true;
        }

        private bool SanganSummon()
        {
            return true;
        }

        private bool SanganEffect()
        {
            if (this.Bot.HasInMonstersZone(CardId.SalamangreatAlmiraj) && !this.Bot.HasInHand(CardId.CrusadiaArboria))
            {
                this.AI.SelectCard(CardId.CrusadiaArboria);
            }
            else if (!this.Bot.HasInHand(CardId.MaxxC))
            {
                this.AI.SelectCard(CardId.MaxxC);
            }
            else if (!this.Bot.HasInHand(CardId.AshBlossomJoyousSpring))
            {
                this.AI.SelectCard(CardId.AshBlossomJoyousSpring);
            }
            else if (!this.Bot.HasInHand(CardId.MagiciansSouls))
            {
                this.AI.SelectCard(CardId.MagiciansSouls);
            }
            else if (!this.Bot.HasInHand(CardId.CrusadiaArboria))
            {
                this.AI.SelectCard(CardId.CrusadiaArboria);
            }
            else
            {
                this.AI.SelectCard(new[] {
                    CardId.AshBlossomJoyousSpring,
                    CardId.MaxxC,
                    CardId.CrusadiaArboria
                });
            }

            return true;
        }

        private bool SalamangreatAlmirajSummon()
        {
            int[] materials = new[] {
                CardId.Sangan,
                CardId.MechaPhantomBeastOLion
            };
            if (this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsCode(materials) && !card.IsSpecialSummoned) == 0)
            {
                return false;
            }

            this.AI.SelectMaterials(materials);
            return true;
        }

        private bool ImdukTheWorldChaliceDragonSummon()
        {
            if (this.Bot.HasInMonstersZone(CardId.PredaplantVerteAnaconda, true) || !this.Bot.HasInExtra(CardId.PredaplantVerteAnaconda))
            {
                return false;
            }

            if (this.Bot.Graveyard.GetMatchingCardsCount(card => (card.Race & (int)CardRace.Dragon) > 0) >= 0)
            {
                return false;
            }

            if (this.Bot.GetMonsterCount() == 1 && this.Bot.Hand.GetMatchingCardsCount(card => card.Level <= 4) == 0 && !this.Util.IsTurn1OrMain2())
            {
                return false;
            }

            if (this.Bot.GetMonsterCount() >= 2 && this.Bot.MonsterZone.GetMatchingCardsCount(card => card.Level >= 8) > 0)
            {
                return false;
            }

            return true;
        }

        private bool LinkSpiderSummon()
        {
            if (!this.Bot.HasInMonstersZone(CardId.MechaPhantomBeastOLionToken))
            {
                return false;
            }

            this.AI.SelectMaterials(CardId.MechaPhantomBeastOLionToken);
            return true;
        }

        private bool NeedMonster()
        {
            if (this.Bot.HasInMonstersZone(CardId.PredaplantVerteAnaconda, true) || !this.Bot.HasInExtra(CardId.PredaplantVerteAnaconda))
            {
                return false;
            }

            if (this.Bot.MonsterZone.GetMatchingCardsCount(card => card.Level >= 8) > 0)
            {
                return false;
            }

            if (this.Bot.GetMonsterCount() == 0 && this.Bot.Hand.GetMatchingCardsCount(card => card.Level <= 4) == 0)
            {
                return false;
            }

            if (this.Bot.GetMonsterCount() >= 2)
            {
                return false;
            }

            return true;
        }

        private bool InstantFusionEffect()
        {
            if (!this.NeedMonster())
            {
                return false;
            }

            if (this.Enemy.GetMonsterCount() > 0)
            {
                this.AI.SelectCard(CardId.ThousandEyesRestrict);
            }
            else
            {
                this.AI.SelectCard(CardId.SeaMonsterofTheseus);
            }

            return true;
        }

        private bool SummonForMaterial()
        {
            if (this.Bot.HasInMonstersZone(CardId.PredaplantVerteAnaconda, true) || !this.Bot.HasInExtra(CardId.PredaplantVerteAnaconda))
            {
                return false;
            }

            if (this.Bot.MonsterZone.GetMatchingCardsCount(card => (card.HasType(CardType.Effect) || card.IsTuner()) && card.Level < 8) == 1)
            {
                return true;
            }

            if (this.Bot.HasInHand(CardId.MagiciansSouls))
            {
                return true;
            }

            return false;
        }

        private bool MagiciansSoulsEffect()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                if (this.RedEyesFusionUsed)
                {
                    return false;
                }

                if (this.Bot.GetMonsterCount() >= 2)
                {
                    return false;
                }

                this.AI.SelectOption(1);
                this.AI.SelectYesNo(true);
                return true;
            }
            else
            {
                int[] costs = new[] {
                    CardId.RedEyesInsight,
                    CardId.RedEyesFusion
                };
                if (this.Bot.HasInHand(costs))
                {
                    this.AI.SelectCard(costs);
                    return true;
                }
                return false;
            }
        }

        private bool PredaplantVerteAnacondaSummon()
        {
            if (this.Bot.HasInMonstersZone(CardId.PredaplantVerteAnaconda, true))
            {
                return false;
            }

            int[] materials = new[] {
                CardId.ImdukTheWorldChaliceDragon,
                CardId.Sangan,
                CardId.TourGuideFromTheUnderworld,
                CardId.CrusadiaArboria,
                CardId.MechaPhantomBeastOLion,
                CardId.MagiciansSouls,
                CardId.SalamangreatAlmiraj,
                CardId.LinkSpider,
                CardId.ThousandEyesRestrict,
                CardId.AshBlossomJoyousSpring,
                CardId.MaxxC,
                CardId.RedEyesWyvern,
                CardId.CrystronHalqifibrax
            };
            if (this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsCode(materials)) >= 2)
            {
                this.AI.SelectMaterials(materials);
                return true;
            }
            return false;
        }

        private bool MagicalizedFusionEffect()
        {
            if (this.Bot.HasInMonstersZone(new[] { CardId.DragunofRedEyes, CardId.RedEyesBDragon }))
            { // you don't want to use DragunofRedEyes which is treated as RedEyesBDragon as fusion material
                if (this.Util.GetBotAvailZonesFromExtraDeck() == 0)
                {
                    return false;
                }

                if (this.Bot.Graveyard.GetMatchingCardsCount(card => (card.Race & (int)CardRace.Dragon) > 0) == 0)
                {
                    return false;
                }
            }
            this.AI.SelectMaterials(CardLocation.Grave);
            return true;
        }

        private bool PredaplantVerteAnacondaEffect()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.PredaplantVerteAnaconda, 0))
            {
                return false;
            }

            this.AI.SelectCard(CardId.RedEyesFusion);
            this.AI.SelectMaterials(CardLocation.Deck);
            return true;
        }

        private bool FoolishBurialEffect()
        {
            if (this.RedEyesFusionUsed)
            {
                return false;
            }

            if (this.Bot.HasInHand(CardId.MagicalizedFusion))
            {
                if (this.Bot.HasInGraveyard(CardId.DarkMagician) && this.Bot.Graveyard.GetMatchingCardsCount(card => (card.Race & (int)CardRace.Dragon) > 0) == 0)
                {
                    this.AI.SelectCard(new[]
                    {
                        CardId.RedEyesWyvern,
                        CardId.RedEyesBDragon
                    });
                    return true;
                }
                if (!this.Bot.HasInGraveyard(CardId.DarkMagician) && this.Bot.Graveyard.GetMatchingCardsCount(card => (card.Race & (int)CardRace.Dragon) > 0) > 0)
                {
                    this.AI.SelectCard(CardId.DarkMagician);
                    return true;
                }
            }

            if (!this.NeedMonster())
            {
                return false;
            }

            this.AI.SelectCard(new[] {
                CardId.MechaPhantomBeastOLion
            });
            return true;
        }

        private bool MonsterRebornEffect()
        {
            if (this.Bot.HasInGraveyard(CardId.DragunofRedEyes))
            {
                this.AI.SelectCard(CardId.DragunofRedEyes);
                return true;
            }
            else
            {
                if (!this.NeedMonster())
                {
                    return false;
                }

                this.AI.SelectCard(new[] {
                    CardId.PredaplantVerteAnaconda,
                    CardId.Sangan,
                    CardId.ThousandEyesRestrict,
                    CardId.MechaPhantomBeastOLion,
                    CardId.CrusadiaArboria,
                    CardId.AshBlossomJoyousSpring
                });
                return true;
            }
        }

        private bool MechaPhantomBeastOLionEffect()
        {
            if (this.ActivateDescription == -1)
            {
                this.BeastOLionUsed = true;
                return true;
            }
            return !this.BeastOLionUsed;
        }


        private bool CrystronNeedlefiberSummon()
        {
            if (this.Bot.HasInMonstersZone(CardId.PredaplantVerteAnaconda, true))
            {
                return false;
            }

            int[] materials = new[] {
                CardId.CrusadiaArboria,
                CardId.MechaPhantomBeastOLion,
                CardId.AshBlossomJoyousSpring,
                CardId.SeaMonsterofTheseus,
                CardId.MechaPhantomBeastOLionToken,
                CardId.DarkMagician,
                CardId.ImdukTheWorldChaliceDragon,
                CardId.Sangan,
                CardId.TourGuideFromTheUnderworld,
                CardId.MagiciansSouls,
                CardId.SalamangreatAlmiraj,
                CardId.LinkSpider,
                CardId.ThousandEyesRestrict,
                CardId.SeaMonsterofTheseus,
                CardId.MaxxC,
                CardId.RedEyesWyvern
            };
            if (this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsCode(materials)) >= 2)
            {
                this.AI.SelectMaterials(materials);
                return true;
            }
            return false;
        }

        private bool CrystronNeedlefiberEffect()
        {
            if (this.Duel.Player == 0)
            {
                this.AI.SelectCard(CardId.MechaPhantomBeastOLion);
                return true;
            }
            else
            {
                return true;
            }
        }

        private bool TrapSet()
        {
            if (this.Bot.HasInMonstersZone(new[] { CardId.DragunofRedEyes, CardId.RedEyesBDragon }) && this.Bot.GetHandCount() == 1)
            {
                return false;
            }

            this.AI.SelectPlace(Zones.MonsterZone1 + Zones.MonsterZone2 + Zones.MonsterZone4 + Zones.MonsterZone5);
            return true;
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
