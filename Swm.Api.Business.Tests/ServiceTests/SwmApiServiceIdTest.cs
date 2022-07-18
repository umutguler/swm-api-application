using Microsoft.Extensions.Logging;
using Moq;
using Swm.Api.Business.Services;

namespace Swm.Api.Business.Tests.ServiceTests
{
    [TestFixture]
    public class SwmApiServiceIdTest
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
        public async Task GetUsersByAge_Positive_GetsAged23()
        {
            int expectedAge = 23;
            int expectedCount = 2;
            var swmApiService = new SwmApiService(MockHttpClientFactory.Object, MockLoggerFactory.Object);

            var result = (await swmApiService.GetUsersByAge(expectedAge)).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result[0], Is.Not.Null);
                Assert.That(result[0].Age, Is.EqualTo(expectedAge));
                Assert.That(result[1].Age, Is.EqualTo(expectedAge));
                Assert.That(result, Has.Count.EqualTo(expectedCount));
            });
        }

        [Test]
        public async Task GetUsersByAge_Positive_GetsAged66()
        {
            int expectedAge = 66;
            int expectedCount = 3;
            var swmApiService = new SwmApiService(MockHttpClientFactory.Object, MockLoggerFactory.Object);

            var result = (await swmApiService.GetUsersByAge(expectedAge)).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result[0], Is.Not.Null);
                Assert.That(result[0].Age, Is.EqualTo(expectedAge));
                Assert.That(result[1].Age, Is.EqualTo(expectedAge));
                Assert.That(result, Has.Count.EqualTo(expectedCount));
            });
        }

        [Test]
        public async Task GetUsersByAge_Negative_GetsAged1000()
        {
            int expectedAge = 1000;
            int expectedCount = 0;
            var swmApiService = new SwmApiService(MockHttpClientFactory.Object, MockLoggerFactory.Object);

            var result = (await swmApiService.GetUsersByAge(expectedAge)).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Empty);
                Assert.That(result, Is.Empty);
                Assert.That(result, Has.Count.EqualTo(expectedCount));
            });
        }

        [Test]
        public async Task GetUsersByAge_Negative_GetsAgedMinus1()
        {
            int expectedAge = -1;
            int expectedCount = 0;
            var swmApiService = new SwmApiService(MockHttpClientFactory.Object, MockLoggerFactory.Object);

            var result = (await swmApiService.GetUsersByAge(expectedAge)).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Empty);
                Assert.That(result, Is.Empty);
                Assert.That(result, Has.Count.EqualTo(expectedCount));
            });
        }
    }
}