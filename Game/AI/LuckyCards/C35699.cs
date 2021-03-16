using System.Collections.Generic;
using WindBot.Game.AI.Enums;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.LuckyCards
{
    /// <summary>
    /// SPYRAL－ボルテックス / SPYRAL Sleeper / 秘旋谍-龙卷风
    /// </summary>
    public class C35699 : LuckyCard
    {
        public override bool ShouldExec(Executor exec, GameAI ai, Duel duel, ClientField bot, ClientField enemy, ClientCard card)
        {
            if (exec.ExecType == ExecutorType.SpSummon)
            {
                return (bot.MonsterZone.ContainsCardWithId(37433748)
                    || bot.Hand.ContainsCardWithId(37433748)) || enemy.MonsterZone.IsExistingMatchingCard(this.IsDangerousCard)
                    || enemy.SpellZone.IsExistingMatchingCard(this.IsDangerousCard);
            }
            if (exec.ExecType == ExecutorType.Activate)
            {
                if (bot.MonsterZone.ContainsCardWithId(37433748)
                    || bot.Hand.ContainsCardWithId(37433748))
                {
                    return false;
                }
                List<ClientCard> targetList = new List<ClientCard>();
                int canSelect = 2;
                targetList.AddRange(enemy.MonsterZone.GetMatchingCards(this.IsDangerousCard));
                targetList.AddRange(enemy.SpellZone.GetMatchingCards(this.IsDangerousCard));
                if (targetList.Count <= 2)
                {
                    ai.SelectCard(targetList);
                    canSelect -= targetList.Count;
                    if (canSelect == 0)
                    {
                        return true;
                    }
                    var targetList2 = new List<ClientCard>();
                    if (duel.Phase == DuelPhase.End)
                    {
                        targetList2.AddRange(enemy.MonsterZone.GetMatchingCards(this.IsDestroyableCard));
                        targetList2.AddRange(enemy.SpellZone.GetMatchingCards(this.IsDestroyableCard));
                    }
                    else
                    {
                        targetList2.AddRange(enemy.MonsterZone.GetMatchingCards(this.ShouldBeDestroyedBeforeMainPhaseEnd));
                        targetList2.AddRange(enemy.SpellZone.GetMatchingCards(this.ShouldBeDestroyedBeforeMainPhaseEnd));
                    }
                    if (canSelect == 1)
                    {
                        targetList2.Remove(targetList[0]);
                        if (targetList2.Count > 0)
                        {
                            ai.SelectCard(targetList2[0]);
                        }
                        return true;
                    }
                    while(canSelect > 0)
                    {
                        canSelect--;
                        ai.SelectCard(targetList2[0]);
                        targetList2.RemoveAt(0);
                    }
                    return true;
                }
                else
                {
                    while(canSelect > 0)
                    {
                        canSelect--;
                        ai.SelectCard(targetList[0]);
                        targetList.RemoveAt(0);
                    }
                    return true;
                }
            }
            return false;
        }
        private bool IsDangerousCard(ClientCard card)
        {
            return (card.IsMonsterDangerous() || card.IsFloodgate()
                || card.IsMonsterShouldBeDisabledBeforeItUseEffect()) && this.IsDestroyableCard(card);
        }
        private bool IsDestroyableCard(ClientCard card)
        {
            return !card.IsShouldNotBeMonsterTarget()
                && card.IsCanBeDestroyed(Reason.Effect);
        }
        private bool ShouldBeDestroyedBeforeMainPhaseEnd(ClientCard card)
        {
            return ((card.IsMonster() && card.IsFaceup()) || (card.IsFaceup() && (card.HasType(CardType.Continuous) || card.HasType(CardType.Field)))) && this.IsDestroyableCard(card);
        }
    }
}
