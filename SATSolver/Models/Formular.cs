using System;
using System.Collections.Generic;

namespace XiSharpSATSolver.Models
{
    public class Formular
    {
        protected Atom ALeft;
        protected Atom ARight;

        private Formular FLeft;
        private Formular FRight;

        protected OperatorsEnum Op;

        public Formular() { }

        public Formular(Atom left, OperatorsEnum op, Atom right)
        {
            ALeft = left;
            ARight = right;
            Op = op;
        }

        public Formular(Atom right, OperatorsEnum op = OperatorsEnum.NEGATE)
        {
            ALeft = null;
            ARight = right;
            Op = op;
        }

        public Formular (Formular left, OperatorsEnum op, Formular right)
        {
            FLeft = left;
            FRight = right;
            Op = op;
        }

        public Formular(Formular right, OperatorsEnum op = OperatorsEnum.NEGATE)
        {
            FLeft = null;
            FRight = right;
            Op = op;
        }

        public override string ToString()
        {
            if (ARight == null && FRight == null)
            {
                throw new Exception("Formular.ToString: Right Hand Side not exist");
            }

            if (Op == OperatorsEnum.NEGATE)
            {
                string rightHandSide = FRight != null ? FRight.ToString() : ARight.ToString();
                return OperatorToString(Op) + rightHandSide;
            }

            string retVal = FLeft + " " + OperatorToString(Op) + " " + FRight;
            return "(" + retVal + ")";
        }

        public static string OperatorToString(OperatorsEnum op)
        {
            switch(op)
            {
                case OperatorsEnum.AND:
                    return "∧";
                case OperatorsEnum.OR:
                    return "∨";
                case OperatorsEnum.NEGATE:
                    return "¬";
                case OperatorsEnum.IMPLY:
                    return "→";
                default:
                    throw new Exception("OperatorToString: Unknow operator");
            }
        }

        public static Formular operator ~(Formular right)
        {
            return new Formular(right, OperatorsEnum.NEGATE);
        }

        public static Formular operator |(Formular left, Formular right)
        {
            return new Formular(left, OperatorsEnum.OR, right);
        }

        public static Formular operator &(Formular left, Formular right)
        {
            return new Formular(left, OperatorsEnum.AND, right);
        }

        public static Formular operator >(Formular left, Formular right)
        {
            return new Formular(left, OperatorsEnum.IMPLY, right);
        }

        public static Formular operator <(Formular left, Formular right)
        {
            return new Formular(right, OperatorsEnum.IMPLY, left);
        }

        public bool Evaluate(Dictionary<string, bool> feedDict)
        {
            switch(Op) {
                case OperatorsEnum.NONE:
                    if (ARight == null)
                    {
                        throw new Exception("Evaluate: missing right Atom");
                    }
                    if (!feedDict.ContainsKey(ARight.Name))
                    {
                        throw new Exception($"Evaluate: missing {ARight.Name}in feedDict");
                    }
                    return feedDict[ARight.Name];
                case OperatorsEnum.NEGATE:
                    if (FRight == null && ARight == null)
                    {
                        throw new Exception("Evaluate: missing right hand side");
                    }
                    if (FRight != null)
                    {
                        return !FRight.Evaluate(feedDict);
                    } else
                    {
                        return !feedDict[ARight.Name];
                    }
                default:
                    throw new Exception("Evaluate: Unknown formular operator");
            }
        }

    }
}
