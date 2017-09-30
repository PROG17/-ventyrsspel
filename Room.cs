using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
   
    class Room
    {
        //Fields
        private string roomDescription;
        private string roomName;


        // Exits uses the dicitonary class. In this case a direction from the... 
        // ... enum Direction "North, South, East, West" is entered and a room variable is returned
        //private Dictionary<String, Room> exits = new Dictionary<String, Room>();
        private Dictionary<String, Door> roomExits = new Dictionary<String, Door>();

        private Dictionary<String, Items> roomInventory = new Dictionary<String, Items>();

        // Properties
        public string RoomDescription { get => roomDescription; set => roomDescription = value; }
        public string RoomName { get => roomName; set => roomName = value; }
        //internal Dictionary<String, Room> Exits { get => exits; set => exits = value; }
        public Dictionary<String, Items> RoomInventory { get => roomInventory; set => roomInventory = value; }
        internal Dictionary<string, Door> RoomExits { get => roomExits; set => roomExits = value; }


        // Constructor
        public Room(string roomName, string roomDescription)
        {
            this.roomDescription = roomDescription;
            this.roomName = roomName;                    
        }


        // Methods

        
        public void PrintDescription() // Print description
        {
            
            Console.WriteLine("Room name: {0}", roomName);
            Console.WriteLine("Room description: {0}", roomDescription);
            Console.WriteLine("Exits: ");

            foreach (var item in roomExits)
            {
                Console.Write("To the {0}, there's a {1} door. {2}.",item.Key, item.Value.DoorName,item.Value.DoorDescription);
                if (item.Value.IsLocked)
                {
                    Console.WriteLine(" It is locked.");
                }
                else if (!item.Value.IsOpen)
                {
                    Console.WriteLine(" It is closed.");
                }
                else
                {
                    Console.WriteLine(" It is open.");
                }
            }

            Console.WriteLine("Items in room: "); // SKriv ut vilka saker som ligger i rummet
            foreach (var item in roomInventory)
            {
                Console.WriteLine(item.Key);
            }
            Console.WriteLine();
            Console.WriteLine("Allowable commands: {0}", string.Join(", ", Enum.GetNames(typeof(AllowableCommands))) + ", or QUIT");
        }
            
        




    }
}
