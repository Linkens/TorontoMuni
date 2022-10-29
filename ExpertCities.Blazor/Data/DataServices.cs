using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
using System.Security.Claims;
namespace ExpertCities.Blazor
{
    public class DataServices
    {

        public event EventHandler StateHasChanged;
        public event EventHandler Critical;
        public bool IsLoaded { get; set; }
        public bool IsBusy { get; set; }
        protected bool _IsLoading;
        public bool IsLoading { get => _IsLoading; protected set { _IsLoading = value; StateHasChanged?.Invoke(this, EventArgs.Empty); } }
        public string LoadingText { get; set; }
        public bool IsLogged { get; set; }

        public DataServices()
        {
        }
        public void SetBusy(string Message = "Chargement des données ...")
        {
            LoadingText = Message;
            IsBusy = true;
        }
        public void EndBusy()
        {
            IsBusy = false;
        }
        public async Task Initialize()
        {
            if (IsLoaded || IsLoading) return;
            LoadingText = "Chargement de la base de donnée";
            IsLoading = true;

            StateHasChanged?.Invoke(this, EventArgs.Empty);

            IsLoading = false;
            StateHasChanged?.Invoke(this, EventArgs.Empty);

        }
        public async Task IsInitialized()
        {
            _ = Initialize();
            while (IsLoading)
            {
                await Task.Delay(200);
            }
        }
    }
}
