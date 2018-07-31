using No7.Solution.Logger;

namespace No7.Solution
{
    internal class TradeRecordValidation : IDbRecordValidation
    {
        private readonly ILogger DEFAULT_LOGGER = new ConsoleLogger();

        public bool IsValid(string[] fields, int lineCount)
        {
            ILogger logger = DEFAULT_LOGGER;

            if (fields.Length != 3)
            {
                string message = string.Format("Line {0} malformed. Only {1} field(s) found.", lineCount, fields.Length);
                logger.Log(message);

                return false;
            }

            if (fields[0].Length != 6)
            {
                string message = string.Format("Trade currencies on line {0} malformed: '{1}'", lineCount, fields[0]);
                logger.Log(message);

                return false;
            }

            return true;
        }

        public bool IsValid(string[] fields, int lineCount, ILogger logger)
        {
            if (fields.Length != 3)
            {
                string message = string.Format("Line {0} malformed. Only {1} field(s) found.", lineCount, fields.Length);
                logger.Log(message);

                return false;
            }

            if (fields[0].Length != 6)
            {
                string message = string.Format("Trade currencies on line {0} malformed: '{1}'", lineCount, fields[0]);
                logger.Log(message);

                return false;
            }

            return true;
        }
    }
}

//Тип-валидатор, созданный для проверки некоторых условий записи типа TradeRecord