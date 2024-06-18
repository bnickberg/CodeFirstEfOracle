using CodeFirstEfOracle;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;

namespace EfTests
{
    public class UnitTest1
    {
        protected IContainer _container;

        public UnitTest1()
        {

        }

        [Fact]
        public async Task Test1()
        {
            _container = new ContainerBuilder()
                .WithImage("gvenzl/oracle-xe:11-slim-faststart")
                .WithPortBinding(1521, 1521)
                .WithEnvironment("ORACLE_PASSWORD", "haj")
                .WithEnvironment("APP_USER", "beo")
                .WithEnvironment("APP_USER_PASSWORD", "haj")
                .Build();

            await _container.StartAsync();

            using var ctx = new OracleContext();

            ctx.Add(new User { Id = 3, FirstName = "Jack" });
            await ctx.SaveChangesAsync();
            Assert.True(ctx.Users.Any(u => u.Id == 3));
            ctx.Users.ExecuteDelete();
            Assert.False(ctx.Users.Any());
        }
    }
}