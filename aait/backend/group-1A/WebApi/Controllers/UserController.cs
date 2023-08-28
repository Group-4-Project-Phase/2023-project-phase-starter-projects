﻿using Application.Common;
using Application.DTO.Common;
using Application.DTO.UserDTO.DTO;
using Application.Features.UserFeature.Requests.Commands;
using Application.Features.UserFeature.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("User/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserResponseDTO>>> Get()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDTO>> Get(int id)
        {
            var result = await _mediator.Send(new GetSingleUserQuery { userId = id });

            return result;
        }


        [HttpPost]
        public async Task<ActionResult<CommonResponseDTO>> User([FromBody] UserCreateDTO newUserData)
        {
            var result = await _mediator.Send(new CreateUserCommand{ NewUserData = newUserData });
            return Ok(result);
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDTO>> Put(int id, [FromBody] UserUpdateDTO UpdateUserData)
        {
            var userId = 3;
            var result = await _mediator.Send(new UpdateUserCommand { userId = userId, UserUpdateData = UpdateUserData});
            return Ok(result);
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<CommonResponseDTO>> Delete(int id)
        {
            var userId = 3;
            var result = await _mediator.Send(new DeleteUserCommand { userId = id});
            return Ok(result);
        }
    }
}
