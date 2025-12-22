using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TricolorSonda.Api.Models;

namespace TricolorSonda.Api.Infra
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Transfers> MercadoPagoEvents => _database.GetCollection<Transfers>("transfers");
    }
}
