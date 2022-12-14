namespace API.Infra
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
