using MediatR;

namespace ChatApi.model.command
{
    public class AcceptFriendRequestCommand : IRequest<string>
    {
        public string RequestId { get; set; }
    }
}
