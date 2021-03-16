using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using System.Linq;


namespace WindBot.Game.AI.Decks
{
    [Deck("Witchcraft", "AI_Witchcraft")]

    class WitchcraftExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int PSYDriver = 49036338;
            public const int GolemAruru = 71074418;
            public const int MadameVerre = 21522601;
            public const int Haine = 84523092;
            public const int Schmietta = 21744288;
            public const int Pittore = 95245544;
            public const int AshBlossom_JoyousSpring = 14558127;
            public const int PSYGamma = 38814750;
            public const int MaxxC = 23434538;
            public const int Potterie = 59851535;
            public const int Genni = 64756282;
            public const int Collaboration = 10805153;
            public const int ThatGrassLooksGreener = 11110587;
            public const int LightningStorm = 14532163;
            public const int PotofExtravagance = 49238328;
            public const int DarkRulerNoMore = 54693926;
            public const int Creation = 57916305;
            public const int Reasoning = 58577036;
            public const int MetalfoesFusion = 73594093;
            public const int Holiday = 83301414;
            public const int CalledbytheGrave = 24224830;
            public const int Draping = 56894757;
            public const int CrossoutDesignator = 65681983;
            public const int Unveiling = 70226289;
            public const int MagiciansLeftHand = 13758665;
            public const int Scroll = 19673561;
            public const int MagiciansRestage = 40252269;
            public const int WitchcrafterBystreet = 83289866;
            public const int MagicianRightHand = 87769556;
            public const int InfiniteImpermanence = 10045474;
            public const int Masterpiece = 55072170;
            public const int Patronus = 94553671;
            public const int BorreloadSavageDragon = 27548199;
            public const int DracoBerserkeroftheTenyi = 5041348;
            public const int PSYOmega = 74586817;
            public const int TGWonderMagician = 98558751;
            public const int BorrelswordDragon = 85289965;
            public const int KnightmareUnicorn = 38342335;
            public const int KnightmarePhoenix = 2857636;
            public const int PSYLambda = 8802510;
            public const int CrystronHalqifibrax = 50588353;
            public const int SalamangreatAlmiraj = 60303245;
            public const int RelinquishedAnima = 94259633;

            public const int NaturalExterio = 99916754;
            public const int NaturalBeast = 33198837;
            public const int ImperialOrder = 61740673;
            public const int SwordsmanLV7 = 37267041;
            public const int RoyalDecreel = 51452091;
            public const int Anti_Spell = 58921041;
            public const int Numbe41BagooskatheTerriblyTiredTapir = 90590303;
            public const int PerformapalFive_RainbowMagician = 19619755;

