﻿using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Identity;
using Application.Contracts.Persistance;
using Application.DTO.Post.Validation;
using Application.DTO.UserDTO.Validator;
using Application.Exceptions;
using Application.Features.User.Request.Commands;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.UserFeatures.Handlers.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, AuthResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;


        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
        {
            _mapper = mapper; 
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken){
            var validator = new CreateUserDTOValidator(_unitOfWork.userRepository);
            var validationResult = await validator.ValidateAsync(request.CreateUser);
            if (!validationResult.IsValid)
            {
                
                throw new ValidationException(validationResult);
            }
            var user = _mapper.Map<Domain.Entities.User>(request.CreateUser);
            await _unitOfWork.userRepository.Add(user);
            Console.WriteLine("reached Here");
            var token = await _authService.Register(user);
            Console.WriteLine("---------------AuthServise");
            if (token is null){
                throw new Exception();
            }
            
            await _unitOfWork.Save();

            return new AuthResponse(){ Id = user.Id, Email = user.Email, Username = user.UserName, Token = token};
        }
    }
}
