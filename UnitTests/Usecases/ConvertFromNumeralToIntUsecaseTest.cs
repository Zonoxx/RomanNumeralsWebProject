using RomanNumeralsWebProject.UseCases;

namespace UnitTests
{
    [TestClass]
    public class ConvertFromNumeralsToIntTest
    {
        private ConvertFromNumeraltoIntUsecase? converter = new();

        [TestMethod]
        [DataRow("I", 1)]
        [DataRow("V", 5)]
        [DataRow("X", 10)]
        [DataRow("L", 50)]
        [DataRow("C", 100)]
        [DataRow("D", 500)]
        [DataRow("M", 1000)]
        public void simpleInputSchouldReturnCorrectly(string input, int expectedResult)
        {
            var convertedNumeral = converter!.convertRomanNumeralToInt(input);

            Assert.AreEqual(convertedNumeral, expectedResult);
        }
    }
}