using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
        private Room finishroom;
        private Room deathroom;
        private Player player;
        private Door door1;
        private Door door2;
        private Door door3;
        private Door door4;
        private Items tank;
        private Items filledChainSaw;
        private Items emtpyChainSaw;

        // Properties
        public Items Key { get => key; set => key = value; }
        public Items Hat { get => hat; set => hat = value; }
        internal Room Entrance { get => entrance; set => entrance = value; }
        internal Room Hall { get => hall; set => hall = value; }
        internal Room Kitchen { get => kitchen; set => kitchen = value; }
        internal Room Deathroom { get => deathroom; set => deathroom = value; }
        internal Player Player { get => player; set => player = value; }
        internal Door Door1 { get => door1; set => door1 = value; }
        public Items Tank { get => tank; set => tank = value; }
        public Items FilledChainSaw { get => filledChainSaw; set => filledChainSaw = value; }
        public Items EmtpyChainSaw { get => emtpyChainSaw; set => emtpyChainSaw = value; }
        internal Door Door2 { get => door2; set => door2 = value; }
        internal Door Door3 { get => door3; set => door3 = value; }
        internal Door Door4 { get => door4; set => door4 = value; }


        // Constructor

        public World()
        {
            
            
            // Create items (item description, item name)
            key = new Items("This is a key with some ancient symbols on it", "KEY");
            hat = new Items("This a your favorite hat", "HAT");
            filledChainSaw = new Items("This is chainsaw, its filled with gasoline", "FILLEDCHAINSAW");
            tank = new Items("This is a container filled with gasoline","TANK",filledChainSaw,emtpyChainSaw);
            emtpyChainSaw = new Items("This is chainsaw, its empty", "CHAINSAW",filledChainSaw,tank);

            // Create doors
            door1 = new Door("GLASSDOOR", "It is a door made of glass");
            door2 = new Door("SMALLDOOR", "It is a small door made of metal");
            door3 = new Door("ROUNDDOOR", "It is a with circular shape");
            door4 = new Door("BIGDOOR", "It is a huge wooden door", filledChainSaw);

            // Create rooms (roomname, roomdescription)
            entrance = new Room("Entrance", "This is the room where the player starts.");
            hall = new Room("Hall", "This is the hall, its dirty full old useless stuff.");
            kitchen = new Room("Kitchen", "This is the kitchen. Leftovers from someones dinner is laying on the floor.");
            deathroom = new Room("Deatchroom", "This is where you fall into a hole and die!.");
            finishroom = new Room("Finshroom", "This is the room where you finsh the game.");
            // bara att skapa fler rum om man vill

            // Create player
            player = new Player("Kalle", entrance); // Sätter PlayerLocation till rummet "entrance" från början det blir där spelaren startar spelet
            // "Playern" har i dagsläget ingen funktionalitet förutom att den bär på ett antal "items"
            
            // Add roomexits

            entrance.RoomExits.Add("SOUTH", door1);
            hall.RoomExits.Add("NORTH", door1);
            hall.RoomExits.Add("SOUTH", door3);
            hall.RoomExits.Add("EAST",door2);
            kitchen.RoomExits.Add("WEST",door2);
            kitchen.RoomExits.Add("SOUTH", door4);
            
            // Add doorexits
            door1.DoorExits.Add("SOUTH", hall);
            door1.DoorExits.Add("NORTH", entrance);
            door2.DoorExits.Add("WEST", hall);
            door2.DoorExits.Add("EAST", kitchen);
            door3.DoorExits.Add("SOUTH",deathroom);
            door4.DoorExits.Add("SOUTH",finishroom);

            // Items to rooms or player
            player.PlayerInventory.Add(hat.ItemName, hat); // Player inventory uses the dictionary class
            kitchen.RoomInventory.Add(emtpyChainSaw.ItemName,emtpyChainSaw);
            hall.RoomInventory.Add(tank.ItemName,tank);
            entrance.RoomInventory.Add(key.ItemName, key);
            
        }
        /**************************************************************************/

        
        public void PlayGame()  // Huvudloopen som hanterar kommandon 
                              
        {
            Room currentRoom = Player.PlayerLocation; 
            Room destination;
            bool keepPlaying = true;
            Console.Write("Hello! Welcome to this exciting game, what is your name?: ");
            player.PlayerName = Console.ReadLine();
            
            while (keepPlaying && (currentRoom != deathroom)) // Main game loop
            {

                
                currentRoom.PrintDescription();
                Console.WriteLine(player.PlayerName + " is carrying:");
                player.PrintInventory();
                Console.Write(">");
                string command = Console.ReadLine(); // läs in kommando tangentbordet
                string[] str = command.Split(' '); // Splitta mening till ord
                string[] ToUpperCommand = MyToUpper(str); // Gör om till stora bokstäver
                               

                if (ToUpperCommand[0] == "GO") // om man väljer "Go"........
                {
                    currentRoom = Go(currentRoom, ToUpperCommand[1]); 
                }
                else if (ToUpperCommand[0] == "TAKE") // om man väljer "TAKE"
                {
                    Take(currentRoom, player, ToUpperCommand[1]);
                }
                else if (ToUpperCommand[0] == "DROP") // om man väljer "DROP"........
                {
                    Drop(currentRoom, player, ToUpperCommand[1]);
                }
                else if (ToUpperCommand[0] == "OPEN") // om man väljer "OPEN"    
                {
                    Open(currentRoom, ToUpperCommand[1]);
                }
                else if (ToUpperCommand[0] == "CLOSE") // om man väljer "CLOSE"    
                {
                    Close(currentRoom, ToUpperCommand[1]);
                }
                
                else if (ToUpperCommand[0] == "USE") // om man väljer "USE"    
                {
                   Use(currentRoom, player, ToUpperCommand);
                }
                // "Use" // use key on door, use gasoline on chainsaw (chainsaw is empty) => chainsaw (chainsaw is filled up with 
                else if (ToUpperCommand[0] == "LOOK") // Om man väljer "LOOK"
                {
                    Look(currentRoom, player, ToUpperCommand[1]);
                }
                // "Look" funktion // "Titta i rummet"

                else if (ToUpperCommand[0] == "QUIT") // Avslutar spelet
                {
                    Console.WriteLine("Thank you for playing!");
                    keepPlaying = false;
                }
                else
                {
                   Console.Clear();
                   Console.WriteLine("I don't understand."); // Skrivs ut om man inte skriver något av kommandon enum AllowableCommands listan som definnieras i program.cs
                   Console.WriteLine("Try: {0}", string.Join(", ", Enum.GetNames(typeof(AllowableCommands))) + ", or QUIT");
                }
            }
            if (currentRoom == deathroom)
            {
                Console.WriteLine("You fell down a hole in the floor and died!");
                Console.WriteLine("Thank you for trying!! See you next time!!");
            }
            if (currentRoom == finishroom)
            {
                Console.WriteLine("You managed to use the chainsaw to open the wooden door.");
                Console.WriteLine("You won! and are free to leave the this awful place.");
            }

        }

        public Room Go(Room currentroom, string direction) 
        {
            
            Door destinationdoor;
            //Room destinationroom;
            
            if (currentroom.RoomExits.TryGetValue(direction, out destinationdoor))
            {
                
                if (destinationdoor.IsOpen && !destinationdoor.IsLocked) // Om dörren är öppen och olåst
                {
                    Console.Clear();
                    Console.WriteLine("You went {0}", direction);
                    return destinationdoor.DoorExits[direction]; // Returnera vilket rum som dörren leder till
                    //return destinationdoor.DoorExits.TryGetValue(direction, out destinationroom); // Ändra aktuellt rum till det nya rummet
                }
                else if (destinationdoor.IsLocked) // Om dörren är låst
                {
                    Console.Clear();
                    Console.WriteLine("The door is locked");
                    return currentroom;
                }
                else if (!destinationdoor.IsOpen) // Om dörren är stängd
                {
                    Console.Clear();
                    Console.WriteLine("The door is closed");
                    return currentroom;
                }
                else return currentroom;            
               
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You can't go that way.");
                return currentroom;
            }
            
            
        }
        public void Take(Room currentroom, Player player, string command)
        {
            
            Items item;
            
            if (currentroom.RoomInventory.TryGetValue(command, out item))
            // kollar om det inskriva kommandot motsvarar ett namn i RoomInventory dictionaryn och returnerar true och item om finns
            {
                player.PlayerInventory.Add(item.ItemName, item); // Lägg till item från det aktuella rummet
                currentroom.RoomInventory.Remove(item.ItemName); // Ta bort item från det aktuella rummet
                Console.Clear();
                Console.WriteLine("You took the {0}", item.ItemName);
                
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Can't find a {0}", command);
                return;

            }
         }

        public void Drop(Room currentroom, Player player, string command)
        {
            
            Items item;
         
            if (player.PlayerInventory.TryGetValue(command, out item))
               // kollar om det inskriva kommandot motsvarar ett namn i PlayerInventory dictionaryn och returnerar true och item om finns
            {
                currentroom.RoomInventory.Add(item.ItemName, item); // Ta bort item från det aktuella rummet
                player.PlayerInventory.Remove(item.ItemName); // Lägg till bort från PlayerInventory dictionaryt
                Console.Clear();
                Console.WriteLine("You dropped the {0}",item.ItemName);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You are not carrying a {0}", command);
                return;
            }
        }

        public void Open(Room currentroom, string doorname)
        {
            if (currentroom.RoomExits.Values.Any(x => x.DoorName == doorname))
            {
                if (currentroom.RoomExits.Values.Any(x => !x.IsLocked))
                {
                    var values = currentroom.RoomExits.Values.Where(x => x.DoorName == doorname).ToList();
                    values[0].IsOpen = true;
                    Console.Clear();
                    Console.WriteLine("You opened the {0}", values[0].DoorName);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("The door is locked.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("There no such door here.");
            }


        }
        public void Close(Room currentroom, string doorname)
        {
            if (currentroom.RoomExits.Values.Any(x => x.DoorName == doorname))
            {
                var values = currentroom.RoomExits.Values.Where(x => x.DoorName == doorname).ToList();
                values[0].IsOpen = false;
                Console.Clear();
                Console.WriteLine("You closed the {0}", values[0].DoorName);

            }
            else
            {
                Console.Clear();
                Console.WriteLine("There no such door here.");
            }


        }

        public void Use(Room currentroom, Player player, string[] commands)
        {

            Items item1;
            Items item2;

            if (player.PlayerInventory.TryGetValue(commands[1], out item1)
                && player.PlayerInventory.TryGetValue(commands[3], out item2))
            // kollar om det två saker som ska användas på varandra finns i playerinventory
            { 
                if ((item1.PairItem == item2) || (item2.PairItem == item1))  // om item1 kan kombineras med item2
                {
                    player.PlayerInventory.Add(item1.NewItem.ItemName, item1.NewItem); // Lägg till item från det aktuella rummet
                    player.PlayerInventory.Remove(item1.ItemName); // Ta bort item1 från playerinventory
                    player.PlayerInventory.Remove(item2.ItemName); // Ta bort item2 från playerinventory
                    Console.Clear();
                    Console.WriteLine("You used {0} on the {1}", commands[1], commands[3]);
                    return;
                }
            }
            //else
            //{
            //    Console.Clear();
            //    Console.WriteLine("You are not carrying those items");
            //    return;
            //}
            else if (player.PlayerInventory.TryGetValue(commands[1], out item1) 
                && currentroom.RoomExits.Values.Any(x => x.DoorName == commands[3]))
            {
                
                //if (currentroom.RoomExits.Values.Any(x => x.ItemToUnlock == item1))
                if (currentroom.RoomExits.Values.Any(x => x.DoorName == commands[3] && x.ItemToUnlock == item1))
                {
                    var values = currentroom.RoomExits.Values.Where(x => x.DoorName == commands[3]).ToList();
                    values[0].IsLocked = false;
                    Console.Clear();
                    Console.WriteLine("You unlocked the {0}", values[0].DoorName);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You can't do that");
                }
                

            }
            else
            {
                Console.Clear();
                Console.WriteLine("That's not an option.");
               
            }
        }

        public void Look(Room currentroom, Player player, string command)
        {

            Items item;

            if (currentroom.RoomInventory.TryGetValue(command, out item) ||
                player.PlayerInventory.TryGetValue(command, out item))
            
                // kollar om det inskriva kommandot motsvarar ett namn i RoomInventory, playerinventory eller dörr
            {
                Console.Clear();
                Console.WriteLine(item.ItemDescription);
            }
            else if (currentroom.RoomExits.Values.Any(x => x.DoorName == command))
            {
                Console.Clear();
                var values = currentroom.RoomExits.Values.Where(x => x.DoorName == command).ToList();
                Console.WriteLine("You see a door. {0}", values[0].DoorDescription);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You see no {0}", command);
            }
        }

        public static string[] MyToUpper(string[] myarray)
        {
            string[] temp = new string[myarray.Length];

            for (int i = 0; i < myarray.Length; i++)
                temp[i] = myarray[i].ToUpper();
            return temp;
        }

























        // basklass spelobjekt
        // class rum : spelobjekt
        // class föremål : spelobjekt
        // class player : spelobjekt
    }
}




