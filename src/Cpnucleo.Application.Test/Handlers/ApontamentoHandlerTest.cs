﻿namespace Cpnucleo.Application.Test.Handlers;

public class ApontamentoHandlerTest
{
    [Fact]
    public async Task CreateApontamentoCommand_Handle_Success()
    {
        // Arrange
        IApplicationDbContext context = DbContextHelper.GetContext();
        await DbContextHelper.SeedData(context);

        var tarefa = context.Tarefas.First();
        var recurso = context.Recursos.First();

        CreateApontamentoCommand request = MockCommandHelper.GetNewCreateApontamentoCommand(tarefa.Id, recurso.Id);

        // Act
        CreateApontamentoCommandHandler handler = new(context);
        OperationResult response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(response == OperationResult.Success);
    }

    [Fact]
    public async Task GetApontamentoQuery_Handle_Success()
    {
        // Arrange
        IApplicationDbContext context = DbContextHelper.GetContext();
        IMapper mapper = AutoMapperHelper.GetMappings();
        await DbContextHelper.SeedData(context);

        var apontamento = context.Apontamentos.First();

        GetApontamentoQuery request = MockQueryHelper.GetNewGetApontamentoQuery(apontamento.Id);

        // Act
        GetApontamentoQueryHandler handler = new(context, mapper);
        GetApontamentoViewModel response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(response.Apontamento != null);
        Assert.True(response.Apontamento.Id != Guid.Empty);
        Assert.True(response.Apontamento.DataInclusao.Ticks != 0);
    }

    [Fact]
    public async Task ListApontamentoQuery_Handle_Success()
    {
        // Arrange
        IApplicationDbContext context = DbContextHelper.GetContext();
        IMapper mapper = AutoMapperHelper.GetMappings();
        await DbContextHelper.SeedData(context);

        ListApontamentoQuery request = MockQueryHelper.GetNewListApontamentoQuery();

        // Act
        ListApontamentoQueryHandler handler = new(context, mapper);
        ListApontamentoViewModel response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(response.Apontamentos != null);
        Assert.True(response.Apontamentos.Any());
    }

    [Fact]
    public async Task RemoveApontamentoCommand_Handle_Success()
    {
        // Arrange
        IApplicationDbContext context = DbContextHelper.GetContext();
        IMapper mapper = AutoMapperHelper.GetMappings();
        await DbContextHelper.SeedData(context);

        var apontamento = context.Apontamentos.First();

        RemoveApontamentoCommand request = MockCommandHelper.GetNewRemoveApontamentoCommand(apontamento.Id);
        GetApontamentoQuery request2 = MockQueryHelper.GetNewGetApontamentoQuery(apontamento.Id);

        // Act
        RemoveApontamentoCommandHandler handler = new(context);
        OperationResult response = await handler.Handle(request, CancellationToken.None);

        GetApontamentoQueryHandler handler2 = new(context, mapper);
        GetApontamentoViewModel response2 = await handler2.Handle(request2, CancellationToken.None);

        // Assert
        Assert.True(response == OperationResult.Success);
        Assert.True(response2.OperationResult == OperationResult.NotFound);
    }

    [Fact]
    public async Task UpdateApontamentoCommand_Handle_Success()
    {
        // Arrange
        IApplicationDbContext context = DbContextHelper.GetContext();
        IMapper mapper = AutoMapperHelper.GetMappings();
        await DbContextHelper.SeedData(context);

        var tarefa = context.Tarefas.First();
        var recurso = context.Recursos.First();
        var apontamento = context.Apontamentos.First();

        UpdateApontamentoCommand request = MockCommandHelper.GetNewUpdateApontamentoCommand(tarefa.Id, recurso.Id, apontamento.Id);
        GetApontamentoQuery request2 = MockQueryHelper.GetNewGetApontamentoQuery(apontamento.Id);

        // Act
        UpdateApontamentoCommandHandler handler = new(context);
        OperationResult response = await handler.Handle(request, CancellationToken.None);

        GetApontamentoQueryHandler handler2 = new(context, mapper);
        GetApontamentoViewModel response2 = await handler2.Handle(request2, CancellationToken.None);

        // Assert
        Assert.True(response == OperationResult.Success);
        Assert.True(response2.Apontamento != null);
        Assert.True(response2.Apontamento.Id == apontamento.Id);
    }
}
