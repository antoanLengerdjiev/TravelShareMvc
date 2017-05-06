using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelShare.Data.Models;
using TravelShare.Web.Mappings;

namespace TravelShare.Web.Areas.Administration.Models.NewsManagement
{
    public class SingleNewsModel : IMapFrom<News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}