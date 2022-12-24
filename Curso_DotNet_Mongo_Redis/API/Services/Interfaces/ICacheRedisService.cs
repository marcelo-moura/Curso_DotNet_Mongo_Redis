namespace API.Services.Interfaces
{
    public interface ICacheRedisService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
        void Remove(string key);
    }
}
