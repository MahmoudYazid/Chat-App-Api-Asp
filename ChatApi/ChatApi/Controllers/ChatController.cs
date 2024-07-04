using ChatApi.model.command;
using ChatApi.model.query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        public IMediator _Mediator { get; set; }
        public ChatController(IMediator mediator)
        {
            _Mediator = mediator;
        }
        [HttpPost("SendMsg")]

        public async Task<IActionResult> SendMsg([FromForm] SendMsgCommand sendMsgCommand)
        {
            var resp = await _Mediator.Send(sendMsgCommand);
            return Ok(resp);
        }


        [HttpPost("GetChat")]
        [Authorize]
        public async Task<IActionResult> GetChatBetween(GetCertainChatQuery _getCertainChatQuery)
        {
            var req = await _Mediator.Send(_getCertainChatQuery);
            return Ok(req);


        }

        [HttpDelete("DeleteMsg")]
        [Authorize]
        public async Task<IActionResult> DeleteMsg(DeleteMsgCommand _DeleteMsgCommand)
        {
            var req = await _Mediator.Send(_DeleteMsgCommand);
            return Ok(req);


        }


        [HttpGet("GetMyFriends")]
        [Authorize]
        public async Task<IActionResult> GetMyFriends()
        {
            GetMyFriendQuery _GetMyFriendQuery = new GetMyFriendQuery();
            var req = await _Mediator.Send(_GetMyFriendQuery);
            return Ok(req);


        }
    }
}
