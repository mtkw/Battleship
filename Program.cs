namespace Battleship
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Display displayer = new Display();
            Player player1 = new Player("stefan");
            Board board1 = new Board(player1, 5);
            player1.AssignBoardToPlayer(board1);
            displayer.PrintBoard(board1, player1);
            board1.PlaceShipOnBoard(board1.shipList[0], 0, 0, "horizontal"); 
            displayer.PrintBoard(board1, player1);
            board1.PlaceShipOnBoard(board1.shipList[1], 1, 0, "horizontal");
            displayer.PrintBoard(board1, player1);
            board1.PlaceShipOnBoard(board1.shipList[2], 0, 2, "horizontal");
            displayer.PrintBoard(board1, player1);

            Console.WriteLine((int)board1.fields[1,1].Status);
            Console.WriteLine((int)SquareStatusEnum.Ship);

        }
    }
}
