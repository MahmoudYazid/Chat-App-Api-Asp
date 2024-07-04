using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatApi.model.dbModel
{
    public class PersonDbModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string password { get; set; }


    }
}
