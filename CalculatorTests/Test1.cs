using dotnet_demo_pipeline;
namespace CalculatorTests
{
    [TestClass]
    public sealed class CalculatorTests
    {
        [TestMethod]
        public void Add_ReturnsCorrectSum()
        {
            var calculator = new Calculator();
            var result = calculator.Add(2, 3);
            Assert.AreEqual(5, result);
        }
    }
}
