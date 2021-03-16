using System.Collections.Generic;
using System.Linq;
using WindBot.Game.AI;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game
{
    public class ClientField
    {
        public IList<ClientCard> Hand { get; private set; }
        public ClientCard[] MonsterZone { get; private set; }
        public ClientCard[] SpellZone { get; private set; }
        public IList<ClientCard> Graveyard { get; private set; }
        public IList<ClientCard> Banished { get; private set; }
        public IList<ClientCard> Deck { get; private set; }
        public IList<ClientCard> ExtraDeck { get; private set; }

        public int LifePoints;
        public ClientCard BattlingMonster;
        public bool UnderAttack;

        public ClientField()
        {
        }

        public void Init(int deck, int extra)
        {
            this.Hand = new List<ClientCard>();
            this.MonsterZone = new ClientCard[7];
            this.SpellZone = new ClientCard[8];
            this.Graveyard = new List<ClientCard>();
            this.Banished = new List<ClientCard>();
            this.Deck = new List<ClientCard>();
            this.ExtraDeck = new List<ClientCard>();

            for (int i = 0; i < deck; ++i)
            {
                this.Deck.Add(new ClientCard(0, CardLocation.Deck, -1));
            }

            for (int i = 0; i < extra; ++i)
            {
                this.ExtraDeck.Add(new ClientCard(0, CardLocation.Extra, -1));
            }
        }

        public int GetMonstersExtraZoneCount()
        {
            int count = 0;
            if (this.MonsterZone[5] != null)
            {
                count++;
            }

            if (this.MonsterZone[6] != null)
            {
                count++;
            }

            return count;
        }
        public int GetMonsterCount()
        {
            return GetCount(this.MonsterZone);
        }

        public int GetSpellCount()
        {
            return GetCount(this.SpellZone);
        }

        public int GetHandCount()
        {
            return GetCount(this.Hand);
        }

        public int GetSpellCountWithoutField()
        {
            int count = 0;
            for (int i = 0; i < 5; ++i)
            {
                if (this.SpellZone[i] != null)
                {
                    ++count;
                }
            }
            return count;
        }

        /// <summary>
        /// Count Column
        /// </summary>
        /// <param zone>range of zone 0-4</param>
        public int GetColumnCount(int zone, bool IncludeExtraMonsterZone = true)
        {
            int count = 0;
            if (this.SpellZone[zone] != null)
            {
                count++;
            }

            if (this.MonsterZone[zone] != null)
            {
                count++;
            }

            if (zone == 1 && IncludeExtraMonsterZone)
            {
                if (this.MonsterZone[5] != null)
                {
                    count++;
                }
            }
            if (zone == 3 && IncludeExtraMonsterZone)
            {
                if (this.MonsterZone[6] != null)
                {
                    count++;
                }
            }
            return count;
        }

        public int GetFieldCount()
        {
            return this.GetSpellCount() + this.GetMonsterCount();
        }

        public int GetFieldHandCount()
        {
            return this.GetSpellCount() + this.GetMonsterCount() + this.GetHandCount();
        }

        public bool IsFieldEmpty()
        {
            return this.GetMonsters().Count == 0 && this.GetSpells().Count == 0;
        }

        public int GetLinkedZones()
        {
            int zones = 0;
            for (int i = 0; i < 7; i++)
            {
                zones |= this.MonsterZone[i]?.GetLinkedZones() ?? 0;
            }
            return zones;
        }

        public List<ClientCard> GetMonsters()
        {
            return GetCards(this.MonsterZone);
        }

        public List<ClientCard> GetGraveyardMonsters()
        {
            return GetCards(this.Graveyard, CardType.Monster);
        }

        public List<ClientCard> GetGraveyardSpells()
        {
            return GetCards(this.Graveyard, CardType.Spell);
        }

        public List<ClientCard> GetGraveyardTraps()
        {
            return GetCards(this.Graveyard, CardType.Trap);
        }

        public List<ClientCard> GetSpells()
        {
            return GetCards(this.SpellZone);
        }

        public List<ClientCard> GetMonstersInExtraZone()
        {
            return this.GetMonsters().Where(card => card.Sequence >= 5).ToList();
        }

        public List<ClientCard> GetMonstersInMainZone()
        {
            return this.GetMonsters().Where(card => card.Sequence < 5).ToList();
        }

        public ClientCard GetFieldSpellCard()
        {
            return this.SpellZone[5];
        }

        public bool HasInHand(int cardId)
        {
            return HasInCards(this.Hand, cardId);
        }

        public bool HasInHand(IList<int> cardId)
        {
            return HasInCards(this.Hand, cardId);
        }

        public bool HasInGraveyard(int cardId)
        {
            return HasInCards(this.Graveyard, cardId);
        }
    
        public bool HasInGraveyard(IList<int> cardId)
        {
            return HasInCards(this.Graveyard, cardId);
        }

        public bool HasInBanished(int cardId)
        {
            return HasInCards(this.Banished, cardId);
        }

        public bool HasInBanished(IList<int> cardId)
        {
            return HasInCards(this.Banished, cardId);
        }

        public bool HasInExtra(int cardId)
        {
            return HasInCards(this.ExtraDeck, cardId);
        }

        public bool HasInExtra(IList<int> cardId)
        {
            return HasInCards(this.ExtraDeck, cardId);
        }

        public bool HasAttackingMonster()
        {
            return this.GetMonsters().Any(card => card.IsAttack());
        }

        public bool HasDefendingMonster()
        {
            return this.GetMonsters().Any(card => card.IsDefense());
        }

        public bool HasInMonstersZone(int cardId, bool notDisabled = false, bool hasXyzMaterial = false, bool faceUp = false)
        {
            return HasInCards(this.MonsterZone, cardId, notDisabled, hasXyzMaterial, faceUp);
        }

        public bool HasInMonstersZone(IList<int> cardId, bool notDisabled = false, bool hasXyzMaterial = false, bool faceUp = false)
        {
            return HasInCards(this.MonsterZone, cardId, notDisabled, hasXyzMaterial, faceUp);
        }

        public bool HasInSpellZone(int cardId, bool notDisabled = false, bool faceUp = false)
        {
            return HasInCards(this.SpellZone, cardId, notDisabled, false, faceUp);
        }

        public bool HasInSpellZone(IList<int> cardId, bool notDisabled = false, bool faceUp = false)
        {
            return HasInCards(this.SpellZone, cardId, notDisabled, false, faceUp);
        }

        public bool HasInHandOrInSpellZone(int cardId)
        {
            return this.HasInHand(cardId) || this.HasInSpellZone(cardId);
        }

        public bool HasInHandOrHasInMonstersZone(int cardId)
        {
            return this.HasInHand(cardId) || this.HasInMonstersZone(cardId);
        }

        public bool HasInHandOrInGraveyard(int cardId)
        {
            return this.HasInHand(cardId) || this.HasInGraveyard(cardId);
        }

        public bool HasInMonstersZoneOrInGraveyard(int cardId)
        {
            return this.HasInMonstersZone(cardId) || this.HasInGraveyard(cardId);
        }

        public bool HasInSpellZoneOrInGraveyard(int cardId)
        {
            return this.HasInSpellZone(cardId) || this.HasInGraveyard(cardId);
        }

        public bool HasInHandOrInMonstersZoneOrInGraveyard(int cardId)
        {
            return this.HasInHand(cardId) || this.HasInMonstersZone(cardId) || this.HasInGraveyard(cardId);
        }

        public bool HasInHandOrInSpellZoneOrInGraveyard(int cardId)
        {
            return this.HasInHand(cardId) || this.HasInSpellZone(cardId) || this.HasInGraveyard(cardId);
        }

        public bool HasInHandOrInSpellZone(IList<int> cardId)
        {
            return this.HasInHand(cardId) || this.HasInSpellZone(cardId);
        }

        public bool HasInHandOrHasInMonstersZone(IList<int> cardId)
        {
            return this.HasInHand(cardId) || this.HasInMonstersZone(cardId);
        }

        public bool HasInHandOrInGraveyard(IList<int> cardId)
        {
            return this.HasInHand(cardId) || this.HasInGraveyard(cardId);
        }

        public bool HasInMonstersZoneOrInGraveyard(IList<int> cardId)
        {
            return this.HasInMonstersZone(cardId) || this.HasInGraveyard(cardId);
        }

        public bool HasInSpellZoneOrInGraveyard(IList<int> cardId)
        {
            return this.HasInSpellZone(cardId) || this.HasInGraveyard(cardId);
        }

        public bool HasInHandOrInMonstersZoneOrInGraveyard(IList<int> cardId)
        {
            return this.HasInHand(cardId) || this.HasInMonstersZone(cardId) || this.HasInGraveyard(cardId);
        }

        public bool HasInHandOrInSpellZoneOrInGraveyard(IList<int> cardId)
        {
            return this.HasInHand(cardId) || this.HasInSpellZone(cardId) || this.HasInGraveyard(cardId);
        }

        public int GetRemainingCount(int cardId, int initialCount)
        {
            int remaining = initialCount;
            remaining = remaining - this.Hand.Count(card => card != null && card.IsOriginalCode(cardId));
            remaining = remaining - this.SpellZone.Count(card => card != null && card.IsOriginalCode(cardId));
            remaining = remaining - this.MonsterZone.Count(card => card != null && card.IsOriginalCode(cardId));
            remaining = remaining - this.Graveyard.Count(card => card != null && card.IsOriginalCode(cardId));
            remaining = remaining - this.Banished.Count(card => card != null && card.IsOriginalCode(cardId));
            return (remaining < 0) ? 0 : remaining;
        }

        private static int GetCount(IEnumerable<ClientCard> cards)
        {
            return cards.Count(card => card != null);
        }

        public int GetCountCardInZone(IEnumerable<ClientCard> cards, int cardId)
        {
            return cards.Count(card => card != null && card.IsCode(cardId));
        }

        public int GetCountCardInZone(IEnumerable<ClientCard> cards, List<int> cardId)
        {
            return cards.Count(card => card != null && card.IsCode(cardId));
        }

        private static List<ClientCard> GetCards(IEnumerable<ClientCard> cards, CardType type)
        {
            return cards.Where(card => card != null && card.HasType(type)).ToList();
        }

        private static List<ClientCard> GetCards(IEnumerable<ClientCard> cards)
        {
            return cards.Where(card => card != null).ToList();
        }

        private static bool HasInCards(IEnumerable<ClientCard> cards, int cardId, bool notDisabled = false, bool hasXyzMaterial = false, bool faceUp = false)
        {
            return cards.Any(card => card != null && card.IsCode(cardId) && !(notDisabled && card.IsDisabled()) && !(hasXyzMaterial && !card.HasXyzMaterial()) && !(faceUp && card.IsFacedown()));
        }

        private static bool HasInCards(IEnumerable<ClientCard> cards, IList<int> cardId, bool notDisabled = false, bool hasXyzMaterial = false, bool faceUp = false)
        {
            return cards.Any(card => card != null && card.IsCode(cardId) && !(notDisabled && card.IsDisabled()) && !(hasXyzMaterial && !card.HasXyzMaterial()) && !(faceUp && card.IsFacedown()));
        }
    }
}