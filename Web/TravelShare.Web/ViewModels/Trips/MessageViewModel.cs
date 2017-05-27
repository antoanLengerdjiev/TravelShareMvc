using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelShare.Web.Mappings;
using TravelShare.Data.Models;

namespace TravelShare.Web.ViewModels.Trips
{
    public class MessageViewModel : IMapFrom<Message>
    {
        public string Content { get; set; }

        public UserViewModel Sender { get; set; }
    }
}