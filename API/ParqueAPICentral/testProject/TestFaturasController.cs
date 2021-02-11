using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Controllers;
using ParqueAPICentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace testProject
{
    public class TestFaturasController
    {
        [Fact]
        public async Task PostFaturas_ShouldCreateFaturasAsync()
        {
            Thread.Sleep(2300);
            //Arrange
            var testContext = APICentralContextMocker.GetAPICentralContext("DBTest4PostFatura");
            var testController = new FaturasController(testContext);

            //Act
            var result = await testController.PostFaturaByReservaID(DateTime.Parse("2020-01-04 21:00:00"), 33, 1);
            var get = await testController.GetFatura(1);
            // defaultFatura.Add(new Fatura(DateTime.Parse("2020-01-04 21:00:00"), 33, 1));

            //Assert
            Assert.IsType<Fatura>(get.Value);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task GetAllFaturasAsync_ShouldReturnAllFaturasAsync()
        {
            Thread.Sleep(2000);
            //Arrange
            var testContext = APICentralContextMocker.GetAPICentralContext("DBTest4GetAllFaturas");
            var theController = new FaturasController(testContext);

            //Act
            var result = await theController.GetFaturas();

            //Assert
            var items = Assert.IsType<List<Fatura>>(result.Value);
            Assert.Equal(3, items.Count);
        }
    }
}
