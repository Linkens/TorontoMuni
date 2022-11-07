using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpertCities.Data
{
    public static class DebugBuildingEntities
    {
        public static void CreateDebug()
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
                var b = new Asset { Category = AssetCatEnum.School, Denomination = "Ecole", Structure = AssetStructEnum.Concrete };
                var B1 = b;
                b.Lat = -74.60503901404996f; b.Long = 45.60859520641057f;
                b.Country = "Canada"; b.City = "Toronto"; b.CivicNumber = "15665421-4"; b.Telephone = "+4523884716"; b.Val_Acquire = 1500000; b.Length = 45; b.Width = 50; b.Shape = BuildShapeEnum.Square; b.Date_Acquire = new DateTime(1970, 10, 5); b.Date_Commission = new DateTime(1970, 10, 5);
                var w = new Work { State = WorkStateEnum.WorkOrder | WorkStateEnum.Completed, Asset = b, WorkOrderDate = new DateTime(2022, 8, 12), WorkCompleted = new DateTime(2022, 8, 27), IsInternal = true, Summary = "Autumn maintenance" };
                BuildManyActions(w);
                var Random = new Random();
                c.Works.Add(w);
                b.Inspections = new List<Inspection>();
                b.Inspections.Add(new Inspection { Class = InspectionClassEnum.C_InteriorFinishing, Item = "Drain français", Type = InspectionTypeEnum.Scheduled, Scheduled = DateTime.Now.Date.AddDays(Random.Next(5)) });
                b.Inspections.Add(new Inspection { Class = InspectionClassEnum.A_Foundation, Item = "Mur Est", Type = InspectionTypeEnum.Scheduled, Scheduled = DateTime.Now.Date.AddDays(Random.Next(5)) });
                b.Inspections.Add(new Inspection { Class = InspectionClassEnum.A_Foundation, Item = "Mur Sud", Type = InspectionTypeEnum.Spontaneous, Scheduled = DateTime.Now.Date.AddDays(Random.Next(5)) });
                b.Inspections.Add(new Inspection { Class = InspectionClassEnum.A_Foundation, Item = "Mur Sud", Type = InspectionTypeEnum.Scheduled, Scheduled = DateTime.Now.Date.AddDays(Random.Next(5)) });
                var d = DateTime.Now.Date.AddDays(Random.Next(5));
                b.Inspections.Add(new Inspection { Class = InspectionClassEnum.A_Foundation, Item = "Mur 1", Type = InspectionTypeEnum.Spontaneous, Scheduled = d });
                b.Inspections.Add(new Inspection { Class = InspectionClassEnum.A_Foundation, Item = "Mur 2", Type = InspectionTypeEnum.Spontaneous, Scheduled = d });
                b.Inspections.Add(new Inspection { Class = InspectionClassEnum.A_Foundation, Item = "Mur 3", Type = InspectionTypeEnum.Spontaneous, Scheduled = d });
                d = DateTime.Now.Date.AddDays(Random.Next(20));
                b.Inspections.Add(new Inspection { Class = InspectionClassEnum.A_Foundation, Item = "Drain 1", Type = InspectionTypeEnum.Spontaneous, Scheduled = d });
                b.Inspections.Add(new Inspection { Class = InspectionClassEnum.A_Foundation, Item = "Drain 2", Type = InspectionTypeEnum.Scheduled, Scheduled = d });
                b.Inspections.Add(new Inspection { Class = InspectionClassEnum.A_Foundation, Item = "Drain 3", Type = InspectionTypeEnum.Scheduled, Scheduled = d });
                c.Buildings.Add(b);
                c.Workforce.Add(new Workforce { Name = "Carlos Gnacadja", Type = WorkforceTypeEnum.InspectionAgent });
                c.Workforce.Add(new Workforce { Name = "Samuel Cardarelli", Type = WorkforceTypeEnum.InspectionAgent });
                c.Workforce.Add(new Workforce { Name = "Jean-Francois Dubois", Type = WorkforceTypeEnum.InspectionAgent });
                b = new Asset { Category = AssetCatEnum.Residential, Denomination = "Résidence", Structure = AssetStructEnum.Concrete };
                b.Lat = -74.61084084623728f; b.Long = 45.60228250701617f;
                c.Buildings.Add(b);
                var f = CreateUniformat(c);
                CreateInventory(c, B1, f);
                c.SaveChanges();
                LoadManyNames();
            }
        }

        private static void CreateInventory(ECContext c, Asset b1, List<Uniformat> Formats)
        {
            b1.Inventory = new List<Inventory> { new Inventory { Exterior = true, Uniformat = Formats.First(u => u.Code == "A202003"), Method = InventoryMethodEnum.Uniformat, Manufacturer="GE" } };
            b1.Inventory.Add(new Inventory { Exterior = true, Item = "Special item", Method = InventoryMethodEnum.Legacy ,Width = 12.5f });
            b1.Inventory.Add(new Inventory { Exterior = true, Uniformat = Formats.First(u => u.Code == "A202002"), Method = InventoryMethodEnum.Uniformat });
        }

        static void BuildManyActions(Work w)
        {
            w.Actions = new List<WorkAction>();
            var Workers = new List<string> { "Bob", "John", "Alex", "Carlos", "Julian" };
            var Descriptions = new List<string> { "Fixing the ceiling lamp", "Unclogged the sink", "Sprayed W40", "Replaced hinges", "Replacing furniture", "Removing broken item" };
            var Random = new Random();
            for (int i = 0; i < 40; i++)
            {
                w.Actions.Add(new WorkAction { Date = DateTime.Now.Date.AddDays(-Random.Next(30)), Worker = Workers[Random.Next(Workers.Count)], Description = Descriptions[Random.Next(Descriptions.Count)] });
            }
            //https://api.namefake.com/english-united-states
        }
        static async void LoadManyNames()
        {
            var Http = new HttpClient();
            var Random = new Random();
            for (int i = 0; i < 25; i++)
            {
                var s = await Http.GetStringAsync("https://api.namefake.com/english-united-states");
                var user = JsonSerializer.Deserialize<User>(s);
                using (var c = new ECContext())
                {
                    c.Workforce.Add(new Workforce { Name = user.Name, Type = (WorkforceTypeEnum)Random.Next(4) + 1 });
                    await c.SaveChangesAsync();
                }

            }
        }
        private class User
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
        }
        private static List<Uniformat> CreateUniformat(ECContext c)
        {
            var l = new List<Uniformat>();
            var Ext = new Uniformat { Code = "0", Value = "EXTERIOR", ValueCA = "EXTERIEUR" };
            Ext.Children = new List<Uniformat>();
            var Inf = new Uniformat { Code = "A", Value = "INFRASTRUCTURE", ValueCA = "INFRASTRUCTURE" };
            var InfFond = new Uniformat { Code = "A10", Value = "Foundations", ValueCA = "Fondations" };
            var InfSous = new Uniformat { Code = "A20", Value = "Underground constructions", ValueCA = "Construction de sous-sol" };
            Inf.Children = new List<Uniformat> { InfFond, InfSous };
            var DalleInf = new Uniformat { Code = "A1030", Value = "Lower slab", ValueCA = "Dalle inférieure" };
            DalleInf.Children =
                new List<Uniformat> { new Uniformat { Code ="A103001", ValueCA = "Murs de fondation et empattement", Value= "Foundation walls and footing" } ,
                                      new Uniformat { Code ="A103002", ValueCA = "Pilastres aux murs de fondation", Value= "Pilasters at the foundation walls" },
                                      new Uniformat { Code ="A103003", ValueCA = "Revêtement et finition extérieurs sur murs de fondations", Value= "Exterior coating and finishing on foundation walls" },
                                      new Uniformat { Code ="A103004", ValueCA = "Système de drainage des fondations", Value= "Foundation drainage system" },
                                      new Uniformat { Code ="A103005", ValueCA = "Pieux, micropieux, radiers", Value= "Piles, micropiles, foundation rafts" },
                                      new Uniformat { Code ="A103006", ValueCA = "Dalles sur sol", Value= "Ground slabs" },
                                      new Uniformat { Code ="A103007", ValueCA = "Fosses d’ascenseurs", Value= "Elevator pits" }};
            InfFond.Children = new List<Uniformat> { new Uniformat { Code="A1010" ,Value="Standard Foundation" , ValueCA="Fondation standard"},
                new Uniformat { Code ="A1020", Value="Special Foundation",ValueCA="Fondations spéciales" },DalleInf };
            var BaseConsturc = new Uniformat { Code = "A2020", ValueCA = "Construction en sous-sol", Value = "Basement constuction" };
            InfSous.Children = new List<Uniformat> { new Uniformat { Code = "A2010", ValueCA = "Excavation de sous-sol", Value = "Basement excavation" }, BaseConsturc };
            BaseConsturc.Children = new List<Uniformat>
            {
                new Uniformat { Code ="A202001", ValueCA = "Plancher de vide sanitaire", Value= "Crawl space floor" } ,
                new Uniformat { Code ="A202002", ValueCA = "Plafond de vide sanitaire", Value= "Crawl space ceiling" },
                new Uniformat { Code ="A202003", ValueCA = "Coupe-feu de vide sanitaire", Value= "Crawl space firestop" },
                new Uniformat { Code ="A202004", ValueCA = "???", Value= "???" },
                new Uniformat { Code ="A202005", ValueCA = "Isolation de murs de sous-sol et de vide sanitaire", Value= "Insulation of basement walls and crawl spaces" },
                new Uniformat { Code ="A202007", ValueCA = "Grille de ventilation de vide sanitaire", Value= "Crawl space ventilation" },
                new Uniformat { Code ="A202006", ValueCA = "Trappe d’accès de vide sanitaire", Value= "Crawl space access hatch" },
            };
            l.AddRange(Ext.Children);
            l.AddRange(DalleInf.Children);
            l.AddRange(BaseConsturc.Children);
            Ext.Children.Add(Inf);
            c.Uniformats.Add(Ext);
            return l;
        }
    }
}
