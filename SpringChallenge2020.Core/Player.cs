using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SpringChallenge2020.Core
{
    class Player
    {
        static void Main(string[] args)
        {
            string[] inputs;
            string input = Console.ReadLine();
            Log(input);
            inputs = input.Split(' ');
            int width = int.Parse(inputs[0]); // size of the grid
            int height = int.Parse(inputs[1]); // top left corner is (x=0, y=0)
            Manager manager = new Manager(width, height);
            for (int i = 0; i < height; i++)
            {
                string row = Console.ReadLine(); // one line of the grid: space " " is floor, pound "#" is wall
                Log(row);
                manager.Map.AddRow(i, row);
            }

            // game loop
            while (true)
            {
                input = Console.ReadLine();
                Log(input);
                inputs = input.Split(' ');
                int myScore = int.Parse(inputs[0]);
                int opponentScore = int.Parse(inputs[1]);
                manager.SetScore(myScore, opponentScore);
                input = Console.ReadLine();
                Log(input);
                int visiblePacCount = int.Parse(input); // all your pacs and enemy pacs in sight
                for (int i = 0; i < visiblePacCount; i++)
                { 
                    input = Console.ReadLine();
                    Log(input);
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
                input = Console.ReadLine();
                Log(input);
                int visiblePelletCount = int.Parse(input); // all pellets in sight
                for (int i = 0; i < visiblePelletCount; i++)
                {
                    input = Console.ReadLine();
                    LogPoint(input);
                    inputs = input.Split(' ');
                    int x = int.Parse(inputs[0]);
                    int y = int.Parse(inputs[1]);
                    int value = int.Parse(inputs[2]); // amount of points this pellet is worth
                }

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                manager.MoveMine();
            }
        }
        static void Log(string input)
        {
            Console.Error.WriteLine($"Parse: {input}");
        }
        static void LogPoint(string input)
        {
            Console.Error.WriteLine($"PP: {input}");
        }
    }
}
