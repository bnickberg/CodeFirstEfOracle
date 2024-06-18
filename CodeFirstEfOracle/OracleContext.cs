using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace CodeFirstEfOracle
{
    public class OracleContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public OracleContext() : base()
        {
            ConfigureOracle();
        }

        public OracleContext(DbContextOptions<OracleContext> options) : base(options)
        {
            ConfigureOracle();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connectString = "User Id=beo;Password=haj;Data Source=localhost:1521/xe;";
            builder.UseOracle(connectString, (options) => options.UseOracleSQLCompatibility("11"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "John" },
                new User { Id = 2, FirstName = "Jane" }
            );
        }

        protected static void ConfigureOracle()
        {
            if (OracleConfiguration.OracleDataSources.Count == 0 && OracleConfiguration.StatementCacheSize == 0)
            {
                // Set default statement cache size to be used by all connections.
                OracleConfiguration.StatementCacheSize = 25;
                // Disable self tuning by default.
                OracleConfiguration.SelfTuning = false;
                // Bind all parameters by name.
                OracleConfiguration.BindByName = true;
                // Set default timeout to 60 seconds.
                OracleConfiguration.CommandTimeout = 60;
                // Set default fetch size as 1 MB.
                OracleConfiguration.FetchSize = 1024 * 1024;
                // Set tracing options
                OracleConfiguration.TraceOption = 1;
                OracleConfiguration.TraceFileLocation = @"c:\traces";
                // Uncomment below to generate trace files
                //OracleConfiguration.TraceLevel = 7;
                // Set network properties
                OracleConfiguration.SendBufferSize = 8192;
                OracleConfiguration.ReceiveBufferSize = 8192;
                OracleConfiguration.DisableOOB = false;
            }
        }


    }
}
