using System;
using System.IO;

namespace HW3
{
    class Program
    {
        private static void printUsage()
        {
            Console.WriteLine("Usage is:\n" +
                "\tHW3.exe C inputfile outputfile\n\n" +
                "Where:" +
                "  C is the column number to fit to\n" +
                "  inputfile is the input text file \n" +
                "  outputfile is the new output file base name containing the wrapped text.\n" +
                "e.g. HW3.exe 72 myfile.txt myfile_wrapped.txt");
        } 
        static void Main(string[] args)
        {
            int C = 72;                     // Column length to wrap to
            string inputFilename = "../../WarOfTheWorlds.txt";
            string outputFilename = "../../output.txt";
            StreamReader sr = null;
            if (args.Length != 3)
            {
                printUsage();
                Environment.Exit(1);
            }
            try
            {
                C = int.Parse(args[0]);
                inputFilename = args[1];
                outputFilename = args[2];
                sr = File.OpenText(inputFilename);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Could not find the input file.");
                Environment.Exit(1);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something is wrong with the input.");
                printUsage();
                Environment.Exit(1);
            }

            IQueueInterface<string> words = new LinkedQueue<string>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] tmp = line.Split(' ');
                foreach(var word in tmp)
                {
                    words.push(word);
                }
                
            }
            sr.Close();

            int spacesRemaining = wrapSimply(words, C, outputFilename);
            Console.WriteLine("Total spaces remaining (Greedy): " + spacesRemaining);
            return;
        }

        private static int wrapSimply(IQueueInterface<String> words, int columnLength, string outputFilename)
        {
            StreamWriter sw = null;
            try
            {
			    sw = new StreamWriter(outputFilename);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Cannot create or open " + outputFilename +
                            " for writing.  Using standard output instead.");
			    sw = new StreamWriter(Console.OpenStandardOutput());
            }

            int col = 1;
            int spacesRemaining = 0;            // Running count of spaces left at the end of lines
            while (!words.isEmpty())
            {
                string str = words.peek();
                int len = str.Length;
                // See if we need to wrap to the next line
                if (col == 1)
                {
                    sw.Write(str);
                    col += len;
                    words.pop();
                }
                else if ((col + len) >= columnLength)
                {
				    // go to the next line
				    sw.WriteLine();
                    spacesRemaining += (columnLength - col) + 1;
                    col = 1;
                }
                else
                {
				    sw.Write(" ");
				    sw.Write(str);
                    col += (len + 1);
                    words.pop();
                }

            }
		    sw.WriteLine();
		    sw.Flush();
		    sw.Close();
            return spacesRemaining;
        }
    }
}
