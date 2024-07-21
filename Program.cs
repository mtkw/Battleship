namespace Battleship
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player Player1 = new Player();
            Board boardPlayerOne = new Board(Player1, 5);
            Console.WriteLine(ShipTypeEnum.Submarine);

        }
    }
}
