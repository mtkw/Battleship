using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Board
    {
        private readonly Player boardOwner;
        private Square[,] fields;
        private int boardSize;
        public List<Ship> shipList = new List<Ship>();

        public Board(Player boardOwner, int boardSize)
        {
            this.boardOwner = boardOwner;
            this.boardSize = boardSize;
            this.fields = new Square[boardSize, boardSize];
            InitEmptyBoard();
        }

        private void InitEmptyBoard()
        {
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    fields[x, y] = new Square(x, y);
                }
            }
        }

        //Create ship i pleace ship should be here.
        public void CreateShip(ShipTypeEnum shipType)
        {
            Ship Ship = new Ship(shipType);
            shipList.Add(Ship);
        }

        private bool IsPossiblePlacement(Ship Ship, int startX, int startY, string Direction)
        {
            if (Ship == null) {
                return false;
            }
            else
            {
                if (Direction.ToLower() == "horizontal" && startX + Ship.ShipLength() < boardSize)
                {
                    return true;
                }
                if (Direction.ToLower() == "vertical" && startY + Ship.ShipLength() < boardSize) 
                { 
                    return true; 
                }
            }
            return false;
        }

        public void PlaceShipOnBoard(Ship ship, int startX, int startY, string Direction)
        {
            if (IsPossiblePlacement(ship, startX, startY, Direction)) {
                PlaceShip(ship, startX, startY, Direction);
            }
            else
            {
                shipList.Remove(ship);
            }
        }

        private void PlaceShip(Ship ship, int startX, int startY, string Direction)
        {
            if (Direction.ToLower() == "horizontal")
            {
                for (int i = 0; i < ship.ShipLength(); i++)
                {
                    fields[startX + i, startY].placeShip(ship);
                    fields[startX + i, startY].changeStatus(SquareStatusEnum.Ship);
                }
            }
            else if (Direction.ToLower() == "vertical")
            {
                for (int i = 0; i < ship.ShipLength(); i++)
                {
                    fields[startX, startY + i].placeShip(ship);
                    fields[startX, startY + i].changeStatus(SquareStatusEnum.Ship);
                }
            }
            else {
                throw new Exception("Invalid Direction");
            }
        }

        private string ToStringPlayerBoard()
        {
            string board = "";

            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    int FieldStatus = fields[x, y].getStatus();
                    switch (FieldStatus)
                    {
                        case 0:
                            board += " ";
                            break;
                        case 1:
                            board += "S";
                            break;
                        case 2:
                            board += "H";
                            break;
                        case 3:
                            board += "M";
                            break;
                        default:
                            board += "?";
                            break;
                    }
                }
                board += "\n";
            }
            return board;
        }

        private string ToStringOppenentBoard()
        {
            string board = "";

            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    int FieldStatus = fields[x, y].getStatus();
                    switch (FieldStatus)
                    {
                        case 0:
                            board += " ";
                            break;
                        case 2:
                            board += "H";
                            break;
                        case 3:
                            board += "M";
                            break;
                        default:
                            board += "?";
                            break;
                    }
                }
                board += "\n";
            }
            return board;
        }

        public string ToString(Player ActivePlayer)
        {
            if(ActivePlayer.GetBoard() == this)
            {
                return ToStringPlayerBoard();
            }
            return ToStringOppenentBoard();
        }
    }
}
