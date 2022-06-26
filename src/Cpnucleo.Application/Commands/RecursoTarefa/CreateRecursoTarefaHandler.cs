﻿namespace Cpnucleo.Application.Commands.RecursoTarefa;

public class CreateRecursoTarefaHandler : IRequestHandler<CreateRecursoTarefaCommand, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateRecursoTarefaHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OperationResult> Handle(CreateRecursoTarefaCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.RecursoTarefaRepository.AddAsync(_mapper.Map<Domain.Entities.RecursoTarefa>(request));

        bool success = await _unitOfWork.SaveChangesAsync();

        OperationResult result = success ? OperationResult.Success : OperationResult.Failed;

        return result;
    }
}
