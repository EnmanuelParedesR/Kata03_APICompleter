using System;
using System.Collections.Generic;
using Kata_03_API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APITest
{
    [TestClass]
    public class UnitTest1
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

            Assert.AreEqual(Expected, Trial.GetStringBetweenIndexes(Example, 0, 5));
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
        public void NullOrEmptyStringReturnsNull()
        {
            string testCase = "";
            Result expected = new Result();

            APICompleter completer = new APICompleter();

            Result result = completer.CreateParameterFromString(testCase);
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void NoParametersButStringHasTextReturnsEmptyList()
        {
            string testCase = "ID";
            Result expected = new Result();
            List<Parameter> expectedParameters = new List<Parameter>();
            expected.parameters = expectedParameters;

            APICompleter completer = new APICompleter();

            Result result = completer.CreateParameterFromString(testCase);
            CollectionAssert.AreEqual(expected.parameters, result.parameters);
        }

        [TestMethod]
        public void SinglePathParameterGetsAddedToList()
        {
            string testCase = "{ID}";
            Result expected = new Result();
            List<Parameter> expectedParameters = new List<Parameter>();
            expectedParameters.Add(new Parameter("ID", new Path()));
            expected.parameters = expectedParameters;

            APICompleter completer = new APICompleter();

            Result result = completer.CreateParameterFromString(testCase);
            Assert.AreEqual(0, expected.parameters[0].CompareTo(result.parameters[0]));
        }

        [TestMethod]
        public void TwoPathParameterGetAddedToList()
        {
            string testCase = "{ID} Empty Space {Name}";
            Result expected = new Result();
            List<Parameter> expectedParameters = new List<Parameter>();
            expectedParameters.Add(new Parameter("ID", new Path()));
            expectedParameters.Add(new Parameter("Name", new Path()));
            expected.parameters = expectedParameters;

            APICompleter completer = new APICompleter();

            Result result = completer.CreateParameterFromString(testCase);
            Assert.AreEqual(0, expected.parameters[1].CompareTo(result.parameters[1]));
        }

        [TestMethod]
        public void OnePathParameterInMiddleOfStringGetAddedToList()
        {
            string testCase = "Empty Space {ID} More Empty Space";
            Result expected = new Result();
            List<Parameter> expectedParameters = new List<Parameter>();
            expectedParameters.Add(new Parameter("ID", new Path()));
            expected.parameters = expectedParameters;

            APICompleter completer = new APICompleter();

            Result result = completer.CreateParameterFromString(testCase);
            Assert.AreEqual(0, expected.parameters[0].CompareTo(result.parameters[0]));
        }

        [TestMethod]
        public void AddQueryStringParameterToList()
        {
            string testCase = "?{ID}";
            Result expected = new Result();
            List<Parameter> expectedParameters = new List<Parameter>();
            expectedParameters.Add(new Parameter("ID", new QueryString()));
            expected.parameters = expectedParameters;

            APICompleter completer = new APICompleter();

            Result result = completer.CreateParameterFromString(testCase);
            Assert.AreEqual(0, expected.parameters[0].CompareTo(result.parameters[0]));
        }

        [TestMethod]
        public void AddTwoQueryStringParametersToList()
        {
            string testCase = "?{ID} Empty Space {Name}";
            Result expected = new Result();
            List<Parameter> expectedParameters = new List<Parameter>();
            expectedParameters.Add(new Parameter("ID", new QueryString()));
            expectedParameters.Add(new Parameter("Name", new QueryString()));
            expected.parameters = expectedParameters;

            APICompleter completer = new APICompleter();

            Result result = completer.CreateParameterFromString(testCase);
            Assert.AreEqual(0, expected.parameters[1].CompareTo(result.parameters[1]));
        }

        [TestMethod]
        public void AddPathParameterAndQueryStringParameterToList()
        {
            string testCase = "{ID} ?Empty Space {Name}";
            Result expected = new Result();
            List<Parameter> expectedParameters = new List<Parameter>();
            expectedParameters.Add(new Parameter("ID", new Path()));
            expectedParameters.Add(new Parameter("Name", new QueryString()));
            expected.parameters = expectedParameters;

            APICompleter completer = new APICompleter();

            Result result = completer.CreateParameterFromString(testCase);
            List<int> expectedComparison = new List<int>();
            for(int i = 0; i < 2; i++)
                expectedComparison.Add(0);

            List<int> comparisonResults = new List<int>();
            comparisonResults.Add(expected.parameters[0].CompareTo(result.parameters[0]));
            comparisonResults.Add(expected.parameters[1].CompareTo(result.parameters[1]));
            CollectionAssert.AreEqual(expectedComparison, comparisonResults);
        }

        [TestMethod]
        public void TestAddingParametersFromTestURL()
        {
            string testCase = "http://www.somesite.com/api/students/{id}/grades/?min={minValue}";
            Result expected = new Result();
            List<Parameter> expectedParameters = new List<Parameter>();
            expectedParameters.Add(new Parameter("id", new Path()));
            expectedParameters.Add(new Parameter("minValue", new QueryString()));
            expected.parameters = expectedParameters;

            APICompleter completer = new APICompleter();
            Result result = completer.CreateParameterFromString(testCase);

            List<int> expectedComparison = new List<int>();
            for (int i = 0; i < 2; i++)
                expectedComparison.Add(0);

            List<int> comparisonResults = new List<int>();
            comparisonResults.Add(expected.parameters[0].CompareTo(result.parameters[0]));
            comparisonResults.Add(expected.parameters[1].CompareTo(result.parameters[1]));

            CollectionAssert.AreEqual(expectedComparison, comparisonResults);
        }

        [TestMethod]
        public void IfParameterListContainsAllKeysInDictionaryReturnTrue()
        {
            List<Parameter> expectedParameters = new List<Parameter>();
            expectedParameters.Add(new Parameter("id", new Path()));
            expectedParameters.Add(new Parameter("minValue", new QueryString()));

            Dictionary<string, string> parameterValue = new Dictionary<string, string>();
            parameterValue.Add("id", "1070890");
            parameterValue.Add("minValue", "90");

            APICompleter completer = new APICompleter();
            Assert.AreEqual(true, completer.IsComplete(expectedParameters, parameterValue));
        }

        [TestMethod]
        public void IfParameterListDoesNotContainsAllKeysInDictionaryReturnTrue()
        {
            List<Parameter> expectedParameters = new List<Parameter>();
            expectedParameters.Add(new Parameter("id", new Path()));

            Dictionary<string, string> parameterValue = new Dictionary<string, string>();
            parameterValue.Add("id", "1070890");
            parameterValue.Add("minValue", "90");

            APICompleter completer = new APICompleter();
            Assert.AreEqual(false, completer.IsComplete(expectedParameters, parameterValue));
        }

        [TestMethod]
        public void GivenStringWithAllTheParemetersAndDictionaryReturnResponseWithParametersAndWithCompletionStatus()
        {
            string testCase = "http://www.somesite.com/api/students/{id}/grades/?min={minValue}";

            Dictionary<string, string> parameterValue = new Dictionary<string, string>();
            parameterValue.Add("id", "1070890");
            parameterValue.Add("minValue", "90");

            Result expected = new Result();
            List<Parameter> expectedParameters = new List<Parameter>();
            expectedParameters.Add(new Parameter("id", new Path()));
            expectedParameters.Add(new Parameter("minValue", new QueryString()));
            expected.parameters = expectedParameters;

            APICompleter completer = new APICompleter();

            Result result = completer.CompleteURL(testCase, parameterValue);
            List<int> expectedComparison = new List<int>();
            for (int i = 0; i < 3; i++)
                expectedComparison.Add(0);

            List<int> comparisonResults = new List<int>();
            comparisonResults.Add(expected.parameters[0].CompareTo(result.parameters[0]));
            comparisonResults.Add(expected.parameters[1].CompareTo(result.parameters[1]));
            comparisonResults.Add(true.CompareTo(result.isComplete));
            CollectionAssert.AreEqual(expectedComparison, comparisonResults);
        }

        [TestMethod]
        public void GivenStringWithoutAllTheParemetersAndDictionaryReturnResponseWithParametersAndWithCompletionStatus()
        {
            string testCase = "http://www.somesite.com/api/students/{id}/grades/?";

            Dictionary<string, string> parameterValue = new Dictionary<string, string>();
            parameterValue.Add("id", "1070890");
            parameterValue.Add("minValue", "90");

            Result expected = new Result();
            List<Parameter> expectedParameters = new List<Parameter>();
            expectedParameters.Add(new Parameter("id", new Path()));
            expected.parameters = expectedParameters;

            APICompleter completer = new APICompleter();

            Result result = completer.CompleteURL(testCase, parameterValue);
            List<int> expectedComparison = new List<int>();
            for (int i = 0; i < 2; i++)
                expectedComparison.Add(0);

            List<int> comparisonResults = new List<int>();
            for (int i = 0; i < expected.parameters.Count; i++)
                comparisonResults.Add(expected.parameters[i].CompareTo(result.parameters[i]));

            comparisonResults.Add(false.CompareTo(result.isComplete));

            CollectionAssert.AreEqual(expectedComparison, comparisonResults);
        }

        [TestMethod]
        public void GivenStringWithAllTheParemetersAndDictionaryReturnResponseWithParametersAndWithCompletionStatusAndFinalURL()
        {
            string testCase = "http://www.somesite.com/api/students/{id}/grades/?min={minValue}";
            string completeURL = "http://www.somesite.com/api/students/1070890/grades/?min=90";

            Dictionary<string, string> parameterValue = new Dictionary<string, string>();
            parameterValue.Add("id", "1070890");
            parameterValue.Add("minValue", "90");

            Result expected = new Result();
            List<Parameter> expectedParameters = new List<Parameter>();
            expectedParameters.Add(new Parameter("id", new Path()));
            expectedParameters.Add(new Parameter("minValue", new QueryString()));
            expected.parameters = expectedParameters;

            APICompleter completer = new APICompleter();

            Result result = completer.CompleteURL(testCase, parameterValue);
            List<int> expectedComparison = new List<int>();
            for (int i = 0; i < (expectedParameters.Count + 2); i++)
                expectedComparison.Add(0);

            List<int> comparisonResults = new List<int>();
            for (int i = 0; i < expected.parameters.Count; i++)
                comparisonResults.Add(expected.parameters[i].CompareTo(result.parameters[i]));

            comparisonResults.Add(true.CompareTo(result.isComplete));
            comparisonResults.Add(completeURL.CompareTo(result.completedURL));

            CollectionAssert.AreEqual(expectedComparison, comparisonResults);
        }

        [TestMethod]
        public void GivenStringWithOutAllTheParemetersAndDictionaryReturnResponseWithParametersAndWithCompletionStatusAndWithoutFinalURL()
        {
            string testCase = "http://www.somesite.com/api/students/{id}/grades/";
            string completeURL = "";

            Dictionary<string, string> parameterValue = new Dictionary<string, string>();
            parameterValue.Add("id", "1070890");
            parameterValue.Add("minValue", "90");

            Result expected = new Result();
            List<Parameter> expectedParameters = new List<Parameter>();
            expectedParameters.Add(new Parameter("id", new Path()));
            expected.parameters = expectedParameters;

            APICompleter completer = new APICompleter();

            Result result = completer.CompleteURL(testCase, parameterValue);
            List<int> expectedComparison = new List<int>();
            for (int i = 0; i < (expectedParameters.Count + 2); i++)
                expectedComparison.Add(0);

            List<int> comparisonResults = new List<int>();
            for (int i = 0; i < expected.parameters.Count; i++)
                comparisonResults.Add(expected.parameters[i].CompareTo(result.parameters[i]));

            comparisonResults.Add(false.CompareTo(result.isComplete));
            comparisonResults.Add(completeURL.CompareTo(result.completedURL));

            CollectionAssert.AreEqual(expectedComparison, comparisonResults);
        }
    }
}
