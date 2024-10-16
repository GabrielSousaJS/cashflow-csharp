using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Users.Profile;

public interface IGetUserProfileUseCase
{
    Task<ResponseUserProffileJson> Execute();
}
