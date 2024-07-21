using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Board
    {
        private int boardId;
        private Player boardOwner;


        private string ToStringPlayerBoard()
        {
            return string.Empty;
        }

        private string ToStringOppenentBoard()
        {
            return string.Empty;
        }

        public string ToString(Player ActivePlayer)
        {
            if(ActivePlayer == this.boardOwner)
            {
                return ToStringPlayerBoard();
            }
            return ToStringOppenentBoard();
        }
    }
}
