using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using ParqueAPICentral.Controllers;
using ParqueAPICentral.Models;
using ParqueAPICentral.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace testProject
{
    public class TestFaturasController
    {
        protected HttpClientHelper HttpClientHelperUnderTest { get; }
        public TestFaturasController()
        {
            HttpClientHelperUnderTest = new HttpClientHelper();
        }
        public class GetAsyncHttpHelper : TestFaturasController
        {

            [Fact]
            public async Task GetAsync_Fatura_Return_Success_Result()
            {
                //Arrange;
                var result = new List<Fatura>();
                {
                    new Fatura(DateTime.Parse("2020-01-04 21:00:00"), 32, 1);
                }
                var httpMessageHandler = new Mock<HttpMessageHandler>();
                var fixture = new Fixture();

                // Setup Protected method on HttpMessageHandler mock.
                httpMessageHandler.Protected()
                     .Setup<Task<HttpResponseMessage>>(
                         "SendAsync",
                         ItExpr.IsAny<HttpRequestMessage>(),
                         ItExpr.IsAny<CancellationToken>()
                        )
                        .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                        {
                            HttpResponseMessage response = new HttpResponseMessage();
                            response.StatusCode = System.Net.HttpStatusCode.OK;//Setting statuscode
                            response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(result)); // configure your response here
                            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); //Setting media type for the response
                            return response;
                        });

                var httpClient = new HttpClient(httpMessageHandler.Object);
                httpClient.BaseAddress = fixture.Create<Uri>();

                HttpClientHelperUnderTest.HttpClient = httpClient; //Mocking setting Httphandler object to interface property.

                //Act
                var fatura = await HttpClientHelperUnderTest.GetAsync<List<Fatura>>(string.Empty);

                // Assert
                Assert.NotNull(fatura);
            }
        }
    }
}
