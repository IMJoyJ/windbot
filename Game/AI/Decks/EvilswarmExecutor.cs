﻿using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    // NOT FINISHED YET
    [Deck("Evilswarm", "AI_Evilswarm", "NotFinished")]
    public class EvilswarmExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int DarkHole = 53129443;
            public const int CosmicCyclone = 8267140;
            public const int InfestationPandemic = 27541267;
            public const int SolemnJudgment = 41420027;
            public const int SolemnWarning = 84749824;
            public const int SolemnStrike = 40605147;
        }

        public EvilswarmExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);
            this.AddExecutor(ExecutorType.Activate, CardId.CosmicCyclone, this.DefaultCosmicCyclone);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnJudgment, this.DefaultSolemnJudgment);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnWarning, this.DefaultSolemnWarning);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, this.DefaultSolemnStrike);
            this.AddExecutor(ExecutorType.SpellSet, CardId.InfestationPandemic);
            this.AddExecutor(ExecutorType.Activate, this.DefaultDontChainMyself);
            this.AddExecutor(ExecutorType.Summon);
            this.AddExecutor(ExecutorType.SpSummon);
            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
            this.AddExecutor(ExecutorType.SpellSet);
        }

        // will be added soon...?
    }
}