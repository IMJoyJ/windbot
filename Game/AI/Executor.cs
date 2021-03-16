﻿using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI
{
    public abstract class Executor
    {
        public Dictionary<int, int> EffectsUsedInTurnEnemy = new Dictionary<int, int>();
        public Dictionary<int, int> EffectsUsedInTurnSelf = new Dictionary<int, int>();
        public string Deck { get; set; }
        public Duel Duel { get; private set; }
        public IList<CardExecutor> Executors { get; private set; }
        public GameAI AI { get; private set; }
        public AIUtil Util { get; private set; }

        protected MainPhase Main { get; private set; }
        protected BattlePhase Battle { get; private set; }

        protected ExecutorType ExecType { get; private set; }
        protected ClientCard Card { get; private set; }
        protected int ActivateDescription { get; private set; }

        protected ClientField Bot { get; private set; }
        protected ClientField Enemy { get; private set; }

        protected Executor(GameAI ai, Duel duel)
        {
            this.Duel = duel;
            this.AI = ai;
            this.Util = new AIUtil(duel);
            this.Executors = new List<CardExecutor>();

            this.Bot = this.Duel.Fields[0];
            this.Enemy = this.Duel.Fields[1];
        }

        public virtual int OnRockPaperScissors()
        {
            return Program._rand.Next(1, 4);
        }

        public virtual bool OnSelectHand()
        {
            return Program._rand.Next(2) > 0;
        }

        public virtual bool IsEffectUsedInTurn(int code, bool IsEnemy = false)
        {
            if (IsEnemy)
            {
                return EffectsUsedInTurnEnemy.ContainsKey(code) && EffectsUsedInTurnEnemy[code] == this.Duel.Turn;
            }
            return EffectsUsedInTurnSelf.ContainsKey(code) && EffectsUsedInTurnSelf[code] == this.Duel.Turn;
        }

        /// <summary>
        /// Called when the AI has to decide if it should attack
        /// </summary>
        /// <param name="attackers">List of monsters that can attcack.</param>
        /// <param name="defenders">List of monsters of enemy.</param>
        /// <returns>A new BattlePhaseAction containing the action to do.</returns>
        public virtual BattlePhaseAction OnBattle(IList<ClientCard> attackers, IList<ClientCard> defenders)
        {
            // For overriding
            return null;
        }

        /// <summary>
        /// Called when the AI has to decide which card to attack first
        /// </summary>
        /// <param name="attackers">List of monsters that can attcack.</param>
        /// <param name="defenders">List of monsters of enemy.</param>
        /// <returns>The card to attack first.</returns>
        public virtual ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
        {
            // For overriding
            return null;
        }

        public virtual BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
        {
            // Overrided in DefalultExecutor
            return null;
        }

        public virtual bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            // Overrided in DefalultExecutor
            return true;
        }

        public virtual void OnChaining(int player, ClientCard card)
        {
            // For overriding
        }

        public virtual void OnChainEnd()
        {
            // For overriding
        }
        public virtual void OnNewPhase()
        {
            // Some AI need do something on new phase
        }
        public virtual void OnNewTurn()
        {
            // Some AI need do something on new turn
        }
		
        public virtual void OnDraw(int player)
        {
            // Some AI need do something on draw
        }

        public virtual IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
        {
            // For overriding
            return null;
        }

        public virtual IList<ClientCard> OnSelectSum(IList<ClientCard> cards, int sum, int min, int max, int hint, bool mode)
        {
            // For overriding
            return null;
        }

        public virtual IList<ClientCard> OnSelectFusionMaterial(IList<ClientCard> cards, int min, int max)
        {
            // For overriding
            return null;
        }

        public virtual IList<ClientCard> OnSelectSynchroMaterial(IList<ClientCard> cards, int sum, int min, int max)
        {
            // For overriding
            return null;
        }

        public virtual IList<ClientCard> OnSelectXyzMaterial(IList<ClientCard> cards, int min, int max)
        {
            // For overriding
            return null;
        }

        public virtual IList<ClientCard> OnSelectLinkMaterial(IList<ClientCard> cards, int min, int max)
        {
            // For overriding
            return null;
        }

        public virtual IList<ClientCard> OnSelectRitualTribute(IList<ClientCard> cards, int sum, int min, int max)
        {
            // For overriding
            return null;
        }

        public virtual IList<ClientCard> OnSelectPendulumSummon(IList<ClientCard> cards, int max)
        {
            // For overriding
            return null;
        }

        public virtual IList<ClientCard> OnCardSorting(IList<ClientCard> cards)
        {
            // For overriding
            return null;
        }

        public virtual bool OnSelectYesNo(int desc)
        {
            return true;
        }

        public virtual int OnSelectOption(IList<int> options)
        {
            return -1;
        }

        public virtual int OnSelectPlace(int cardId, int player, CardLocation location, int available)
        {
            // For overriding
            return 0;
        }

        public virtual CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
        {
            // Overrided in DefalultExecutor
            return 0;
        }

        public virtual bool OnSelectBattleReplay()
        {
            // Overrided in DefalultExecutor
            return false;
        }

        /// <summary>
        /// Called when bot is going to annouce a card
        /// </summary>
        /// <param name="avail">Available card's ids.</param>
        /// <returns>Card's id to annouce.</returns>
        public virtual int OnAnnounceCard(IList<int> avail)
        {
            // For overriding
            return 0;
        }

        public void SetMain(MainPhase main)
        {
            this.Main = main;
        }

        public void SetBattle(BattlePhase battle)
        {
            this.Battle = battle;
        }

        /// <summary>
        /// Set global variables Type, Card, ActivateDescription for Executor
        /// </summary>
        public void SetCard(ExecutorType type, ClientCard card, int description)
        {
            this.ExecType = type;
            this.Card = card;
            this.ActivateDescription = description;
        }

        /// <summary>
        /// Do the action for the card if func return true.
        /// </summary>
        public void AddExecutor(ExecutorType type, int cardId, Func<bool> func)
        {
            this.Executors.Add(new CardExecutor(type, cardId, func));
        }

        /// <summary>
        /// Do the action for the card if available.
        /// </summary>
        public void AddExecutor(ExecutorType type, int cardId)
        {
            this.Executors.Add(new CardExecutor(type, cardId, null));
        }

        /// <summary>
        /// Do the action for every card if func return true.
        /// </summary>
        public void AddExecutor(ExecutorType type, Func<bool> func)
        {
            this.Executors.Add(new CardExecutor(type, -1, func));
        }

        /// <summary>
        /// Do the action for every card if no other Executor is added to it.
        /// </summary>
        public void AddExecutor(ExecutorType type)
        {
            this.Executors.Add(new CardExecutor(type, -1, this.DefaultNoExecutor));
        }

        private bool DefaultNoExecutor()
        {
            return this.Executors.All(exec => exec.Type != this.ExecType || exec.CardId != this.Card.Id);
        }
    }
}