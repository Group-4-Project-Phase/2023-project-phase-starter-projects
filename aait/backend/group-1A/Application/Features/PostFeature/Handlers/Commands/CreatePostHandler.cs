﻿using System.Text.RegularExpressions;
using Application.Contracts;
using Application.DTO.NotificationDTO;
using Application.DTO.PostDTO.DTO;
using Application.DTO.PostDTO.validations;
using Application.Exceptions;
using Application.Features.NotificationFeaure.Requests.Commands;
using Application.Features.PostFeature.Requests.Commands;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using MediatR;
using SocialSync.Application.Contracts;
using SocialSync.Domain.Entities;

namespace Application.Features.PostFeature.Handlers.Commands
{
    public class CreatePostHandler : IRequestHandler<CreatePostCommand, BaseResponse<PostResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        private readonly ITagRepository _tagRepository;

        private readonly IPostTagRepository _postTagRepository;
        private readonly IMediator _mediator;

        public CreatePostHandler(IMapper mapper, IPostRepository postRepository,ITagRepository tagRepository,IPostTagRepository postTagRepository, IMediator mediator)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _mediator = mediator;
            _tagRepository = tagRepository;
            _postTagRepository = postTagRepository;
            
        }
        public async Task<BaseResponse<PostResponseDTO>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var validator = new PostCreateValidation();
            var validationResult = await validator.ValidateAsync(request.NewPostData);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var newPost = _mapper.Map<Post>(request.NewPostData);
            newPost.UserId = request.userId;
            var result = await _postRepository.Add(newPost);

            // tags
            var tags = PostTagParser(result);

            foreach (var tag in tags)
            {
                var tagEntity = await _tagRepository.GetTagByName(tag);

                if (tagEntity == null)
                {
                    var newTag = new Tag
                    {
                        Title = tag
                    };
                    await _tagRepository.Add(newTag);
                    var postTag = new PostTag
                    {
                        PostId = result.Id,
                        TagId = newTag.Id
                    };

                    if (!await _postTagRepository.Exists(postTag))
                        await _postTagRepository.Add(postTag);
                    

                } 
            }

            return new BaseResponse<PostResponseDTO> {
                Success = true,
                Message = "Post Is cereated successfully",
                Value =  _mapper.Map<PostResponseDTO>(result)
            };
        }


        public static List<string> PostTagParser(Post post)
        {
            string pattern = @"#\w+";

            var postBody = post.Content;

            Regex regex = new(pattern);

            MatchCollection matchCollection = regex.Matches(postBody);

            var tags = new List<string>();

            foreach (Match match in matchCollection.Cast<Match>())
            {
                tags.Add(match.Value);
            }

            return tags;

        }
    }
}
