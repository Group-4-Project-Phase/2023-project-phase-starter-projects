using SocialSync.Application.Features.Interactions.Requests.Commands;
using SocialSync.Application.DTOs.InteractionDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialSync.Application.Features.Comments.Requests.Commands;
using SocialSync.Application.DTOs.InteractionDTOs.CommentDTOs;
using SocialSync.Application.Features.Comments.Requests.Queries;
using SocialSync.Domain.Entities;

namespace SocialSync.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InteractionController : ControllerBase
{
    private readonly IMediator _mediator;

    public InteractionController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("like")]
    public async Task<IActionResult> LikeUnlikePost([FromBody] InteractionDTO likeDto)
    {
        likeDto.type = InteractionType.Like;
        likeDto.Body = null;
        var command = new LikeUnlikePostInteractionCommand { LikeDto = likeDto };
        var response = await _mediator.Send(command);

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }


    [HttpPost("AddComment")]
    public async Task<IActionResult> CommentOnPost([FromBody] InteractionDTO interactionDto)
    {
        interactionDto.type = InteractionType.Comment;
        var command = new CreateCommentInteractionCommand { CreateCommentDto = interactionDto };
        var response = await _mediator.Send(command);

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }


    [HttpPut("UpdateComment")]
    public async Task<IActionResult> UpdateCommentOfAPost(
        [FromBody] UpdateCommentInteractionDTO interactionDto
    )
    {
        var command = new UpdateCommentInteractionCommand { UpdateCommentDto = interactionDto };
        var response = await _mediator.Send(command);

        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }


    [HttpDelete("DeleteComment")]
    public async Task<IActionResult> DeleteCommentOfAPost(
        [FromBody] DeleteCommentInteractionDTO interactionId
    )
    {
        var command = new DeleteCommentInteractionCommand { deleteCommentInteractionDTO = interactionId };
        var response = await _mediator.Send(command);

        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("GetAllCommentsOfAPost/{postId}")]
    public async Task<IActionResult> GetAllCommentOfAPost(int postId)
    {
        var command = new GetAllCommentInteractionRequest { PostId = postId };
        var response = await _mediator.Send(command);

        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("GetAComment/{id}")]
    public async Task<IActionResult> GetCommentOfAPost(int id)
    {
        var command = new GetInteractionRequest { id = id };
        var response = await _mediator.Send(command);

        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}