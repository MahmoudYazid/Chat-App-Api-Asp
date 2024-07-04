using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatApi.model.dbModel
{
    public class MongoDbService
    {
        public readonly IMongoCollection<PersonDbModel> PersonDb ;
        public readonly IMongoCollection<MsgDbModel> MsgsDb;
        public readonly IMongoCollection<FriendsRelationDbModel> friends;
        public MongoDbService(IOptions<DbDataClass> options) {
            MongoClient client = new MongoClient(options.Value.DbConnectionString);
            IMongoDatabase mongoDatabase = client.GetDatabase(options.Value.DatabaseName);
            PersonDb = mongoDatabase.GetCollection<PersonDbModel>(options.Value.CollectionName);
            MsgsDb = mongoDatabase.GetCollection<MsgDbModel>("msgsCollection");
            friends = mongoDatabase.GetCollection<FriendsRelationDbModel>("friends");
        }
    }
}
