using System;
using System.Collections.Generic;
using XiSharpSATSolver.Models;

namespace XiSharpSATSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Atom C = new Atom("C");
            Atom B = new Atom("B");
            Atom A = new Atom("A");

            var feedDict = new Dictionary<string, bool>()
            {
                {"A", true },
                {"B", false },
                {"C", true }
            };

            Formular formular = A;
            Console.WriteLine(formular);
            Console.WriteLine("= " + formular.Evaluate(feedDict).ToString());
        }
    }
}
