using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Battleship
{
    internal class Ship
    {
        private ShipTypeEnum shipType;
        private int lenght;
        private List<Square>? Squares = new List<Square>();
        private List<Square>? hits = new List<Square>();
        private List<Square>? restrictedArea = new List<Square>();
        private bool isHitted;
        public bool isSunk;


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

        public void AddHit(Square square)
        {
            hits.Add(square);
        }

        public void AddSquareToSquareList(Square square)
        {
            Squares.Add(square);
        }

        /*TO DO*/
        public void IsShipSunk()
        {
            if (Squares.Count() != 0 && hits.Count() != 0)
            {
                IEnumerable<Square> difference = Squares.Except(hits);
                if (difference.Count() == 0)
                {
                    isSunk = true;
                }
            }
        }





    }
}
