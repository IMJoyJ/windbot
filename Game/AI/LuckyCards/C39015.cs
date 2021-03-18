using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.LuckyCards
{
    /// <summary>
    /// バスター・スナイパー / - / 爆裂狙击手
    /// </summary>
    public class C39015 : LuckyCard
    {
        public override bool ShouldExec(Executor exec, GameAI ai, Duel duel, ClientField bot, ClientField enemy, ClientCard card)
        {
            ai.SelectCard(74644400, 3431737, 77036039, 49826746, 40048324);
            return true;
        }
    }
}
