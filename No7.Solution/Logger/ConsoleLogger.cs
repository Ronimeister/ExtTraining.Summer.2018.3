using System;
namespace No7.Solution.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"WARN: {message}");
        }
    }
}
//Класс, представляющий консольный логгер