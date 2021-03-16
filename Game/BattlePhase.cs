using System.Collections.Generic;

namespace WindBot.Game
{
    public class BattlePhase
    {
        public IList<ClientCard> AttackableCards { get; private set; }
        public IList<ClientCard> ActivableCards { get; private set; }
        public IList<int> ActivableDescs { get; private set; }
        public bool CanMainPhaseTwo { get; set; }
        public bool CanEndPhase { get; set; }

        public BattlePhase()
        {
            this.AttackableCards = new List<ClientCard>();
            this.ActivableCards = new List<ClientCard>();
            this.ActivableDescs = new List<int>();
        }
    }
}