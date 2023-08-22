using System;
using System.IO;
using System.Globalization;
using CSV_Summary_Generator.Entities;

namespace CSV_Summary_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter file full path: ");
            string sourcePath = Console.ReadLine();

            try
            {
                string[] lines = File.ReadAllLines(sourcePath);

                string sourceFolderPath = Path.GetDirectoryName(sourcePath);
                string targetFolderPath = sourceFolderPath + @"\out";
                string targetFilePath = targetFolderPath + @"\summary.cvs";

                Directory.CreateDirectory(targetFolderPath);

                using (StreamWriter sw = File.AppendText(targetFilePath))
                {
                    foreach (string line in lines)
                    {
                        string[] fields = line.Split(',');
                        string name = fields[0];
                        double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                        int quantity = int.Parse(fields[2]);

                        Product prod = new Product(name, price, quantity);

                        sw.WriteLine(prod.Name + ", $" + prod.Total().ToString("F2", CultureInfo.InvariantCulture));

                    }
                }
            }
            catch (IOException e)
            {
                Console.Write("Error: " + e.Message);
            }
        }
    }
}