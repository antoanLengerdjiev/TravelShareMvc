namespace TravelShare.Web.Hubs
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Autofac;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using Services.Data.Common.Contracts;

    public class Chat : Hub
    {
        private static Dictionary<int, List<string>> connectUserToChat = new Dictionary<int, List<string>>();
        private static Dictionary<string, string> usersChat = new Dictionary<string, string>();

        private readonly ITripService tripService;
        private readonly IMessageService messageService;
        private readonly ILifetimeScope hubLifetimeScope;

        public Chat()
        {

        }

        public Chat(ILifetimeScope lifetimeScope)
        {
            this.hubLifetimeScope = lifetimeScope.BeginLifetimeScope();
            this.tripService = this.hubLifetimeScope.Resolve<ITripService>();
            this.messageService = this.hubLifetimeScope.Resolve<IMessageService>();
        }

        public void JoinRoom(string room)
        {
            var username = this.Context.User.Identity.Name;

            var tripId = int.Parse(room);
            var trip = this.tripService.GetById(tripId);

            var isPassenger = trip.Passengers.Select(x => x.Email).ToList().Contains(username);

            if (trip.Driver.Email == username || isPassenger)
            {
                if (!connectUserToChat.ContainsKey(tripId))
                {
                    connectUserToChat.Add(tripId, new List<string> { username });
                }
                else
                {
                    connectUserToChat[tripId].Add(username);
                }

                this.Groups.Add(this.Context.ConnectionId, room);
                usersChat.Add(this.Context.ConnectionId, room);
                foreach (var item in connectUserToChat[tripId])
                {
                    this.Clients.Caller.connectUser(item);
                }

                this.Clients.Group(room, this.Context.ConnectionId).connectUser(username);
                this.Clients.Caller.joinRoom();
            }
        }

        public void SendMessageToRoom(string message, string room)
        {
            var tripId = int.Parse(room);
            var senderId = this.Context.User.Identity.GetUserId();
            var messageToBeAdded = new Data.Models.Message() { SenderId = senderId,  Content = message, TripId = tripId };
            this.messageService.Create(messageToBeAdded);
            var msg = message;
            var sender = this.Context.User.Identity.Name;
            this.Clients.Group(room).addMessage(msg, sender);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var room = usersChat[this.Context.ConnectionId];
            this.Groups.Remove(this.Context.ConnectionId, room);
            connectUserToChat[int.Parse(room)].Remove(this.Context.User.Identity.Name);
            this.Clients.Group(room).disconnectUser(this.Context.User.Identity.Name);
            return base.OnDisconnected(stopCalled);
        }

        protected override void Dispose(bool disposing)
        {
            // Dipose the hub lifetime scope when the hub is disposed.
            if (disposing && this.hubLifetimeScope != null)
            {
                this.hubLifetimeScope.Dispose();
            }

            base.Dispose(disposing);
        }

    }
}