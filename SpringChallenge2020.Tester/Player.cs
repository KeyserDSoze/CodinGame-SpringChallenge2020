using SpringChallenge2020.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace SpringChallenge2020.Tester
{
    class Player
    {
        static List<string> Inputs = new List<string>();
        static List<string> Points = new List<string>();
        static Player()
        {
            using (StreamReader streamReader = new StreamReader(@"C:\Users\aless\source\repos\SpringChallenge2020\SpringChallenge2020.Tester\Input.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    string value = streamReader.ReadLine();
                    if (value.StartsWith("Parse: "))
                        Inputs.Add(value.Replace("Parse: ", string.Empty));
                    if (value.StartsWith("PP: "))
                        Points.Add(value.Replace("PP: ", string.Empty));
                }
            }
        }
        static int Counter = 0;
        static string Next()
            => Inputs[Counter++];
        static int CounterPoints = 0;
        static string NextPoint()
            => Points[CounterPoints++];
        static void Main(string[] args)
        {
            Console.WriteLine("Insert the stop debug");
            int turn = int.Parse(Console.ReadLine());
            string[] inputs;
            string input = Next();
            inputs = input.Split(' ');
            int width = int.Parse(inputs[0]); // size of the grid
            int height = int.Parse(inputs[1]); // top left corner is (x=0, y=0)
            Manager manager = new Manager(width, height);
            for (int i = 0; i < height; i++)
            {
                string row = Next(); // one line of the grid: space " " is floor, pound "#" is wall
                manager.Map.AddRow(i, row);
            }
            int count = 0;
            // game loop
            while (true)
            {
                manager.NewTurn();
                count++;
                if (count == turn)
                {
                    string stop = string.Empty;
                }
                input = Next();
                inputs = input.Split(' ');
                int myScore = int.Parse(inputs[0]);
                int opponentScore = int.Parse(inputs[1]);
                manager.SetScore(myScore, opponentScore);
                input = Next();
                int visiblePacCount = int.Parse(input); // all your pacs and enemy pacs in sight
                for (int i = 0; i < visiblePacCount; i++)
                {
                    input = Next();
                    inputs = input.Split(' ');
                    int pacId = int.Parse(inputs[0]); // pac number (unique within a team)
                    bool mine = inputs[1] != "0"; // true if this pac is yours
                    int x = int.Parse(inputs[2]); // position in the grid
                    int y = int.Parse(inputs[3]); // position in the grid
                    string typeId = inputs[4]; // unused in wood leagues
                    int speedTurnsLeft = int.Parse(inputs[5]); // unused in wood leagues
                    int abilityCooldown = int.Parse(inputs[6]); // unused in wood leagues
                    manager.SetPac(pacId, mine, x, y, typeId, speedTurnsLeft, abilityCooldown);
                }
                input = Next();
                int visiblePelletCount = int.Parse(input); // all pellets in sight
                for (int i = 0; i < visiblePelletCount; i++)
                {
                    input = NextPoint();
                    inputs = input.Split(' ');
                    if (inputs.Length != 3)
                        break;
                    int x = int.Parse(inputs[0]);
                    int y = int.Parse(inputs[1]);
                    if (inputs[2] == "...")
                        break;
                    int value = int.Parse(inputs[2]); // amount of points this pellet is worth
                    if (value > 1)
                        manager.Map.SetSuperPellet(new Position(x, y));
                }

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");
                manager.Map.Draw();
                manager.MoveMine();
            }
        }
    }
    public static class MapTypeExtensions
    {
        public static void Draw(this Map map)
        {
            for (int j = 0; j < map.Height; j++)
            {
                for (int i = 0; i < map.Width; i++)
                    map[new Position(i, j)].Write();
                Console.WriteLine();
            }
        }
        public static void Write(this Cell cell)
        {
            switch (cell.MapType)
            {
                default:
                case MapType.Ate:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ");
                    Console.ResetColor();
                    break;
                case MapType.Wall:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("#");
                    Console.ResetColor();
                    break;
                case MapType.EnemyPac:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("E");
                    Console.ResetColor();
                    break;
                case MapType.MyPac:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("M");
                    Console.ResetColor();
                    break;
                case MapType.Pellet:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("o");
                    Console.ResetColor();
                    break;
                case MapType.SuperPellet:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("@");
                    Console.ResetColor();
                    break;
            }
        }
    }
}
