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
        private List<Square> hits { get; set; }
        private bool isHitted { get; set; }
        private bool isSunk { get; set; }


        //Bez metody isHit() niepotrzebna jest
        //Potrzebna jest metoda isSunk()
        public Ship(ShipTypeEnum shipType)
        {
            this.shipType = shipType;
            this.lenght = (int)shipType;
            hits = new ();
            isHitted = false;
            isSunk = false;
        }
    }
}
