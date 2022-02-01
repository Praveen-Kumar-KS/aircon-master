using Aircon.Test;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Aircon.Web.IntegrationTests.ControllerTest
{
    [Collection("AirconWebApplicationFactory")]
    public class HomeControllerTests : IntegrationTestBase
    {
        public HomeControllerTests(AirconWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Get_SecurePageRequiresAnAuthenticatedUser()
        {
            // Arrange
            var client = GetFactory().CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

            // Act
            var response = await client.GetAsync("/Home");

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("http://localhost/Identity/Account/Login",
                               response.Headers.Location.OriginalString);
        }

        [Fact]
        public async Task Get_SecurePageIsAvailableForAuthenticatedUser()
        {
            // Arrange
            var client = GetFactory(hasUser: true).CreateClient();

            // Act
            var response = await client.GetAsync("/Home/Index");
            var body = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = GetFactory().CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
