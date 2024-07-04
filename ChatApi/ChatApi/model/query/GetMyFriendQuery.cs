using ChatApi.model.dbModel;
using MediatR;

namespace ChatApi.model.query
{
    public class GetMyFriendQuery : IRequest<IEnumerable<PersonDbModel>>
    {
        public string MyId { get; set; }
    }
}
