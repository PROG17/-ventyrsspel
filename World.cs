using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    
    class World
    {
        //Fields
        private Items key;
        private Items hat;
        private Room entrance;
        private Room hall;
        private Room kitchen;
        private Room bedroom;
        private Player player;

        // Properties
        public Items Key { get => key; set => key = value; }
        public Items Hat { get => hat; set => hat = value; }
        internal Room Entrance { get => entrance; set => entrance = value; }
        internal Room Hall { get => hall; set => hall = value; }
        internal Room Kitchen { get => kitchen; set => kitchen = value; }
        internal Room Bedroom { get => bedroom; set => bedroom = value; }
        internal Player Player { get => player; set => player = value; }
        

        // Constructor

        public World()
        {
            // Create items (item description, item name)
            key = new Items("This is a key", "Key");
            hat = new Items("This a hat", "Hat");
            // sword = new Items("This a sword", "Sword"); OSV. bara att lägga till fler om man vill

            // Create rooms (roomname, roomdescription)
            entrance = new Room("Entrance", "This is the room where the player starts.");
            hall = new Room("Hall", "This is the hall.");
            kitchen = new Room("Kitchen", "This is the kitchen.");
            bedroom = new Room("Bedroom", "This is the bedroom.");
            // bara att skapa fler rum om man vill

            // Create player
            player = new Player("Kalle", entrance); // Sätter PlayerLocation till rummet "entrance" från början det blir där spelaren startar spelet
            // "Playern" har i dagsläget ingen funktionalitet förutom att den bär på ett antal "items"



            // Add "exits".This is where the "connections" between the rooms are defined.
            // If more rooms are added under "create rooms" above", new Exits must be defined below
            // Possible exits are "North, South, West, East" and must point to a destination room object
            // 
            // exits uses the dictionary class

            entrance.Exits.Add(Direction.South, hall);
            hall.Exits.Add(Direction.East, kitchen); 
            hall.Exits.Add(Direction.North, entrance);
            kitchen.Exits.Add(Direction.South, bedroom);
            kitchen.Exits.Add(Direction.West, hall);
            bedroom.Exits.Add(Direction.North, kitchen);

            // Add created items to room/rooms/players
                        
            bedroom.RoomInventory.Add(key.ItemName, key); // Room inventory uses the dictionary class
            player.PlayerInventory.Add(hat.ItemName, hat); // Player inventory uses the dictionary class
        }
        /**************************************************************************/

        
        public void PlayGame() // Handling player console inputs and making descions about 
            //what to do based on the commands
        {
            Room currentRoom = Player.PlayerLocation; 
            Room destination;
            
            
            bool keepPlaying = true;
            Direction direction;
            AllowableCommands commands;

            
            while (keepPlaying) // Main game loop
            {
                
                currentRoom.PrintDescription();
                player.PrintInventory();
                Console.Write(">");
                string command = Console.ReadLine(); // läs in kommando tangentbordet
                
                if (Enum.TryParse<AllowableCommands>(command, out commands)) // kollar om ordet finns med i enum AllowableCommands listan som definnieras i program.cs
                {
                    if (commands == AllowableCommands.Go) // om man väljer "Go"........
                    {
                        currentRoom = Go(currentRoom);
                    }
                    else if (commands == AllowableCommands.Take) // om man väljer "Take"
                    {
                        Take(currentRoom, player);
                    }
                    else if (commands == AllowableCommands.Drop) // om man väljer "Drop"........
                    {
                        Drop(currentRoom, player);
                    }
                    // "Look" funktion // "Titta i rummet"
                    // "Open" funktion // Öppna dörr
                    // "Unlock" funktion // lås upp dörr
                    // "Use" // kombinera två föremål

                }
                else if (command == "Quit") // Avslutar spelet
                {
                    Console.WriteLine("Thank you for playing!");
                    keepPlaying = false;
                }
                else
                {
                   Console.WriteLine("I don't understand."); // Skrivs ut om man inte skriver något av kommandon enum AllowableCommands listan som definnieras i program.cs
                   Console.WriteLine("Try: {0}", string.Join(", ", Enum.GetNames(typeof(AllowableCommands))) + ", or Quit");
                }
            }

        }

        public Room Go(Room currentroom)
        {
            bool keepInputting = true;
            Direction direction;
            Room destination;
            
            {
                while (keepInputting)
                {
                    Console.WriteLine("Go where? :");
                    Console.Write(">");
                    string command = Console.ReadLine();
                    if (Enum.TryParse<Direction>(command, out direction))
                    {
                        // kollar om det inskriva kommandot motsvarar ett namn i exits dictionaryn och returnerar true och destination (dvs destinations rums objektet)
                        if (currentroom.Exits.TryGetValue(direction, out destination))
                        {
                            return destination; // Ändra aktuellt rum till det nya rummet
                        }
                        Console.WriteLine("You can't go that way.");
                        
                    }
                    else
                    {
                        Console.WriteLine("I don't understand.");
                        Console.WriteLine("Try: {0}", string.Join(", ", Enum.GetNames(typeof(Direction))));
                    }
                }
                return null;
            }
        }
        public void Take(Room currentroom, Player player)
        {
            bool keepInputting = true;
            Items item;
            {
                while (keepInputting)
                {
                    Console.WriteLine("Take what? :");
                    Console.Write(">");
                    string command = Console.ReadLine();
                    
                    if (currentroom.RoomInventory.TryGetValue(command, out item))
                    // kollar om det inskriva kommandot motsvarar ett namn i RoomInventory dictionaryn och returnerar true och item om finns
                    {
                        player.PlayerInventory.Add(item.ItemName, item); // Lägg till item från det aktuella rummet
                        currentroom.RoomInventory.Remove(item.ItemName); // Ta bort item från det aktuella rummet
                        Console.WriteLine("You took the {0}", item.ItemName);
                        keepInputting = false;
                    }
                    else
                    {
                        Console.WriteLine("That's not possible.");
                        break;

                    }



                }
                
            }
        }

        public void Drop(Room currentroom, Player player)
        {
            bool keepInputting = true;
            Items item;
            {
                while (keepInputting)
                {
                    Console.WriteLine("Drop what? :");
                    Console.Write(">");
                    string command = Console.ReadLine();

                    if (player.PlayerInventory.TryGetValue(command, out item))
                        // kollar om det inskriva kommandot motsvarar ett namn i PlayerInventory dictionaryn och returnerar true och item om finns
                    {
                        currentroom.RoomInventory.Add(item.ItemName, item); // Ta bort item från det aktuella rummet
                        player.PlayerInventory.Remove(item.ItemName); // Lägg till bort från PlayerInventory dictionaryt
                        Console.WriteLine("You dropped the {0}",item.ItemName);
                        keepInputting = false; // Gå tillbaka till huvud loopen
                    }
                    else
                    {
                        Console.WriteLine("That's not possible.");
                        break;

                    }



                }

            }
        }

























        // basklass spelobjekt
        // class rum : spelobjekt
        // class föremål : spelobjekt
        // class player : spelobjekt
    }
}




