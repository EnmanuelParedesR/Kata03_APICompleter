using System;
using System.Collections.Generic;
using System.Text;

namespace Kata_03_API
{
    public class Parameter
    {
        public string name;
        public IType type;

        public Parameter() { }

        public Parameter(string Name, IType Type)
        {
            name = Name;
            type = Type;
        }

    }
}
