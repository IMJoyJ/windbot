﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace WindBot.Game.AI
{
    [DataContract]
    public class DialogsData
    {
        [DataMember]
        public string[] Welcome { get; set; }
        [DataMember]
        public string[] DeckError { get; set; }
        [DataMember]
        public string[] DuelStart { get; set; }
        [DataMember]
        public string[] NewTurn { get; set; }
        [DataMember]
        public string[] EndTurn { get; set; }
        [DataMember]
        public string[] DirectAttack { get; set; }
        [DataMember]
        public string[] Attack { get; set; }
        [DataMember]
        public string[] OnDirectAttack { get; set; }
        [DataMember]
        public string FacedownMonsterName { get; set; }
        [DataMember]
        public string[] Activate { get; set; }
        [DataMember]
        public string[] Summon { get; set; }
        [DataMember]
        public string[] SetMonster { get; set; }
        [DataMember]
        public string[] Chaining { get; set; }                                          
    }
    public class Dialogs
    {
        private readonly GameClient game;

        private readonly string[] welcome;
        private readonly string[] deckError;
        private readonly string[] duelStart;
        private readonly string[] newTurn;
        private readonly string[] endTurn;
        private readonly string[] directAttack;
        private readonly string[] attack;
        private readonly string[] onDirectAttack;
        private readonly string facedownMonsterName;
        private readonly string[] activate;
        private readonly string[] summon;
        private readonly string[] setMonster;
        private readonly string[] chaining;
        
        public Dialogs(GameClient game)
        {
            this.game = game;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DialogsData));
            string dialogfilename = game.Dialog;
            using (FileStream fs = File.OpenRead("Dialogs/" + dialogfilename + ".json"))
            {
                DialogsData data = (DialogsData)serializer.ReadObject(fs);
                this.welcome = data.Welcome ?? new string[1] {"" };
                this.deckError = data.DeckError ?? new string[1] { "" }; ;
                this.duelStart = data.DuelStart ?? new string[1] { "" }; ;
                this.newTurn = data.NewTurn ?? new string[1] { "" }; ;
                this.endTurn = data.EndTurn ?? new string[1] { "" }; ;
                this.directAttack = data.DirectAttack ?? new string[1] { "" }; ;
                this.attack = data.Attack ?? new string[1] { "" }; ;
                this.onDirectAttack = data.OnDirectAttack ?? new string[1] { "" }; ;
                this.facedownMonsterName = data.FacedownMonsterName ?? "";
                this.activate = data.Activate ?? new string[1] { "" }; ;
                this.summon = data.Summon ?? new string[1] { "" }; ;
                this.setMonster = data.SetMonster ?? new string[1] { "" }; ;
                this.chaining = data.Chaining ?? new string[1] { "" }; ;
            }
        }

        public void SendSorry()
        {
            this.InternalSendMessageForced(new[] { "Sorry, an error occurs." });
        }

        public void SendDeckSorry(string card)
        {
            if (card == "DECK")
            {
                this.InternalSendMessageForced(new[] { "Deck illegal. Please check the database of your YGOPro and WindBot." });
            }
            else
            {
                this.InternalSendMessageForced(this.deckError, card);
            }
        }

        public void SendWelcome()
        {
            this.InternalSendMessage(this.welcome);
        }

        public void SendDuelStart()
        {
            this.InternalSendMessage(this.duelStart);
        }

        public void SendNewTurn()
        {
            this.InternalSendMessage(this.newTurn);
        }

        public void SendEndTurn()
        {
            this.InternalSendMessage(this.endTurn);
        }

        public void SendDirectAttack(string attacker)
        {
            this.InternalSendMessage(this.directAttack, attacker);
        }

        public void SendAttack(string attacker, string defender)
        {
            if (defender=="monster")
            {
                defender = this.facedownMonsterName;
            }
            this.InternalSendMessage(this.attack, attacker, defender);
        }

        public void SendOnDirectAttack(string attacker)
        {
            if (string.IsNullOrEmpty(attacker))
            {
                attacker = this.facedownMonsterName;
            }
            this.InternalSendMessage(this.onDirectAttack, attacker);
        }
        public void SendOnDirectAttack()
        {
            this.InternalSendMessage(this.onDirectAttack);
        }

        public void SendActivate(string spell)
        {
            this.InternalSendMessage(this.activate, spell);
        }

        public void SendSummon(string monster)
        {
            this.InternalSendMessage(this.summon, monster);
        }

        public void SendSetMonster()
        {
            this.InternalSendMessage(this.setMonster);
        }

        public void SendChaining(string card)
        {
            this.InternalSendMessage(this.chaining, card);
        }

        private void InternalSendMessage(IList<string> array, params object[] opts)
        {
            if (!this.game._chat)
            {
                return;
            }

            string message = string.Format(array[Program._rand.Next(array.Count)], opts);
            if (message != "")
            {
                this.game.Chat(message);
            }
        }

        private void InternalSendMessageForced(IList<string> array, params object[] opts)
        {
            string message = string.Format(array[Program._rand.Next(array.Count)], opts);
            if (message != "")
			{
                this.game.Chat(message);
				Logger.WriteLine("Error: " + message);
			}
        }
    }
}
