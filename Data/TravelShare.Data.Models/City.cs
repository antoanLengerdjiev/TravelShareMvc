namespace TravelShare.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TravelShare.Data.Models.Base;

    public class City : BaseModel<int>
    {
        public City()
        {
        }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
