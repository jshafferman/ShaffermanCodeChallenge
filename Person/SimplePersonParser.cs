using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SimplePersonParser : IPersonParser
    {
        public SimplePersonParser(IPersonValidator validator, IPersonMapper mapper)
        {
            this.validator = validator;
            this.personMapper = mapper;
        }
       
        public IEnumerable<Person> Parse(IEnumerable<string> birthData)
        {
            var people = new List<Person>();

            foreach(string line in birthData)
            {
                var fields = line.Split(new char[] { ',' });

                if(!validator.IsValid(fields))
                {
                    continue;
                }

                var person = personMapper.Map(fields);

                people.Add(person);
            }

            return people;
        }

        private readonly IPersonValidator validator;
        private readonly IPersonMapper personMapper;
    }
}
