namespace WindBot.Game
{
    public class Room
    {
        public bool IsHost { get; set; }
        public string[] Names { get; set; }
        public bool[] IsReady { get; set; }
        public int Position { get; set; }

        public Room()
        {
            this.Names = new string[8];
            this.IsReady = new bool[8];
            this.Position = -1;
        }
    }
}