using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsometricGameEngine;

namespace IsometricMinesweeper
{
    class FlagMap
    {
        public TileMap Map;

        public string[,] grid;

        public FlagMap(IsometricGrid2D isometricGrid)
        {
            int length = isometricGrid.gridSize;

            grid = new string[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int x = 0; x < length; x++)
                {
                    grid[i, x] = "0";
                }
            }

            Map = new TileMap(grid, "0");
        }

        public void updateMap()
        {
            Map = new TileMap(grid, "0");
        }
    }
}
