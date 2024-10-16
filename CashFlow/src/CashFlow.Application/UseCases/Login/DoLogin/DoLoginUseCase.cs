using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncrypter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(
        IUserReadOnlyRepository repository,
        IPasswordEncrypter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetUserByEmail(request.Email) ?? throw new InvalidLoginException();
        var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);

        if (!passwordMatch)
            throw new InvalidLoginException();

        return new ResponseRegisterUserJson
        {

            Name = user.Name,
            Token = _accessTokenGenerator.Generate(user)

        };
    }
}
