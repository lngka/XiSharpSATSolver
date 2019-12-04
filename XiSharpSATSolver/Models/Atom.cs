using System;
namespace XiSharpSATSolver.Models
{
    public class Atom: Formular
    {
        bool Value { get; set; }
        public string Name { get; set; }

        public Atom(string name)
        {
            Name = name;
            Op = OperatorsEnum.NONE;
            ARight = this;
        }

        
        public override string ToString() => Name;
    }
}
