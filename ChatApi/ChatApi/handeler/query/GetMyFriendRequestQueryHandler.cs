using ChatApi.model.dbModel;
using ChatApi.model.query;
using MediatR;
using MongoDB.Driver;
using System.Security.Claims;

namespace ChatApi.handeler.query
{
    public class GetMyFriendRequestQueryHandler : IRequestHandler<GetMyFriendRequestQuery, IEnumerable<FriendsRelationDbModel>>
    {
        public MongoDbService MongoDbServiceInst { get; set; }
        public IHttpContextAccessor _IHttpContextAccessor { get; set; }

        public GetMyFriendRequestQueryHandler (MongoDbService mongoDbServiceInjected , IHttpContextAccessor httpContextAccessor)
        {
            MongoDbServiceInst = mongoDbServiceInjected;
            _IHttpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<FriendsRelationDbModel>> Handle(GetMyFriendRequestQuery request, CancellationToken cancellationToken)
        {
            var Claims = _IHttpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;


            /// get all friends Ids
            var filter = Builders<FriendsRelationDbModel>.Filter.Or(
                Builders<FriendsRelationDbModel>.Filter.And(
                    Builders<FriendsRelationDbModel>.Filter.Eq("SenderId", Claims.Name.ToString()),
                    Builders<FriendsRelationDbModel>.Filter.Eq("status", false)
                    )
                ,

                     Builders<FriendsRelationDbModel>.Filter.And(
                    Builders<FriendsRelationDbModel>.Filter.Eq("status", false),
                    Builders<FriendsRelationDbModel>.Filter.Eq("RecieverId", Claims.Name.ToString())
                    )
                );

            var query = await MongoDbServiceInst.friends.FindAsync(filter);
            var resultOfIds = await query.ToListAsync();

            return resultOfIds ;



        }
    }
}
