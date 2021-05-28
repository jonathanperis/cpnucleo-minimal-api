﻿using Cpnucleo.Infra.CrossCutting.Util.Commands.Requests.Recurso;
using Cpnucleo.Infra.CrossCutting.Util.Commands.Responses.Recurso;
using Cpnucleo.Infra.CrossCutting.Util.Queries.Requests.Recurso;
using Cpnucleo.Infra.CrossCutting.Util.Queries.Responses.Recurso;
using ProtoBuf.Grpc;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Cpnucleo.Infra.CrossCutting.Util.Interfaces
{
    [ServiceContract]
    public interface IRecursoGrpcService
    {
        [OperationContract]
        Task<CreateRecursoResponse> AddAsync(CreateRecursoComand command, CallContext context = default);

        [OperationContract]
        Task<UpdateRecursoResponse> UpdateAsync(UpdateRecursoComand command, CallContext context = default);

        [OperationContract]
        Task<GetRecursoResponse> GetAsync(GetRecursoQuery query, CallContext context = default);

        [OperationContract]
        Task<ListRecursoResponse> AllAsync(ListRecursoQuery query, CallContext context = default);

        [OperationContract]
        Task<RemoveRecursoResponse> RemoveAsync(RemoveRecursoComand command, CallContext context = default);
    }
}