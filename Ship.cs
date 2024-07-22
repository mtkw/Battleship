using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Ship
    {
        private ShipTypeEnum shipType;
        private int lenght;
        private List<Square>? Squares;
        private List<Square> hits;
        private bool isHitted;
        private bool isSunk;


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

        public int ShipLength()
        {
            return lenght;
        }

        public string ShipType()
        {
            return shipType.ToString();
        }

/*        public string ShipType()
        {
            string shipType = "";
            switch (lenght)
            {
                case 1:
                    shipType = "Carrier";
                    break;
            }
        }*/

    }
}
