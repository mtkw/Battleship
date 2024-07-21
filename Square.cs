using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Battleship
{
    internal class Square
    {
        private SquareStatusEnum status = 0;
        private int CoordinateX { get; set; }
        private int CoordinateY { get; set; }

        public Square(int coordinateX, int coordinateY)
        {
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
        }
    }
}
