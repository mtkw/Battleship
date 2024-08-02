using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Player
    {
        private string _name;
        private Board? _board;
        private Dictionary<string, int> _statistics;

        public Player(string name)
        {
            _name = name;
            _statistics = new Dictionary<string, int>();
        }

        public string PlayerName()
        {
            return _name;
        }

        public void AssignBoardToPlayer(Board board)
        {
            _board = board;
        }

        public Board GetBoard()
        {
            return _board;
        }

        public void Shot(Board board, int x, int y)
        {
            board.Shot(x, y);
        }
    }
}