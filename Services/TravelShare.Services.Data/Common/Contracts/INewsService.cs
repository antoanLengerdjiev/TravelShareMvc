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
        News GetById(int id);

        void Create(News news);

        void Delete(News news);

        IEnumerable<News> GetLastestNews(int numberOfNews);

        IEnumerable<News> SearchNews(string searchPattern, string searchBy, int page, int perPage);

        int GetSearchNewsPageCount(string searchPattern, string searchBy, int perPage);
    }
}
