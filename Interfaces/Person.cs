using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class Person
    {
        public int BirthYear;
        public int? DeathYear;

        public const int LowestBirthYear = 1900;
        public const int HighestBirthYear = 2000;
    }
}
