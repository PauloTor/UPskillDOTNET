
using JetBrains.dotMemoryUnit.Controller;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using ParqueAPICentral.Controllers;
using ParqueAPICentral.Models;
using ParqueAPICentral.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace testProject
{
    public class TestFaturasController
    {
        [Fact]
        public async Task GetAllFaturasAsync_ShouldReturnAllFaturasAsync()
        {
            Thread.Sleep(2000);
            //Arrange
            var testContext = APICentralContextMocker.GetAPICentralContext("DBTest4GetAllClientes");
            var theController = new FaturasController(testContext);

            //Act
            var result = await theController.GetFaturas();

            //Assert
            var items = Assert.IsType<List<Fatura>>(result.Value);
            Assert.Equal(3, items.Count);
        }
    }  
}