using Api.Temeprature.IoC.Tests;
using Api.Temperature.Data.Context.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Temperature.Tests.Common
{
    public class TestBase
    {
        protected ServiceProvider _serviceProvider;

        protected IMeteoFranceDBContext _context;

        private void InitTestDatabase()
        {
            _context = _serviceProvider.GetService<IMeteoFranceDBContext>();
            _context?.Database.EnsureDeleted();
            _context?.Database.EnsureCreated();


        }
        public void SetUpTest()
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging()
                .ConfigureDBContextTest()
                .ConfigureInjectionDependencyRepositoryTest()
                .BuildServiceProvider();

            InitTestDatabase();
        }

        public void CleanTest()
        {
            _context?.Database.EnsureDeleted();
            _serviceProvider.Dispose();
            _context?.Dispose();
        }

    }
}
