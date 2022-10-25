using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCities.Data
{
    public interface IRetrieveFile
    {
        public Task<byte[]> GetFile(HostedFile File);

        public Task<string> SaveFile(byte[] Content);

    }
}
