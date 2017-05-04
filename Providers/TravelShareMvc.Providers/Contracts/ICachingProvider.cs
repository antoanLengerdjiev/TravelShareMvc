using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelShareMvc.Providers.Contracts
{
    public interface ICachingProvider
    {
        void AddItem(string key, object value, DateTime expirationDateTime);

        void InsertItem(string key, object value);

        object GetItem(string key);
    }
}
