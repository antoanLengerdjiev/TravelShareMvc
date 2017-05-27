namespace TravelShare.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bytes2you.Validation;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data.Common.Contracts;

    public class MessageService : IMessageService
    {
        private readonly IEfDbRepository<Message> messageRepository;

        private readonly IApplicationDbContextSaveChanges dbSaveChanges;

        public MessageService(IEfDbRepository<Message> messageRepository, IApplicationDbContextSaveChanges dbSaveChanges)
        {
            Guard.WhenArgument(messageRepository, "Message repository cannot be null").IsNull().Throw();
            Guard.WhenArgument<IApplicationDbContextSaveChanges>(dbSaveChanges, "DbContext cannot be null.")
               .IsNull()
               .Throw();

            this.messageRepository = messageRepository;
            this.dbSaveChanges = dbSaveChanges;

        }

        public void Create(Message message)
        {
            this.messageRepository.Add(message);
            this.dbSaveChanges.SaveChanges();
        }

        public IEnumerable<Message> GetOlderMessages(int tripId, int skip, int take)
        {
           return this.messageRepository.All().Where(x => x.TripId == tripId)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip)
                .Take(take)
                .OrderBy(x => x.CreatedOn)
                .ToList();
        }
    }
}
