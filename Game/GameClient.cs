using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using YGOSharp.Network;
using YGOSharp.Network.Enums;
using YGOSharp.Network.Utils;

namespace WindBot.Game
{
    public class GameClient
    {
        public YGOClient Connection { get; private set; }
        public string Username;
        public string Deck;
        public string DeckFile;
        public string DeckCode;
        public string Dialog;
        public int Hand;
        public bool Debug;
        public bool _chat;
        private readonly string _serverHost;
        private readonly int _serverPort;
        private readonly short _proVersion;

        private readonly string _roomInfo;

        private GameBehavior _behavior;

        public GameClient(WindBotInfo Info)
        {
            this.Username = Info.Name;
            this.Deck = Info.Deck;
            this.DeckFile = Info.DeckFile;
            this.DeckCode = Info.DeckCode;
            this.Dialog = Info.Dialog;
            this.Hand = Info.Hand;
            this.Debug = Info.Debug;
            this._chat = Info.Chat;
            this._serverHost = Info.Host;
            this._serverPort = Info.Port;
            this._roomInfo = Info.HostInfo;
            this._proVersion = (short)Info.Version;
        }

        public void Start()
        {
            this.Connection = new YGOClient();
            this._behavior = new GameBehavior(this);

            this.Connection.Connected += this.OnConnected;
            this.Connection.PacketReceived += this.OnPacketReceived;

            IPAddress target_address;
            try
            {
                target_address = IPAddress.Parse(this._serverHost);
            }
            catch (System.Exception)
            {
                IPHostEntry _hostEntry = Dns.GetHostEntry(this._serverHost);
                target_address = _hostEntry.AddressList.FirstOrDefault(findIPv4 => findIPv4.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            }

            this.Connection.Connect(target_address, this._serverPort);
        }

        private void OnConnected()
        {
            BinaryWriter packet = GamePacketFactory.Create(CtosMessage.PlayerInfo);
            packet.WriteUnicode(this.Username, 20);
            this.Connection.Send(packet);

            byte[] junk = { 0xCC, 0xCC, 0x00, 0x00, 0x00, 0x00 };
            packet = GamePacketFactory.Create(CtosMessage.JoinGame);
            packet.Write(this._proVersion);
            packet.Write(junk);
            packet.WriteUnicode(this._roomInfo, 30);
            this.Connection.Send(packet);
        }

        public void Tick()
        {
            this.Connection.Update();
        }

        public void Chat(string message)
        {
            byte[] content = Encoding.Unicode.GetBytes(message + "\0");
            BinaryWriter chat = GamePacketFactory.Create(CtosMessage.Chat);
            chat.Write(content);
            this.Connection.Send(chat);
        }

        private void OnPacketReceived(BinaryReader reader)
        {
            this._behavior.OnPacket(reader);
        }
    }
}
