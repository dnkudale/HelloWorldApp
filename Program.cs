using System;
 
namespace HelloWorldApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(GetMessage());
        }
 
        public static string GetMessage()
        {
            return "Hello, World from .NET!";
        }
    }
}
