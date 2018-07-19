using System;
using System.Collections.Generic;
using System.Text;

namespace Kata_03_API
{
    public class Parameter : IComparable<Parameter>
    {
        public string name;
        public IType type;

        public Parameter() { }

        public Parameter(string Name, IType Type)
        {
            name = Name;
            type = Type;
        }

        public int CompareTo(Parameter other)
        {
            int result;
            if ((this.type.GetType() == new Path().GetType()) && (other.type.GetType() == new QueryString().GetType()))
                result = -1;

            else
                if ((this.type.GetType() == new QueryString().GetType()) && (other.type.GetType() == new Path().GetType()))
                    result = 1;

                else
                    result = this.name.CompareTo(other.name);


            return result;
        }

    }
}
