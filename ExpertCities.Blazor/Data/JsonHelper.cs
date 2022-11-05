using ExpertCities.Data;
using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ExpertCities.Blazor
{
    public class JsonHelper
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "geojson";
        [JsonPropertyName("data")]
        public FeatureCollection Data { get; set; } = new FeatureCollection();
    }
    public class FeatureCollection
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "FeatureCollection";
        [JsonPropertyName("features")]
        public List<Feature> Features { get; set; } = new List<Feature>();
    }
    public class Feature
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "Feature";
        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }
        [JsonPropertyName("properties")]
        public Properties Properties { get; set; }
        public Feature(Building Build, string BaseUrl, IStringLocalizer Loc)
        {
            Properties = new Properties(Build, BaseUrl, Loc);
            Geometry = new Geometry(Build);
        }
    }
    public class Geometry
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "Point";
        [JsonPropertyName("coordinates")]
        public float[] Coordinates { get; set; }
        public Geometry(Building Build)
        {
            Coordinates = new float[] { Build.Lat, Build.Long };
        }
    }
    public class Properties
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("icon")]
        public string Icon { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        public Properties(Building Build, string BaseUrl, IStringLocalizer Loc)
        {
            Title = Build.Denomination;
            Description = $"<a class=\"text-center\" target=\"_blank\" href=\"{BaseUrl}Assets/Building/{Build.ID}\">{Build.Denomination}</a> {Build.Description} <br/><table class=\"accent\" style=\"width:100%;\"><tr>" +
                $"<td>{GetA(BaseUrl, Build.ID.ToString(), "Inventory", Loc["Inventory"])}</td>" +
                $"<td>{GetA(BaseUrl, Build.ID.ToString(), "Inspections", Loc["Inspections"])}</td>" +
                $"<td>{GetA(BaseUrl, Build.ID.ToString(), "Works", Loc["Works"])}</td>" +
                      $"<td>{GetA(BaseUrl, Build.ID.ToString(), "LifeCycle", Loc["Life Cycle"])}</td>" +
                $"</tr></table>";
            Icon = "school-15";
        }
        protected string GetA(string BaseUrl, string ID, string Link, string Name)
        {
            return $"<a class=\"text-center link-light mx-1\" target=\"_blank\" href=\"{BaseUrl}Assets/Building/{ID}/{Link}\">{Name}</a>";
        }
    }

}
