﻿namespace Cpnucleo.Application.Queries;

public sealed class AuthUserQueryHandler : IRequestHandler<AuthUserQuery, AuthUserViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthUserQueryHandler(IApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async ValueTask<AuthUserViewModel> Handle(AuthUserQuery request, CancellationToken cancellationToken)
    {
        AuthUserViewModel result = new()
        {
            OperationResult = OperationResult.Failed
        };

        var recurso = await _context.Recursos
            .AsNoTracking()
            .Where(x => x.Login == request.Usuario && x.Ativo)
            .FirstOrDefaultAsync(cancellationToken);

        if (recurso is null)
        {
            result.OperationResult = OperationResult.NotFound;

            return result;
        }

        var success = Recurso.VerifyPassword(request.Senha, recurso.Senha!, recurso.Salt!);

        if (!success)
        {
            result.OperationResult = OperationResult.NotFound;

            return result;
        }

        result.Recurso = recurso.MapToDto();

        int.TryParse(_configuration["Jwt:Expires"], out var jwtExpires);
        result.Token = TokenService.GenerateToken(result.Recurso.Id.ToString(), _configuration["Jwt:Key"]!, _configuration["Jwt:Issuer"]!, jwtExpires);

        result.OperationResult = OperationResult.Success;

        return result;
    }
}
