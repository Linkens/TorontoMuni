using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using ExpertCities.Blazor.Data;
using ExpertCities.Blazor;
using ExpertCities.Data;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var SupportedCulture = new[] { "en-US", "fr-CA" };

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<DataServices>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddLocalization();
builder.Services.AddDbContext<ECContext>(ServiceLifetime.Transient);
//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    var supportedCultures = new[]
//    {
//        new CultureInfo(SupportedCulture[0]),
//        new CultureInfo(SupportedCulture[1])
//    };

//    options.DefaultRequestCulture = new RequestCulture(culture: SupportedCulture[0], uiCulture: SupportedCulture[0]);
//    options.SupportedCultures = supportedCultures;
//    options.SupportedUICultures = supportedCultures;

//    options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
//    {
//        // My custom request culture logic
//        return await Task.FromResult(new ProviderCultureResult("en"));
//    }));
//});
using (var c = new ECContext())
{
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 1, Value = "Résidence" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 1, Value = "Chalet" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 1, Value = "Ranch" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 2, Value = "Tour administratif" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 2, Value = "Hôtel de ville" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 2, Value = "Caserne des pompiers" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 2, Value = "Parlement" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 2, Value = "Président" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 2, Value = "Ministères" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 4, Value = "Complexe sportif"});
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 4, Value = "Maison ou centre culturel" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 4, Value = "Salle de spectacle" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 8, Value = "Magasin" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 8, Value = "Condominium" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 16, Value = "Usine" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 16, Value = "Entrepôt" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 32, Value = "Grange" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 32, Value = "Entrepôt" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 64, Value = "Centre hospitalier" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 64, Value = "Résidence pour personnes âgées" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 128, Value = "Université" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 128, Value = "Collège" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 128, Value = "Centre de formation scolaire" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 128, Value = "Secondaire" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 128, Value = "Primaire" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 128, Value = "Maternel " });

    var b = new Building { Category = BuildCatEnum.School, Denomination = "Ecole", Structure = BuildStructEnum.Concrete };
    b.Country = "Canada";b.City = "Toronto"; b.CivicNumber = "15665421-4"; b.Telephone = "+4523884716"; b.Val_Acquire =1500000;b.Length = 45; b.Width = 50; b.Shape = BuildShapeEnum.Square; b.Date_Acquire = new DateTime(1970, 10, 5);b.Date_Commission = new DateTime(1970, 10, 5);
    c.Batiments.Add(b);
    c.Batiments.Add(new Building { Category = BuildCatEnum.Residential, Denomination = "Résidence", Structure = BuildStructEnum.Concrete });
    c.SaveChanges();
}
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
