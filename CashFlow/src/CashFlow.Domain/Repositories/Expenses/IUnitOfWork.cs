namespace CashFlow.Domain.Repositories.Expenses;
public interface IUnitOfWork
{
    Task Commit();
}
