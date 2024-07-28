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
        private SquareStatusEnum status;
        private int CoordinateX;
        private int CoordinateY;
        private Ship? Ship;

        public SquareStatusEnum Status
        {
            get
            { return status; }
        }

        public int getStatus()
        {
            return (int)status;
        }

        public Square(int coordinateX, int coordinateY)
        {
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
            status = 0;
        }

        public void placeShip(Ship ship) { 
            this.Ship = ship;
        }

        public void changeStatus(SquareStatusEnum status) { 
            this.status = status;
        }

        public void addHitToList()
        {
            if(status == SquareStatusEnum.Hit)
            {
                Ship.AddHit(this);
            }
        }

        public void AddSquare() {
            Ship.AddSquareToSquareList(this);
        }

        public void AddRestrictedArea(string Direction)
        {
            if(Direction.ToLower() =="horizontal")
            {
                
            }
            if(Direction.ToLower() == "vertical")
            {

            }
        }

        public bool IsShipBelongToSquare(Ship ship) {
            if(ship == this.Ship)
            {
                return true;
            }
            return false;
        }

      
    }
}
