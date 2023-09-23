using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ConsoleApp1
{

    internal class TheMatrix
    {

        object block = new object();

        int[] symbols = {
                0x3044, 0x30DE, 0x319B, 0x319D, 0x319F, 0x3163, 0x30E1, 0x30ED, 0x30E9, 0x30CF, 0x30CA,
                0x304F, 0x3057, 0x3058, 0x306E, 0x30A0, 0x30AA, 0x30EA, 0x308D, 0x30C5, 0x30B3, 0x30CB,
                0x30CD, 0x30DB, 0x30D5, 0x30DF, 0x30D2, 0x30FC, 0x30C1, 0x30AF, 0x30E2, 0x30B5, 0x30CD,
                0x308A, 0x30BD, 0x30B3, 0x30C6, 0x30AD, 0x30AF, 0x314A, 0x002B, 0x0030, 0x0032, 0x0034,
                0x0035, 0x0036, 0x0038, 0x0039, 0xFE30, 0x318D, 0x005A, 0x007E
            };


        public TheMatrix()
        {

        }

        void Print(int posW, int posH)
        {
            int rand = new Random().Next(0, symbols.Length);
            Console.SetCursorPosition(posW, posH);
            Console.Write(char.ConvertFromUtf32(symbols[rand]));
            Console.SetCursorPosition(0, 0);
        }

        public void Matrix()
        {
            //Thread.Sleep(new Random().Next(50, 100));
            ConsoleColor[] color = new ConsoleColor[]
            {
                    ConsoleColor.DarkGreen,
                    ConsoleColor.Green,
                    ConsoleColor.White,
            };


            int lengthOfLine = new Random().Next(20, 35);
            int pos = new Random().Next(0, Console.WindowWidth);
            int lineTail = 0;

            int count = 0;


            for (int i = 0; i <= Console.WindowHeight; i++)
            {
                lock (block)
                {
                    count++;

                    if (i <= lengthOfLine)
                    {
                        Console.ForegroundColor = color[2];
                        Print(pos, i);

                        if (i > 0)
                        {
                            Console.ForegroundColor = color[1];
                            Print(pos, i - 1);
                        }

                        if (i > 2)
                        {
                            Console.ForegroundColor = color[0];
                            Print(pos, i - 2);

                            int z = new Random().Next(1, i - 2);
                            for (int j = 0; j < z; j++)
                            {
                                int poses = new Random().Next(2, i - 1);
                                Print(pos, i - poses);
                            }
                        }


                    }

                    if (i >= lengthOfLine)
                    {
                        lineTail = i - lengthOfLine - 1;
                    }

                    if (lengthOfLine < i && i < Console.WindowHeight - lengthOfLine)
                    {
                        Console.ForegroundColor = color[2];
                        Print(pos, i);

                        Console.SetCursorPosition(pos, lineTail);
                        Console.Write("  ");
                        Console.SetCursorPosition(0, 0);

                        Console.ForegroundColor = color[1];
                        Print(pos, i - 1);

                        Console.ForegroundColor = color[0];
                        Print(pos, i - 2);

                        int z = new Random().Next(1, 3);
                        Console.ForegroundColor = color[0];
                        for (int j = 0; j < z; j++)
                        {
                            int poses = new Random().Next(3, lengthOfLine);

                            Print(pos, i - poses);
                        }
                    }

                    if (i >= Console.WindowHeight - lengthOfLine - 1)
                    {

                        lineTail = i - lengthOfLine;

                        if (i == Console.WindowHeight && lengthOfLine > 0)
                        {
                            i--;
                            lengthOfLine--;
                            Console.ForegroundColor = color[0];
                            Print(pos, i);
                            if (lengthOfLine > 4)
                            {
                                int z = new Random().Next(1, 3);
                                Console.ForegroundColor = color[0];
                                for (int j = 0; j < z; j++)
                                {

                                    int poses = new Random().Next(3, lengthOfLine);

                                    Print(pos, i - poses);
                                }
                            }
                        }

                        if (i < Console.WindowHeight - 1)
                        {
                            Console.ForegroundColor = color[2];
                            Print(pos, i);
                        }

                        if (lengthOfLine > 2)
                        {
                            Console.ForegroundColor = color[1];
                            Print(pos, i - 1);

                            Console.ForegroundColor = color[0];
                            Print(pos, i - 2);
                        }

                        if (lineTail < 0)
                        {
                            Console.SetCursorPosition(pos, 0);
                            Console.Write("  ");
                            Console.SetCursorPosition(0, 0);
                        }
                        else
                        {
                            Console.SetCursorPosition(pos, lineTail);
                            Console.Write("  ");
                            Console.SetCursorPosition(0, 0);
                        }
                    }

                    int sleep = new Random().Next(5, 10);
                    //Thread.Sleep(sleep);
                }

            }


        }

    }

    internal class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            TheMatrix theMatrix = new TheMatrix();

            Console.ReadKey();

            int count = 0;

            Dictionary<Thread, int> keyValuePairs = new Dictionary<Thread, int>();

            while (true)
            {
                if (count % Console.WindowWidth == 0)
                {

                    for (int i = 0; i < Console.WindowWidth; i++)
                    {
                        count++;
                        new Thread(theMatrix.Matrix).Start();
                        //xxx.Start();
                        //int rand = new Random().Next(0, 60);
                        //keyValuePairs.Add(xxx, 60);
                        Thread.Sleep(25);
                        //xxx.Abort();

                    }
                }
                // Thread.Sleep(8000);
            }


        }
    }
}
