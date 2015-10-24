using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            IGradesTracker book = CreateGradeBook();
            //GetBookName(book);
            AddGrades(book);
            SaveGrades(book);
            WriteResults(book);
        }

        private static IGradesTracker CreateGradeBook()
        {
            return new DiscardLowestGrade();
        }

        private static void WriteResults(IGradesTracker book)
        {
            GradeStatistics stats = book.ComputeStatistics();

            foreach (float grade in book)
            {
                Console.WriteLine(grade);
            }

            WriteResult("Average", stats.AverageGrade);
            WriteResult("Highest", stats.HighestGrade);
            WriteResult("Lowest", stats.LowestGrade);
            WriteResult(stats.Description, stats.LetterGrade);
        }

        private static void SaveGrades(IGradesTracker book)
        {
            string path = @"D:\output_file\grades.txt"; //set path
            using (StreamWriter outputFile = File.CreateText(path))
            {
                book.WriteGrades(outputFile);
            }
        }

        private static void AddGrades(IGradesTracker book)
        {
            book.AddGrade(91);
            book.AddGrade(89.5f);
            book.AddGrade(75);
        }

        private static void GetBookName(IGradesTracker book)
        {
            try
            {
                Console.WriteLine("Enter the name");
                book.Name = Console.ReadLine();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //catch (NullReferenceException ex)
            //{
            //    Console.WriteLine("Oops, something went wrong...");
            //}
        }

        static void WriteResult(string description, string result)
        {
            Console.WriteLine($"{description}: {result}", description, result);
        }

        static void WriteResult(string description, float result)
        {
            Console.WriteLine($"{description}: {result:F2}", description, result);
        }

    }
}
