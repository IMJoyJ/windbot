using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI
{
    public class CardSelector
    {
        private enum SelectType
        {
            Card,
            Cards,
            Id,
            Ids,
            Location
        }

        private SelectType type;
        private ClientCard card;
        private IList<ClientCard> cards;
        private int id;
        private IList<int> ids;
        private CardLocation location;

        public CardSelector(ClientCard card)
        {
            this.type = SelectType.Card;
            this.card = card;
        }

        public CardSelector(IList<ClientCard> cards)
        {
            this.type = SelectType.Cards;
            this.cards = cards;
        }

        public CardSelector(int cardId)
        {
            this.type = SelectType.Id;
            this.id = cardId;
        }

        public CardSelector(IList<int> ids)
        {
            this.type = SelectType.Ids;
            this.ids = ids;
        }

        public CardSelector(CardLocation location)
        {
            this.type = SelectType.Location;
            this.location = location;
        }

        public IList<ClientCard> Select(IList<ClientCard> cards, int min, int max)
        {
            IList<ClientCard> result = new List<ClientCard>();

            switch (this.type)
            {
                case SelectType.Card:
                    if (cards.Contains(this.card))
                    {
                        result.Add(this.card);
                    }

                    break;
                case SelectType.Cards:
                    foreach (ClientCard card in this.cards)
                    {
                        if (cards.Contains(card) && !result.Contains(card))
                        {
                            result.Add(card);
                        }
                    }

                    break;
                case SelectType.Id:
                    foreach (ClientCard card in cards)
                    {
                        if (card.IsCode(this.id))
                        {
                            result.Add(card);
                        }
                    }

                    break;
                case SelectType.Ids:
                    foreach (int id in this.ids)
                    {
                        foreach (ClientCard card in cards)
                        {
                            if (card.IsCode(id) && !result.Contains(card))
                            {
                                result.Add(card);
                            }
                        }
                    }

                    break;
                case SelectType.Location:
                    foreach (ClientCard card in cards)
                    {
                        if (card.Location == this.location)
                        {
                            result.Add(card);
                        }
                    }

                    break;
            }

            if (result.Count < min)
            {
                foreach (ClientCard card in cards)
                {
                    if (!result.Contains(card))
                    {
                        result.Add(card);
                    }

                    if (result.Count >= min)
                    {
                        break;
                    }
                }
            }

            while (result.Count > max)
            {
                result.RemoveAt(result.Count - 1);
            }

            return result;
        }
    }
}