using System.Collections.Generic;
using System.IO;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game
{
    public class ClientCard
    {
        public int Id { get; private set; }
        public NamedCard Data { get; private set; }
        public string Name { get; private set; }

        public int Position { get; set; }
        public int Sequence { get; set; }
        public CardLocation Location { get; set; }
        public int Alias { get; private set; }
        public int Level { get; private set; }
        public int Rank { get; private set; }
        public int Type { get; private set; }
        public int Attribute { get; private set; }
        public int Race { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int LScale { get; private set; }
        public int RScale { get; private set; }
        public int LinkCount { get; private set; }
        public int LinkMarker { get; private set; }
        public int BaseAttack { get; private set; }
        public int BaseDefense { get; private set; }
        public int RealPower { get; set; }
        public List<int> Overlays { get; private set; }
        public int Owner { get; private set; }
        public int Controller { get; private set; }
        public int Disabled { get; private set; }
        public int ProcCompleted { get; private set; }
        public int SelectSeq { get; set; }
        public int OpParam1 { get; set; }
        public int OpParam2 { get; set; }

        public List<ClientCard> EquipCards { get; set; }
        public ClientCard EquipTarget;
        public List<ClientCard> OwnTargets { get; set; }
        public List<ClientCard> TargetCards { get; set; }

        public bool CanDirectAttack { get; set; }
        public bool ShouldDirectAttack { get; set; }
        public bool Attacked { get; set; }
        public bool IsLastAttacker { get; set; }
        public bool IsSpecialSummoned { get; set; }

        public int[] ActionIndex { get; set; }
        public IDictionary<int, int> ActionActivateIndex { get; private set; }

        public ClientCard(int id, CardLocation loc, int sequence)
            : this(id, loc, -1 , 0)
        {
        }

        public ClientCard(int id, CardLocation loc, int sequence, int position)
        {
            this.SetId(id);
            this.Sequence = sequence;
            this.Position = position;
            this.Overlays = new List<int>();
            this.EquipCards = new List<ClientCard>();
            this.OwnTargets = new List<ClientCard>();
            this.TargetCards = new List<ClientCard>();
            this.ActionIndex = new int[16];
            this.ActionActivateIndex = new Dictionary<int, int>();
            this.Location = loc;
        }

        public void SetId(int id)
        {
            if (this.Id == id)
            {
                return;
            }

            this.Id = id;
            this.Data = NamedCard.Get(this.Id);
            if (this.Data != null)
            {
                this.Name = this.Data.Name;
                if (this.Data.Alias != 0)
                {
                    this.Alias = this.Data.Alias;
                }
            }
        }

        public void Update(BinaryReader packet, Duel duel)
        {
            int flag = packet.ReadInt32();
            if ((flag & (int)Query.Code) != 0)
            {
                this.SetId(packet.ReadInt32());
            }

            if ((flag & (int)Query.Position) != 0)
            {
                this.Controller = duel.GetLocalPlayer(packet.ReadByte());
                this.Location = (CardLocation)packet.ReadByte();
                this.Sequence = packet.ReadByte();
                this.Position = packet.ReadByte();
            }
            if ((flag & (int)Query.Alias) != 0)
            {
                this.Alias = packet.ReadInt32();
            }

            if ((flag & (int)Query.Type) != 0)
            {
                this.Type = packet.ReadInt32();
            }

            if ((flag & (int)Query.Level) != 0)
            {
                this.Level = packet.ReadInt32();
            }

            if ((flag & (int)Query.Rank) != 0)
            {
                this.Rank = packet.ReadInt32();
            }

            if ((flag & (int)Query.Attribute) != 0)
            {
                this.Attribute = packet.ReadInt32();
            }

            if ((flag & (int)Query.Race) != 0)
            {
                this.Race = packet.ReadInt32();
            }

            if ((flag & (int)Query.Attack) != 0)
            {
                this.Attack = packet.ReadInt32();
            }

            if ((flag & (int)Query.Defence) != 0)
            {
                this.Defense = packet.ReadInt32();
            }

            if ((flag & (int)Query.BaseAttack) != 0)
            {
                this.BaseAttack = packet.ReadInt32();
            }

            if ((flag & (int)Query.BaseDefence) != 0)
            {
                this.BaseDefense = packet.ReadInt32();
            }

            if ((flag & (int)Query.Reason) != 0)
            {
                packet.ReadInt32();
            }

            if ((flag & (int)Query.ReasonCard) != 0)
            {
                packet.ReadInt32(); // Int8 * 4
            }

            if ((flag & (int)Query.EquipCard) != 0)
            {
                packet.ReadInt32(); // Int8 * 4
            }

            if ((flag & (int)Query.TargetCard) != 0)
            {
                int count = packet.ReadInt32();
                for (int i = 0; i < count; ++i)
                {
                    packet.ReadInt32(); // Int8 * 4
                }
            }
            if ((flag & (int)Query.OverlayCard) != 0)
            {
                this.Overlays.Clear();
                int count = packet.ReadInt32();
                for (int i = 0; i < count; ++i)
                {
                    this.Overlays.Add(packet.ReadInt32());
                }
            }
            if ((flag & (int)Query.Counters) != 0)
            {
                int count = packet.ReadInt32();
                for (int i = 0; i < count; ++i)
                {
                    packet.ReadInt32(); // Int16 * 2
                }
            }
            if ((flag & (int)Query.Owner) != 0)
            {
                this.Owner = duel.GetLocalPlayer(packet.ReadInt32());
            }

            if ((flag & (int)Query.Status) != 0) {
                int status = packet.ReadInt32();
                const int STATUS_DISABLED = 0x0001;
                const int STATUS_PROC_COMPLETE = 0x0008;
                this.Disabled = status & STATUS_DISABLED;
                this.ProcCompleted = status & STATUS_PROC_COMPLETE;
            }
            if ((flag & (int)Query.LScale) != 0)
            {
                this.LScale = packet.ReadInt32();
            }

            if ((flag & (int)Query.RScale) != 0)
            {
                this.RScale = packet.ReadInt32();
            }

            if ((flag & (int)Query.Link) != 0)
            {
                this.LinkCount = packet.ReadInt32();
                this.LinkMarker = packet.ReadInt32();
            }
        }

        public void ClearCardTargets()
        {
            foreach (ClientCard card in this.TargetCards)
            {
                card.OwnTargets.Remove(this);
            }
            foreach (ClientCard card in this.OwnTargets)
            {
                card.TargetCards.Remove(this);
            }
            this.OwnTargets.Clear();
            this.TargetCards.Clear();
        }

        public bool HasLinkMarker(int dir)
        {
            return (this.LinkMarker & dir) != 0;
        }

        public bool HasLinkMarker(CardLinkMarker dir)
        {
            return (this.LinkMarker & (int)dir) != 0;
        }

        public int GetLinkedZones()
        {
            if (!this.HasType(CardType.Link) || this.Location != CardLocation.MonsterZone)
            {
                return 0;
            }

            int zones = 0;
            if (this.Sequence > 0 && this.Sequence <= 4 && this.HasLinkMarker(CardLinkMarker.Left))
            {
                zones |= 1 << (this.Sequence - 1);
            }

            if (this.Sequence <= 3 && this.HasLinkMarker(CardLinkMarker.Right))
            {
                zones |= 1 << (this.Sequence + 1);
            }

            if (this.Sequence == 0 && this.HasLinkMarker(CardLinkMarker.TopRight)
                || this.Sequence == 1 && this.HasLinkMarker(CardLinkMarker.Top)
                || this.Sequence == 2 && this.HasLinkMarker(CardLinkMarker.TopLeft))
            {
                zones |= (1 << 5) | (1 << (16 + 6));
            }

            if (this.Sequence == 2 && this.HasLinkMarker(CardLinkMarker.TopRight)
                || this.Sequence == 3 && this.HasLinkMarker(CardLinkMarker.Top)
                || this.Sequence == 4 && this.HasLinkMarker(CardLinkMarker.TopLeft))
            {
                zones |= (1 << 6) | (1 << (16 + 5));
            }

            if (this.Sequence == 5)
            {
                if (this.HasLinkMarker(CardLinkMarker.BottomLeft))
                {
                    zones |= 1 << 0;
                }

                if (this.HasLinkMarker(CardLinkMarker.Bottom))
                {
                    zones |= 1 << 1;
                }

                if (this.HasLinkMarker(CardLinkMarker.BottomRight))
                {
                    zones |= 1 << 2;
                }

                if (this.HasLinkMarker(CardLinkMarker.TopLeft))
                {
                    zones |= 1 << (16 + 4);
                }

                if (this.HasLinkMarker(CardLinkMarker.Top))
                {
                    zones |= 1 << (16 + 3);
                }

                if (this.HasLinkMarker(CardLinkMarker.TopRight))
                {
                    zones |= 1 << (16 + 2);
                }
            }
            if (this.Sequence == 6)
            {
                if (this.HasLinkMarker(CardLinkMarker.BottomLeft))
                {
                    zones |= 1 << 2;
                }

                if (this.HasLinkMarker(CardLinkMarker.Bottom))
                {
                    zones |= 1 << 3;
                }

                if (this.HasLinkMarker(CardLinkMarker.BottomRight))
                {
                    zones |= 1 << 4;
                }

                if (this.HasLinkMarker(CardLinkMarker.TopLeft))
                {
                    zones |= 1 << (16 + 2);
                }

                if (this.HasLinkMarker(CardLinkMarker.Top))
                {
                    zones |= 1 << (16 + 1);
                }

                if (this.HasLinkMarker(CardLinkMarker.TopRight))
                {
                    zones |= 1 << (16 + 0);
                }
            }
            return zones;
        }

        public bool HasType(CardType type)
        {
            return (this.Type & (int)type) != 0;
        }

        public bool HasPosition(CardPosition position)
        {
            return (this.Position & (int)position) != 0;
        }

        public bool HasAttribute(CardAttribute attribute)
        {
            return (this.Attribute & (int)attribute) != 0;
        }

        public bool HasRace(CardRace race)
        {
            return (this.Race & (int)race) != 0;
        }

        public bool HasSetcode(int setcode)
        {
            if (this.Data == null)
            {
                return false;
            }

            long setcodes = this.Data.Setcode;
            int settype = setcode & 0xfff;
            int setsubtype = setcode & 0xf000;
            while (setcodes > 0)
            {
                long check_setcode = setcodes & 0xffff;
                setcodes >>= 16;
                if ((check_setcode & 0xfff) == settype && (check_setcode & 0xf000 & setsubtype) == setsubtype)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsMonster()
        {
            return this.HasType(CardType.Monster);
        }

        public bool IsTuner()
        {
            return this.HasType(CardType.Tuner);
        }

        public bool IsSpell()
        {
            return this.HasType(CardType.Spell);
        }

        public bool IsTrap()
        {
            return this.HasType(CardType.Trap);
        }

        public bool IsExtraCard()
        {
            return this.HasType(CardType.Fusion) || this.HasType(CardType.Synchro) || this.HasType(CardType.Xyz) || this.HasType(CardType.Link);
        }

        public bool IsFaceup()
        {
            return this.HasPosition(CardPosition.FaceUp);
        }

        public bool IsFacedown()
        {
            return this.HasPosition(CardPosition.FaceDown);
        }

        public bool IsAttack()
        {
            return this.HasPosition(CardPosition.Attack);
        }

        public bool IsDefense()
        {
            return this.HasPosition(CardPosition.Defence);
        }

        public bool IsDisabled()
        {
            return this.Disabled != 0;
        }

        public bool IsCanRevive()
        {
            return this.ProcCompleted != 0 || !(this.IsExtraCard() || this.HasType(CardType.Ritual) || this.HasType(CardType.SpSummon));
        }

        public bool IsCode(int id)
        {
            return this.Id == id || this.Alias != 0 && this.Alias == id;
        }

        public bool IsCode(IList<int> ids)
        {
            return ids.Contains(this.Id) || this.Alias != 0 && ids.Contains(this.Alias);
        }

        public bool IsCode(params int[] ids)
        {
            return ids.Contains(this.Id) || this.Alias != 0 && ids.Contains(this.Alias);
        }

        public bool IsOriginalCode(int id)
        {
            return this.Id == id || this.Alias - this.Id < 10 && this.Alias == id;
        }

        public bool HasXyzMaterial()
        {
            return this.Overlays.Count > 0;
        }

        public bool HasXyzMaterial(int count)
        {
            return this.Overlays.Count >= count;
        }

        public bool HasXyzMaterial(int count, int cardid)
        {
            return this.Overlays.Count >= count && this.Overlays.Contains(cardid);
        }

        public int GetDefensePower()
        {
            return this.IsAttack() ? this.Attack : this.Defense;
        }

        public bool Equals(ClientCard card)
        {
            return ReferenceEquals(this, card);
        }
    }
}