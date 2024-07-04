using ChatApi.model.command;
using ChatApi.model.query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IMediator _Mediator { get; set; }
        public LoginController(IMediator mediator) {
            _Mediator = mediator;
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand addUser) { 
            var resp = await _Mediator.Send(addUser);
            return Ok(resp);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginQuery loginQuery_)
        {
            var resp = await _Mediator.Send(loginQuery_);
            return Ok(resp);
        }
    }
}
