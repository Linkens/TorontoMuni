using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using TorontoMuni.Blazor.Data;
using TorontoMuni.Blazor;
using TorontoMuni.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<DataServices>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddDbContext<TMContext>(ServiceLifetime.Transient);
using(var c = new TMContext())
{
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 1, Value = "R�sidence" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 1, Value = "Chalet" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 1, Value = "Ranch" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 2, Value = "Tour administratif" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 2, Value = "H�tel de ville" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 2, Value = "Caserne des pompiers" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 2, Value = "Parlement" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 2, Value = "Pr�sident" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 2, Value = "Minist�res" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 4, Value = "Complexe sportif"});
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 4, Value = "Maison ou centre culturel" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 4, Value = "Salle de spectacle" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 8, Value = "Magasin" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 8, Value = "Condominium" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 16, Value = "Usine" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 16, Value = "Entrep�t" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 32, Value = "Grange" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 32, Value = "Entrep�t" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 64, Value = "Centre hospitalier" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 64, Value = "R�sidence pour personnes �g�es" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 128, Value = "Universit�" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 128, Value = "Coll�ge" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 128, Value = "Centre de formation scolaire" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 128, Value = "Secondaire" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 128, Value = "Primaire" });
    c.Choices.Add(new ChoiceList { Filter = ChoiceListEnum.Denomination, Key = 128, Value = "Maternel " });

    var b = new Batiment { Categorie = BatCatEnum.Scolaire, Denomination = "Ecole", Structure = BatStructEnum.Beton };
    b.Pays = "Canada";b.Ville = "Toronto"; b.NumeroCivique = "15665421-4"; b.Telephone = "+4523884716"; b.ValAcqu =1500000;b.Longueur = 45; b.Largeur = 50; b.Forme = BatFormEnum.Carr�;
    b.Dates = new List<BatDates>() { new BatDates { Type = DateTypeEnum.Acquisition, Date = new DateTime(1970, 10, 5) }, { new BatDates { Type = DateTypeEnum.MiseEnService, Date = new DateTime(1971, 1, 3) } } };
    c.Batiments.Add(b);
    c.Batiments.Add(new Batiment { Categorie = BatCatEnum.Residentiel, Denomination = "R�sidence", Structure = BatStructEnum.Beton });
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

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
