namespace WindBot.Game
{
    public class BattlePhaseAction
    {
        public enum BattleAction
        {
            Activate = 0,
            Attack = 1,
            ToMainPhaseTwo = 2,
            ToEndPhase = 3
        }

        public BattleAction Action { get; private set; }
        public int Index { get; private set; }

        public BattlePhaseAction(BattleAction action)
        {
            this.Action = action;
            this.Index = 0;
        }

        public BattlePhaseAction(BattleAction action, int[] indexes)
        {
            this.Action = action;
            this.Index = indexes[(int)action];
        }

        public int ToValue()
        {
            return (this.Index << 16) + (int)this.Action;
        }
    }
}