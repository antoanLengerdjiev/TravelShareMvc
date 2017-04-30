using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelShare.Web.Areas.Administration.Models
{
    public class UsersViewModel
    {
        public IEnumerable<SingleUserViewModel> Users { get; set; }

        public int UsersCount { get; set; }

        public int Pages { get; set; }

        public int Page { get; set; }

        public SearchModel SearchModel { get; set; }
    }
}