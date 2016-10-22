using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisRecon.AmmoGenerator
{
    class Program
    {
        
        private static List<string> _firstNames;
        private static List<string> _lastNames;
        
        private static Random _random;

        static void Main(string[] args)
        {
            var fileName = string.Format("sample {0}", DateTime.Now.ToString("yyyyMMdd hhmmss"));
            var sampleRootPath = "..\\..\\..\\..\\SampleData";
            var filePath = string.Format("{0}\\{1}.csv", sampleRootPath, fileName);

            _firstNames = File.ReadAllLines(string.Format("{0}\\CSV_Database_of_First_Names.csv", sampleRootPath)).Skip(1).ToList();
            _lastNames = File.ReadAllLines(string.Format("{0}\\CSV_Database_of_Last_Names.csv", sampleRootPath)).Skip(1).ToList();
            
            _random = new Random();

            var header = "id,firstname,lastname,age\n";

            File.WriteAllText(filePath, header);
            using (var outfile = new StreamWriter(filePath))
            {
                for (int i = 0; i < 5000000; i++)
                {
                    outfile.Write($"{i},{GetRandomFirstname()},{GetRandomLastname()},{GetRandomAge()}\n");
                }
            }
        }

        private static int GetRandomAge()
        {
            return _random.Next(0, 100);
        }

        private static string GetRandomLastname()
        {
            return _lastNames[_random.Next(0, _lastNames.Count - 1)];
        }

        private static string GetRandomFirstname()
        {
            return _firstNames[_random.Next(0, _firstNames.Count - 1)];
        }
    }
}
