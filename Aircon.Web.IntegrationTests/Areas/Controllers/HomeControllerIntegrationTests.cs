using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Aircon.Data;
using Newtonsoft.Json;
using Xunit;
using System.Linq;
using System;
using Aircon.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Aircon.Test;
using System.Net;
using Aircon.Web.IntegrationTests.Helpers;

namespace Aircon.Web.IntegrationTests.Areas.Controllers
{
    [Collection("AirconWebApplicationFactory")]
    public class HomeControllerIntegrationTests : IntegrationTestBase
    {

        public HomeControllerIntegrationTests(AirconWebApplicationFactory factory) : base(factory)
        {

        }

        //[Fact]
        //public async Task CanGetPlayers()
        //{
        //    // The endpoint or route of the controller action.
        //    //var httpResponse = await _client.GetAsync("/Admin/Home/Index");
        //    var httpResponse = await GetFactory().CreateClient().GetAsync("/Identity/Account/Login");

        //    // Must be successful.
        //    httpResponse.EnsureSuccessStatusCode();

        //    //Assert.Equal(1, cnt);
        //    //.Equal(4, AirconDbContext.Permissions.ToList().Count);
        //    // Deserialize and examine results.
        //    var stringResponse = await httpResponse.Content.ReadAsStringAsync();
        //    var players = JsonConvert.DeserializeObject<int>(stringResponse);
        //    Assert.Equal(2, players);

        //    //var indexViewHtml = await HtmlHelpers.GetDocumentAsync(httpResponse);
        //    //var todoItems = indexViewHtml.QuerySelectorAll(".todo-item");
        //    //Assert.Equal(2, todoItems.Length);

        //    //var players = JsonConvert.DeserializeObject<IEnumerable<Player>>(stringResponse);
        //    //Assert.Contains(players, p => p.FirstName == "Wayne");
        //    //Assert.Contains(players, p => p.FirstName == "Mario");

        //    //var client = Factory.CreateClient();

        //    var indexView = await GetFactory().CreateClient().GetAsync("/Admin/Home/Index");
            
        //    Assert.Equal(HttpStatusCode.OK, indexView.StatusCode);
        //    var indexViewHtml = await HtmlHelpers.GetDocumentAsync(indexView);
        //    var todoItems = indexViewHtml .QuerySelectorAll(".usercount");
        //    Assert.Equal(2, todoItems.Length);

        //}

        public static readonly IEnumerable<object[]> Endpoints = new List<object[]>()
        {
            new object[] {"/"},
            new object[] {"/Home"},
            new object[] {"/Admin/Home/Index"},
        };

        [Theory]
        [MemberData(nameof(Endpoints))]
        public async Task GetEndpointsReturnSuccessAndCorrectContentType(string url)
        {
            const string expectedContentType = "text/html; charset=utf-8";

            var response = await GetFactory().CreateClient().GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal(expectedContentType,
                response.Content.Headers.ContentType.ToString());
        }
    }
}
