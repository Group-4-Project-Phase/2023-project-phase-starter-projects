using Application.Contracts;
using Application.DTO.CommentDTO.DTO;
using Application.DTO.CommentDTO.Validations;
using Application.DTO.NotificationDTO;
using Application.Exceptions;
using Application.Features.CommentFeatures.Requests.Commands;
using Application.Features.NotificationFeaure.Requests.Commands;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using MediatR;


namespace Application.Features.CommentFeatures.Handlers.Commands
{
    public class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand, BaseResponse<CommentResponseDTO>>
    {
        // private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCommentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            // _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse<CommentResponseDTO>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            
            var validator = new CommentUpdateValidation();
            var validationResult = await validator.ValidateAsync(request.CommentData);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }
            var comment = await _unitOfWork.CommentRepository.Get(request.Id);

            if (comment == null) 
            {
                throw new NotFoundException("Comment is not found");
            }


            _mapper.Map(request.CommentData, comment);
            
            var updationResult = await _unitOfWork.CommentRepository.Update(comment);
            var result = _mapper.Map<CommentResponseDTO>(updationResult);

            await _unitOfWork.Save();



            return new BaseResponse<CommentResponseDTO> {
                Success = true,
                Message = "The Comment is updated successfully",
                Value = result
            };






        }
    }
}
