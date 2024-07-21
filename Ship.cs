using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Ship
    {
        private ShipTypeEnum shipType { get; }
        private int lenght { get; }
        private List<Square>? Squares { get; set; }
        private List<Square> hits = new();
        private bool isHitted = false;
        private bool isSunk = false;

        public Ship(ShipTypeEnum shipType, List<Square> squares)
        {
            this.shipType = shipType;
            this.lenght = (int)shipType;
            Squares = new (lenght);
        }
    }
}
