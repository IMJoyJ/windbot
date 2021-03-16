using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using System.Linq;

namespace WindBot.Game.AI.Decks
{
    [Deck("TimeThief", "AI_Timethief")]
    public class TimeThiefExecutor : DefaultExecutor
    {
        public class Monsters
        {
            //monsters
            public const int TimeThiefWinder = 56308388;
            public const int TimeThiefBezelShip = 82496079;
            public const int TimeThiefCronocorder = 74578720;
            public const int TimeThiefRegulator = 19891131;
            public const int PhotonTrasher = 65367484;
            public const int PerformTrickClown = 67696066;
            public const int ThunderKingRaiOh = 71564252;
            public const int MaxxC = 23434538;
            public const int AshBlossomAndJoyousSpring = 14558127;
        }

        public class CardId
        {
            public const int ImperialOrder = 61740673;
            public const int NaturalExterio = 99916754;
            public const int NaturalBeast = 33198837;
            public const int SwordsmanLV7 = 37267041;
            public const int RoyalDecreel = 51452091;
        }

        public class Spells
        {
            // spells
            public const int Raigeki = 12580477;
            public const int FoolishBurial = 81439173;
            public const int TimeThiefStartup = 10877309;
            public const int TimeThiefHack = 81670445;
            public const int HarpieFeatherDuster = 18144506;
            public const int PotOfDesires = 35261759;
            public const int PotofExtravagance = 49238328;
        }
        public class Traps
        {
            //traps
            public const int SolemnWarning = 84749824;
            public const int SolemStrike = 40605147;
            public const int SolemnJudgment = 41420027;
            public const int TimeThiefRetrograte = 76587747;
            public const int PhantomKnightsShade = 98827725;
            public const int TimeThiefFlyBack = 18678554;
            public const int Crackdown = 36975314;
        }
        public class XYZs
        {
            //xyz
            public const int TimeThiefRedoer = 55285840;
            public const int TimeThiefPerpetua = 59208943;
            public const int CrazyBox = 42421606;
            public const int GagagaCowboy = 12014404;
            public const int Number39Utopia = 84013237;
            public const int NumberS39UtopiatheLightning = 56832966;
            public const int NumberS39UtopiaOne = 86532744;
            public const int DarkRebellionXyzDragon = 16195942;
            public const int EvilswarmExcitonKnight = 46772449;
        }



