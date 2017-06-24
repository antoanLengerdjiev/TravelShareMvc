namespace TravelShare.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Bytes2you.Validation;
    using TravelShare.Common;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data.Common.Contracts;

    public class ChatService : IChatService
    {
        private readonly IEfDbRepository<Chat> chatRepository;

        private readonly IApplicationDbContextSaveChanges dbSaveChanges;

        public ChatService(IEfDbRepository<Chat> chatRepository, IApplicationDbContextSaveChanges dbSaveChanges)
        {
            Guard.WhenArgument(chatRepository, GlobalConstants.ChatRepositoryNullExceptionMessage).IsNull().Throw();
            Guard.WhenArgument<IApplicationDbContextSaveChanges>(dbSaveChanges, GlobalConstants.DbContextSaveChangesNullExceptionMessage)
               .IsNull()
               .Throw();

            this.chatRepository = chatRepository;
            this.dbSaveChanges = dbSaveChanges;
        }

        public Chat Create(Trip trip)
        {
            var newChat = new Chat() { TripId = trip.Id, Trip = trip };
            this.chatRepository.Add(newChat);
            this.dbSaveChanges.SaveChanges();
            return newChat;
        }

        public IEnumerable<Message> GetOlderMessages(int chatId, int skip, int take)
        {
            return this.chatRepository.GetById(chatId).Messages.OrderByDescending(x => x.CreatedOn)
                .Skip(skip)
                .Take(take)
                .OrderBy(x => x.CreatedOn)
                .ToList();
        }
    }
}
