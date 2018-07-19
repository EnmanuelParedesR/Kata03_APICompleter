using System;
using System.Collections.Generic;

namespace Kata_03_API
{
    public class APICompleter
    {
        public Result result = new Result();
        public int curIteration = 0;
        int interrogationSignPos = 0;
        bool isPastInterrogationSign = false;

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

        public int FindFirstInterrogationSignPosition(String s)
        {
            int position = 0;
            position = s.IndexOf('?');
            return position;
        }

        public string GetStringBetweenIndexes(string s, int startpos, int finishpos)
        {
            int stringLength = finishpos - (startpos + 1);
            string result = s.Substring(startpos + 1, stringLength);
            return result;
        }

        public Result CreateParameterFromString(string testCase)
        {
            if ((testCase == null || testCase.Length <= 0) && curIteration <= 0)
                return null;
            else
                if ((testCase == null || testCase.Length <= 0) && curIteration > 0)
                    return result;

            interrogationSignPos = FindFirstInterrogationSignPosition(testCase);

            int openingBracketPos = FindFirstOpeningBracketPosition(testCase);
            if (openingBracketPos == -1)
                return result;

            int closingBracketPos = FindFirstClosingBracketPosition(testCase);
            if (closingBracketPos == -1)
                return result;

            if(isPastInterrogationSign || (interrogationSignPos > -1 && interrogationSignPos < openingBracketPos))
            {
                isPastInterrogationSign = true;
                result.parameters.Add(new Parameter(GetStringBetweenIndexes(testCase, openingBracketPos, closingBracketPos), new QueryString()));
            }
            else
                result.parameters.Add(new Parameter(GetStringBetweenIndexes(testCase, openingBracketPos, closingBracketPos), new Path()));
            curIteration++;
            return CreateParameterFromString(testCase.Substring(closingBracketPos + 1));
        }

        public bool IsComplete(List<Parameter> expectedParameters, Dictionary<string, string> parameterValue)
        {
            bool isComplete = true;
            if (expectedParameters.Count < parameterValue.Count)
                isComplete = false;

            else
                if (expectedParameters.Count > parameterValue.Count)
                    isComplete = false;

                else
                    foreach(Parameter parameter in expectedParameters)
                    {
                        if (!parameterValue.ContainsKey(parameter.name))
                            isComplete = false;
                    }

            return isComplete;
        }

        public Result CompleteURL(string URL, Dictionary<string, string> parameterValues)
        {
            result = CreateParameterFromString(URL);
            result.isComplete = IsComplete(result.parameters, parameterValues);

            if (result.isComplete)
                result.completedURL = CompleteURL(URL, result.parameters, parameterValues);
            else
                result.completedURL = "";
            return result;
        }

        private string CompleteURL(string URL, List<Parameter> parameters, Dictionary<string, string> parameterValues)
        {
            foreach(Parameter parameter in parameters)
            {
                string bracketedName = "{" + parameter.name + "}";
                string value;
                parameterValues.TryGetValue(parameter.name, out value);
                URL = URL.Replace(bracketedName, value);
            }
            return URL;
        }
    }
}
