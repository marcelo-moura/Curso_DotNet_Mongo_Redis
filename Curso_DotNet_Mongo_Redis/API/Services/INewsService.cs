using API.Entities.ViewModels;

namespace API.Services
{
    public interface INewsService
    {
        public List<NewsViewModel> Get();
        public NewsViewModel Get(string id);
        public NewsViewModel Create(NewsViewModel newsEntrada);
        public void Update(string id, NewsViewModel newsEntrada);
        public void Remove(string id);
    }
}
