using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                var b = new Building { Category = BuildCatEnum.School, Denomination = "Ecole", Structure = BuildStructEnum.Concrete };
                b.Lat = -74.60503901404996f; b.Long = 45.60859520641057f;
                b.Country = "Canada"; b.City = "Toronto"; b.CivicNumber = "15665421-4"; b.Telephone = "+4523884716"; b.Val_Acquire = 1500000; b.Length = 45; b.Width = 50; b.Shape = BuildShapeEnum.Square; b.Date_Acquire = new DateTime(1970, 10, 5); b.Date_Commission = new DateTime(1970, 10, 5);
                var w = new Work { State = WorkStateEnum.WorkOrder | WorkStateEnum.Completed, Building = b, WorkOrderDate = new DateTime(2022, 8, 12), WorkCompleted = new DateTime(2022, 8, 27), IsInternal = true, Summary = "Autumn maintenance" };
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
                c.Workforce.Add(new Workforce { Name = "Carlos", Type = WorkforceTypeEnum.InspectionAgent });
                c.Workforce.Add(new Workforce { Name = "Julian", Type = WorkforceTypeEnum.InspectionAgent });
                c.Workforce.Add(new Workforce { Name = "Alex", Type = WorkforceTypeEnum.InspectionAgent });
                b = new Building { Category = BuildCatEnum.Residential, Denomination = "Résidence", Structure = BuildStructEnum.Concrete };
                b.Lat = -74.61084084623728f; b.Long = 45.60228250701617f;
                c.Buildings.Add(b);
                c.SaveChanges();
            }
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

        }
    }
}
