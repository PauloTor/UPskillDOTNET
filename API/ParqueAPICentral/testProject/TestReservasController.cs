using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ParqueAPICentral.Controllers;
using ParqueAPICentral.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotFoundResult = Microsoft.AspNetCore.Mvc.NotFoundResult;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace testProject
{
    public class TestReservasController
    {
        [Fact]
        public async Task GetAllReservasAsync_ShouldReturnAllReservasAsync()
        {
            Thread.Sleep(3000);
            //Arrange
            IConfiguration configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();
            var testContext = APICentralContextMocker.GetAPICentralContext("DBTest4GetAllReservas");
            var theController = new ReservasController(testContext, configuration);

            //Act
            var result = await theController.GetReservas();

            //Assert
            var items = Assert.IsType<List<Reserva_>>(result.Value);
            Assert.Equal(4, items.Count);
        }

        //[Fact]
        //public async Task PostReservasByData_ShouldCreateReservaByDataAsync()
        //{
        //    Thread.Sleep(3200);
        //    //Arrange
        //    IConfiguration configuration = new ConfigurationBuilder()
        //         .AddJsonFile("appsettings.json")
        //         .Build();
        //    var testContext = APICentralContextMocker.GetAPICentralContext("DBTest4PostReservas");
        //    var testController = new ReservasController(testContext, configuration);

        //    //Act
        //    var result = await testController.PostReservaByData(new Reserva (144444444, 1, 4));
        //    var get = await testController.GetReservas();

        //    //Assert
        //    Assert.IsType<Reserva>(get.Value);
        //    Assert.IsType<CreatedAtActionResult>(result.Result);
        //}
 
        [Fact]
        public async Task DeleteReservabyIDAsync_ShouldDeleteReserva1Async()
        {
            Thread.Sleep(3400);
            //Arrange
            IConfiguration configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();
            var testContext = APICentralContextMocker.GetAPICentralContext("DBTest4DeleteReserva");
            var testController = new ReservasController(testContext, configuration);

            //Act
            var result = await testController.CancelarReserva(1,11111);
            var get = await testController.GetReservas();

            //Assert
            Assert.IsType<NotFoundResult>(get.Result);
            //Assert.IsType<NoContentResult>(result);
        }
    }
}
