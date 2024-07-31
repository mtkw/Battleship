using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private bool PlayerOneEndMove = false;
        private bool PlayerTwoEndMove = true;


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

        private void InitGame()
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

        private void PlacementPhase()
        {
            foreach (Player player in _players)
            {
                Board board = player.GetBoard();
                Display.PrintMessage("Placement Phase");
                Display.PrintMessage(player.PlayerName());
                foreach(Ship ship in board.shipList)
                {
                    while (true)
                    {
                        Display.PrintMessage(ship.ShipType + " size: " + ship.ShipLength);
                        int[] coordinates = Input.InputCoordinates();
                        string direction = Input.InputShipDirection();
                        if (board.PlaceShipOnBoard(ship, coordinates[0], coordinates[1], direction))
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
                int[] coordinates = Input.InputShotingCoordinates();
                if (oponentBoard.Shot(coordinates[0], coordinates[1]))
                {
                Display.PrintMessage("Shooting Phase");
                Display.PrintMessage(player.PlayerName());
                Display.PrintMessage("Player Board");
                Display.PrintBoard(player.GetBoard(), player); 
                Display.PrintMessage("Oponent Board");
                Display.PrintBoard(oponentBoard, player);
                    if (oponentBoard.fields[coordinates[0], coordinates[1]].Status == SquareStatusEnum.Hit)
                    {
                        EndPlayerTurn = false;
                    }
                    else
                    {
                        EndPlayerTurn = true;
                    }

                }
                else
                {
                    Display.PrintMessage("Wrong Shooting Coordinates. Please provide correct coordinates");
                    EndPlayerTurn = false;
                }
            return EndPlayerTurn;
        }

        private bool Round(Player player, Player oponent)
        {
            bool IsMissed = false;

            while (!IsMissed)
            {
                if(ShootingPhase(player, oponent))
                {
                    Display.PrintMessage("Hitted !!!");
                    IsMissed = false;
                    continue;
                }
                    Display.PrintMessage(player + $" you missed");
                    IsMissed = true;    
            }
            return EndPlayerTurn;
        }

        public void PlayGame()
        {
            /*Start and Init Game*/
            InitGame();
            PlacementPhase();

            Player currentPlayer = _players[0];
            Player opponentPlayer = _players[1];

            /*Shooting Phase wiht checking winning condition*/
            while (!IsWinner)
            {
                if (!EndPlayerTurn)
                {
                    Console.WriteLine(_players[0].PlayerName() + "Is shooting");
                    ShootingPhase(_players[0], _players[1]);
                    WinningCondition();
                }
                else
                {
                    Console.WriteLine(_players[1].PlayerName() + "Is shooting");
                    ShootingPhase(_players[1], _players[0]);
                    WinningCondition();
                }
            }
        }

        private bool WinningCondition()
        {
            foreach (Player player in _players) { 
                Board PlayerBoard = player.GetBoard();
                foreach (Ship ship in PlayerBoard.shipList)
                {
                    if (!ship.isSunk)
                    {
                        IsWinner = false;
                        break;
                    }
                    else
                    {
                        IsWinner = true;
                        continue;
                    }
                }
            }

            return IsWinner;
        }
    }
}
