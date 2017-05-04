using System;
using TravelShare.Data.Models;
using TravelShare.Web.Mappings;

namespace TravelShare.Web.ViewModels.Home
{
    public class NewsViewModel : IMapFrom<News>
    {

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}