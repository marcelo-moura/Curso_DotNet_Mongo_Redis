using API.Core;
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

        public Result<TEntity> FindPagedSearch(int page, int qtd)
        {
            var result = new Result<TEntity>();
            result.Page = page;
            result.Qtd = qtd;

            var filter = Builders<TEntity>.Filter.Eq(x => x.Deleted, false);

            result.Data = _model.Find(filter)
                                .SortByDescending(x => x.PublishDate)
                                .Skip((page - 1) * qtd)
                                .Limit(qtd)
                                .ToList();

            result.Total = _model.CountDocuments(filter);
            result.TotalPages = result.Total / qtd;

            return result;
        }

        public TEntity Create(TEntity entity)
        {
            _model.InsertOne(entity);
            return entity;
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
