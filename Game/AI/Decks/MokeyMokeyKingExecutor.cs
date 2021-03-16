using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("MokeyMokeyKing", "AI_MokeyMokeyKing", "Easy")]
    public class MokeyMokeyKingExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int LeoWizard = 4392470;
            public const int Bunilla = 69380702;
        }

        private int RockCount = 0;

        public MokeyMokeyKingExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.SpSummon);
            this.AddExecutor(ExecutorType.SummonOrSet);
            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
            this.AddExecutor(ExecutorType.Activate, this.DefaultField);
        }

        public override int OnRockPaperScissors()
        {
            this.RockCount++;
            if (this.RockCount <= 3)
            {
                return 2;
            }
            else
            {
                return base.OnRockPaperScissors();
            }
        }
    }
}