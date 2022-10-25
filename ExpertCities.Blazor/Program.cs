using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using ExpertCities.Blazor;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using ExpertCities.Data;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseWebRoot("wwwroot").UseStaticWebAssets();
var SupportedCulture = new[] { "en-US", "fr-CA" };

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<DataServices>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddSingleton<IRetrieveFile, MemoryFileManager>();
builder.Services.AddLocalization();
builder.Services.AddDbContext<ECContext>(ServiceLifetime.Transient);

BuildContext();

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


void BuildContext()
{
    using (var c = new ECContext())
    {
        var Keys = new List<int> { 1, 1, 1, 2, 2, 2, 2, 2, 2, 4, 4, 4, 8, 8, 16, 16, 32, 32, 64, 64, 128, 128, 128, 128, 128, 128 };
        var CA = new List<string> { "Résidence", "Chalet", "Ranch", "Tour administratif", "Hôtel de ville", "Caserne des pompiers"
        , "Parlement", "Président", "Ministères", "Complexe sportif", "Maison ou centre culturel", "Salle de spectacle", "Magasin", "Condominium",
        "Usine", "Entrepôt", "Grange", "Entrepôt", "Centre hospitalier", "Résidence pour personnes âgées", "Université", "Collège", "Centre de formation scolaire", "Secondaire", "Primaire", "Maternel" };
        var Values = new List<string> { "Home", "Cabin", "Ranch", "Administrative tower", "City Hall", "Fire station"
        , "Parliament", "President", "Ministries", "Sports Complex", "Cultural Center", "Theater", "Shop", "Condominium",
        "Factory", "Warehouse", "Barn", "Warehouse", "Hospital Center", "Retirement home", "University", "Middle School", "School training center", "High School", "School", "Kindergarten" };
        for (int i = 0; i < Keys.Count; i++)
        {
            c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = Keys[i], Value = Values[i], ValueCA = CA[i] });
        }
        var b = new Building { Category = BuildCatEnum.School, Denomination = "Ecole", Structure = BuildStructEnum.Concrete };
        b.Country = "Canada"; b.City = "Toronto"; b.CivicNumber = "15665421-4"; b.Telephone = "+4523884716"; b.Val_Acquire = 1500000; b.Length = 45; b.Width = 50; b.Shape = BuildShapeEnum.Square; b.Date_Acquire = new DateTime(1970, 10, 5); b.Date_Commission = new DateTime(1970, 10, 5);
        var w = new Work { State = WorkStateEnum.WorkOrder | WorkStateEnum.Completed, Building = b, WorkOrderDate = new DateTime(2022, 8, 12), IsInternal = true, Summary = "Autumn maintenance" };
        w.Actions = new List<WorkAction>();
        w.Actions.Add(new WorkAction { Date = new DateTime(2022, 8, 24), Worker = "Bob", Description = "Fixing the ceiling lamp" });
        w.Actions.Add(new WorkAction { Date = new DateTime(2022, 8, 26), Worker = "John", Description = "Unclogged the sink" });
        w.Actions.Add(new WorkAction { Date = new DateTime(2022, 8, 26), Worker = "John", Description = "Sprayed W40" });
        w.Actions.Add(new WorkAction { Date = new DateTime(2022, 8, 27), Worker = "Alex", Description = "Replaced hinges" });
        c.Works.Add(w);
        c.Buildings.Add(b);
        c.Buildings.Add(new Building { Category = BuildCatEnum.Residential, Denomination = "Résidence", Structure = BuildStructEnum.Concrete });
        c.SaveChanges();
    }
}