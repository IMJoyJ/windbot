﻿using System;

namespace WindBot.Game.AI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DeckAttribute : Attribute
    {
        public string Name { get; private set; }
        public string File { get; private set; }
        public string Level { get; private set; }

        public DeckAttribute(string name, string file = null, string level = "Normal")
        {
            if (string.IsNullOrEmpty(file))
            {
                file = name;
            }

            this.Name = name;
            this.File = file;
            this.Level = level;
        }
    }
}
