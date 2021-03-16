using System;

namespace WindBot
{
    public class WindBotInfo
    {
        public string Name { get; set; }
        public string Deck { get; set; }
        public string DeckFile { get; set; }
        public string DeckCode { get; set; }
        public string Dialog { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string HostInfo { get; set; }
        public int Version { get; set; }
        public int Hand { get; set; }
        public bool Debug { get; set; }
        public bool Chat { get; set; }
        public WindBotInfo()
        {
            this.Name = "WindBot";
            this.Deck = null;
            this.DeckFile = null;
            this.DeckCode = null;
            this.Dialog = "default";
            this.Host = "127.0.0.1";
            this.Port = 7911;
            this.HostInfo = "";
            this.Version = 0x1352;
            this.Hand = 0;
            this.Debug = false;
            this.Chat = true;
        }
    }
}
