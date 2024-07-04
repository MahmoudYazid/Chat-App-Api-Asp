using MediatR;

namespace ChatApi.model.command
{
    public class DeleteMsgCommand : IRequest<string>
    {
        public string MsgId { get; set; }
    }
}
