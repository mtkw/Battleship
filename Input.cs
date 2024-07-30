using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Input
    {
        Display displayer = new Display();

        public string InputPlayerName()
        {
            string playerName;
            string message = "Please Provide Player Name: ";
            displayer.PrintMessage(message);
            playerName = Console.ReadLine();

            return playerName;
        }

        public int InputBoardSize()
        {
            int boardSize;
            string message = "Please Provide Board Size: ";
            displayer.PrintMessage(message);
            string input = Console.ReadLine();
            Int32.TryParse(input, out boardSize);

            if(!Int32.TryParse(input, out boardSize))
            {
                displayer.PrintMessage("Invalid Board Size. Please provide correct board size");
                return InputBoardSize();
            }
            else
            {
                return boardSize;
            }
        }

        public string InputShipDirection()
        {
            string direction = "";
            string message = "Please Provide Direction to Place Ship: ";
            displayer.PrintMessage(message);
            direction = Console.ReadLine();
            return direction;
        }

        public int[] InputCoordinates()
        {
            int[] coordinates;
            string input = "";
            string message = "Please Provide Coordinates to Place Ship: ";
            displayer.PrintMessage(message);
            input = Console.ReadLine();
            coordinates = TranslateCoordinates(input);

            return coordinates;
        }

        public int[] InputShotingCoordinates()
        {
            int[] coordinates;
            string input = "";
            string message = "Please Provide Coordinates to Shot: ";
            displayer.PrintMessage(message);
            input = Console.ReadLine();
            coordinates = TranslateCoordinates(input);

            return coordinates;
        }

        private int[] TranslateCoordinates(string input)
        {
            int[] coordinates;

            if (input.Length == 2)
            {
                string xString = input[0].ToString();
                int x = char.Parse(xString.ToUpper()) - 65;
                string yString = input[1].ToString();
                int y = Convert.ToInt32(yString);
                coordinates = new int[] { x, y };

                return coordinates;
            }
            if (input.Length == 3)
            {
                string xString = input[0].ToString();
                int x = char.Parse(xString.ToUpper()) - 65;
                string yString = input.Substring(1, 2);
                int y = Convert.ToInt32(yString);
                coordinates = new int[] { x, y };
                return coordinates;
            }

            else
            {
                string message = "Invalid Cordinates";
                displayer.PrintMessage($"{message} {input}");
                return InputCoordinates();
            }
        }
    }
}
