using AutoMapper;
using SocialSync.Application.Features.Interactions.Requests.Commands;
using SocialSync.Application.Responses;
using MediatR;
using SocialSync.Domain.Entities;
using SocialSync.Application.Contracts.Persistence;
using SocialSync.Application.DTOs.InteractionDTOs.Validator;

namespace SocialSync.Application.Features.Interactions.Handlers.Commands;

public class LikeUnlikePostInteractionCommandHandler
    : IRequestHandler<LikeUnlikePostInteractionCommand, BaseCommandResponse>
{
    private readonly IInteractionRepository _interactionRepository;
    private readonly IMapper _mapper;
    private readonly IPostRepository _PostRepository;
    private readonly IUserRepository _UserRepository;

    public LikeUnlikePostInteractionCommandHandler(
        IInteractionRepository interactionRepository,
        IMapper mapper,
        IPostRepository PostRepository,
        IUserRepository UserRepository
    )
    {
        _PostRepository = PostRepository;
        _UserRepository = UserRepository;
        _interactionRepository = interactionRepository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(
        LikeUnlikePostInteractionCommand command,
        CancellationToken cancellationToken
    )
    {
        var response = new BaseCommandResponse();
        var validator = new LikeDtoValidator(_PostRepository, _UserRepository);

        var validationResult = await validator.ValidateAsync(command.LikeDto);

        if (!validationResult.IsValid)
        {
            response.Message = "Failed to create Comment";
            response.Success = false;
            response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
        }
        else
        {
            var createdInteraction = await _interactionRepository.likeUnlikeInteraction(
                _mapper.Map<Interaction>(command)
            );
            response.Success = true;
            response.Message = "Comment Created Successfully";
            response.Id = createdInteraction.Id;
        }

        return response;
    }
}