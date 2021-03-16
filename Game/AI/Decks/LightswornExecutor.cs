using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    // NOT FINISHED YET
    [Deck("Lightsworn", "AI_Lightsworn", "NotFinished")]
    public class LightswornExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int JudgmentDragon = 57774843;
            public const int Wulf = 58996430;
            public const int Garoth = 59019082;
            public const int Raiden = 77558536;
            public const int Lyla = 22624373;
            public const int Felis = 73176465;
            public const int Lumina = 95503687;
            public const int Minerva = 40164421;
            public const int Ryko = 21502796;
            public const int PerformageTrickClown = 67696066;
            public const int Goblindbergh = 25259669;
            public const int ThousandBlades = 1833916;
            public const int Honest = 37742478;
            public const int GlowUpBulb = 67441435;

            public const int SolarRecharge = 691925;
            public const int GalaxyCyclone = 5133471;
            public const int HarpiesFeatherDuster = 18144506;
            public const int ReinforcementOfTheArmy = 32807846;
            public const int MetalfoesFusion = 73594093;
            public const int ChargeOfTheLightBrigade = 94886282;

            public const int Michael = 4779823;
            public const int MinervaTheExalted = 30100551;
            public const int TrishulaDragonOfTheIceBarrier = 52687916;
            public const int ScarlightRedDragonArchfiend = 80666118;
            public const int PSYFramelordOmega = 74586817;
            public const int PSYFramelordZeta = 37192109;
            public const int NumberS39UtopiatheLightning = 56832966;
            public const int Number39Utopia = 84013237;
            public const int CastelTheSkyblasterMusketeer = 82633039;
            public const int EvilswarmExcitonKnight = 46772449;
            public const int DanteTravelerOfTheBurningAbyss = 83531441;
            public const int DecodeTalker = 1861629;
            public const int MissusRadiant = 3987233;
        }

        bool ClownUsed = false;

        public LightswornExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.Activate, CardId.HarpiesFeatherDuster, this.DefaultHarpiesFeatherDusterFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.GalaxyCyclone, this.DefaultGalaxyCyclone);
            this.AddExecutor(ExecutorType.Activate, CardId.HarpiesFeatherDuster);

            this.AddExecutor(ExecutorType.Activate, CardId.MetalfoesFusion);
            this.AddExecutor(ExecutorType.Activate, CardId.GlowUpBulb);

            this.AddExecutor(ExecutorType.Activate, CardId.JudgmentDragon, this.DefaultDarkHole);
            this.AddExecutor(ExecutorType.SpSummon, CardId.JudgmentDragon);

            this.AddExecutor(ExecutorType.Activate, CardId.ReinforcementOfTheArmy, this.ReinforcementOfTheArmyEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.ChargeOfTheLightBrigade, this.ChargeOfTheLightBrigadeEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.SolarRecharge, this.SolarRechargeEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.Goblindbergh, this.GoblindberghSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.Goblindbergh, this.GoblindberghEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.EvilswarmExcitonKnight, this.DefaultEvilswarmExcitonKnightSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.EvilswarmExcitonKnight, this.DefaultEvilswarmExcitonKnightEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.CastelTheSkyblasterMusketeer, this.DefaultCastelTheSkyblasterMusketeerSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.CastelTheSkyblasterMusketeer, this.DefaultCastelTheSkyblasterMusketeerEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.ScarlightRedDragonArchfiend, this.DefaultScarlightRedDragonArchfiendSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.ScarlightRedDragonArchfiend, this.DefaultScarlightRedDragonArchfiendEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Number39Utopia, this.DefaultNumberS39UtopiaTheLightningSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.NumberS39UtopiatheLightning);
            this.AddExecutor(ExecutorType.Activate, CardId.NumberS39UtopiatheLightning, this.DefaultNumberS39UtopiaTheLightningEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.PerformageTrickClown, this.PerformageTrickClownEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.ThousandBlades);
            this.AddExecutor(ExecutorType.Activate, CardId.Honest, this.DefaultHonestEffect);

            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
        }

        public override void OnNewTurn()
        {
            this.ClownUsed = false;
            base.OnNewTurn();
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
            Logger.DebugWriteLine("OnSelectXyzMaterial " + cards.Count + " " + min + " " + max);
            IList<ClientCard> result = new List<ClientCard>();
            foreach (ClientCard card in cards)
            {
                if (!result.Contains(card) && (!this.ClownUsed || !card.IsCode(CardId.PerformageTrickClown)))
                {
                    result.Add(card);
                }

                if (result.Count >= max)
                {
                    break;
                }
            }
            
            return this.Util.CheckSelectCount(result, cards, min, max);
        }

        private bool ReinforcementOfTheArmyEffect()
        {
            if (!this.Bot.HasInHand(CardId.Raiden))
            {
                this.AI.SelectCard(CardId.Raiden);
                return true;
            }
            else if (!this.Bot.HasInHand(CardId.Goblindbergh))
            {
                this.AI.SelectCard(CardId.Goblindbergh);
                return true;
            }
            return false;
        }

        private bool ChargeOfTheLightBrigadeEffect()
        {
            if (!this.Bot.HasInHand(CardId.Lumina))
            {
                this.AI.SelectCard(CardId.Lumina);
            }
            else
            {
                this.AI.SelectCard(
                    CardId.Raiden,
                    CardId.Lumina,
                    CardId.Minerva,
                    CardId.Lyla
                    );
            }

            return true;
        }

        private bool SolarRechargeEffect()
        {
            this.AI.SelectCard(
                CardId.Wulf,
                CardId.Felis,
                CardId.Minerva,
                CardId.Lyla,
                CardId.Raiden
                );
            return true;
        }

        private bool GoblindberghSummon()
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
                CardId.Felis,
                CardId.Wulf,
                CardId.Raiden,
                CardId.PerformageTrickClown,
                CardId.ThousandBlades
                );
            return true;
        }

        private bool LuminaEffect()
        {
            if (!this.Bot.HasInGraveyard(CardId.Raiden) && this.Bot.HasInHand(CardId.Raiden))
            {
                this.AI.SelectCard(CardId.Raiden);
            }
            else if (!this.ClownUsed && this.Bot.HasInHand(CardId.PerformageTrickClown))
            {
                this.AI.SelectCard(CardId.PerformageTrickClown);
            }
            else
            {
                this.AI.SelectCard(
                    CardId.Wulf,
                    CardId.Felis,
                    CardId.Minerva,
                    CardId.ThousandBlades
                    );
            }
            this.AI.SelectNextCard(CardId.Raiden, CardId.Felis);
            return true;
        }

        private bool PerformageTrickClownEffect()
        {
            this.ClownUsed = true;
            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }

        private bool MinervaTheExaltedEffect()
        {
            if (this.Card.Location == CardLocation.MonsterZone)
            {
                return true;
            }
            else
            {
                IList<ClientCard> targets = new List<ClientCard>();

                ClientCard target1 = this.Util.GetBestEnemyMonster();
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
                    if (targets.Count >= 3)
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
                    if (targets.Count >= 3)
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
        }

    }
}