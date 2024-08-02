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
        private bool PlayerOneEndMove = false;
        private bool PlayerTwoEndMove = true;


        public Game()
        {
            _players = new List<Player>();
            _boards = new List<Board>();
        }

        public List<Player> Players
        {
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

        public void AddBoard(Board board)
        {
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
                foreach (Ship ship in board.ShipList())
                {
                    while (true)
                    {
                        Display.DisplayShipInfo(ship);
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

                if (oponentBoard.getField(coordinates[0], coordinates[1]).Status == SquareStatusEnum.Hit || oponentBoard.getField(coordinates[0], coordinates[1]).Status == SquareStatusEnum.Sunk)
                {
                    EndPlayerTurn = false;
                    return EndPlayerTurn;
                }
                else
                {
                    EndPlayerTurn = true;
                    return EndPlayerTurn;
                }

            }
            Display.PrintMessage("Wrong Shooting Coordinates. Please provide correct coordinates");
            EndPlayerTurn = false;
            return EndPlayerTurn;
        }

        private bool Round(Player player, Player opponent)
        {
            bool IsMissed = false;
            while (!IsMissed)
            {
                if (!ShootingPhase(player, opponent))
                {
                    Display.PrintMessage("Hitted !!!");
                    IsMissed = false;
                    if (WinningCondition(opponent))
                    {
                        IsWinner = true;
                        return true;
                    }
                    continue;
                }
                Display.PrintMessage(player.PlayerName() + " you missed");
                IsMissed = true;
                return IsMissed;
            }
            return IsMissed;
        }

        public void PlayGame()
        {
            /*Start and Init Game*/
            InitGame();
            PlacementPhase();

            /*Shooting Phase wiht checking winning condition*/
            while (!IsWinner)
            {
                Display.ClearConsole();
                if (!PlayerOneEndMove)
                {
                    Display.PrintMessage(_players[0].PlayerName() + " Is shooting");
                    if (Round(_players[0], _players[1]))
                    {
                        PlayerOneEndMove = true;
                        PlayerTwoEndMove = false;
                        if (IsWinner)
                        {
                            Display.PrintMessage("Player One Win Game");
                            break;
                        }
                        continue;
                    }
                }
                if (!PlayerTwoEndMove)
                {
                    Display.PrintMessage(_players[1].PlayerName() + " Is shooting");
                    if (Round(_players[1], _players[0]))
                    {
                        PlayerOneEndMove = false;
                        PlayerTwoEndMove = true;
                        if (IsWinner)
                        {
                            Display.PrintMessage("Player Two Win Game");
                            break;
                        }
                        continue;
                    }
                }
            }
        }

        private bool WinningCondition(Player opponent)
        {
            Board opponentBoard = opponent.GetBoard();
            foreach (Ship ship in opponentBoard.ShipList())
            {
                if (!ship.IsSunk())
                {
                    return false;
                }
            }
            return true;
        }
    }
}