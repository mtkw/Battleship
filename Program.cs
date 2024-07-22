namespace Battleship
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player Player1 = new Player("Stefan");
            Board boardPlayerOne = new Board(Player1, 5);
            Player Player2 = new Player("Zbyszek");
            Board boardPlayerTwo = new Board(Player2, 5);
            Player1.AssignBoardToPlayer(boardPlayerOne);
            Player2.AssignBoardToPlayer(boardPlayerTwo);
            Display displayer = new Display();
            displayer.PrintBoard(boardPlayerOne, Player1);
            displayer.PrintBoard(boardPlayerTwo, Player1);
            boardPlayerOne.CreateShip(ShipTypeEnum.Submarine);
            boardPlayerOne.PlaceShipOnBoard(boardPlayerOne.shipList[0], 0, 0, "horizontal");
            displayer.PrintBoard(boardPlayerOne, Player1);



        }
    }
}
