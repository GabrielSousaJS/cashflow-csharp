using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IUpdateExpensesOnlyRepository
{
    Task<Expense?> GetById(Domain.Entities.User user, long id);
    void Update(Expense expense);
}
