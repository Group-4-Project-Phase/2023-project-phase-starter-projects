using System.Security.Claims;
using Application.DTO;
using Application.DTO.Common;
using Application.DTO.FollowDTo;
using Application.DTO.PostDTO.DTO;
using Application.DTO.UserDTO.DTO;
using Application.Features.FollowFeature.Requests.Commands;
using Application.Features.FollowFeature.Requests.Queries;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FollowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("followers")]
        [Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> GetFollowers()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _mediator.Send(new GetFollowersQuery(){Id = userId});
            return Ok(result);
        }
       
        [HttpGet("followees")]
        [Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> GetFollowees()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _mediator.Send(new GetFollowedUsersQuery(){Id = userId});
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post(int Id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var newFollowData = new FollowDTO() {FollowerId = userId, FolloweeId = Id };
            var result = await _mediator.Send(new CreateFollowCommand() { FollowDTO = newFollowData });
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete(int Id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var followToDelete = new FollowDTO() {FollowerId = userId, FolloweeId = Id };
            var result = await _mediator.Send(new DeleteFollowCommand() { FollowDTO = followToDelete });
            return Ok(result);
        }
    }
}