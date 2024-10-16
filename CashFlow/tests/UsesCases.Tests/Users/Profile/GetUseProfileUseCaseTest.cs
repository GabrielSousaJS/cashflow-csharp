using CashFlow.Application.UseCases.Users.Profile;
using CashFlow.Domain.Entities;
using CommonTestsUtilities.Entities;
using CommonTestsUtilities.LoggedUser;
using CommonTestsUtilities.Mapper;
using FluentAssertions;

namespace UsesCases.Tests.Users.Profile;

public class GetUseProfileUseCaseTest
{
    public async Task Success()
    {
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var response = await useCase.Execute(); 

        response.Should().NotBeNull();
        response.Name.Should().Be(user.Name);
        response.Email.Should().Be(user.Email);
    }

    private GetUserProfileUseCase CreateUseCase(User user)
    {
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetUserProfileUseCase(loggedUser, mapper);
    }
}
