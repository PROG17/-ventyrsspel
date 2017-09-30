using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    enum Direction { NORTH, SOUTH, WEST, EAST } // enum variabler för tillåtna riktingar

    enum AllowableCommands { GO, TAKE, DROP, OPEN, USE, LOOK} // enum variabler för tillåtna kommandon

    class Program
    {
             
       
        static void Main(string[] args)
        {

            World spel = new World(); // Skapa nytt spel

           spel.PlayGame(); // Starta spelet
            

        }
    }
}

