using API.Core;
using API.Entities;
using API.Entities.ViewModels;
using API.Infra;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<Gallery> _galleryRepository;
        private readonly ICacheService _cacheService;
        private readonly string keyForCache = "gallery";

        public GalleryService(IMapper mapper, IMongoRepository<Gallery> galleryRepository, ICacheService cacheService)
        {
            _mapper = mapper;
            _galleryRepository = galleryRepository;
            _cacheService = cacheService;
        }

        public List<GalleryViewModel> Get()
        {
            return _mapper.Map<List<GalleryViewModel>>(_galleryRepository.Get().ToList());
        }

        public GalleryViewModel Get(string id)
        {
            var keyCache = $"{keyForCache}/{id}";
            var gallery = _cacheService.Get<GalleryViewModel>(keyCache);

            if (gallery is null)
            {
                gallery = _mapper.Map<GalleryViewModel>(_galleryRepository.Get(id));
                _cacheService.Set(keyCache, gallery);
            }

            return gallery;
        }

        public GalleryViewModel GetBySlug(string slug)
        {
            var keyCache = $"{keyForCache}/{slug}";
            var gallery = _cacheService.Get<GalleryViewModel>(keyCache);

            if (gallery is null)
            {
                gallery = _mapper.Map<GalleryViewModel>(_galleryRepository.GetBySlug(slug));
                _cacheService.Set(keyCache, gallery);
            }

            return gallery;
        }

        public Result<GalleryViewModel> GetPagedSearch(int page, int qtd)
        {
            var keyCache = $"{keyForCache}/{page}/{qtd}";
            var gallery = _cacheService.Get<Result<GalleryViewModel>>(keyCache);

            if (gallery is null)
            {
                gallery = _mapper.Map<Result<GalleryViewModel>>(_galleryRepository.FindPagedSearch(page, qtd));
                _cacheService.Set(keyCache, gallery);
            }

            return gallery;
        }

        public GalleryViewModel Create(GalleryViewModel galleryEntrada)
        {
            var entity = new Gallery(galleryEntrada.Title, galleryEntrada.Legend, galleryEntrada.Author,
                                     galleryEntrada.Tags, galleryEntrada.Status, galleryEntrada.GalleryImages,
                                     galleryEntrada.Thumb);

            _galleryRepository.Create(entity);

            var keyCache = $"{keyForCache}/{entity.Slug}";
            _cacheService.Set(keyCache, entity);

            return Get(entity.Id);
        }

        public void Update(string id, GalleryViewModel galleryEntrada)
        {
            var keyCache = $"{keyForCache}/{id}";
            _galleryRepository.Update(id, _mapper.Map<Gallery>(galleryEntrada));

            _cacheService.Remove(keyCache);
            _cacheService.Set(keyCache, galleryEntrada);
        }

        public void Remove(string id)
        {
            var keyCache = $"{keyForCache}/{id}";
            _cacheService.Remove(keyCache);

            var galley = Get(id);
            keyCache = $"{keyForCache}/{galley.Slug}";
            _cacheService.Remove(keyCache);

            _galleryRepository.Remove(id);
        }
    }
}
