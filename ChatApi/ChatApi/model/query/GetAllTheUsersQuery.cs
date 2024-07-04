using ChatApi.model.dbModel;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ChatApi.model.query
{
    public class GetAllTheUsersQuery : IRequest<IEnumerable<PersonDbModel>>
    {


        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string password { get; set; }
    }
}
