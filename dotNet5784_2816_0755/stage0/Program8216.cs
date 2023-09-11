// See https://aka.ms/new-console-template for more information

 using System;
namespace stage0
{
    partial class Program
    {
        static void main(string[] args)
        {
            welcome8216();
            welcom0755();
            Console.ReadKey();
            
        }
        static void welcome8216()
        {
            Console.WriteLine("enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("{0},welcome to my first console application",name);
        }
        static partial void welcom0755();
    }
}