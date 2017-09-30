using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    public class Items
    {
        // Fields
        private string itemDescription;
        private string itemName;
        private Items newItem; // Ny sak som kan skapas vid kombinering 
        private Items pairItem; // Den andra saken som behövs för att kombinera
        
        // Properties
        public string ItemDescription { get => itemDescription; set => itemDescription = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public Items NewItem { get => newItem; set => newItem = value; }
        public Items PairItem { get => pairItem; set => pairItem = value; }

        // Constructor
        public Items(string itemDescription, string itemName)
        {
            this.itemDescription = itemDescription;
            this.itemName = itemName;
        }
        public Items(string itemDescription, string itemName, Items newItem, Items pairItem)
        {
            this.itemDescription = itemDescription;
            this.itemName = itemName;
            this.NewItem = newItem;
            this.PairItem = pairItem;
        }
    }
}
