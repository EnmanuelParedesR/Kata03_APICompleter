using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata_03_API
{
    public class Result
    {
        public List<Parameter> parameters = new List<Parameter>();
        public bool isComplete = false;
        public string completedURL = null;
    }
}
