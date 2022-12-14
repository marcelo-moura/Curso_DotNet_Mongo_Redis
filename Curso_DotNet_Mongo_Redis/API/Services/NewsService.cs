using API.Entities;
using API.Entities.ViewModels;
using API.Infra;
using AutoMapper;

namespace API.Services
{
    public class NewsService : INewsService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<News> _newsRepository;

        public NewsService(IMapper mapper, IMongoRepository<News> newsRepository)
        {
            _mapper = mapper;
            _newsRepository = newsRepository;
        }

        public List<NewsViewModel> Get()
        {
            return _mapper.Map<List<NewsViewModel>>(_newsRepository.Get().ToList());
        }

        public NewsViewModel Get(string id)
        {
            return _mapper.Map<NewsViewModel>(_newsRepository.Get(id));
        }

        public NewsViewModel Create(NewsViewModel newsEntrada)
        {
            var entity = new News(newsEntrada.Hat, newsEntrada.Title, newsEntrada.Text,
                                  newsEntrada.Author, newsEntrada.Img, newsEntrada.Link, newsEntrada.Status);

            _newsRepository.Create(entity);
            return Get(entity.Id);
        }

        public void Update(string id, NewsViewModel newsEntrada)
        {
            _newsRepository.Update(id, _mapper.Map<News>(newsEntrada));
        }

        public void Remove(string id)
        {
            _newsRepository.Remove(id);
        }
    }
}
