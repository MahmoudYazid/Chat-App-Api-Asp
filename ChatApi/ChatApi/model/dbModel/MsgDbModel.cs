using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatApi.model.dbModel
{
    public class MsgDbModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string senderId { get; set; }
        public string recieverId { get; set; }
        
        public string TextMsg { get; set; }

        public string VideoMsg { get; set; } = "";
        public DateTime date { get; set; } = DateTime.Now;

    }
}
