using MediatR;

namespace ChatApi.model.command
{
    public class AddUserCommand:IRequest<String>
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string password { get; set; }
    }
}
