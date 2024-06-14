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

SentrySdk.Init(options =>
{
    options.Dsn = "https://3148ff56b0e94e37a61069aa9e998362@o4506960048881664.ingest.us.sentry.io/4507288528748544";
    options.ExperimentalMetrics = new ExperimentalMetricsOptions
    {
        EnableCodeLocations = true
    };
});

// Add Sentry
builder.Logging.AddSentry(o =>
{
    o.Dsn = "https://3148ff56b0e94e37a61069aa9e998362@o4506960048881664.ingest.us.sentry.io/4507288528748544"; //Add url here
    o.Debug = true;
    o.TracesSampleRate = 1.0;
    o.ProfilesSampleRate = 1.0;
    o.AddIntegration(new ProfilingIntegration());
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
