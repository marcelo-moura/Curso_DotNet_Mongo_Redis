using API.Core;

namespace API.Infra
{
    public interface IMongoRepository<TEntity>
    {
        List<TEntity> Get();
        TEntity Get(string id);
        TEntity GetBySlug(string slug);
        Result<TEntity> FindPagedSearch(int page, int qtd);
        TEntity Create(TEntity entity);        
        void Update(string id, TEntity entity);
        void Remove(string id);
    }
}
