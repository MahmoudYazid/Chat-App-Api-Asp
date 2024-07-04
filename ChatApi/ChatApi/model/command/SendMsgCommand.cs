using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ChatApi.model.command
{
    public class SendMsgCommand : IRequest<string>
    {
   
        public string recieverId { get; set; }
        [Required]
        public string TextMsg { get; set; }
        public IFormFile? VideoMsg { get; set; }
    }
}
