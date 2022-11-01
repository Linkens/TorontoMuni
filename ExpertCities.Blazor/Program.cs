using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using ExpertCities.Blazor;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using ExpertCities.Data;
using OxyPlot;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseWebRoot("wwwroot").UseStaticWebAssets();
var SupportedCulture = new[] { "en-CA", "fr-CA" };

var Ser = new SecretServices();
Ser.Configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<DataServices>();
builder.Services.AddSingleton(Ser);
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddSingleton<IRetrieveFile, MemoryFileManager>();
builder.Services.AddLocalization();
builder.Services.AddDbContext<ECContext>(ServiceLifetime.Transient);
builder.Services.AddOxyPlotBlazor();
DebugBuildingEntities.CreateDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapControllers();
app.UseRouting();
app.UseRequestLocalization(new RequestLocalizationOptions() { ApplyCurrentCultureToResponseHeaders = true }
    .SetDefaultCulture(SupportedCulture[0])
    .AddSupportedCultures(SupportedCulture)
    .AddSupportedUICultures(SupportedCulture));
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


