﻿

using Application.Contracts;
using Application.DTO.PostDTO.DTO;
using Application.DTO.PostDTO.validations;
using Application.Exceptions;
using Application.Features.PostFeature.Requests.Commands;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.PostFeature.Handlers.Commands
{
    public class UpdatePostHandler : IRequestHandler<UpdatePostCommand, BaseResponse<PostResponseDTO>>
    {
        // private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdatePostHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            // _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<PostResponseDTO>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var validator = new PostUpdateValidation();
            var validationResult = await validator.ValidateAsync(request.PostUpdateData);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }
            var post = await _unitOfWork.PostRepository.Get(request.Id);


            if (post == null) 
            {
                throw new NotFoundException("Post is not found");
            }

            _mapper.Map(request.PostUpdateData, post);

            //var newPost = _mapper.Map<Post>(request.PostUpdateData);
            //newPost.Id = request.Id;
            //newPost.UserId = request.userId;
            var updationResult = await _unitOfWork.PostRepository.Update(post);
            var result = _mapper.Map<PostResponseDTO>(updationResult);

            return new BaseResponse<PostResponseDTO> {
                Success = true,
                Message = "The Post is updated successfully",
                Value = result
            };
        }
    }
}
