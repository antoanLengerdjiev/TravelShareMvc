using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelShare.Data.Models;

namespace TravelShare.Data.Common.Contracts
{
    public interface INewsRepository : IDbRepository<News>
    {
    }
}