        public TimeThiefExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // executors
            //Spell activate
            this.AddExecutor(ExecutorType.Activate, Spells.Raigeki, this.DefaultDarkHole);
            this.AddExecutor(ExecutorType.Activate, Spells.FoolishBurial, this.FoolishBurialTarget);
            this.AddExecutor(ExecutorType.Activate, Spells.TimeThiefStartup, this.TimeThiefStartupEffect);
            this.AddExecutor(ExecutorType.Activate, Spells.TimeThiefHack);
            this.AddExecutor(ExecutorType.Activate, Spells.PotofExtravagance, this.PotofExtravaganceActivate);
            this.AddExecutor(ExecutorType.Activate, Spells.HarpieFeatherDuster, this.DefaultHarpiesFeatherDusterFirst);
            this.AddExecutor(ExecutorType.Activate, Spells.PotOfDesires, this.PotOfDesireseff);
            // trap executors set
            this.AddExecutor(ExecutorType.SpellSet, Traps.PhantomKnightsShade);
            this.AddExecutor(ExecutorType.SpellSet, Traps.TimeThiefRetrograte);
            this.AddExecutor(ExecutorType.SpellSet, Traps.TimeThiefFlyBack);
            this.AddExecutor(ExecutorType.SpellSet, Traps.SolemnWarning);
            this.AddExecutor(ExecutorType.SpellSet, Traps.SolemStrike);
            this.AddExecutor(ExecutorType.SpellSet, Traps.SolemnJudgment);
            this.AddExecutor(ExecutorType.SpellSet, Traps.Crackdown);
            //normal summons
            this.AddExecutor(ExecutorType.Summon, Monsters.TimeThiefRegulator);
            this.AddExecutor(ExecutorType.SpSummon, Monsters.PhotonTrasher, this.SummonToDef);
            this.AddExecutor(ExecutorType.Summon, Monsters.TimeThiefWinder);
            this.AddExecutor(ExecutorType.Summon, Monsters.TimeThiefBezelShip);
            this.AddExecutor(ExecutorType.Summon, Monsters.PerformTrickClown);
            this.AddExecutor(ExecutorType.Summon, Monsters.TimeThiefCronocorder);
            this.AddExecutor(ExecutorType.Summon, Monsters.ThunderKingRaiOh, this.ThunderKingRaiOhsummon);
            //xyz summons
            this.AddExecutor(ExecutorType.SpSummon, XYZs.TimeThiefRedoer);
            this.AddExecutor(ExecutorType.SpSummon, XYZs.TimeThiefPerpetua);
            this.AddExecutor(ExecutorType.SpSummon, XYZs.EvilswarmExcitonKnight, this.DefaultEvilswarmExcitonKnightSummon);
            this.AddExecutor(ExecutorType.SpSummon, XYZs.GagagaCowboy, this.GagagaCowboySummon);
            this.AddExecutor(ExecutorType.SpSummon, XYZs.Number39Utopia, this.DefaultNumberS39UtopiaTheLightningSummon);
            this.AddExecutor(ExecutorType.SpSummon, XYZs.NumberS39UtopiaOne);
            this.AddExecutor(ExecutorType.SpSummon, XYZs.NumberS39UtopiatheLightning);
            this.AddExecutor(ExecutorType.SpSummon, XYZs.DarkRebellionXyzDragon, this.DarkRebellionXyzDragonSummon);
            //activate trap
            this.AddExecutor(ExecutorType.Activate, Traps.PhantomKnightsShade);
            this.AddExecutor(ExecutorType.Activate, Traps.TimeThiefRetrograte, this.RetrograteEffect);
            this.AddExecutor(ExecutorType.Activate, Traps.TimeThiefFlyBack);
            this.AddExecutor(ExecutorType.Activate, Traps.SolemnWarning, this.DefaultSolemnWarning);
            this.AddExecutor(ExecutorType.Activate, Traps.SolemStrike, this.DefaultSolemnStrike);
            this.AddExecutor(ExecutorType.Activate, Traps.SolemnJudgment, this.DefaultSolemnJudgment);
            this.AddExecutor(ExecutorType.Activate, Traps.Crackdown, this.Crackdowneff);
            //xyz effects
            this.AddExecutor(ExecutorType.Activate, XYZs.TimeThiefRedoer, this.RedoerEffect);
            this.AddExecutor(ExecutorType.Activate, XYZs.TimeThiefPerpetua, this.PerpertuaEffect);
            this.AddExecutor(ExecutorType.Activate, XYZs.EvilswarmExcitonKnight, this.DefaultEvilswarmExcitonKnightEffect);
            this.AddExecutor(ExecutorType.Activate, XYZs.GagagaCowboy);
            this.AddExecutor(ExecutorType.Activate, XYZs.NumberS39UtopiatheLightning, this.DefaultNumberS39UtopiaTheLightningEffect);
            this.AddExecutor(ExecutorType.Activate, XYZs.DarkRebellionXyzDragon, this.DarkRebellionXyzDragonEffect);

            //monster effects
            this.AddExecutor(ExecutorType.Activate, Monsters.TimeThiefRegulator, this.RegulatorEffect);
            this.AddExecutor(ExecutorType.Activate, Monsters.TimeThiefWinder);
            this.AddExecutor(ExecutorType.Activate, Monsters.TimeThiefCronocorder);
            this.AddExecutor(ExecutorType.Activate, Monsters.PerformTrickClown, this.TrickClownEffect);
            this.AddExecutor(ExecutorType.Activate, Monsters.TimeThiefBezelShip);
            this.AddExecutor(ExecutorType.Activate, Monsters.ThunderKingRaiOh, this.ThunderKingRaiOheff);
            this.AddExecutor(ExecutorType.Activate, Monsters.AshBlossomAndJoyousSpring, this.DefaultAshBlossomAndJoyousSpring);
            this.AddExecutor(ExecutorType.Activate, Monsters.MaxxC, this.DefaultMaxxC);
        }

