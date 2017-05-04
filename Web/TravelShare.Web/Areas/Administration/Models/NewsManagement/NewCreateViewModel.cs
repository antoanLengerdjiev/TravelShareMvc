namespace TravelShare.Web.Areas.Administration.Models.NewsManagement
{
    using System.ComponentModel.DataAnnotations;
    using TravelShare.Data.Models;
    using TravelShare.Web.Mappings;

    public class NewCreateViewModel : IMapFrom<News>
    {
        [Required]
        [StringLength(25, ErrorMessage = "Title must be between 5 and 25", MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [StringLength(255, ErrorMessage ="Content must be between 10 and 255", MinimumLength = 10)]
        public string Content { get; set; }
    }
}