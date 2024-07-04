using ChatApi.model.command;
using ChatApi.model.dbModel;
using MediatR;
using MongoDB.Driver;

namespace ChatApi.handeler.command
{
    public class DeleteMsgCommandHandler : IRequestHandler<DeleteMsgCommand, string>
    {
        public MongoDbService _MongoDbService { get; set; }
        public DeleteMsgCommandHandler(MongoDbService mongoDbServiceInjected)
        {

            _MongoDbService = mongoDbServiceInjected;
        }
        public async Task<string> Handle(DeleteMsgCommand request, CancellationToken cancellationToken)
        {
            var Filters = Builders<MsgDbModel>.Filter.Eq("Id", request.MsgId);

            var command = await _MongoDbService.MsgsDb.FindOneAndDeleteAsync(Filters);
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
