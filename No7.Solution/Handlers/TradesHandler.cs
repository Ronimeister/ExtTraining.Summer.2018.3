using No7.Solution.File_classes.Readers;
using System.Collections.Generic;
using System.IO;

namespace No7.Solution
{
    public class TradesHandler : DbHandler
    {
        protected override void HandleInner(Stream stream, string databaseName)
        {
            TradeFileParser parser = new TradeFileParser();

            List<TradeRecord> trades = parser.Parse(stream, new TradeFileReader(), new TradeRecordValidation());
            DbConnection<TradeRecord>.AddToDatabase(databaseName, trades, new TradeRecordInserter());
        }        
    }
}

// Класс-шаблон для хэндлера типа TradeRecord.
    //Функциональность метода HandleInner - получения листа записей при помощи парсинга нужного файла и добавление их в бд