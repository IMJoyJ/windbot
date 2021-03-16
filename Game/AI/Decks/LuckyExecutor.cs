using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;

namespace WindBot.Game.AI.Decks
{
    [Deck("Lucky", "AI_Test", "Test")]
    public class LuckyExecutor : DefaultExecutor
    {
        public LuckyExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.SpSummon, this.ImFeelingLucky);
            this.AddExecutor(ExecutorType.Activate, this.ImFeelingLucky);
            this.AddExecutor(ExecutorType.SummonOrSet, this.ImFeelingLucky);
            this.AddExecutor(ExecutorType.SpellSet, this.ImFeelingLucky);
            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);

            this.AddExecutor(ExecutorType.Activate, CardId.MysticalSpaceTyphoon, this.DefaultMysticalSpaceTyphoon);
            this.AddExecutor(ExecutorType.Activate, CardId.CosmicCyclone, this.DefaultCosmicCyclone);
            this.AddExecutor(ExecutorType.Activate, CardId.GalaxyCyclone, this.DefaultGalaxyCyclone);
            this.AddExecutor(ExecutorType.Activate, CardId.BookOfMoon, this.DefaultBookOfMoon);
            this.AddExecutor(ExecutorType.Activate, CardId.CompulsoryEvacuationDevice, this.DefaultCompulsoryEvacuationDevice);
            this.AddExecutor(ExecutorType.Activate, CardId.CallOfTheHaunted, this.DefaultCallOfTheHaunted);
            this.AddExecutor(ExecutorType.Activate, CardId.Scapegoat, this.DefaultScapegoat);
            this.AddExecutor(ExecutorType.Activate, CardId.MaxxC, this.DefaultMaxxC);
            this.AddExecutor(ExecutorType.Activate, CardId.AshBlossom, this.DefaultAshBlossomAndJoyousSpring);
            this.AddExecutor(ExecutorType.Activate, CardId.GhostOgreAndSnowRabbit, this.DefaultGhostOgreAndSnowRabbit);
            this.AddExecutor(ExecutorType.Activate, CardId.GhostBelle, this.DefaultGhostBelleAndHauntedMansion);
            this.AddExecutor(ExecutorType.Activate, CardId.EffectVeiler, this.DefaultEffectVeiler);
            this.AddExecutor(ExecutorType.Activate, CardId.CalledByTheGrave, this.DefaultCalledByTheGrave);
            this.AddExecutor(ExecutorType.Activate, CardId.InfiniteImpermanence, this.DefaultInfiniteImpermanence);
            this.AddExecutor(ExecutorType.Activate, CardId.BreakthroughSkill, this.DefaultBreakthroughSkill);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnJudgment, this.DefaultSolemnJudgment);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnWarning, this.DefaultSolemnWarning);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, this.DefaultSolemnStrike);
            this.AddExecutor(ExecutorType.Activate, CardId.TorrentialTribute, this.DefaultTorrentialTribute);
            this.AddExecutor(ExecutorType.Activate, CardId.HeavyStorm, this.DefaultHeavyStorm);
            this.AddExecutor(ExecutorType.Activate, CardId.HarpiesFeatherDuster, this.DefaultHarpiesFeatherDusterFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.HammerShot, this.DefaultHammerShot);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);
            this.AddExecutor(ExecutorType.Activate, CardId.Raigeki, this.DefaultRaigeki);
            this.AddExecutor(ExecutorType.Activate, CardId.SmashingGround, this.DefaultSmashingGround);
            this.AddExecutor(ExecutorType.Activate, CardId.PotOfDesires, this.DefaultPotOfDesires);
            this.AddExecutor(ExecutorType.Activate, CardId.AllureofDarkness, this.DefaultAllureofDarkness);
            this.AddExecutor(ExecutorType.Activate, CardId.DimensionalBarrier, this.DefaultDimensionalBarrier);
            this.AddExecutor(ExecutorType.Activate, CardId.InterruptedKaijuSlumber, this.DefaultInterruptedKaijuSlumber);

            this.AddExecutor(ExecutorType.SpSummon, CardId.JizukirutheStarDestroyingKaiju, this.DefaultKaijuSpsummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.GadarlatheMysteryDustKaiju, this.DefaultKaijuSpsummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.GamecieltheSeaTurtleKaiju, this.DefaultKaijuSpsummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.RadiantheMultidimensionalKaiju, this.DefaultKaijuSpsummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.KumongoustheStickyStringKaiju, this.DefaultKaijuSpsummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.ThunderKingtheLightningstrikeKaiju, this.DefaultKaijuSpsummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.DogorantheMadFlameKaiju, this.DefaultKaijuSpsummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.SuperAntiKaijuWarMachineMechaDogoran, this.DefaultKaijuSpsummon);

            this.AddExecutor(ExecutorType.SpSummon, CardId.EvilswarmExcitonKnight, this.DefaultEvilswarmExcitonKnightSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.EvilswarmExcitonKnight, this.DefaultEvilswarmExcitonKnightEffect);
        }
        public override IList<ClientCard> OnSelectCard(IList<ClientCard> _cards, int min, int max, int hint, bool cancelable)
        {
            if (this.Duel.Phase == DuelPhase.BattleStart)
            {
                return null;
            }

            if (this.AI.HaveSelectedCards())
            {
                return null;
            }

            IList<ClientCard> cards = new List<ClientCard>(_cards);
            IList<ClientCard> selected = new List<ClientCard>();

            if (max > cards.Count)
            {
                max = cards.Count;
            }

            // select random cards
            while (selected.Count < max)
            {
                ClientCard card = cards[Program._rand.Next(cards.Count)];
                selected.Add(card);
                cards.Remove(card);
            }

            return selected;
        }

        public override int OnSelectOption(IList<int> options)
        {
            return Program._rand.Next(options.Count);
        }

        private LuckyCards.LuckyCard GetLuckyCardByCardId(int id)
        {
            var typ = System.Type.GetType($"WindBot.Game.AI.LuckyCards.C{id}", false);
            if (typ == null)
            {
                typ = System.Type.GetType($"WindBot.Game.AI.LuckyCards.LuckyCard", false);
            }
            object o = System.Activator.CreateInstance(typ);
            return (LuckyCards.LuckyCard)o;
        }

        private bool ImFeelingLucky()
        {
            var lc = this.GetLuckyCardByCardId(this.Card.Id);
            return lc.ShouldExec(this, this.AI, this.Duel, Bot, Enemy, this.Card)
                && this.DefaultDontChainMyself();
        }
        public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
        {
            var lc = this.GetLuckyCardByCardId(this.Card.Id);
            return lc.GetSummonPosition(this, this.AI, this.Duel, Bot, Enemy, this.Card);
        }

        public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
        {
            var lc = this.GetLuckyCardByCardId(this.Card.Id);
            return lc.GetAppearPlace(this, this.AI, this.Duel, Bot, Enemy, cardId, player, location, available);
        }
    }
}