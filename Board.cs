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
        public Square[,] fields;
        private int boardSize;
        public List<Ship> shipList = new List<Ship>();
        public int PossibleShipsCount;
        private List<ShipTypeEnum> shipTypeEnumList = new List<ShipTypeEnum>();

        public Board(Player boardOwner, int boardSize)
        {
            this.boardOwner = boardOwner;
            this.boardSize = boardSize;
            this.fields = new Square[boardSize, boardSize];
            if(boardSize >= 5 && boardSize <= 8)
            {
                this.PossibleShipsCount = 3;
            }
            if(boardSize >= 9 && boardSize <= 12)
            {
                this.PossibleShipsCount = 4;
            }
            if(boardSize > 12 && boardSize <= 15)
            {
                this.PossibleShipsCount = 5;
            }
            InitEmptyBoard();
            getShipTypesEnum();
            CreateShip();
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
/*        public void CreateShip(ShipTypeEnum shipType)
        {
            Ship Ship = new Ship(shipType);
            shipList.Add(Ship);
        }*/

        private void CreateShip()
        {
            for (int x = 0; x < PossibleShipsCount; x++)
            {
                Ship Ship = new Ship(shipTypeEnumList[x]);
                shipList.Add(Ship);
            }
        }

        private void getShipTypesEnum()
        {
            var shipTypes = Enum.GetNames(typeof(ShipTypeEnum));
            foreach (string shipType in shipTypes)
            {
                if (Enum.TryParse(shipType, out ShipTypeEnum enumValue))
                {
                    shipTypeEnumList.Add(enumValue);
                }
            }
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
                    if(IsEmptySpace(Ship, startX, startY, Direction))
                    {
                        return true;
                    }
                }
                if (Direction.ToLower() == "vertical" && startY + Ship.ShipLength() < boardSize) 
                { 
                    if(IsEmptySpace(Ship, startY, startX, Direction))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsEmptySpace(Ship ship, int startX, int startY, string Direction) 
        {
            if (Direction.ToLower() == "horizontal") 
            {
                for(int y = 0; y < ship.ShipLength(); y++)
                {
                    if ((int)fields[startX, startY + y].Status != (int)SquareStatusEnum.Empty)
                    {
                        return false;
                    }
                }
            }
            if (Direction.ToLower() == "vertical")
            {
                for (int x = startX; x < ship.ShipLength(); x++)
                {
                    if (fields[startX + x, startY].getStatus() != (int)SquareStatusEnum.Empty)
                    {
                        return false;
                    }
                }
            }
                return true;
        }

        public bool PlaceShipOnBoard(Ship ship, int startX, int startY, string Direction)
        {
            if (IsPossiblePlacement(ship, startX, startY, Direction)) {
                PlaceShip(ship, startX, startY, Direction);
                return true;
            }
            else
            {
                throw new Exception("Wrong Coordinates. Please provide correct coordinates");
            }
        }

        private void PlaceShip(Ship ship, int startX, int startY, string Direction)
        {
            if (Direction.ToLower() == "horizontal")
            {
                for (int i = 0; i < ship.ShipLength(); i++)
                {
                    fields[startX, startY + i].placeShip(ship);
                    fields[startX, startY + i].changeStatus(SquareStatusEnum.Ship);
                    fields[startX, startY + i].AddSquare();
                    fields[startX, startY + i].AddRestrictedArea(Direction);
                }
            }
            else if (Direction.ToLower() == "vertical")
            {
                for (int i = 0; i < ship.ShipLength(); i++)
                {
                    fields[startX + i, startY].placeShip(ship);
                    fields[startX + i, startY].changeStatus(SquareStatusEnum.Ship);
                    fields[startX + i, startY].AddSquare();
                    fields[startX + i, startY].AddRestrictedArea(Direction);
                }
            }
            else {
                throw new Exception("Invalid Direction");
            }
        }
        private bool IsValidShotCoordinate(int x, int y)
        {
            if(x >= 0 && x < boardSize)
            {
                if(y >= 0 && y < boardSize)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsValidShot(int x, int y)
        {
            if (fields[x,y].Status == SquareStatusEnum.Ship || fields[x, y].Status == SquareStatusEnum.Empty)
            {
                return true;
            }
            return false;
        }
        public void Shot(int x, int y)
        {
            if (IsValidShotCoordinate(x,y))
            {
                if (IsValidShot(x, y))
                {
                    if (fields[x,y].Status == SquareStatusEnum.Ship)
                    {
                        fields[x,y].changeStatus(SquareStatusEnum.Hit);
                        fields[x, y].addHitToList();
                    }
                    if (fields[x,y].Status == SquareStatusEnum.Empty)
                    {
                        fields[x, y].changeStatus(SquareStatusEnum.Missed);
                    }
                }
            }
            else
            {
                throw new Exception("You already shooted on this field. Please provide correct shooting coordinates");
            }
        }

        public void IsSunk()
        {
            foreach (var ship in shipList)
            {
                ship.IsShipSunk();
                if (ship.isSunk)
                {
                    foreach(var square in fields)
                    {
                        if (square.IsShipBelongToSquare(ship)){
                            square.changeStatus(SquareStatusEnum.Sunk);
                        }
                    }
                }
            }
        }

        public void ChangeFiledsStatusToSunk(Ship ship)
        {

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
                        case 4:
                            board += "D";
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
                         case 4:
                            board += "D";
                            break;  
                        default:
                            board += "*";
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
