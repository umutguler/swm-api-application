using Microsoft.Extensions.Logging;
using Moq;
using Swm.Api.Business.Services;

namespace Swm.Api.Business.Tests.ServiceTests
{
    [TestFixture]
    public class SwmApiServiceGenderCountTest
    {
        public HttpClient HttpClient { get; set; }
        public Mock<IHttpClientFactory> MockHttpClientFactory { get; set; }
        public Mock<ILogger> MockLogger { get; set; }
        public Mock<ILoggerFactory> MockLoggerFactory { get; set; }

        [SetUp]
        public void Init()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri("https://f43qgubfhf.execute-api.ap-southeast-2.amazonaws.com/sampletest")
            };

            MockHttpClientFactory = new Mock<IHttpClientFactory>();
            MockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(HttpClient);
            MockLogger = new Mock<ILogger>();
            MockLogger.Setup(m => m.Log(
                              LogLevel.Information,
                              It.IsAny<EventId>(),
                              It.IsAny<object>(),
                              It.IsAny<Exception>(),
                              It.IsAny<Func<object, Exception, string>>()));
            MockLoggerFactory = new Mock<ILoggerFactory>();
            MockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(() => MockLogger.Object);
        }


        [Test]
        public async Task GetAgeGenderCount_Positive_StringTest()
        {
            string expected = string.Format("23: M : 1, T : 1\n54: M : 1\n66: Y : 2, F : 1\n");
            var swmApiService = new SwmApiService(MockHttpClientFactory.Object, MockLoggerFactory.Object);

            var result = (await swmApiService.GetGenderCountPerAge()).ToString();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Empty);
                Assert.That(result, Is.EqualTo(expected));
            });
        }
    }
}