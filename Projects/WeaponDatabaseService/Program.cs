using Sentry.Profiling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.WebHost.UseSentry(options =>
{
    options.Dsn = "https://3148ff56b0e94e37a61069aa9e998362@o4506960048881664.ingest.us.sentry.io/4507288528748544";
    options.Debug = true; // Enable Sentry SDK debugging
    options.TracesSampleRate = 1.0; // Adjust this value for sampling traces
    options.ProfilesSampleRate = 1.0; // Adjust this value for sampling profiles
    options.AttachStacktrace = true;
    options.EnableTracing = true;
    options.AddIntegration(new ProfilingIntegration());
    options.ExperimentalMetrics = new ExperimentalMetricsOptions
    {
        EnableCodeLocations = true
    };
});

builder.Logging.AddSentry(o =>
{
    o.InitializeSdk = false; // Prevent re-initialization
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