            public const int DimensionShifter = 91800273;
            public const int MacroCosmos = 30241314;
            public const int DimensionalFissure = 81674782;
            public const int BanisheroftheRadiance = 94853057;
            public const int BanisheroftheLight = 61528025;
        }

        public WitchcraftExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // do first
            this.AddExecutor(ExecutorType.Activate, CardId.PotofExtravagance, this.PotofExtravaganceActivate);
            this.AddExecutor(ExecutorType.SpellSet, this.SpellSetForFiveRainbow);

            // clear
            this.AddExecutor(ExecutorType.Activate, CardId.DarkRulerNoMore, this.DarkRulerNoMoreActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.LightningStorm, this.LightningStormActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.RelinquishedAnima);

            // counter & quick effect
            this.AddExecutor(ExecutorType.Activate, CardId.Schmietta, this.DeckSSWitchcraft);
            this.AddExecutor(ExecutorType.Activate, CardId.Pittore, this.DeckSSWitchcraft);
            this.AddExecutor(ExecutorType.Activate, CardId.Potterie, this.DeckSSWitchcraft);
            this.AddExecutor(ExecutorType.Activate, CardId.Genni, this.DeckSSWitchcraft);
            this.AddExecutor(ExecutorType.Activate, CardId.PSYGamma, this.PSYGammaActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.MaxxC, this.MaxxCActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.GolemAruru, this.GolemAruruActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.BorreloadSavageDragon, this.BorreloadSavageDragonActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.InfiniteImpermanence, this.InfiniteImpermanenceActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.AshBlossom_JoyousSpring, this.AshBlossom_JoyousSpringActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.CalledbytheGrave, this.CalledbytheGraveActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.CrossoutDesignator, this.CrossoutDesignatorActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.MagicianRightHand, this.SpellsActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.MagiciansLeftHand, this.SpellsActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.Unveiling, this.UnveilingActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.Draping, this.DrapingActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.PSYOmega, this.PSYOmegaActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.DracoBerserkeroftheTenyi, this.DracoBerserkeroftheTenyiActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.MadameVerre, this.MadameVerreActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.Haine, this.HaineActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.SalamangreatAlmiraj, this.SalamangreatAlmirajActivate);

            // PSY auto
            this.AddExecutor(ExecutorType.Activate, CardId.PSYLambda);
            this.AddExecutor(ExecutorType.SpSummon, CardId.PSYLambda, this.PSYLambdaSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.PSYOmega, this.Lv8Summon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BorreloadSavageDragon, this.BorreloadSavageDragonSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.DracoBerserkeroftheTenyi, this.Lv8Summon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BorreloadSavageDragon, this.Lv8Summon);

            // auto
            this.AddExecutor(ExecutorType.Activate, CardId.WitchcrafterBystreet, this.WitchcraftRecycle);
            this.AddExecutor(ExecutorType.Activate, this.WitchcraftRecycle);
            this.AddExecutor(ExecutorType.Activate, CardId.MetalfoesFusion);
            this.AddExecutor(ExecutorType.Activate, CardId.TGWonderMagician, this.TGWonderMagicianActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.KnightmareUnicorn, this.KnightmareUnicornActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.KnightmarePhoenix, this.KnightmarePhoenixActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.CrystronHalqifibrax, this.CrystronHalqifibraxActivate);

            // activate with counter
            this.AddExecutor(ExecutorType.Activate, CardId.ThatGrassLooksGreener, this.SpellsActivatewithCounter);
            this.AddExecutor(ExecutorType.Activate, CardId.Reasoning, this.SpellsActivatewithCounter);

            // witchcraft summon
            this.AddExecutor(ExecutorType.Activate, CardId.Masterpiece, this.MasterpieceActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.Patronus, this.PatronusActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.MagiciansRestage, this.MagiciansRestageActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.Holiday, this.HolidayActivate);

            // summon
            this.AddExecutor(ExecutorType.Summon, CardId.Schmietta, this.WitchcraftSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.Pittore, this.WitchcraftSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.Potterie, this.WitchcraftSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.Genni, this.WitchcraftSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.Creation, this.CreationActivate);

            // witchcraft resources
            this.AddExecutor(ExecutorType.Activate, CardId.Pittore, this.PittoreActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.Schmietta, this.SchmiettaActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.Genni, this.GenniActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.Potterie, this.PotterieActivate);

            // extra calling
            this.AddExecutor(ExecutorType.SpSummon, CardId.KnightmarePhoenix, this.KnightmarePhoenixSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.RelinquishedAnima, this.RelinquishedAnimaSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.CrystronHalqifibrax, this.CrystronHalqifibraxSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BorrelswordDragon, this.BorrelswordDragonSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.KnightmareUnicorn, this.KnightmareUnicornSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.SalamangreatAlmiraj, this.SalamangreatAlmirajSummon);
            this.AddExecutor(ExecutorType.Summon, this.SummonForLink);

            // activate spells normally
            this.AddExecutor(ExecutorType.Activate, CardId.ThatGrassLooksGreener, this.SpellsActivateNoCost);
            this.AddExecutor(ExecutorType.Activate, CardId.Reasoning, this.SpellsActivateNoCost);
            this.AddExecutor(ExecutorType.Activate, CardId.MagicianRightHand, this.SpellsActivateNoCost);
            this.AddExecutor(ExecutorType.Activate, CardId.MagiciansLeftHand, this.SpellsActivateNoCost);

            //AddExecutor(ExecutorType.SummonOrSet);

            // rest
            this.AddExecutor(ExecutorType.Summon, this.WitchcraftSummonForRecycle);
            this.AddExecutor(ExecutorType.Repos, this.MonsterRepos);
            this.AddExecutor(ExecutorType.Activate, CardId.WitchcrafterBystreet, this.WitchcrafterBystreetActivate);
            this.AddExecutor(ExecutorType.Activate, CardId.Scroll, this.ScrollActivate);
            this.AddExecutor(ExecutorType.SpellSet, this.SpellSet);
        }

        readonly int Witchcraft_setcode = 0x128;
        readonly int TimeLord_setcode = 0x4a;
        readonly int[] important_witchcraft = { CardId.Haine, CardId.MadameVerre };
        readonly Dictionary<int, int> witchcraft_level = new Dictionary<int, int> {
            {CardId.GolemAruru, 8}, {CardId.MadameVerre, 7}, {CardId.Haine, 7}, {CardId.Schmietta, 4},
            {CardId.Pittore, 3}, {CardId.Potterie, 2}, {CardId.Genni, 1}
        };
        readonly List<int> Impermanence_list = new List<int>();
        readonly List<int> FirstCheckSS = new List<int>();
        readonly List<int> UseSSEffect = new List<int>();
        readonly List<int> ActivatedCards = new List<int>();
        readonly Dictionary<int, int> CalledbytheGraveCount = new Dictionary<int, int>();
        int CrossoutDesignatorTarget = 0;
        bool MadameVerreGainedATK = false;
        bool summoned = false;
        bool enemy_activate_MaxxC = false;
        bool enemy_activate_DimensionShifter = false;
        bool MagiciansLeftHand_used = false;
        bool MagicianRightHand_used = false;
        ClientCard MagiciansLeftHand_negate = null;
        ClientCard MagicianRightHand_negate = null;
        int PSYOmega_count = 0;

        // go first
        public override bool OnSelectHand()
        {
            return true;
        }

        // reset the negated card in case of activated again
        public override void OnChainEnd()
        {
            if (this.MagiciansLeftHand_negate != null)
            {
                this.MagiciansLeftHand_used = true;
                this.MagiciansLeftHand_negate = null;
            }
            if (this.MagicianRightHand_negate != null)
            {
                this.MagicianRightHand_used = true;
                this.MagicianRightHand_negate = null;
            }
            base.OnChainEnd();
        }

        // check whether enemy activate important card
        public override void OnChaining(int player, ClientCard card)
        {
            if (card == null)
            {
                return;
            }
            // MagiciansLeftHand / MagicianRightHand
            if (!this.MagicianRightHand_used && card.IsSpell() && card.Controller == 1)
            {
                if (this.Bot.MonsterZone.GetFirstMatchingCard(c => c.HasRace(CardRace.SpellCaster)) != null
                    && this.Bot.HasInSpellZone(CardId.MagicianRightHand, true))
                {
                    Logger.DebugWriteLine("MagicianRightHand negate: " + card.Name ?? "???");
                    this.MagicianRightHand_negate = card;
                }
            }
            if (!this.MagiciansLeftHand_used && card.IsTrap() && card.Controller == 1)
            {
                if (this.Bot.MonsterZone.GetFirstMatchingCard(c => c.HasRace(CardRace.SpellCaster)) != null
                    && this.Bot.HasInSpellZone(CardId.MagiciansLeftHand, true))
                {
                    Logger.DebugWriteLine("MagiciansLeftHand negate: " + card.Name ?? "???");
                    this.MagiciansLeftHand_negate = card;
                }
            }

            if (player == 1 && card.Id == CardId.MaxxC && this.CheckCalledbytheGrave(CardId.MaxxC) == 0)
            {
                this.enemy_activate_MaxxC = true;
            }
            if (player == 1 && card.Id == CardId.DimensionShifter && this.CheckCalledbytheGrave(CardId.DimensionShifter) == 0)
            {
                this.enemy_activate_DimensionShifter = true;
            }
            if (player == 1 && card.Id == CardId.InfiniteImpermanence && this.CrossoutDesignatorTarget != CardId.InfiniteImpermanence)
            {
                for (int i = 0; i < 5; ++i)
                {
                    if (this.Enemy.SpellZone[i] == card)
                    {
                        this.Impermanence_list.Add(4-i);
                        break;
                    }
                }
            }
            base.OnChaining(player, card);
        }

        // new turn reset
        public override void OnNewTurn()
        {
            this.CrossoutDesignatorTarget = 0;
            this.PSYOmega_count = 0;
            this.MadameVerreGainedATK = false;
            this.summoned = false;
            this.enemy_activate_MaxxC = false;
            this.enemy_activate_DimensionShifter = false;
            this.MagiciansLeftHand_used = false;
            this.MagicianRightHand_used = false;
            this.MagiciansLeftHand_negate = null;
            this.MagicianRightHand_negate = null;
            this.Impermanence_list.Clear();
            this.FirstCheckSS.Clear();
            this.UseSSEffect.Clear();
            this.ActivatedCards.Clear();
            // CalledbytheGrave refresh
            List<int> key_list = this.CalledbytheGraveCount.Keys.ToList();
            foreach (int dic in key_list)
            {
                if (this.CalledbytheGraveCount[dic] > 1)
                {
                    this.CalledbytheGraveCount[dic] -= 1;
                }
            }
        }

        // power fix
        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (!defender.IsMonsterHasPreventActivationEffectInBattle())
            {
                if (!this.MadameVerreGainedATK && this.Bot.HasInMonstersZone(CardId.MadameVerre, true, false, true) && attacker.HasSetcode(this.Witchcraft_setcode)) 
                {
                    attacker.RealPower += this.CheckPlusAttackforMadameVerre();
                }
            }
            return base.OnPreBattleBetween(attacker, defender);
        }

        // overwrite OnSelectCard to act normally in SelectUnselect
        public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
        {
            // Patronus HINTMSG_ATOHAND
            if (hint == 506)
            {
                bool flag = true;
                foreach(ClientCard card in cards)
                {
                    if (!card.HasSetcode(this.Witchcraft_setcode) || card.Location != CardLocation.Removed || !card.IsSpell())
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    Logger.DebugWriteLine("** Patronus recycle.");
                    // select all
                    IList<ClientCard> selected = new List<ClientCard>();
                    for (int i = 1; i <= max; ++i)
                    {
                        selected.Add(cards[cards.Count - i]);
                        Logger.DebugWriteLine("** Select " + cards[cards.Count - i].Name ?? "???");
                    }
                    return selected;
                }
            }
            // MaxxC HINTMSG_SPSUMMON
            if (hint == 509 && this.enemy_activate_MaxxC)
            {
                // check whether SS from deck while using effect
                bool flag = true;
                List<int> levels = new List<int>();
                List<int> check_cardid = new List<int> { CardId.Haine, CardId.MadameVerre, CardId.GolemAruru };
                List<ClientCard> checked_card = new List<ClientCard> { null, null, null };
                foreach (ClientCard card in cards)
                {
                    if (card != null && card.Location == CardLocation.Deck && card.Controller == 0 && card.HasSetcode(this.Witchcraft_setcode))
                    {
                        for (int i = 0; i < 3; ++i)
                        {
                            if (card.Id == check_cardid[i])
                            {
                                checked_card[i] = card;
                            }
                        }
                        // Patronus also special summon from deck
                        if (!levels.Contains(card.Level))
                        {
                            levels.Add(card.Level);
                        }
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }
                
                // only special summon advance monster
                if (flag && levels.Count > 1)
                {
                    Logger.DebugWriteLine("SS with MaxxC.");
                    IList<ClientCard> result = new List<ClientCard>();
                    // check MadameVerre
                    int extra_attack = this.CheckPlusAttackforMadameVerre(true, true, true);
                    int bot_best = this.Util.GetBestAttack(this.Bot);
                    if (this.CheckProblematicCards() != null && this.Util.IsAllEnemyBetterThanValue(bot_best + extra_attack, true) == false)
                    {
                        if (!this.Bot.HasInMonstersZone(CardId.MadameVerre) && checked_card[1] != null)
                        {
                            result.Add(checked_card[1]);
                            return result;
                        } 
                    }
                    for (int i = 0; i < 3; ++i)
                    {
                        if (checked_card[i] != null)
                        {
                            result.Add(checked_card[i]);
                            return result;
                        }
                    }
                }
            }
            // MadameVerre HINTMSG_CONFIRM
            if (hint == 526)
            {
                Logger.DebugWriteLine("** min-max: " + min.ToString() + " / " + max.ToString());
                foreach (ClientCard card in cards)
                {
                    Logger.DebugWriteLine(card.Name ?? "???");
                }

                // select all
                IList<ClientCard> selected = new List<ClientCard>();
                for (int i = 1; i <= max; ++i)
                {
                    selected.Add(cards[cards.Count - i]);
                    Logger.DebugWriteLine("** Select " + cards[cards.Count - i].Name ?? "???");
                }
                return selected;
            }

            return base.OnSelectCard(cards, min, max, hint, cancelable);
        }

        // position select
        public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
        {
            NamedCard Data = NamedCard.Get(cardId);
            if (Data == null)
            {
                return base.OnSelectPosition(cardId, positions);
            }
            if (!this.Enemy.HasInMonstersZone(DefaultExecutor.CardId.BlueEyesChaosMAXDragon) 
                && (this.Duel.Player == 1 && (cardId == CardId.MadameVerre ||
                this.Util.GetOneEnemyBetterThanValue(Data.Attack + 1) != null))
                || cardId == CardId.MaxxC || cardId == CardId.AshBlossom_JoyousSpring)
            {
                return CardPosition.FaceUpDefence;
            }
            if (cardId == CardId.MadameVerre && this.Util.IsTurn1OrMain2())
            {
                return CardPosition.FaceUpDefence;
            }
            return base.OnSelectPosition(cardId, positions);
        }

        // shuffle List<ClientCard>
        public List<ClientCard> CardListShuffle(List<ClientCard> list)
        {
            List<ClientCard> result = list;
            int n = result.Count;
            while (n-- > 1)
            {
                int index = Program._rand.Next(n + 1);
                ClientCard temp = result[index];
                result[index] = result[n];
                result[n] = temp;
            }
            return result;
        }

        // check negated time count of id
        public int CheckCalledbytheGrave(int id)
        {
            if (!this.CalledbytheGraveCount.ContainsKey(id))
            {
                return 0;
            }
            return this.CalledbytheGraveCount[id];
        }

        // check enemy's dangerous card in grave
        public List<ClientCard> CheckDangerousCardinEnemyGrave(bool onlyMonster = false)
        {
            List<ClientCard> result = this.Enemy.Graveyard.GetMatchingCards(card => 
            (!onlyMonster || card.IsMonster()) && card.HasSetcode(0x11b)).ToList();
            return result;
        }

        // check whether negate maxxc and InfiniteImpermanence
        public void CheckDeactiveFlag()
        {
            if (this.Util.GetLastChainCard() != null && this.Util.GetLastChainCard().Id == CardId.MaxxC && this.Duel.LastChainPlayer == 1)
            {
                this.enemy_activate_MaxxC = false;
            }
            if (this.Util.GetLastChainCard() != null && this.Util.GetLastChainCard().Id == CardId.DimensionShifter && this.Duel.LastChainPlayer == 1)
            {
                this.enemy_activate_DimensionShifter = false;
            }
        }

        /// <summary>
        /// Check count of discardable spells for witchcraft monsters.
        /// </summary>
        /// <param name="except">Card that prepared to use and can't discard.</param>
        public int CheckDiscardableSpellCount(ClientCard except = null)
        {
            int discardable_hands = 0;
            int count_witchcraftspell = this.Bot.Hand.GetMatchingCardsCount(card => (card.IsSpell() && (card.HasSetcode(this.Witchcraft_setcode)) && card != except));
            int count_remainhands = this.CheckRemainInDeck(CardId.MagiciansLeftHand, CardId.MagicianRightHand);
            int count_MagiciansRestage = this.Bot.Hand.GetMatchingCardsCount(card => card.Id == CardId.MagiciansRestage && card != except);
            int count_MetalfoesFusion = this.Bot.Hand.GetCardCount(CardId.MetalfoesFusion);
            int count_WitchcrafterBystreet = this.Bot.SpellZone.GetMatchingCardsCount(card => card.IsFaceup() && card.Id == CardId.WitchcrafterBystreet && !card.IsDisabled());
            if (count_MagiciansRestage > 0)
            {
                discardable_hands += (count_MagiciansRestage > count_remainhands ? count_remainhands : count_MagiciansRestage);
            }
            if (!this.ActivatedCards.Contains(CardId.WitchcrafterBystreet) && (count_WitchcrafterBystreet >= 2 || (count_WitchcrafterBystreet >= 1 && this.Duel.Phase > DuelPhase.Battle)))
            {
                discardable_hands += 1;
            }
            discardable_hands += count_witchcraftspell + count_MetalfoesFusion;
            return discardable_hands;
        }

        /// <summary>
        /// Check whether last chain card should be disabled.
        /// </summary>
        public bool CheckLastChainNegated()
        {
            ClientCard lastcard = this.Util.GetLastChainCard();
            if (lastcard == null || lastcard.Controller != 1)
            {
                return false;
            }

            if (lastcard.IsMonster() && lastcard.HasSetcode(this.TimeLord_setcode) && this.Duel.Phase == DuelPhase.Standby)
            {
                return false;
            }

            return lastcard == this.MagiciansLeftHand_negate || lastcard == this.MagicianRightHand_negate;
        }

        /// <summary>
        /// Check whether match link condition.
        /// </summary>
        /// <param name="LinkCount">Min Link count</param>
        /// <param name="MaterialCount">Min material count</param>
        /// <param name="list">materails list</param>
        /// <param name="need_tune">whether need tuner</param>
        /// <returns></returns>
        public bool CheckLinkMaterialsMatch(int LinkCount, int MaterialCount, List<ClientCard> list, bool need_tune = false)
        {
            // material count check
            if (list.Count < MaterialCount)
            {
                return false;
            }

            // link marker check
            int linkcount = 0;
            foreach(ClientCard card in list)
            {
                linkcount += (card.HasType(CardType.Link) ? card.LinkCount : 1);
            }
            if (linkcount != LinkCount)
            {
                foreach (ClientCard card in list)
                {
                    linkcount += 1;
                }
                if (linkcount != LinkCount)
                {
                    return false;
                }
            }

            // tuner check
            if (need_tune && list.GetFirstMatchingCard(card => card.IsTuner()) == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check link summon materials. If not enough, return an empty list.
        /// </summary>
        /// <param name="LinkCount">Link monster's link count.</param>
        /// <param name="MaterialCount">Link monster's least material count.</param>
        /// <param name="need_tuner">Whether materials need tuner(use for CrystronHalqifibrax)</param>
        /// <param name="extra">Extra monster use for material check.</param>
        public List<ClientCard> CheckLinkMaterials(int LinkCount, int MaterialCount, bool need_tuner = false, List<ClientCard> extra = null)
        {
            List<int> psy_cardids = new List<int> { CardId.PSYGamma, CardId.PSYDriver };
            List<ClientCard> result = this.Bot.MonsterZone.GetMatchingCards(card => card.IsFaceup() && psy_cardids.Contains(card.Id)).ToList();
            if (this.CheckLinkMaterialsMatch(LinkCount, MaterialCount, result, need_tuner))
            {
                return result;
            }

            List<ClientCard> bot_monsters = this.Enemy.MonsterZone.GetMatchingCards(c => c.IsFaceup()).ToList();
            if (extra != null)
            {
                bot_monsters = bot_monsters.Union(extra).ToList();
            }

            bot_monsters.Sort(CardContainer.CompareCardAttack);

            int remaindiscard = this.CheckDiscardableSpellCount();
            int enemybest = this.Util.GetBestAttack(this.Enemy);
            foreach (ClientCard card in bot_monsters)
            {
                if ((card.HasSetcode(this.Witchcraft_setcode) && (card.Level >= 5 || remaindiscard >= 2))
                    || (card.Attack >= enemybest)
                    || (card.HasType(CardType.Link) && card.LinkMarker > 2))
                {
                    continue;
                }
                result.Add(card);
                if (this.CheckLinkMaterialsMatch(LinkCount, MaterialCount, result, need_tuner))
                {
                    return result;
                }
            }
            if (!this.CheckLinkMaterialsMatch(LinkCount, MaterialCount, result, need_tuner))
            {
                result.Clear();
            }

            return result;
        }

        /// <summary>
        /// Check how many attack MadameVerre can provide
        /// </summary>
        /// <param name="ignore_activated">whether ignore the activate of MadameVerre</param>
        /// <param name="check_recycle">check prerecycle spells in grave</param>
        /// <param name="force">force check whether have MadameVerre</param>
        public int CheckPlusAttackforMadameVerre(bool ignore_activated = false, bool check_recycle = false, bool force = false)
        {
            // not MadameVerre on field
            if (!force && this.Bot.MonsterZone.GetFirstMatchingCard(card => card.Id == CardId.MadameVerre && !card.IsDisabled()) == null)
            {
                return 0;
            }

            if (!ignore_activated && this.MadameVerreGainedATK)
            {
                return 0;
            }

            HashSet<int> spells_id = new HashSet<int>();
            foreach(ClientCard card in this.Bot.Hand)
            {
                if (card.IsSpell())
                {
                    spells_id.Add(card.Id);
                }
            }
            if (check_recycle && this.Bot.MonsterZone.GetFirstMatchingCard(card => card.IsFaceup() && card.HasSetcode(this.Witchcraft_setcode)) != null)
            {
                List<int> spell_checklist = new List<int> { CardId.Holiday, CardId.Creation, CardId.Draping, CardId.Unveiling, CardId.Collaboration };
                foreach (int cardid in spell_checklist)
                {
                    if (this.Bot.HasInGraveyard(cardid) && !this.ActivatedCards.Contains(cardid))
                    {
                        spells_id.Add(this.Card.Id);
                    }
                }
            }
            int max_hand = spells_id.Count() >= 6 ? 6 : spells_id.Count();
            return max_hand * 1000;
            
        }

        /// <summary>
        /// Check problematic cards on enemy's field.
        /// </summary>
        /// <param name="canBeTarget">whether can be targeted</param>
        /// <param name="OnlyDanger">only check danger monsters</param>
        public ClientCard CheckProblematicCards(bool canBeTarget = false, bool OnlyDanger = false)
        {
            ClientCard card = this.Enemy.MonsterZone.GetFloodgate(canBeTarget);
            if (card != null)
            {
                return card;
            }

            card = this.Enemy.MonsterZone.GetDangerousMonster(canBeTarget);
            if (card != null
                && (this.Duel.Player == 0 || (this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2)))
            {
                return card;
            }

            card = this.Enemy.MonsterZone.GetInvincibleMonster(canBeTarget);
            if (card != null
                && (this.Duel.Player == 0 || (this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2)))
            {
                return card;
            }

            List<ClientCard> enemy_monsters = this.Enemy.MonsterZone.GetMatchingCards(c => c.IsFaceup()).ToList();
            enemy_monsters.Sort(CardContainer.CompareCardAttack);
            enemy_monsters.Reverse();
            foreach (ClientCard target in enemy_monsters)
            {
                if (target.HasType(CardType.Fusion) || target.HasType(CardType.Ritual) || target.HasType(CardType.Synchro) || target.HasType(CardType.Xyz) || (target.HasType(CardType.Link) && target.LinkCount >= 2))
                {
                    if (!canBeTarget || !(target.IsShouldNotBeTarget() || target.IsShouldNotBeMonsterTarget()))
                    {
                        return target;
                    }
                }
            }

            if (OnlyDanger)
            {
                return null;
            }

            int highest_self = this.Util.GetBestPower(this.Bot);
            if (!this.MadameVerreGainedATK && this.Bot.HasInMonstersZone(CardId.MadameVerre, true, false, true))
            {
                highest_self += this.CheckPlusAttackforMadameVerre();
            }
            return this.Util.GetProblematicEnemyCard(highest_self, canBeTarget);
        }

        /// <summary>
        /// Check how many spells can be recylced to hand.
        /// </summary>
        public int CheckRecyclableCount(bool tohand = false, bool ignore_monster = false)
        {
            if (!ignore_monster && this.Bot.MonsterZone.GetFirstMatchingCard(card => card.IsFaceup() && card.HasSetcode(this.Witchcraft_setcode)) == null)
            {
                return 0;
            }

            int result = 0;
            List<int> spell_checklist = new List<int> { CardId.Holiday, CardId.Creation, CardId.Draping, CardId.Unveiling, CardId.Collaboration };
            if (!tohand)
            {
                spell_checklist.Add(CardId.WitchcrafterBystreet);
                spell_checklist.Add(CardId.Scroll);
            }
            foreach (int cardid in spell_checklist)
            {
                if (this.Bot.HasInGraveyard(cardid) && !this.ActivatedCards.Contains(cardid))
                {
                    result++;
                }
            }
            return result;
        }

        /// <summary>
        /// Check remain cards in deck
        /// </summary>
        /// <param name="id">Card's ID</param>
        public int CheckRemainInDeck(int id)
        {
            switch (id)
            {
                case CardId.PSYDriver:
                    return this.Bot.GetRemainingCount(CardId.PSYDriver, 1);
                case CardId.GolemAruru:
                    return this.Bot.GetRemainingCount(CardId.GolemAruru, 1);
                case CardId.MadameVerre:
                    return this.Bot.GetRemainingCount(CardId.MadameVerre, 1);
                case CardId.Haine:
                    return this.Bot.GetRemainingCount(CardId.Haine, 2);
                case CardId.Schmietta:
                    return this.Bot.GetRemainingCount(CardId.Schmietta, 3);
                case CardId.Pittore:
                    return this.Bot.GetRemainingCount(CardId.Pittore, 3);
                case CardId.AshBlossom_JoyousSpring:
                    return this.Bot.GetRemainingCount(CardId.AshBlossom_JoyousSpring, 1);
                case CardId.PSYGamma:
                    return this.Bot.GetRemainingCount(CardId.PSYGamma, 3);
                case CardId.MaxxC:
                    return this.Bot.GetRemainingCount(CardId.MaxxC, 1);
                case CardId.Potterie:
                    return this.Bot.GetRemainingCount(CardId.Potterie, 1);
                case CardId.Genni:
                    return this.Bot.GetRemainingCount(CardId.Genni, 2);
                case CardId.Collaboration:
                    return this.Bot.GetRemainingCount(CardId.Collaboration, 1);
                case CardId.ThatGrassLooksGreener:
                    return this.Bot.GetRemainingCount(CardId.ThatGrassLooksGreener, 2);
                case CardId.LightningStorm:
                    return this.Bot.GetRemainingCount(CardId.LightningStorm, 2);
                case CardId.PotofExtravagance:
                    return this.Bot.GetRemainingCount(CardId.PotofExtravagance, 3);
                case CardId.DarkRulerNoMore:
                    return this.Bot.GetRemainingCount(CardId.DarkRulerNoMore, 2);
                case CardId.Creation:
                    return this.Bot.GetRemainingCount(CardId.Creation, 3);
                case CardId.Reasoning:
                    return this.Bot.GetRemainingCount(CardId.Reasoning, 3);
                case CardId.MetalfoesFusion:
                    return this.Bot.GetRemainingCount(CardId.MetalfoesFusion, 1);
                case CardId.Holiday:
                    return this.Bot.GetRemainingCount(CardId.Holiday, 3);
                case CardId.CalledbytheGrave:
                    return this.Bot.GetRemainingCount(CardId.CalledbytheGrave, 3);
                case CardId.Draping:
                    return this.Bot.GetRemainingCount(CardId.Draping, 1);
                case CardId.CrossoutDesignator:
                    return this.Bot.GetRemainingCount(CardId.CrossoutDesignator, 2);
                case CardId.Unveiling:
                    return this.Bot.GetRemainingCount(CardId.Unveiling, 1);
                case CardId.MagiciansLeftHand:
                    return this.Bot.GetRemainingCount(CardId.MagiciansLeftHand, 1);
                case CardId.Scroll:
                    return this.Bot.GetRemainingCount(CardId.Scroll, 1);
                case CardId.MagiciansRestage:
                    return this.Bot.GetRemainingCount(CardId.MagiciansRestage, 2);
                case CardId.WitchcrafterBystreet:
                    return this.Bot.GetRemainingCount(CardId.WitchcrafterBystreet, 3);
                case CardId.MagicianRightHand:
                    return this.Bot.GetRemainingCount(CardId.MagicianRightHand, 1);
                case CardId.InfiniteImpermanence:
                    return this.Bot.GetRemainingCount(CardId.InfiniteImpermanence, 3);
                case CardId.Masterpiece:
                    return this.Bot.GetRemainingCount(CardId.Masterpiece, 1);
                case CardId.Patronus:
                    return this.Bot.GetRemainingCount(CardId.Patronus, 2);
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Check remain cards in deck
        /// </summary>
        /// <param name="ids">Card's ID list</param>
        public int CheckRemainInDeck(params int[] ids)
        {
            int result = 0;
            foreach (int cardid in ids)
            {
                result += this.CheckRemainInDeck(cardid);
            }
            return result;
        }

        /// <summary>
        /// Check whether cards will be removed. If so, do not send cards to grave.
        /// </summary>
        public bool CheckWhetherWillbeRemoved()
        {
            if (this.enemy_activate_DimensionShifter)
            {
                return true;
            }

            List<int> check_card = new List<int> { CardId.BanisheroftheRadiance, CardId.BanisheroftheLight, CardId.MacroCosmos, CardId.DimensionalFissure };
            foreach(int cardid in check_card)
            {
                List<ClientField> fields = new List<ClientField> { this.Bot, this.Enemy };
                foreach (ClientField cf in fields)
                {
                    if (cf.HasInMonstersZone(cardid, true) || cf.HasInSpellZone(cardid, true))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Whether spell or trap will be negate. If so, return true.
        /// </summary>
        /// <param name="isCounter">is counter trap</param>
        /// <param name="target">check target</param>
        /// <returns></returns>
        public bool SpellNegatable(bool isCounter = false, ClientCard target = null)
        {
            // target default set
            if (target == null)
            {
                target = this.Card;
            }

            if (target.Id == this.CrossoutDesignatorTarget)
            {
                return true;
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

        /// <summary>
        /// Check whether'll be negated
        /// </summary>
        public bool NegatedCheck(bool disablecheck = true){
            if (this.Card.IsSpell() || this.Card.IsTrap()){
                if (this.SpellNegatable())
                {
                    return true;
                }
            }
            if (this.CheckCalledbytheGrave(this.Card.Id) > 0 || this.Card.Id == this.CrossoutDesignatorTarget){
                return true;
            }
            if (this.Card.IsMonster() && this.Card.Location == CardLocation.MonsterZone && this.Card.IsDefense())
            {
                if (this.Enemy.MonsterZone.GetFirstMatchingFaceupCard(card => card.Id == CardId.Numbe41BagooskatheTerriblyTiredTapir && card.IsDefense() && !card.IsDisabled()) != null
                    || this.Bot.MonsterZone.GetFirstMatchingFaceupCard(card => card.Id == CardId.Numbe41BagooskatheTerriblyTiredTapir && card.IsDefense() && !card.IsDisabled()) != null)
                {
                    return true;
                }
            }
            if (disablecheck){
                return this.Card.IsDisabled();
            }
            return false;
        }

        /// <summary>
        /// Select spell/trap's place randomly to avoid InfiniteImpermanence and so on.
        /// </summary>
        /// <param name="card">Card to set(default current card)</param>
        /// <param name="avoid_Impermanence">Whether need to avoid InfiniteImpermanence</param>
        /// <param name="avoid_list">Whether need to avoid set in this place</param>
        public void SelectSTPlace(ClientCard card = null, bool avoid_Impermanence = false, List<int> avoid_list=null)
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
                    if (card != null && card.Location == CardLocation.Hand && avoid_Impermanence && this.Impermanence_list.Contains(seq))
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

        // Spell&trap's set
        public bool SpellSet(){
            if (this.Duel.Phase == DuelPhase.Main1 && this.Bot.HasAttackingMonster() && this.Duel.Turn > 1)
            {
                return false;
            }

            if (this.Card.Id == CardId.CrossoutDesignator && this.Duel.Turn >= 5)
            {
                return false;
            }

            // set condition
            int[] activate_with_condition = { CardId.Masterpiece, CardId.Draping };
            if (activate_with_condition.Contains(this.Card.Id))
            {
                if (this.Bot.MonsterZone.GetFirstMatchingCard(card => card.HasSetcode(this.Witchcraft_setcode)) == null)
                {
                    return false;
                }
            }
            if (this.Card.Id == CardId.Unveiling)
            {
                return false;
            }
            if (this.Card.Id == CardId.Patronus)
            {
                int count = this.Bot.Banished.GetMatchingCardsCount(card => card.HasSetcode(this.Witchcraft_setcode));
                if (count == 0)
                {
                    count += this.Bot.Graveyard.GetMatchingCardsCount(card => card.HasSetcode(this.Witchcraft_setcode));
                }
                if (count == 0)
                {
                    return false;
                }
            }

            // prepare spells to discard
            if (this.Card.IsSpell()){
                int spells_todiscard = this.CheckRecyclableCount() + this.Bot.Hand.GetMatchingCardsCount(card => card.IsSpell());
                int will_discard = 0;
                if (this.Bot.HasInMonstersZone(CardId.Haine))
                {
                    will_discard ++;
                }

                if (this.Bot.HasInMonstersZone(CardId.MadameVerre))
                {
                    will_discard ++;
                }

                if (will_discard >= spells_todiscard){
                    return false;
                }
            }

            // select place
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
                if (this.Bot.HasInHand(CardId.InfiniteImpermanence))
                {
                    if (this.Card.IsCode(CardId.InfiniteImpermanence))
                    {
                        this.AI.SelectPlace(Impermanence_set);
                        return true;
                    } else
                    {
                        this.SelectSTPlace(this.Card, false, avoid_list);
                        return true;
                    }
                } else
                {
                    this.SelectSTPlace();
                }
                return true;
            }
            // anti-spell relevant
            else if (this.Enemy.HasInSpellZone(CardId.Anti_Spell, true) || this.Bot.HasInSpellZone(CardId.Anti_Spell, true))
            {
                if (this.Card.IsSpell() && this.Card.Id != CardId.MetalfoesFusion)
                {
                    this.SelectSTPlace();
                    return true;
                }
            }
            return false;
        }

        // Spell&trap's set for Performapal Five-Rainbow Magician
        public bool SpellSetForFiveRainbow()
        {
            // check
            bool have_FiveRainbow = false;
            List<ClientCard> list = new List<ClientCard>();
            if (this.Duel.IsNewRule || this.Duel.IsNewRule2020)
            {
                list.Add(this.Enemy.SpellZone[0]);
                list.Add(this.Enemy.SpellZone[4]);
            }
            else
            {
                list.Add(this.Enemy.SpellZone[6]);
                list.Add(this.Enemy.SpellZone[7]);
            }
            foreach(ClientCard card in list)
            {
                if (card != null && card.Id == CardId.PerformapalFive_RainbowMagician)
                {
                    have_FiveRainbow = true;
                    break;
                }
            }

            if (!have_FiveRainbow)
            {
                return false;
            }

            if (this.Bot.GetMonsterCount() == 0 || this.Bot.SpellZone.GetFirstMatchingCard(card => card.IsFacedown()) != null)
            {
                return false;
            }

            if (this.Card.IsSpell())
            {
                this.SelectSTPlace(null, true);
                return true;
            }

            return false;
        }

        // use for repos
        public bool MonsterRepos()
        {
            int self_attack = this.Card.Attack + 1;
            int extra_attack = this.CheckPlusAttackforMadameVerre(true, true);
            Logger.DebugWriteLine("self_attack of " + (this.Card.Name ?? "X") + ": " + self_attack.ToString());
            if (this.Card.HasSetcode(this.Witchcraft_setcode))
            {
                self_attack += extra_attack;
            }

            if (this.Card.IsFaceup() && this.Card.IsDefense() && self_attack <= 1)
            {
                return false;
            }

            int best_attack = 0;
            foreach (ClientCard card in this.Bot.GetMonsters())
            {
                int attack = card.Attack;
                if (card.HasSetcode(this.Witchcraft_setcode))
                {
                    attack += extra_attack;
                }
                if (attack >= best_attack)
                {
                    best_attack = attack;
                }
            }

            bool enemyBetter = this.Util.IsAllEnemyBetterThanValue(best_attack, true);

            if (this.Card.IsAttack() && enemyBetter)
            {
                return true;
            }

            if (this.Card.IsDefense() && !enemyBetter && self_attack >= this.Card.Defense)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Select spell cost for Witchcraft.
        /// </summary>
        public void SelectDiscardSpell()
        {
            int count_remainhands = this.CheckRemainInDeck(CardId.MagiciansLeftHand, CardId.MagicianRightHand);
            int count_witchcraftspell = this.Bot.Hand.GetMatchingCardsCount(card => (card.IsSpell() && (card.HasSetcode(this.Witchcraft_setcode))));
            int WitchcrafterBystreet_count = this.Bot.SpellZone.GetMatchingCardsCount(card => card.IsFaceup() && card.Id == CardId.WitchcrafterBystreet);
            if (this.Bot.HasInHand(CardId.MagiciansRestage) && count_remainhands > 0)
            {
                this.AI.SelectCard(CardId.MagiciansRestage);
            }
            else if (this.Bot.HasInHand(CardId.MetalfoesFusion))
            {
                this.AI.SelectCard(CardId.MetalfoesFusion);
            }
            else if (!this.ActivatedCards.Contains(CardId.Scroll) && this.Bot.SpellZone.GetCardCount(CardId.Scroll) > 0)
            {
                this.AI.SelectCard(this.Bot.SpellZone.GetFirstMatchingFaceupCard(card => card.Id == CardId.Scroll));
                this.ActivatedCards.Add(CardId.Scroll);
            }
            else if (!this.ActivatedCards.Contains(CardId.WitchcrafterBystreet) && WitchcrafterBystreet_count >= 2)
            {
                this.AI.SelectCard(this.Bot.SpellZone.GetFirstMatchingFaceupCard(card => card.Id == CardId.WitchcrafterBystreet));
                this.ActivatedCards.Add(CardId.WitchcrafterBystreet);
            }
            else if (count_witchcraftspell > 0)
            {
                List<int> cost_list = new List<int>{ CardId.Scroll, CardId.WitchcrafterBystreet, CardId.Collaboration, CardId.Unveiling, CardId.Draping };
                if (this.Duel.Player == 1)
                {
                    cost_list.Add(CardId.Creation);
                    cost_list.Add(CardId.Holiday);
                } else
                {
                    cost_list.Add(CardId.Holiday);
                    cost_list.Add(CardId.Creation);
                }
                foreach (int cardid in cost_list)
                {
                    IList<ClientCard> targets = this.Bot.Hand.GetMatchingCards(card => card.Id == cardid);
                    if (targets.Count() > 0)
                    {
                        this.AI.SelectCard(targets);
                        return;
                    }
                }
                this.AI.SelectCard(CardId.Scroll, CardId.WitchcrafterBystreet);
            }
            else if (this.Bot.HasInHand(CardId.PotofExtravagance) && this.Bot.ExtraDeck.Count < 6)
            {
                this.AI.SelectCard(CardId.PotofExtravagance);
            }
            else if (WitchcrafterBystreet_count >= 1)
            {
                this.AI.SelectCard(this.Bot.SpellZone.GetFirstMatchingFaceupCard(card => card.Id == CardId.WitchcrafterBystreet));
                this.ActivatedCards.Add(CardId.WitchcrafterBystreet);
            }
            else
            {
                this.AI.SelectCard(CardId.ThatGrassLooksGreener, CardId.LightningStorm, CardId.PotofExtravagance, CardId.MagiciansLeftHand, CardId.MagicianRightHand, CardId.CrossoutDesignator, CardId.CalledbytheGrave);
            }
        }

        /// <summary>
        /// For normal spells activate
        /// </summary>
        public bool SpellsActivate()
        {
            if (this.SpellNegatable())
            {
                return false;
            }

            if (this.CheckDiscardableSpellCount() <= 1)
            {
                return false;
            }

            if ((this.Card.Id == CardId.ThatGrassLooksGreener || this.Card.Id == CardId.Reasoning) && this.CheckWhetherWillbeRemoved())
            {
                return false;
            }

            if (this.Card.Id == CardId.MagiciansLeftHand || this.Card.Id == CardId.MagicianRightHand)
            {
                if (this.Bot.MonsterZone.GetFirstMatchingCard(card => card.HasRace(CardRace.SpellCaster)) == null
                    && (this.summoned || this.Bot.Hand.GetFirstMatchingCard(card => card.HasRace(CardRace.SpellCaster) && card.Level <= 4) == null))
                {
                    return false;
                }
            }
            this.SelectSTPlace(this.Card, true);
            return true;
        }

        /// <summary>
        /// For normal spells activate without cost
        /// </summary>
        public bool SpellsActivateNoCost()
        {
            if (this.SpellNegatable())
            {
                return false;
            }

            if ((this.Card.Id == CardId.ThatGrassLooksGreener || this.Card.Id == CardId.Reasoning) && this.CheckWhetherWillbeRemoved())
            {
                return false;
            }

            if (this.Card.Id == CardId.MagiciansLeftHand || this.Card.Id == CardId.MagicianRightHand)
            {
                if (this.Bot.MonsterZone.GetFirstMatchingCard(card => card.HasRace(CardRace.SpellCaster)) == null
                    && (this.summoned || this.Bot.Hand.GetFirstMatchingCard(card => card.HasRace(CardRace.SpellCaster) && card.Level <= 4) == null))
                {
                    return false;
                }
            }
            this.SelectSTPlace(this.Card, true);
            return true;
        }

        /// <summary>
        /// Check wheter have enough counter to care for important spells. if not, delay it.
        /// </summary>
        public bool SpellsActivatewithCounter()
        {
            if (this.SpellNegatable())
            {
                return false;
            }

            if ((this.Card.Id == CardId.ThatGrassLooksGreener || this.Card.Id == CardId.Reasoning) && this.CheckWhetherWillbeRemoved())
            {
                return false;
            }

            int[] counter_cards = { CardId.PSYGamma, CardId.CalledbytheGrave, CardId.CrossoutDesignator };
            int count = this.Bot.Hand.GetMatchingCardsCount(card => counter_cards.Contains(card.Id));
            count += this.Bot.SpellZone.GetMatchingCardsCount(card => counter_cards.Contains(card.Id));
            if (count > 0 || this.Bot.Hand.GetCardCount(this.Card.Id) >= 2)
            {
                this.SelectSTPlace(this.Card, true);
                return true;
            }
            return Program._rand.Next(2) > 0;
        }

        /// <summary>
        /// Summon Witchcraft for special summoning from deck.
        /// </summary>
        public bool WitchcraftSummon()
        {
            if (this.UseSSEffect.Contains(this.Card.Id))
            {
                return false;
            }

            int count_spell = this.Bot.Hand.GetMatchingCardsCount(card => (card.IsSpell()));
            int count_target = this.CheckRemainInDeck(CardId.MadameVerre, CardId.Haine, CardId.GolemAruru);
            if (count_spell > 0 && count_target > 0)
            {
                this.summoned = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Summon Witchcraft for recycling spells
        /// </summary>
        public bool WitchcraftSummonForRecycle()
        {
            if (!this.Card.HasSetcode(this.Witchcraft_setcode) || this.Card.Level > 4)
            {
                return false;
            }
            if (this.CheckRecyclableCount(false, true) > 0 && this.Bot.MonsterZone.GetFirstMatchingFaceupCard(card => card.HasSetcode(this.Witchcraft_setcode)) == null)
            {
                this.summoned = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check whether summon monster for link summon.
        /// </summary>
        public bool SummonForLink()
        {
            // reject advance summon
            if (this.Card.Level >= 5)
            {
                return false;
            }

            this.summoned = true;

            if (this.BorrelswordDragonSummonCheck(this.Card).Count >= 3)
            {
                Logger.DebugWriteLine("Summon for BorrelswordDragon.");
                List<ClientCard> list = this.BorrelswordDragonSummonCheck(this.Card);
                foreach( ClientCard c in list)
                {
                    Logger.DebugWriteLine(c.Name ?? "???");
                }
                return true;
            }
            if (this.KnightmareUnicornSummonCheck(this.Card).Count >= 2)
            {
                Logger.DebugWriteLine("Summon for KnightmareUnicorn.");
                return true;
            }
            if (this.KnightmarePhoenixSummonCheck(this.Card).Count >= 2)
            {
                Logger.DebugWriteLine("Summon for KnightmarePhoenix.");
                return true;
            }
            if (this.RelinquishedAnimaSummonCheck(this.Card) != -1)
            {
                Logger.DebugWriteLine("Summon for RelinquishedAnima.");
                return true;
            }
            if (this.SalamangreatAlmirajSummonCheck(this.Card))
            {
                Logger.DebugWriteLine("Summon for SalamangreatAlmiraj.");
                return true;
            }

            this.summoned = false;
            return false;
        }

        /// <summary>
        /// Special Witchcraft from deck for all monsters, except spells/traps.
        /// </summary>
        /// <param name="level">max level can be special summoned.</param>
        public bool DeckSSWitchcraft()
        {
            if (this.Card.Location != CardLocation.MonsterZone)
            {
                return false;
            }

            if (this.Duel.LastChainPlayer == 0)
            {
                return false;
            }

            if (this.NegatedCheck(false))
            {
                return false;
            }

            if (this.Duel.Player == 0 && !this.FirstCheckSS.Contains(this.Card.Id))
            {
                // activate when ask twice
                this.FirstCheckSS.Add(this.Card.Id);
                return false;
            }

            // get discardable count
            int discardable_hands = this.CheckDiscardableSpellCount();

            // not must SS
            if (discardable_hands == 0 && this.Bot.MonsterZone.GetFirstMatchingCard(card => card.HasSetcode(this.Witchcraft_setcode) && card.Level >= 6) != null)
            {
                return false;
            }

            this.SelectDiscardSpell();

            // check whether should call MadameVerre for destroying monster
            bool lesssummon = false;
            int extra_attack = this.CheckPlusAttackforMadameVerre(true, false, true);
            int best_power = this.Util.GetBestAttack(this.Bot);
            if (this.CheckRemainInDeck(CardId.Haine) > 0 && best_power < 2400)
            {
                best_power = 2400;
            }

            Logger.DebugWriteLine("less summon check: " + (best_power + extra_attack - 1000).ToString() + " to " + (best_power + extra_attack).ToString());
            if (this.Util.GetOneEnemyBetterThanValue(best_power) != null 
                && this.Util.GetOneEnemyBetterThanValue(best_power + extra_attack) == null
                && this.Util.GetOneEnemyBetterThanValue(best_power + extra_attack - 1000) != null)
            {
                lesssummon = true;
            }
            
            // SS lower 4
            if (!this.enemy_activate_MaxxC && !lesssummon && discardable_hands >= 2 && this.Duel.Player == 0)
            {
                int[] SS_priority = { CardId.Schmietta, CardId.Pittore, CardId.Genni, CardId.Potterie };
                foreach (int cardid in SS_priority)
                {
                    if (!this.UseSSEffect.Contains(cardid) && this.Card.Id != cardid && this.CheckRemainInDeck(cardid) > 0
                        && this.Bot.MonsterZone.GetFirstMatchingCard(card => card.Id == cardid && card.IsFaceup()) == null)
                    {
                        this.UseSSEffect.Add(this.Card.Id);
                        this.AI.SelectNextCard(cardid);
                        return true;
                    }
                }
            }

            // check whether continue to ss
            bool should_attack = this.Util.GetOneEnemyBetterThanValue(this.Card.Attack) == null;
            if ((should_attack ^ this.Card.IsDefense()) && this.Duel.Player == 1)
            {
                return false;
            }

            if (this.CheckRemainInDeck(CardId.Haine, CardId.MadameVerre, CardId.GolemAruru) == 0)
            {
                return false;
            }

            // SS higer level
            if (this.Bot.HasInMonstersZone(CardId.Haine) || (lesssummon && !this.Bot.HasInMonstersZone(CardId.MadameVerre, true)))
            {
                this.AI.SelectNextCard(CardId.MadameVerre, CardId.Haine, CardId.GolemAruru);
            }
            else
            {
                this.AI.SelectNextCard(CardId.Haine, CardId.MadameVerre, CardId.GolemAruru);
            }
            this.UseSSEffect.Add(this.Card.Id);
            return true;
        }

        // recycle witchcraft spells in grave
        public bool WitchcraftRecycle()
        {
            if (this.Card.IsSpell() && this.Card.HasSetcode(this.Witchcraft_setcode) && this.Card.Location == CardLocation.Grave) {
                this.ActivatedCards.Add(this.Card.Id);
                if (this.Card.HasType(CardType.Continuous))
                {
                    this.SelectSTPlace(this.Card);
                }
                return true;
            }
            return false;
        }

        // activate of GolemAruru
        public bool GolemAruruActivate()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.GolemAruru, 2))
            {
                return true;
            }
            if (this.NegatedCheck())
            {
                return false;
            }

            ClientCard targetcard = this.CheckProblematicCards(true);
            if (targetcard != null)
            {
                this.AI.SelectCard(targetcard);
                return true;
            }
            this.AI.SelectCard(CardId.Holiday, CardId.Creation, CardId.Draping, CardId.Scroll, CardId.WitchcrafterBystreet, CardId.Unveiling, CardId.Collaboration );
            return true;
        }

        // activate of MadameVerre
        public bool MadameVerreActivate()
        {
            if (this.NegatedCheck(true))
            {
                return false;
            }
            // negate
            if (this.ActivateDescription == this.Util.GetStringId(CardId.MadameVerre, 1))
            {
                if (this.Card.IsDisabled())
                {
                    return false;
                }

                if (this.CheckLastChainNegated())
                {
                    return false;
                }

                // negate before activate
                if (this.Enemy.MonsterZone.GetFirstMatchingCard(card => card.IsMonsterShouldBeDisabledBeforeItUseEffect() && !card.IsDisabled()) != null)
                {
                    this.SelectDiscardSpell();
                    return true;
                }

                // chain check
                ClientCard LastChainCard = this.Util.GetLastChainCard();
                if ((LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.MonsterZone))
                {
                    // negate monsters' activate
                    this.SelectDiscardSpell();
                    return true;
                }

                // negate battle related effect
                if (this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2)
                {
                    if (this.Enemy.MonsterZone.GetFirstMatchingCard(card => 
                        card.IsMonsterDangerous() || (this.Duel.Player == 0) && card.IsMonsterInvincible()) != null)
                    {
                        this.SelectDiscardSpell();
                        return true;
                    }
                }

                return false;
            }
            // gain ATK
            else
            {
                ClientCard self_card = this.Bot.BattlingMonster;
                ClientCard enemy_card = this.Enemy.BattlingMonster;
                if (self_card != null && enemy_card != null)
                {
                    int power_cangain = this.CheckPlusAttackforMadameVerre();
                    int diff = enemy_card.GetDefensePower() - self_card.GetDefensePower();
                    Logger.DebugWriteLine("power: " + power_cangain.ToString());
                    Logger.DebugWriteLine("diff: " + diff.ToString());
                    if (diff > 0)
                    {
                        // avoid useless effect
                        if (self_card.IsDefense() && power_cangain < diff)
                        {
                            return false;
                        }
                        this.AI.SelectCard(this.Bot.Hand.GetMatchingCards(card => card.IsSpell()));
                        this.MadameVerreGainedATK = true;
                        return true;
                    }
                    else if (this.Enemy.GetMonsterCount() == 1 || (enemy_card.IsAttack() && this.Enemy.LifePoints <= diff + power_cangain))
                    {
                        this.AI.SelectCard(this.Bot.Hand.GetMatchingCards(card => card.IsSpell()));
                        this.MadameVerreGainedATK = true;
                        return true;
                    }
                }
            }
            return false;
        }

        // activate of Haine
        public bool HaineActivate()
        {
            if (this.NegatedCheck(true) || this.Duel.LastChainPlayer == 0)
            {
                return false;
            }
            // danger check
            ClientCard targetcard = this.Enemy.MonsterZone.GetFloodgate(true);
            if (targetcard == null)
            {
                Logger.DebugWriteLine("*** Haine 2nd check.");
                targetcard = this.Enemy.SpellZone.FirstOrDefault(card => card?.Data != null && card.IsFloodgate() && card.IsFaceup() && (!card.IsShouldNotBeTarget() || !this.Duel.ChainTargets.Contains(card)));
                // GetFloodgate(true);
            }
            if (targetcard == null)
            {
                Logger.DebugWriteLine("*** Haine 3rd check.");
                targetcard = this.CheckProblematicCards(true, (this.Duel.Phase <= DuelPhase.Main1 || this.Duel.Phase >= DuelPhase.Main2));
                if (targetcard != null && targetcard.HasSetcode(this.TimeLord_setcode) && !targetcard.IsDisabled())
                {
                    targetcard = null;
                }
            }
            if (targetcard == null && this.Duel.LastChainPlayer == 1)
            {
                Logger.DebugWriteLine("*** Haine 4th check.");
                ClientCard lastcard = this.Util.GetLastChainCard();
                if (lastcard != null && !lastcard.IsDisabled() && !this.CheckLastChainNegated()
                    && (lastcard.HasType(CardType.Continuous) || lastcard.HasType(CardType.Equip) || lastcard.HasType(CardType.Field))
                    && (lastcard.Location == CardLocation.SpellZone || lastcard.Location == CardLocation.FieldZone))
                {
                    targetcard = lastcard;
                }
            }
            if (targetcard != null)
            {
                Logger.DebugWriteLine("*** Haine target: "+ targetcard.Name ?? "???");
                this.SelectDiscardSpell();
                this.AI.SelectNextCard(targetcard);
                return true;
            }

            // pendulum check
            if (!this.CheckLastChainNegated())
            {
                ClientCard l = null;
                ClientCard r = null;
                if (this.Duel.IsNewRule || this.Duel.IsNewRule2020)
                {
                    l = this.Enemy.SpellZone[0];
                    r = this.Enemy.SpellZone[4];
                }
                else
                {
                    l = this.Enemy.SpellZone[6];
                    r = this.Enemy.SpellZone[7];
                }
                if (l != null && r != null && l.LScale != r.RScale)
                {
                    Logger.DebugWriteLine("*** Haine pendulum destroy");
                    this.SelectDiscardSpell();
                    this.AI.SelectNextCard(Program._rand.Next(2) == 1 ? l : r);
                    return true;
                }
            }
            

            // end check
            if (this.Duel.Player == 0 && this.Duel.Phase == DuelPhase.End)
            {
                Logger.DebugWriteLine("*** Haine self check");
                int selected_cost = 0;
                // spare spell check
                int[] checklist = { CardId.Collaboration, CardId.Unveiling, CardId.Scroll, CardId.Holiday, CardId.Creation, CardId.Draping };
                foreach (int cardid in checklist)
                {
                    if (!this.ActivatedCards.Contains(cardid) && this.Bot.HasInHand(cardid))
                    {
                        selected_cost = cardid;
                        break;
                    }
                }

                if (selected_cost == 0)
                {
                    return false;
                }

                IList<ClientCard> target_1 = this.Enemy.SpellZone.GetMatchingCards(card => card.IsFaceup());
                IList<ClientCard> target_2 = this.Enemy.MonsterZone.GetMatchingCards(card => card.IsFaceup());
                List<ClientCard> targets = target_1.Union(target_2).ToList();
                if (targets.Count == 0)
                {
                    return false;
                }
                // shuffle and select randomly
                targets = this.CardListShuffle(targets);
                this.AI.SelectCard(selected_cost);
                this.AI.SelectNextCard(targets);
                return true;
            }
            return false;
        }

        // activate of Schmietta
        public bool SchmiettaActivate()
        {
            if (this.Card.Location != CardLocation.Grave)
            {
                return false;
            }

            if (this.NegatedCheck(false) || this.CheckWhetherWillbeRemoved())
            {
                return false;
            }
            // spell check
            bool can_recycle = this.Bot.MonsterZone.GetFirstMatchingCard(
                card => card.IsFaceup() && card.HasSetcode(this.Witchcraft_setcode) && card.Id != CardId.GolemAruru
                ) != null;
            if (can_recycle)
            {
                int[] spell_checklist = { CardId.WitchcrafterBystreet, CardId.Holiday, CardId.Creation, CardId.Draping, CardId.Scroll, CardId.Unveiling, CardId.Collaboration };
                foreach (int cardid in spell_checklist)
                {
                    if (this.CheckRemainInDeck(cardid) > 0 && !this.Bot.HasInHandOrInSpellZone(cardid) && !this.Bot.HasInGraveyard(cardid) && !this.ActivatedCards.Contains(cardid))
                    {
                        this.AI.SelectCard(cardid);
                        this.ActivatedCards.Add(CardId.Schmietta);
                        return true;
                    }
                }
            }

            bool can_find_Holiday = this.Bot.HasInHandOrInSpellZone(CardId.Holiday) || (can_recycle && this.Bot.HasInGraveyard(CardId.Holiday) && !(this.ActivatedCards.Contains(CardId.Holiday)));
            // monster check
            if (this.Bot.HasInHand(this.important_witchcraft)  && !this.Bot.HasInGraveyard(CardId.Pittore) 
                && !this.ActivatedCards.Contains(CardId.Pittore) && this.CheckRemainInDeck(CardId.Pittore) > 0 && can_find_Holiday){
                this.AI.SelectCard(CardId.Pittore);
                this.ActivatedCards.Add(CardId.Schmietta);
                return true;
            }

            // ss check
            if (this.Bot.HasInHand(CardId.Holiday) && !this.ActivatedCards.Contains(CardId.Holiday) && !this.Bot.HasInGraveyard(this.important_witchcraft))
            {
                this.AI.SelectCard(this.important_witchcraft);
                this.ActivatedCards.Add(CardId.Schmietta);
                return true;
            }

            // copy check
            if (!this.ActivatedCards.Contains(CardId.Genni))
            {
                int has_Genni = this.Bot.HasInGraveyard(CardId.Genni) ? 1 : 0;
                int has_Holiday = this.Bot.HasInGraveyard(CardId.Holiday) ? 1 : 0;
                int has_important = this.Bot.HasInGraveyard(this.important_witchcraft) ? 1 : 0;
                // lack one of them
                if (has_Genni + has_Holiday + has_important == 2)
                {
                    if (has_Genni == 0)
                    {
                        this.AI.SelectCard(CardId.Genni);
                        this.ActivatedCards.Add(CardId.Schmietta);
                        return true;
                    }
                    if (has_Holiday == 0)
                    {
                        this.AI.SelectCard(CardId.Holiday);
                        this.ActivatedCards.Add(CardId.Schmietta);
                        return true;
                    }
                    if (has_important == 0)
                    {
                        this.AI.SelectCard(this.important_witchcraft);
                        this.ActivatedCards.Add(CardId.Schmietta);
                        return true;
                    }
                }
            }

            // Pittore check
            if (!this.ActivatedCards.Contains(CardId.Pittore) && !this.Bot.HasInGraveyard(CardId.Pittore))
            {
                if (this.PittoreActivate())
                {
                    this.AI.SelectCard(CardId.Pittore);
                    this.ActivatedCards.Add(CardId.Schmietta);
                    return true;
                }
            }

            // trap check
            if (this.CheckRemainInDeck(CardId.Masterpiece) >= 2){
                this.AI.SelectCard(CardId.Masterpiece);
                this.ActivatedCards.Add(CardId.Schmietta);
                return true;
            }

            return false;
        }

        // activate of Pittore
        public bool PittoreActivate()
        {
            if (this.Card.Location != CardLocation.Grave)
            {
                return false;
            }

            if (this.NegatedCheck(false) || this.CheckWhetherWillbeRemoved())
            {
                return false;
            }

            if (this.Bot.Hand.GetFirstMatchingCard(card => card.HasSetcode(this.Witchcraft_setcode)) == null)
            {
                return false;
            }

            // discard advance
            if (this.Bot.Hand.GetFirstMatchingCard(card => card.Id == CardId.MadameVerre || card.Id == CardId.Haine) != null)
            {
                this.AI.SelectCard(CardId.MadameVerre, CardId.Haine);
                this.ActivatedCards.Add(CardId.Pittore);
                return true;
            }

            // spell check
            int[] spell_checklist = { CardId.Scroll, CardId.Unveiling, CardId.Collaboration, CardId.Draping, CardId.WitchcrafterBystreet, CardId.Holiday, CardId.Creation };
            foreach (int cardid in spell_checklist)
            {
                if (this.Bot.HasInHand(cardid) && !this.ActivatedCards.Contains(cardid)){
                    this.AI.SelectCard(cardid);
                    this.ActivatedCards.Add(CardId.Pittore);
                    return true;
                }
            }

            // monster check
            if ((this.Bot.HasInHand(CardId.Schmietta) && !this.ActivatedCards.Contains(CardId.Schmietta))
                || this.Bot.Hand.GetMatchingCardsCount(card => card.HasSetcode(this.Witchcraft_setcode) && card.Level <= 4) >= 2){
                int[] monster_checklist = { CardId.Schmietta, CardId.Pittore, CardId.Genni, CardId.Potterie};
                foreach (int cardid in spell_checklist)
                {
                    if (this.Bot.HasInHand(cardid)){
                        this.AI.SelectCard(cardid);
                        this.ActivatedCards.Add(CardId.Pittore);
                        return true;
                    }
                }
            }

            return false;
        }

        // activate of AshBlossom_JoyousSpring
        public bool AshBlossom_JoyousSpringActivate()
        {
            if (this.NegatedCheck(true) || this.CheckLastChainNegated())
            {
                return false;
            }

            this.CheckDeactiveFlag();
            return this.DefaultAshBlossomAndJoyousSpring();
        }

        // activate of PSYGamma
        public bool PSYGammaActivate()
        {
            if (this.NegatedCheck(true))
            {
                return false;
            }

            this.CheckDeactiveFlag();
            return true;
        }

        // activate of MaxxC
        public bool MaxxCActivate()
        {
            if (this.NegatedCheck(true))
            {
                return false;
            }

            return this.DefaultMaxxC();
        }

        // activate of Potterie
        public bool PotterieActivate()
        {
            if (this.Card.Location != CardLocation.Grave)
            {
                return false;
            }

            if (this.NegatedCheck(true))
            {
                return false;
            }

            // Holiday check
            if (!this.ActivatedCards.Contains(CardId.Holiday) && this.Bot.HasInGraveyard(CardId.Holiday)){
                if (this.Bot.HasInGraveyard(this.important_witchcraft)){
                    this.AI.SelectCard(CardId.Holiday);
                    this.ActivatedCards.Add(CardId.Potterie);
                    return true;
                }
            }
            
            // safe check
            if (this.CheckProblematicCards() == null){
                int[] checklist = {CardId.Patronus, CardId.GolemAruru};
                foreach (int cardid in checklist){
                    if (this.Bot.HasInGraveyard(cardid)){
                        this.AI.SelectCard(cardid);
                        this.ActivatedCards.Add(CardId.Potterie);
                        return true;
                    }
                }
            }
            return false;
        }

        // activate of Genni
        public bool GenniActivate()
        {
            if (this.Card.Location != CardLocation.Grave)
            {
                return false;
            }

            if (this.NegatedCheck(true))
            {
                return false;
            }

            // Holiday check
            int HolidayCount = this.Bot.Graveyard.GetMatchingCardsCount(card => card.Id == CardId.Holiday);
            int SS_id = this.HolidayCheck(this.Card);
            if (HolidayCount > 0 && SS_id > 0){
                this.AI.SelectCard(CardId.Holiday);
                this.AI.SelectNextCard(SS_id);
                this.ActivatedCards.Add(CardId.Genni);
                return true;
            }

            // Draping check
            if (this.Bot.HasInGraveyard(CardId.Draping)){
                if (this.Enemy.GetMonsterCount() == 0 && this.Duel.Phase == DuelPhase.Main1){
                    int total_attack = 0;
                    foreach (ClientCard card in this.Bot.GetMonsters()){
                        total_attack += card.Attack;
                    }
                    // otk confirm
                    if (total_attack >= this.Enemy.LifePoints){
                        int bot_count = this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsFaceup() && card.HasSetcode(this.Witchcraft_setcode));
                        IList<ClientCard> enemy_cards = this.Enemy.GetSpells();
                        if (bot_count >= enemy_cards.Count()){
                            this.AI.SelectCard(CardId.Draping);
                            this.AI.SelectNextCard(enemy_cards);
                            this.ActivatedCards.Add(CardId.Genni);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        // activate of Collaboration
        public bool CollaborationActivate()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                return false;
            }

            if (this.NegatedCheck(true))
            {
                return false;
            }

            ClientCard target = this.Util.GetBestBotMonster(true);
            if (this.Util.GetOneEnemyBetterThanMyBest() == null){
                if (this.Enemy.SpellZone.GetFirstMatchingCard(card => card.IsFacedown()) != null
                    || this.Enemy.MonsterZone.GetMatchingCardsCount(card => card.GetDefensePower() < target.Attack) >= 2){
                    this.AI.SelectCard(target);
                    this.SelectSTPlace(null, true);
                    this.ActivatedCards.Add(CardId.Collaboration);
                    return true;
                }
            }
            return false;
        }

        // activate of LightningStorm
        public bool LightningStormActivate()
        {
            int bestPower = 0;
            foreach (ClientCard hand in this.Bot.Hand)
            {
                if (hand.IsMonster() && hand.Level <= 4 && hand.Attack > bestPower)
                {
                    bestPower = hand.Attack;
                }
            }

            int opt = -1;
            // destroy monster
            if (this.Enemy.MonsterZone.GetFirstMatchingCard(card => card.IsFloodgate() && card.IsAttack()) != null
                || this.Enemy.MonsterZone.GetMatchingCardsCount(card => card.IsAttack() && card.Attack >= bestPower) >= 2)
            {
                opt = 0;
            }
            // destroy spell/trap
            else if (this.Enemy.GetSpellCount() >= 2 || this.Util.GetProblematicEnemySpell() != null)
            {
                opt = 1;
            }

            if (opt == -1)
            {
                return false;
            }

            // only one selection
            if (this.Enemy.MonsterZone.GetFirstMatchingCard(card => card.IsAttack()) == null 
                || this.Enemy.GetSpellCount() == 0)
            {
                this.AI.SelectOption(0);
                this.SelectSTPlace(null, true);
                return true;
            }
            this.AI.SelectOption(opt);
            this.SelectSTPlace(null, true);
            return true;
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

        // activate of DarkRulerNoMore
        public bool DarkRulerNoMoreActivate()
        {
            if (this.SpellNegatable())
            {
                return false;
            }

            if (this.Enemy.MonsterZone.GetFirstMatchingCard(card => card.IsFloodgate() && !card.IsDisabled()) != null)
            {
                this.SelectSTPlace(null, true);
                return true;
            }
            return false;
        }

        // activate of Creation
        public bool CreationActivate()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                return false;
            }

            if (this.NegatedCheck(true))
            {
                return false;
            }

            // discard cost ensure
            int least_cost = (this.Bot.HasInMonstersZone(CardId.Haine) ? 1 : 0) + (this.Bot.HasInMonstersZone(CardId.MadameVerre) ? 1 : 0);
            int discardable = this.Bot.Hand.GetMatchingCardsCount(card => card != this.Card && card.IsSpell()) + this.CheckRecyclableCount() -1;
            if (discardable < least_cost)
            {
                return false;
            }

            // search monster to summon
            bool need_lower = (!this.summoned || (
                this.Bot.MonsterZone.GetFirstMatchingCard(card => card.HasSetcode(this.Witchcraft_setcode)) == null
                && this.Bot.Hand.GetFirstMatchingCard(card => card.IsMonster() && card.HasSetcode(this.Witchcraft_setcode) && card.Level <= 4) == null));
            if (need_lower)
            {
                this.AI.SelectCard(CardId.Schmietta, CardId.Pittore, CardId.Genni, CardId.Potterie, CardId.GolemAruru);
                this.SelectSTPlace(null, true);
                this.ActivatedCards.Add(CardId.Creation);
                return true;
            }
            // search GolemAruru
            else
            {
                if (this.Bot.HasInHand(CardId.GolemAruru))
                {
                    return false;
                }

                if (this.Bot.MonsterZone.GetFirstMatchingCard(card => card.IsFaceup() && card.HasSetcode(this.Witchcraft_setcode)) == null)
                {
                    this.AI.SelectCard(CardId.GolemAruru, CardId.Schmietta, CardId.Pittore, CardId.Genni, CardId.Potterie);
                    this.SelectSTPlace(null, true);
                    this.ActivatedCards.Add(CardId.Creation);
                    return true;
                } else
                {
                    this.AI.SelectCard(CardId.Schmietta, CardId.Pittore, CardId.Genni, CardId.Potterie, CardId.GolemAruru);
                    this.SelectSTPlace(null, true);
                    this.ActivatedCards.Add(CardId.Creation);
                    return true;
                }
            }
        }

        /// <summary>
        /// Check Holiday's target. If nothing should be SS, return 0.
        /// </summary>
        /// <param name="except_card"></param>
        /// <returns></returns>
        public int HolidayCheck(ClientCard except_card = null){
            // SS important first
            List<int> check_list = new List<int> { CardId.Haine, CardId.MadameVerre, CardId.GolemAruru};
            foreach (int cardid in check_list)
            {
                if (this.Bot.HasInGraveyard(cardid) && this.Bot.MonsterZone.GetFirstMatchingCard(card => card.IsFaceup() && card.Id == cardid) == null)
                {
                    Logger.DebugWriteLine("*** Holiday check 1st: " + cardid.ToString());
                    return cardid;
                }
            }
            check_list.Clear();
            if (this.CheckProblematicCards() == null)
            {
                if (this.Bot.HasInGraveyard(CardId.GolemAruru) && this.Bot.MonsterZone.GetFirstMatchingCard(card => card.IsFaceup() && card.HasSetcode(this.Witchcraft_setcode)) != null)
                {
                    Logger.DebugWriteLine("*** Holiday check 2nd: GolemAruru");
                    return CardId.GolemAruru;
                }
                check_list.Add(CardId.Schmietta);
                check_list.Add(CardId.Pittore);
                check_list.Add(CardId.Genni);
                check_list.Add(CardId.Potterie);
                foreach (int cardid in check_list)
                {
                    if (!this.UseSSEffect.Contains(cardid) && this.Bot.Graveyard.GetFirstMatchingCard(card => card.Id == cardid && card != except_card) != null && this.CheckDiscardableSpellCount(this.Card) > 0)
                    {
                        Logger.DebugWriteLine("*** Holiday check 3rd: " + cardid.ToString());
                        return cardid;
                    }
                }
            }
            else
            {
                check_list.Add(CardId.Haine);
                check_list.Add(CardId.MadameVerre);
                check_list.Add(CardId.GolemAruru);
                foreach (int cardid in check_list)
                {
                    if (this.Bot.Graveyard.GetFirstMatchingCard(card => card.Id == cardid && card != except_card) != null)
                    {
                        return cardid;
                    }
                }
            }
            return 0;
        }

        // activate of Holiday
        public bool HolidayActivate()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                return false;
            }

            if (this.NegatedCheck(true))
            {
                return false;
            }

            int target = this.HolidayCheck();
            if (target != 0)
            {
                this.AI.SelectCard(target);
                this.SelectSTPlace(null, true);
                this.ActivatedCards.Add(CardId.Holiday);
                return true;
            }
            return false;
        }

        // activate of CalledbytheGrave
        public bool CalledbytheGraveActivate()
        {
            if (this.NegatedCheck(true))
            {
                return false;
            }

            if (this.Duel.LastChainPlayer == 1)
            {
                // negate
                if (this.Util.GetLastChainCard().IsMonster())
                {
                    int code = this.Util.GetLastChainCard().Id;
                    if (code == 0)
                    {
                        return false;
                    }

                    if (this.CheckCalledbytheGrave(code) > 0 || this.CrossoutDesignatorTarget == code)
                    {
                        return false;
                    }

                    if (this.Enemy.Graveyard.GetFirstMatchingCard(card => card.IsMonster() && card.IsOriginalCode(code)) != null)
                    {
                        if (!(this.Card.Location == CardLocation.SpellZone))
                        {
                            this.SelectSTPlace(null, true);
                        }
                        this.AI.SelectCard(code);
                        this.CalledbytheGraveCount[code] = 2;
                        this.CheckDeactiveFlag();
                        return true;
                    }
                }
                
                // banish target
                foreach (ClientCard cards in this.Enemy.Graveyard)
                {
                    if (this.Duel.ChainTargets.Contains(cards))
                    {
                        int code = cards.Id;
                        this.AI.SelectCard(cards);
                        this.CalledbytheGraveCount[code] = 2;
                        return true;
                    }
                }

                // become targets
                if (this.Duel.ChainTargets.Contains(this.Card))
                {
                    List<ClientCard> enemy_monsters = this.Enemy.Graveyard.GetMatchingCards(card => card.IsMonster()).ToList();
                    if (enemy_monsters.Count > 0)
                    {
                        enemy_monsters.Sort(CardContainer.CompareCardAttack);
                        enemy_monsters.Reverse();
                        int code = enemy_monsters[0].Id;
                        this.AI.SelectCard(code);
                        this.CalledbytheGraveCount[code] = 2;
                        return true;
                    }
                }
            }

            // avoid danger monster in grave
            if (this.Duel.LastChainPlayer == 1)
            {
                return false;
            }

            List<ClientCard> targets = this.CheckDangerousCardinEnemyGrave(true);
            if (targets.Count() > 0) {
                int code = targets[0].Id;
                if (!(this.Card.Location == CardLocation.SpellZone))
                {
                    this.SelectSTPlace(null, true);
                }
                this.AI.SelectCard(code);
                this.CalledbytheGraveCount[code] = 2;
                return true;
            }

            return false;
        }

        // activate of Draping
        public bool DrapingActivate()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                return false;
            }

            if (this.NegatedCheck(true))
            {
                return false;
            }

            IList<ClientCard> dangerours_spells = this.Enemy.SpellZone.GetMatchingCards(card => card.IsFloodgate() && !card.IsDisabled() && card.IsSpell());
            IList<ClientCard> dangerours_traps = this.Enemy.SpellZone.GetMatchingCards(card => card.IsFloodgate() && !card.IsDisabled() && card.IsTrap());
            List<ClientCard> faceup_spells = this.CardListShuffle(this.Enemy.SpellZone.GetMatchingCards(card => card.IsFaceup() && card.IsSpell()).ToList());
            List<ClientCard> faceup_traps = this.CardListShuffle(this.Enemy.SpellZone.GetMatchingCards(card => card.IsFaceup() && card.IsTrap()).ToList());
            List<ClientCard> setcards = this.CardListShuffle(this.Enemy.SpellZone.GetMatchingCards(card => card.IsFacedown()).ToList());
            if (this.Duel.Player == 0 || this.Duel.Phase == DuelPhase.End)
            {
                IList<ClientCard> targets_1 = dangerours_spells.Union(dangerours_traps).Union(faceup_spells).Union(faceup_traps).Union(setcards).ToList();
                if (targets_1.Count() == 0)
                {
                    return false;
                }

                this.AI.SelectCard(targets_1);
                this.SelectSTPlace(null, true);
                this.ActivatedCards.Add(CardId.Draping);
                return true;
            }
            IList<ClientCard> targets_2 = dangerours_traps.Union(faceup_traps).ToList();
            if (targets_2.Count() == 0)
            {
                return false;
            }

            targets_2 = targets_2.Union(dangerours_spells).Union(faceup_spells).Union(setcards).ToList();
            this.AI.SelectCard(targets_2);
            this.SelectSTPlace(null, true);
            this.ActivatedCards.Add(CardId.Draping);
            return true;
        }

        // activate of CrossoutDesignator
        public bool CrossoutDesignatorActivate()
        {
            if (this.NegatedCheck(true) || this.CheckLastChainNegated())
            {
                return false;
            }
            // negate 
            if (this.Duel.LastChainPlayer == 1 && this.Util.GetLastChainCard() != null)
            {
                int code = this.Util.GetLastChainCard().Id;
                int alias = this.Util.GetLastChainCard().Alias;
                if (alias != 0 && alias - code < 10)
                {
                    code = alias;
                }

                if (code == 0)
                {
                    return false;
                }

                if (this.CheckCalledbytheGrave(code) > 0 || this.CrossoutDesignatorTarget == code)
                {
                    return false;
                }

                if (this.CheckRemainInDeck(code) > 0)
                {
                    if (!(this.Card.Location == CardLocation.SpellZone))
                    {
                        this.SelectSTPlace(null, true);
                    }
                    this.AI.SelectAnnounceID(code);
                    this.CrossoutDesignatorTarget = code;
                    this.CheckDeactiveFlag();
                    return true;
                }
            }
            return false;
        }

        // activate of Unveiling
        public bool UnveilingActivate()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                return false;
            }

            if (this.NegatedCheck(true))
            {
                return false;
            }

            // LightningStorm check
            if (this.Bot.HasInHandOrInSpellZone(CardId.LightningStorm))
            {
                int faceup_count = this.Bot.SpellZone.GetMatchingCardsCount(card => card.IsFaceup());
                faceup_count += this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsFaceup());
                if (faceup_count == 0 && this.LightningStormActivate())
                {
                    return false;
                }
            }

            if (this.Bot.HasInHand(this.important_witchcraft))
            {
                this.AI.SelectCard(this.important_witchcraft);
                this.SelectSTPlace(null, true);
                this.ActivatedCards.Add(CardId.Unveiling);
                return true;
            }
            return false;
        }

        // activate of Scroll
        public bool ScrollActivate()
        {
            if (this.SpellNegatable() || this.Card.Location == CardLocation.Grave || this.Duel.Phase == DuelPhase.Main2)
            {
                return false;
            }
            if (this.Bot.MonsterZone.GetFirstMatchingCard(card => card.HasRace(CardRace.SpellCaster)) == null)
            {
                return false;
            }
            this.SelectSTPlace(null, true);
            return true;
        }

        // activate of MagiciansRestage
        public bool MagiciansRestageActivate()
        {
            // search
            if (this.Card.Location == CardLocation.Grave)
            {
                if (this.Enemy.SpellZone.GetFirstMatchingCard(card => card.IsFacedown()) != null)
                {
                    this.AI.SelectCard(CardId.MagiciansLeftHand, CardId.MagicianRightHand);
                }
                else
                {
                    this.AI.SelectCard(CardId.MagicianRightHand, CardId.MagiciansLeftHand);
                }
                return true;
            }

            if (this.SpellNegatable())
            {
                return false;
            }

            // find target
            if (this.CheckDiscardableSpellCount(this.Card) < 1)
            {
                return false;
            }

            int target = 0;
            int[] target_list = { CardId.Genni, CardId.Pittore, CardId.Potterie };
            foreach (int cardid in target_list)
            {
                if (!this.UseSSEffect.Contains(cardid) && this.Bot.HasInGraveyard(cardid))
                {
                    target = cardid;
                    break;
                }
            }
            if (target == 0)
            {
                return false;
            }

            if (this.Card.Location == CardLocation.Hand)
            {
                this.SelectSTPlace(null, true);
                return true;
            }
            this.AI.SelectCard(target);
            return true;
        }

        // activate of WitchcrafterBystreet
        public bool WitchcrafterBystreetActivate()
        {
            if (this.SpellNegatable() || this.Card.Location == CardLocation.Grave)
            {
                return false;
            }
            if (this.Bot.HasInSpellZone(CardId.WitchcrafterBystreet, true) || this.Bot.MonsterZone.GetFirstMatchingCard(card => card.HasSetcode(this.Witchcraft_setcode) && card.IsFaceup()) == null)
            {
                return false;
            }
            this.SelectSTPlace(null, true);
            return true;
        }

        // activate of Impermanence
        public bool InfiniteImpermanenceActivate()
        {
            if (this.SpellNegatable())
            {
                return false;
            }

            if (this.CrossoutDesignatorTarget == CardId.InfiniteImpermanence)
            {
                return false;
            }

            if (this.CheckLastChainNegated())
            {
                return false;
            }
            // negate before monster's effect's used
            foreach (ClientCard m in this.Enemy.GetMonsters())
            {
                if (!m.IsDisabled() && this.Duel.LastChainPlayer != 0 && 
                    ((m.IsMonsterShouldBeDisabledBeforeItUseEffect() || m.IsFloodgate())
                    || (this.Duel.Phase > DuelPhase.Main1 && this.Duel.Phase < DuelPhase.Main2 && 
                        (m.IsMonsterDangerous() || m.IsMonsterInvincible() 
                        || (m.IsMonsterHasPreventActivationEffectInBattle() && this.Bot.HasInMonstersZone(CardId.MadameVerre)))
                     )))
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
                        this.SelectSTPlace(this.Card, true);
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

            // negate monsters
            if ((LastChainCard == null || LastChainCard.Controller != 1 || LastChainCard.Location != CardLocation.MonsterZone
                || this.CheckLastChainNegated() || LastChainCard.IsShouldNotBeTarget() || LastChainCard.IsShouldNotBeSpellTrapTarget()))
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
                this.SelectSTPlace(this.Card, true);
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

        // activate of Masterpiece
        public bool MasterpieceActivate()
        {
            // search effect
            if (this.Card.Location == CardLocation.SpellZone)
            {
                if (this.NegatedCheck(true))
                {
                    return false;
                }
                // select randomly (TODO)
                IList<ClientCard> target_1 = this.Bot.Graveyard.GetMatchingCards(card => card.IsSpell() && this.CheckRemainInDeck(card.Id) > 0);
                IList<ClientCard> target_2 = this.Enemy.Graveyard.GetMatchingCards(card => card.IsSpell() && this.CheckRemainInDeck(card.Id) > 0);
                List<ClientCard> targets = this.CardListShuffle(target_1.Union(target_2).ToList());
                this.AI.SelectCard(targets);
                return true;
            }
            else
            // ss effect
            {
                // LightningStorm check
                if (this.Bot.HasInHandOrInSpellZone(CardId.LightningStorm))
                {
                    int faceup_count = this.Bot.SpellZone.GetMatchingCardsCount(card => card.IsFaceup());
                    faceup_count += this.Bot.MonsterZone.GetMatchingCardsCount(card => card.IsFaceup());
                    if (faceup_count == 0 && this.LightningStormActivate())
                    {
                        return false;
                    }
                }

                List<ClientCard> tobanish_spells = this.CardListShuffle(this.Bot.Graveyard.GetMatchingCards(card => card.IsSpell() && !card.HasSetcode(this.Witchcraft_setcode) && card.Id != CardId.MetalfoesFusion).ToList());
                if (this.Bot.HasInGraveyard(CardId.Patronus))
                {
                    List<ClientCard> witchcraft_spells = this.CardListShuffle(this.Bot.Graveyard.GetMatchingCards(card => card.IsSpell() && card.HasSetcode(this.Witchcraft_setcode)).ToList());
                    tobanish_spells = witchcraft_spells.Union(tobanish_spells).ToList();
                }
                int max_level = tobanish_spells.Count();

                //check discardable count
                int discardable_hands = this.CheckDiscardableSpellCount();
                List<int> will_discard_list = new List<int> { CardId.Haine, CardId.MadameVerre, CardId.Schmietta, CardId.Pittore, CardId.Potterie, CardId.Genni };
                foreach (int cardid in will_discard_list)
                {
                    if (this.Bot.HasInMonstersZone(cardid))
                    {
                        discardable_hands--;
                    }
                }

                // SS lower 4
                if (discardable_hands >= 1 && this.Duel.Player == 0)
                {
                    int[] SS_priority = { CardId.Schmietta, CardId.Pittore, CardId.Genni, CardId.Potterie };
                    foreach (int cardid in SS_priority)
                    {
                        int level = this.witchcraft_level[cardid];
                        if (!this.UseSSEffect.Contains(cardid) & this.CheckRemainInDeck(cardid) > 0 && level <= max_level)
                        {
                            this.AI.SelectNumber(level);
                            this.AI.SelectCard(tobanish_spells);
                            this.AI.SelectNextCard(cardid);
                            return true;
                        }
                    }
                }

                // SS higer level
                List<int> ss_priority = new List<int>();
                if (this.Bot.HasInMonstersZone(CardId.Haine))
                {
                    ss_priority.Add(CardId.MadameVerre);
                    ss_priority.Add(CardId.Haine);
                }
                else
                {
                    ss_priority.Add(CardId.Haine);
                    ss_priority.Add(CardId.MadameVerre);
                }
                ss_priority.Add(CardId.GolemAruru);
                foreach (int cardid in ss_priority)
                {
                    int level = this.witchcraft_level[cardid];
                    if (this.CheckRemainInDeck(cardid) > 0 && level <= max_level)
                    {
                        this.AI.SelectNumber(level);
                        this.AI.SelectCard(tobanish_spells);
                        this.AI.SelectNextCard(cardid);
                        return true;
                    }
                }

            }
            return false;
        }

        // activate of Patronus
        public bool PatronusActivate()
        {
            // activate immediately
            if (this.ActivateDescription == 94)
            {
                return true;
            }
            // search
            if (this.Card.Location == CardLocation.SpellZone)
            {
                if (this.NegatedCheck(true) || this.Duel.LastChainPlayer == 0)
                {
                    return false;
                }
                // find lack of spells
                int lack_spells = 0;
                int[] spell_checklist = { CardId.WitchcrafterBystreet, CardId.Holiday, CardId.Creation, CardId.Draping, CardId.Scroll, CardId.Unveiling, CardId.Collaboration };
                foreach (int cardid in spell_checklist)
                {
                    if (!this.Bot.HasInHandOrInSpellZone(cardid) && !this.Bot.HasInGraveyard(cardid))
                    {
                        lack_spells = cardid;
                        break;
                    }
                }

                // banish check
                List<int> banish_checklist = new List<int>{ CardId.Haine, CardId.MadameVerre, CardId.GolemAruru, CardId.Schmietta, CardId.Pittore};
                if (lack_spells == 0)
                {
                    List<int> new_list = new List<int> { CardId.Pittore, CardId.Genni, CardId.Schmietta, CardId.Potterie };
                    banish_checklist = banish_checklist.Union(new_list).ToList();
                }
                else
                {
                    List<int> new_list = new List<int> { CardId.Schmietta, CardId.Pittore, CardId.Genni, CardId.Potterie };
                    banish_checklist = banish_checklist.Union(new_list).ToList();
                }
                foreach(int cardid in banish_checklist)
                {
                    ClientCard target = this.Bot.Banished.GetFirstMatchingCard(card => card.Id == cardid);
                    if (target != null)
                    {
                        this.AI.SelectCard(target);
                        this.AI.SelectNextCard(lack_spells);
                        return true;
                    }
                }
            }

            // recycle
            if (this.Card.Location == CardLocation.Grave)
            {
                if (this.Bot.HasInHandOrInSpellZoneOrInGraveyard(CardId.Masterpiece))
                {
                    return false;
                }
                IList<ClientCard> targets = this.Bot.Banished.GetMatchingCards(card => card.IsSpell() && card.HasSetcode(this.Witchcraft_setcode));
                this.AI.SelectCard(targets);
                return true;
            }
            return false;
        }

        // summmon process of Level 8 Synchro Monster
        public bool Lv8Summon()
        {
            if (this.Bot.HasInMonstersZone(CardId.PSYGamma) && this.Bot.HasInMonstersZone(CardId.PSYDriver))
            {
                List<int> targets = new List<int> { CardId.PSYDriver, CardId.PSYGamma };
                this.AI.SelectMaterials(targets);
                return true;
            }
            return false;
        }

        // summon process of BorreloadSavageDragon
        public bool BorreloadSavageDragonSummon()
        {
            if (this.Bot.Graveyard.GetFirstMatchingCard(card => card.HasType(CardType.Link)) == null)
            {
                return false;
            }
            return this.Lv8Summon();
        }

        // equip target comparer for BorreloadSavageDragon
        public static int BorreloadSavageDragonEquipCompare(ClientCard cardA, ClientCard cardB)
        {
            if (cardA.LinkCount > cardB.LinkCount)
            {
                return -1;
            }

            if (cardA.LinkCount < cardB.LinkCount)
            {
                return -1;
            }

            if (cardA.Attack > cardB.Attack)
            {
                return 1;
            }

            if (cardA.Attack < cardB.Attack)
            {
                return -1;
            }

            return 0;
        }

        // activate of BorreloadSavageDragon
        public bool BorreloadSavageDragonActivate()
        {
            // equip
            if (this.ActivateDescription == this.Util.GetStringId(CardId.BorreloadSavageDragon, 0))
            {
                List<ClientCard> links = this.Bot.Graveyard.GetMatchingCards(card => card.HasType(CardType.Link)).ToList();
                links.Sort(BorreloadSavageDragonEquipCompare);
                this.AI.SelectCard(links);
                return true;
            }
            // negate
            if (this.NegatedCheck(true) || this.Duel.LastChainPlayer != 1)
            {
                return false;
            }

            if (this.Util.GetLastChainCard().HasSetcode(0x11e) && this.Util.GetLastChainCard().Location == CardLocation.Hand)
            {
                return false;
            }

            this.CheckDeactiveFlag();
            return false;
        }

        // activate of DracoBerserkeroftheTenyi(TODO)
        public bool DracoBerserkeroftheTenyiActivate()
        {
            Logger.DebugWriteLine("DracoBerserkeroftheTenyi's Effect: " + this.ActivateDescription.ToString());
            return true;
        }

        // activate of PSYOmega
        public bool PSYOmegaActivate()
        {
            // recycle
            if (this.Duel.Phase == DuelPhase.Standby)
            {
                if (this.Bot.Banished.Count == 0)
                {
                    return false;
                }
                List<ClientCard> targets = this.CardListShuffle(this.Bot.Banished.GetMatchingCards(card => card.HasSetcode(this.Witchcraft_setcode)).ToList());
                this.AI.SelectCard(targets);
                return true;
            }
            // banish hands
            if (this.Card.Location == CardLocation.MonsterZone)
            {
                if (this.Duel.Player == 1 || this.Bot.HasInMonstersZone(CardId.PSYLambda) || (this.Util.IsChainTarget(this.Card)) )
                {
                    return true;
                } else
                {
                    return this.Util.IsAllEnemyBetterThanValue(this.Card.Attack, true);
                }
            }
            // recycle from grave
            if (this.Card.Location == CardLocation.Grave)
            {
                if (this.PSYOmega_count >= 5){
                    return false;
                }
                List<ClientCard> enemy_danger = this.CheckDangerousCardinEnemyGrave();
                if (enemy_danger.Count > 0)
                {
                    this.AI.SelectCard(enemy_danger);
                    this.PSYOmega_count ++;
                    return true;
                }
                if (!this.Bot.HasInHandOrInSpellZoneOrInGraveyard(CardId.Holiday) && this.Bot.HasInGraveyard(this.important_witchcraft))
                {
                    this.AI.SelectCard(this.important_witchcraft);
                    this.PSYOmega_count ++;
                    return true;
                }
                if (this.CheckProblematicCards() == null)
                {
                    this.AI.SelectCard(CardId.CalledbytheGrave, CardId.CrossoutDesignator,
                        CardId.MaxxC, CardId.AshBlossom_JoyousSpring,
                        CardId.MagicianRightHand, CardId.MagiciansLeftHand, CardId.MagiciansRestage, CardId.Patronus, 
                        CardId.LightningStorm, CardId.Reasoning);
                    this.PSYOmega_count ++;
                    return true;
                }
            }
            return false;
        }

        // activate of TGWonderMagician
        public bool TGWonderMagicianActivate()
        {
            if (this.Card.Location != CardLocation.MonsterZone)
            {
                return true;
            }

            Logger.DebugWriteLine("TGWonderMagician: " + this.ActivateDescription.ToString());
            List<ClientCard> problem_cards = this.Enemy.SpellZone.GetMatchingCards(card => card.IsFloodgate()).ToList();
            List<ClientCard> faceup_cards = this.Enemy.SpellZone.GetMatchingCards(card => card.IsFaceup()).ToList();
            List<ClientCard> facedown_cards = this.Enemy.SpellZone.GetMatchingCards(card => card.IsFacedown()).ToList();
            List<ClientCard> result = problem_cards.Union(faceup_cards).ToList().Union(facedown_cards).ToList();
            this.AI.SelectCard(result);
            return true;
        }

        // check whether summon BorrelswordDragon
        public List<ClientCard> BorrelswordDragonSummonCheck(ClientCard included = null)
        {
            List<ClientCard> empty_list = new List<ClientCard>();
            List<ClientCard> extra_list = new List<ClientCard>();
            if (included != null)
            {
                extra_list.Add(included);
            }

            List<ClientCard> materials = this.CheckLinkMaterials(4, 3, false, extra_list);
            if (materials.Count < 3)
            {
                return empty_list;
            }

            // need BorrelswordDragon?
            // for problem monster
            ClientCard flag = this.Util.GetOneEnemyBetterThanMyBest();
            if (flag != null)
            {
                return materials;
            }
            // for higher attack
            int total_attack = 0;
            foreach (ClientCard card in materials)
            {
                total_attack += card.Attack;
            }
            if (total_attack >= 3000)
            {
                return empty_list;
            }

            return materials;
        }

        // summon process of BorrelswordDragon
        public bool BorrelswordDragonSummon()
        {
            List<ClientCard> materials = this.BorrelswordDragonSummonCheck();
            if (materials.Count < 3)
            {
                return false;
            }

            this.AI.SelectMaterials(materials);
            return true;
        }

        // activate of BorrelswordDragon
        public bool BorrelswordDragonActivate()
        {
            if (this.ActivateDescription == -1)
            {
                ClientCard enemy_monster = this.Enemy.BattlingMonster;
                if (enemy_monster != null && enemy_monster.HasPosition(CardPosition.Attack))
                {
                    return (this.Card.Attack - enemy_monster.Attack < this.Enemy.LifePoints);
                }
                return true;
            };
            ClientCard BestEnemy = this.Util.GetBestEnemyMonster(true);
            ClientCard WorstBot = this.Bot.GetMonsters().GetLowestAttackMonster();
            if (BestEnemy == null || BestEnemy.HasPosition(CardPosition.FaceDown))
            {
                return false;
            }

            if (WorstBot == null || WorstBot.HasPosition(CardPosition.FaceDown))
            {
                return false;
            }

            if (BestEnemy.Attack >= WorstBot.RealPower)
            {
                this.AI.SelectCard(BestEnemy);
                return true;
            }
            return false;
        }

        // check whether summon KnightmareUnicorn
        public List<ClientCard> KnightmareUnicornSummonCheck(ClientCard included = null)
        {
            List<ClientCard> empty_list = new List<ClientCard>();
            List<ClientCard> extra_list = new List<ClientCard>();
            if (included != null)
            {
                extra_list.Add(included);
            }

            List<ClientCard> materials = this.CheckLinkMaterials(3, 2, false, extra_list);
            if (materials.Count < 2)
            {
                return empty_list;
            }

            // need KnightmareUnicorn?
            // for clear spells
            ClientCard flag = this.CheckProblematicCards(true, true);
            if (flag != null)
            {
                if (this.Bot.Hand.GetMatchingCardsCount(card => card != this.Card) == 0)
                {
                    return empty_list;
                }
                else
                {
                    return materials;
                }
            }
            // for higher attack
            int total_attack = 0;
            foreach(ClientCard card in materials)
            {
                total_attack += card.Attack;
            }
            if (total_attack >= 2200)
            {
                return empty_list;
            }

            return materials;
        }

        // summon process of KnightmareUnicorn
        public bool KnightmareUnicornSummon()
        {
            List<ClientCard> materials = this.KnightmareUnicornSummonCheck();
            if (materials.Count < 2)
            {
                return false;
            }

            this.AI.SelectMaterials(materials);
            return true;
        }

        // activate of KnightmareUnicorn
        public bool KnightmareUnicornActivate()
        {
            ClientCard card = this.CheckProblematicCards(true);
            if (card == null)
            {
                return false;
            }
            // avoid cards that cannot target.
            IList<ClientCard> enemy_list = new List<ClientCard>();
            if (!card.IsShouldNotBeMonsterTarget() && !card.IsShouldNotBeTarget())
            {
                enemy_list.Add(card);
            }

            foreach (ClientCard enemy in this.Enemy.GetMonstersInExtraZone())
            {
                if (enemy != null && !enemy_list.Contains(enemy) && !enemy.IsShouldNotBeMonsterTarget() && !enemy.IsShouldNotBeTarget())
                {
                    enemy_list.Add(enemy);
                }
            }
            foreach (ClientCard enemy in this.Enemy.GetMonstersInMainZone())
            {
                if (enemy != null && !enemy_list.Contains(enemy) && !enemy.IsShouldNotBeMonsterTarget() && !enemy.IsShouldNotBeTarget())
                {
                    enemy_list.Add(enemy);
                }
            }
            foreach (ClientCard enemy in this.Enemy.GetSpells())
            {
                if (enemy != null && !enemy_list.Contains(enemy) && !enemy.IsShouldNotBeMonsterTarget() && !enemy.IsShouldNotBeTarget())
                {
                    enemy_list.Add(enemy);
                }
            }
            if (enemy_list.Count > 0)
            {
                this.SelectDiscardSpell();
                this.AI.SelectNextCard(enemy_list);
                return true;
            }
            return false;
        }
        
        // check whether summon KnightmarePhoenix
        public List<ClientCard> KnightmarePhoenixSummonCheck(ClientCard included = null)
        {
            List<ClientCard> empty_list = new List<ClientCard>();
            List<ClientCard> extra_list = new List<ClientCard>();
            if (included != null)
            {
                extra_list.Add(included);
            }

            List<ClientCard> materials = this.CheckLinkMaterials(2, 2, true, extra_list);
            if (materials.Count < 2)
            {
                return empty_list;
            }

            // need KnightmarePhoenix?
            // for clear spells
            ClientCard flag = this.Util.GetProblematicEnemySpell();
            if (flag != null)
            {
                if (this.Bot.Hand.GetMatchingCardsCount(card => card != this.Card) == 0)
                {
                    return empty_list;
                } else
                {
                    return materials;
                }
            }
            // for higher attack
            if (materials[0].Attack + materials[1].Attack >= 1900)
            {
                return empty_list;
            }

            return materials;
        }

        // summon process of KnightmarePhoenix
        public bool KnightmarePhoenixSummon()
        {
            List<ClientCard> materials = this.KnightmarePhoenixSummonCheck();
            if (materials.Count < 2)
            {
                return false;
            }

            this.AI.SelectMaterials(materials);
            return true;
        }

        // activate of KnightmarePhoenix
        public bool KnightmarePhoenixActivate()
        {
            List<ClientCard> targets = new List<ClientCard>();
            targets.Add(this.Util.GetProblematicEnemySpell());
            List<ClientCard> spells = this.Enemy.GetSpells();
            List<ClientCard> faceups = new List<ClientCard>();
            List<ClientCard> facedowns = new List<ClientCard>();
            this.CardListShuffle(spells);
            foreach (ClientCard card in spells)
            {
                if (card.HasPosition(CardPosition.FaceUp) && !(card.IsShouldNotBeTarget() || card.IsShouldNotBeMonsterTarget()))
                {
                    faceups.Add(card);
                }
                else if (card.HasPosition(CardPosition.FaceDown))
                {
                    facedowns.Add(card);
                }
            }
            targets = targets.Union(faceups).Union(facedowns).ToList();
            if (targets.Count == 0)
            {
                return false;
            }

            this.SelectDiscardSpell();
            this.AI.SelectNextCard(targets);
            return true;
        }

        // check whether summon CrystronHalqifibrax
        public List<ClientCard> CrystronHalqifibraxSummonCheck(ClientCard included = null)
        {
            List<ClientCard> empty_list = new List<ClientCard>();
            List<ClientCard> extra_list = new List<ClientCard>();
            if (included != null)
            {
                extra_list.Add(included);
            }

            List<ClientCard> materials = this.CheckLinkMaterials(2, 2, true, extra_list);
            if (materials.Count < 2)
            {
                return empty_list;
            }

            // need CrystronHalqifibrax?
            if (this.CheckRemainInDeck(CardId.PSYGamma, CardId.AshBlossom_JoyousSpring) == 0)
            {
                return empty_list;
            }

            return empty_list;
        }

        // summon process of CrystronHalqifibrax
        public bool CrystronHalqifibraxSummon()
        {
            List<ClientCard> materials = this.CrystronHalqifibraxSummonCheck();
            if (materials.Count < 2)
            {
                return false;
            }

            this.AI.SelectMaterials(materials);
            return true;
        }

        // activate of CrystronHalqifibrax
        public bool CrystronHalqifibraxActivate()
        {
            if (this.Duel.Player == 0)
            {
                return true;
            }
            else if (this.Util.IsChainTarget(this.Card) || this.Util.GetProblematicEnemySpell() != null)
            {
                return true;
            }
            else if (this.Duel.Player == 1 && this.Duel.Phase == DuelPhase.BattleStart && this.Util.IsOneEnemyBetterThanValue(1500, true))
            {
                if (this.Util.IsOneEnemyBetterThanValue(1900, true))
                {
                    this.AI.SelectPosition(CardPosition.FaceUpDefence);
                }
                else
                {
                    this.AI.SelectPosition(CardPosition.FaceUpAttack);
                }
                return true;
            }
            return false;
        }

        // check whether summon SalamangreatAlmiraj
        public bool SalamangreatAlmirajSummonCheck(ClientCard included = null)
        {
            // use witchcraft first
            if (this.CheckDiscardableSpellCount() >= 2)
            {
                return false;
            }

            List<ClientCard> materials = this.Bot.GetMonsters();
            if (included != null)
            {
                materials.Add(included);
            }

            if (materials.GetCardCount(CardId.Pittore) + materials.GetCardCount(CardId.Genni) == 0)
            {
                return false;
            }

            if (this.Bot.HasInHand(this.important_witchcraft))
            {
                return true;
            }

            return false;
        }

        // summmon process of SalamangreatAlmiraj
        public bool SalamangreatAlmirajSummon()
        {
            if (!this.SalamangreatAlmirajSummonCheck())
            {
                return false;
            }

            List<int> material = new List<int> { CardId.Pittore, CardId.Genni };
            this.AI.SelectMaterials(material);
            return true;
        }

        // activate of SalamangreatAlmiraj
        public bool SalamangreatAlmirajActivate()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                return true;
            }

            if (this.Duel.Player == 1)
            {
                this.AI.SelectCard(this.Util.GetBestBotMonster());
                return true;
            }
            return false;
        }

        // summmon process of PSYLambda
        public bool PSYLambdaSummon()
        {
            if (this.Bot.HasInMonstersZone(CardId.PSYGamma) && this.Bot.HasInMonstersZone(CardId.PSYDriver))
            {
                if (this.Bot.HasInHand(CardId.PSYGamma) || this.Bot.HasInMonstersZone(CardId.PSYOmega)) {
                    List<int> targets = new List<int>{CardId.PSYDriver, CardId.PSYGamma};
                    this.AI.SelectMaterials(targets);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// return place to summon RelinquishedAnima.
        /// if no need to summon, return -1
        /// </summary>
        /// <param name="included">Cards included into account</param>
        public int RelinquishedAnimaSummonCheck(ClientCard included = null)
        {
            // use witchcraft first
            if (this.CheckDiscardableSpellCount() >= 2)
            {
                return -1;
            }

            List<ClientCard> materials = this.Bot.GetMonsters();
            if (included != null)
            {
                materials.Add(included);
            }

            int place = -1;
            int attack = this.Util.GetBestAttack(this.Bot);
            // select place

            List<ClientCard> checklist = new List<ClientCard> { this.Enemy.MonsterZone[6], this.Enemy.MonsterZone[5] };
            List<int> placelist = new List<int> { 1, 3 };
            for (int i = 0; i < 2; ++i)
            {
                ClientCard card = checklist[i];
                int _place = placelist[i];
                if (card != null && card.HasLinkMarker((int)CardLinkMarker.Top) && card.Attack > attack &&
                    !card.IsShouldNotBeMonsterTarget() && !card.IsShouldNotBeTarget())
                {
                    ClientCard self_card = this.Bot.MonsterZone[_place];
                    if (self_card == null || self_card.Level == 1)
                    {
                        place = _place;
                        attack = card.Attack;
                    }
                }
            }
            checklist = new List<ClientCard> { this.Enemy.MonsterZone[3], this.Enemy.MonsterZone[1] };
            placelist = new List<int> { 5, 6 };
            for (int i = 0; i < 2; ++i)
            {
                ClientCard card = checklist[i];
                int _place = placelist[i];
                if (card != null && card.Attack > attack &&
                    !card.IsShouldNotBeMonsterTarget() && !card.IsShouldNotBeTarget())
                {
                    ClientCard enemy_card = this.Enemy.MonsterZone[11 - _place];
                    if (enemy_card != null)
                    {
                        continue;
                    }

                    ClientCard self_card = this.Bot.MonsterZone[_place];
                    if (self_card == null || self_card.Level == 1)
                    {
                        place = _place;
                        attack = card.Attack;
                    }
                }
            }

            return place;
        }

        // summmon process of RelinquishedAnima
        public bool RelinquishedAnimaSummon()
        {
            int place = this.RelinquishedAnimaSummonCheck();
            Logger.DebugWriteLine("RelinquishedAnima summon check: " + place.ToString());
            if (place != -1)
            {
                int zone = (int)System.Math.Pow(2, place);
                this.AI.SelectPlace(zone);
                if (this.Bot.MonsterZone[place] != null && this.Bot.MonsterZone[place].Level == 1)
                {
                    this.AI.SelectMaterials(this.Bot.MonsterZone[place]);
                } else
                {
                    this.AI.SelectMaterials(CardId.Genni);
                }
                return true;
            }
            return false;
        }

        // default Chicken game
        public bool ChickenGame()
        {
            if (this.SpellNegatable())
            {
                return false;
            }

            if (this.Bot.LifePoints <= 1000)
            {
                return false;
            }

            if (this.Bot.LifePoints - 1000 <= this.Enemy.LifePoints && this.ActivateDescription == this.Util.GetStringId(DefaultExecutor.CardId.ChickenGame, 0))
            {
                return true;
            }
            if (this.Bot.LifePoints - 1000 > this.Enemy.LifePoints && this.ActivateDescription == this.Util.GetStringId(DefaultExecutor.CardId.ChickenGame, 1))
            {
                return true;
            }
            return false;
        }
    }
}