using API.Core;
using API.Entities;
using API.Entities.ViewModels;
using API.Infra;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class NewsService : INewsService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<News> _newsRepository;
        private readonly ICacheRedisService _cacheRedisService;
        private readonly string keyForCache = "news";

        public NewsService(IMapper mapper, IMongoRepository<News> newsRepository, ICacheRedisService cacheRedisService)
        {
            _mapper = mapper;
            _newsRepository = newsRepository;
            _cacheRedisService = cacheRedisService;
        }

        public List<NewsViewModel> Get()
        {
            return _mapper.Map<List<NewsViewModel>>(_newsRepository.Get().ToList());
        }

        public NewsViewModel Get(string id)
        {
            var keyCache = $"{keyForCache}/{id}";
            var news = _cacheRedisService.Get<NewsViewModel>(keyCache);

            if (news is null)
            {
                news = _mapper.Map<NewsViewModel>(_newsRepository.Get(id));
                _cacheRedisService.Set(keyCache, news);
            }

            return news;
        }

        public NewsViewModel GetBySlug(string slug)
        {
            var keyCache = $"{keyForCache}/{slug}";
            var news = _cacheRedisService.Get<NewsViewModel>(keyCache);

            if (news is null)
            {
                news = _mapper.Map<NewsViewModel>(_newsRepository.GetBySlug(slug));
                _cacheRedisService.Set(keyCache, news);
            }

            return news;
        }

        public Result<NewsViewModel> GetPagedSearch(int page, int qtd)
        {
            var keyCache = $"{keyForCache}/{page}/{qtd}";
            var news = _cacheRedisService.Get<Result<NewsViewModel>>(keyCache);

            if (news is null)
            {
                news = _mapper.Map<Result<NewsViewModel>>(_newsRepository.FindPagedSearch(page, qtd));
                _cacheRedisService.Set(keyCache, news);
            }

            return news;
        }

        public NewsViewModel Create(NewsViewModel newsEntrada)
        {
            var entity = new News(newsEntrada.Hat, newsEntrada.Title, newsEntrada.Text,
                                  newsEntrada.Author, newsEntrada.Img, newsEntrada.Status);

            _newsRepository.Create(entity);

            var keyCache = $"{keyForCache}/{entity.Slug}";
            _cacheRedisService.Set(keyCache, entity);

            return Get(entity.Id);
        }

        public void Update(string id, NewsViewModel newsEntrada)
        {
            var keyCache = $"{keyForCache}/{id}";
            _newsRepository.Update(id, _mapper.Map<News>(newsEntrada));

            _cacheRedisService.Remove(keyCache);
            _cacheRedisService.Set(keyCache, newsEntrada);
        }

        public void Remove(string id)
        {
            var keyCache = $"{keyForCache}/{id}";
            _cacheRedisService.Remove(keyCache);

            var galley = Get(id);
            keyCache = $"{keyForCache}/{galley.Slug}";
            _cacheRedisService.Remove(keyCache);

            _newsRepository.Remove(id);
        }
    }
}
