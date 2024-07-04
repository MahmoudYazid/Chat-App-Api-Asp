using ChatApi.model.command;
using ChatApi.model.dbModel;
using MediatR;
using MongoDB.Driver;

namespace ChatApi.handeler.command
{
    public class AcceptFriendRequestHandler : IRequestHandler<AcceptFriendRequestCommand, string>
    {
        public MongoDbService _MongoDbService { get; set; }
        public AcceptFriendRequestHandler (MongoDbService mongoDbServiceInjected)
        {

            _MongoDbService = mongoDbServiceInjected;
        }

        public async Task<string> Handle(AcceptFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<FriendsRelationDbModel>.Filter.Eq("Id", request.RequestId);
            var update = Builders<FriendsRelationDbModel>.Update.Set(m => m.status, true);
            var result = await _MongoDbService.friends.UpdateOneAsync(filter,update);

            if (result.MatchedCount == 0)
            {
                return "Document not found." ;
            }

            return "Document updated successfully." ;



        }
    }
}
