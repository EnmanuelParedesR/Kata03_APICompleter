using System;

namespace Kata_03_API
{
    public class APICompleter
    {

        public int FindFirstOpeningBracketPosition(String s)
        {
            int position = 0;
            position =  s.IndexOf('{');
            
            return position;
        }


        public int FindFirstClosingBracketPosition(String s)
        {
            int position = 0;
            position = s.IndexOf('}');
            return position;
        }

        public string GetStringBetweenBrackets(string s, int startpos, int finishpos)
        {
            int stringLength = finishpos - (startpos + 1);
            string result = s.Substring(startpos + 1, stringLength);
            return result;
        }

        public Parameter CreateParameterFromString(string testCase)
        {
           
        }
    }
}
