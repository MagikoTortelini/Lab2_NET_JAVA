using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab2
{
    internal class Program
    {
        
        static void Main(string[] args)
        {

            Console.WriteLine("Podaj walutę");
            var waluty=Console.ReadLine();
            var waluty2 = Console.ReadLine();
            APITest_with_db t = new APITest_with_db();
            APITest t2 = new APITest();
            Console.WriteLine($"Przelicznik dla: {waluty}->{waluty2}");
            t2.GetData(waluty, waluty2).Wait();
        }

        
    }
}
