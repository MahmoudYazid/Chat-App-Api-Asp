using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatApi.model.dbModel
{
    public class FriendsRelationDbModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string SenderId{ get; set; }
        public string RecieverId{ get; set; }
        public bool status { get; set; }

    }
}
