using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Board
    {
        public readonly Player boardOwner;
        public Square[,] fields;
        private int boardSize;
        public List<Ship> shipList = new List<Ship>();
        public int PossibleShipsCount;
        private List<ShipTypeEnum> shipTypeEnumList = new List<ShipTypeEnum>();

        public Board(Player boardOwner, int boardSize)
        {
            this.boardOwner = boardOwner;
            /*            if(boardSize < 5 || boardSize > 15)
                        {
                            throw new Exception("Board size must be between 5 to 15");
                        }
                        else
                        {
                            this.boardSize = boardSize;
                        }*/
            while (boardSize < 4 || boardSize > 16)
            {
                Console.WriteLine("Board size must be between 5 to 15. Please enter a valid board size:");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int validBoardSize))
                {
                    boardSize = validBoardSize;
                }
            }

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
                if (Direction.ToLower() == "horizontal" && startY + Ship.ShipLength() < boardSize)
                {
                    if(IsEmptySpace(Ship, startX, startY, Direction))
                    {
                        return true;
                    }
                }
                if (Direction.ToLower() == "vertical" && startX + Ship.ShipLength() < boardSize) 
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
            bool IsEmptySpace = false;
            for(int i = 0; i < ship.ShipLength(); i++)
            {
                if(Direction.ToLower() == "horizontal")
                {
                    if (fields[startX, startY + i].Status == SquareStatusEnum.Empty)
                    {
                        IsEmptySpace = true;
                    }
                    else
                    {
                        IsEmptySpace = false;
                        break;
                    }
                }
                if(Direction.ToLower() == "vertical")
                {
                    if (fields[startX + i, startY].Status == SquareStatusEnum.Empty)
                    {
                        IsEmptySpace = true;
                    }
                    else
                    {
                        IsEmptySpace = false;
                    }
                }
            }
            return IsEmptySpace;
        }

        public bool PlaceShipOnBoard(Ship ship, int startX, int startY, string Direction)
        {
            if (IsPossiblePlacement(ship, startX, startY, Direction)) {
                PlaceShip(ship, startX, startY, Direction);
                return true;
            }
            else
            {
                return false;
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
                    fields[startX, startY + i].AddRestrictedArea(Direction);/*TO DO*/
                }
            }
            else if (Direction.ToLower() == "vertical")
            {
                for (int i = 0; i < ship.ShipLength(); i++)
                {
                    fields[startX + i, startY].placeShip(ship);
                    fields[startX + i, startY].changeStatus(SquareStatusEnum.Ship);
                    fields[startX + i, startY].AddSquare();
                    fields[startX + i, startY].AddRestrictedArea(Direction);/*TO DO*/
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
        public bool Shot(int x, int y)
        {
            if (IsValidShotCoordinate(x,y))
            {
                if (IsValidShot(x, y))
                {
                    if (fields[x,y].Status == SquareStatusEnum.Ship)
                    {
                        fields[x,y].changeStatus(SquareStatusEnum.Hit);
                        fields[x, y].addHitToList();
                        IsSunk();
                    }
                    if (fields[x,y].Status == SquareStatusEnum.Empty)
                    {
                        fields[x, y].changeStatus(SquareStatusEnum.Missed);
                    }
                    return true;
                }
            }
            return false;
        }

        private void IsSunk()
        {
            foreach (Ship ship in shipList)
            {
                ship.IsShipSunk();
                if (ship.isSunk)
                {
                    foreach (var square in fields)
                    {
                        if (square.IsShipBelongToSquare(ship))
                        {
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
            var board = new StringBuilder();

            board.Append("  ");
            for (int i = 0; i <boardSize; i++)
            {
                board.Append(i + 1);
                board.Append(" ");
            }
            board.AppendLine();

            for (int x = 0; x < boardSize; x++)
            {
                board.Append((char)('A' + x));
                board.Append(" ");

                for (int y = 0; y < boardSize; y++)
                {
                    int FieldStatus = fields[x, y].getStatus();
                    switch (FieldStatus)
                    {
                        case 0:
                            board.Append(" ");
                            break;
                        case 1:
                            board.Append("S");
                            break;
                        case 2:
                            board.Append("H");
                            break;
                        case 3:
                            board.Append("M");
                            break;
                        case 4:
                            board.Append("D");
                            break;
                        default:
                            board.Append("?");
                            break;
                    }
                    board.Append(" ");
                }
                board.AppendLine();
            }
            return board.ToString();
        }

        private string ToStringOppenentBoard()
        {
            var board = new StringBuilder();

            board.Append("  ");
            for(int i = 0; i < boardSize; i++)
            {
                board.Append(i + 1);
                board.Append(" ");
            }
            board.AppendLine();

            for (int x = 0; x < boardSize; x++)
            {
                board.Append((char)('A' + x));
                board.Append(" ");

                for (int y = 0; y < boardSize; y++)
                {
                    int FieldStatus = fields[x, y].getStatus();
                    switch (FieldStatus)
                    {
                        case 0:
                            board.Append(" ");
                            break;
                        case 1:
                            board.Append(" ");
                            break;
                        case 2:
                            board.Append("H");
                            break;
                        case 3:
                            board.Append("M");
                            break;
                         case 4:
                            board.Append("D");
                            break;  
                        default:
                            board.Append("*");
                            break;
                    }
                    board.Append(" ");
                }
                board.AppendLine();
            }
            return board.ToString();
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
