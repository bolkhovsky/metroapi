using MetroApi.Core.Models;
using Microsoft.Owin.Testing;
using System.Net.Http;
using Xunit;
using Xunit.Abstractions;

namespace MetroApi.Tests
{
    public class HttpTests
    {
        private readonly ITestOutputHelper _output;

        public HttpTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async void BasicHttpTest()
        {
            using (var server = TestServer.Create<OwinTestConf>())
            {
                var client = server.HttpClient;
                // Act
                var response = await client.GetAsync("/api/metro/spb");
                var result = await response.Content.ReadAsAsync<City>();
                // Assert
                Assert.NotNull(result);
                Assert.Equal(Core.Constants.CityIds.SaintPetersburg, result.Id);
                Assert.NotNull(result.MetroLines);
                Assert.NotEmpty(result.MetroLines);
            }
        }
    }
}