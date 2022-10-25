using ExpertCities.Data;

namespace ExpertCities.Blazor
{
    public class MemoryFileManager : IRetrieveFile
    {
        protected Dictionary<Guid, byte[]> _files = new Dictionary<Guid, byte[]>();
        public async Task<byte[]> GetFile(HostedFile File)
        {
            if (Guid.TryParse(File.ProviderID, out Guid ProviderID) && _files.ContainsKey(ProviderID))
            {
                return _files[ProviderID];
            }
            else return null;
        }

        public async Task<string> SaveFile(byte[] Content)
        {
            var id = Guid.NewGuid();
            _files.Add(id, Content);
            return id.ToString();
        }
    }
}
