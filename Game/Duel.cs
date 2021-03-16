using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game
{
    public class Duel
    {
        public bool IsFirst { get; set; }
        public bool IsNewRule { get; set; }
        public bool IsNewRule2020 { get; set; }

        public ClientField[] Fields { get; private set; }

        public int Turn { get; set; }
        public int Player { get; set; }
        public DuelPhase Phase { get; set; }
        public MainPhase MainPhase { get; set; }
        public BattlePhase BattlePhase { get; set; }

        public int LastChainPlayer { get; set; }
        public IList<ClientCard> CurrentChain { get; set; }
        public IList<ClientCard> ChainTargets { get; set; }
        public IList<ClientCard> ChainTargetOnly { get; set; }
        public int LastSummonPlayer { get; set; }
        public IList<ClientCard> SummoningCards { get; set; }
        public IList<ClientCard> LastSummonedCards { get; set; }

        public Duel()
        {
            this.Fields = new ClientField[2];
            this.Fields[0] = new ClientField();
            this.Fields[1] = new ClientField();
            this.LastChainPlayer = -1;
            this.CurrentChain = new List<ClientCard>();
            this.ChainTargets = new List<ClientCard>();
            this.ChainTargetOnly = new List<ClientCard>();
            this.LastSummonPlayer = -1;
            this.SummoningCards = new List<ClientCard>();
            this.LastSummonedCards = new List<ClientCard>();
        }

        public ClientCard GetCard(int player, CardLocation loc, int seq)
        {
            return this.GetCard(player, (int)loc, seq, 0);
        }

        public ClientCard GetCard(int player, int loc, int seq, int subSeq)
        {
            if (player < 0 || player > 1)
            {
                return null;
            }

            bool isXyz = (loc & 0x80) != 0;
            CardLocation location = (CardLocation)(loc & 0x7f);

            IList<ClientCard> cards = null;
            switch (location)
            {
                case CardLocation.Deck:
                    cards = this.Fields[player].Deck;
                    break;
                case CardLocation.Hand:
                    cards = this.Fields[player].Hand;
                    break;
                case CardLocation.MonsterZone:
                    cards = this.Fields[player].MonsterZone;
                    break;
                case CardLocation.SpellZone:
                    cards = this.Fields[player].SpellZone;
                    break;
                case CardLocation.Grave:
                    cards = this.Fields[player].Graveyard;
                    break;
                case CardLocation.Removed:
                    cards = this.Fields[player].Banished;
                    break;
                case CardLocation.Extra:
                    cards = this.Fields[player].ExtraDeck;
                    break;
            }
            if (cards == null)
            {
                return null;
            }

            if (seq >= cards.Count)
            {
                return null;
            }

            if (isXyz)
            {
                ClientCard card = cards[seq];
                if (card == null || subSeq >= card.Overlays.Count)
                {
                    return null;
                }

                return null; // TODO card.Overlays[subSeq]
            }

            return cards[seq];
        }

        public void AddCard(CardLocation loc, int cardId, int player, int seq, int pos)
        {
            switch (loc)
            {
                case CardLocation.Hand:
                    this.Fields[player].Hand.Add(new ClientCard(cardId, loc, -1, pos));
                    break;
                case CardLocation.Grave:
                    this.Fields[player].Graveyard.Add(new ClientCard(cardId, loc,-1, pos));
                    break;
                case CardLocation.Removed:
                    this.Fields[player].Banished.Add(new ClientCard(cardId, loc, -1, pos));
                    break;
                case CardLocation.MonsterZone:
                    this.Fields[player].MonsterZone[seq] = new ClientCard(cardId, loc, seq, pos);
                    break;
                case CardLocation.SpellZone:
                    this.Fields[player].SpellZone[seq] = new ClientCard(cardId, loc, seq, pos);
                    break;
                case CardLocation.Deck:
                    this.Fields[player].Deck.Add(new ClientCard(cardId, loc, -1, pos));
                    break;
                case CardLocation.Extra:
                    this.Fields[player].ExtraDeck.Add(new ClientCard(cardId, loc, -1, pos));
                    break;
            }
        }

        public void AddCard(CardLocation loc, ClientCard card, int player, int seq, int pos, int id)
        {
            card.Location = loc;
            card.Sequence = seq;
            card.Position = pos;
            card.SetId(id);
            switch (loc)
            {
                case CardLocation.Hand:
                    this.Fields[player].Hand.Add(card);
                    break;
                case CardLocation.Grave:
                    this.Fields[player].Graveyard.Add(card);
                    break;
                case CardLocation.Removed:
                    this.Fields[player].Banished.Add(card);
                    break;
                case CardLocation.MonsterZone:
                    this.Fields[player].MonsterZone[seq] = card;
                    break;
                case CardLocation.SpellZone:
                    this.Fields[player].SpellZone[seq] = card;
                    break;
                case CardLocation.Deck:
                    this.Fields[player].Deck.Add(card);
                    break;
                case CardLocation.Extra:
                    this.Fields[player].ExtraDeck.Add(card);
                    break;
            }
        }

        public void RemoveCard(CardLocation loc, ClientCard card, int player, int seq)
        {
            switch (loc)
            {
                case CardLocation.Hand:
                    this.Fields[player].Hand.Remove(card);
                    break;
                case CardLocation.Grave:
                    this.Fields[player].Graveyard.Remove(card);
                    break;
                case CardLocation.Removed:
                    this.Fields[player].Banished.Remove(card);
                    break;
                case CardLocation.MonsterZone:
                    this.Fields[player].MonsterZone[seq] = null;
                    break;
                case CardLocation.SpellZone:
                    this.Fields[player].SpellZone[seq] = null;
                    break;
                case CardLocation.Deck:
                    this.Fields[player].Deck.Remove(card);
                    break;
                case CardLocation.Extra:
                    this.Fields[player].ExtraDeck.Remove(card);
                    break;
            }
        }

        public int GetLocalPlayer(int player)
        {
            return this.IsFirst ? player : 1 - player;
        }
    }
}