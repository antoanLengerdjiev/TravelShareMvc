using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelShare.Data.Models.Base;

namespace TravelShar.Data.Tests
{
    public class MockedModel : BaseModel<int>, IAuditInfo, IDeletableEntity
    {
        public string Name { get; set; }
    }
}