        public void SelectSTPlace(ClientCard card = null, bool avoid_Impermanence = false, List<int> avoid_list = null)
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
                    if (card != null && card.Location == CardLocation.Hand && avoid_Impermanence)
                    {
                        continue;
                    }

                    if (avoid_list != null && avoid_list.Contains(seq))
                    {
                        continue;
                    }

                    this.AI.SelectPlace(zone);
                    return;
                };
            }
            this.AI.SelectPlace(0);
        }

        public bool SpellNegatable(bool isCounter = false, ClientCard target = null)
        {
            // target default set
            if (target == null)
            {
                target = this.Card;
            }
            // won't negate if not on field
            if (target.Location != CardLocation.SpellZone && target.Location != CardLocation.Hand)
            {
                return false;
            }

            // negate judge
            if (this.Enemy.HasInMonstersZone(CardId.NaturalExterio, true) && !isCounter)
            {
                return true;
            }

            if (target.IsSpell())
            {
                if (this.Enemy.HasInMonstersZone(CardId.NaturalBeast, true))
                {
                    return true;
                }

                if (this.Enemy.HasInSpellZone(CardId.ImperialOrder, true) || this.Bot.HasInSpellZone(CardId.ImperialOrder, true))
                {
                    return true;
                }

                if (this.Enemy.HasInMonstersZone(CardId.SwordsmanLV7, true) || this.Bot.HasInMonstersZone(CardId.SwordsmanLV7, true))
                {
                    return true;
                }
            }
            if (target.IsTrap())
            {
                if (this.Enemy.HasInSpellZone(CardId.RoyalDecreel, true) || this.Bot.HasInSpellZone(CardId.RoyalDecreel, true))
                {
                    return true;
                }
            }
            // how to get here?
            return false;
        }
        private bool SummonToDef()
        {
            this.AI.SelectPosition(CardPosition.Defence);
            return true;
        }
        private bool RegulatorEffect()
        {
            if (this.Card.Location == CardLocation.MonsterZone)
            {
                this.AI.SelectCard(Monsters.TimeThiefCronocorder);
                this.AI.SelectCard(Monsters.TimeThiefWinder);
                return true;
            }

            if (this.Card.Location == CardLocation.Grave)
            {
                return true;
            }

            return false;
        }

        private bool PerpertuaEffect()
        {
            if (this.Bot.HasInGraveyard(XYZs.TimeThiefRedoer))
            {
                this.AI.SelectCard(XYZs.TimeThiefRedoer);
                return true;
            }

            if (this.Bot.HasInMonstersZone(XYZs.TimeThiefRedoer))
            {
                this.AI.SelectCard(Monsters.TimeThiefBezelShip);
                this.AI.SelectNextCard(XYZs.TimeThiefRedoer);
                return true;
            }

            return false;
        }

        private int _totalAttack;
        private int _totalBotAttack;
        private bool RedoerEffect()
        {

            List<ClientCard> enemy = this.Enemy.GetMonstersInMainZone();
            List<int> units = this.Card.Overlays;
            if (this.Duel.Phase == DuelPhase.Standby && (this.AI.Executor.Util.GetStringId(XYZs.TimeThiefRedoer, 0) ==
                                                    this.ActivateDescription))
            {

                return true;
            }

            try
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    this._totalAttack += enemy[i].Attack;
                }

                foreach (var t in this.Bot.GetMonsters())
                {
                    this._totalBotAttack += t.Attack;
                }

                if (this._totalAttack > this.Bot.LifePoints + this._totalBotAttack)
                {
                    return false;
                }



                foreach (var t in enemy)
                {
                    if (t.Attack < 2400 || !t.IsAttack())
                    {
                        continue;
                    }

                    try
                    {
                        this.AI.SelectCard(t.Id);
                        this.AI.SelectCard(t.Id);
                    }
                    catch { }

                    return true;
                }
            }
            catch { }

            if (this.Bot.UnderAttack)
            {
                //AI.SelectCard(Util.GetBestEnemyMonster());
                return true;
            }

            return false;

        }
        private bool RetrograteEffect()
        {
            if (this.Card.Owner == 1)
            {
                return true;
            }
            return false;

        }
        private bool TimeThiefStartupEffect()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                if (this.Bot.HasInHand(Monsters.TimeThiefRegulator) && !(this.Bot.GetMonsterCount() > 0))
                {
                    this.AI.SelectCard(Monsters.TimeThiefRegulator);
                    return true;
                }
                if (this.Bot.HasInHand(Monsters.TimeThiefWinder) && this.Bot.GetMonsterCount() > 1)
                {
                    this.AI.SelectCard(Monsters.TimeThiefWinder);
                    return true;
                }
                return true;

            }
            if (this.Card.Location == CardLocation.Grave)
            {
                this.AI.SelectCard(Monsters.TimeThiefCronocorder);
                this.AI.SelectCard(Spells.TimeThiefHack);
                this.AI.SelectCard(Traps.TimeThiefFlyBack);
                return true;
            }

            return false;

        }
        private bool FoolishBurialTarget()
        {
            this.AI.SelectCard(Monsters.PerformTrickClown);
            return true;
        }

        private bool TrickClownEffect()
        {
            if (this.Bot.LifePoints <= 1000)
            {
                return false;
            }
            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
        private bool GagagaCowboySummon()
        {
            if (this.Enemy.LifePoints <= 800 || (this.Bot.GetMonsterCount() >= 4 && this.Enemy.LifePoints <= 1600))
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;
        }

        private bool DarkRebellionXyzDragonSummon()
        {
            int selfBestAttack = this.Util.GetBestAttack(this.Bot);
            int oppoBestAttack = this.Util.GetBestAttack(this.Enemy);
            return selfBestAttack <= oppoBestAttack;
        }

        private bool DarkRebellionXyzDragonEffect()
        {
            int oppoBestAttack = this.Util.GetBestAttack(this.Enemy);
            ClientCard target = this.Util.GetOneEnemyBetterThanValue(oppoBestAttack, true);
            if (target != null)
            {
                this.AI.SelectCard(0);
                this.AI.SelectNextCard(target);
            }
            return true;
        }
        private bool ThunderKingRaiOhsummon()
        {
            if (this.Bot.MonsterZone[0] == null)
            {
                this.AI.SelectPlace(Zones.MonsterZone1);
            }
            else
            {
                this.AI.SelectPlace(Zones.MonsterZone5);
            }

            return true;
        }
        private bool ThunderKingRaiOheff()
        {
            if (this.Duel.SummoningCards.Count > 0)
            {
                foreach (ClientCard m in this.Duel.SummoningCards)
                {
                    if (m.Attack >= 1900)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool Crackdowneff()
        {
            if (this.Util.GetOneEnemyBetterThanMyBest(true, true) != null && this.Bot.UnderAttack)
            {
                this.AI.SelectCard(this.Util.GetOneEnemyBetterThanMyBest(true, true));
            }

            return this.Util.GetOneEnemyBetterThanMyBest(true, true) != null && this.Bot.UnderAttack;
        }
        private bool PotOfDesireseff()
        {
            return this.Bot.Deck.Count > 14 && !this.DefaultSpellWillBeNegated();
        }

        // activate of PotofExtravagance
        public bool PotofExtravaganceActivate()
        {
            // won't activate if it'll be negate
            if (this.SpellNegatable())
            {
                return false;
            }

            this.SelectSTPlace(this.Card, true);
            this.AI.SelectOption(1);
            return true;
        }


    }

}
