using MediatR;

namespace ChatApi.model.command
{
    public class RefuseFriendRequestCammand :IRequest<String>
    {

        public string RequestId { get; set; }

    }
}
