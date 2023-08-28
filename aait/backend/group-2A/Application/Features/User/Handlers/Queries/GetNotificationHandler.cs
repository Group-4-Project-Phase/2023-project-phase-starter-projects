using Application.Contracts.Persistance;
using Application.Features.User.Request.Queries;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.User.Handlers.Queries;

public class GetNotificationHandler : IRequestHandler<GetNotifications, BaseCommandResponse<List<Notification>>>{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetNotificationHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper; 
        _unitOfWork = unitOfWork;
    }
    

    public async Task<BaseCommandResponse<List<Notification>>> Handle(GetNotifications request, CancellationToken cancellationToken){
        try{
            var notifications = await _unitOfWork.notificationRepository.GetNotifications(request.Id);
            if (notifications == null){
                notifications = new List<Notification>();
            }
            return BaseCommandResponse<List<Notification>>.SuccessHandler(notifications);
    
        }
        catch(Exception ex){
            return BaseCommandResponse<List<Notification>>.FailureHandler(ex);
        }
        
    }
}
