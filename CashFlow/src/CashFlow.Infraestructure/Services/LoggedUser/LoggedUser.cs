using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Domain.Services.LoogedUser;
using CashFlow.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CashFlow.Infraestructure.Services.LoggedUser;

internal class LoggedUser : ILoggedUser
{
    private readonly CashFlowDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(CashFlowDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Get()
    {
        var token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext
            .User
            .AsNoTracking()
            .FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
    }
}
