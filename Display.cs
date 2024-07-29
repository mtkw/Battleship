using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Display
    {

        public void PrintBoard(Board board, Player player)
        {
            Console.WriteLine(board.ToString(player));
        }

        public void PrintPlayer(Player player)
        {
            Console.WriteLine(player.PlayerName());
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void PrintShipLength(Ship ship) {
            Console.WriteLine($"Ship Size: " + ship.ShipLength());
        }
    }
}
