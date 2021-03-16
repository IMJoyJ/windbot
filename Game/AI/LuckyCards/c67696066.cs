using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindBot.Game.AI.LuckyCards
{
    /// <summary>
    /// Emトリック・クラウン / Performage Trick Clown / 娱乐法师 戏法小丑
    /// </summary>
    public class C67696066 : LuckyCard
    {
        public override bool ShouldExec(Duel duel, ClientField bot, ClientField enemy, ClientCard card)
        {
            return bot.LifePoints > 1000;
        }
    }
}
