using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelShare.Data.Models;
using TravelShare.Web.Infrastructure.Mapping;

namespace TravelShare.Web.ViewModels
{
    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }
    }
}