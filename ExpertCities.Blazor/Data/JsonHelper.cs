using ExpertCities.Data;
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
        public Feature(Building Build, string BaseUrl)
        {
            Properties = new Properties(Build, BaseUrl);
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
        public Properties(Building Build, string BaseUrl)
        {
            Title = Build.Denomination;
            Description = $"<b>{Build.Denomination}</b><a href=\"{BaseUrl}Assets/Building/{Build.ID}\">{Build.Denomination}</a> {Build.Description}";
            Icon = "school-15";
        }
    }

}
