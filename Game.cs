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
                int[] coordinates = Input.InputShotingCoordinates();
                if (oponentBoard.Shot(coordinates[0], coordinates[1]))
                {
                Display.PrintMessage("Shooting Phase");
                Display.PrintPlayer(player);
                Display.PrintMessage("Oponent Board");
                Display.PrintBoard(oponentBoard, player);
                return oponentBoard.fields[coordinates[0], coordinates[1]].Status == SquareStatusEnum.Hit;
                /*                    if (oponentBoard.fields[coordinates[0], coordinates[1]].Status == SquareStatusEnum.Hit)
                                    {
                                        EndPlayerTurn = false;
                                    }
                                    else
                                    {
                                        EndPlayerTurn = true;
                                    }*/

                }
                else
                {
                    Display.PrintMessage("Wrong Shooting Coordinates. Please provide correct coordinates");
                /*                    EndPlayerTurn = false;*/
                    return false;
                }
/*            return EndPlayerTurn;*/
        }

        private bool Round(Player player, Player oponent)
        {
/*            bool IsMissed = false;

            while (!IsMissed)
            {
                if (ShootingPhase(player, oponent))
                {
                    Display.PrintMessage("Hitted !!!");
                }
                Display.PrintMessage(player + $" you missed");
                IsMissed = true;
            }
            return EndPlayerTurn;*/
            while (ShootingPhase(player, oponent))
            {
                Display.PrintMessage("Hit!");
            }
            Display.PrintMessage($"{player.PlayerName()} you missed");
            return true;
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
                /*                if (!EndPlayerTurn)
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
                                }*/
                Display.PrintMessage($"{currentPlayer.PlayerName()} is shooting");
                Round(currentPlayer, opponentPlayer);
                if (WinningCondition())
                {
                    break;
                }

                // Swap players
                Player temp = currentPlayer;
                currentPlayer = opponentPlayer;
                opponentPlayer = temp;
            }
        }

        private bool WinningCondition()
        {
            /*            foreach (Player player in _players) { 
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

                        return IsWinner;*/
            foreach (Player player in _players)
            {
                Board playerBoard = player.GetBoard();
                if (playerBoard.shipList.All(ship => ship.isSunk))
                {
                    IsWinner = true;
                    Display.PrintMessage($"{player.PlayerName()} has won the game!");
                    return true;
                }
            }
            IsWinner = false;
            return false;
        }
    }
}
