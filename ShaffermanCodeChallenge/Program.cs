using Contracts;
using Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaffermanCodeChallenge
{
    class Program
    {
        private const int NumberOfPeople = 1000;
        private const int Seed = 1;
        private const string Filename = "people.txt";

        static void Main(string[] args)
        {
            // Only performed this once so that I could get a good data set
            // Generate a list of people
            //List<Person> people = GeneratePersonDataSet();

            // Write that list to a file
            //WriteListOfPeopleToFile(people);

            var logger = new ConsoleLogger();
            var personValidator = new SimplePersonValidator(logger);
            var personDataProvider = new StreamPersonDataProvider(Filename);
            var personMapper = new SimplePersonMapper();
            var personParser = new SimplePersonParser(personValidator, personMapper);

            PeopleProcessor people = new PeopleProcessor(personDataProvider, personParser);

            IEnumerable<int> yearsWithMostAlive = people.GetYearsWithMostAlive();

            foreach(int year in yearsWithMostAlive)
            {
                Console.WriteLine("Year {0}", year);
            }

            // Just so it doesn't disappear when testing it out
            Console.ReadKey();
        }

        private static void WriteListOfPeopleToFile(List<Person> people)
        {
            using (StreamWriter sw = File.CreateText(Filename))
            {
                foreach (Person p in people)
                {
                    sw.WriteLine("{0}, {1}", p.BirthYear);
                }
            }
        }

        private static List<Person> GeneratePersonDataSet()
        {
            List<Person> tempList = new List<Person>(NumberOfPeople);
            Random rnd = new Random(Seed);

            for (int i = 0; i < NumberOfPeople; ++i)
            {
                int birthYear = rnd.Next(1900, 2000);
                int? deathYear;

                if(i % 10 != 0)
                {
                    deathYear = null;
                }
                else
                {
                    deathYear = rnd.Next(birthYear, 2000);
                }

                tempList.Add(new Person() { BirthYear = birthYear, DeathYear = deathYear });
                
            }

            return tempList;
        }
    }
}
