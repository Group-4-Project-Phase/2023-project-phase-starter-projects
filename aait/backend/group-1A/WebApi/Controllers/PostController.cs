﻿using System.Security.Claims;
using Application.DTO.CommentDTO.DTO;
using Application.DTO.NotificationDTO;
using Application.DTO.PostDTO.DTO;
using Application.Features.NotificationFeaure.Requests.Commands;
using Application.Features.PostFeature.Requests.Commands;
using Application.Features.PostFeature.Requests.Queries;
using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("post/")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<BaseResponse<List<PostResponseDTO>>>> Get()
        {   
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _mediator.Send(new GetAllPostsQuery { userId = userId});
            return Ok(result);    
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<BaseResponse<PostResponseDTO>>> Get(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _mediator.Send(new GetSinglePostQuery { Id = id, userId = userId });
            return Ok(result);            
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BaseResponse<PostResponseDTO>>> Post([FromBody] PostCreateDTO newPostData)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _mediator.Send(new CreatePostCommand{ NewPostData = newPostData, userId = userId });
            await _mediator.Send(new CreateNotification {NotificationData = new NotificationCreateDTO()
            {Content = "A Post has been Created",NotificationContentId = result.Value.Id,NotificationType = "post",UserId = userId}});

            return Ok(result);            
        }
   
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<BaseResponse<PostResponseDTO>>> Put(int id, [FromBody] PostUpdateDTO UpdatePostData)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _mediator.Send(new UpdatePostCommand { Id = id, PostUpdateData = UpdatePostData , userId = userId });
            await _mediator.Send(new CreateNotification {NotificationData = new NotificationCreateDTO()
            {Content = "A Post has been Updated",NotificationContentId = result.Value.Id,NotificationType = "post",UserId = userId}});
            return Ok(result);
            
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<BaseResponse<CommentResponseDTO>>> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _mediator.Send(new DeletePostCommand { userId = userId, Id = id });    
            await _mediator.Send(new CreateNotification {NotificationData = new NotificationCreateDTO()
            {Content = "A Post has been Deleted",NotificationContentId = result.Value,NotificationType = "post",UserId = userId}});        
            return Ok(result);
            
        }
    }
}
