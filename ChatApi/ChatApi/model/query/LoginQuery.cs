using MediatR;

namespace ChatApi.model.query
{
    public class LoginQuery : IRequest<string>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
