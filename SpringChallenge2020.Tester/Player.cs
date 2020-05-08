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
        static Player()
        {
            using (StreamReader streamReader = new StreamReader(@"C:\Users\aless\source\repos\SpringChallenge2020\SpringChallenge2020.Tester\Input.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    string value = streamReader.ReadLine();
                    if (value.StartsWith("Parse: "))
                        Inputs.Add(value.Replace("Parse: ", string.Empty));
                }
            }
        }
        static int Counter = 0;
        static string Next()
            => Inputs[Counter++];
        static void Main(string[] args)
        {
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

            // game loop
            while (true)
            {
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
                    input = Next();
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
    }
}
