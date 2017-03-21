namespace TravelShare.Data.Models
{
    using System;
    using TravelShare.Data.Models.Base;

    public class News : BaseModel<int>
    {
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
