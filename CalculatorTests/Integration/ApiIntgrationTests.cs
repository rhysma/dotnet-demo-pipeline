using IntegrationAPI;
using Microsoft.AspNetCore.Mvc.Testing;
namespace CalculatorTests;

[TestClass]
public class ApiIntegrationTests
{
    private static WebApplicationFactory<Program> _factory;
    private static HttpClient _client;

    [ClassInitialize]
    public static void Setup(TestContext context)
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task Multiply_Endpoint_ReturnsCorrectResults()
    {
        var response = await _client.GetAsync("/multiply?a=4&b=5");
        var result = await response.Content.ReadAsStringAsync();

        Assert.AreEqual("20", result);
    }

    [TestMethod]
    [TestCategory("Smoke")]
    public async Task HealthCheck_ReturnsHealthStatus()
    {
        var response = await _client.GetAsync("/health");
        var result = await response.Content.ReadAsStringAsync();

        Assert.AreEqual("\"Healthy\"", result);
        Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    [TestCategory("Performance")]
    public async Task MultiplyEndpoint_RespondsWithin500Milliseconds()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var response = await _client.GetAsync("/multiply?a=10&b=20");

        stopwatch.Stop();

        Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.IsTrue(stopwatch.ElapsedMilliseconds < 500, $"Expected response time was under 500ms but was {stopwatch.ElapsedMilliseconds}ms");

    }
}
