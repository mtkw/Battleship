using System;
using System.Collections.Generic;
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
            foreach (Board board in _boards)
            {
                
            }
        }
    }
}
