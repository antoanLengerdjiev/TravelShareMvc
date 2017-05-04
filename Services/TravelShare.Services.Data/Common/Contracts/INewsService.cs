using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelShare.Data.Models;

namespace TravelShare.Services.Data.Common.Contracts
{
    public interface INewsService
    {

        void Create(News news);

        IEnumerable<News> GetLastestNews(int numberOfNews);
    }
}
