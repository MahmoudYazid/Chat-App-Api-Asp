using ChatApi.model.dbModel;
using MediatR;

namespace ChatApi.model.query
{
    public class GetCertainChatQuery:IRequest<IEnumerable<MsgDbModel>>
    {
        public string SenderId { get; set; }
        public string RecieverId { get; set; }
    }
}
