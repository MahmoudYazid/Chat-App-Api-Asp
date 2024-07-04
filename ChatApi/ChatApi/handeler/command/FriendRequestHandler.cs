using ChatApi.model.command;
using ChatApi.model.dbModel;
using MediatR;
using System.Security.Claims;

namespace ChatApi.handeler.command
{
    public class FriendRequestHandler : IRequestHandler<AddFriendCommand, string>
    {
        public MongoDbService _MongoDbService { get; set; }
        public IHttpContextAccessor _IHttpContextAccessor { get; set; }
        public FriendRequestHandler (MongoDbService mongoDbServiceInjected,   IHttpContextAccessor httpContextAccessorInjected)
        {

            _MongoDbService = mongoDbServiceInjected;
            _IHttpContextAccessor = httpContextAccessorInjected;
        }

        public async Task<string> Handle(AddFriendCommand request, CancellationToken cancellationToken)
        {
            FriendsRelationDbModel friendsRelationDbModel = new FriendsRelationDbModel();

            var Claims = _IHttpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            friendsRelationDbModel.SenderId = Claims.Name.ToString();
            friendsRelationDbModel.RecieverId = request.RecieverId;
            friendsRelationDbModel.status = false;
            await _MongoDbService.friends.InsertOneAsync(friendsRelationDbModel);
            return "friend request added"; 
        }
    }
}
