using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.LuckyCards
{
    public class LuckyCard
    {
        public virtual bool ShouldExec(Executor exec, GameAI ai, Duel duel, ClientField bot, ClientField enemy, ClientCard card)
        {
            return Program._rand.Next(9) >= 3;
        }
        public virtual int GetAppearPlace(Executor exec, GameAI ai, Duel duel, ClientField bot, ClientField enemy, int id, int player, CardLocation location, int available)
        {
            if (location == CardLocation.MonsterZone)
            {
                if ((available & ~bot.GetLinkedZones()) > 0)
                {
                    available = available & ~bot.GetLinkedZones();
                }
                if ((available & Zones.MonsterZone1) > 0)
                {
                    return Zones.MonsterZone1;
                }
                return available;
            }
            else
            {
                return available;
            }
        }

        public virtual CardPosition GetSummonPosition(Executor exec, GameAI ai, Duel duel, ClientField bot, ClientField enemy, ClientCard card)
        {
            int atk = 0;
            YGOSharp.OCGWrapper.NamedCard cardData = YGOSharp.OCGWrapper.NamedCard.Get(card.Id);
            if (cardData != null)
            {
                atk = cardData.Attack;
            }
            foreach (ClientCard c in enemy.GetMonsters())
            {
                if (c.RealPower <= atk)
                {
                    continue;
                }
                //Enemy atk is more than bot's
                if (c.IsLastAttacker)
                {
                    return CardPosition.FaceUpAttack;
                }
                if (exec.Util.IsTurn1OrMain2()) //Can't attack -> defense
                {
                    return CardPosition.FaceUpDefence;
                }
                else
                {
                    if (c.HasPosition(CardPosition.Defence) && c.Defense < atk)
                    {
                        return CardPosition.FaceUpAttack;
                    }
                    else
                    {
                        if(exec.Util.GetWorstBotMonster(false).RealPower > atk)
                        {
                            return CardPosition.FaceUpDefence;
                        }
                        if (c.Attack - atk > bot.LifePoints)
                        {
                            return CardPosition.FaceUpDefence;
                        }
                    }
                }
            }
            return CardPosition.Attack;
        }
    }
}
