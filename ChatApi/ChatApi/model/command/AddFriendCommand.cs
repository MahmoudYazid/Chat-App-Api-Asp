using MediatR;

namespace ChatApi.model.command
{
    public class AddFriendCommand: IRequest<string>
    {
    
        public string RecieverId { get; set; }
    }
}
