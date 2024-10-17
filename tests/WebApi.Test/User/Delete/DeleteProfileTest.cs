using FluentAssertions;
using System.Net;

namespace WebApi.Test.User.Delete;

public class DeleteProfileTest : CashFlowClassFixture
{
    private const string METHOD = "api/user";

    private readonly string _token;

    public DeleteProfileTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.UserTeamMember.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(requestUri: METHOD, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
