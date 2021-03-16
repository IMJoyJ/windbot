using System.Collections.Generic;

namespace WindBot.Game
{
    public class MainPhase
    {
        public IList<ClientCard> SummonableCards { get; private set; }
        public IList<ClientCard> SpecialSummonableCards { get; private set; }
        public IList<ClientCard> ReposableCards { get; private set; }
        public IList<ClientCard> MonsterSetableCards { get; private set; }
        public IList<ClientCard> SpellSetableCards { get; private set; }
        public IList<ClientCard> ActivableCards { get; private set; }
        public IList<int> ActivableDescs { get; private set; }
        public bool CanBattlePhase { get; set; }
        public bool CanEndPhase { get; set; }

        public MainPhase()
        {
            this.SummonableCards = new List<ClientCard>();
            this.SpecialSummonableCards = new List<ClientCard>();
            this.ReposableCards = new List<ClientCard>();
            this.MonsterSetableCards = new List<ClientCard>();
            this.SpellSetableCards = new List<ClientCard>();
            this.ActivableCards = new List<ClientCard>();
            this.ActivableDescs = new List<int>();
        }
    }
}