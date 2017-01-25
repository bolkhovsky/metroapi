using Xunit;
using Xunit.Abstractions;

namespace MetroApi.Tests
{
    public class ClientTests
    {
        private readonly ITestOutputHelper _output;

        public ClientTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void MetroClientTest()
        {
            // Arrange 
            var client = new Client.MetroApiClient();
            // Act
            var result = client.GetSaintPetersburgMetro().Result;
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Lines);
            Assert.NotEmpty(result.Lines);
        }
    }
}