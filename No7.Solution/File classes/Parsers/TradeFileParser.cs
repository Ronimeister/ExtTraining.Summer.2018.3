using No7.Solution.File_classes.Readers;
using No7.Solution.Logger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace No7.Solution
{
    public class TradeFileParser : IFileParser
    {
        #region Private fields
        private const float LOT_SIZE = 100000f;
        private CultureInfo DEFAULT_CULTURE = new CultureInfo("en-US");
        #endregion

        #region Public API
        public List<TradeRecord> Parse(Stream stream, IFileReader reader, IDbRecordValidation validation)
        {
            if (stream == null)
            {
                throw new ArgumentNullException($"{nameof(stream)} can't be equal to null!");
            }

            if (reader == null)
            {
                throw new ArgumentNullException($"{nameof(reader)} can't be equal to null!");
            }

            return ParseInner(stream, reader, validation, DEFAULT_CULTURE, new ConsoleLogger());
        }

        public List<TradeRecord> Parse(Stream stream, IFileReader reader, IDbRecordValidation validation, CultureInfo culture)
        {
            if (stream == null)
            {
                throw new ArgumentNullException($"{nameof(stream)} can't be equal to null!");
            }

            if (reader == null)
            {
                throw new ArgumentNullException($"{nameof(reader)} can't be equal to null!");
            }

            if (culture == null)
            {
                throw new ArgumentNullException($"{nameof(culture)} can't be equal to null!");
            }

            return ParseInner(stream, reader, validation, culture, new ConsoleLogger());
        }

        public List<TradeRecord> Parse(Stream stream, IFileReader reader, IDbRecordValidation validation, CultureInfo culture, ILogger logger)
        {
            if (stream == null)
            {
                throw new ArgumentNullException($"{nameof(stream)} can't be equal to null!");
            }

            if (reader == null)
            {
                throw new ArgumentNullException($"{nameof(reader)} can't be equal to null!");
            }

            if (culture == null)
            {
                throw new ArgumentNullException($"{nameof(culture)} can't be equal to null!");
            }

            return ParseInner(stream, reader, validation, culture, logger);
        }
        #endregion

        #region Private methods
        private List<TradeRecord> ParseInner(Stream stream, IFileReader reader, IDbRecordValidation validation, CultureInfo culture, ILogger logger)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            List<string> lines = reader.ReadFile(stream);

            return GetRecords(lines, validation, logger);
        }

        private List<TradeRecord> GetRecords(List<string> lines, IDbRecordValidation validation, ILogger logger)
        {
            List <TradeRecord> trades = new List<TradeRecord>();
            int lineCount = 1;

            foreach (string line in lines)
            {
                string[] fields = line.Split(new char[] { ',' });

                if (validation.IsValid(fields, lineCount))
                {
                    if (!int.TryParse(fields[1], out var tradeAmount))
                    {
                        string message = string.Format("Trade amount on line {0} not a valid integer: '{1}'", lineCount, fields[1]);
                        logger.Log(message);
                    }

                    if (!decimal.TryParse(fields[2], out var tradePrice))
                    {
                        string message = string.Format("Trade price on line {0} not a valid decimal: '{1}'", lineCount, fields[2]);
                        logger.Log(message);
                    }

                    var sourceCurrencyCode = fields[0].Substring(0, 3);
                    var destinationCurrencyCode = fields[0].Substring(3, 3);

                    trades.Add(GenerateNewTradeRecord(sourceCurrencyCode, destinationCurrencyCode, tradeAmount, tradePrice));
                }               

                lineCount++;
            }

            return trades;
        }

        private TradeRecord GenerateNewTradeRecord(string sourceCurrencyCode, string destinationCurrencyCode, float tradeAmount, decimal tradePrice)
        {
            TradeRecord result = new TradeRecord
            {
                SourceCurrency = sourceCurrencyCode,
                DestinationCurrency = destinationCurrencyCode,
                Lots = tradeAmount / LOT_SIZE,
                Price = tradePrice
            };

            return result;
        }
        #endregion
    }
}

//Тип, реализующий интерфейс IFileParser. Позволяет получать готовые записи типа TradeRecord из распаршенного файла.
    //Так же есть привязка к культуре, т.к. значения мб некорректными. По умолчанию стоит культура en-US.
        //Валидация для парсинга файла зависит от валидатора типа IDBRecordValidation