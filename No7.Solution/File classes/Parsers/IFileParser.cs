using No7.Solution.File_classes.Readers;
using No7.Solution.Logger;
using System.Collections.Generic;
using System.IO;

namespace No7.Solution
{
    public interface IFileParser
    {
        List<TradeRecord> Parse(Stream stream, IFileReader reader, IDbRecordValidation validation, ILogger logger);
    }
}

// Интерфейс, описывающий все сущности, которые могут парсить файл с какой-либо информацией для БД