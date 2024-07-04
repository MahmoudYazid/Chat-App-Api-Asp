using ChatApi.model.command;
using ChatApi.model.dbModel;
using MediatR;
using MongoDB.Driver;

namespace ChatApi.handeler.command
{
    public class RefuseFriendRequestHandler : IRequestHandler<RefuseFriendRequestCammand, string>
    {
        public MongoDbService _MongoDbService { get; set; }
        public RefuseFriendRequestHandler(MongoDbService mongoDbServiceInjected)
        {

            _MongoDbService = mongoDbServiceInjected;
        }
        public async Task<string> Handle(RefuseFriendRequestCammand request, CancellationToken cancellationToken)
        {
            var Filters = Builders<FriendsRelationDbModel>.Filter.Eq("Id", request.RequestId);

            var command = await _MongoDbService.friends.FindOneAndDeleteAsync(Filters);
            if (command != null)
            {
                return "msg has been deleted";

            }
            else
            {
                return "Msg not deleted";
            };
        }
    }
}
