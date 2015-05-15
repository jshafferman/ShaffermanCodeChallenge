using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SimplePersonMapper : IPersonMapper
    {
        public Person Map(string[] fields)
        {
            int birthYear = int.Parse(fields[Index.BirthYearIndex]);
            int? deathYear = null;

            if(!string.IsNullOrWhiteSpace(fields[Index.DeathYearIndex]))
            {
                deathYear = int.Parse(fields[Index.DeathYearIndex]);
            }

            Person person = new Person()
            {
                BirthYear = birthYear,
                DeathYear = deathYear
            };

            return person;
        }
    }
}
