namespace Battleship
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Display displayer = new Display();
            Player player1 = new Player("stefan");
            Player player2 = new Player("zbyszek");

            Board board1 = new Board(player1, 5);
            Board board2 = new Board(player2, 5);

            player1.AssignBoardToPlayer(board1);
            player2.AssignBoardToPlayer(board2);

            board1.PlaceShipOnBoard(board1.shipList[0], 0, 0, "horizontal");
            board1.PlaceShipOnBoard(board1.shipList[1], 2, 0, "horizontal");
            board1.PlaceShipOnBoard(board1.shipList[2], 0, 2, "horizontal");
            displayer.PrintBoard(board1, player1);
            Console.WriteLine("-------------------------------------------");
            board2.PlaceShipOnBoard(board2.shipList[0], 0, 0, "horizontal");
            board2.PlaceShipOnBoard(board2.shipList[1], 2, 0, "horizontal");
            board2.PlaceShipOnBoard(board2.shipList[2], 0, 2, "horizontal");
            displayer.PrintBoard(board2, player2);

            player1.Shot(board2,0,0);
            player1.Shot(board2,1,0);
            player1.Shot(board2,2,0);
            player1.Shot(board2,3,0);
            player1.Shot(board2,0,4);
            Console.WriteLine("-------------");
            displayer.PrintBoard(board2,player2);
            board2.IsSunk();
            displayer.PrintBoard(board2, player2);






        }
    }
}
