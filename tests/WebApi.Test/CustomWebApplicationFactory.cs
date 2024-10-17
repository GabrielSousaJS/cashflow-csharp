using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infraestructure.DataAccess;
using CommonTestsUtilities.Entities;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Test.Resources;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public ExpensesIdentityManager ExpenseTeamMember { get; private set; } = default!;
    public ExpensesIdentityManager ExpenseAdmin { get; private set; } = default!;
    public UserIdentityManager UserTeamMember { get; private set; } = default!;
    public UserIdentityManager UserAdmin { get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<CashFlowDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<CashFlowDbContext>();
                var passwordEncryper = scope.ServiceProvider.GetRequiredService<IPasswordEncrypter>();
                var tokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                StartDatabase(dbContext, passwordEncryper, tokenGenerator);
            });
    }

    private void StartDatabase(CashFlowDbContext dbContext,
        IPasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator tokenGenerator)
    {
        var userTeamMember = AddUserTeamMember(dbContext, passwordEncrypter, tokenGenerator);
        var expenseTeamMember = AddExpense(dbContext, userTeamMember, expenseId: 1, tagId: 1);
        ExpenseTeamMember = new ExpensesIdentityManager(expenseTeamMember);

        var userAdmin = AddUserAdmin(dbContext, passwordEncrypter, tokenGenerator);
        var expenseAdmin = AddExpense(dbContext, userAdmin, expenseId: 2, tagId: 2);
        ExpenseAdmin = new ExpensesIdentityManager(expenseAdmin);

        dbContext.SaveChanges();
    }

    private CashFlow.Domain.Entities.User AddUserTeamMember(CashFlowDbContext dbContext,
        IPasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator tokenGenerator)
    {
        var user = UserBuilder.Build();
        var password = user.Password;
        user.Id = 1;

        user.Password = passwordEncrypter.Encrypt(user.Password);

        dbContext.User.Add(user);

        dbContext.SaveChanges();

        var token = tokenGenerator.Generate(user);

        UserTeamMember = new UserIdentityManager(user, password, token);

        return user;
    }

    private CashFlow.Domain.Entities.User AddUserAdmin(CashFlowDbContext dbContext,
        IPasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator tokenGenerator)
    {
        var user = UserBuilder.Build(Roles.ADMIN);
        var password = user.Password;
        user.Id = 2;

        user.Password = passwordEncrypter.Encrypt(user.Password);

        dbContext.User.Add(user);

        dbContext.SaveChanges();

        var token = tokenGenerator.Generate(user);

        UserAdmin = new UserIdentityManager(user, password, token);

        return user;
    }

    private Expense AddExpense(CashFlowDbContext dbContext, CashFlow.Domain.Entities.User user, long expenseId, long tagId)
    {
        var expense = ExpenseBuilder.Build(user);
        expense.Id = expenseId;

        foreach (var tag in expense.Tags)
        {
            tag.Id = tagId;
            tag.ExpenseId = expense.Id;
        }

        dbContext.Expenses.Add(expense);

        return expense;
    }
}
