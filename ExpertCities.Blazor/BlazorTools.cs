using Microsoft.JSInterop;
using System.Reflection.Emit;

namespace ExpertCities
{
    public static class BlazorTools
    {
        public static async Task SaveAny(this IJSRuntime JS, string Data64, string MimeType, string Filename)
        {
            await JS.InvokeVoidAsync("AnyFileSaveAs", Data64, MimeType, Filename);
        }
        public static async Task MapBox_SignIn(this IJSRuntime JS, string Token, float[] Center, float Zoom = 12.5f)
        {
            await JS.InvokeVoidAsync("SignInMapBox", Token, Center, Zoom);
        }
    }
}
