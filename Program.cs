using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GamePacman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            bool isPlaying = true;
            Console.WriteLine("Выберете на какой карте будете играть. 1 2 или 3?");
            Console.Write("Введите номер выбраной карты:");

            string userInput;
            string mapName = "";
            userInput = Console.ReadLine();


            switch (userInput)
            {
                case "1":
                mapName = "map1";
                break;                
                case "2":
                mapName = "map2";
                break;                
                case "3":
                mapName = "map3";
                break;

            }

            int pacmanX, pacmanY;
            int pacmanDX = 0, pacmanDY = 1;

            char[,] map = ReadMap(mapName, out pacmanX, out pacmanY);
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            DrawMap(map);

            while(isPlaying)
            {
                Console.SetCursorPosition(0, 25);
                Console.WriteLine(pacmanX);
                Console.WriteLine(pacmanY);
                Console.SetCursorPosition(pacmanY, pacmanX); //нужно смотреть за позициями курсора а то промах
                Console.Write('Y');
                

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    ChangeDirection(key, ref pacmanDX, ref pacmanDY);

                }

                if (map[pacmanX+pacmanDX,pacmanY+pacmanDY] != '#')
                {
                    Move(ref pacmanX,ref pacmanY,pacmanDX, pacmanDY);
                }
            }
        }

        static char[,] ReadMap(string mapName, out int packmanX, out int packmanY)
        {
            packmanX = 0;
            packmanY = 0;

            string[] newFile = File.ReadAllLines($"Maps/{mapName}.txt");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newFile[i][j];

                    if (map[i, j] == '@')
                    {
                        packmanX = i;
                        packmanY = j;
                    }
                }
            }

            return map;
        }


        //static void Draw


        static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void Move(ref int X, ref int Y, int DX, int DY)
        {
            Console.SetCursorPosition(Y, X);    
            Console.Write(' ');

            X += DX;
            Y += DY;

            Console.SetCursorPosition(Y, X);
            Console.Write('Y');

            System.Threading.Thread.Sleep(200);
        }

        static void ChangeDirection(ConsoleKeyInfo key, ref int DX, ref int DY)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    DX = -1; DY = 0;
                    break;
                case ConsoleKey.DownArrow:
                    DX = 1; DY = 0;
                    break;
                case ConsoleKey.LeftArrow:
                    DX = 0; DY = -1;
                    break;
                case ConsoleKey.RightArrow:
                    DX = 0; DY = 1;
                    break;
            }
        }
    }
}
