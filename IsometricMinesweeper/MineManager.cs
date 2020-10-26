using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsometricGameEngine;

namespace IsometricMinesweeper
{
    class MineManager
    {
        int NumOfMines;

        public TileMap mineMap;

        Random rand = new Random();

        private string[,] grid;

        public MineManager(IsometricGrid3D isometricGrid, int MineCount)
        {
            NumOfMines = MineCount;

            generateMines(isometricGrid);

            mineMap = new TileMap(grid, "0");
        }

        private void generateMines(IsometricGrid3D isometricGrid)
        {
            int length = isometricGrid.GridSize;

            //int placedMines = 0;

            int x, y;

            grid = new string[length, length];

            // Placing Mines
            for (int i = 0; i < NumOfMines; i++)
            {
                x = rand.Next(0, length);
                y = rand.Next(0, length);

                while (grid[x, y] == "f")
                {
                    x = rand.Next(0, length);
                    y = rand.Next(0, length);
                }

                grid[x, y] = "f";
            }

            // Filling in blanks
            for (int for_y = 0; for_y < length; for_y++)
            {
                for (int for_x = 0; for_x < length; for_x++)
                {
                    if (grid[for_x, for_y] != "f")
                    {
                        grid[for_x, for_y] = "0";
                    }
                }
            }
        }
    }
}
