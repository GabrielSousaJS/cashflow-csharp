using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Services.LoogedUser;

public interface ILoggedUser
{
    Task<User> Get();
}
