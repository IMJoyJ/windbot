using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.LuckyCards
{
    /// <summary>
    /// リミット・リバース / Limit Reverse / 限制苏生
    /// </summary>
    public class C27551 : LuckyCard
    {
        public override bool ShouldExec(Executor exec, GameAI ai, Duel duel, ClientField bot, ClientField enemy, ClientCard card)
        {
            if (exec.ExecType == ExecutorType.Activate)
            {
                return duel.Phase == DuelPhase.End;
            }
            if (exec.ExecType == ExecutorType.SpellSet)
            {
                var grave = bot.Graveyard;
                foreach (ClientCard c in grave)
                {
                    if (c.BaseAttack < 1000)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
