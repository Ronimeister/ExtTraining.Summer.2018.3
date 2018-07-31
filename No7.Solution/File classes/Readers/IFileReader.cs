using System.Collections.Generic;
using System.IO;

namespace No7.Solution.File_classes.Readers
{
    public interface IFileReader
    {
        List<string> ReadFile(Stream stream);
    }
}

// Интерфейс, которые должны реализовывать все типы, созданные для чтения из файлов

    //P.S. Всё еще думаю над целесообразностью этого интерфейса, мб вечером выпилю и сделаю один класс для чтения,
        //т.к. пока не могу представить случаи, когда нам нужна эта иерархия