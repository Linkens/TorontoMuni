namespace ExpertCities.Blazor
{
    public class SecretServices
    {
        public Dictionary<string, string> Secrets;
        public ConfigurationManager Configuration;
        public SecretServices()
        {
            Secrets = new Dictionary<string, string>();
        }
    }
}
