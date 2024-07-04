using ChatApi.model.dbModel;
using ChatApi.model.query;
using MediatR;
using MongoDB.Driver;

namespace ChatApi.handeler.query
{
    public class GetSpecificChatHandler : IRequestHandler<GetCertainChatQuery, IEnumerable<MsgDbModel>>
    {
        public MongoDbService MongoDbServiceInst { get; set; }


        public GetSpecificChatHandler(MongoDbService mongoDbServiceInjected)
        {
            MongoDbServiceInst = mongoDbServiceInjected;


        }
        public async Task<IEnumerable<MsgDbModel>> Handle(GetCertainChatQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<MsgDbModel>.Filter.Or(
                Builders<MsgDbModel>.Filter.And(
                    Builders<MsgDbModel>.Filter.Eq("senderId", request.SenderId),
                    Builders<MsgDbModel>.Filter.Eq("recieverId", request.RecieverId)
                    )
                ,

                     Builders<MsgDbModel>.Filter.And(
                    Builders<MsgDbModel>.Filter.Eq("senderId", request.RecieverId),
                    Builders<MsgDbModel>.Filter.Eq("recieverId", request.SenderId)
                    )
                );
            var sortFilter = Builders<MsgDbModel>.Sort.Ascending("date");
            var query=  MongoDbServiceInst.MsgsDb.Find(filter).Sort(sortFilter);
            var res = await query.ToListAsync();

            return res;


        }
    }
}
