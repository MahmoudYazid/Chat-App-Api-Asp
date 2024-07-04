using ChatApi.model.dbModel;
using MediatR;

namespace ChatApi.model.query
{
    public class GetMyFriendRequestQuery : IRequest<IEnumerable<FriendsRelationDbModel>>
    {
        public string MyId { get; set; }
    }
}
