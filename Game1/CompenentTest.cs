//Spencer Corkran
//GSD III
//Tonedeaf Studios

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace DungeonGen
{
    class CompenentTest
    {
        static void Main(string[] args)
        {
            //new cave
            Generator caveOfDoom = new Generator(0, "Cave of Doom");
            Console.WriteLine("Parameters for dungeon creation.  Good values are hard to come by," +
                "but we found that 5 levels, 5 passes, 1 pass, 40%, and 65 x 65 work pretty well.");
            Console.WriteLine("# of levels: (valid: > 0)");
            int l = int.Parse(Console.ReadLine());
            Console.WriteLine("# of passes with the 4-5/2 rule: (valid: > 0)");
            int a = int.Parse(Console.ReadLine());
            Console.WriteLine("# of passes with the 4-5 rule: (valid: > 0)");
            int b = int.Parse(Console.ReadLine());
            Console.WriteLine("% of initial wall coverage: (valid: 0-100)");
            int w = int.Parse(Console.ReadLine());
            Console.WriteLine("X dimensions: (valid: > 0");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Y dimensions: (valid: > 0)");
            int y = int.Parse(Console.ReadLine());
            caveOfDoom.Cave(l, a, b, w, x, y);
            //prints the cave
            foreach (CaveLevel cl in caveOfDoom.arrayOfLevels)
            {
                foreach (Item i in cl.items)
                {
                    Console.WriteLine(i.ToString(false));
                }
                foreach (Monster m in cl.monsters)
                {
                    Console.WriteLine(m.ToString(false));
                }
                cl.printMap();
            }

            //Saves the dungeon to [name].txt
            Console.WriteLine("Save dungeon? Y/N");
            string input;
            while (!(input = Console.ReadLine().ToLower()).Equals("y") && !input.Equals("n"))
            {
                Console.WriteLine("'Y' or 'N'");
            }
            if (input.Equals("y"))
                caveOfDoom.saveInstance();

            //Load a dungeon?
            Console.WriteLine("Load from .txt? Y/N");
            while(!(input = Console.ReadLine().ToLower()).Equals("y") && !input.Equals("n"))
            {
                Console.WriteLine("'Y' or 'N'");
            }
            if (input.Equals("y"))
            {
                Generator test = new Generator();
                Console.WriteLine("Name of file?  Don't include the extension! ('Cave of Doom')");
                test.loadInstance(Console.ReadLine());
                //Prints the items, monsters, and maps associated with
                //each level for comparison to the pre-save print
                foreach (CaveLevel cl in test.arrayOfLevels)
                {
                    foreach (Item i in cl.items)
                    {
                        Console.WriteLine(i.ToString(false));
                    }
                    foreach (Monster m in cl.monsters)
                    {
                        Console.WriteLine(m.ToString(false));
                    }
                    cl.printMap();
                }
            }
            //Create a dungeon with only 1 level, a division mark between
            //9/20 and 11/20, minimum room area of 9, 2 splits, and
            //64x64 dimensions.  Doesn't really work yet.
            /*Instance dungeonOfDeath = new Instance(1, "Dungeon of Death");
            dungeonOfDeath.Dungeon(1, .45, .55, 9, 2, 64, 64);
            foreach (DungeonLevel dl in dungeonOfDeath.arrayOfLevels)
            {
                dl.printMap();
            }*/
            /*Item item = new Item(50, 50, false);
            Thread itemGen = new Thread(new ThreadStart(item.testItems));
            itemGen.Start();
            int count = 0;
            while (count < 1000)
            {
                count++;
                Thread.Sleep(1);
            }
            itemGen.Abort();*/
            Console.WriteLine("Press 'return' to exit");
            Console.ReadLine();//Just to keep it from quitting during debug...
        }
    }
}