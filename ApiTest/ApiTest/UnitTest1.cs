using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kata_03_API;
namespace ApiTest
{
    [TestClass]
    public class APITest
    {
        [TestMethod]
        public void FindFirstOpeningCurlyBrackets()
        {
            String Example = "Hola {  } Jesus";

            APICompleter Trial = new APICompleter();
            int Expected = 5;

            Assert.AreEqual(Expected, Trial.FindFirstOpeningBracketPosition(Example));
        }

        [TestMethod]
        public void TryToFindFirstOpeningCurlyBracketsWhenThereIsNone()
        {
            String Example = "Hola   } Jesus";

            APICompleter Trial = new APICompleter();
            int Expected = -1;

            Assert.AreEqual(Expected, Trial.FindFirstOpeningBracketPosition(Example));
        }

        [TestMethod]
        public void FindFirstClosingCurlyBrackets()
        {
            String Example = "Hola {  } Jesus";

            APICompleter Trial = new APICompleter();
            int Expected = 8;

            Assert.AreEqual(Expected, Trial.FindFirstClosingBracketPosition(Example));
        }

        [TestMethod]
        public void FindStringInTheMiddleOfTwoArrayPosition()
        {
            String Example = "{Hola}";

            APICompleter Trial = new APICompleter();
            string Expected = "Hola";
            
            Assert.AreEqual(Expected, Trial.GetStringBetweenBrackets(Example, 0, 5));
        }

        [TestMethod]
        public void CreateParameterOfPathType()
        {
            string name = "id";
            IType type = new Path();

            Parameter param = new Parameter(name, type);
            Assert.AreEqual(type.GetType(), param.type.GetType());
        }

        [TestMethod]
        public void CreateParameterOfQueryStringType()
        {
            string name = "id";
            IType type = new QueryString();

            Parameter param = new Parameter(name, type);
            Assert.AreEqual(type.GetType(), param.type.GetType());
        }

        [TestMethod]
        public void CreatParameterFromNameBetweenBracket()
        {
            string testCase = "ID";
            string expected = "ID";

            APICompleter completer = new APICompleter();

            Parameter result = completer.CreateParameterFromString(testCase);
            Assert.AreEqual(expected, result.name);
        }




    }
}
