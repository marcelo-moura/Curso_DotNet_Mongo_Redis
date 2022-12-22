using API.Core;
using API.Entities.ViewModels;

namespace API.Services.Interfaces
{
    public interface IGalleryService
    {
        public List<GalleryViewModel> Get();
        public GalleryViewModel Get(string id);
        public GalleryViewModel GetBySlug(string slug);
        public Result<GalleryViewModel> GetPagedSearch(int page, int qtd);
        public GalleryViewModel Create(GalleryViewModel galleryEntrada);
        public void Update(string id, GalleryViewModel galleryEntrada);
        public void Remove(string id);
    }
}
