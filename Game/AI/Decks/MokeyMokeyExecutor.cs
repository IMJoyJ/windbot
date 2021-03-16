using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("MokeyMokey", "AI_MokeyMokey", "Easy")]
    public class MokeyMokeyExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int LeoWizard = 4392470;
            public const int Bunilla = 69380702;
        }

        private int RockCount = 0;

        public MokeyMokeyExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.Summon);
            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
            this.AddExecutor(ExecutorType.SpellSet);
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