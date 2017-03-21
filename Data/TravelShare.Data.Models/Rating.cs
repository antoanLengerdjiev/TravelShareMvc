using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelShare.Data.Models.Base;

namespace TravelShare.Data.Models
{
    public class Rating : BaseModel<int>
    {
        [Required]
        public string RecieverId { get; set; }

        [Required]
        [Range(1,10)]
        public int Points { get; set; }

        [Required]
        public string GiverId { get; set; }
    }
}
