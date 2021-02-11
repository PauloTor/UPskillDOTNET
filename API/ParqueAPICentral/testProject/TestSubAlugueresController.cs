using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ParqueAPICentral.Controllers;
using ParqueAPICentral.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotFoundResult = Microsoft.AspNetCore.Mvc.NotFoundResult;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace testProject
{
    public class TestSubAlugueresController
    {

        [Fact]
         public async Task PostSubAluguer_ShouldCreateSubAluguerAsync()
         {
             Thread.Sleep(4000);
             //Arrange
             IConfiguration configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json")
                  .Build();
             var testContext = APICentralContextMocker.GetAPICentralContext("DBTest4PostReservas");
             var testController = new SubAlugueresController(testContext, configuration);

            //Act
             var result = await testController.CreateSubAluguer(new SubAluguer(15, DateTime.Parse("2019-01-01 15:00:00"), DateTime.Parse("2019-01-05 15:00:00"), DateTime.Parse("2019-01-05 16:00:00"), 1));

             //Assert
             Assert.IsType<Reserva>(result.Value);
             Assert.IsType<CreatedAtActionResult>(result.Result);
         }

        //[Fact]
        //public async Task DeleteSubAluguerbyIDAsync_ShouldDeleteSubAluguer1Async()
        //{
        //    Thread.Sleep(4200);
        //    //Arrange
        //    IConfiguration configuration = new ConfigurationBuilder()
        //         .AddJsonFile("appsettings.json")
        //         .Build();
        //    var testContext = APICentralContextMocker.GetAPICentralContext("DBTest4DeleteReserva");
        //    var testController = new SubAlugueresController(testContext, configuration);

        //    //Act
        //    var result = await testController.CancelarSubAluguer();
        //    var get = await testController.GetSubAluguer();

        //    //Assert
        //    Assert.IsType<NotFoundResult>(get.Result);
        //    //Assert.IsType<NoContentResult>(result);
        //}
    }
}
