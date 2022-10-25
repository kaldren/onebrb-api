using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Messages.Models;
using Onebrb.Domain.Entities.MessageAggregate;

namespace Onebrb.Application.Messages.Queries.GetSingleConversationByConversationId;

public record GetSingleConversationByConversationIdQuery : IRequest<ICollection<MessageModel>>
{
    public string Id { get; set; }
}

public class GetSingleConversationByConversationIdQueryHandler : IRequestHandler<GetSingleConversationByConversationIdQuery, ICollection<MessageModel>>
{
    private readonly IGenericRepository<Conversation> _conversationRepository;
    private readonly IMapper _mapper;

    public GetSingleConversationByConversationIdQueryHandler(IGenericRepository<Conversation> conversationRepository, IMapper mapper)
    {
        _conversationRepository = conversationRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<MessageModel>> Handle(GetSingleConversationByConversationIdQuery request, CancellationToken cancellationToken)
    {
        Conversation? conversationEntity = await _conversationRepository.GetAsync(request.Id);

        return _mapper.Map<ICollection<MessageModel>>(conversationEntity);
    }
}