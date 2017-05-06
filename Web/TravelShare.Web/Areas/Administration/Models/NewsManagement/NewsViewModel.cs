using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelShare.Web.Areas.Administration.Models.NewsManagement
{
    public class NewsViewModel
    {
        public IEnumerable<SingleNewsModel> News { get; set; }

        public int CurrentPage { get; set; }

        public int NewsCount { get; set; }

        public int Pages { get; set; }

        public SearchNewsModel SearchModel { get; set; }
    }
}