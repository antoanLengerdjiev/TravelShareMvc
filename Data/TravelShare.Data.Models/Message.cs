namespace TravelShare.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TravelShare.Data.Models.Base;

    public class Message : BaseModel<int>
    {
        [Required]
        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        [Index]
        [Required]
        public int ChatId { get; set; }

        public virtual Chat Chat { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Content { get; set; }
    }
}
