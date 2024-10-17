using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestsUtilities.Requests;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Login.DoLogin;

public class DoLoginTest : CashFlowClassFixture
{
    private const string METHOD = "api/login";
    private readonly string _name;
    private readonly string _email;
    private readonly string _password;

    public DoLoginTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
    {
        _email = customWebApplication.UserTeamMember.GetEmail();
        _name = customWebApplication.UserTeamMember.GetName();
        _password = customWebApplication.UserTeamMember.GetPassword();
    }

    [Fact]
    public async void Success()
    {
        var request = new RequestLoginJson
        {
            Email = _email,
            Password = _password,
        };

        var result = await DoPost(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("name").GetString().Should().Be(_name);
        response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async void Error_Login_Invalid(string cultureInfo)
    {
        var request = RequestLoginJsonBuilder.Build();

        var result = await DoPost(requestUri: METHOD, request: request, culture: cultureInfo);

        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(cultureInfo));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}
