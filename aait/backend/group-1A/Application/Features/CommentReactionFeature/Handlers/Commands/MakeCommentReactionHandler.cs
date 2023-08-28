using Application.Contracts;
using Application.DTO.Common;
using Application.Exceptions;
using Application.Features.CommentReactionFeature.Requests.Commands;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CommentReactionFeature.Handlers.Commands
{
    public class MakeCommentReactionHandler : IRequestHandler<MakeReactionOnComment, BaseResponse<int>>
    {
        private readonly ICommentReactionRepository _commentReactionRespository;
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;

        public MakeCommentReactionHandler(ICommentReactionRepository commentReactionRepository, IMapper mapper, ICommentRepository commentRepository)
        {
            _commentReactionRespository = commentReactionRepository;
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public async Task<BaseResponse<int>> Handle(MakeReactionOnComment request, CancellationToken cancellationToken)
        {
            var validator = new ReactionValidator();
            var validationResult = await validator.ValidateAsync(request.ReactionData);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var exists = await _commentRepository.Exists(request.ReactionData.ReactedId);
            if (exists == false)
            {
                throw new NotFoundException("Comment is not found to make the Reactions");
            }


            var commentReaction = _mapper.Map<CommentReaction>(request.ReactionData);
            


            if (request.ReactionData.ReactionType == "like")
            {
                commentReaction.Like = true;
            }
            else
            {
                commentReaction.Dislike = true;
            }

            commentReaction.CommentId = request.ReactionData.ReactedId;



            var result = await _commentReactionRespository.MakeReaction(request.UserId, commentReaction);
            if (result == null)
            {
                throw new BadRequestException("Comment is not found");
            }



            return new BaseResponse<int>()
            {
                Success = true,
                Message = "Reaction is made successfully",
                Value = request.ReactionData.ReactedId
            };
        }
    }
}