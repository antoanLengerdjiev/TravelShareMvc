using TravelShare.Data.Models;
using TravelShare.Web.Mappings;

namespace TravelShare.Web.Areas.Administration.Models
{
    public class SingleUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}