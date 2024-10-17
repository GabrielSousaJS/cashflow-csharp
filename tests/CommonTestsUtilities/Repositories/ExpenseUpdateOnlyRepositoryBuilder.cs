using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommonTestsUtilities.Repositories;

public class ExpenseUpdateOnlyRepositoryBuilder
{
    private Mock<IUpdateExpensesOnlyRepository> _mock;

    public ExpenseUpdateOnlyRepositoryBuilder()
    {
        _mock = new Mock<IUpdateExpensesOnlyRepository>();
    }

    public ExpenseUpdateOnlyRepositoryBuilder GetById(CashFlow.Domain.Entities.User user, Expense? expense)
    {
        if (expense is not null)
            _mock.Setup(repository => repository.GetById(user, expense.Id)).ReturnsAsync(expense);

        return this;
    }

    public IUpdateExpensesOnlyRepository Build() => _mock.Object;
}
