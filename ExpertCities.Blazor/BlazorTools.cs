using ExpertCities.Blazor;
using ExpertCities.Data;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.Reflection.Emit;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExpertCities
{
    public static class BlazorTools
    {
        public static async Task SaveAny(this IJSRuntime JS, string Data64, string MimeType, string Filename)
        {
            await JS.InvokeVoidAsync("AnyFileSaveAs", Data64, MimeType, Filename);
        }
        public static async Task MapBox_SignIn(this IJSRuntime JS, string Token, float[] Center,  float Zoom = 12.5f)
        {
       
            await JS.InvokeVoidAsync("SignInMapBox", Token, Center, Zoom);
        }
        public static async Task AddLocation(this IJSRuntime JS, List<Asset> Buildings, string BaseUrl, IStringLocalizer Loc)
        {
            var Helper = new JsonHelper();
            foreach (var item in Buildings)
            {
                Helper.Data.Features.Add(new Feature(item, BaseUrl, Loc));
            }
            var Building = JsonSerializer.Serialize(Helper);
            await JS.InvokeVoidAsync("AddBuilding", Helper);
        }


    }
}

