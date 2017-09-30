using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    class Player
    {
        // Fields
        private string playerName;
        private Room playerLocation;
        private Dictionary<String, Items> playerInventory = new Dictionary<String, Items>();

        // Properties
        public string PlayerName { get => playerName; set => playerName = value; }
        internal Room PlayerLocation { get => playerLocation; set => playerLocation = value; }
        public Dictionary<String, Items> PlayerInventory { get => playerInventory; set => playerInventory = value; }

        // Constructor
        public Player(string playerName, Room playerLocation)
        {
            this.playerName = playerName;
            this.playerLocation = playerLocation;            
        }

        // Metoder

        public void PrintInventory() // skriv ut vad spelaren bär på
        {
            
            foreach (var item in playerInventory)
            {
                Console.WriteLine(item.Key);
            }
            
        }


    }
}
