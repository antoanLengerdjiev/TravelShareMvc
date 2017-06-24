namespace TravelShare.Services.Data.Common.Contracts
{
    using System.Collections.Generic;
    using TravelShare.Data.Models;

    public interface IChatService
    {
        Chat Create(Trip trip);

        IEnumerable<Message> GetOlderMessages(int chatId, int skip, int take);
    }
}
