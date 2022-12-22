using API.Core;
using API.Entities.ViewModels;

namespace API.Services.Interfaces
{
    public interface INewsService
    {
        public List<NewsViewModel> Get();
        public NewsViewModel Get(string id);
        public NewsViewModel GetBySlug(string slug);
        public Result<NewsViewModel> GetPagedSearch(int page, int qtd);
        public NewsViewModel Create(NewsViewModel newsEntrada);
        public void Update(string id, NewsViewModel newsEntrada);
        public void Remove(string id);
    }
}
