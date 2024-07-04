using ChatApi.model.dbModel;
using ChatApi.model.query;
using MediatR;
using MongoDB.Driver;
using System.Security.Claims;

namespace ChatApi.handeler.query
{
    public class GetMyFriendQueryHandler : IRequestHandler<GetMyFriendQuery, IEnumerable<PersonDbModel>>
    {
        public MongoDbService _MongoDbService { get; set; }
        public IHttpContextAccessor _IHttpContextAccessor { get; set; }
        public GetMyFriendQueryHandler(MongoDbService mongoDbServiceInjected, IHttpContextAccessor httpContextAccessorInjected)
        {

            _MongoDbService = mongoDbServiceInjected;
            _IHttpContextAccessor = httpContextAccessorInjected;
        }
        public async Task<IEnumerable<PersonDbModel>> Handle(GetMyFriendQuery request, CancellationToken cancellationToken)
        {
            var Claims = _IHttpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            

            /// get all friends Ids
            var filter = Builders<FriendsRelationDbModel>.Filter.Or(
                Builders<FriendsRelationDbModel>.Filter.And(
                    Builders<FriendsRelationDbModel>.Filter.Eq("SenderId", Claims.Name.ToString()),
                    Builders<FriendsRelationDbModel>.Filter.Eq("status", true)
                    )
                ,

                     Builders<FriendsRelationDbModel>.Filter.And(
                    Builders<FriendsRelationDbModel>.Filter.Eq("status", true),
                    Builders<FriendsRelationDbModel>.Filter.Eq("RecieverId", Claims.Name.ToString())
                    )
                );

            var query = await _MongoDbService.friends.FindAsync(filter);
            var resultOfIds = await query.ToListAsync();
    
            var friendIds = resultOfIds.Select(f => f.SenderId == Claims.Name.ToString() ? f.RecieverId : f.SenderId).ToList();
            // get data of these ids
            
            var FilterForIds = Builders<PersonDbModel>.Filter.In(p=> p.Id, friendIds);

            var findFriends = await _MongoDbService.PersonDb.FindAsync(FilterForIds);
            var finalResult = await findFriends.ToListAsync();

            return finalResult;





        }
    }
}
