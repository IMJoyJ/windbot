using System.Linq;
using System.Collections.Generic;
using WindBot.Game.AI;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game
{
    public class GameAI
    {
        public GameClient Game { get; private set; }
        public Duel Duel { get; private set; }
        public Executor Executor { get; set; }

        // record activated count to prevent infinite actions
        private Dictionary<int, int> activatedCards;

        private readonly Dialogs dialogs;

        public GameAI(GameClient game, Duel duel)
        {
            this.Game = game;
            this.Duel = duel;

            this.dialogs = new Dialogs(game);
            activatedCards = new Dictionary<int, int>();
        }

        /// <summary>
        /// Called when the AI got the error message.
        /// </summary>
        public void OnRetry()
        {
            this.dialogs.SendSorry();
        }

        public void OnDeckError(string card)
        {
            this.dialogs.SendDeckSorry(card);
        }

        /// <summary>
        /// Called when the AI join the game.
        /// </summary>
        public void OnJoinGame()
        {
            this.dialogs.SendWelcome();
        }

        /// <summary>
        /// Called when the duel starts.
        /// </summary>
        public void OnStart()
        {
            this.dialogs.SendDuelStart();
        }

        /// <summary>
        /// Called when the AI do the rock-paper-scissors.
        /// </summary>
        /// <returns>1 for Scissors, 2 for Rock, 3 for Paper.</returns>
        public int OnRockPaperScissors()
        {
            return this.Executor.OnRockPaperScissors();
        }

        /// <summary>
        /// Called when the AI won the rock-paper-scissors.
        /// </summary>
        /// <returns>True if the AI should begin first, false otherwise.</returns>
        public bool OnSelectHand()
        {
            return this.Executor.OnSelectHand();
        }

        /// <summary>
        /// Called when any player draw card.
        /// </summary>
        public void OnDraw(int player)
        {
            this.Executor.OnDraw(player);
        }

        /// <summary>
        /// Called when it's a new turn.
        /// </summary>
        public void OnNewTurn()
        {
            activatedCards.Clear();
            this.Executor.OnNewTurn();
        }

        /// <summary>
        /// Called when it's a new phase.
        /// </summary>
        public void OnNewPhase()
        {
            this.selector.Clear();
            this.position.Clear();
            this.selector_pointer = -1;
            this.materialSelector = null;
            this.option = -1;
            this.yesno = -1;
            this.announce = 0;

            this.place = 0;
            if (this.Duel.Player == 0 && this.Duel.Phase == DuelPhase.Draw)
            {
                this.dialogs.SendNewTurn();
            }
            this.Executor.OnNewPhase();
        }

        /// <summary>
        /// Called when the AI got attack directly.
        /// </summary>
        public void OnDirectAttack(ClientCard card)
        {
            this.dialogs.SendOnDirectAttack(card.Name);
        }

        public Dictionary<int, int> EffectsUsedInTurnEnemy = new Dictionary<int, int>();
        public Dictionary<int, int> EffectsUsedInTurnSelf = new Dictionary<int, int>();
        /// <summary>
        /// Called when a chain is executed.
        /// </summary>
        /// <param name="card">Card who is chained.</param>
        /// <param name="player">Player who is currently chaining.</param>
        public void OnChaining(ClientCard card, int player)
        {
            if (!EffectsUsedInTurnEnemy.ContainsKey(card.Id))
            {
                EffectsUsedInTurnEnemy.Add(card.Id, 0);
            }
            if (!EffectsUsedInTurnSelf.ContainsKey(card.Id))
            {
                EffectsUsedInTurnSelf.Add(card.Id, 0);
            }
            if (card.Controller == Duel.Player)
            {
                EffectsUsedInTurnEnemy[card.Id] = this.Duel.Turn;
            }
            else
            {
                EffectsUsedInTurnSelf[card.Id] = this.Duel.Turn;
            }
            this.Executor.OnChaining(player,card);
        }
        
        /// <summary>
        /// Called when a chain has been solved.
        /// </summary>
        public void OnChainEnd()
        {
            this.selector.Clear();
            this.selector_pointer = -1;
            this.Executor.OnChainEnd();
        }

        /// <summary>
        /// Called when the AI has to do something during the battle phase.
        /// </summary>
        /// <param name="battle">Informations about usable cards.</param>
        /// <returns>A new BattlePhaseAction containing the action to do.</returns>
        public BattlePhaseAction OnSelectBattleCmd(BattlePhase battle)
        {
            this.Executor.SetBattle(battle);
            foreach (CardExecutor exec in this.Executor.Executors)
            {
                if (exec.Type == ExecutorType.GoToMainPhase2 && battle.CanMainPhaseTwo && exec.Func()) // check if should enter main phase 2 directly
                {
                    return this.ToMainPhase2();
                }
                if (exec.Type == ExecutorType.GoToEndPhase && battle.CanEndPhase && exec.Func()) // check if should enter end phase directly
                {
                    return this.ToEndPhase();
                }
                for (int i = 0; i < battle.ActivableCards.Count; ++i)
                {
                    ClientCard card = battle.ActivableCards[i];
                    if (this.ShouldExecute(exec, card, ExecutorType.Activate, battle.ActivableDescs[i]))
                    {
                        this.dialogs.SendChaining(card.Name);
                        return new BattlePhaseAction(BattlePhaseAction.BattleAction.Activate, card.ActionIndex);
                    }
                }
            }

            // Sort the attackers and defenders, make monster with higher attack go first.
            List<ClientCard> attackers = new List<ClientCard>(battle.AttackableCards);
            attackers.Sort(CardContainer.CompareCardAttack);
            attackers.Reverse();

            List<ClientCard> defenders = new List<ClientCard>(this.Duel.Fields[1].GetMonsters());
            defenders.Sort(CardContainer.CompareDefensePower);
            defenders.Reverse();

            // Let executor decide which card should attack first.
            ClientCard selected = this.Executor.OnSelectAttacker(attackers, defenders);
            if (selected != null && attackers.Contains(selected))
            {
                attackers.Remove(selected);
                attackers.Insert(0, selected);
            }

            // Check for the executor.
            BattlePhaseAction result = this.Executor.OnBattle(attackers, defenders);
            if (result != null)
            {
                return result;
            }

            if (attackers.Count == 0)
            {
                return this.ToMainPhase2();
            }

            if (defenders.Count == 0)
            {
                // Attack with the monster with the lowest attack first
                for (int i = attackers.Count - 1; i >= 0; --i)
                {
                    ClientCard attacker = attackers[i];
                    if (attacker.Attack > 0)
                    {
                        return this.Attack(attacker, null);
                    }
                }
            }
            else
            {
                for (int k = 0; k < attackers.Count; ++k)
                {
                    ClientCard attacker = attackers[k];
                    attacker.IsLastAttacker = (k == attackers.Count - 1);
                    result = this.Executor.OnSelectAttackTarget(attacker, defenders);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            if (!battle.CanMainPhaseTwo)
            {
                return this.Attack(attackers[0], (defenders.Count == 0) ? null : defenders[0]);
            }

            return this.ToMainPhase2();
        }

        /// <summary>
        /// Called when the AI has to select one or more cards.
        /// </summary>
        /// <param name="cards">List of available cards.</param>
        /// <param name="min">Minimal quantity.</param>
        /// <param name="max">Maximal quantity.</param>
        /// <param name="hint">The hint message of the select.</param>
        /// <param name="cancelable">True if you can return an empty list.</param>
        /// <returns>A new list containing the selected cards.</returns>
        public IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
        {
            const int HINTMSG_FMATERIAL = 511;
            const int HINTMSG_SMATERIAL = 512;
            const int HINTMSG_XMATERIAL = 513;
            const int HINTMSG_LMATERIAL = 533;
            const int HINTMSG_SPSUMMON = 509;

            // Check for the executor.
            IList<ClientCard> result = this.Executor.OnSelectCard(cards, min, max, hint, cancelable);
            if (result != null)
            {
                return result;
            }

            if (hint == HINTMSG_SPSUMMON && min == 1 && max > min) // pendulum summon
            {
                result = this.Executor.OnSelectPendulumSummon(cards, max);
                if (result != null)
                {
                    return result;
                }
            }

            CardSelector selector = null;
            if (hint == HINTMSG_FMATERIAL || hint == HINTMSG_SMATERIAL || hint == HINTMSG_XMATERIAL || hint == HINTMSG_LMATERIAL)
            {
                if (this.materialSelector != null)
                {
                    //Logger.DebugWriteLine("m_materialSelector");
                    selector = this.materialSelector;
                }
                else
                {
                    if (hint == HINTMSG_FMATERIAL)
                    {
                        result = this.Executor.OnSelectFusionMaterial(cards, min, max);
                    }

                    if (hint == HINTMSG_SMATERIAL)
                    {
                        result = this.Executor.OnSelectSynchroMaterial(cards, 0, min, max);
                    }

                    if (hint == HINTMSG_XMATERIAL)
                    {
                        result = this.Executor.OnSelectXyzMaterial(cards, min, max);
                    }

                    if (hint == HINTMSG_LMATERIAL)
                    {
                        result = this.Executor.OnSelectLinkMaterial(cards, min, max);
                    }

                    if (result != null)
                    {
                        return result;
                    }

                    // Update the next selector.
                    selector = this.GetSelectedCards();
                }
            }
            else
            {
                // Update the next selector.
                selector = this.GetSelectedCards();
            }

            // If we selected a card, use this card.
            if (selector != null)
            {
                return selector.Select(cards, min, max);
            }

            // Always select the first available cards and choose the minimum.
            IList<ClientCard> selected = new List<ClientCard>();

            if (cards.Count >= min)
            {
                for (int i = 0; i < min; ++i)
                {
                    selected.Add(cards[i]);
                }
            }
            return selected;
        }

        /// <summary>
        /// Called when the AI can chain (activate) a card.
        /// </summary>
        /// <param name="cards">List of activable cards.</param>
        /// <param name="descs">List of effect descriptions.</param>
        /// <param name="forced">You can't return -1 if this param is true.</param>
        /// <returns>Index of the activated card or -1.</returns>
        public int OnSelectChain(IList<ClientCard> cards, IList<int> descs, bool forced)
        {
            foreach (CardExecutor exec in this.Executor.Executors)
            {
                for (int i = 0; i < cards.Count; ++i)
                {
                    ClientCard card = cards[i];
                    if (this.ShouldExecute(exec, card, ExecutorType.Activate, descs[i]))
                    {
                        this.dialogs.SendChaining(card.Name);
                        return i;
                    }
                }
            }
            // If we're forced to chain, we chain the first card. However don't do anything.
            return forced ? 0 : -1;
        }
        
        /// <summary>
        /// Called when the AI has to use one or more counters.
        /// </summary>
        /// <param name="type">Type of counter to use.</param>
        /// <param name="quantity">Quantity of counter to select.</param>
        /// <param name="cards">List of available cards.</param>
        /// <param name="counters">List of available counters.</param>
        /// <returns>List of used counters.</returns>
        public IList<int> OnSelectCounter(int type, int quantity, IList<ClientCard> cards, IList<int> counters)
        {
            // Always select the first available counters.
            int[] used = new int[counters.Count];
            int i = 0;
            while (quantity > 0)
            {
                if (counters[i] >= quantity)
                {
                    used[i] = quantity;
                    quantity = 0;
                }
                else
                {
                    used[i] = counters[i];
                    quantity -= counters[i];
                }
                i++;
            }
            return used;
        }

        /// <summary>
        /// Called when the AI has to sort cards.
        /// </summary>
        /// <param name="cards">Cards to sort.</param>
        /// <returns>List of sorted cards.</returns>
        public IList<ClientCard> OnCardSorting(IList<ClientCard> cards)
        {

            IList<ClientCard> result = this.Executor.OnCardSorting(cards);
            if (result != null)
            {
                return result;
            }

            result = new List<ClientCard>();
            // TODO: use selector
            result = cards.ToList();
            return result;
        }

        /// <summary>
        /// Called when the AI has to choose to activate or not an effect.
        /// </summary>
        /// <param name="card">Card to activate.</param>
        /// <returns>True for yes, false for no.</returns>
        public bool OnSelectEffectYn(ClientCard card, int desc)
        {
            foreach (CardExecutor exec in this.Executor.Executors)
            {
                if (this.ShouldExecute(exec, card, ExecutorType.Activate, desc))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Called when the AI has to do something during the main phase.
        /// </summary>
        /// <param name="main">A lot of informations about the available actions.</param>
        /// <returns>A new MainPhaseAction containing the action to do.</returns>
        public MainPhaseAction OnSelectIdleCmd(MainPhase main)
        {
            this.Executor.SetMain(main);
            foreach (CardExecutor exec in this.Executor.Executors)
            {
            	if (exec.Type == ExecutorType.GoToEndPhase && main.CanEndPhase && exec.Func()) // check if should enter end phase directly
                {
                    this.dialogs.SendEndTurn();
                    return new MainPhaseAction(MainPhaseAction.MainAction.ToEndPhase);
                }
                if (exec.Type==ExecutorType.GoToBattlePhase && main.CanBattlePhase && exec.Func()) // check if should enter battle phase directly
                {
                    return new MainPhaseAction(MainPhaseAction.MainAction.ToBattlePhase);
                }
                // NOTICE: GoToBattlePhase and GoToEndPhase has no "card" can be accessed to ShouldExecute(), so instead use exec.Func() to check ...
                // enter end phase and enter battle pahse is in higher priority. 

                for (int i = 0; i < main.ActivableCards.Count; ++i)
                {
                    ClientCard card = main.ActivableCards[i];
                    if (this.ShouldExecute(exec, card, ExecutorType.Activate, main.ActivableDescs[i]))
                    {
                        this.dialogs.SendActivate(card.Name);
                        return new MainPhaseAction(MainPhaseAction.MainAction.Activate, card.ActionActivateIndex[main.ActivableDescs[i]]);
                    }
                }
                foreach (ClientCard card in main.MonsterSetableCards)
                {
                    if (this.ShouldExecute(exec, card, ExecutorType.MonsterSet))
                    {
                        this.dialogs.SendSetMonster();
                        return new MainPhaseAction(MainPhaseAction.MainAction.SetMonster, card.ActionIndex);
                    }
                }
                foreach (ClientCard card in main.ReposableCards)
                {
                    if (this.ShouldExecute(exec, card, ExecutorType.Repos))
                    {
                        return new MainPhaseAction(MainPhaseAction.MainAction.Repos, card.ActionIndex);
                    }
                }
                foreach (ClientCard card in main.SpecialSummonableCards)
                {
                    if (this.ShouldExecute(exec, card, ExecutorType.SpSummon))
                    {
                        this.dialogs.SendSummon(card.Name);
                        return new MainPhaseAction(MainPhaseAction.MainAction.SpSummon, card.ActionIndex);
                    }
                }
                foreach (ClientCard card in main.SummonableCards)
                {
                    if (this.ShouldExecute(exec, card, ExecutorType.Summon))
                    {
                        this.dialogs.SendSummon(card.Name);
                        return new MainPhaseAction(MainPhaseAction.MainAction.Summon, card.ActionIndex);
                    }
                    if (this.ShouldExecute(exec, card, ExecutorType.SummonOrSet))
                    {
                        if (this.Executor.Util.IsAllEnemyBetter(true) && this.Executor.Util.IsAllEnemyBetterThanValue(card.Attack + 300, false) &&
                            main.MonsterSetableCards.Contains(card))
                        {
                            this.dialogs.SendSetMonster();
                            return new MainPhaseAction(MainPhaseAction.MainAction.SetMonster, card.ActionIndex);
                        }
                        this.dialogs.SendSummon(card.Name);
                        return new MainPhaseAction(MainPhaseAction.MainAction.Summon, card.ActionIndex);
                    }
                }                
                foreach (ClientCard card in main.SpellSetableCards)
                {
                    if (this.ShouldExecute(exec, card, ExecutorType.SpellSet))
                    {
                        return new MainPhaseAction(MainPhaseAction.MainAction.SetSpell, card.ActionIndex);
                    }
                }
            }

            if (main.CanBattlePhase && this.Duel.Fields[0].HasAttackingMonster())
            {
                return new MainPhaseAction(MainPhaseAction.MainAction.ToBattlePhase);
            }

            this.dialogs.SendEndTurn();
            return new MainPhaseAction(MainPhaseAction.MainAction.ToEndPhase); 
        }

        /// <summary>
        /// Called when the AI has to select an option.
        /// </summary>
        /// <param name="options">List of available options.</param>
        /// <returns>Index of the selected option.</returns>
        public int OnSelectOption(IList<int> options)
        {
            if (this.option != -1 && this.option < options.Count)
            {
                return this.option;
            }

            int result = this.Executor.OnSelectOption(options);
            if (result != -1)
            {
                return result;
            }

            return 0; // Always select the first option.
        }

        public int OnSelectPlace(int cardId, int player, CardLocation location, int available)
        {
            int selector_selected = this.place;
            this.place = 0;

            int executor_selected = this.Executor.OnSelectPlace(cardId, player, location, available);

            if ((executor_selected & available) > 0)
            {
                return executor_selected & available;
            }

            if ((selector_selected & available) > 0)
            {
                return selector_selected & available;
            }

            // TODO: LinkedZones

            return 0;
        }

        /// <summary>
        /// Called when the AI has to select a card position.
        /// </summary>
        /// <param name="cardId">Id of the card to position on the field.</param>
        /// <param name="positions">List of available positions.</param>
        /// <returns>Selected position.</returns>
        public CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
        {
            CardPosition selector_selected = this.GetSelectedPosition();

            CardPosition executor_selected = this.Executor.OnSelectPosition(cardId, positions);

            // Selects the selected position if available, the first available otherwise.
            if (positions.Contains(executor_selected))
            {
                return executor_selected;
            }
            else
            {
                CardPosition secondSelect = CardPosition.FaceUpAttack;
                if (executor_selected == CardPosition.Defence)
                {
                    secondSelect = CardPosition.FaceUpDefence;
                }
                if (executor_selected == CardPosition.FaceUpDefence)
                {
                    secondSelect = CardPosition.FaceDownDefence;
                }
                if (executor_selected == CardPosition.FaceDownDefence)
                {
                    secondSelect = CardPosition.FaceUpDefence;
                }
                if (executor_selected == CardPosition.FaceUpAttack)
                {
                    secondSelect = CardPosition.FaceDownAttack;
                }
                if (positions.Contains(secondSelect))
                {
                    return secondSelect;
                }
            }

            if (positions.Contains(selector_selected))
            {
                return selector_selected;
            }

            return positions[0];
        }

        /// <summary>
        /// Called when the AI has to tribute for a synchro monster or ritual monster.
        /// </summary>
        /// <param name="cards">Available cards.</param>
        /// <param name="sum">Result of the operation.</param>
        /// <param name="min">Minimum cards.</param>
        /// <param name="max">Maximum cards.</param>
        /// <param name="mode">True for exact equal.</param>
        /// <returns></returns>
        public IList<ClientCard> OnSelectSum(IList<ClientCard> cards, int sum, int min, int max, int hint, bool mode)
        {
            const int HINTMSG_RELEASE = 500;
            const int HINTMSG_SMATERIAL = 512;

            IList<ClientCard> selected = this.Executor.OnSelectSum(cards, sum, min, max, hint, mode);
            if (selected != null)
            {
                return selected;
            }

            if (hint == HINTMSG_RELEASE || hint == HINTMSG_SMATERIAL)
            {
                if (this.materialSelector != null)
                {
                    selected = this.materialSelector.Select(cards, min, max);
                }
                else
                {
                    switch (hint)
                    {
                        case HINTMSG_SMATERIAL:
                            selected = this.Executor.OnSelectSynchroMaterial(cards, sum, min, max);
                            break;
                        case HINTMSG_RELEASE:
                            selected = this.Executor.OnSelectRitualTribute(cards, sum, min, max);
                            break;
                    }
                }
                if (selected != null)
                {
                    int s1 = 0, s2 = 0;
                    foreach (ClientCard card in selected)
                    {
                        s1 += card.OpParam1;
                        s2 += (card.OpParam2 != 0) ? card.OpParam2 : card.OpParam1;
                    }
                    if ((mode && (s1 == sum || s2 == sum)) || (!mode && (s1 >= sum || s2 >= sum)))
                    {
                        return selected;
                    }
                }
            }

            if (mode)
            {
                // equal

                if (sum == 0 && min == 0)
                {
                    return new List<ClientCard>();
                }

                if (min <= 1)
                {
                    // try special level first
                    foreach (ClientCard card in cards)
                    {
                        if (card.OpParam2 == sum)
                        {
                            return new[] { card };
                        }
                    }
                    // try level equal
                    foreach (ClientCard card in cards)
                    {
                        if (card.OpParam1 == sum)
                        {
                            return new[] { card };
                        }
                    }
                }

                // try all
                int s1 = 0, s2 = 0;
                foreach (ClientCard card in cards)
                {
                    s1 += card.OpParam1;
                    s2 += (card.OpParam2 != 0) ? card.OpParam2 : card.OpParam1;
                }
                if (s1 == sum || s2 == sum)
                {
                    return cards;
                }

                // try all combinations
                int i = (min <= 1) ? 2 : min;
                while (i <= max && i <= cards.Count)
                {
                    IEnumerable<IEnumerable<ClientCard>> combos = CardContainer.GetCombinations(cards, i);

                    foreach (IEnumerable<ClientCard> combo in combos)
                    {
                        Logger.DebugWriteLine("--");
                        s1 = 0;
                        s2 = 0;
                        foreach (ClientCard card in combo)
                        {
                            s1 += card.OpParam1;
                            s2 += (card.OpParam2 != 0) ? card.OpParam2 : card.OpParam1;
                        }
                        if (s1 == sum || s2 == sum)
                        {
                            return combo.ToList();
                        }
                    }
                    i++;
                }
            }
            else
            {
                // larger
                if (min <= 1)
                {
                    // try special level first
                    foreach (ClientCard card in cards)
                    {
                        if (card.OpParam2 >= sum)
                        {
                            return new[] { card };
                        }
                    }
                    // try level equal
                    foreach (ClientCard card in cards)
                    {
                        if (card.OpParam1 >= sum)
                        {
                            return new[] { card };
                        }
                    }
                }

                // try all combinations
                int i = (min <= 1) ? 2 : min;
                while (i <= max && i <= cards.Count)
                {
                    IEnumerable<IEnumerable<ClientCard>> combos = CardContainer.GetCombinations(cards, i);

                    foreach (IEnumerable<ClientCard> combo in combos)
                    {
                        Logger.DebugWriteLine("----");
                        int s1 = 0, s2 = 0;
                        foreach (ClientCard card in combo)
                        {
                            s1 += card.OpParam1;
                            s2 += (card.OpParam2 != 0) ? card.OpParam2 : card.OpParam1;
                        }
                        if (s1 >= sum || s2 >= sum)
                        {
                            return combo.ToList();
                        }
                    }
                    i++;
                }
            }

            Logger.WriteErrorLine("Fail to select sum.");
            return new List<ClientCard>();
        }

        /// <summary>
        /// Called when the AI has to tribute one or more cards.
        /// </summary>
        /// <param name="cards">List of available cards.</param>
        /// <param name="min">Minimal quantity.</param>
        /// <param name="max">Maximal quantity.</param>
        /// <param name="hint">The hint message of the select.</param>
        /// <param name="cancelable">True if you can return an empty list.</param>
        /// <returns>A new list containing the tributed cards.</returns>
        public IList<ClientCard> OnSelectTribute(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
        {
            // Always choose the minimum and lowest atk.
            List<ClientCard> sorted = new List<ClientCard>();
            sorted.AddRange(cards);
            sorted.Sort(CardContainer.CompareCardAttack);

            IList<ClientCard> selected = new List<ClientCard>();

            for (int i = 0; i < min && i < sorted.Count; ++i)
            {
                selected.Add(sorted[i]);
            }

            return selected;
        }

        /// <summary>
        /// Called when the AI has to select yes or no.
        /// </summary>
        /// <param name="desc">Id of the question.</param>
        /// <returns>True for yes, false for no.</returns>
        public bool OnSelectYesNo(int desc)
        {
            if (this.yesno != -1)
            {
                return this.yesno > 0;
            }

            return this.Executor.OnSelectYesNo(desc);
        }

        /// <summary>
        /// Called when the AI has to select if to continue attacking when replay.
        /// </summary>
        /// <returns>True for yes, false for no.</returns>
        public bool OnSelectBattleReplay()
        {
            return this.Executor.OnSelectBattleReplay();
        }

        /// <summary>
        /// Called when the AI has to declare a card.
        /// </summary>
        /// <param name="avail">Available card's ids.</param>
        /// <returns>Id of the selected card.</returns>
        public int OnAnnounceCard(IList<int> avail)
        {
            int selected = this.Executor.OnAnnounceCard(avail);
            if (avail.Contains(selected))
            {
                return selected;
            }

            if (avail.Contains(this.announce))
            {
                return this.announce;
            }
            else if (this.announce > 0)
            {
                Logger.WriteErrorLine("Pre-announced card cant be used: " + this.announce);
            }

            return avail[0];
        }

        // _ Others functions _
        // Those functions are used by the AI behavior.

        
        private CardSelector materialSelector;
        private int place;
        private int option;
        private int number;
        private int announce;
        private int yesno;
        private readonly IList<CardAttribute> attributes = new List<CardAttribute>();
        private readonly IList<CardSelector> selector = new List<CardSelector>();
        private readonly IList<CardPosition> position = new List<CardPosition>();
        private int selector_pointer = -1;
        private readonly IList<CardRace> races = new List<CardRace>();

        public void SelectCard(ClientCard card)
        {
            this.selector_pointer = this.selector.Count();
            this.selector.Add(new CardSelector(card));
        }

        public void SelectCard(IList<ClientCard> cards)
        {
            this.selector_pointer = this.selector.Count();
            this.selector.Add(new CardSelector(cards));
        }

        public void SelectCard(int cardId)
        {
            this.selector_pointer = this.selector.Count();
            this.selector.Add(new CardSelector(cardId));
        }

        public void SelectCard(IList<int> ids)
        {
            this.selector_pointer = this.selector.Count();
            this.selector.Add(new CardSelector(ids));
        }

        public void SelectCard(params int[] ids)
        {
            this.selector_pointer = this.selector.Count();
            this.selector.Add(new CardSelector(ids));
        }

        public void SelectCard(CardLocation loc)
        {
            this.selector_pointer = this.selector.Count();
            this.selector.Add(new CardSelector(loc));
        }

        public void SelectNextCard(ClientCard card)
        {
            if (this.selector_pointer == -1)
            {
                Logger.WriteErrorLine("Error: Call SelectNextCard() before SelectCard()");
                this.selector_pointer = 0;
            }
            this.selector.Insert(this.selector_pointer, new CardSelector(card));
        }

        public void SelectNextCard(IList<ClientCard> cards)
        {
            if (this.selector_pointer == -1)
            {
                Logger.WriteErrorLine("Error: Call SelectNextCard() before SelectCard()");
                this.selector_pointer = 0;
            }
            this.selector.Insert(this.selector_pointer, new CardSelector(cards));
        }

        public void SelectNextCard(int cardId)
        {
            if (this.selector_pointer == -1)
            {
                Logger.WriteErrorLine("Error: Call SelectNextCard() before SelectCard()");
                this.selector_pointer = 0;
            }
            this.selector.Insert(this.selector_pointer, new CardSelector(cardId));
        }

        public void SelectNextCard(IList<int> ids)
        {
            if (this.selector_pointer == -1)
            {
                Logger.WriteErrorLine("Error: Call SelectNextCard() before SelectCard()");
                this.selector_pointer = 0;
            }
            this.selector.Insert(this.selector_pointer, new CardSelector(ids));
        }

        public void SelectNextCard(params int[] ids)
        {
            if (this.selector_pointer == -1)
            {
                Logger.WriteErrorLine("Error: Call SelectNextCard() before SelectCard()");
                this.selector_pointer = 0;
            }
            this.selector.Insert(this.selector_pointer, new CardSelector(ids));
        }

        public void SelectNextCard(CardLocation loc)
        {
            if (this.selector_pointer == -1)
            {
                Logger.WriteErrorLine("Error: Call SelectNextCard() before SelectCard()");
                this.selector_pointer = 0;
            }
            this.selector.Insert(this.selector_pointer, new CardSelector(loc));
        }

        public void SelectThirdCard(ClientCard card)
        {
            if (this.selector_pointer == -1)
            {
                Logger.WriteErrorLine("Error: Call SelectThirdCard() before SelectCard()");
                this.selector_pointer = 0;
            }
            this.selector.Insert(this.selector_pointer, new CardSelector(card));
        }

        public void SelectThirdCard(IList<ClientCard> cards)
        {
            if (this.selector_pointer == -1)
            {
                Logger.WriteErrorLine("Error: Call SelectThirdCard() before SelectCard()");
                this.selector_pointer = 0;
            }
            this.selector.Insert(this.selector_pointer, new CardSelector(cards));
        }

        public void SelectThirdCard(int cardId)
        {
            if (this.selector_pointer == -1)
            {
                Logger.WriteErrorLine("Error: Call SelectThirdCard() before SelectCard()");
                this.selector_pointer = 0;
            }
            this.selector.Insert(this.selector_pointer, new CardSelector(cardId));
        }

        public void SelectThirdCard(IList<int> ids)
        {
            if (this.selector_pointer == -1)
            {
                Logger.WriteErrorLine("Error: Call SelectThirdCard() before SelectCard()");
                this.selector_pointer = 0;
            }
            this.selector.Insert(this.selector_pointer, new CardSelector(ids));
        }

        public void SelectThirdCard(params int[] ids)
        {
            if (this.selector_pointer == -1)
            {
                Logger.WriteErrorLine("Error: Call SelectThirdCard() before SelectCard()");
                this.selector_pointer = 0;
            }
            this.selector.Insert(this.selector_pointer, new CardSelector(ids));
        }

        public void SelectThirdCard(CardLocation loc)
        {
            if (this.selector_pointer == -1)
            {
                Logger.WriteErrorLine("Error: Call SelectThirdCard() before SelectCard()");
                this.selector_pointer = 0;
            }
            this.selector.Insert(this.selector_pointer, new CardSelector(loc));
        }

        public void SelectMaterials(ClientCard card)
        {
            this.materialSelector = new CardSelector(card);
        }

        public void SelectMaterials(IList<ClientCard> cards)
        {
            this.materialSelector = new CardSelector(cards);
        }

        public void SelectMaterials(int cardId)
        {
            this.materialSelector = new CardSelector(cardId);
        }

        public void SelectMaterials(IList<int> ids)
        {
            this.materialSelector = new CardSelector(ids);
        }

        public void SelectMaterials(CardLocation loc)
        {
            this.materialSelector = new CardSelector(loc);
        }

        public void CleanSelectMaterials()
        {
            this.materialSelector = null;
        }

        public bool HaveSelectedCards()
        {
            return this.selector.Count > 0 || this.materialSelector != null;
        }

        public CardSelector GetSelectedCards()
        {
            CardSelector selected = null;
            if (this.selector.Count > 0)
            {
                selected = this.selector[this.selector.Count - 1];
                this.selector.RemoveAt(this.selector.Count - 1);
            }
            return selected;
        }

        public CardPosition GetSelectedPosition()
        {
            CardPosition selected = CardPosition.FaceUpAttack;
            if (this.position.Count > 0)
            {
                selected = this.position[0];
                this.position.RemoveAt(0);
            }
            return selected;
        }

        public void SelectPosition(CardPosition pos)
        {
            this.position.Add(pos);
        }

        public void SelectPlace(int zones)
        {
            this.place = zones;
        }

        public void SelectOption(int opt)
        {
            this.option = opt;
        }

        public void SelectNumber(int number)
        {
            this.number = number;
        }

        public void SelectAttribute(CardAttribute attribute)
        {
            this.attributes.Clear();
            this.attributes.Add(attribute);
        }

        public void SelectAttributes(CardAttribute[] attributes)
        {
            this.attributes.Clear();
            foreach (CardAttribute attribute in attributes)
            {
                this.attributes.Add(attribute);
            }
        }

        public void SelectRace(CardRace race)
        {
            this.races.Clear();
            this.races.Add(race);
        }

        public void SelectRaces(CardRace[] races)
        {
            this.races.Clear();
            foreach (CardRace race in races)
            {
                this.races.Add(race);
            }
        }

        public void SelectAnnounceID(int id)
        {
            this.announce = id;
        }

        public void SelectYesNo(bool opt)
        {
            this.yesno = opt ? 1 : 0;
        }

        /// <summary>
        /// Called when the AI has to declare a number.
        /// </summary>
        /// <param name="numbers">List of available numbers.</param>
        /// <returns>Index of the selected number.</returns>
        public int OnAnnounceNumber(IList<int> numbers)
        {
            if (numbers.Contains(this.number))
            {
                return numbers.IndexOf(this.number);
            }

            return Program._rand.Next(0, numbers.Count); // Returns a random number.
        }

        /// <summary>
        /// Called when the AI has to declare one or more attributes.
        /// </summary>
        /// <param name="count">Quantity of attributes to declare.</param>
        /// <param name="attributes">List of available attributes.</param>
        /// <returns>A list of the selected attributes.</returns>
        public virtual IList<CardAttribute> OnAnnounceAttrib(int count, IList<CardAttribute> attributes)
        {
            IList<CardAttribute> foundAttributes = this.attributes.Where(attributes.Contains).ToList();
            if (foundAttributes.Count > 0)
            {
                return foundAttributes;
            }

            return attributes; // Returns the first available Attribute.
        }

        /// <summary>
        /// Called when the AI has to declare one or more races.
        /// </summary>
        /// <param name="count">Quantity of races to declare.</param>
        /// <param name="races">List of available races.</param>
        /// <returns>A list of the selected races.</returns>
        public virtual IList<CardRace> OnAnnounceRace(int count, IList<CardRace> races)
        {
            IList<CardRace> foundRaces = this.races.Where(races.Contains).ToList();
            if (foundRaces.Count > 0)
            {
                return foundRaces;
            }

            return races; // Returns the first available Races.
        }

        public BattlePhaseAction Attack(ClientCard attacker, ClientCard defender)
        {
            this.Executor.SetCard(0, attacker, -1);
            if (defender != null)
            {
                string cardName = defender.Name ?? "monster";
                attacker.ShouldDirectAttack = false;
                this.dialogs.SendAttack(attacker.Name, cardName);
                this.SelectCard(defender);
            }
            else
            {
                attacker.ShouldDirectAttack = true;
                this.dialogs.SendDirectAttack(attacker.Name);
            }
            return new BattlePhaseAction(BattlePhaseAction.BattleAction.Attack, attacker.ActionIndex);
        }

        public BattlePhaseAction ToEndPhase()
        {
            this.dialogs.SendEndTurn();
            return new BattlePhaseAction(BattlePhaseAction.BattleAction.ToEndPhase);
        }
        public BattlePhaseAction ToMainPhase2()
        {
            return new BattlePhaseAction(BattlePhaseAction.BattleAction.ToMainPhaseTwo);
        }

        private bool ShouldExecute(CardExecutor exec, ClientCard card, ExecutorType type, int desc = -1)
        {
            if (card.Id != 0 && type == ExecutorType.Activate &&
       activatedCards.ContainsKey(card.Id) && activatedCards[card.Id] >= 9)
            {
                return false;
            }
            this.Executor.SetCard(type, card, desc);
            bool result = card != null && exec.Type == type &&
                (exec.CardId == -1 || exec.CardId == card.Id) &&
                (exec.Func == null || exec.Func());
            if (card.Id != 0 && type == ExecutorType.Activate && result)
            {
                int count = card.IsDisabled() ? 3 : 1;
                if (!activatedCards.ContainsKey(card.Id))
                    activatedCards.Add(card.Id, count);
                else
                {
                    activatedCards[card.Id] += count;
                }
            }
            return result;
        }
    }
}
