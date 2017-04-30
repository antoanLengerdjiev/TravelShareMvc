namespace TravelShare.Web.ViewModels
{
    using Mappings;
    using TravelShare.Data.Models;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}