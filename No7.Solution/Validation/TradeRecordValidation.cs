namespace No7.Solution
{
    internal class TradeRecordValidation : IDbRecordValidation
    {
        public bool IsValid(string[] fields, int lineCount)
        {
            if (fields.Length != 3)
            {
                System.Console.WriteLine("WARN: Line {0} malformed. Only {1} field(s) found.", lineCount, fields.Length);
                return false;
            }

            if (fields[0].Length != 6)
            {
                System.Console.WriteLine("WARN: Trade currencies on line {0} malformed: '{1}'", lineCount, fields[0]);
                return false;
            }

            return true;
        }
    }
}

//Тип-валидатор, созданный для проверки некоторых условий записи типа TradeRecord