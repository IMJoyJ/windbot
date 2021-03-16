using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using System.Linq;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("Blue-Eyes", "AI_BlueEyes")]
    class BlueEyesExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int WhiteDragon = 89631139;
            public const int AlternativeWhiteDragon = 38517737;
            public const int DragonSpiritOfWhite = 45467446;
            public const int WhiteStoneOfAncients = 71039903;
            public const int WhiteStoneOfLegend = 79814787;
            public const int SageWithEyesOfBlue = 8240199;
            public const int EffectVeiler = 97268402;
            public const int GalaxyCyclone = 5133471;
            public const int HarpiesFeatherDuster = 18144506;
            public const int ReturnOfTheDragonLords = 6853254;
            public const int PotOfDesires = 35261759;
            public const int TradeIn = 38120068;
            public const int CardsOfConsonance = 39701395;
            public const int DragonShrine = 41620959;
            public const int MelodyOfAwakeningDragon = 48800175;
            public const int SoulCharge = 54447022;
            public const int MonsterReborn = 83764718;
            public const int SilversCry = 87025064;

            public const int Giganticastle = 63422098;
            public const int AzureEyesSilverDragon = 40908371;
            public const int BlueEyesSpiritDragon = 59822133;
            public const int GalaxyEyesDarkMatterDragon = 58820923;
            public const int GalaxyEyesCipherBladeDragon = 2530830;
            public const int GalaxyEyesFullArmorPhotonDragon = 39030163;
            public const int GalaxyEyesPrimePhotonDragon = 31801517;
            public const int GalaxyEyesCipherDragon = 18963306;
            public const int HopeHarbingerDragonTitanicGalaxy = 63767246;
            public const int SylvanPrincessprite = 33909817;
        }

        private readonly List<ClientCard> UsedAlternativeWhiteDragon = new List<ClientCard>();
        ClientCard UsedGalaxyEyesCipherDragon;
        bool AlternativeWhiteDragonSummoned = false;
        bool SoulChargeUsed = false;

        public BlueEyesExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // destroy traps
            this.AddExecutor(ExecutorType.Activate, CardId.HarpiesFeatherDuster, this.DefaultHarpiesFeatherDusterFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.GalaxyCyclone, this.DefaultGalaxyCyclone);
            this.AddExecutor(ExecutorType.Activate, CardId.HarpiesFeatherDuster);

            this.AddExecutor(ExecutorType.Activate, CardId.DragonShrine, this.DragonShrineEffect);

            // Sage search
            this.AddExecutor(ExecutorType.Summon, CardId.SageWithEyesOfBlue, this.SageWithEyesOfBlueSummon);

            // search Alternative White Dragon
            this.AddExecutor(ExecutorType.Activate, CardId.MelodyOfAwakeningDragon, this.MelodyOfAwakeningDragonEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.CardsOfConsonance, this.CardsOfConsonanceEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.TradeIn, this.TradeInEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.PotOfDesires, this.DefaultPotOfDesires);

            // spsummon Alternative White Dragon if possible
            this.AddExecutor(ExecutorType.SpSummon, CardId.AlternativeWhiteDragon, this.AlternativeWhiteDragonSummon);

            // reborn
            this.AddExecutor(ExecutorType.Activate, CardId.ReturnOfTheDragonLords, this.RebornEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.SilversCry, this.RebornEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.MonsterReborn, this.RebornEffect);

            // monster effects
            this.AddExecutor(ExecutorType.Activate, CardId.AlternativeWhiteDragon, this.AlternativeWhiteDragonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.SageWithEyesOfBlue, this.SageWithEyesOfBlueEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.WhiteStoneOfAncients, this.WhiteStoneOfAncientsEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.DragonSpiritOfWhite, this.DragonSpiritOfWhiteEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.BlueEyesSpiritDragon, this.BlueEyesSpiritDragonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.HopeHarbingerDragonTitanicGalaxy, this.HopeHarbingerDragonTitanicGalaxyEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.GalaxyEyesCipherDragon, this.GalaxyEyesCipherDragonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.GalaxyEyesPrimePhotonDragon, this.GalaxyEyesPrimePhotonDragonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.GalaxyEyesFullArmorPhotonDragon, this.GalaxyEyesFullArmorPhotonDragonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.GalaxyEyesCipherBladeDragon, this.GalaxyEyesCipherBladeDragonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.GalaxyEyesDarkMatterDragon, this.GalaxyEyesDarkMatterDragonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.AzureEyesSilverDragon, this.AzureEyesSilverDragonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.SylvanPrincessprite, this.SylvanPrincesspriteEffect);

            // normal summon
            this.AddExecutor(ExecutorType.Summon, CardId.SageWithEyesOfBlue, this.WhiteStoneSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.WhiteStoneOfAncients, this.WhiteStoneSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.WhiteStoneOfLegend, this.WhiteStoneSummon);

            // special summon from extra
            this.AddExecutor(ExecutorType.SpSummon, CardId.GalaxyEyesCipherDragon, this.GalaxyEyesCipherDragonSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.GalaxyEyesPrimePhotonDragon, this.GalaxyEyesPrimePhotonDragonSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.GalaxyEyesFullArmorPhotonDragon, this.GalaxyEyesFullArmorPhotonDragonSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.GalaxyEyesCipherBladeDragon, this.GalaxyEyesCipherBladeDragonSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.GalaxyEyesDarkMatterDragon, this.GalaxyEyesDarkMatterDragonSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Giganticastle, this.GiganticastleSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BlueEyesSpiritDragon, this.BlueEyesSpiritDragonSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.HopeHarbingerDragonTitanicGalaxy, this.HopeHarbingerDragonTitanicGalaxySummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.SylvanPrincessprite, this.SylvanPrincesspriteSummon);

            // if we don't have other things to do...
            this.AddExecutor(ExecutorType.Activate, CardId.SoulCharge, this.SoulChargeEffect);
            this.AddExecutor(ExecutorType.Repos, this.Repos);
            // summon White Stone to use the hand effect of Sage
            this.AddExecutor(ExecutorType.Summon, CardId.WhiteStoneOfLegend, this.WhiteStoneSummonForSage);
            this.AddExecutor(ExecutorType.Summon, CardId.WhiteStoneOfAncients, this.WhiteStoneSummonForSage);
            this.AddExecutor(ExecutorType.Summon, CardId.SageWithEyesOfBlue, this.WhiteStoneSummonForSage);
            this.AddExecutor(ExecutorType.Activate, CardId.SageWithEyesOfBlue, this.SageWithEyesOfBlueEffectInHand);
            // set White Stone of Legend frist
            this.AddExecutor(ExecutorType.MonsterSet, CardId.WhiteStoneOfLegend);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.WhiteStoneOfAncients);

            this.AddExecutor(ExecutorType.SpellSet, this.SpellSet);
        }

        public override void OnNewTurn()
        {
            // reset
            this.UsedAlternativeWhiteDragon.Clear();
            this.UsedGalaxyEyesCipherDragon = null;
            this.AlternativeWhiteDragonSummoned = false;
            this.SoulChargeUsed = false;
        }

        public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
        {
            Logger.DebugWriteLine("OnSelectCard " + cards.Count + " " + min + " " + max);
            if (max == 2 && cards[0].Location == CardLocation.Deck)
            {
                Logger.DebugWriteLine("OnSelectCard MelodyOfAwakeningDragon");
                List<ClientCard> result = new List<ClientCard>();
                if (!this.Bot.HasInHand(CardId.WhiteDragon))
                {
                    result.AddRange(cards.Where(card => card.IsCode(CardId.WhiteDragon)).Take(1));
                }

                result.AddRange(cards.Where(card => card.IsCode(CardId.AlternativeWhiteDragon)));
                return this.Util.CheckSelectCount(result, cards, min, max);
            }
            Logger.DebugWriteLine("Use default.");
            return null;
        }

        public override IList<ClientCard> OnSelectXyzMaterial(IList<ClientCard> cards, int min, int max)
        {
            Logger.DebugWriteLine("OnSelectXyzMaterial " + cards.Count + " " + min + " " + max);
            IList<ClientCard> result = this.Util.SelectPreferredCards(this.UsedAlternativeWhiteDragon, cards, min, max);
            return this.Util.CheckSelectCount(result, cards, min, max);
        }

        public override IList<ClientCard> OnSelectSynchroMaterial(IList<ClientCard> cards, int sum, int min, int max)
        {
            Logger.DebugWriteLine("OnSelectSynchroMaterial " + cards.Count + " " + sum + " " + min + " " + max);
            if (sum != 8)
            {
                return null;
            }

            foreach (ClientCard AlternativeWhiteDragon in this.UsedAlternativeWhiteDragon)
            {
                if (cards.IndexOf(AlternativeWhiteDragon) > 0)
                {
                    this.UsedAlternativeWhiteDragon.Remove(AlternativeWhiteDragon);
                    Logger.DebugWriteLine("select UsedAlternativeWhiteDragon");
                    return new[] { AlternativeWhiteDragon };
                }
            }

            return null;
        }

        private bool DragonShrineEffect()
        {
            this.AI.SelectCard(
                CardId.DragonSpiritOfWhite,
                CardId.WhiteDragon,
                CardId.WhiteStoneOfAncients,
                CardId.WhiteStoneOfLegend
                );
            if (!this.Bot.HasInHand(CardId.WhiteDragon))
            {
                this.AI.SelectNextCard(CardId.WhiteStoneOfLegend);
            }
            else
            {
                this.AI.SelectNextCard(
                    CardId.WhiteStoneOfAncients,
                    CardId.DragonSpiritOfWhite,
                    CardId.WhiteStoneOfLegend
                    );
            }
            return true;
        }

        private bool MelodyOfAwakeningDragonEffect()
        {
            this.AI.SelectCard(
                CardId.WhiteStoneOfAncients,
                CardId.DragonSpiritOfWhite,
                CardId.WhiteStoneOfLegend,
                CardId.GalaxyCyclone,
                CardId.EffectVeiler,
                CardId.TradeIn,
                CardId.SageWithEyesOfBlue
                );
            return true;
        }

        private bool CardsOfConsonanceEffect()
        {
            if (!this.Bot.HasInHand(CardId.WhiteDragon))
            {
                this.AI.SelectCard(CardId.WhiteStoneOfLegend);
            }
            else if (this.Bot.HasInHand(CardId.TradeIn))
            {
                this.AI.SelectCard(CardId.WhiteStoneOfLegend);
            }
            else
            {
                this.AI.SelectCard(CardId.WhiteStoneOfAncients);
            }
            return true;
        }

        private bool TradeInEffect()
        {
            if (this.Bot.HasInHand(CardId.DragonSpiritOfWhite))
            {
                this.AI.SelectCard(CardId.DragonSpiritOfWhite);
                return true;
            }
            else if (this.HasTwoInHand(CardId.WhiteDragon))
            {
                this.AI.SelectCard(CardId.WhiteDragon);
                return true;
            }
            else if (this.HasTwoInHand(CardId.AlternativeWhiteDragon))
            {
                this.AI.SelectCard(CardId.AlternativeWhiteDragon);
                return true;
            }
            else if (!this.Bot.HasInHand(CardId.WhiteDragon) || !this.Bot.HasInHand(CardId.AlternativeWhiteDragon))
            {
                this.AI.SelectCard(CardId.WhiteDragon, CardId.AlternativeWhiteDragon);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool AlternativeWhiteDragonEffect()
        {
            ClientCard target = this.Util.GetProblematicEnemyMonster(this.Card.GetDefensePower());
            if (target != null)
            {
                this.AI.SelectCard(target);
                this.UsedAlternativeWhiteDragon.Add(this.Card);
                return true;
            }
            if (this.Util.GetBotAvailZonesFromExtraDeck(this.Card) > 0
                && (this.Bot.HasInMonstersZone(new[]
                {
                    CardId.SageWithEyesOfBlue,
                    CardId.WhiteStoneOfAncients,
                    CardId.WhiteStoneOfLegend,
                    CardId.WhiteDragon,
                    CardId.DragonSpiritOfWhite
                }) || this.Bot.GetCountCardInZone(this.Bot.MonsterZone, CardId.AlternativeWhiteDragon) >= 2))
            {
                target = this.Util.GetBestEnemyMonster(false, true);
                this.AI.SelectCard(target);
                this.UsedAlternativeWhiteDragon.Add(this.Card);
                return true;
            }
            return false;
        }

        private bool RebornEffect()
        {
            if (this.Duel.Player == 0 && this.Duel.CurrentChain.Count > 0)
            {
                // Silver's Cry spsummon Dragon Spirit at chain 2 will miss the timing
                return false;
            }
            if (this.Duel.Player == 0 && (this.Duel.Phase == DuelPhase.Draw || this.Duel.Phase == DuelPhase.Standby))
            {
                // Let Azure-Eyes spsummon first
                return false;
            }
            IList<int> targets = new[] {
                    CardId.HopeHarbingerDragonTitanicGalaxy,
                    CardId.GalaxyEyesDarkMatterDragon,
                    CardId.AlternativeWhiteDragon,
                    CardId.AzureEyesSilverDragon,
                    CardId.BlueEyesSpiritDragon,
                    CardId.WhiteDragon,
                    CardId.DragonSpiritOfWhite
                };
            if (!this.Bot.HasInGraveyard(targets))
            {
                return false;
            }
            ClientCard floodgate = this.Enemy.SpellZone.GetFloodgate();
            if (floodgate != null && this.Bot.HasInGraveyard(CardId.DragonSpiritOfWhite))
            {
                this.AI.SelectCard(CardId.DragonSpiritOfWhite);
            }
            else
            {
                this.AI.SelectCard(targets);
            }
            return true;
        }

        private bool AzureEyesSilverDragonEffect()
        {
            if (this.Enemy.GetSpellCount() > 0)
            {
                this.AI.SelectCard(CardId.DragonSpiritOfWhite);
            }
            else
            {
                this.AI.SelectCard(CardId.WhiteDragon);
            }
            return true;
        }

        private bool SageWithEyesOfBlueSummon()
        {
            return !this.Bot.HasInHand(new[]
                {
                    CardId.WhiteStoneOfAncients,
                    CardId.WhiteStoneOfLegend
                });
        }

        private bool SageWithEyesOfBlueEffect()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                return false;
            }
            this.AI.SelectCard(
                CardId.WhiteStoneOfAncients,
                CardId.EffectVeiler,
                CardId.WhiteStoneOfLegend
                );
            return true;
        }

        private bool WhiteStoneSummonForSage()
        {
            return this.Bot.HasInHand(CardId.SageWithEyesOfBlue);
        }

        private bool SageWithEyesOfBlueEffectInHand()
        {
            if (this.Card.Location != CardLocation.Hand)
            {
                return false;
            }
            if (!this.Bot.HasInMonstersZone(new[]
                {
                    CardId.WhiteStoneOfLegend,
                    CardId.WhiteStoneOfAncients
                }) || this.Bot.HasInMonstersZone(new[]
                {
                    CardId.AlternativeWhiteDragon,
                    CardId.WhiteDragon,
                    CardId.DragonSpiritOfWhite
                }))
            {
                return false;
            }
            this.AI.SelectCard(CardId.WhiteStoneOfLegend, CardId.WhiteStoneOfAncients);
            if (this.Enemy.GetSpellCount() > 0)
            {
                this.AI.SelectNextCard(CardId.DragonSpiritOfWhite);
            }
            else
            {
                this.AI.SelectNextCard(CardId.WhiteDragon);
            }
            return true;
        }

        private bool DragonSpiritOfWhiteEffect()
        {
            if (this.ActivateDescription == -1)
            {
                ClientCard target = this.Util.GetBestEnemySpell();
                this.AI.SelectCard(target);
                return true;
            }
            else if (this.HaveEnoughWhiteDragonInHand())
            {
                if (this.Duel.Player == 0 && this.Duel.Phase == DuelPhase.BattleStart)
                {
                    return this.Card.Attacked;
                }
                if (this.Duel.Player == 1 && this.Duel.Phase == DuelPhase.End)
                {
                    return this.Bot.HasInMonstersZone(CardId.AzureEyesSilverDragon, true)
                        && !this.Bot.HasInGraveyard(CardId.DragonSpiritOfWhite)
                        && !this.Bot.HasInGraveyard(CardId.WhiteDragon);
                }
                if (this.Util.IsChainTarget(this.Card))
                {
                    return true;
                }
            }
            return false;
        }

        private bool BlueEyesSpiritDragonEffect()
        {
            if (this.ActivateDescription == -1 || this.ActivateDescription == this.Util.GetStringId(CardId.BlueEyesSpiritDragon, 0))
            {
                return this.Duel.LastChainPlayer == 1;
            }
            else if (this.Duel.Player == 1 && (this.Duel.Phase == DuelPhase.BattleStart || this.Duel.Phase == DuelPhase.End))
            {
                this.AI.SelectCard(CardId.AzureEyesSilverDragon);
                return true;
            }
            else
            {
                if (this.Util.IsChainTarget(this.Card))
                {
                    this.AI.SelectCard(CardId.AzureEyesSilverDragon);
                    return true;
                }
                return false;
            }
        }

        private bool HopeHarbingerDragonTitanicGalaxyEffect()
        {
            if (this.ActivateDescription == -1 || this.ActivateDescription == this.Util.GetStringId(CardId.HopeHarbingerDragonTitanicGalaxy, 0))
            {
                return this.Duel.LastChainPlayer == 1;
            }
            return true;
        }

        private bool WhiteStoneOfAncientsEffect()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.WhiteStoneOfAncients, 0))
            {
                if (this.Bot.HasInHand(CardId.TradeIn)
                    && !this.Bot.HasInHand(CardId.WhiteDragon)
                    && !this.Bot.HasInHand(CardId.AlternativeWhiteDragon))
                {
                    this.AI.SelectCard(CardId.WhiteDragon);
                    return true;
                }
                if (this.AlternativeWhiteDragonSummoned)
                {
                    return false;
                }
                if (this.Bot.HasInHand(CardId.WhiteDragon)
                    && !this.Bot.HasInHand(CardId.AlternativeWhiteDragon)
                    && this.Bot.HasInGraveyard(CardId.AlternativeWhiteDragon))
                {
                    this.AI.SelectCard(CardId.AlternativeWhiteDragon);
                    return true;
                }
                if (this.Bot.HasInHand(CardId.AlternativeWhiteDragon)
                    && !this.Bot.HasInHand(CardId.WhiteDragon)
                    && this.Bot.HasInGraveyard(CardId.WhiteDragon))
                {
                    this.AI.SelectCard(CardId.WhiteDragon);
                    return true;
                }
                return false;
            }
            else
            {
                if (this.Enemy.GetSpellCount() > 0)
                {
                    this.AI.SelectCard(CardId.DragonSpiritOfWhite);
                }
                else
                {
                    this.AI.SelectCard(CardId.WhiteDragon);
                }
                return true;
            }
        }

        private bool AlternativeWhiteDragonSummon()
        {
            this.AlternativeWhiteDragonSummoned = true;
            return true;
        }

        private bool WhiteStoneSummon()
        {
            return this.Bot.HasInMonstersZone(new[]
                {
                    CardId.SageWithEyesOfBlue,
                    CardId.WhiteStoneOfAncients,
                    CardId.WhiteStoneOfLegend,
                    CardId.AlternativeWhiteDragon,
                    CardId.WhiteDragon,
                    CardId.DragonSpiritOfWhite
                });
        }

        private bool GalaxyEyesCipherDragonSummon()
        {
            if (this.Duel.Turn == 1 || this.SoulChargeUsed)
            {
                return false;
            }
            List<ClientCard> monsters = this.Enemy.GetMonsters();
            if (monsters.Count == 1 && !monsters[0].IsFacedown() && ((monsters[0].IsDefense() && monsters[0].GetDefensePower() >= 3000) && monsters[0].HasType(CardType.Xyz)))
            {
                return true;
            }
            if (monsters.Count >= 3)
            {
                foreach (ClientCard monster in monsters)
                {
                    if (!monster.IsFacedown() && ((monster.IsDefense() && monster.GetDefensePower() >= 3000) || monster.HasType(CardType.Xyz)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool GalaxyEyesPrimePhotonDragonSummon()
        {
            if (this.Duel.Turn == 1)
            {
                return false;
            }
            if (this.Util.IsOneEnemyBetterThanValue(2999, false))
            {
                return true;
            }
            return false;
        }

        private bool GalaxyEyesFullArmorPhotonDragonSummon()
        {
            if (this.Bot.HasInMonstersZone(CardId.GalaxyEyesCipherDragon))
            {
                foreach (ClientCard monster in this.Bot.GetMonsters())
                {
                    if ((monster.IsDisabled() && monster.HasType(CardType.Xyz) && !monster.Equals(this.UsedGalaxyEyesCipherDragon))
                        || (this.Duel.Phase == DuelPhase.Main2 && monster.Equals(this.UsedGalaxyEyesCipherDragon)))
                    {
                        this.AI.SelectCard(monster);
                        return true;
                    }
                }
            }
            if (this.Bot.HasInMonstersZone(CardId.GalaxyEyesPrimePhotonDragon))
            {
                if (!this.Util.IsOneEnemyBetterThanValue(4000, false))
                {
                    this.AI.SelectCard(CardId.GalaxyEyesPrimePhotonDragon);
                    return true;
                }
            }
            return false;
        }

        private bool GalaxyEyesCipherBladeDragonSummon()
        {
            if (this.Bot.HasInMonstersZone(CardId.GalaxyEyesFullArmorPhotonDragon) && this.Util.GetProblematicEnemyCard() != null)
            {
                this.AI.SelectCard(CardId.GalaxyEyesFullArmorPhotonDragon);
                return true;
            }
            return false;
        }

        private bool GalaxyEyesDarkMatterDragonSummon()
        {
            if (this.Bot.HasInMonstersZone(CardId.GalaxyEyesFullArmorPhotonDragon))
            {
                this.AI.SelectCard(CardId.GalaxyEyesFullArmorPhotonDragon);
                return true;
            }
            return false;
        }

        private bool GalaxyEyesPrimePhotonDragonEffect()
        {
            return true;
        }

        private bool GalaxyEyesCipherDragonEffect()
        {
            List<ClientCard> monsters = this.Enemy.GetMonsters();
            foreach (ClientCard monster in monsters)
            {
                if (monster.HasType(CardType.Xyz))
                {
                    this.AI.SelectCard(monster);
                    this.UsedGalaxyEyesCipherDragon = this.Card;
                    return true;
                }
            }
            foreach (ClientCard monster in monsters)
            {
                if (monster.IsDefense())
                {
                    this.AI.SelectCard(monster);
                    this.UsedGalaxyEyesCipherDragon = this.Card;
                    return true;
                }
            }
            this.UsedGalaxyEyesCipherDragon = this.Card;
            return true;
        }

        private bool GalaxyEyesFullArmorPhotonDragonEffect()
        {
            ClientCard target = this.Util.GetProblematicEnemySpell();
            if (target != null)
            {
                this.AI.SelectCard(target);
                return true;
            }
            target = this.Util.GetProblematicEnemyMonster();
            if (target != null)
            {
                this.AI.SelectCard(target);
                return true;
            }
            foreach (ClientCard spell in this.Enemy.GetSpells())
            {
                if (spell.IsFaceup())
                {
                    this.AI.SelectCard(spell);
                    return true;
                }
            }
            List<ClientCard> monsters = this.Enemy.GetMonsters();
            if (monsters.Count >= 2)
            {
                foreach (ClientCard monster in monsters)
                {
                    if (monster.IsDefense())
                    {
                        this.AI.SelectCard(monster);
                        return true;
                    }
                }
                return true;
            }
            if (monsters.Count == 2)
            {
                foreach (ClientCard monster in monsters)
                {
                    if (monster.IsMonsterInvincible() || monster.IsMonsterDangerous() || monster.GetDefensePower() > 4000)
                    {
                        this.AI.SelectCard(monster);
                        return true;
                    }
                }
            }
            if (monsters.Count == 1)
            {
                return true;
            }
            return false;
        }

        private bool GalaxyEyesCipherBladeDragonEffect()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                return true;
            }
            ClientCard target = this.Util.GetProblematicEnemyCard();
            if (target != null)
            {
                this.AI.SelectCard(target);
                return true;
            }
            List<ClientCard> monsters = this.Enemy.GetMonsters();
            foreach (ClientCard monster in monsters)
            {
                if (monster.IsDefense())
                {
                    this.AI.SelectCard(monster);
                    return true;
                }
            }
            foreach (ClientCard monster in monsters)
            {
                this.AI.SelectCard(monster);
                return true;
            }
            List<ClientCard> spells = this.Enemy.GetSpells();
            foreach (ClientCard spell in spells)
            {
                if (spell.IsFacedown())
                {
                    this.AI.SelectCard(spell);
                    return true;
                }
            }
            foreach (ClientCard spell in spells)
            {
                this.AI.SelectCard(spell);
                return true;
            }
            return false;
        }

        private bool GalaxyEyesDarkMatterDragonEffect()
        {
            this.AI.SelectCard(
                CardId.WhiteStoneOfAncients,
                CardId.WhiteStoneOfLegend,
                CardId.DragonSpiritOfWhite,
                CardId.WhiteDragon
                );
            this.AI.SelectNextCard(
                CardId.WhiteStoneOfAncients,
                CardId.WhiteStoneOfLegend,
                CardId.DragonSpiritOfWhite,
                CardId.WhiteDragon
                );
            return true;
        }

        private bool GiganticastleSummon()
        {
            if (this.Duel.Phase != DuelPhase.Main1 || this.Duel.Turn == 1 || this.SoulChargeUsed)
            {
                return false;
            }

            int bestSelfAttack = this.Util.GetBestAttack(this.Bot);
            int bestEnemyAttack = this.Util.GetBestPower(this.Enemy);
            return bestSelfAttack <= bestEnemyAttack && bestEnemyAttack > 2500 && bestEnemyAttack <= 3100;
        }

        private bool BlueEyesSpiritDragonSummon()
        {
            if (this.Duel.Phase == DuelPhase.Main1)
            {
                if (this.UsedAlternativeWhiteDragon.Count > 0)
                {
                    return true;
                }
                if (this.Duel.Turn == 1 || this.SoulChargeUsed)
                {
                    this.AI.SelectPosition(CardPosition.FaceUpDefence);
                    return true;
                }
            }
            if (this.Duel.Phase == DuelPhase.Main2)
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;
        }

        private bool HopeHarbingerDragonTitanicGalaxySummon()
        {
            if (this.Duel.Phase == DuelPhase.Main1)
            {
                if (this.UsedAlternativeWhiteDragon.Count > 0)
                {
                    return true;
                }
                if (this.Duel.Turn == 1 || this.SoulChargeUsed)
                {
                    return true;
                }
            }
            if (this.Duel.Phase == DuelPhase.Main2)
            {
                return true;
            }
            return false;
        }

        private bool SylvanPrincesspriteSummon()
        {
            if (this.Duel.Turn == 1)
            {
                return true;
            }
            if (this.Duel.Phase == DuelPhase.Main1 && !this.Bot.HasInMonstersZone(new[]
                {
                    CardId.AlternativeWhiteDragon,
                    CardId.WhiteDragon,
                    CardId.DragonSpiritOfWhite
                }))
            {
                return true;
            }
            if (this.Duel.Phase == DuelPhase.Main2 || this.SoulChargeUsed)
            {
                return true;
            }
            return false;
        }

        private bool SylvanPrincesspriteEffect()
        {
            this.AI.SelectCard(CardId.WhiteStoneOfLegend, CardId.WhiteStoneOfAncients);
            return true;
        }

        private bool SoulChargeEffect()
        {
            if (this.Bot.HasInMonstersZone(CardId.BlueEyesSpiritDragon, true))
            {
                return false;
            }

            int count = this.Bot.GetGraveyardMonsters().Count;
            int space = 5 - this.Bot.GetMonstersInMainZone().Count;
            if (count < space)
            {
                count = space;
            }

            if (count < 2 || this.Bot.LifePoints < count*1000)
            {
                return false;
            }

            if (this.Duel.Turn != 1)
            {
                int attack = 0;
                int defence = 0;
                foreach (ClientCard monster in this.Bot.GetMonsters())
                {
                    if (!monster.IsDefense())
                    {
                        attack += monster.Attack;
                    }
                }
                foreach (ClientCard monster in this.Enemy.GetMonsters())
                {
                    defence += monster.GetDefensePower();
                }
                if (attack - defence > this.Enemy.LifePoints)
                {
                    return false;
                }
            }
            this.AI.SelectCard(
                CardId.BlueEyesSpiritDragon,
                CardId.HopeHarbingerDragonTitanicGalaxy,
                CardId.AlternativeWhiteDragon,
                CardId.WhiteDragon,
                CardId.DragonSpiritOfWhite,
                CardId.AzureEyesSilverDragon,
                CardId.WhiteStoneOfAncients,
                CardId.WhiteStoneOfLegend
                );
            this.SoulChargeUsed = true;
            return true;
        }

        private bool Repos()
        {
            bool enemyBetter = this.Util.IsAllEnemyBetter(true);

            if (this.Card.IsAttack() && enemyBetter)
            {
                return true;
            }

            if (this.Card.IsFacedown())
            {
                return true;
            }

            if (this.Card.IsDefense() && !enemyBetter && this.Card.Attack >= this.Card.Defense)
            {
                return true;
            }

            if (this.Card.IsDefense() && this.Card.IsCode(CardId.BlueEyesSpiritDragon, CardId.AzureEyesSilverDragon))
            {
                return true;
            }

            if (this.Card.IsAttack() && this.Card.IsCode(CardId.SageWithEyesOfBlue, CardId.WhiteStoneOfAncients, CardId.WhiteStoneOfLegend))
            {
                return true;
            }

            return false;
        }

        private bool SpellSet()
        {
            return (this.Card.IsTrap() || this.Card.IsCode(CardId.SilversCry)) && this.Bot.GetSpellCountWithoutField() < 4;
        }

        private bool HasTwoInHand(int id)
        {
            int num = 0;
            foreach (ClientCard card in this.Bot.Hand)
            {
                if (card != null && card.IsCode(id))
                {
                    num++;
                }
            }
            return num >= 2;
        }

        private bool HaveEnoughWhiteDragonInHand()
        {
            return this.HasTwoInHand(CardId.WhiteDragon) || (
                this.Bot.HasInGraveyard(CardId.WhiteDragon)
                && this.Bot.HasInGraveyard(CardId.WhiteStoneOfAncients)
                );
        }
    }
}
