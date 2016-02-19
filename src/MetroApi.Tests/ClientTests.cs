using Microsoft.Owin.Testing;
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
        public async void MetroClientTest()
        {
            using (var server = TestServer.Create<OwinTestConf>())
            {
                // Arrange 
                var client = new Client.MetroApiClient(server.BaseAddress);
                // Act
                var result = client.GetSaintPetersburgMetro();
                // Assert
                Assert.NotNull(result);
                Assert.NotNull(result.MetroLines);
                Assert.NotEmpty(result.MetroLines);
            }
        }
    }
}