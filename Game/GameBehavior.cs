using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using WindBot.Game.AI;
using YGOSharp.Network;
using YGOSharp.Network.Enums;
using YGOSharp.Network.Utils;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game
{
    public class GameBehavior
    {
        public GameClient Game { get; private set; }
        public YGOClient Connection { get; private set; }
        public Deck Deck { get; private set; }
        public Deck DeckForWin { get; private set; }
        public Deck DeckForLose { get; private set; }
        public string DeckCode { get; private set; }

        private readonly GameAI ai;

        private readonly IDictionary<StocMessage, Action<BinaryReader>> packets;
        private readonly IDictionary<GameMessage, Action<BinaryReader>> messages;

        private readonly Room room;
        private readonly Duel duel;
        private readonly int hand;
        private readonly bool debug;        
        private int selectHint;
        private GameMessage lastMessage;
        private int lastDuelResult;

        public GameBehavior(GameClient game)
        {
            this.Game = game;
            this.Connection = game.Connection;
            this.hand = game.Hand;
            this.debug = game.Debug;
            this.packets = new Dictionary<StocMessage, Action<BinaryReader>>();
            this.messages = new Dictionary<GameMessage, Action<BinaryReader>>();
            this.RegisterPackets();

            this.room = new Room();
            this.duel = new Duel();

            this.ai = new GameAI(this.Game, this.duel);
            this.ai.Executor = DecksManager.Instantiate(this.ai, this.duel);
            if(this.Game.DeckCode != null) {
                this.DeckCode = this.Game.DeckCode;
            } else {
                this.DeckCode = null;
                string deckName = this.Game.DeckFile ?? this.ai.Executor.Deck;
                this.Deck = Deck.Load(deckName);
                this.DeckForWin = Deck.Load("Win/" + deckName);
                this.DeckForLose = Deck.Load("Lose/" + deckName);
            }
            this.selectHint = 0;
            this.lastDuelResult = 2;
        }

        public int GetLocalPlayer(int player)
        {
            return this.duel.IsFirst ? player : 1 - player;
        }

        public void OnPacket(BinaryReader packet)
        {
            StocMessage id = (StocMessage)packet.ReadByte();
            if (id == StocMessage.GameMsg)
            {
                GameMessage msg = (GameMessage)packet.ReadByte();
                if (this.messages.ContainsKey(msg))
                {
                    this.messages[msg](packet);
                }

                this.lastMessage = msg;
                return;
            }
            if (this.packets.ContainsKey(id))
            {
                this.packets[id](packet);
            }
        }

        private void RegisterPackets()
        {
            this.packets.Add(StocMessage.JoinGame, this.OnJoinGame);
            this.packets.Add(StocMessage.TypeChange, this.OnTypeChange);
            this.packets.Add(StocMessage.HsPlayerEnter, this.OnPlayerEnter);
            this.packets.Add(StocMessage.HsPlayerChange, this.OnPlayerChange);
            this.packets.Add(StocMessage.SelectHand, this.OnSelectHand);
            this.packets.Add(StocMessage.SelectTp, this.OnSelectTp);
            this.packets.Add(StocMessage.TimeLimit, this.OnTimeLimit);
            this.packets.Add(StocMessage.Replay, this.OnReplay);
            this.packets.Add(StocMessage.DuelEnd, this.OnDuelEnd);
            this.packets.Add(StocMessage.Chat, this.OnChat);
            this.packets.Add(StocMessage.ChangeSide, this.OnChangeSide);
            this.packets.Add(StocMessage.ErrorMsg, this.OnErrorMsg);

            this.messages.Add(GameMessage.Retry, this.OnRetry);
            this.messages.Add(GameMessage.Start, this.OnStart);
            this.messages.Add(GameMessage.Hint, this.OnHint);
            this.messages.Add(GameMessage.Win, this.OnWin);
            this.messages.Add(GameMessage.Draw, this.OnDraw);
            this.messages.Add(GameMessage.ShuffleDeck, this.OnShuffleDeck);
            this.messages.Add(GameMessage.ShuffleHand, this.OnShuffleHand);
            this.messages.Add(GameMessage.ShuffleExtra, this.OnShuffleExtra);
            this.messages.Add(GameMessage.ShuffleSetCard, this.OnShuffleSetCard);
            this.messages.Add(GameMessage.TagSwap, this.OnTagSwap);
            this.messages.Add(GameMessage.NewTurn, this.OnNewTurn);
            this.messages.Add(GameMessage.NewPhase, this.OnNewPhase);
            this.messages.Add(GameMessage.Damage, this.OnDamage);
            this.messages.Add(GameMessage.PayLpCost, this.OnDamage);
            this.messages.Add(GameMessage.Recover, this.OnRecover);
            this.messages.Add(GameMessage.LpUpdate, this.OnLpUpdate);
            this.messages.Add(GameMessage.Move, this.OnMove);
            this.messages.Add(GameMessage.Swap, this.OnSwap);
            this.messages.Add(GameMessage.Attack, this.OnAttack);
            this.messages.Add(GameMessage.Battle, this.OnBattle);
            this.messages.Add(GameMessage.AttackDisabled, this.OnAttackDisabled);
            this.messages.Add(GameMessage.PosChange, this.OnPosChange);
            this.messages.Add(GameMessage.Chaining, this.OnChaining);
            this.messages.Add(GameMessage.ChainEnd, this.OnChainEnd);
            this.messages.Add(GameMessage.SortCard, this.OnCardSorting);
            this.messages.Add(GameMessage.SortChain, this.OnChainSorting);
            this.messages.Add(GameMessage.UpdateCard, this.OnUpdateCard);
            this.messages.Add(GameMessage.UpdateData, this.OnUpdateData);
            this.messages.Add(GameMessage.BecomeTarget, this.OnBecomeTarget);
            this.messages.Add(GameMessage.SelectBattleCmd, this.OnSelectBattleCmd);
            this.messages.Add(GameMessage.SelectCard, this.OnSelectCard);
            this.messages.Add(GameMessage.SelectUnselect, this.OnSelectUnselectCard);
            this.messages.Add(GameMessage.SelectChain, this.OnSelectChain);
            this.messages.Add(GameMessage.SelectCounter, this.OnSelectCounter);
            this.messages.Add(GameMessage.SelectDisfield, this.OnSelectDisfield);
            this.messages.Add(GameMessage.SelectEffectYn, this.OnSelectEffectYn);
            this.messages.Add(GameMessage.SelectIdleCmd, this.OnSelectIdleCmd);
            this.messages.Add(GameMessage.SelectOption, this.OnSelectOption);
            this.messages.Add(GameMessage.SelectPlace, this.OnSelectPlace);
            this.messages.Add(GameMessage.SelectPosition, this.OnSelectPosition);
            this.messages.Add(GameMessage.SelectSum, this.OnSelectSum);
            this.messages.Add(GameMessage.SelectTribute, this.OnSelectTribute);
            this.messages.Add(GameMessage.SelectYesNo, this.OnSelectYesNo);
            this.messages.Add(GameMessage.AnnounceAttrib, this.OnAnnounceAttrib);
            this.messages.Add(GameMessage.AnnounceCard, this.OnAnnounceCard);
            this.messages.Add(GameMessage.AnnounceNumber, this.OnAnnounceNumber);
            this.messages.Add(GameMessage.AnnounceRace, this.OnAnnounceRace);
            this.messages.Add(GameMessage.RockPaperScissors, this.OnRockPaperScissors);
            this.messages.Add(GameMessage.Equip, this.OnEquip);
            this.messages.Add(GameMessage.Unequip, this.OnUnEquip);
            this.messages.Add(GameMessage.CardTarget, this.OnCardTarget);
            this.messages.Add(GameMessage.CancelTarget, this.OnCancelTarget);
            this.messages.Add(GameMessage.Summoning, this.OnSummoning);
            this.messages.Add(GameMessage.Summoned, this.OnSummoned);
            this.messages.Add(GameMessage.SpSummoning, this.OnSpSummoning);
            this.messages.Add(GameMessage.SpSummoned, this.OnSpSummoned);
            this.messages.Add(GameMessage.FlipSummoning, this.OnSummoning);
            this.messages.Add(GameMessage.FlipSummoned, this.OnSummoned);
        }

        private BinaryWriter buildUpdateDeck(Deck targetDeck) {
            BinaryWriter deck = GamePacketFactory.Create(CtosMessage.UpdateDeck);
            if(this.DeckCode != null) {
                try {
                    byte[] deckContent = Convert.FromBase64String(this.DeckCode);
                    deck.Write(deckContent);
                } catch {
                    this.ai.OnDeckError("base64 decode");
                }
                return deck;
            }
            deck.Write(targetDeck.Cards.Count + targetDeck.ExtraCards.Count);
            //Logger.WriteLine("Main + Extra: " + targetDeck.Cards.Count + targetDeck.ExtraCards.Count);
            deck.Write(targetDeck.SideCards.Count);
            //Logger.WriteLine("Side: " + targetDeck.SideCards.Count);
            foreach (NamedCard card in targetDeck.Cards)
            {
                deck.Write(card.Id);
            }

            foreach (NamedCard card in targetDeck.ExtraCards)
            {
                deck.Write(card.Id);
            }

            foreach (NamedCard card in targetDeck.SideCards)
            {
                deck.Write(card.Id);
            }

            return deck;
        }

        private void OnJoinGame(BinaryReader packet)
        {
            /*int lflist = (int)*/ packet.ReadUInt32();
            /*int rule = */ packet.ReadByte();
            /*int mode = */ packet.ReadByte();
            int duel_rule = packet.ReadByte();
            this.ai.Duel.IsNewRule = (duel_rule >= 4);
            this.ai.Duel.IsNewRule2020 = (duel_rule >= 5);
            BinaryWriter deck = this.buildUpdateDeck(this.pickDeckOnResult());
            this.Connection.Send(deck);
            this.ai.OnJoinGame();
        }
        
        private Deck pickDeckOnResult() {
            if(this.DeckCode != null) {
                return null;
            }
            if(this.lastDuelResult == 0 && this.DeckForWin != null) {
                //Logger.WriteLine("Using deck for win: " + DeckForWin.SideCards[2].Name);
                return this.DeckForWin;
            }
            if(this.lastDuelResult == 1 && this.DeckForLose != null) {
                //Logger.WriteLine("Using deck for lose: " + DeckForLose.SideCards[2].Name);
                return this.DeckForLose;
            }
            //Logger.WriteLine("Using default deck.");
            return this.Deck;
        }

        private void OnChangeSide(BinaryReader packet)
        {
            BinaryWriter deck = this.buildUpdateDeck(this.pickDeckOnResult());
            this.Connection.Send(deck);
            this.ai.OnJoinGame();
        }

        private void OnTypeChange(BinaryReader packet)
        {
            int type = packet.ReadByte();
            int pos = type & 0xF;
            if (pos < 0 || pos > 3)
            {
                this.Connection.Close();
                return;
            }
            this.room.Position = pos;
            this.room.IsHost = ((type >> 4) & 0xF) != 0;
            this.room.IsReady[pos] = true;
            this.Connection.Send(CtosMessage.HsReady);
        }

        private void OnPlayerEnter(BinaryReader packet)
        {
            string name = packet.ReadUnicode(20);
            int pos = packet.ReadByte();
            if (pos < 8)
            {
                this.room.Names[pos] = name;
            }
        }

        private void OnPlayerChange(BinaryReader packet)
        {
            int change = packet.ReadByte();
            int pos = (change >> 4) & 0xF;
            int state = change & 0xF;
            if (pos > 3)
            {
                return;
            }

            if (state < 8)
            {
                string oldname = this.room.Names[pos];
                this.room.Names[pos] = null;
                this.room.Names[state] = oldname;
                this.room.IsReady[pos] = false;
                this.room.IsReady[state] = false;
            }
            else if (state == (int)PlayerChange.Ready)
            {
                this.room.IsReady[pos] = true;
            }
            else if (state == (int)PlayerChange.NotReady)
            {
                this.room.IsReady[pos] = false;
            }
            else if (state == (int)PlayerChange.Leave || state == (int)PlayerChange.Observe)
            {
                this.room.IsReady[pos] = false;
                this.room.Names[pos] = null;
                if (state == (int)PlayerChange.Leave && Config.GetBool("AutoQuit", false)) {
                    this.Connection.Close();
                    return;
                }
            }

            if (this.room.IsHost && this.room.IsReady[0] && this.room.IsReady[1])
            {
                this.Connection.Send(CtosMessage.HsStart);
            }
        }

        private void OnSelectHand(BinaryReader packet)
        {
            int result;
            if (this.hand > 0)
            {
                result = this.hand;
            }
            else
            {
                result = this.ai.OnRockPaperScissors();
            }

            this.Connection.Send(CtosMessage.HandResult, (byte)result);
        }

        private void OnSelectTp(BinaryReader packet)
        {
            bool start = this.ai.OnSelectHand();
            this.Connection.Send(CtosMessage.TpResult, (byte)(start ? 1 : 0));
        }

        private void OnTimeLimit(BinaryReader packet)
        {
            int player = this.GetLocalPlayer(packet.ReadByte());
            if (player == 0)
            {
                this.Connection.Send(CtosMessage.TimeConfirm);
            }
        }

        private void OnReplay(BinaryReader packet)
        {
            /*byte[] replay =*/ packet.ReadToEnd();

            /*
            const string directory = "Replays";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string otherName = _room.Position == 0 ? _room.Names[1] : _room.Names[0];
            string file = DateTime.Now.ToString("yyyy-MM-dd.HH-mm.") + otherName + ".yrp";
            string fullname = Path.Combine(directory, file);

            if (Regex.IsMatch(file, @"^[\w\-. ]+$"))
                File.WriteAllBytes(fullname, replay);
            */

            //Connection.Close();
        }
        
        private void OnDuelEnd(BinaryReader packet)
        {
            this.Connection.Close();
        }

        private void OnChat(BinaryReader packet)
        {
            int player = packet.ReadInt16();
            string message = packet.ReadUnicode(256);
            string myName = (player != 0) ? this.room.Names[1] : this.room.Names[0];
            string otherName = (player == 0) ? this.room.Names[1] : this.room.Names[0];
			if (this.debug) {
				if (player < 4)
                {
                    Logger.WriteLine(otherName + " say to " + myName + ": " + message);
                }
                else
                {
                    Logger.WriteLine("Server say to " + myName + ": " + message);
                }
            }
        }

        private void OnErrorMsg(BinaryReader packet)
        {
            int msg = packet.ReadByte();
            Logger.WriteLine("Got error: " + msg);
            // align
            packet.ReadByte();
            packet.ReadByte();
            packet.ReadByte();
            int pcode = packet.ReadInt32();
            if (msg == 2) //ERRMSG_DECKERROR
            {
                int code = pcode & 0xFFFFFFF;
                int flag = pcode >> 28;
                if (flag <= 5) //DECKERROR_CARDCOUNT
                {
                    NamedCard card = NamedCard.Get(code);
                    if (card != null)
                    {
                        this.ai.OnDeckError(card.Name);
                    }
                    else
                    {
                        this.ai.OnDeckError("Unknown Card");
                    }
                }
                else
                {
                    this.ai.OnDeckError("DECK");
                }
            }
            //Connection.Close();
        }

        private void OnRetry(BinaryReader packet)
        {
            this.ai.OnRetry();
            this.Connection.Close();
            throw new Exception("Got MSG_RETRY. Last message is " + this.lastMessage);
        }

        private void OnHint(BinaryReader packet)
        {
            int type = packet.ReadByte();
            int player = packet.ReadByte();
            int data = packet.ReadInt32();
            if (type == 1) // HINT_EVENT
            {
                if (data == 24) // battling
                {
                    this.duel.Fields[0].UnderAttack = false;
                    this.duel.Fields[1].UnderAttack = false;
                }
            }
            if (type == 3) // HINT_SELECTMSG
            {
                this.selectHint = data;
            }
        }

        private void OnStart(BinaryReader packet)
        {
            int type = packet.ReadByte();
            this.duel.IsFirst = (type & 0xF) == 0;
            this.duel.Turn = 0;
            int duel_rule = packet.ReadByte();
            this.ai.Duel.IsNewRule = (duel_rule >= 4);
            this.ai.Duel.IsNewRule2020 = (duel_rule >= 5);
            this.duel.Fields[this.GetLocalPlayer(0)].LifePoints = packet.ReadInt32();
            this.duel.Fields[this.GetLocalPlayer(1)].LifePoints = packet.ReadInt32();
            int deck = packet.ReadInt16();
            int extra = packet.ReadInt16();
            this.duel.Fields[this.GetLocalPlayer(0)].Init(deck, extra);
            deck = packet.ReadInt16();
            extra = packet.ReadInt16();
            this.duel.Fields[this.GetLocalPlayer(1)].Init(deck, extra);

            Logger.WriteLine("Duel started: " + this.room.Names[0] + " versus " + this.room.Names[1]);
            this.ai.OnStart();
        }

        private void OnWin(BinaryReader packet)
        {
            int result = this.GetLocalPlayer(packet.ReadByte());

            this.lastDuelResult = result;

            string otherName = this.room.Position == 0 ? this.room.Names[1] : this.room.Names[0];
            string textResult = (result == 2 ? "Draw" : result == 0 ? "Win" : "Lose");
            Logger.WriteLine("Duel finished against " + otherName + ", result: " + textResult);
        }

        private void OnDraw(BinaryReader packet)
        {
            int player = this.GetLocalPlayer(packet.ReadByte());
            int count = packet.ReadByte();
            if (this.debug)
            {
                Logger.WriteLine("(" + player.ToString() + " draw " + count.ToString() + " card)");
            }

            for (int i = 0; i < count; ++i)
            {
                this.duel.Fields[player].Deck.RemoveAt(this.duel.Fields[player].Deck.Count - 1);
                this.duel.Fields[player].Hand.Add(new ClientCard(0, CardLocation.Hand, -1));
            }
            this.ai.OnDraw(player);
        }

        private void OnShuffleDeck(BinaryReader packet)
        {
            int player = this.GetLocalPlayer(packet.ReadByte());
            foreach (ClientCard card in this.duel.Fields[player].Deck)
            {
                card.SetId(0);
            }
        }

        private void OnShuffleHand(BinaryReader packet)
        {
            int player = this.GetLocalPlayer(packet.ReadByte());
            packet.ReadByte();
            foreach (ClientCard card in this.duel.Fields[player].Hand)
            {
                card.SetId(packet.ReadInt32());
            }
        }

        private void OnShuffleExtra(BinaryReader packet)
        {
            int player = this.GetLocalPlayer(packet.ReadByte());
            packet.ReadByte();
            foreach (ClientCard card in this.duel.Fields[player].ExtraDeck)
            {
                if (!card.IsFaceup())
                {
                    card.SetId(packet.ReadInt32());
                }
            }
        }

        private void OnShuffleSetCard(BinaryReader packet)
        {
            int location = packet.ReadByte();
            int count = packet.ReadByte();
            ClientCard[] list = new ClientCard[5];
            for (int i = 0; i < count; ++i)
            {
                int player = this.GetLocalPlayer(packet.ReadByte());
                int loc = packet.ReadByte();
                int seq = packet.ReadByte();
                /*int sseq = */packet.ReadByte();
                ClientCard card = this.duel.GetCard(player, (CardLocation)loc, seq);
                if (card == null)
                {
                    continue;
                }

                list[i] = card;
                card.SetId(0);
            }
            for (int i = 0; i < count; ++i)
            {
                int player = this.GetLocalPlayer(packet.ReadByte());
                int loc = packet.ReadByte();
                int seq = packet.ReadByte();
                /*int sseq = */packet.ReadByte();
                ClientCard card = this.duel.GetCard(player, (CardLocation)loc, seq);
                if (card == null)
                {
                    continue;
                }

                ClientCard[] zone = (loc == (int)CardLocation.MonsterZone) ? this.duel.Fields[player].MonsterZone : this.duel.Fields[player].SpellZone;
                zone[seq] = list[i];
            }
        }

        private void OnTagSwap(BinaryReader packet)
        {
            int player = this.GetLocalPlayer(packet.ReadByte());
            int mcount = packet.ReadByte();
            int ecount = packet.ReadByte();
            /*int pcount = */ packet.ReadByte();
            int hcount = packet.ReadByte();
            /*int topcode =*/ packet.ReadInt32();
            this.duel.Fields[player].Deck.Clear();
            for (int i = 0; i < mcount; ++i)
            {
                this.duel.Fields[player].Deck.Add(new ClientCard(0, CardLocation.Deck, -1));
            }
            this.duel.Fields[player].ExtraDeck.Clear();
            for (int i = 0; i < ecount; ++i)
            {
                int code = packet.ReadInt32() & 0x7fffffff;
                this.duel.Fields[player].ExtraDeck.Add(new ClientCard(code, CardLocation.Extra, -1));
            }
            this.duel.Fields[player].Hand.Clear();
            for (int i = 0; i < hcount; ++i)
            {
                int code = packet.ReadInt32();
                this.duel.Fields[player].Hand.Add(new ClientCard(code, CardLocation.Hand,-1));
            }
        }

        private void OnNewTurn(BinaryReader packet)
        {
            this.duel.Turn++;
            this.duel.Player = this.GetLocalPlayer(packet.ReadByte());
            this.ai.OnNewTurn();
        }

        private void OnNewPhase(BinaryReader packet)
        {
            this.duel.Phase = (DuelPhase)packet.ReadInt16();
            if (this.debug && this.duel.Phase == DuelPhase.Standby)
            {
                Logger.WriteLine("*********Bot Hand*********");
                foreach (ClientCard card in this.duel.Fields[0].Hand)
                {
                    Logger.WriteLine(card.Name);
                }
                Logger.WriteLine("*********Bot Spell*********");
                foreach (ClientCard card in this.duel.Fields[0].SpellZone)
                {
                    Logger.WriteLine(card?.Name);
                }
                Logger.WriteLine("*********Bot Monster*********");
                foreach (ClientCard card in this.duel.Fields[0].MonsterZone)
                {
                    Logger.WriteLine(card?.Name);
                }
                Logger.WriteLine("*********Finish*********");
            }
            if (this.debug)
            {
                Logger.WriteLine("(Go to " + (this.duel.Phase.ToString()) + ")");
            }

            this.duel.LastSummonPlayer = -1;
            this.duel.SummoningCards.Clear();
            this.duel.LastSummonedCards.Clear();
            this.duel.Fields[0].BattlingMonster = null;
            this.duel.Fields[1].BattlingMonster = null;
            this.duel.Fields[0].UnderAttack = false;
            this.duel.Fields[1].UnderAttack = false;
            List<ClientCard> monsters = this.duel.Fields[0].GetMonsters();
            foreach (ClientCard monster in monsters)
            {
                monster.Attacked = false;
            }
            this.selectHint = 0;
            this.ai.OnNewPhase();
        }

        private void OnDamage(BinaryReader packet)
        {
            int player = this.GetLocalPlayer(packet.ReadByte());
            int final = this.duel.Fields[player].LifePoints - packet.ReadInt32();
            if (final < 0)
            {
                final = 0;
            }

            if (this.debug)
            {
                Logger.WriteLine("(" + player.ToString() + " got damage , LifePoint left = " + final.ToString() + ")");
            }

            this.duel.Fields[player].LifePoints = final;
        }

        private void OnRecover(BinaryReader packet)
        {
            int player = this.GetLocalPlayer(packet.ReadByte());
            int final = this.duel.Fields[player].LifePoints + packet.ReadInt32();
            if (this.debug)
            {
                Logger.WriteLine("(" + player.ToString() + " got healed , LifePoint left = " + final.ToString() + ")");
            }

            this.duel.Fields[player].LifePoints = final;
        }

        private void OnLpUpdate(BinaryReader packet)
        {
            int player = this.GetLocalPlayer(packet.ReadByte());
            this.duel.Fields[player].LifePoints = packet.ReadInt32();
        }

        private void OnMove(BinaryReader packet)
        {
            // TODO: update equip cards and target cards
            int cardId = packet.ReadInt32();
            int previousControler = this.GetLocalPlayer(packet.ReadByte());
            int previousLocation = packet.ReadByte();
            int previousSequence = packet.ReadSByte();
            /*int previousPosotion = */packet.ReadSByte();
            int currentControler = this.GetLocalPlayer(packet.ReadByte());
            int currentLocation = packet.ReadByte();
            int currentSequence = packet.ReadSByte();
            int currentPosition = packet.ReadSByte();
            packet.ReadInt32(); // reason

            ClientCard card = this.duel.GetCard(previousControler, (CardLocation)previousLocation, previousSequence);
            if ((previousLocation & (int)CardLocation.Overlay) != 0)
            {
                previousLocation = previousLocation & 0x7f;
                card = this.duel.GetCard(previousControler, (CardLocation)previousLocation, previousSequence);
                if (card != null)
                {
                    if (this.debug)
                    {
                        Logger.WriteLine("(" + previousControler.ToString() + " 's " + (card.Name ?? "UnKnowCard") + " deattach " + (NamedCard.Get(cardId)?.Name) + ")");
                    }

                    card.Overlays.Remove(cardId);
                }
                previousLocation = 0; // the card is removed when it go to overlay, so here we treat it as a new card
            }
            else
            {
                this.duel.RemoveCard((CardLocation)previousLocation, card, previousControler, previousSequence);
            }

            if ((currentLocation & (int)CardLocation.Overlay) != 0)
            {
                currentLocation = currentLocation & 0x7f;
                card = this.duel.GetCard(currentControler, (CardLocation)currentLocation, currentSequence);
                if (card != null)
                {
                    if (this.debug)
                    {
                        Logger.WriteLine("(" + previousControler.ToString() + " 's " + (card.Name ?? "UnKnowCard") + " overlay " + (NamedCard.Get(cardId)?.Name) + ")");
                    }

                    card.Overlays.Add(cardId);
                }
            }
            else
            {
                if (previousLocation == 0)
                {
                    if (this.debug)
                    {
                        Logger.WriteLine("(" + previousControler.ToString() + " 's " + (NamedCard.Get(cardId)?.Name)
                        + " appear in " + (CardLocation)currentLocation + ")");
                    }

                    this.duel.AddCard((CardLocation)currentLocation, cardId, currentControler, currentSequence, currentPosition);
                }
                else
                {
                    this.duel.AddCard((CardLocation)currentLocation, card, currentControler, currentSequence, currentPosition, cardId);
                    if (card != null && previousLocation != currentLocation)
                    {
                        card.IsSpecialSummoned = false;
                    }

                    if (this.debug && card != null)
                    {
                        Logger.WriteLine("(" + previousControler.ToString() + " 's " + (card.Name ?? "UnKnowCard")
                        + " from " +
                        (CardLocation)previousLocation + " move to " + (CardLocation)currentLocation + ")");
                    }
                }
            }
        }

        private void OnSwap(BinaryReader packet)
        {
            int cardId1 = packet.ReadInt32();
            int controler1 = this.GetLocalPlayer(packet.ReadByte());
            int location1 = packet.ReadByte();
            int sequence1 = packet.ReadByte();
            packet.ReadByte();
            int cardId2 = packet.ReadInt32();
            int controler2 = this.GetLocalPlayer(packet.ReadByte());
            int location2 = packet.ReadByte();
            int sequence2 = packet.ReadByte();
            packet.ReadByte();
            ClientCard card1 = this.duel.GetCard(controler1, (CardLocation)location1, sequence1);
            ClientCard card2 = this.duel.GetCard(controler2, (CardLocation)location2, sequence2);
            if (card1 == null || card2 == null)
            {
                return;
            }

            this.duel.RemoveCard((CardLocation)location1, card1, controler1, sequence1);
            this.duel.RemoveCard((CardLocation)location2, card2, controler2, sequence2);
            this.duel.AddCard((CardLocation)location2, card1, controler2, sequence2, card1.Position, cardId1);
            this.duel.AddCard((CardLocation)location1, card2, controler1, sequence1, card2.Position, cardId2);
        }

        private void OnAttack(BinaryReader packet)
        {
            int ca = this.GetLocalPlayer(packet.ReadByte());
            int la = packet.ReadByte();
            int sa = packet.ReadByte();
            packet.ReadByte(); //
            int cd = this.GetLocalPlayer(packet.ReadByte());
            int ld = packet.ReadByte();
            int sd = packet.ReadByte();
            packet.ReadByte(); //

            ClientCard attackcard = this.duel.GetCard(ca, (CardLocation)la, sa);
            ClientCard defendcard = this.duel.GetCard(cd, (CardLocation)ld, sd);
            if (this.debug)
            {
                if (defendcard == null)
                {
                    Logger.WriteLine("(" + (attackcard.Name ?? "UnKnowCard") + " direct attack!!)");
                }
                else
                {
                    Logger.WriteLine("(" + ca.ToString() + " 's " + (attackcard.Name ?? "UnKnowCard") + " attack  " + cd.ToString() + " 's " + (defendcard.Name ?? "UnKnowCard") + ")");
                }
            }
            this.duel.Fields[attackcard.Controller].BattlingMonster = attackcard;
            this.duel.Fields[1 - attackcard.Controller].BattlingMonster = defendcard;
            this.duel.Fields[1 - attackcard.Controller].UnderAttack = true;

            if (ld == 0 && ca != 0)
            {
                this.ai.OnDirectAttack(attackcard);
            }
        }

        private void OnBattle(BinaryReader packet)
        {
            this.duel.Fields[0].UnderAttack = false;
            this.duel.Fields[1].UnderAttack = false;
        }

        private void OnAttackDisabled(BinaryReader packet)
        {
            this.duel.Fields[0].UnderAttack = false;
            this.duel.Fields[1].UnderAttack = false;
        }

        private void OnPosChange(BinaryReader packet)
        {
            packet.ReadInt32(); // card id
            int pc = this.GetLocalPlayer(packet.ReadByte());
            int pl = packet.ReadByte();
            int ps = packet.ReadSByte();
            int pp = packet.ReadSByte();
            int cp = packet.ReadSByte();
            ClientCard card = this.duel.GetCard(pc, (CardLocation)pl, ps);
            if (card != null)
            {
                card.Position = cp;
                if ((pp & (int) CardPosition.FaceUp) > 0 && (cp & (int) CardPosition.FaceDown) > 0)
                {
                    card.ClearCardTargets();
                }

                if (this.debug)
                {
                    Logger.WriteLine("(" + (card.Name ?? "UnKnowCard") + " change position to " + (CardPosition)cp + ")");
                }
            }
        }

        private void OnChaining(BinaryReader packet)
        {
            int cardId = packet.ReadInt32();
            int pcc = this.GetLocalPlayer(packet.ReadByte());
            int pcl = packet.ReadByte();
            int pcs = packet.ReadSByte();
            int subs = packet.ReadSByte();
            ClientCard card = this.duel.GetCard(pcc, pcl, pcs, subs);
            if (card.Id == 0)
            {
                card.SetId(cardId);
            }

            int cc = this.GetLocalPlayer(packet.ReadByte());
            if (this.debug)
            {
                if (card != null)
                {
                    Logger.WriteLine("(" + cc.ToString() + " 's " + (card.Name ?? "UnKnowCard") + " activate effect)");
                }
            }

            this.ai.OnChaining(card, cc);
            //_duel.ChainTargets.Clear();
            this.duel.ChainTargetOnly.Clear();
            this.duel.LastSummonPlayer = -1;
            this.duel.CurrentChain.Add(card);
            this.duel.LastChainPlayer = cc;

        }

        private void OnChainEnd(BinaryReader packet)
        {
            this.ai.OnChainEnd();
            this.duel.LastChainPlayer = -1;
            this.duel.CurrentChain.Clear();
            this.duel.ChainTargets.Clear();
            this.duel.ChainTargetOnly.Clear();
        }

        private void OnCardSorting(BinaryReader packet)
        {
            /*int player =*/
            this.GetLocalPlayer(packet.ReadByte());
            IList<ClientCard> originalCards = new List<ClientCard>();
            IList<ClientCard> cards = new List<ClientCard>();
            int count = packet.ReadByte();
            for (int i = 0; i < count; ++i)
            {
                int id = packet.ReadInt32();
                int controler = this.GetLocalPlayer(packet.ReadByte());
                CardLocation loc = (CardLocation)packet.ReadByte();
                int seq = packet.ReadByte();
                ClientCard card;
                if (((int)loc & (int)CardLocation.Overlay) != 0)
                {
                    card = new ClientCard(id, CardLocation.Overlay, -1);
                }
                else
                {
                    card = this.duel.GetCard(controler, loc, seq);
                }

                if (card == null)
                {
                    continue;
                }

                if (id != 0)
                {
                    card.SetId(id);
                }

                originalCards.Add(card);
                cards.Add(card);
            }

            IList<ClientCard> selected = this.ai.OnCardSorting(cards);
            byte[] result = new byte[count];
            for (int i = 0; i < count; ++i)
            {
                int id = 0;
                for (int j = 0; j < count; ++j)
                {
                    if (selected[j] == null)
                    {
                        continue;
                    }

                    if (selected[j].Equals(originalCards[i]))
                    {
                        id = j;
                        break;
                    }
                }
                result[i] = (byte)id;
            }

            BinaryWriter reply = GamePacketFactory.Create(CtosMessage.Response);
            reply.Write(result);
            this.Connection.Send(reply);
        }

        private void OnChainSorting(BinaryReader packet)
        {
            /*BinaryWriter writer =*/ GamePacketFactory.Create(CtosMessage.Response);
            this.Connection.Send(CtosMessage.Response, -1);
        }

        private void OnUpdateCard(BinaryReader packet)
        {
            int player = this.GetLocalPlayer(packet.ReadByte());
            int loc = packet.ReadByte();
            int seq = packet.ReadByte();

            packet.ReadInt32(); // ???

            ClientCard card = this.duel.GetCard(player, (CardLocation)loc, seq);

            card?.Update(packet, this.duel);
        }

        private void OnUpdateData(BinaryReader packet)
        {
            int player = this.GetLocalPlayer(packet.ReadByte());
            CardLocation loc = (CardLocation)packet.ReadByte();
            IList<ClientCard> cards = null;
            switch (loc)
            {
                case CardLocation.Hand:
                    cards = this.duel.Fields[player].Hand;
                    break;
                case CardLocation.MonsterZone:
                    cards = this.duel.Fields[player].MonsterZone;
                    break;
                case CardLocation.SpellZone:
                    cards = this.duel.Fields[player].SpellZone;
                    break;
                case CardLocation.Grave:
                    cards = this.duel.Fields[player].Graveyard;
                    break;
                case CardLocation.Removed:
                    cards = this.duel.Fields[player].Banished;
                    break;
                case CardLocation.Deck:
                    cards = this.duel.Fields[player].Deck;
                    break;
                case CardLocation.Extra:
                    cards = this.duel.Fields[player].ExtraDeck;
                    break;
            }
            if (cards != null)
            {
                foreach (ClientCard card in cards)
                {
                    int len = packet.ReadInt32();
                    long pos = packet.BaseStream.Position;
                    if (len > 8)
                    {
                        card.Update(packet, this.duel);
                    }

                    packet.BaseStream.Position = pos + len - 4;
                }
            }
        }

        private void OnBecomeTarget(BinaryReader packet)
        {
            int count = packet.ReadByte();
            for (int i = 0; i < count; ++i)
            {
                int player = this.GetLocalPlayer(packet.ReadByte());
                int loc = packet.ReadByte();
                int seq = packet.ReadByte();
                /*int sseq = */packet.ReadByte();
                ClientCard card = this.duel.GetCard(player, (CardLocation)loc, seq);
                if (card == null)
                {
                    continue;
                }

                if (this.debug)
                {
                    Logger.WriteLine("(" + (CardLocation)loc + " 's " + (card.Name ?? "UnKnowCard") + " become target)");
                }

                this.duel.ChainTargets.Add(card);
                this.duel.ChainTargetOnly.Add(card);
            }
        }

        private void OnSelectBattleCmd(BinaryReader packet)
        {
            packet.ReadByte(); // player
            this.duel.BattlePhase = new BattlePhase();
            BattlePhase battle = this.duel.BattlePhase;

            int count = packet.ReadByte();
            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32(); // card id
                int con = this.GetLocalPlayer(packet.ReadByte());
                CardLocation loc = (CardLocation)packet.ReadByte();
                int seq = packet.ReadByte();
                int desc = packet.ReadInt32();

                ClientCard card = this.duel.GetCard(con, loc, seq);
                if (card != null)
                {
                    card.ActionIndex[0] = i;
                    battle.ActivableCards.Add(card);
                    battle.ActivableDescs.Add(desc);
                }
            }

            count = packet.ReadByte();
            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32(); // card id
                int con = this.GetLocalPlayer(packet.ReadByte());
                CardLocation loc = (CardLocation)packet.ReadByte();
                int seq = packet.ReadByte();
                int diratt = packet.ReadByte();

                ClientCard card = this.duel.GetCard(con, loc, seq);
                if (card != null)
                {
                    card.ActionIndex[1] = i;
                    if (diratt > 0)
                    {
                        card.CanDirectAttack = true;
                    }
                    else
                    {
                        card.CanDirectAttack = false;
                    }

                    battle.AttackableCards.Add(card);
                    card.Attacked = false;
                }
            }
            List<ClientCard> monsters = this.duel.Fields[0].GetMonsters();
            foreach (ClientCard monster in monsters)
            {
                if (!battle.AttackableCards.Contains(monster))
                {
                    monster.Attacked = true;
                }
            }

            battle.CanMainPhaseTwo = packet.ReadByte() != 0;
            battle.CanEndPhase = packet.ReadByte() != 0;

            this.Connection.Send(CtosMessage.Response, this.ai.OnSelectBattleCmd(battle).ToValue());
        }

        private void InternalOnSelectCard(BinaryReader packet, Func<IList<ClientCard>, int, int, int, bool, IList<ClientCard>> func)
        {
            packet.ReadByte(); // player
            bool cancelable = packet.ReadByte() != 0;
            int min = packet.ReadByte();
            int max = packet.ReadByte();

            IList<ClientCard> cards = new List<ClientCard>();
            int count = packet.ReadByte();
            for (int i = 0; i < count; ++i)
            {
                int id = packet.ReadInt32();
                int player = this.GetLocalPlayer(packet.ReadByte());
                CardLocation loc = (CardLocation)packet.ReadByte();
                int seq = packet.ReadByte();
                packet.ReadByte(); // pos
                ClientCard card;
                if (((int)loc & (int)CardLocation.Overlay) != 0)
                {
                    card = new ClientCard(id, CardLocation.Overlay, -1);
                }
                else
                {
                    card = this.duel.GetCard(player, loc, seq);
                }

                if (card == null)
                {
                    continue;
                }

                if (card.Id == 0)
                {
                    card.SetId(id);
                }

                cards.Add(card);
            }

            IList<ClientCard> selected = func(cards, min, max, this.selectHint, cancelable);
            this.selectHint = 0;

            if (selected.Count == 0 && cancelable)
            {
                this.Connection.Send(CtosMessage.Response, -1);
                return;
            }

            byte[] result = new byte[selected.Count + 1];
            result[0] = (byte)selected.Count;
            for (int i = 0; i < selected.Count; ++i)
            {
                int id = 0;
                for (int j = 0; j < count; ++j)
                {
                    if (cards[j] == null)
                    {
                        continue;
                    }

                    if (cards[j].Equals(selected[i]))
                    {
                        id = j;
                        break;
                    }
                }
                result[i + 1] = (byte)id;
            }

            BinaryWriter reply = GamePacketFactory.Create(CtosMessage.Response);
            reply.Write(result);
            this.Connection.Send(reply);
        }

        private void InternalOnSelectUnselectCard(BinaryReader packet, Func<IList<ClientCard>, int, int, int, bool, IList<ClientCard>> func)
        {
            packet.ReadByte(); // player
            bool finishable = packet.ReadByte() != 0;
            bool cancelable = packet.ReadByte() != 0 || finishable;
            int min = packet.ReadByte();
            int max = packet.ReadByte();

            IList<ClientCard> cards = new List<ClientCard>();
            int count = packet.ReadByte();
            for (int i = 0; i < count; ++i)
            {
                int id = packet.ReadInt32();
                int player = this.GetLocalPlayer(packet.ReadByte());
                CardLocation loc = (CardLocation)packet.ReadByte();
                int seq = packet.ReadByte();
                packet.ReadByte(); // pos
                ClientCard card;
                if (((int)loc & (int)CardLocation.Overlay) != 0)
                {
                    card = new ClientCard(id, CardLocation.Overlay, -1);
                }
                else
                {
                    card = this.duel.GetCard(player, loc, seq);
                }

                if (card == null)
                {
                    continue;
                }

                if (card.Id == 0)
                {
                    card.SetId(id);
                }

                cards.Add(card);
            }
            int count2 = packet.ReadByte();
            for (int i = 0; i < count2; ++i)
            {
                int id = packet.ReadInt32();
                int player = this.GetLocalPlayer(packet.ReadByte());
                CardLocation loc = (CardLocation)packet.ReadByte();
                int seq = packet.ReadByte();
                packet.ReadByte(); // pos
            }

            IList<ClientCard> selected = func(cards, (finishable ? 0 : 1), 1, this.selectHint, cancelable);

            if (selected.Count == 0 && cancelable)
            {
                this.Connection.Send(CtosMessage.Response, -1);
                return;
            }

            byte[] result = new byte[selected.Count + 1];
            result[0] = (byte)selected.Count;
            for (int i = 0; i < selected.Count; ++i)
            {
                int id = 0;
                for (int j = 0; j < count; ++j)
                {
                    if (cards[j] == null)
                    {
                        continue;
                    }

                    if (cards[j].Equals(selected[i]))
                    {
                        id = j;
                        break;
                    }
                }
                result[i + 1] = (byte)id;
            }

            BinaryWriter reply = GamePacketFactory.Create(CtosMessage.Response);
            reply.Write(result);
            this.Connection.Send(reply);
        }

        private void OnSelectCard(BinaryReader packet)
        {
            this.InternalOnSelectCard(packet, this.ai.OnSelectCard);
        }

        private void OnSelectUnselectCard(BinaryReader packet)
        {
            this.InternalOnSelectUnselectCard(packet, this.ai.OnSelectCard);
        }

        private void OnSelectChain(BinaryReader packet)
        {
            packet.ReadByte(); // player
            int count = packet.ReadByte();
            packet.ReadByte(); // specount
            bool forced = packet.ReadByte() != 0;
            packet.ReadInt32(); // hint1
            packet.ReadInt32(); // hint2

            IList<ClientCard> cards = new List<ClientCard>();
            IList<int> descs = new List<int>();

            for (int i = 0; i < count; ++i)
            {
                packet.ReadByte(); // flag
                packet.ReadInt32(); // card id
                int con = this.GetLocalPlayer(packet.ReadByte());
                int loc = packet.ReadByte();
                int seq = packet.ReadByte();
                int sseq = packet.ReadByte();

                int desc = packet.ReadInt32();
                if (desc == 221) // trigger effect
                {
                    desc = 0;
                }
                cards.Add(this.duel.GetCard(con, loc, seq, sseq));
                descs.Add(desc);
            }

            if (cards.Count == 0)
            {
                this.Connection.Send(CtosMessage.Response, -1);
                return;
            }

            if (cards.Count == 1 && forced)
            {
                this.Connection.Send(CtosMessage.Response, 0);
                return;
            }

            this.Connection.Send(CtosMessage.Response, this.ai.OnSelectChain(cards, descs, forced));
        }

        private void OnSelectCounter(BinaryReader packet)
        {
            packet.ReadByte(); // player
            int type = packet.ReadInt16();
            int quantity = packet.ReadInt16();

            IList<ClientCard> cards = new List<ClientCard>();
            IList<int> counters = new List<int>();
            int count = packet.ReadByte();
            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32(); // card id
                int player = this.GetLocalPlayer(packet.ReadByte());
                CardLocation loc = (CardLocation) packet.ReadByte();
                int seq = packet.ReadByte();
                int num = packet.ReadInt16();
                cards.Add(this.duel.GetCard(player, loc, seq));
                counters.Add(num);
            }

            IList<int> used = this.ai.OnSelectCounter(type, quantity, cards, counters);
            byte[] result = new byte[used.Count * 2];
            for (int i = 0; i < used.Count; ++i)
            {
                result[i * 2] = (byte)(used[i] & 0xff);
                result[i * 2 + 1] = (byte)(used[i] >> 8);
            }
            BinaryWriter reply = GamePacketFactory.Create(CtosMessage.Response);
            reply.Write(result);
            this.Connection.Send(reply);
        }

        private void OnSelectDisfield(BinaryReader packet)
        {
            this.OnSelectPlace(packet);
        }

        private void OnSelectEffectYn(BinaryReader packet)
        {
            packet.ReadByte(); // player

            int cardId = packet.ReadInt32();
            int player = this.GetLocalPlayer(packet.ReadByte());
            CardLocation loc = (CardLocation)packet.ReadByte();
            int seq = packet.ReadByte();
            packet.ReadByte();
            int desc = packet.ReadInt32();

            if (desc == 0 || desc == 221)
            {
                // 0: phase trigger effect
                // 221: trigger effect
                // for compatibility
                desc = -1;
            }

            ClientCard card = this.duel.GetCard(player, loc, seq);
            if (card == null)
            {
                this.Connection.Send(CtosMessage.Response, 0);
                return;
            }
            
            if (card.Id == 0)
            {
                card.SetId(cardId);
            }

            int reply = this.ai.OnSelectEffectYn(card, desc) ? (1) : (0);
            this.Connection.Send(CtosMessage.Response, reply);
        }

        private void OnSelectIdleCmd(BinaryReader packet)
        {
            packet.ReadByte(); // player

            this.duel.MainPhase = new MainPhase();
            MainPhase main = this.duel.MainPhase;
            int count;
            for (int k = 0; k < 5; k++)
            {
                count = packet.ReadByte();
                for (int i = 0; i < count; ++i)
                {
                    packet.ReadInt32(); // card id
                    int con = this.GetLocalPlayer(packet.ReadByte());
                    CardLocation loc = (CardLocation)packet.ReadByte();
                    int seq = packet.ReadByte();
                    ClientCard card = this.duel.GetCard(con, loc, seq);
                    if (card == null)
                    {
                        continue;
                    }

                    card.ActionIndex[k] = i;
                    switch (k)
                    {
                        case 0:
                            main.SummonableCards.Add(card);
                            break;
                        case 1:
                            main.SpecialSummonableCards.Add(card);
                            break;
                        case 2:
                            main.ReposableCards.Add(card);
                            break;
                        case 3:
                            main.MonsterSetableCards.Add(card);
                            break;
                        case 4:
                            main.SpellSetableCards.Add(card);
                            break;
                    }
                }
            }
            count = packet.ReadByte();
            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32(); // card id
                int con = this.GetLocalPlayer(packet.ReadByte());
                CardLocation loc = (CardLocation)packet.ReadByte();
                int seq = packet.ReadByte();
                int desc = packet.ReadInt32();

                ClientCard card = this.duel.GetCard(con, loc, seq);
                if (card == null)
                {
                    continue;
                }

                card.ActionIndex[5] = i;
                if (card.ActionActivateIndex.ContainsKey(desc))
                {
                    card.ActionActivateIndex.Remove(desc);
                }

                card.ActionActivateIndex.Add(desc, i);
                main.ActivableCards.Add(card);
                main.ActivableDescs.Add(desc);
            }

            main.CanBattlePhase = packet.ReadByte() != 0;
            main.CanEndPhase = packet.ReadByte() != 0;
            packet.ReadByte(); // CanShuffle

            this.Connection.Send(CtosMessage.Response, this.ai.OnSelectIdleCmd(main).ToValue());
        }

        private void OnSelectOption(BinaryReader packet)
        {
            IList<int> options = new List<int>();
            packet.ReadByte(); // player
            int count = packet.ReadByte();
            for (int i = 0; i < count; ++i)
            {
                options.Add(packet.ReadInt32());
            }

            this.Connection.Send(CtosMessage.Response, this.ai.OnSelectOption(options));
        }

        private void OnSelectPlace(BinaryReader packet)
        {
            packet.ReadByte(); // player
            packet.ReadByte(); // min
            int field = ~packet.ReadInt32();

            int player;
            CardLocation location;
            int filter;

            if ((field & 0x7f) != 0)
            {
                player = 0;
                location = CardLocation.MonsterZone;
                filter = field & Zones.MonsterZones;
            }
            else if ((field & 0x1f00) != 0)
            {
                player = 0;
                location = CardLocation.SpellZone;
                filter = (field >> 8) & Zones.SpellZones;
            }
            else if ((field & 0xc000) != 0)
            {
                player = 0;
                location = CardLocation.PendulumZone;
                filter = (field >> 14) & Zones.PendulumZones;
            }
            else if ((field & 0x7f0000) != 0)
            {
                player = 1;
                location = CardLocation.MonsterZone;
                filter = (field >> 16) & Zones.MonsterZones;
            }
            else if ((field & 0x1f000000) != 0)
            {
                player = 1;
                location = CardLocation.SpellZone;
                filter = (field >> 24) & Zones.SpellZones;
            }
            else
            {
                player = 1;
                location = CardLocation.PendulumZone;
                filter = (field >> 30) & Zones.PendulumZones;
            }

            int selected = this.ai.OnSelectPlace(this.selectHint, player, location, filter);
            this.selectHint = 0;

            byte[] resp = new byte[3];
            resp[0] = (byte)this.GetLocalPlayer(player);

            if (location != CardLocation.PendulumZone)
            {
                resp[1] = (byte)location;
                if ((selected & filter) > 0)
                {
                    filter &= selected;
                }

                if ((filter & Zones.MonsterZone3) != 0)
                {
                    resp[2] = 2;
                }
                else if ((filter & Zones.MonsterZone2) != 0)
                {
                    resp[2] = 1;
                }
                else if ((filter & Zones.MonsterZone4) != 0)
                {
                    resp[2] = 3;
                }
                else if ((filter & Zones.MonsterZone1) != 0)
                {
                    resp[2] = 0;
                }
                else if ((filter & Zones.MonsterZone5) != 0)
                {
                    resp[2] = 4;
                }
                else if ((filter & Zones.ExtraZone2) != 0)
                {
                    resp[2] = 6;
                }
                else if ((filter & Zones.ExtraZone1) != 0)
                {
                    resp[2] = 5;
                }
            }
            else
            {
                resp[1] = (byte)CardLocation.SpellZone;
                if ((selected & filter) > 0)
                {
                    filter &= selected;
                }

                if ((filter & Zones.MonsterZone1) != 0)
                {
                    resp[2] = 6;
                }

                if ((filter & Zones.MonsterZone2) != 0)
                {
                    resp[2] = 7;
                }
            }

            BinaryWriter reply = GamePacketFactory.Create(CtosMessage.Response);
            reply.Write(resp);
            this.Connection.Send(reply);
        }

        private void OnSelectPosition(BinaryReader packet)
        {
            packet.ReadByte(); // player
            int cardId = packet.ReadInt32();
            int pos = packet.ReadByte();
            if (pos == 0x1 || pos == 0x2 || pos == 0x4 || pos == 0x8)
            {
                this.Connection.Send(CtosMessage.Response, pos);
                return;
            }
            IList<CardPosition> positions = new List<CardPosition>();
            if ((pos & (int)CardPosition.FaceUpAttack) != 0)
            {
                positions.Add(CardPosition.FaceUpAttack);
            }

            if ((pos & (int)CardPosition.FaceDownAttack) != 0)
            {
                positions.Add(CardPosition.FaceDownAttack);
            }

            if ((pos & (int)CardPosition.FaceUpDefence) != 0)
            {
                positions.Add(CardPosition.FaceUpDefence);
            }

            if ((pos & (int)CardPosition.FaceDownDefence) != 0)
            {
                positions.Add(CardPosition.FaceDownDefence);
            }

            this.Connection.Send(CtosMessage.Response, (int)this.ai.OnSelectPosition(cardId, positions));
        }

        private void OnSelectSum(BinaryReader packet)
        {
            bool mode = packet.ReadByte() == 0;
            packet.ReadByte(); // player
            int sumval = packet.ReadInt32();
            int min = packet.ReadByte();
            int max = packet.ReadByte();

            if (max <= 0)
            {
                max = 99;
            }

            IList<ClientCard> mandatoryCards = new List<ClientCard>();
            IList<ClientCard> cards = new List<ClientCard>();

            for (int j = 0; j < 2; ++j)
            {
                int count = packet.ReadByte();
                for (int i = 0; i < count; ++i)
                {
                    int cardId = packet.ReadInt32();
                    int player = this.GetLocalPlayer(packet.ReadByte());
                    CardLocation loc = (CardLocation)packet.ReadByte();
                    int seq = packet.ReadByte();
                    ClientCard card = this.duel.GetCard(player, loc, seq);
                    if (cardId != 0 && card.Id != cardId)
                    {
                        card.SetId(cardId);
                    }

                    card.SelectSeq = i;
                    int OpParam = packet.ReadInt32();
                    int OpParam1 = OpParam & 0xffff;
                    int OpParam2 = OpParam >> 16;
                    if (OpParam2 > 0 && OpParam1 > OpParam2)
                    {
                        card.OpParam1 = OpParam2;
                        card.OpParam2 = OpParam1;
                    }
                    else
                    {
                        card.OpParam1 = OpParam1;
                        card.OpParam2 = OpParam2;
                    }
                    if (j == 0)
                    {
                        mandatoryCards.Add(card);
                    }
                    else
                    {
                        cards.Add(card);
                    }
                }
            }

            for (int k = 0; k < mandatoryCards.Count; ++k)
            {
                sumval -= mandatoryCards[k].OpParam1;
            }

            IList<ClientCard> selected = this.ai.OnSelectSum(cards, sumval, min, max, this.selectHint, mode);
            this.selectHint = 0;

            byte[] result = new byte[mandatoryCards.Count + selected.Count + 1];
            int index = 0;

            result[index++] = (byte)(mandatoryCards.Count + selected.Count);
            while (index <= mandatoryCards.Count)
            {
                result[index++] = 0;
            }
            int l = 0;
            while (l < selected.Count)
            {
                result[index++] = (byte)selected[l].SelectSeq;
                ++l;
            }

            BinaryWriter reply = GamePacketFactory.Create(CtosMessage.Response);
            reply.Write(result);
            this.Connection.Send(reply);
        }

        private void OnSelectTribute(BinaryReader packet)
        {
            this.InternalOnSelectCard(packet, this.ai.OnSelectTribute);
        }

        private void OnSelectYesNo(BinaryReader packet)
        {
            packet.ReadByte(); // player
            int desc = packet.ReadInt32();
            int reply;
            if (desc == 30)
            {
                reply = this.ai.OnSelectBattleReplay() ? 1 : 0;
            }
            else
            {
                reply = this.ai.OnSelectYesNo(desc) ? 1 : 0;
            }

            this.Connection.Send(CtosMessage.Response, reply);
        }

        private void OnAnnounceAttrib(BinaryReader packet)
        {
            IList<CardAttribute> attributes = new List<CardAttribute>();
            packet.ReadByte(); // player
            int count = packet.ReadByte();
            int available = packet.ReadInt32();
            int filter = 0x1;
            for (int i = 0; i < 7; ++i)
            {
                if ((available & filter) != 0)
                {
                    attributes.Add((CardAttribute) filter);
                }

                filter <<= 1;
            }
            attributes = this.ai.OnAnnounceAttrib(count, attributes);
            int reply = 0;
            for (int i = 0; i < count; ++i)
            {
                reply += (int)attributes[i];
            }

            this.Connection.Send(CtosMessage.Response, reply);
        }

        private void OnAnnounceCard(BinaryReader packet)
        {
            IList<int> opcodes = new List<int>();
            packet.ReadByte(); // player
            int count = packet.ReadByte();
            for (int i = 0; i < count; ++i)
            {
                opcodes.Add(packet.ReadInt32());
            }

            IList<int> avail = new List<int>();
            IList<NamedCard> all = NamedCardsManager.GetAllCards();
            foreach (NamedCard card in all)
            {
                if (card.HasType(CardType.Token) || (card.Alias > 0 && card.Id - card.Alias < 10))
                {
                    continue;
                }

                Stack<int> stack = new Stack<int>();
                for (int i = 0; i < opcodes.Count; i++)
                {
                    switch (opcodes[i])
                    {
                        case Opcodes.OPCODE_ADD:
                            if (stack.Count >= 2)
                            {
                                int rhs = stack.Pop();
                                int lhs = stack.Pop();
                                stack.Push(lhs + rhs);
                            }
                            break;
                        case Opcodes.OPCODE_SUB:
                            if (stack.Count >= 2)
                            {
                                int rhs = stack.Pop();
                                int lhs = stack.Pop();
                                stack.Push(lhs - rhs);
                            }
                            break;
                        case Opcodes.OPCODE_MUL:
                            if (stack.Count >= 2)
                            {
                                int rhs = stack.Pop();
                                int lhs = stack.Pop();
                                stack.Push(lhs * rhs);
                            }
                            break;
                        case Opcodes.OPCODE_DIV:
                            if (stack.Count >= 2)
                            {
                                int rhs = stack.Pop();
                                int lhs = stack.Pop();
                                stack.Push(lhs / rhs);
                            }
                            break;
                        case Opcodes.OPCODE_AND:
                            if (stack.Count >= 2)
                            {
                                int rhs = stack.Pop();
                                int lhs = stack.Pop();
                                bool b0 = rhs != 0;
                                bool b1 = lhs != 0;
                                if (b0 && b1)
                                {
                                    stack.Push(1);
                                }
                                else
                                {
                                    stack.Push(0);
                                }
                            }
                            break;
                        case Opcodes.OPCODE_OR:
                            if (stack.Count >= 2)
                            {
                                int rhs = stack.Pop();
                                int lhs = stack.Pop();
                                bool b0 = rhs != 0;
                                bool b1 = lhs != 0;
                                if (b0 || b1)
                                {
                                    stack.Push(1);
                                }
                                else
                                {
                                    stack.Push(0);
                                }
                            }
                            break;
                        case Opcodes.OPCODE_NEG:
                            if (stack.Count >= 1)
                            {
                                int rhs = stack.Pop();
                                stack.Push(-rhs);
                            }
                            break;
                        case Opcodes.OPCODE_NOT:
                            if (stack.Count >= 1)
                            {
                                int rhs = stack.Pop();
                                bool b0 = rhs != 0;
                                if (b0)
                                {
                                    stack.Push(0);
                                }
                                else
                                {
                                    stack.Push(1);
                                }
                            }
                            break;
                        case Opcodes.OPCODE_ISCODE:
                            if (stack.Count >= 1)
                            {
                                int code = stack.Pop();
                                bool b0 = code == card.Id;
                                if (b0)
                                {
                                    stack.Push(1);
                                }
                                else
                                {
                                    stack.Push(0);
                                }
                            }
                            break;
                        case Opcodes.OPCODE_ISSETCARD:
                            if (stack.Count >= 1)
                            {
                                if (card.HasSetcode(stack.Pop()))
                                {
                                    stack.Push(1);
                                }
                                else
                                {
                                    stack.Push(0);
                                }
                            }
                            break;
                        case Opcodes.OPCODE_ISTYPE:
                            if (stack.Count >= 1)
                            {
                                if ((stack.Pop() & card.Type) > 0)
                                {
                                    stack.Push(1);
                                }
                                else
                                {
                                    stack.Push(0);
                                }
                            }
                            break;
                        case Opcodes.OPCODE_ISRACE:
                            if (stack.Count >= 1)
                            {
                                if ((stack.Pop() & card.Race) > 0)
                                {
                                    stack.Push(1);
                                }
                                else
                                {
                                    stack.Push(0);
                                }
                            }
                            break;
                        case Opcodes.OPCODE_ISATTRIBUTE:
                            if (stack.Count >= 1)
                            {
                                if ((stack.Pop() & card.Attribute) > 0)
                                {
                                    stack.Push(1);
                                }
                                else
                                {
                                    stack.Push(0);
                                }
                            }
                            break;
                        default:
                            stack.Push(opcodes[i]);
                            break;
                    }
                }
                if (stack.Count == 1 && stack.Pop() != 0)
                {
                    avail.Add(card.Id);
                }
            }
            if (avail.Count == 0)
            {
                throw new Exception("No avail card found for announce!");
            }

            this.Connection.Send(CtosMessage.Response, this.ai.OnAnnounceCard(avail));
        }

        private void OnAnnounceNumber(BinaryReader packet)
        {
            IList<int> numbers = new List<int>();
            packet.ReadByte(); // player
            int count = packet.ReadByte();
            for (int i = 0; i < count; ++i)
            {
                numbers.Add(packet.ReadInt32());
            }

            this.Connection.Send(CtosMessage.Response, this.ai.OnAnnounceNumber(numbers));
        }

        private void OnAnnounceRace(BinaryReader packet)
        {
            IList<CardRace> races = new List<CardRace>();
            packet.ReadByte(); // player
            int count = packet.ReadByte();
            int available = packet.ReadInt32();
            int filter = 0x1;
            for (int i = 0; i < 23; ++i)
            {
                if ((available & filter) != 0)
                {
                    races.Add((CardRace)filter);
                }

                filter <<= 1;
            }
            races = this.ai.OnAnnounceRace(count, races);
            int reply = 0;
            for (int i = 0; i < count; ++i)
            {
                reply += (int)races[i];
            }

            this.Connection.Send(CtosMessage.Response, reply);
        }

        private void OnRockPaperScissors(BinaryReader packet)
        {
            packet.ReadByte(); // player
            int result;
            if (this.hand > 0)
            {
                result = this.hand;
            }
            else
            {
                result = this.ai.OnRockPaperScissors();
            }

            this.Connection.Send(CtosMessage.Response, result);
        }

        private void OnEquip(BinaryReader packet)
        {
            int equipCardControler = this.GetLocalPlayer(packet.ReadByte());
            int equipCardLocation = packet.ReadByte();
            int equipCardSequence = packet.ReadSByte();
            packet.ReadByte();
            int targetCardControler = this.GetLocalPlayer(packet.ReadByte());
            int targetCardLocation = packet.ReadByte();
            int targetCardSequence = packet.ReadSByte();
            packet.ReadByte();
            ClientCard equipCard = this.duel.GetCard(equipCardControler, (CardLocation)equipCardLocation, equipCardSequence);
            ClientCard targetCard = this.duel.GetCard(targetCardControler, (CardLocation)targetCardLocation, targetCardSequence);
            if (equipCard == null || targetCard == null)
            {
                return;
            }

            equipCard.EquipTarget?.EquipCards.Remove(equipCard);
            equipCard.EquipTarget = targetCard;
            targetCard.EquipCards.Add(equipCard);
        }

        private void OnUnEquip(BinaryReader packet)
        {
            int equipCardControler = this.GetLocalPlayer(packet.ReadByte());
            int equipCardLocation = packet.ReadByte();
            int equipCardSequence = packet.ReadSByte();
            packet.ReadByte();
            ClientCard equipCard = this.duel.GetCard(equipCardControler, (CardLocation)equipCardLocation, equipCardSequence);
            if (equipCard == null)
            {
                return;
            }

            if (equipCard.EquipTarget != null)
            {
                equipCard.EquipTarget.EquipCards.Remove(equipCard);
                equipCard.EquipTarget = null;
            }
        }

        private void OnCardTarget(BinaryReader packet)
        {
            int ownerCardControler = this.GetLocalPlayer(packet.ReadByte());
            int ownerCardLocation = packet.ReadByte();
            int ownerCardSequence = packet.ReadSByte();
            packet.ReadByte();
            int targetCardControler = this.GetLocalPlayer(packet.ReadByte());
            int targetCardLocation = packet.ReadByte();
            int targetCardSequence = packet.ReadSByte();
            packet.ReadByte();
            ClientCard ownerCard = this.duel.GetCard(ownerCardControler, (CardLocation)ownerCardLocation, ownerCardSequence);
            ClientCard targetCard = this.duel.GetCard(targetCardControler, (CardLocation)targetCardLocation, targetCardSequence);
            if (ownerCard == null || targetCard == null)
            {
                return;
            }

            ownerCard.TargetCards.Add(targetCard);
            targetCard.OwnTargets.Add(ownerCard);
        }

        private void OnCancelTarget(BinaryReader packet)
        {
            int ownerCardControler = this.GetLocalPlayer(packet.ReadByte());
            int ownerCardLocation = packet.ReadByte();
            int ownerCardSequence = packet.ReadSByte();
            packet.ReadByte();
            int targetCardControler = this.GetLocalPlayer(packet.ReadByte());
            int targetCardLocation = packet.ReadByte();
            int targetCardSequence = packet.ReadSByte();
            packet.ReadByte();
            ClientCard ownerCard = this.duel.GetCard(ownerCardControler, (CardLocation)ownerCardLocation, ownerCardSequence);
            ClientCard targetCard = this.duel.GetCard(targetCardControler, (CardLocation)targetCardLocation, targetCardSequence);
            if (ownerCard == null || targetCard == null)
            {
                return;
            }

            ownerCard.TargetCards.Remove(targetCard);
            targetCard.OwnTargets.Remove(ownerCard);
        }

        private void OnSummoning(BinaryReader packet)
        {
            this.duel.LastSummonedCards.Clear();
            int code = packet.ReadInt32();
            int currentControler = this.GetLocalPlayer(packet.ReadByte());
            int currentLocation = packet.ReadByte();
            int currentSequence = packet.ReadSByte();
            int currentPosition = packet.ReadSByte();
            ClientCard card = this.duel.GetCard(currentControler, (CardLocation)currentLocation, currentSequence);
            this.duel.SummoningCards.Add(card);
            this.duel.LastSummonPlayer = currentControler;
        }

        private void OnSummoned(BinaryReader packet)
        {
            foreach (ClientCard card in this.duel.SummoningCards)
            {
                this.duel.LastSummonedCards.Add(card);
            }
            this.duel.SummoningCards.Clear();
        }

        private void OnSpSummoning(BinaryReader packet)
        {
            this.duel.LastSummonedCards.Clear();
            this.ai.CleanSelectMaterials();
            int code = packet.ReadInt32();
            int currentControler = this.GetLocalPlayer(packet.ReadByte());
            int currentLocation = packet.ReadByte();
            int currentSequence = packet.ReadSByte();
            int currentPosition = packet.ReadSByte();
            ClientCard card = this.duel.GetCard(currentControler, (CardLocation)currentLocation, currentSequence);
            this.duel.SummoningCards.Add(card);
            this.duel.LastSummonPlayer = currentControler;
        }

        private void OnSpSummoned(BinaryReader packet)
        {
            foreach (ClientCard card in this.duel.SummoningCards)
            {
                card.IsSpecialSummoned = true;
                this.duel.LastSummonedCards.Add(card);
            }
            this.duel.SummoningCards.Clear();
        }
    }
}
