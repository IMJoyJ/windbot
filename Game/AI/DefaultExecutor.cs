using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI
{
    public abstract class DefaultExecutor : Executor
    {
        protected class CardId
        {
            public const int JizukirutheStarDestroyingKaiju = 63941210;
            public const int ThunderKingtheLightningstrikeKaiju = 48770333;
            public const int DogorantheMadFlameKaiju = 93332803;
            public const int RadiantheMultidimensionalKaiju = 28674152;
            public const int GadarlatheMysteryDustKaiju = 36956512;
            public const int KumongoustheStickyStringKaiju = 29726552;
            public const int GamecieltheSeaTurtleKaiju = 55063751;
            public const int SuperAntiKaijuWarMachineMechaDogoran = 84769941;

            public const int UltimateConductorTytanno = 18940556;
            public const int ElShaddollConstruct = 20366274;
            public const int AllyOfJusticeCatastor = 26593852;

            public const int DupeFrog = 46239604;
            public const int MaraudingCaptain = 2460565;

            public const int BlackRoseDragon = 73580471;
            public const int JudgmentDragon = 57774843;
            public const int TopologicTrisbaena = 72529749;
            public const int EvilswarmExcitonKnight = 46772449;
            public const int HarpiesFeatherDuster = 18144506;
            public const int DarkMagicAttack = 2314238;
            public const int MysticalSpaceTyphoon = 5318639;
            public const int CosmicCyclone = 8267140;
            public const int GalaxyCyclone = 5133471;
            public const int BookOfMoon = 14087893;
            public const int CompulsoryEvacuationDevice = 94192409;
            public const int CallOfTheHaunted = 97077563;
            public const int Scapegoat = 73915051;
            public const int BreakthroughSkill = 78474168;
            public const int SolemnJudgment = 41420027;
            public const int SolemnWarning = 84749824;
            public const int SolemnStrike = 40605147;
            public const int TorrentialTribute = 53582587;
            public const int HeavyStorm = 19613556;
            public const int HammerShot = 26412047;
            public const int DarkHole = 53129443;
            public const int Raigeki = 12580477;
            public const int SmashingGround = 97169186;
            public const int PotOfDesires = 35261759;
            public const int AllureofDarkness = 1475311;
            public const int DimensionalBarrier = 83326048;
            public const int InterruptedKaijuSlumber = 99330325;

            public const int ChickenGame = 67616300;
            public const int SantaClaws = 46565218;

            public const int CastelTheSkyblasterMusketeer = 82633039;
            public const int CrystalWingSynchroDragon = 50954680;
            public const int NumberS39UtopiaTheLightning = 56832966;
            public const int Number39Utopia = 84013237;
            public const int UltimayaTzolkin = 1686814;
            public const int MekkKnightCrusadiaAstram = 21887175;
            public const int HamonLordofStrikingThunder = 32491822;

            public const int MoonMirrorShield = 19508728;
            public const int PhantomKnightsFogBlade = 25542642;

            public const int VampireFraeulein = 6039967;
            public const int InjectionFairyLily = 79575620;

            public const int BlueEyesChaosMAXDragon = 55410871;

            public const int AshBlossom = 14558127;
            public const int MaxxC = 23434538;
            public const int LockBird = 94145021;
            public const int GhostOgreAndSnowRabbit = 59438930;
            public const int GhostBelle = 73642296;
            public const int EffectVeiler = 63845230;
            public const int ArtifactLancea = 34267821;

            public const int CalledByTheGrave = 24224830;
            public const int InfiniteImpermanence = 10045474;
            public const int GalaxySoldier = 46659709;
            public const int MacroCosmos = 30241314;
            public const int UpstartGoblin = 70368879;
            public const int CyberEmergency = 60600126;

            public const int EaterOfMillions = 63845230;

            public const int InvokedPurgatrio = 12307878;
            public const int ChaosAncientGearGiant = 51788412;
            public const int UltimateAncientGearGolem = 12652643;

            public const int RedDragonArchfiend = 70902743;

            public const int ImperialOrder = 61740673;
            public const int NaturiaBeast = 33198837;
            public const int AntiSpellFragrance = 58921041;

            public const int LightningStorm = 14532163;
        }

        int honestEffectCount = 0;

        protected DefaultExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.Activate, CardId.ChickenGame, this.DefaultChickenGame);
            this.AddExecutor(ExecutorType.Activate, CardId.SantaClaws);
        }

        /// <summary>
        /// Decide which card should the attacker attack.
        /// </summary>
        /// <param name="attacker">Card that attack.</param>
        /// <param name="defenders">Cards that defend.</param>
        /// <returns>BattlePhaseAction including the target, or null (in this situation, GameAI will check the next attacker)</returns>
        public override BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
        {
            foreach (ClientCard defender in defenders)
            {
                attacker.RealPower = attacker.Attack;
                defender.RealPower = defender.GetDefensePower();
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

        /// <summary>
        /// Decide whether to declare attack between attacker and defender.
        /// Can be overrided to update the RealPower of attacker for cards like Honest.
        /// </summary>
        /// <param name="attacker">Card that attack.</param>
        /// <param name="defender">Card that defend.</param>
        /// <returns>false if the attack shouldn't be done.</returns>
        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (attacker.RealPower <= 0)
            {
                return false;
            }

            if (!attacker.IsMonsterHasPreventActivationEffectInBattle())
            {
                if (defender.IsMonsterInvincible() && defender.IsDefense())
                {
                    return false;
                }

                if (defender.IsMonsterDangerous())
                {
                    bool canIgnoreIt = !attacker.IsDisabled() && (
                        attacker.IsCode(CardId.UltimateConductorTytanno) && defender.IsDefense() || 
                        attacker.IsCode(CardId.ElShaddollConstruct) && defender.IsSpecialSummoned ||
                        attacker.IsCode(CardId.AllyOfJusticeCatastor) && !defender.HasAttribute(CardAttribute.Dark));
                    if (!canIgnoreIt)
                    {
                        return false;
                    }
                }

                foreach (ClientCard equip in defender.EquipCards)
                {
                    if (equip.IsCode(CardId.MoonMirrorShield) && !equip.IsDisabled())
                    {
                        return false;
                    }
                }

                if (!defender.IsDisabled())
                {
                    if (defender.IsCode(CardId.MekkKnightCrusadiaAstram) && defender.IsAttack() && attacker.IsSpecialSummoned)
                    {
                        return false;
                    }

                    if (defender.IsCode(CardId.CrystalWingSynchroDragon) && defender.IsAttack() && attacker.Level >= 5)
                    {
                        return false;
                    }

                    if (defender.IsCode(CardId.AllyOfJusticeCatastor) && !attacker.HasAttribute(CardAttribute.Dark))
                    {
                        return false;
                    }

                    if (defender.IsCode(CardId.NumberS39UtopiaTheLightning) && defender.IsAttack() && defender.HasXyzMaterial(2, CardId.Number39Utopia))
                    {
                        defender.RealPower = 5000;
                    }

                    if (defender.IsCode(CardId.VampireFraeulein))
                    {
                        defender.RealPower += (this.Enemy.LifePoints > 3000) ? 3000 : (this.Enemy.LifePoints - 100);
                    }

                    if (defender.IsCode(CardId.InjectionFairyLily) && this.Enemy.LifePoints > 2000)
                    {
                        defender.RealPower += 3000;
                    }
                }
            }

            if (!defender.IsMonsterHasPreventActivationEffectInBattle())
            {
                if (attacker.IsCode(CardId.NumberS39UtopiaTheLightning) && !attacker.IsDisabled() && attacker.HasXyzMaterial(2, CardId.Number39Utopia))
                {
                    attacker.RealPower = 5000;
                }

                foreach (ClientCard equip in attacker.EquipCards)
                {
                    if (equip.IsCode(CardId.MoonMirrorShield) && !equip.IsDisabled())
                    {
                        attacker.RealPower = defender.RealPower + 100;
                    }
                }
            }

            if (this.Enemy.HasInMonstersZone(CardId.MekkKnightCrusadiaAstram, true) && !(defender).IsCode(CardId.MekkKnightCrusadiaAstram))
            {
                return false;
            }

            if (this.Enemy.HasInMonstersZone(CardId.DupeFrog, true) && !(defender).IsCode(CardId.DupeFrog))
            {
                return false;
            }

            if (this.Enemy.HasInMonstersZone(CardId.MaraudingCaptain, true) && !defender.IsCode(CardId.MaraudingCaptain) && defender.Race == (int)CardRace.Warrior)
            {
                return false;
            }

            if (defender.IsCode(CardId.UltimayaTzolkin) && !defender.IsDisabled() && this.Enemy.GetMonsters().Any(monster => !monster.Equals(defender) && monster.HasType(CardType.Synchro)))
            {
                return false;
            }

            if (this.Enemy.GetMonsters().Any(monster => !monster.Equals(defender) && monster.IsCode(CardId.HamonLordofStrikingThunder) && !monster.IsDisabled() && monster.IsDefense()))
            {
                return false;
            }

            if (defender.OwnTargets.Any(card => card.IsCode(CardId.PhantomKnightsFogBlade) && !card.IsDisabled()))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Called when the AI has to select a card position.
        /// </summary>
        /// <param name="cardId">Id of the card to position on the field.</param>
        /// <param name="positions">List of available positions.</param>
        /// <returns>Selected position, or 0 if no position is set for this card.</returns>
        public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
        {
            YGOSharp.OCGWrapper.NamedCard cardData = YGOSharp.OCGWrapper.NamedCard.Get(cardId);
            if (cardData != null)
            {
                if (cardData.Attack == 0)
                {
                    return CardPosition.FaceUpDefence;
                }
            }
            return 0;
        }

        public override bool OnSelectBattleReplay()
        {
            if (this.Bot.BattlingMonster == null)
            {
                return false;
            }

            List<ClientCard> defenders = new List<ClientCard>(this.Duel.Fields[1].GetMonsters());
            defenders.Sort(CardContainer.CompareDefensePower);
            defenders.Reverse();
            BattlePhaseAction result = this.OnSelectAttackTarget(this.Bot.BattlingMonster, defenders);
            if (result != null && result.Action == BattlePhaseAction.BattleAction.Attack)
            {
                return true;
            }
            return false;
        }

        public override void OnNewTurn()
        {
            this.honestEffectCount = 0;
        }

        /// <summary>
        /// Destroy face-down cards first, in our turn.
        /// </summary>
        protected bool DefaultMysticalSpaceTyphoon()
        {
            if (this.Duel.CurrentChain.Any(card => card.IsCode(CardId.MysticalSpaceTyphoon)))
            {
                return false;
            }

            List<ClientCard> spells = this.Enemy.GetSpells();
            if (spells.Count == 0)
            {
                return false;
            }

            ClientCard selected = this.Enemy.SpellZone.GetFloodgate();

            if (selected == null)
            {
                if (this.Duel.Player == 0)
                {
                    selected = spells.FirstOrDefault(card => card.IsFacedown());
                }

                if (this.Duel.Player == 1)
                {
                    selected = spells.FirstOrDefault(card => card.HasType(CardType.Continuous) || card.HasType(CardType.Equip) || card.HasType(CardType.Field));
                }
            }

            if (selected == null)
            {
                return false;
            }

            this.AI.SelectCard(selected);
            return true;
        }

        /// <summary>
        /// Destroy face-down cards first, in our turn.
        /// </summary>
        protected bool DefaultCosmicCyclone()
        {
            foreach (ClientCard card in this.Duel.CurrentChain)
            {
                if (card.IsCode(CardId.CosmicCyclone))
                {
                    return false;
                }
            }

            return (this.Bot.LifePoints > 1000) && this.DefaultMysticalSpaceTyphoon();
        }

        /// <summary>
        /// Activate if avail.
        /// </summary>
        protected bool DefaultGalaxyCyclone()
        {
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
                selected = spells.FirstOrDefault(card => card.IsFacedown());
            }

            if (selected == null)
            {
                return false;
            }

            this.AI.SelectCard(selected);
            return true;
        }

        /// <summary>
        /// Set the highest ATK level 4+ effect enemy monster.
        /// </summary>
        protected bool DefaultBookOfMoon()
        {
            if (this.Util.IsAllEnemyBetter(true))
            {
                ClientCard monster = this.Enemy.GetMonsters().GetHighestAttackMonster(true);
                if (monster != null && monster.HasType(CardType.Effect) && !monster.HasType(CardType.Link) && (monster.HasType(CardType.Xyz) || monster.Level > 4))
                {
                    this.AI.SelectCard(monster);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Return problematic monster, and if this card become target, return any enemy monster.
        /// </summary>
        protected bool DefaultCompulsoryEvacuationDevice()
        {
            ClientCard target = this.Util.GetProblematicEnemyMonster(0, true);
            if (target != null)
            {
                this.AI.SelectCard(target);
                return true;
            }
            if (this.Util.IsChainTarget(this.Card))
            {
                ClientCard monster = this.Util.GetBestEnemyMonster(false, true);
                if (monster != null)
                {
                    this.AI.SelectCard(monster);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Revive the best monster when we don't have better one in field.
        /// </summary>
        protected bool DefaultCallOfTheHaunted()
        {
            if (!this.Util.IsAllEnemyBetter(true))
            {
                return false;
            }

            ClientCard selected = this.Bot.Graveyard.GetMatchingCards(card => card.IsCanRevive()).OrderByDescending(card => card.Attack).FirstOrDefault();
            this.AI.SelectCard(selected);
            return true;
        }

        /// <summary>
        /// Default Scapegoat effect
        /// </summary>
        protected bool DefaultScapegoat()
        {
            if (this.DefaultSpellWillBeNegated())
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

            if (this.DefaultOnBecomeTarget())
            {
                return true;
            }

            if (this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2)
            {
                if (this.Enemy.HasInMonstersZone(new[]
                {
                    CardId.UltimateConductorTytanno,
                    CardId.InvokedPurgatrio,
                    CardId.ChaosAncientGearGiant,
                    CardId.UltimateAncientGearGolem,
                    CardId.RedDragonArchfiend
                }, true))
                {
                    return false;
                }

                if (this.Util.GetTotalAttackingMonsterAttack(1) >= this.Bot.LifePoints)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Always active in opponent's turn.
        /// </summary>
        protected bool DefaultMaxxC()
        {
            return this.Duel.Player == 1;
        }
        /// <summary>
        /// Always disable opponent's effect except some cards like UpstartGoblin
        /// </summary>
        protected bool DefaultAshBlossomAndJoyousSpring()
        {
            int[] ignoreList = {
                CardId.MacroCosmos,
                CardId.UpstartGoblin,
                CardId.CyberEmergency
            };
            if (this.Util.GetLastChainCard().IsCode(ignoreList))
            {
                return false;
            }

            if (this.Util.GetLastChainCard().HasSetcode(0x11e) && this.Util.GetLastChainCard().Location == CardLocation.Hand) // Danger! archtype hand effect
            {
                return false;
            }

            return this.Duel.LastChainPlayer == 1;
        }
        /// <summary>
        /// Always activate unless the activating card is disabled
        /// </summary>
        protected bool DefaultGhostOgreAndSnowRabbit()
        {
            if (this.Util.GetLastChainCard() != null && this.Util.GetLastChainCard().IsDisabled())
            {
                return false;
            }

            return this.DefaultTrap();
        }
        /// <summary>
        /// Always disable opponent's effect
        /// </summary>
        protected bool DefaultGhostBelleAndHauntedMansion()
        {
            return this.DefaultTrap();
        }
        /// <summary>
        /// Same as DefaultBreakthroughSkill
        /// </summary>
        protected bool DefaultEffectVeiler()
        {
            if (this.Util.GetLastChainCard() != null && this.Util.GetLastChainCard().IsCode(CardId.GalaxySoldier) && this.Enemy.Hand.Count >= 3)
            {
                return false;
            }

            if (this.Util.ChainContainsCard(CardId.EffectVeiler))
            {
                return false;
            }

            return this.DefaultBreakthroughSkill();
        }
        /// <summary>
        /// Chain common hand traps
        /// </summary>
        protected bool DefaultCalledByTheGrave()
        {
            int[] targetList =
            {
                CardId.MaxxC,
                CardId.LockBird,
                CardId.GhostOgreAndSnowRabbit,
                CardId.AshBlossom,
                CardId.GhostBelle,
                CardId.EffectVeiler,
                CardId.ArtifactLancea
            };
            if (this.Duel.LastChainPlayer == 1)
            {
                foreach (int id in targetList)
                {
                    if (this.Util.GetLastChainCard().IsCode(id))
                    {
                        this.AI.SelectCard(id);
                        return this.UniqueFaceupSpell();
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Default InfiniteImpermanence effect
        /// </summary>
        protected bool DefaultInfiniteImpermanence()
        {
            // TODO: disable s & t
            if (!this.DefaultUniqueTrap())
            {
                return false;
            }

            return this.DefaultDisableMonster();
        }
        /// <summary>
        /// Chain the enemy monster, or disable monster like Rescue Rabbit.
        /// </summary>
        protected bool DefaultBreakthroughSkill()
        {
            if (!this.DefaultUniqueTrap())
            {
                return false;
            }

            return this.DefaultDisableMonster();
        }
        /// <summary>
        /// Chain the enemy monster, or disable monster like Rescue Rabbit.
        /// </summary>
        protected bool DefaultDisableMonster()
        {
            if (this.Duel.Player == 1)
            {
                ClientCard target = this.Enemy.MonsterZone.GetShouldBeDisabledBeforeItUseEffectMonster();
                if (target != null)
                {
                    this.AI.SelectCard(target);
                    return true;
                }
            }

            ClientCard LastChainCard = this.Util.GetLastChainCard();

            if (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.MonsterZone &&
                !LastChainCard.IsDisabled() && !LastChainCard.IsShouldNotBeTarget() && !LastChainCard.IsShouldNotBeSpellTrapTarget())
            {
                this.AI.SelectCard(LastChainCard);
                return true;
            }

            if (this.Bot.BattlingMonster != null && this.Enemy.BattlingMonster != null)
            {
                if (!this.Enemy.BattlingMonster.IsDisabled() && this.Enemy.BattlingMonster.IsCode(CardId.EaterOfMillions))
                {
                    this.AI.SelectCard(this.Enemy.BattlingMonster);
                    return true;
                }
            }

            if (this.Duel.Phase == DuelPhase.BattleStart && this.Duel.Player == 1 &&
                this.Enemy.HasInMonstersZone(CardId.NumberS39UtopiaTheLightning, true))
            {
                this.AI.SelectCard(CardId.NumberS39UtopiaTheLightning);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Activate only except this card is the target or we summon monsters.
        /// </summary>
        protected bool DefaultSolemnJudgment()
        {
            return !this.Util.IsChainTargetOnly(this.Card) && !(this.Duel.Player == 0 && this.Duel.LastChainPlayer == -1) && this.DefaultTrap();
        }

        /// <summary>
        /// Activate only except we summon monsters.
        /// </summary>
        protected bool DefaultSolemnWarning()
        {
            return (this.Bot.LifePoints > 2000) && !(this.Duel.Player == 0 && this.Duel.LastChainPlayer == -1) && this.DefaultTrap();
        }

        /// <summary>
        /// Activate only except we summon monsters.
        /// </summary>
        protected bool DefaultSolemnStrike()
        {
            return (this.Bot.LifePoints > 1500) && !(this.Duel.Player == 0 && this.Duel.LastChainPlayer == -1) && this.DefaultTrap();
        }

        /// <summary>
        /// Activate when all enemy monsters have better ATK.
        /// </summary>
        protected bool DefaultTorrentialTribute()
        {
            return !this.Util.HasChainedTrap(0) && this.Util.IsAllEnemyBetter(true);
        }

        /// <summary>
        /// Activate enemy have more S&T.
        /// </summary>
        protected bool DefaultHeavyStorm()
        {
            return this.Bot.GetSpellCount() < this.Enemy.GetSpellCount();
        }

        /// <summary>
        /// Activate before other winds, if enemy have more than 2 S&T.
        /// </summary>
        protected bool DefaultHarpiesFeatherDusterFirst()
        {
            return this.Enemy.GetSpellCount() >= 2;
        }

        /// <summary>
        /// Activate when one enemy monsters have better ATK.
        /// </summary>
        protected bool DefaultHammerShot()
        {
            return this.Util.IsOneEnemyBetter(true);
        }

        /// <summary>
        /// Activate when one enemy monsters have better ATK or DEF.
        /// </summary>
        protected bool DefaultDarkHole()
        {
            return this.Util.IsOneEnemyBetter();
        }

        /// <summary>
        /// Activate when one enemy monsters have better ATK or DEF.
        /// </summary>
        protected bool DefaultRaigeki()
        {
            return this.Util.IsOneEnemyBetter();
        }

        /// <summary>
        /// Activate when one enemy monsters have better ATK or DEF.
        /// </summary>
        protected bool DefaultSmashingGround()
        {
            return this.Util.IsOneEnemyBetter();
        }

        /// <summary>
        /// Activate when we have more than 15 cards in deck.
        /// </summary>
        protected bool DefaultPotOfDesires()
        {
            return this.Bot.Deck.Count > 15;
        }

        /// <summary>
        /// Set traps only and avoid block the activation of other cards.
        /// </summary>
        protected bool DefaultSpellSet()
        {
            return (this.Card.IsTrap() || this.Card.HasType(CardType.QuickPlay)) && this.Bot.GetSpellCountWithoutField() < 4;
        }

        /// <summary>
        /// Summon with tributes ATK lower.
        /// </summary>
        protected bool DefaultTributeSummon()
        {
            if (!this.UniqueFaceupMonster())
            {
                return false;
            }

            int tributecount = (int)Math.Ceiling((this.Card.Level - 4.0d) / 2.0d);
            for (int j = 0; j < 7; ++j)
            {
                ClientCard tributeCard = this.Bot.MonsterZone[j];
                if (tributeCard == null)
                {
                    continue;
                }

                if (tributeCard.GetDefensePower() < this.Card.Attack)
                {
                    tributecount--;
                }
            }
            return tributecount <= 0;
        }

        /// <summary>
        /// Activate when we have no field.
        /// </summary>
        protected bool DefaultField()
        {
            return this.Bot.SpellZone[5] == null;
        }

        /// <summary>
        /// Turn if all enemy is better.
        /// </summary>
        protected bool DefaultMonsterRepos()
        {
            if (this.Card.IsFaceup() && this.Card.IsDefense() && this.Card.Attack == 0)
            {
                return false;
            }

            if (this.Enemy.HasInMonstersZone(CardId.BlueEyesChaosMAXDragon, true) &&
                this.Card.IsAttack() && (4000 - this.Card.Defense) * 2 > (4000 - this.Card.Attack))
            {
                return false;
            }

            if (this.Enemy.HasInMonstersZone(CardId.BlueEyesChaosMAXDragon, true) &&
                this.Card.IsDefense() && this.Card.IsFaceup() &&
                (4000 - this.Card.Defense) * 2 > (4000 - this.Card.Attack))
            {
                return true;
            }

            bool enemyBetter = this.Util.IsAllEnemyBetter(true);
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

        /// <summary>
        /// If spell will be negated
        /// </summary>
        protected bool DefaultSpellWillBeNegated()
        {
            return this.Bot.HasInSpellZone(CardId.ImperialOrder, true, true) || this.Enemy.HasInSpellZone(CardId.ImperialOrder, true) || this.Enemy.HasInMonstersZone(CardId.NaturiaBeast, true);
        }

        /// <summary>
        /// If spell must set first to activate
        /// </summary>
        protected bool DefaultSpellMustSetFirst()
        {
            ClientCard card = null;
            foreach (ClientCard check in this.Bot.GetSpells())
            {
                if (check.IsCode(CardId.AntiSpellFragrance) && !check.IsDisabled())
                {
                    card = check;
                }
            }
            if (card != null && card.IsFaceup())
            {
                return true;
            }

            return this.Bot.HasInSpellZone(CardId.AntiSpellFragrance, true, true) || this.Enemy.HasInSpellZone(CardId.AntiSpellFragrance, true);
        }

        /// <summary>
        /// if spell/trap is the target or enermy activate HarpiesFeatherDuster
        /// </summary>
        protected bool DefaultOnBecomeTarget()
        {
            if (this.Util.IsChainTarget(this.Card))
            {
                return true;
            }

            int[] destroyAllList =
            {
                CardId.EvilswarmExcitonKnight,
                CardId.BlackRoseDragon,
                CardId.JudgmentDragon,
                CardId.TopologicTrisbaena
            };
            int[] destroyAllOpponentList =
            {
                CardId.HarpiesFeatherDuster,
                CardId.DarkMagicAttack
            };

            if (this.Util.ChainContainsCard(destroyAllList))
            {
                return true;
            }

            if (this.Enemy.HasInSpellZone(destroyAllOpponentList, true))
            {
                return true;
            }
            // TODO: ChainContainsCard(id, player)
            return false;
        }
        /// <summary>
        /// Chain enemy activation or summon.
        /// </summary>
        protected bool DefaultTrap()
        {
            return (this.Duel.LastChainPlayer == -1 && this.Duel.LastSummonPlayer != 0) || this.Duel.LastChainPlayer == 1;
        }

        /// <summary>
        /// Activate when avail and no other our trap card in this chain or face-up.
        /// </summary>
        protected bool DefaultUniqueTrap()
        {
            if (this.Util.HasChainedTrap(0))
            {
                return false;
            }

            return this.UniqueFaceupSpell();
        }

        /// <summary>
        /// Check no other our spell or trap card with same name face-up.
        /// </summary>
        protected bool UniqueFaceupSpell()
        {
            return !this.Bot.GetSpells().Any(card => card.IsCode(this.Card.Id) && card.IsFaceup());
        }

        /// <summary>
        /// Check no other our monster card with same name face-up.
        /// </summary>
        protected bool UniqueFaceupMonster()
        {
            return !this.Bot.GetMonsters().Any(card => card.IsCode(this.Card.Id) && card.IsFaceup());
        }

        /// <summary>
        /// Dumb way to avoid the bot chain in mess.
        /// </summary>
        protected bool DefaultDontChainMyself()
        {
            if (ExecType != ExecutorType.Activate)
            {
                return true;
            }
            if (this.Executors.Any(exec => exec.Type == this.ExecType && exec.CardId == this.Card.Id))
            {
                return false;
            }
            return this.Duel.LastChainPlayer != 0;
        }

        /// <summary>
        /// Draw when we have lower LP, or destroy it. Can be overrided.
        /// </summary>
        protected bool DefaultChickenGame()
        {
            if (this.Executors.Count(exec => exec.Type == this.ExecType && exec.CardId == this.Card.Id) > 1)
            {
                return false;
            }
            if (Card.IsFacedown())
            {
                return true;
            }
            if (this.Bot.LifePoints <= 1000)
            {
                return false;
            }

            if (this.Bot.LifePoints <= this.Enemy.LifePoints && this.ActivateDescription == this.Util.GetStringId(CardId.ChickenGame, 0))
            {
                return true;
            }

            if (this.Bot.LifePoints > this.Enemy.LifePoints && this.ActivateDescription == this.Util.GetStringId(CardId.ChickenGame, 1))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Draw when we have Dark monster in hand,and banish random one. Can be overrided.
        /// </summary>
        protected bool DefaultAllureofDarkness()
        {
            ClientCard target = this.Bot.Hand.FirstOrDefault(card => card.HasAttribute(CardAttribute.Dark));
            return target != null;
        }

        /// <summary>
        /// Clever enough.
        /// </summary>
        protected bool DefaultDimensionalBarrier()
        {
            const int RITUAL = 0;
            const int FUSION = 1;
            const int SYNCHRO = 2;
            const int XYZ = 3;
            const int PENDULUM = 4;
            if (this.Duel.Player != 0)
            {
                List<ClientCard> monsters = this.Enemy.GetMonsters();
                int[] levels = new int[13];
                bool tuner = false;
                bool nontuner = false;
                foreach (ClientCard monster in monsters)
                {
                    if (monster.HasType(CardType.Tuner))
                    {
                        tuner = true;
                    }
                    else if (!monster.HasType(CardType.Xyz) && !monster.HasType(CardType.Link))
                    {
                        nontuner = true;
                        levels[monster.Level] = levels[monster.Level] + 1;
                    }

                    if (monster.IsOneForXyz())
                    {
                        this.AI.SelectOption(XYZ);
                        return true;
                    }
                }
                if (tuner && nontuner)
                {
                    this.AI.SelectOption(SYNCHRO);
                    return true;
                }
                for (int i=1; i<=12; i++)
                {
                    if (levels[i]>1)
                    {
                        this.AI.SelectOption(XYZ);
                        return true;
                    }
                }
                ClientCard l = this.Enemy.SpellZone[6];
                ClientCard r = this.Enemy.SpellZone[7];
                if (l != null && r != null && l.LScale != r.RScale)
                {
                    this.AI.SelectOption(PENDULUM);
                    return true;
                }
            }
            ClientCard lastchaincard = this.Util.GetLastChainCard();
            if (this.Duel.LastChainPlayer == 1 && lastchaincard != null && !lastchaincard.IsDisabled())
            {
                if (lastchaincard.HasType(CardType.Ritual))
                {
                    this.AI.SelectOption(RITUAL);
                    return true;
                }
                if (lastchaincard.HasType(CardType.Fusion))
                {
                    this.AI.SelectOption(FUSION);
                    return true;
                }
                if (lastchaincard.HasType(CardType.Synchro))
                {
                    this.AI.SelectOption(SYNCHRO);
                    return true;
                }
                if (lastchaincard.HasType(CardType.Xyz))
                {
                    this.AI.SelectOption(XYZ);
                    return true;
                }
                if (lastchaincard.IsFusionSpell())
                {
                    this.AI.SelectOption(FUSION);
                    return true;
                }
            }
            if (this.Util.IsChainTarget(this.Card))
            {
                this.AI.SelectOption(XYZ);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Clever enough
        /// </summary>
        protected bool DefaultInterruptedKaijuSlumber()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                this.AI.SelectCard(
                    CardId.GamecieltheSeaTurtleKaiju,
                    CardId.KumongoustheStickyStringKaiju,
                    CardId.GadarlatheMysteryDustKaiju,
                    CardId.RadiantheMultidimensionalKaiju,
                    CardId.DogorantheMadFlameKaiju,
                    CardId.ThunderKingtheLightningstrikeKaiju,
                    CardId.JizukirutheStarDestroyingKaiju
                    );
                return true;
            }

            if (this.DefaultDarkHole())
            {
                this.AI.SelectCard(
                    CardId.JizukirutheStarDestroyingKaiju,
                    CardId.ThunderKingtheLightningstrikeKaiju,
                    CardId.DogorantheMadFlameKaiju,
                    CardId.RadiantheMultidimensionalKaiju,
                    CardId.GadarlatheMysteryDustKaiju,
                    CardId.KumongoustheStickyStringKaiju,
                    CardId.GamecieltheSeaTurtleKaiju
                    );
                this.AI.SelectNextCard(
                    CardId.SuperAntiKaijuWarMachineMechaDogoran,
                    CardId.GamecieltheSeaTurtleKaiju,
                    CardId.KumongoustheStickyStringKaiju,
                    CardId.GadarlatheMysteryDustKaiju,
                    CardId.RadiantheMultidimensionalKaiju,
                    CardId.DogorantheMadFlameKaiju,
                    CardId.ThunderKingtheLightningstrikeKaiju
                    );
                return true;
            }

            return false;
        }

        /// <summary>
        /// Clever enough.
        /// </summary>
        protected bool DefaultKaijuSpsummon()
        {
            IList<int> kaijus = new[] {
                CardId.JizukirutheStarDestroyingKaiju,
                CardId.GadarlatheMysteryDustKaiju,
                CardId.GamecieltheSeaTurtleKaiju,
                CardId.RadiantheMultidimensionalKaiju,
                CardId.KumongoustheStickyStringKaiju,
                CardId.ThunderKingtheLightningstrikeKaiju,
                CardId.DogorantheMadFlameKaiju,
                CardId.SuperAntiKaijuWarMachineMechaDogoran
            };
            foreach (ClientCard monster in this.Enemy.GetMonsters())
            {
                if (monster.IsCode(kaijus))
                {
                    return this.Card.GetDefensePower() > monster.GetDefensePower();
                }
            }
            ClientCard card = this.Enemy.MonsterZone.GetFloodgate();
            if (card != null)
            {
                this.AI.SelectCard(card);
                return true;
            }
            card = this.Enemy.MonsterZone.GetDangerousMonster();
            if (card != null)
            {
                this.AI.SelectCard(card);
                return true;
            }
            card = this.Util.GetOneEnemyBetterThanValue(this.Card.GetDefensePower());
            if (card != null)
            {
                this.AI.SelectCard(card);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Summon when we don't have monster attack higher than enemy's.
        /// </summary>
        protected bool DefaultNumberS39UtopiaTheLightningSummon()
        {
            int bestBotAttack = this.Util.GetBestAttack(this.Bot);
            return this.Util.IsOneEnemyBetterThanValue(bestBotAttack, false);
        }

        /// <summary>
        /// Activate if the card is attack pos, and its attack is below 5000, when the enemy monster is attack pos or not useless faceup defense pos
        /// </summary>
        protected bool DefaultNumberS39UtopiaTheLightningEffect()
        {
            return this.Card.IsAttack() && this.Card.Attack < 5000 && (this.Enemy.BattlingMonster.IsAttack() || this.Enemy.BattlingMonster.IsFacedown() || this.Enemy.BattlingMonster.GetDefensePower() >= this.Card.Attack);
        }

        /// <summary>
        /// Summon when it can and should use effect.
        /// </summary>
        protected bool DefaultEvilswarmExcitonKnightSummon()
        {
            int selfCount = this.Bot.GetMonsterCount() + this.Bot.GetSpellCount() + this.Bot.GetHandCount();
            int oppoCount = this.Enemy.GetMonsterCount() + this.Enemy.GetSpellCount() + this.Enemy.GetHandCount();
            return (selfCount - 1 < oppoCount) && this.DefaultEvilswarmExcitonKnightEffect();
        }

        /// <summary>
        /// Activate when we have less cards than enemy's, or the atk sum of we is lower than enemy's.
        /// </summary>
        protected bool DefaultEvilswarmExcitonKnightEffect()
        {
            int selfCount = this.Bot.GetMonsterCount() + this.Bot.GetSpellCount();
            int oppoCount = this.Enemy.GetMonsterCount() + this.Enemy.GetSpellCount();

            if (selfCount < oppoCount)
            {
                return true;
            }

            int selfAttack = this.Bot.GetMonsters().Sum(monster => (int?)monster.GetDefensePower()) ?? 0;
            int oppoAttack = this.Enemy.GetMonsters().Sum(monster => (int?)monster.GetDefensePower()) ?? 0;

            return selfAttack < oppoAttack;
        }

        /// <summary>
        /// Summon in main2, or when the attack of we is lower than enemy's, but not when enemy have monster higher than 2500.
        /// </summary>
        protected bool DefaultStardustDragonSummon()
        {
            int selfBestAttack = this.Util.GetBestAttack(this.Bot);
            int oppoBestAttack = this.Util.GetBestPower(this.Enemy);
            return (selfBestAttack <= oppoBestAttack && oppoBestAttack <= 2500) || this.Util.IsTurn1OrMain2();
        }

        /// <summary>
        /// Negate enemy's destroy effect, and revive from grave.
        /// </summary>
        protected bool DefaultStardustDragonEffect()
        {
            return (this.Card.Location == CardLocation.Grave) || this.Duel.LastChainPlayer == 1;
        }

        /// <summary>
        /// Summon when enemy have card which we must solve.
        /// </summary>
        protected bool DefaultCastelTheSkyblasterMusketeerSummon()
        {
            return this.Util.GetProblematicEnemyCard() != null;
        }

        /// <summary>
        /// Bounce the problematic enemy card. Ignore the 1st effect.
        /// </summary>
        protected bool DefaultCastelTheSkyblasterMusketeerEffect()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.CastelTheSkyblasterMusketeer, 0))
            {
                return false;
            }

            ClientCard target = this.Util.GetProblematicEnemyCard();
            if (target != null)
            {
                this.AI.SelectCard(0);
                this.AI.SelectNextCard(target);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Summon when it should use effect, or when the attack of we is lower than enemy's, but not when enemy have monster higher than 3000.
        /// </summary>
        protected bool DefaultScarlightRedDragonArchfiendSummon()
        {
            int selfBestAttack = this.Util.GetBestAttack(this.Bot);
            int oppoBestAttack = this.Util.GetBestPower(this.Enemy);
            return (selfBestAttack <= oppoBestAttack && oppoBestAttack <= 3000) || this.DefaultScarlightRedDragonArchfiendEffect();
        }

        /// <summary>
        /// Activate when we have less monsters than enemy, or when enemy have more than 3 monsters.
        /// </summary>
        protected bool DefaultScarlightRedDragonArchfiendEffect()
        {
            int selfCount = this.Bot.GetMonsters().Count(monster => !monster.Equals(this.Card) && monster.IsSpecialSummoned && monster.HasType(CardType.Effect) && monster.Attack <= this.Card.Attack);
            int oppoCount = this.Enemy.GetMonsters().Count(monster => monster.IsSpecialSummoned && monster.HasType(CardType.Effect) && monster.Attack <= this.Card.Attack);
            return selfCount <= oppoCount && oppoCount > 0 || oppoCount >= 3;
        }

        /// <summary>
        /// Clever enough.
        /// </summary>
        protected bool DefaultHonestEffect()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                return this.Bot.BattlingMonster.IsAttack() &&
                    (((this.Bot.BattlingMonster.Attack < this.Enemy.BattlingMonster.Attack) || this.Bot.BattlingMonster.Attack >= this.Enemy.LifePoints)
                    || ((this.Bot.BattlingMonster.Attack < this.Enemy.BattlingMonster.Defense) && (this.Bot.BattlingMonster.Attack + this.Enemy.BattlingMonster.Attack > this.Enemy.BattlingMonster.Defense)));
            }

            if (this.Util.IsTurn1OrMain2() && this.honestEffectCount <= 5)
            {
                //this.honestEffectCount++;
                return true;
            }

            return false;
        }

        protected bool DefaultLightingStorm()
        {
            if ((this.Enemy.MonsterZone.ToList().Count > this.Enemy.SpellZone.ToList().Count ) && this.Enemy.MonsterZone.ToList().Count>3)
            {
                this.AI.SelectPlace(Zones.MonsterZones);
                return true;
            }
            else
            {
                this.AI.SelectPlace(Zones.SpellZones);
                return true;
            }

        }
    }
}
