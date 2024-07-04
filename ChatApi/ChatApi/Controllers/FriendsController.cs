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
    public class FriendsController : ControllerBase
    {
        public IMediator _Mediator { get; set; }
        public FriendsController(IMediator mediator)
        {
            _Mediator = mediator;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPeopleInApp()
        {

            var request = new GetAllTheUsersQuery();
            var med = _Mediator.Send(request);

            return Ok(await med);


        }
        [HttpPost("MakeFriendRequest")]
        [Authorize]
        public async Task<IActionResult> MakeFriendRequest(AddFriendCommand _addFriendCommand)
        {

            
            var req = _Mediator.Send(_addFriendCommand);

            return Ok(await req);


        }

        [HttpPost("AcceptTheFriendRequest")]
        [Authorize]
        public async Task<IActionResult> AcceptTheFriendRequest(AcceptFriendRequestCommand _AcceptFriendRequestCommand)
        {


            var req = _Mediator.Send(_AcceptFriendRequestCommand);

            return Ok(await req);


        }

        [HttpPost("RejectTheFriendRequest")]
        [Authorize]
        public async Task<IActionResult> RejectTheFriendRequest(RefuseFriendRequestCammand _RefuseFriendRequestCammand)
        {


            var req = _Mediator.Send(_RefuseFriendRequestCammand);

            return Ok(await req);


        }


        [HttpPost("GetAllMyFriendRequest")]
        [Authorize]

        public async Task<IActionResult> GetAllMyFriendRequest ()
        {
            GetMyFriendRequestQuery getMyFriendRequestQuery = new GetMyFriendRequestQuery();


            var req = _Mediator.Send(getMyFriendRequestQuery);

            return Ok(await req);


        }

    }
}
