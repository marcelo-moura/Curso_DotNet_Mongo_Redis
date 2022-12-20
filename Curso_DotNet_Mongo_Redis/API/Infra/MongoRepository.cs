using API.Entities;
using MongoDB.Driver;

namespace API.Infra
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IMongoCollection<TEntity> _model;

        public MongoRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _model = database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower());
        }

        public List<TEntity> Get()
        {
            return _model.Find<TEntity>(x => x.Deleted == false).ToList();
        }

        public TEntity Get(string id)
        {
            return _model.Find<TEntity>(x => x.Id == id && x.Deleted == false).FirstOrDefault();
        }

        public TEntity GetBySlug(string slug)
        {
            return _model.Find<TEntity>(x => x.Slug == slug && x.Deleted == false).FirstOrDefault();
        }

        public TEntity Create(TEntity news)
        {
            _model.InsertOne(news);
            return news;
        }

        public void Update(string id, TEntity entity)
        {
            _model.ReplaceOne(x => x.Id == id, entity);
        }

        public void Remove(string id)
        {
            var entity = Get(id);
            entity.Deleted = true;
            _model.ReplaceOne(x => x.Id == id, entity);
        }
    }
}
