using AbpTemplate.Data;
using Serilog;
using Serilog.Events;
using Volo.Abp.Data;

namespace AbpTemplate;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var seqApiKey = Environment.GetEnvironmentVariable("SEQ_API_KEY");
        var loggerConfiguration = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "AbpTemplate")
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console());

        if (seqApiKey != null)
        {
            loggerConfiguration.WriteTo.Seq("https://seq.antosubash.com", apiKey: seqApiKey);
        }

        Log.Logger = loggerConfiguration.CreateLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson().UseAutofac().UseSerilog();
            builder.Services.AddDataMigrationEnvironment();
            await builder.AddApplicationAsync<AbpTemplateModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.Services.GetRequiredService<AbpTemplateDbMigrationService>().MigrateAsync();
            Log.Information("Starting AbpTemplate.");
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, "AbpTemplate terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
