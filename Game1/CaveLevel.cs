//Spencer Corkran
//GSD III
//Tonedeaf Studios

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class CaveLevel:InstanceLevel
    {
        private Tile[,] board2;
        private MersenneTwister rng = new MersenneTwister();
        private int wall_Prob;

        public CaveLevel(int wallChance, int size_y, int size_x)
            : base(size_y, size_x, true)
        {
            wall_Prob = wallChance;
            board = new Tile[base.Size_Y, base.Size_X];
            board2 = new Tile[base.Size_Y, base.Size_X];
        }
        public CaveLevel(int size_y, int size_x)
            : base(size_y, size_x, false)
        {
            board = new Tile[base.Size_Y, base.Size_X];
        }
        public void caveGen()
        {
            //sets the whole map to "wall"
            for (int i = 0; i < base.Size_Y; i++)
            //goes through each row
            {
                for (int j = 0; j < base.Size_X; j++)
                //goes through each column in a row
                {
                    board[i, j] = Tile.W;
                }
            }
            board2 = board;
            //randomizes everything but the edges of the map
            for (int i = 1; i < base.Size_Y - 1; i++)
            //goes through each row
            {
                for (int j = 1; j < base.Size_X - 1; j++)
                //goes through each column in a row
                {
                    if (rng.Next(100) <= wall_Prob)
                        board[i, j] = Tile.W;
                    else
                        board[i, j] = Tile.f;
                }
            }
        }
        /* This method uses cellular automata to bring order to the randomised
         * grid.  Basically, it goes through each of the tiles that isn't on the
         * edge.  If that tile is surrounded by 5 or more walls (including itself)
         * it becomes or remains a wall.  Otherwise, it "starves" and becomes a
         * floor tile.  Algorithm 1 also changes a floor tile into a wall if it
         * is surrounded by less than 2 wall tiles.  More iterations
         * of this method results in smoother caves, less iterations rougher.*/
        public void ageDungeon(bool emptySpaceWall)
        {
            /* yi/xi are the x/y coordinates of the source tile.
             * ii and jj are used to loop through the immediate
             * neighbors of the source tile.  So, [yi+ii,xi+jj]
             * includes all 9 tiles centered around the source
             * (including the source tile)*/
            int yi, xi, ii, jj;
            for (yi = 1; yi < base.Size_Y - 1; yi++)
                for (xi = 1; xi < base.Size_X - 1; xi++)
                {
                    /*running total of surrounding walls,
                     * including the source tile*/
                    int adjWallCount = 0;

                    for (ii = -1; ii <= 1; ii++)
                        for (jj = -1; jj <= 1; jj++)
                        {
                            if (board[yi + ii, xi + jj] != Tile.f)
                                adjWallCount++;
                        }
                    //Algorithm 1
                    if (emptySpaceWall == true)
                    {
                        //5 or more walls OR less than 2 walls surrounding source tile
                        if (adjWallCount >= 5 || adjWallCount < 2)
                            board2[yi, xi] = Tile.W;
                        else
                            board2[yi, xi] = Tile.f;
                    }
                    //Algorithm 2
                    else
                    {
                        //5 or more walls surrounding source tile
                        if (adjWallCount >= 5)
                            board2[yi, xi] = Tile.W;
                        else
                            board2[yi, xi] = Tile.f;
                    }
                }
            /*copies board2 onto board.  If we don't update each tile 'simultaneously'
             * (i.e. we use the updated tile in the next tile's calculations) the
             * algorithm doesn't really seem to work very well*/
            for (yi = 1; yi < base.Size_Y - 1; yi++)
                for (xi = 1; xi < base.Size_X - 1; xi++)
                    board[yi, xi] = board2[yi, xi];
        }
    }
}