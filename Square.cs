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
        private SquareStatusEnum status { get; set; }
        private int CoordinateX { get; set; }
        private int CoordinateY { get; set; }
        private Ship Ship { get; set; }

        public Square(int coordinateX, int coordinateY)
        {
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
            status = 0;
        }
    }
}
