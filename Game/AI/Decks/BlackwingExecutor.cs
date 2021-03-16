using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    // NOT FINISHED YET
    [Deck("Blackwing", "AI_Blackwing", "NotFinished")]
    public class BlackwingExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int KrisTheCrackOfDawn = 81105204;
            public const int SiroccoTheDawn = 75498415;
            public const int ShuraTheBlueFlame = 58820853;
            public const int BoraTheSpear = 49003716;
            public const int KalutTheMoonShadow = 85215458;
            public const int GaleTheWhirlwind = 2009101;
            public const int BlizzardTheFarNorth = 22835145;
            public const int MistralTheSilverShield = 46710683;
            public const int Raigeki = 12580477;
            public const int DarkHole = 53129443;
            public const int MysticalSpaceTyphoon = 5318639;
            public const int BlackWhirlwind = 91351370;
            public const int MirrorForce = 44095762;
            public const int DeltaCrowAntiReverse = 59839761;
            public const int DimensionalPrison = 70342110;
            public const int SilverwindTheAscendant = 33236860;
            public const int BlackWingedDragon = 9012916;
            public const int ArmorMaster = 69031175;
            public const int ArmedWing = 76913983;
            public const int GramTheShiningStar = 17377751;
        }

        public BlackwingExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.SpellSet, this.DefaultSpellSet);

            this.AddExecutor(ExecutorType.Activate, CardId.MysticalSpaceTyphoon, this.DefaultMysticalSpaceTyphoon);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);
            this.AddExecutor(ExecutorType.Activate, CardId.Raigeki, this.DefaultRaigeki);
            this.AddExecutor(ExecutorType.Activate, CardId.BlackWhirlwind, this.BlackWhirlwindEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.KrisTheCrackOfDawn);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.KrisTheCrackOfDawn);
            this.AddExecutor(ExecutorType.Summon, CardId.SiroccoTheDawn, this.SiroccoTheDawnSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.ShuraTheBlueFlame, this.ShuraTheBlueFlameSummon);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.ShuraTheBlueFlame);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BoraTheSpear);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.BoraTheSpear);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.KalutTheMoonShadow, this.KalutTheMoonShadowSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.GaleTheWhirlwind);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.GaleTheWhirlwind);
            this.AddExecutor(ExecutorType.Summon, CardId.BlizzardTheFarNorth, this.BlizzardTheFarNorthSummon);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.MistralTheSilverShield);

            this.AddExecutor(ExecutorType.SpSummon, CardId.SilverwindTheAscendant);
            this.AddExecutor(ExecutorType.SpSummon, CardId.ArmorMaster);
            this.AddExecutor(ExecutorType.SpSummon, CardId.GramTheShiningStar);
            this.AddExecutor(ExecutorType.SpSummon, CardId.ArmedWing);
            this.AddExecutor(ExecutorType.SpSummon, CardId.BlackWingedDragon);

            this.AddExecutor(ExecutorType.Activate, CardId.MirrorForce, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.DimensionalPrison, this.DefaultUniqueTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.DeltaCrowAntiReverse, this.DeltaCrowAntiReverseEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.BlizzardTheFarNorth);
            this.AddExecutor(ExecutorType.Activate, CardId.ShuraTheBlueFlame);
            this.AddExecutor(ExecutorType.Activate, CardId.BoraTheSpear, this.BoraTheSpearEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.KalutTheMoonShadow, this.AttackUpEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.SiroccoTheDawn, this.AttackUpEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.GaleTheWhirlwind, this.GaleTheWhirlwindEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.SilverwindTheAscendant);
            this.AddExecutor(ExecutorType.Activate, CardId.BlackWingedDragon);
            this.AddExecutor(ExecutorType.Activate, CardId.ArmorMaster);
            this.AddExecutor(ExecutorType.Activate, CardId.ArmedWing);
            this.AddExecutor(ExecutorType.Activate, CardId.GramTheShiningStar);

            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
        }

        private bool ShuraTheBlueFlameSummon()
        {
            if (this.Bot.HasInMonstersZone(CardId.SiroccoTheDawn) && this.Bot.GetMonsters().GetHighestAttackMonster().Attack < 3800)
            {
                return true;
            }

            return false;
        }

        private bool BlackWhirlwindEffect()
        {
            if (this.Card.Location == CardLocation.Hand && this.Bot.HasInSpellZone(this.Card.Id))
            {
                return false;
            }

            if (this.ActivateDescription == this.Util.GetStringId((int)this.Card.Id,0))
            {
                this.AI.SelectCard(CardId.GaleTheWhirlwind);
            }

            return true;
        }

        private bool SiroccoTheDawnSummon()
        {
            int OpponentMonster = this.Enemy.GetMonsterCount();
            int AIMonster = this.Bot.GetMonsterCount();
            if (OpponentMonster != 0 && AIMonster == 0)
            {
                return true;
            }

            return false;
        }

        private bool BoraTheSpearEffect()
        {
            List<ClientCard> monster = this.Bot.GetMonsters();
            foreach (ClientCard card in monster)
            {
                if (card != null && card.IsCode(CardId.KrisTheCrackOfDawn, CardId.KalutTheMoonShadow, CardId.GaleTheWhirlwind, CardId.BoraTheSpear, CardId.SiroccoTheDawn, CardId.ShuraTheBlueFlame, CardId.BlizzardTheFarNorth))
                {
                    return true;
                }
            }

            return false;
        }

        private bool KalutTheMoonShadowSummon()
        {
            foreach (ClientCard card in this.Bot.Hand)
            {
                if (card != null && card.IsCode(CardId.KrisTheCrackOfDawn, CardId.GaleTheWhirlwind, CardId.BoraTheSpear, CardId.SiroccoTheDawn, CardId.ShuraTheBlueFlame, CardId.BlizzardTheFarNorth))
                {
                    return false;
                }
            }

            return true;
        }

        private bool BlizzardTheFarNorthSummon()
        {
            foreach (ClientCard card in this.Bot.Graveyard)
            {
                if (card != null && card.IsCode(CardId.KalutTheMoonShadow, CardId.BoraTheSpear, CardId.ShuraTheBlueFlame, CardId.KrisTheCrackOfDawn))
                {
                    return true;
                }
            }

            return false;
        }

        private bool DeltaCrowAntiReverseEffect()
        {
            int Count = 0;

            List<ClientCard> monster = this.Bot.GetMonsters();
            foreach (ClientCard card in monster)
            {
                if (card != null && card.IsCode(CardId.KrisTheCrackOfDawn, CardId.KalutTheMoonShadow, CardId.GaleTheWhirlwind, CardId.BoraTheSpear, CardId.SiroccoTheDawn, CardId.ShuraTheBlueFlame, CardId.BlizzardTheFarNorth))
                {
                    Count++;
                }
            }

            if (Count == 3)
            {
                return true;
            }

            return false;
        }

        private bool GaleTheWhirlwindEffect()
        {
            if (this.Card.Position == (int)CardPosition.FaceUp)
            {
                this.AI.SelectCard(this.Enemy.GetMonsters().GetHighestAttackMonster());
                return true;
            }
            return false;
        }

        private bool AttackUpEffect()
        {
            ClientCard bestMy = this.Bot.GetMonsters().GetHighestAttackMonster();
            ClientCard bestEnemyATK = this.Enemy.GetMonsters().GetHighestAttackMonster();
            ClientCard bestEnemyDEF = this.Enemy.GetMonsters().GetHighestDefenseMonster();
            if (bestMy == null || (bestEnemyATK == null && bestEnemyDEF == null))
            {
                return false;
            }

            if (bestEnemyATK != null && bestMy.Attack < bestEnemyATK.Attack)
            {
                return true;
            }

            if (bestEnemyDEF != null && bestMy.Attack < bestEnemyDEF.Defense)
            {
                return true;
            }

            return false;
        }
    }
}