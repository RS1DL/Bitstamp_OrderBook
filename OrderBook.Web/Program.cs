using OrderBook.Web.Components;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Information()
    .WriteTo.RollingFile(builder.Configuration.GetSection("Logger").GetValue<string>("Path") +"/log-{Date}.txt",
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:W}] {Message:lj}{NewLine}{Exception}", 
            retainedFileCountLimit: 14, 
            fileSizeLimitBytes: 1073741824
            )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
