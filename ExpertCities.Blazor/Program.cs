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
        var CA = new List<string> { "R�sidence", "Chalet", "Ranch", "Tour administratif", "H�tel de ville", "Caserne des pompiers"
        , "Parlement", "Pr�sident", "Minist�res", "Complexe sportif", "Maison ou centre culturel", "Salle de spectacle", "Magasin", "Condominium",
        "Usine", "Entrep�t", "Grange", "Entrep�t", "Centre hospitalier", "R�sidence pour personnes �g�es", "Universit�", "Coll�ge", "Centre de formation scolaire", "Secondaire", "Primaire", "Maternel" };
        var Values = new List<string> { "Home", "Cabin", "Ranch", "Administrative tower", "City Hall", "Fire station"
        , "Parliament", "President", "Ministries", "Sports Complex", "Cultural Center", "Theater", "Shop", "Condominium",
        "Factory", "Warehouse", "Barn", "Warehouse", "Hospital Center", "Retirement home", "University", "Middle School", "School training center", "High School", "School", "Kindergarten" };
        for (int i = 0; i < Keys.Count; i++)
        {
            c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = Keys[i], Value = Values[i], ValueCA = CA[i] });
        }
        var b = new Building { Category = BuildCatEnum.School, Denomination = "Ecole", Structure = BuildStructEnum.Concrete };
        b.Lat = -74.60503901404996f; b.Long = 45.60859520641057f;
        b.Country = "Canada"; b.City = "Toronto"; b.CivicNumber = "15665421-4"; b.Telephone = "+4523884716"; b.Val_Acquire = 1500000; b.Length = 45; b.Width = 50; b.Shape = BuildShapeEnum.Square; b.Date_Acquire = new DateTime(1970, 10, 5); b.Date_Commission = new DateTime(1970, 10, 5);
        var w = new Work { State = WorkStateEnum.WorkOrder | WorkStateEnum.Completed, Building = b, WorkOrderDate = new DateTime(2022, 8, 12), WorkCompleted = new DateTime(2022, 8, 27), IsInternal = true, Summary = "Autumn maintenance" };
        BuildManyActions(w);
        c.Works.Add(w);
        c.Buildings.Add(b);
        b = new Building { Category = BuildCatEnum.Residential, Denomination = "R�sidence", Structure = BuildStructEnum.Concrete };
        b.Lat = -74.61084084623728f; b.Long = 45.60228250701617f;
        c.Buildings.Add(b);
        c.SaveChanges();
    }
}
void BuildManyActions(Work w)
{
    w.Actions = new List<WorkAction>();
    var Workers = new List<string> { "Bob", "John", "Alex","Carlos","Julian" };
    var Descriptions = new List<string> { "Fixing the ceiling lamp", "Unclogged the sink", "Sprayed W40", "Replaced hinges","Replacing furniture","Removing broken item" };
    var Random = new Random();
    for (int i = 0; i < 40; i++)
    {
        w.Actions.Add(new WorkAction { Date = DateTime.Now.Date.AddDays(-Random.Next(30)), Worker = Workers[Random.Next(Workers.Count)], Description = Descriptions[Random.Next(Descriptions.Count)] });
    }

}