using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    class Door
    {
        // Fields
        private string doorName; // Dörrnamn
        private string doorDescription; // Beskrivning av dörren 
        private bool isOpen; // Variabel som representerar om dörren är öppen eller stängd
        private bool isLocked; // Variabel som representerar om dörren är låst eller stängd
        private Items itemToUnlock; // Varje dörr har en "Item" som kan låsa upp en stängd dörr
        private Dictionary<String, Room> doorExits = new Dictionary<String, Room>();

        // Properties
        public bool IsOpen { get => isOpen; set => isOpen = value; }
        public bool IsLocked { get => isLocked; set => isLocked = value; }
        public Items ItemToUnlock { get => itemToUnlock; set => itemToUnlock = value; }
        internal Dictionary<string, Room> DoorExits { get => doorExits; set => doorExits = value; }
        public string DoorName { get => doorName; set => doorName = value; }
        public string DoorDescription { get => doorDescription; set => doorDescription = value; }

        
        // Constructor
        public Door(string doorName, string doorDescription) // För vanliga olåsta dörrar
        {
            this.isOpen = false;
            this.isLocked = false;
            this.doorName = doorName;
            this.doorDescription = doorDescription;
        }
        public Door(string doorName, string doorDescription, Items itemToUnlock) // För låsta dörrar
        {
            this.isOpen = false;
            this.isLocked = true;
            this.doorName = doorName;
            this.doorDescription = doorDescription;
            this.ItemToUnlock = itemToUnlock;
        }

        


    }
}
