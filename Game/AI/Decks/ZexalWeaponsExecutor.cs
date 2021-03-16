using System;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
    [Deck("Zexal Weapons", "AI_ZexalWeapons")]
    class ZexalWeaponsExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int CyberDragon = 70095155;
            public const int ZwTornadoBringer = 81471108;
            public const int ZwLightningBlade = 45082499;
            public const int ZwAsuraStrike = 40941889;
            public const int SolarWindJammer = 33911264;
            public const int PhotonTrasher = 65367484;
            public const int StarDrawing = 24610207;
            public const int SacredCrane = 30914564;
            public const int Goblindbergh = 25259669;
            public const int Honest = 37742478;
            public const int Kagetokage = 94656263;
            public const int HeroicChallengerExtraSword = 34143852;
            public const int TinGoldfish = 18063928;
            public const int SummonerMonk = 423585;
            public const int InstantFusion = 1845204;
            public const int Raigeki = 12580477;
            public const int ReinforcementOfTheArmy = 32807846;
            public const int DarkHole = 53129443;
            public const int MysticalSpaceTyphoon = 5318639;
            public const int BreakthroughSkill = 78474168;
            public const int SolemnWarning = 84749824;
            public const int SolemnStrike = 40605147;
            public const int XyzChangeTactics = 11705261;

            public const int FlameSwordsman = 45231177;
            public const int DarkfireDragon = 17881964;
            public const int GaiaDragonTheThunderCharger = 91949988;
            public const int ZwLionArms = 60992364;
            public const int AdreusKeeperOfArmageddon = 94119480;
            public const int Number61Volcasaurus = 29669359;
            public const int GemKnightPearl = 71594310;
            public const int Number39Utopia = 84013237;
            public const int NumberS39UtopiaOne = 86532744;
            public const int NumberS39UtopiatheLightning = 56832966;
            public const int MaestrokeTheSymphonyDjinn = 25341652;
            public const int GagagaCowboy = 12014404;
        }

        public ZexalWeaponsExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // Quick spells
            this.AddExecutor(ExecutorType.Activate, CardId.MysticalSpaceTyphoon, this.DefaultMysticalSpaceTyphoon);

            // Spell cards
            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);
            this.AddExecutor(ExecutorType.Activate, CardId.Raigeki, this.DefaultRaigeki);
            this.AddExecutor(ExecutorType.Activate, CardId.ReinforcementOfTheArmy, this.ReinforcementOfTheArmy);
            this.AddExecutor(ExecutorType.Activate, CardId.XyzChangeTactics, this.XyzChangeTactics);

            // XYZ summons
            this.AddExecutor(ExecutorType.SpSummon, CardId.Number39Utopia);
            this.AddExecutor(ExecutorType.SpSummon, CardId.NumberS39UtopiaOne);
            this.AddExecutor(ExecutorType.SpSummon, CardId.NumberS39UtopiatheLightning);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Number61Volcasaurus, this.Number61Volcasaurus);
            this.AddExecutor(ExecutorType.SpSummon, CardId.ZwLionArms);
            this.AddExecutor(ExecutorType.SpSummon, CardId.AdreusKeeperOfArmageddon);

            // XYZ effects
            this.AddExecutor(ExecutorType.Activate, CardId.Number39Utopia, this.Number39Utopia);
            this.AddExecutor(ExecutorType.Activate, CardId.NumberS39UtopiaOne);
            this.AddExecutor(ExecutorType.Activate, CardId.NumberS39UtopiatheLightning, this.DefaultNumberS39UtopiaTheLightningEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.ZwLionArms, this.ZwLionArms);
            this.AddExecutor(ExecutorType.Activate, CardId.AdreusKeeperOfArmageddon);
            this.AddExecutor(ExecutorType.Activate, CardId.Number61Volcasaurus);

            // Weapons
            this.AddExecutor(ExecutorType.Activate, CardId.ZwTornadoBringer, this.ZwWeapon);
            this.AddExecutor(ExecutorType.Activate, CardId.ZwLightningBlade, this.ZwWeapon);
            this.AddExecutor(ExecutorType.Activate, CardId.ZwAsuraStrike, this.ZwWeapon);


            // Special summons
            this.AddExecutor(ExecutorType.SpSummon, CardId.PhotonTrasher);
            this.AddExecutor(ExecutorType.SpSummon, CardId.CyberDragon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.SolarWindJammer, this.SolarWindJammer);

            this.AddExecutor(ExecutorType.Activate, CardId.InstantFusion, this.InstantFusion);

            // Normal summons
            this.AddExecutor(ExecutorType.Summon, CardId.Goblindbergh, this.GoblindberghFirst);
            this.AddExecutor(ExecutorType.Summon, CardId.TinGoldfish, this.GoblindberghFirst);
            this.AddExecutor(ExecutorType.Summon, CardId.StarDrawing);
            this.AddExecutor(ExecutorType.Summon, CardId.SacredCrane);
            this.AddExecutor(ExecutorType.Summon, CardId.HeroicChallengerExtraSword);
            this.AddExecutor(ExecutorType.Summon, CardId.Goblindbergh);
            this.AddExecutor(ExecutorType.Summon, CardId.TinGoldfish);
            this.AddExecutor(ExecutorType.Summon, CardId.SummonerMonk);

            // Summons: Effects
            this.AddExecutor(ExecutorType.Activate, CardId.Goblindbergh, this.GoblindberghEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.TinGoldfish, this.GoblindberghEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Kagetokage, this.KagetokageEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.SummonerMonk, this.SummonerMonkEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Honest, this.DefaultHonestEffect);

            // Reposition
            this.AddExecutor(ExecutorType.Repos, this.MonsterRepos);

            // Spummon GaiaDragonTheThunderCharger if Volcasaurus or ZwLionArms had been used
            this.AddExecutor(ExecutorType.SpSummon, CardId.GaiaDragonTheThunderCharger);

            // Set and activate traps
            this.AddExecutor(ExecutorType.SpellSet, this.DefaultSpellSet);

            this.AddExecutor(ExecutorType.Activate, CardId.BreakthroughSkill, this.DefaultBreakthroughSkill);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnWarning, this.DefaultSolemnWarning);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, this.DefaultSolemnStrike);
        }

        private int ZwCount = 0;

        public override void OnNewTurn()
        {
            this.ZwCount = 0;
        }

        public override bool OnSelectHand()
        {
            return false;
        }

        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (!defender.IsMonsterHasPreventActivationEffectInBattle())
            {
                if (attacker.Attribute == (int)CardAttribute.Light && this.Bot.HasInHand(CardId.Honest))
                {
                    attacker.RealPower = attacker.RealPower + defender.Attack;
                }
            }
            return base.OnPreBattleBetween(attacker, defender);
        }

        public override IList<ClientCard> OnSelectXyzMaterial(IList<ClientCard> cards, int min, int max)
        {
            IList<ClientCard> result = this.Util.SelectPreferredCards(new[] {
                CardId.StarDrawing,
                CardId.SolarWindJammer,
                CardId.Goblindbergh
            }, cards, min, max);
            return this.Util.CheckSelectCount(result, cards, min, max);
        }

        private bool Number39Utopia()
        {
            if (!this.Util.HasChainedTrap(0) && this.Duel.Player == 1 && this.Duel.Phase == DuelPhase.BattleStart && this.Card.HasXyzMaterial(2))
            {
                return true;
            }

            return false;
        }

        private bool Number61Volcasaurus()
        {
            return this.Util.IsOneEnemyBetterThanValue(2000, false);
        }

        private bool ZwLionArms()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.ZwLionArms, 0))
            {
                return true;
            }

            if (this.ActivateDescription == this.Util.GetStringId(CardId.ZwLionArms, 1))
            {
                return !this.Card.IsDisabled() && this.ZwWeapon();
            }

            return false;
        }

        private bool ZwWeapon()
        {
            this.ZwCount++;
            return this.ZwCount < 10;
        }

        private bool ReinforcementOfTheArmy()
        {
            this.AI.SelectCard(
                CardId.Goblindbergh,
                CardId.TinGoldfish,
                CardId.StarDrawing,
                CardId.Kagetokage,
                CardId.SacredCrane
                );
            return true;
        }

        private bool InstantFusion()
        {
            if (this.Bot.LifePoints <= 1000)
            {
                return false;
            }

            int count4 = 0;
            int count5 = 0;
            foreach (ClientCard card in this.Bot.GetMonsters())
            {
                if (card.Level == 5)
                {
                    ++count5;
                }

                if (card.Level == 4)
                {
                    ++count4;
                }
            }
            if (count5 == 1)
            {
                this.AI.SelectCard(CardId.FlameSwordsman);
                return true;
            }
            else if (count4 == 1)
            {
                this.AI.SelectCard(CardId.DarkfireDragon);
                return true;
            }
            return false;
        }

        private bool XyzChangeTactics()
        {
            return this.Bot.LifePoints > 500;
        }

        private bool GoblindberghFirst()
        {
            foreach (ClientCard card in this.Bot.Hand.GetMonsters())
            {
                if (!card.Equals(this.Card) && card.Level == 4)
                {
                    return true;
                }
            }
            return false;
        }

        private bool GoblindberghEffect()
        {
            this.AI.SelectCard(
                CardId.SacredCrane,
                CardId.HeroicChallengerExtraSword,
                CardId.StarDrawing,
                CardId.SummonerMonk
                );
            return true;
        }

        private bool KagetokageEffect()
        {
            var lastChainCard = this.Util.GetLastChainCard();
            if (lastChainCard == null)
            {
                return true;
            }

            return !lastChainCard.IsCode(CardId.Goblindbergh, CardId.TinGoldfish);
        }

        private bool SummonerMonkEffect()
        {
            IList<int> costs = new[]
                {
                    CardId.XyzChangeTactics,
                    CardId.DarkHole,
                    CardId.MysticalSpaceTyphoon,
                    CardId.InstantFusion
                };
            if (this.Bot.HasInHand(costs))
            {
                this.AI.SelectCard(costs);
                this.AI.SelectNextCard(
                    CardId.SacredCrane,
                    CardId.StarDrawing,
                    CardId.Goblindbergh,
                    CardId.TinGoldfish
                    );
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;
        }

        private bool SolarWindJammer()
        {
            if (!this.Bot.HasInHand(new[] {
                    CardId.StarDrawing,
                    CardId.InstantFusion
                }))
            {
                return false;
            }

            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }

        private bool MonsterRepos()
        {
            if (this.Card.IsCode(CardId.NumberS39UtopiatheLightning) && this.Card.IsAttack())
            {
                return false;
            }

            return base.DefaultMonsterRepos();
        }
    }
}
