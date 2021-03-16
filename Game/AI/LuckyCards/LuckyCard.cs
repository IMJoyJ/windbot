using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindBot.Game.AI.LuckyCards
{
    public class LuckyCard
    {
        public virtual bool ShouldExec(Duel duel, ClientField bot, ClientField enemy, ClientCard card)
        {
            return Program.Rand.Next(9) >= 3;
        }
    }
}
