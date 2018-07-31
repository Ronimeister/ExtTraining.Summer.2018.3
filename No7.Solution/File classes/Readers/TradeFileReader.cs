using System.Collections.Generic;
using System.IO;

namespace No7.Solution.File_classes.Readers
{
    public class TradeFileReader : IFileReader
    {
        public List<string> ReadFile(Stream stream)
        {
            List<string> result = new List<string>();
            using (StreamReader reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }
    }
}

//Класс, позволяющий нам считывать из файла записи
    //P.S. Смотрите описание IFileReader