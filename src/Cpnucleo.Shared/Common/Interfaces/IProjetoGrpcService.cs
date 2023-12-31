﻿namespace Cpnucleo.Shared.Common.Interfaces;

public interface IProjetoGrpcService : IService<IProjetoGrpcService>
{
    UnaryResult<OperationResult> CreateProjeto(CreateProjetoCommand command);

    UnaryResult<OperationResult> UpdateProjeto(UpdateProjetoCommand command);

    UnaryResult<GetProjetoViewModel> GetProjeto(GetProjetoQuery query);

    UnaryResult<ListProjetoViewModel> ListProjeto(ListProjetoQuery query);

    UnaryResult<OperationResult> RemoveProjeto(RemoveProjetoCommand command);
}