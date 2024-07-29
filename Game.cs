using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Game
    {
        private List<Player> _players;
        private List<Board> _boards;
        private Input Input = new Input();
        private Display Display = new Display();
        private bool EndPlayerTurn = false;
        private bool IsWinner = false;

        public Game()
        {
            _players = new List<Player>();
            _boards = new List<Board>();
        }

        public List<Player> Players { 
            get 
            { return _players; } 
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        public List<Board> Boards
        {
            get 
            { return _boards; } 
        }

        public void AddBoard(Board board) { 
            _boards.Add(board);
        }

        public void InitGame()
        {
            _players.Clear();
            _boards.Clear();
            string PlayerOneName = Input.InputPlayerName();
            string PlayerTwoName = Input.InputPlayerName();

            Player PlayerOne = new Player(PlayerOneName);
            Player PlayerTwo = new Player(PlayerTwoName);

            int boardSize = Input.InputBoardSize();

            Board boardPlayerOne = BoardFactory.CreateBoard(PlayerOne, boardSize);
            Board boardPlayerTwo = BoardFactory.CreateBoard(PlayerTwo, boardSize);

            PlayerOne.AssignBoardToPlayer(boardPlayerOne);
            PlayerTwo.AssignBoardToPlayer(boardPlayerTwo);

            _players.Add(PlayerOne);
            _players.Add(PlayerTwo);

            _boards.Add(boardPlayerOne);
            _boards.Add(boardPlayerTwo);

        }

        public void PlacementPhase()
        {
            foreach (Player player in _players)
            {
                Board board = player.GetBoard();
                Display.PrintMessage("Placement Phase");
                Display.PrintPlayer(player);
                foreach(Ship ship in board.shipList)
                {
                    while (true)
                    {
                        Display.PrintShipLength(ship);/*Nazwa + długość*/
                        int[] coordinates = Input.InputCoordinates();
                        string direction = Input.InputShipDirection();
                        if(board.PlaceShipOnBoard(ship, coordinates[0], coordinates[1], direction))
                        {
                            Display.PrintBoard(board, player);
                            break;
                        }
                        else
                        {
                            Display.PrintMessage("Wrong Coordinates or Not Enough Space to place ship");
                            continue;
                        }
                        
                    }

                }
            }
        }

        private bool ShootingPhase(Player player, Player oponent)
        {
                Board oponentBoard = oponent.GetBoard();
                int[] coordinates = Input.InputCoordinates();
                if (oponentBoard.Shot(coordinates[0], coordinates[1]))
                {
                    Display.PrintBoard(oponentBoard, player);
                    if (oponentBoard.fields[coordinates[0], coordinates[1]].Status == SquareStatusEnum.Hit)
                    {
                        EndPlayerTurn = false;
                    }
                    else
                    {
                        EndPlayerTurn = true;
                        return EndPlayerTurn;
                    }

                }
                else
                {
                    Display.PrintMessage("Wrong Shooting Coordinates. Please provide correct coordinates");
                    EndPlayerTurn = false;
                }
            return EndPlayerTurn;
        }

        public void Round()
        {
            while (WinningCondition())
            {
                foreach (Player currentPlayer in _players)
                {
                    int CurrentPlayerIndex = _players.IndexOf(currentPlayer) + 1;
                    int NumberOfPlayers = _players.Count();
                    Player oponent = _players[NumberOfPlayers - CurrentPlayerIndex];
                    while (true)
                    {
                        if(ShootingPhase(currentPlayer, oponent))
                        {
                            Display.PrintMessage("Hitted !!!");
                            continue;
                        }
                        else
                        {
                            Display.PrintMessage(currentPlayer + $" you missed");
                            break;
                        }
                    }
                }
            }
            Display.PrintMessage("END GAME!!!");
        }

        private bool WinningCondition()
        {
            foreach (Player player in _players) { 
                Board PlayerBoard = player.GetBoard();
                foreach (Ship ship in PlayerBoard.shipList)
                {
                    if (ship.isSunk)
                    {
                        IsWinner = true;
                    }
                    else
                    {
                        IsWinner = false;
                        break;
                    }
                }
            }
            return IsWinner;
        }
    }
}
