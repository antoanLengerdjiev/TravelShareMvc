using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TravelShare.Data.Models.Base;

namespace TravelShare.Data.Models
{
    public class Chat : BaseModel<int>
    {
        public Chat()
        {
            this.Messages = new HashSet<Message>();
        }

        [Required]
        public int TripId { get; set; }

        [Required]
        public virtual Trip Trip { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
