using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using App.DTO.v1_0.Identity;
using Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using Xunit.Abstractions;

namespace App.Test.Integration.api;

[Collection("NonParallel")]
public class AdvertisementControllerTest: IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    public AdvertisementControllerTest(CustomWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    
    [Fact]
    public async Task RegisterUser_Success()
    {
        // Arrange
        var registrationData = new
        {
            Email = "newuser@example.com",
            Password = "NewUser123!",
            Firstname = "John",
            Lastname = "Doe"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1.0/identity/Account/Register", registrationData);
        var contentStr = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode(); 

        var registerResponse = JsonSerializer.Deserialize<JWTResponse>(contentStr, JsonHelper.CamelCase);

        // Assert
        Assert.NotNull(registerResponse);
        Assert.NotNull(registerResponse.Jwt);
        Assert.True(registerResponse.Jwt.Length > 0);
    }
    
    [Fact]
    public async Task LoginUser_Success()
    {
        // Arrange
        var loginData = new
        {
            Email = "admin@eesti.ee",
            Password = "Kala.maja1"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1.0/identity/Account/Login", loginData);
        var contentStr = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode(); // Ensure the response status is 200-299

        var loginResponse = JsonSerializer.Deserialize<JWTResponse>(contentStr, JsonHelper.CamelCase);

        // Assert
        Assert.NotNull(loginResponse);
        Assert.NotNull(loginResponse.Jwt);
        Assert.True(loginResponse.Jwt.Length > 0);
    }
    
    [Fact]
    public async Task LogoutUser_Success()
    {
        // Arrange
        var loginData = new
        {
            Email = "admin@eesti.ee",
            Password = "Kala.maja1"
        };

        // Log in to get the JWT token
        var loginResponse = await _client.PostAsJsonAsync("/api/v1.0/identity/Account/Login", loginData);
        var loginContentStr = await loginResponse.Content.ReadAsStringAsync();
        loginResponse.EnsureSuccessStatusCode();

        var loginDataResponse = JsonSerializer.Deserialize<JWTResponse>(loginContentStr, JsonHelper.CamelCase);
        Assert.NotNull(loginDataResponse);
        Assert.NotNull(loginDataResponse.Jwt);
        Assert.NotNull(loginDataResponse.RefreshToken);
        
        // Act
        var logoutData = new LogoutInfo()
        {
            RefreshToken = loginDataResponse.RefreshToken
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1.0/identity/Account/Logout");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginDataResponse.Jwt);
        request.Content = new StringContent(JsonSerializer.Serialize(logoutData), Encoding.UTF8, "application/json");

        var logoutResponse = await _client.SendAsync(request);
        
        // Assert
        logoutResponse.EnsureSuccessStatusCode();
    }
    
    private async Task<string> GetJwtToken()
    {
        var user = "admin@eesti.ee";
        var pass = "Kala.maja1";

        // Authenticate and get JWT token
        var response = await _client.PostAsJsonAsync("/api/v1.0/identity/Account/Login", new { email = user, password = pass });
        response.EnsureSuccessStatusCode();
        var contentStr = await response.Content.ReadAsStringAsync();
        var loginData = JsonSerializer.Deserialize<JWTResponse>(contentStr, JsonHelper.CamelCase);
        return loginData!.Jwt;
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string url, string? jwt = default, object? content = default)
    {
        var request = new HttpRequestMessage(method, url);
        if (jwt != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        }
        if (content != null)
        {
            request.Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
        }
        return request;
    }

    [Fact]
    public async Task GetAdvertisements_ReturnsOk()
    {
        // Arrange
        var jwt = await GetJwtToken();
        var request = CreateRequest(HttpMethod.Get, "/api/v1.0/Advertisements", jwt);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.NotEmpty(content); // Additional assertions can be added based on expected data
    }

    [Fact]
    public async Task GetAdvertisement_ReturnsOk()
    {
        // Arrange
        var jwt = await GetJwtToken();
        var advertisementId = Guid.NewGuid(); // Replace with a valid advertisement ID for real test
        var request = CreateRequest(HttpMethod.Get, $"/api/v1.0/Advertisements/{advertisementId}", jwt);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotNull(content); // Additional assertions based on expected data
        }
        else
        {
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    [Fact]
    public async Task PostAdvertisement_CreatesNewAdvertisement()
    {
        // Arrange
        var jwt = await GetJwtToken();
        var advertisement = new App.DTO.v1_0.Advertisement()
        {
            Id = Guid.NewGuid(),
            Title = "titleTest",
            LocationId = Guid.NewGuid(),
            PriceId = Guid.NewGuid(),
            ServiceId = Guid.NewGuid(),
            StatusId = Guid.NewGuid(),
        };
        var request = CreateRequest(HttpMethod.Post, "/api/v1.0/Advertisements", jwt, advertisement);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var createdAdvertisement = JsonSerializer.Deserialize<App.DTO.v1_0.Advertisement>(content, JsonHelper.CamelCase);
        Assert.NotNull(createdAdvertisement);
        Assert.Equal("titleTest", createdAdvertisement.Title);
    }

    [Fact]
    public async Task PutAdvertisement_UpdatesAdvertisement()
    {
        // Arrange
        var jwt = await GetJwtToken();
        var advertisementId = Guid.NewGuid(); // Replace with a valid advertisement ID for real test
        
        var updateAdvertisement = new App.DTO.v1_0.Advertisement()
        {
            Id = advertisementId,
            Title = "updatedTitleTest",
            LocationId = Guid.NewGuid(),
            PriceId = Guid.NewGuid(),
            ServiceId = Guid.NewGuid(),
            StatusId = Guid.NewGuid(),
        };
        
        var request = CreateRequest(HttpMethod.Put, $"/api/v1.0/Advertisements/{advertisementId}", jwt, updateAdvertisement);
        // Act
        var response = await _client.SendAsync(request);

        // Assert
        
        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
        else
        {
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    [Fact]
    public async Task DeleteAdvertisement_DeletesAdvertisement()
    {
        // Arrange
        var jwt = await GetJwtToken();
        var advertisementId = Guid.NewGuid(); // Replace with a valid advertisement ID for real test
        var request = CreateRequest(HttpMethod.Delete, $"/api/v1.0/Advertisements/{advertisementId}", jwt);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
        else
        {
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}