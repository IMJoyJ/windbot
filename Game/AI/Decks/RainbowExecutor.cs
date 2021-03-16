using System;
using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("Rainbow", "AI_Rainbow")]
    class RainbowExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int MysteryShellDragon = 18108166;
            public const int PhantomGryphon = 74852097;
            public const int MasterPendulumTheDracoslayer = 75195825;
            public const int AngelTrumpeter = 87979586;
            public const int MetalfoesGoldriver = 33256280;
            public const int MegalosmasherX = 81823360;
            public const int RescueRabbit = 85138716;

            public const int UnexpectedDai = 911883;
            public const int HarpiesFeatherDuster = 18144506;
            public const int PotOfDesires = 35261759;
            public const int MonsterReborn = 83764718;
            public const int SmashingGround = 97169186;

            public const int QuakingMirrorForce = 40838625;
            public const int DrowningMirrorForce = 47475363;
            public const int BlazingMirrorForce = 75249652;
            public const int StormingMirrorForce = 5650082;
            public const int MirrorForce = 44095762;
            public const int DarkMirrorForce = 20522190;
            public const int BottomlessTrapHole = 29401950;
            public const int TraptrixTrapHoleNightmare = 29616929;
            public const int StarlightRoad = 58120309;

            public const int ScarlightRedDragonArchfiend = 80666118;
            public const int IgnisterProminenceTheBlastingDracoslayer = 18239909;
            public const int StardustDragon = 44508094;
            public const int NumberS39UtopiatheLightning = 56832966;
            public const int Number37HopeWovenDragonSpiderShark = 37279508;
            public const int Number39Utopia = 84013237;
            public const int EvolzarLaggia = 74294676;
            public const int Number59CrookedCook = 82697249;
            public const int CastelTheSkyblasterMusketeer = 82633039;
            public const int StarliegePaladynamo = 61344030;
            public const int LightningChidori = 22653490;
            public const int EvilswarmExcitonKnight = 46772449;
            public const int GagagaCowboy = 12014404;
            public const int EvilswarmNightmare = 359563;
            public const int TraptrixRafflesia = 6511113;
        }

        private bool NormalSummoned = false;

        public RainbowExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.Activate, CardId.HarpiesFeatherDuster);

            this.AddExecutor(ExecutorType.Activate, CardId.UnexpectedDai, this.UnexpectedDaiEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.RescueRabbit, this.RescueRabbitSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.RescueRabbit, this.RescueRabbitEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.PotOfDesires, this.DefaultPotOfDesires);

            this.AddExecutor(ExecutorType.Summon, CardId.AngelTrumpeter, this.AngelTrumpeterSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.MegalosmasherX, this.MegalosmasherXSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.MasterPendulumTheDracoslayer, this.MasterPendulumTheDracoslayerSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.MysteryShellDragon, this.MysteryShellDragonSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.PhantomGryphon, this.PhantomGryphonSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.MetalfoesGoldriver, this.MetalfoesGoldriverSummon);

            this.AddExecutor(ExecutorType.Summon, this.NormalSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.IgnisterProminenceTheBlastingDracoslayer, this.IgnisterProminenceTheBlastingDracoslayerSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.IgnisterProminenceTheBlastingDracoslayer, this.IgnisterProminenceTheBlastingDracoslayerEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.GagagaCowboy, this.GagagaCowboySummon);
            this.AddExecutor(ExecutorType.Activate, CardId.GagagaCowboy);
            this.AddExecutor(ExecutorType.SpSummon, CardId.EvilswarmExcitonKnight, this.DefaultEvilswarmExcitonKnightSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.EvilswarmExcitonKnight, this.DefaultEvilswarmExcitonKnightEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.EvolzarLaggia, this.EvolzarLaggiaSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.EvolzarLaggia, this.EvolzarLaggiaEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.EvilswarmNightmare, this.EvilswarmNightmareSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.EvilswarmNightmare);
            this.AddExecutor(ExecutorType.SpSummon, CardId.StarliegePaladynamo, this.StarliegePaladynamoSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.StarliegePaladynamo, this.StarliegePaladynamoEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.LightningChidori, this.LightningChidoriSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.LightningChidori, this.LightningChidoriEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Number37HopeWovenDragonSpiderShark, this.Number37HopeWovenDragonSpiderSharkSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.Number37HopeWovenDragonSpiderShark);
            this.AddExecutor(ExecutorType.SpSummon, CardId.TraptrixRafflesia, this.TraptrixRafflesiaSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.TraptrixRafflesia);

            this.AddExecutor(ExecutorType.Activate, CardId.SmashingGround, this.DefaultSmashingGround);

            this.AddExecutor(ExecutorType.SpSummon, CardId.CastelTheSkyblasterMusketeer, this.DefaultCastelTheSkyblasterMusketeerSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.CastelTheSkyblasterMusketeer, this.DefaultCastelTheSkyblasterMusketeerEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.IgnisterProminenceTheBlastingDracoslayer, this.IgnisterProminenceTheBlastingDracoslayerSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.IgnisterProminenceTheBlastingDracoslayer, this.IgnisterProminenceTheBlastingDracoslayerEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.ScarlightRedDragonArchfiend, this.DefaultScarlightRedDragonArchfiendSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.ScarlightRedDragonArchfiend, this.DefaultScarlightRedDragonArchfiendEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.Number39Utopia, this.DefaultNumberS39UtopiaTheLightningSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.NumberS39UtopiatheLightning);
            this.AddExecutor(ExecutorType.Activate, CardId.NumberS39UtopiatheLightning, this.DefaultNumberS39UtopiaTheLightningEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.StardustDragon, this.DefaultStardustDragonSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.StardustDragon, this.DefaultStardustDragonEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.Number59CrookedCook, this.Number59CrookedCookSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.Number59CrookedCook, this.Number59CrookedCookEffect);

            this.AddExecutor(ExecutorType.SpellSet, CardId.StarlightRoad, this.TrapSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.QuakingMirrorForce, this.TrapSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.DrowningMirrorForce, this.TrapSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.BlazingMirrorForce, this.TrapSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.StormingMirrorForce, this.TrapSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.MirrorForce, this.TrapSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.DarkMirrorForce, this.TrapSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.BottomlessTrapHole, this.TrapSet);
            this.AddExecutor(ExecutorType.SpellSet, CardId.TraptrixTrapHoleNightmare, this.TrapSet);

            this.AddExecutor(ExecutorType.Activate, CardId.StarlightRoad, this.DefaultTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.QuakingMirrorForce, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.DrowningMirrorForce, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.BlazingMirrorForce, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.StormingMirrorForce, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.MirrorForce, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkMirrorForce, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.BottomlessTrapHole, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.TraptrixTrapHoleNightmare, this.DefaultUniqueTrap);

            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
        }

        public override void OnNewTurn()
        {
            this.NormalSummoned = false;
        }

        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (!defender.IsMonsterHasPreventActivationEffectInBattle())
            {
                if (this.Bot.HasInMonstersZone(CardId.Number37HopeWovenDragonSpiderShark, true, true))
                {
                    attacker.RealPower = attacker.RealPower + 1000;
                }
            }
            return base.OnPreBattleBetween(attacker, defender);
        }

        public override IList<ClientCard> OnSelectXyzMaterial(IList<ClientCard> cards, int min, int max)
        {
            // select cards with same name (summoned by rescue rabbit)
            Logger.DebugWriteLine("OnSelectXyzMaterial " + cards.Count + " " + min + " " + max);
            IList<ClientCard> result = new List<ClientCard>();
            foreach (ClientCard card1 in cards)
            {
                foreach (ClientCard card2 in cards)
                {
                    if (card1.IsCode(card2.Id) && !card1.Equals(card2))
                    {
                        result.Add(card1);
                        result.Add(card2);
                        break;
                    }
                }
                if (result.Count > 0)
                {
                    break;
                }
            }
            
            return this.Util.CheckSelectCount(result, cards, min, max);
        }

        private bool UnexpectedDaiEffect()
        {
            if (this.Bot.HasInHand(CardId.RescueRabbit) || this.NormalSummoned)
            {
                this.AI.SelectCard(
                    CardId.MysteryShellDragon,
                    CardId.PhantomGryphon,
                    CardId.MegalosmasherX
                    );
            }
            else if (this.Util.IsTurn1OrMain2())
            {
                if (this.Bot.HasInHand(CardId.MysteryShellDragon))
                {
                    this.AI.SelectCard(CardId.MysteryShellDragon);
                }
                else if (this.Bot.HasInHand(CardId.MegalosmasherX))
                {
                    this.AI.SelectCard(CardId.MegalosmasherX);
                }
                else if (this.Bot.HasInHand(CardId.AngelTrumpeter))
                {
                    this.AI.SelectCard(CardId.AngelTrumpeter);
                }
            }
            else
            {
                if (this.Bot.HasInHand(CardId.MegalosmasherX))
                {
                    this.AI.SelectCard(CardId.MegalosmasherX);
                }
                else if (this.Bot.HasInHand(CardId.MasterPendulumTheDracoslayer))
                {
                    this.AI.SelectCard(CardId.MasterPendulumTheDracoslayer);
                }
                else if (this.Bot.HasInHand(CardId.PhantomGryphon))
                {
                    this.AI.SelectCard(CardId.PhantomGryphon);
                }
                else if (this.Bot.HasInHand(CardId.AngelTrumpeter))
                {
                    this.AI.SelectCard(CardId.MetalfoesGoldriver, CardId.MasterPendulumTheDracoslayer);
                }
            }
            return true;
        }

        private bool RescueRabbitSummon()
        {
            return this.Util.GetBotAvailZonesFromExtraDeck() > 0
                || !this.Enemy.MonsterZone.IsExistingMatchingCard(card => card.GetDefensePower() >= 1900)
                || this.Enemy.MonsterZone.GetMatchingCardsCount(card => card.GetDefensePower() < 1900) > this.Bot.MonsterZone.GetMatchingCardsCount(card => card.Attack >= 1900);
        }

        private bool RescueRabbitEffect()
        {
            if (this.Util.IsTurn1OrMain2())
            {
                this.AI.SelectCard(
                    CardId.MegalosmasherX,
                    CardId.MysteryShellDragon
                    );
            }
            else
            {
                this.AI.SelectCard(
                    CardId.MasterPendulumTheDracoslayer,
                    CardId.PhantomGryphon,
                    CardId.MegalosmasherX,
                    CardId.MetalfoesGoldriver,
                    CardId.AngelTrumpeter
                    );
            }
            return true;
        }

        private bool MysteryShellDragonSummon()
        {
            return this.Bot.HasInMonstersZone(CardId.MysteryShellDragon);
        }
        private bool PhantomGryphonSummon()
        {
            return this.Bot.HasInMonstersZone(CardId.PhantomGryphon);
        }
        private bool MasterPendulumTheDracoslayerSummon()
        {
            return this.Bot.HasInMonstersZone(CardId.MasterPendulumTheDracoslayer);
        }
        private bool AngelTrumpeterSummon()
        {
            return this.Bot.HasInMonstersZone(CardId.AngelTrumpeter);
        }
        private bool MetalfoesGoldriverSummon()
        {
            return this.Bot.HasInMonstersZone(CardId.MetalfoesGoldriver);
        }
        private bool MegalosmasherXSummon()
        {
            return this.Bot.HasInMonstersZone(CardId.MegalosmasherX);
        }
        private bool NormalSummon()
        {
            return this.Card.Id != CardId.RescueRabbit;
        }

        private bool GagagaCowboySummon()
        {
            if (this.Enemy.LifePoints <= 800)
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;
        }

        private bool IgnisterProminenceTheBlastingDracoslayerSummon()
        {
            return this.Util.GetProblematicEnemyCard() != null;
        }

        private bool IgnisterProminenceTheBlastingDracoslayerEffect()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.IgnisterProminenceTheBlastingDracoslayer, 1))
            {
                return true;
            }

            ClientCard target1 = null;
            ClientCard target2 = this.Util.GetProblematicEnemyCard();
            List<ClientCard> spells = this.Enemy.GetSpells();
            foreach (ClientCard spell in spells)
            {
                if (spell.HasType(CardType.Pendulum) && !spell.Equals(target2))
                {
                    target1 = spell;
                    break;
                }
            }
            List<ClientCard> monsters = this.Enemy.GetMonsters();
            foreach (ClientCard monster in monsters)
            {
                if (monster.HasType(CardType.Pendulum) && !monster.Equals(target2))
                {
                    target1 = monster;
                    break;
                }
            }
            if (target2 == null && target1 != null)
            {
                foreach (ClientCard spell in spells)
                {
                    if (!spell.Equals(target1))
                    {
                        target2 = spell;
                        break;
                    }
                }
                foreach (ClientCard monster in monsters)
                {
                    if (!monster.Equals(target1))
                    {
                        target2 = monster;
                        break;
                    }
                }
            }
            if (target2 == null)
            {
                return false;
            }

            this.AI.SelectCard(target1);
            this.AI.SelectNextCard(target2);
            return true;
        }

        private bool Number37HopeWovenDragonSpiderSharkSummon()
        {
            return this.Util.IsAllEnemyBetterThanValue(1700, false) && !this.Util.IsOneEnemyBetterThanValue(3600, true);
        }

        private bool LightningChidoriSummon()
        {
            foreach (ClientCard monster in this.Enemy.GetMonsters())
            {
                if (monster.IsFacedown())
                {
                    return true;
                }
            }
            foreach (ClientCard spell in this.Enemy.GetSpells())
            {
                if (spell.IsFacedown())
                {
                    return true;
                }
            }

            return this.Util.GetProblematicEnemyCard() != null;
        }

        private bool LightningChidoriEffect()
        {
            ClientCard problematicCard = this.Util.GetProblematicEnemyCard();
            this.AI.SelectCard(0);
            this.AI.SelectNextCard(problematicCard);
            return true;
        }

        private bool EvolzarLaggiaSummon()
        {
            return (this.Util.IsAllEnemyBetterThanValue(2000, false) && !this.Util.IsOneEnemyBetterThanValue(2400, true)) || this.Util.IsTurn1OrMain2();
        }

        private bool EvilswarmNightmareSummon()
        {
            if (this.Util.IsTurn1OrMain2())
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;
        }

        private bool TraptrixRafflesiaSummon()
        {
            if (this.Util.IsTurn1OrMain2() && (this.Bot.GetRemainingCount(CardId.BottomlessTrapHole, 1) + this.Bot.GetRemainingCount(CardId.TraptrixTrapHoleNightmare, 1)) > 0)
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;
        }

        private bool Number59CrookedCookSummon()
        {
            return ((this.Bot.GetMonsterCount() + this.Bot.GetSpellCount() - 2) <= 1) &&
                ((this.Util.IsOneEnemyBetter() && !this.Util.IsOneEnemyBetterThanValue(2300, true)) || this.Util.IsTurn1OrMain2());
        }

        private bool Number59CrookedCookEffect()
        {
            if (this.Duel.Player == 0)
            {
                if (this.Util.IsChainTarget(this.Card))
                {
                    return true;
                }
            }
            else
            {
                if ((this.Bot.GetMonsterCount() + this.Bot.GetSpellCount() -1) <= 1)
                {
                    return true;
                }
            }
            return false;
        }

        private bool EvolzarLaggiaEffect()
        {
            return this.DefaultTrap();
        }

        private bool StarliegePaladynamoSummon()
        {
            return this.StarliegePaladynamoEffect();
        }

        private bool StarliegePaladynamoEffect()
        {
            ClientCard result = this.Util.GetOneEnemyBetterThanValue(2000, true);
            if (result != null)
            {
                this.AI.SelectCard(0);
                this.AI.SelectNextCard(result);
                return true;
            }
            return false;
        }

        private bool TrapSet()
        {
            return !this.Bot.HasInMonstersZone(CardId.Number59CrookedCook, true, true);
        }
    }
}
