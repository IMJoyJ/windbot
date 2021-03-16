using System;
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
        private GameClient game;

        private string[] welcome;
        private string[] deckError;
        private string[] duelStart;
        private string[] newTurn;
        private string[] endTurn;
        private string[] directAttack;
        private string[] attack;
        private string[] onDirectAttack;
        private string facedownMonsterName;
        private string[] activate;
        private string[] summon;
        private string[] setMonster;
        private string[] chaining;
        
        public Dialogs(GameClient game)
        {
            this.game = game;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DialogsData));
            string dialogfilename = game.Dialog;
            using (FileStream fs = File.OpenRead("Dialogs/" + dialogfilename + ".json"))
            {
                DialogsData data = (DialogsData)serializer.ReadObject(fs);
                this.welcome = data.Welcome;
                this.deckError = data.DeckError;
                this.duelStart = data.DuelStart;
                this.newTurn = data.NewTurn;
                this.endTurn = data.EndTurn;
                this.directAttack = data.DirectAttack;
                this.attack = data.Attack;
                this.onDirectAttack = data.OnDirectAttack;
                this.facedownMonsterName = data.FacedownMonsterName;
                this.activate = data.Activate;
                this.summon = data.Summon;
                this.setMonster = data.SetMonster;
                this.chaining = data.Chaining;
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

            string message = string.Format(array[Program.Rand.Next(array.Count)], opts);
            if (message != "")
            {
                this.game.Chat(message);
            }
        }

        private void InternalSendMessageForced(IList<string> array, params object[] opts)
        {
            string message = string.Format(array[Program.Rand.Next(array.Count)], opts);
            if (message != "")
			{
                this.game.Chat(message);
				Logger.WriteLine("Error: " + message);
			}
        }
    }
}
