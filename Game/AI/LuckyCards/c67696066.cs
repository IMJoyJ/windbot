using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindBot.Game.AI.LuckyCards
{
    public class C67696066 : LuckyCard
    {
        public override bool ShouldExec(Duel duel, ClientField bot, ClientField enemy, ClientCard card)
        {
            return bot.LifePoints > 1000;
        }
    }
}
