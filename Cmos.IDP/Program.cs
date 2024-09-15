using Cmos.IDP;
using Serilog;
using System.Net.Mail;
using System.Net;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));
    //EMAIL SETUP
    builder.Services.AddSingleton<SmtpClient>(serviceProvider =>
    {
        var smtpClient = new SmtpClient("mail.timetotimetechnical.com")
        {
            Port = 26, // or 465 for SSL
            Credentials = new NetworkCredential("admin@timetotimetechnical.com", "Somipk112@1"),
            EnableSsl = false,
        };
        return smtpClient;
    });

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();
    
    app.Run();
}
catch(HostAbortedException)
{
    //eat exception, cfr https://github.com/dotnet/efcore/issues/29809
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}