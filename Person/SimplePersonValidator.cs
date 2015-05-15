using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SimplePersonValidator : IPersonValidator
    {
        public SimplePersonValidator(ILogger logger)
        {
            this.logger = logger;
        }

        public bool IsValid(string[] personData)
        {
            if (personData.Length != PersonLength)
            {
                logger.LogWarning("Line malformed. Only {0} field(s) found.", personData.Length);
                return false;
            }

            int birthYear;
            if (!int.TryParse(personData[Index.BirthYearIndex], out birthYear))
            {
                logger.LogWarning("Birth year not a valid integer: '{0}'", personData[Index.BirthYearIndex]);
                return false;
            }

            if(birthYear < 1900 || birthYear > 2000)
            {
                logger.LogWarning("Birth year not within valid range: '{0}'", personData[Index.BirthYearIndex]);
                return false;
            }

            int deathYear;
            if (!string.IsNullOrWhiteSpace(personData[Index.DeathYearIndex]))
            {
                if(!int.TryParse(personData[Index.DeathYearIndex], out deathYear))
                {
                    logger.LogWarning("Death year not a valid integer: '{0}'", personData[Index.DeathYearIndex]);
                    return false;
                }
                else
                {
                    if(deathYear < 1900 || deathYear > 2000)
                    {
                        logger.LogWarning("Death year not within valid range: '{0}'", personData[Index.DeathYearIndex]);
                        return false;
                    }

                    if(deathYear < birthYear)
                    {
                        logger.LogWarning("Death year cannot be below birth year: '{0}'", personData[Index.DeathYearIndex]);
                        return false;
                    }
                }
            }

            return true;
        }

        private readonly ILogger logger;
        private const int PersonLength = 2;
    }
}
