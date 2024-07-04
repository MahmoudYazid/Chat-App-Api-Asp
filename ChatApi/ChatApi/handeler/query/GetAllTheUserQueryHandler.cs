using ChatApi.jwt;
using ChatApi.model.dbModel;
using ChatApi.model.query;
using MediatR;
using MongoDB.Driver;

namespace ChatApi.handeler.query
{
    public class GetAllTheUserQueryHandler : IRequestHandler<GetAllTheUsersQuery, IEnumerable<PersonDbModel>>
    {
        public MongoDbService MongoDbServiceInst { get; set; }


        public GetAllTheUserQueryHandler(MongoDbService mongoDbServiceInjected)
        {
            MongoDbServiceInst = mongoDbServiceInjected;
           

        }
        public async Task<IEnumerable<PersonDbModel>> Handle(GetAllTheUsersQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<PersonDbModel>.Filter.Empty;
            var search =  await MongoDbServiceInst.PersonDb.FindAsync(filter);
            var res = await search.ToListAsync();
            return res;
            
        }
    }
}
