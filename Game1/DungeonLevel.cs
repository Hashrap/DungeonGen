//Spencer Corkran
//GSD III
//Tonedeaf Studios

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*This class isn't commented very well ;_; */
namespace Game1
{
    class DungeonLevel : InstanceLevel
    {
        private const int MINIMUM_ROOM_AREA = 9;
        private int rand;
        private int count;
        Tile[,] altBoard;
        private MersenneTwister rng = new MersenneTwister();
        public DungeonLevel(int size_y, int size_x)
            : base(size_y, size_x, true)
        {
            board = new Tile[base.Size_Y, base.Size_X];
            altBoard = new Tile[base.Size_X, base.Size_Y];
            for (int i = 0; i < base.Size_Y; i++)
            //goes through each row
            {
                for (int j = 0; j < base.Size_X; j++)
                //goes through each column in a row
                {
                    board[i, j] |= Tile.W;
                }
            }
        }
        //This is the dumbest piece of code I have ever written and doesn't work
        public Tile[,] dungeonGen(Tile[,] board, int iterations, double min_position, double max_position)
        {
            /*Essentially, what I'm trying to make happen here is this:
             *This method splits the dungeon into two parts.  Then it recurses
             *and splits it the other way, and keeps doing that until the 
             *count limit is reached.  Then it should carve out a random rectangle
             *in each subdungeon, and send it back up through through the
             *recursion stack.  Each time, the method puts the two subdungeons
             *immediately under it together using Array.Copy, then passes it back
             *up to the method that called it, which puts the two pieces it recieved
             *together and so so on until we once again have a fullsized board,
             *but filled with rectangles.  It does not yet accomplish this, though it compiles.*/
            Tile[,] subBoard1;
            Tile[,] subBoard2;
            Tile[,] board01;
            Tile[,] board02;
            double minSplit;
            double maxSplit;

            if (count > iterations)
            {
                Console.WriteLine("try again :(");
                return board;
            }
            if (count <= iterations)
            {
                count++;
                rand = rng.Next(0, 99);
                //vertical split
                if (count % 2 == 1)
                {
                    minSplit = min_position * board.GetLength(0);
                    maxSplit = max_position * board.GetLength(0);
                    rand = rng.Next((int)minSplit, (int)maxSplit);
                    subBoard1 = new Tile[board.GetLength(1), rand];
                    subBoard2 = new Tile[board.GetLength(1),(board.GetLength(0) - rand)];
                    for (int i = 0; i < rand; i++)
                    {
                        for (int j = 0; j < board.GetLength(1); j++)
                        {
                            subBoard1[j, i] = board[i, j];
                        }
                    }
                    for (int i = rand; i < (board.GetLength(0)); i++)
                    {
                        for (int j = 0; j < board.GetLength(1); j++)
                        {
                            subBoard2[j, i-rand] = board[i, j];
                        }
                    }
                    if (count == iterations)
                    {
                        subBoard1 = carveRoom(subBoard1);
                        subBoard2 = carveRoom(subBoard2);
                        count--;
                        Array.Copy(subBoard1, 0, board, 0, subBoard1.Length);
                        Array.Copy(subBoard2, 0, board, subBoard1.Length, subBoard2.Length);
                        return board;
                    }
                    else if (count < iterations)
                    {
                        board01 = dungeonGen(subBoard1, iterations, min_position, max_position);
                        board02 = dungeonGen(subBoard2, iterations, min_position, max_position);
                        count--;
                        Array.Copy(board01, 0, board, 0, board01.Length);
                        Array.Copy(board02, 0, board, board01.Length, board02.Length);
                        return board;
                    }
                }
                //horizontal split
                else if (count % 2 == 0)
                {
                    minSplit = min_position * board.GetLength(1);
                    maxSplit = max_position * board.GetLength(1);
                    rand = rng.Next((int)minSplit, (int)maxSplit);
                    subBoard1 = new Tile[board.GetLength(0), rand];
                    subBoard2 = new Tile[board.GetLength(0), (board.GetLength(1) - rand)];
                    for (int i = 0; i < board.GetLength(0); i++)
                    {
                        for (int j = 0; j < rand; j++)
                        {
                            subBoard1[i, j] = board[i, j];
                        }
                    }
                    for (int i = 0; i < board.GetLength(0); i++)
                    {
                        for (int j = rand; j < (board.GetLength(1)); j++)
                        {
                            subBoard2[i, j-rand] = board[i, j];
                        }
                    }
                    if (count == iterations)
                    {
                        subBoard1 = carveRoom(subBoard1);
                        subBoard2 = carveRoom(subBoard2);
                        count--;
                        Array.Copy(subBoard1, 0, board, 0, subBoard1.Length);
                        Array.Copy(subBoard2, 0, board, subBoard1.Length, subBoard2.Length);
                        return board;
                    }
                    else if (count < iterations)
                    {
                        board01 = dungeonGen(subBoard1, iterations, min_position, max_position);
                        board02 = dungeonGen(subBoard2, iterations, min_position, max_position);
                        count--;
                        Array.Copy(board01, 0, board, 0, board01.Length);
                        Array.Copy(board02, 0, board, board01.Length, board02.Length);
                        return board;
                    }
                }
                else
                {
                    subBoard1 = new Tile[1, 1];
                    subBoard2 = new Tile[1, 1];
                    Console.WriteLine("WHAT ARE YOU DOING LMFAO");
                    return board;
                    //garflbak4rfg this sucks
                }
            }
            return board;
        }
        public Tile[,] carveRoom(Tile[,] array)
        {
            bool good = false;
            int x;
            int x_length;
            int y;
            int y_length;
            while (good == false)
            {
                Console.WriteLine("CARVIN'");
                for (int i = 0; i < array.GetLength(0); i++)
                //goes through each row
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    //goes through each column in a row
                    {
                        board[i, j] |= Tile.W;
                    }
                }
                y_length = rng.Next(array.GetLength(0));
                x_length = rng.Next(array.GetLength(1));
                y = rng.Next(array.GetLength(0) - y_length);
                x = rng.Next(array.GetLength(1) - x_length);
                if (x_length * y_length > 8)
                {
                    for (int i = y; i < y_length; i++)
                    //goes through each row
                    {
                        for (int j = x; j < x_length; j++)
                        //goes through each column in a row
                        {
                            array[i, j] &= ~Tile.W;
                            array[i, j] |= Tile.f;
                            array[i, j] |= Tile.g;
                            good = true;
                        }
                    }
                }
            }
            return array;
        }
    }
}