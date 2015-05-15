using Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PeopleProcessor
    {
        private List<Person> people;

        private const int BirthYears = 100;

        private Dictionary<int, int> alive;

        private readonly IPersonDataProvider personDataProvider;
        private readonly IPersonParser personParser;
        
        public PeopleProcessor(IPersonDataProvider personDataProvider, IPersonParser personParser)
        {
            this.personDataProvider = personDataProvider;
            this.personParser = personParser;

            InitializeAliveDictionary();
        }

        public IEnumerable<int> GetYearsWithMostAlive()
        {
            var lines = personDataProvider.GetBirthData();
            people = personParser.Parse(lines).ToList();

            CalculateAliveValues();

            // Order the hash by value, which is number of people with birth year that are alive
            var orderByNumberAlive = from count in alive.Values
                              orderby count descending
                              select count;

            // the maximum number of people that are still alive, which is first element
            int maxAlive = orderByNumberAlive.ElementAt(0);

            // in case there might be ties find all that share the maximum value
            var listOfBirthYears = from years in alive
                                   where years.Value == maxAlive
                                   select years.Key;

            return listOfBirthYears;
        }

        private void CalculateAliveValues()
        {
            foreach (Person p in people)
            {
                // No death year means you are still alive!!
                if (!p.DeathYear.HasValue)
                {
                    ++alive[p.BirthYear];
                }
            }
        }

        private void InitializeAliveDictionary()
        {
            alive = new Dictionary<int, int>(BirthYears);

            for(int key = Person.LowestBirthYear; key <= Person.HighestBirthYear; ++key)
            {
                alive.Add(key, 0);
            }
        }
    }
}
