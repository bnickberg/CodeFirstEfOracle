﻿using Microsoft.Extensions.Configuration;

namespace CodeFirstEfOracle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Console.WriteLine("Hello, World!");
        }
    }
}
