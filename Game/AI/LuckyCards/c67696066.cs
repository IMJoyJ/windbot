using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.LuckyCards
{
    /// <summary>
    /// Emトリック・クラウン / Performage Trick Clown / 娱乐法师 戏法小丑
    /// </summary>
    public class C67696066 : LuckyCard
    {
        public override bool ShouldExec(Executor exec, GameAI ai, Duel duel, ClientField bot, ClientField enemy, ClientCard card)
        {
            if (exec.ExecType == ExecutorType.Activate)
            {
                return bot.LifePoints > 1000;
            }
            return true;
        }
        public override CardPosition GetSummonPosition(Executor exec, GameAI ai, Duel duel, ClientField bot, ClientField enemy, ClientCard card)
        {
            if (card.Location == CardLocation.Grave)
            {
                return CardPosition.FaceUpDefence;
            }
            return CardPosition.FaceUpAttack;
        }
    }
}
