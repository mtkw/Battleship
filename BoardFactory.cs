using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public static class BoardFactory
    {
        internal static Board CreateBoard(Player player, int boardSize)
        {
            return new Board(player, boardSize);
        }
    }
}
