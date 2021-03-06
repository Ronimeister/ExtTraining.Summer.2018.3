﻿using No7.Solution.Logger;
using System.Reflection;

namespace No7.Solution.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("No7.Solution.Console.trades.txt");

            DbHandler tradeProcessor = new TradesHandler();

            tradeProcessor.Handle(tradeStream, "TradeData", new ConsoleLogger());
        }
    }
}