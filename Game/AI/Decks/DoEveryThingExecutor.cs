using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("Test", "AI_Test", "Test")]
    public class DoEverythingExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int LeoWizard = 4392470;
            public const int Bunilla = 69380702;
        }

        public DoEverythingExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.SpSummon);
            this.AddExecutor(ExecutorType.Activate, this.DefaultDontChainMyself);
            this.AddExecutor(ExecutorType.SummonOrSet);
            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
            this.AddExecutor(ExecutorType.SpellSet);
        }

        public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
        {
            if (this.Duel.Phase == DuelPhase.BattleStart)
            {
                return null;
            }

            IList<ClientCard> selected = new List<ClientCard>();

            // select the last cards
            for (int i = 1; i <= max; ++i)
            {
                selected.Add(cards[cards.Count-i]);
            }

            return selected;
        }

        public override int OnSelectOption(IList<int> options)
        {
            return Program._rand.Next(options.Count);
        }

    }
}