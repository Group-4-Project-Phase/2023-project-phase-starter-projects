using FluentValidation;
using SocialSync.Application.Contracts.Persistence;

namespace SocialSync.Application.DTOs.PostDtos.Validators;
public class UpdatePostDtoValidator : AbstractValidator<UpdatePostDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;
    public UpdatePostDtoValidator(IUserRepository userRepository, IPostRepository postRepository)
    {
        _userRepository = userRepository;
        _postRepository = postRepository;

        RuleFor(p => p.Id)
        .MustAsync(async (id, token) =>
        {
            bool postExists = await _postRepository.ExistsAsync(id);
            return postExists;
        }).WithMessage("Given id did not match any post id.");

        RuleFor(p => p.Content)
        .NotNull().WithMessage("Post content cannot be null.")
        .NotEmpty().WithMessage("Post content cannot be empty.")
        .MaximumLength(500).WithMessage("Post content cannot have more than 500 characters.");

        RuleFor(p => new { p.Id, p.UserId })
        .NotNull().WithMessage("Post author id cannot be null.")
        .MustAsync(async (dto, token) =>
        {
            var post = await _postRepository.GetAsync(dto.Id);
            var user = await _userRepository.GetAsync(dto.UserId);

            return user != null && post.UserId == user.Id;
        }).WithMessage("Post author id does not match with the id of author of the post being updated.");





    }
}