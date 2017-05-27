namespace TravelShare.Services.Data.Common.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TravelShare.Data.Models;

    public interface IMessageService
    {
        void Create(Message message);

        IEnumerable<Message> GetOlderMessages(int tripId, int skip, int take);
    }
}
