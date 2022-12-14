namespace API.Infra
{
    public interface IMongoRepository<TEntity>
    {
        List<TEntity> Get();
        TEntity Get(string id);
        TEntity Create(TEntity entity);
        void Update(string id, TEntity entity);
        void Remove(string id);
    }
}
