using Api.Temperature.Data.Repository.Contract;
using Api.Temperature.Tests.Common;
using Api.Temperature.Tests.Common.ScenarioDatas;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Temperature.Tests.Unitaire
{
    [TestFixture]
    public class UniteUnitTest : TestBase
    {
        private IUniteRepository _uniteRepository;

        [SetUp]
        public void Setup()
        {
            SetUpTest();

            _uniteRepository = _serviceProvider?.GetService<IUniteRepository>();

            _context.CreateUnite();
        }

        [TearDown]
        public void TearDown()
        {
            CleanTest();
        }

        [Test]
        public async Task GetUniteById_ReturnOneUnite()
        {
            // Arrange
            const int uniteId = 1;

            // Act
            var unite = await _uniteRepository.GetUniteByIdAsync(uniteId).ConfigureAwait(false);

            // Assert
            Assert.That(unite, Is.Not.Null);
            Assert.That(unite.IdUnite, Is.EqualTo(uniteId));
        }
    }
}
