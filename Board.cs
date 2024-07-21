using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Board
    {
        private Player boardOwner;
        private Square[,] fields { get; set; }
        private int boardSize;
        private List<Ship> shipList = new List<Ship>();

        public Board(Player boardOwner, int boardSize)
        {
            this.boardOwner = boardOwner;
            this.boardSize = boardSize;
            this.fields = new Square[boardSize,boardSize];
        }

        //Create ship i pleace ship should be here.
        public void CreateShip(ShipTypeEnum shipType)
        {
            Ship Ship = new Ship(shipType);
        }

        public bool IsPossiblePlacement(Ship Ship, int startX, int startY, string Direction)
        {
            if (Ship == null) {
                return false;
            }
            else
            {

            }
           

            return false;
        }

        

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
