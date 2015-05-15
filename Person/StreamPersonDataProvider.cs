using Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class StreamPersonDataProvider : IPersonDataProvider
    {
        public StreamPersonDataProvider(string filename)
        {
            this.filename = filename;
        }

        public IEnumerable<string> GetBirthData()
        {
            var birthData = new List<string>();

            using (StreamReader sr = File.OpenText(filename))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    birthData.Add(line);
                }
            }

            return birthData;
        }

        private readonly string filename;
    }
}
