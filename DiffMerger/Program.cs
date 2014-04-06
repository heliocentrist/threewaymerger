using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merger;

namespace DiffMerger
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Please supply parameters, in this order:\ndiffmerger.exe <original file> <first version> <second version>");
                return;
            }

            try
            {
                string[] original = File.ReadAllLines(args[0]);
                string[] left = File.ReadAllLines(args[1]);
                string[] right = File.ReadAllLines(args[2]);

                List<string> result = ThreeWayMerger.Merge(original, left, right);

                string result_filename = "merged_" + args[0];

                File.WriteAllLines(result_filename, result);

                Console.WriteLine(String.Format("Merge result written to file {0}", result_filename));
            }
            catch(Exception exc)
            {
                Console.WriteLine(String.Format("An error happened: {0}", exc.Message));
            }
        }
    }
}
