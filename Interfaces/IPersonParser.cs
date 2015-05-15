using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPersonParser
    {
        IEnumerable<Person> Parse(IEnumerable<string> birthData);
    }
}
