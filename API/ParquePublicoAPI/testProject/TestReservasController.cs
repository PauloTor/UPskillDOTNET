using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ParquePublicoAPI.Controllers;
using ParquePublicoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotFoundResult = Microsoft.AspNetCore.Mvc.NotFoundResult;
using Xunit;

namespace testProject
{
    public class TestReservasController
    {
        /*[Fact]
        public async Task GetAllReservasAsync_ShouldReturnAllReservasAsync()
        {
            Thread.Sleep(2000);
            //Arrange
            var testContext = ParquePublicoAPIContextMocker.GetParquePublicoAPIContext("DBTest4GetAllReservas");
            var theController = new ReservasController(testContext);

            //Act
            var result = await theController.GetReserva();

            //Assert
            var items = Assert.IsType<List<Reserva>>(result.Value);
            Assert.Equal(4, items.Count);
        }*/

        [Fact]
        public async Task PostReservas_ShouldCreateReservaAsync()
        {
            Thread.Sleep(3200);
            //Arrange
            var testContext = ParquePublicoAPIContextMocker.GetParquePublicoAPIContext("DBTest4PostReservas");
            var testController = new ReservasController(testContext);

            //Act
            var result = await testController.PostReserva(new Reserva { ReservaID = 5, DataReserva = DateTime.Parse("2019-01-01 15:00:00"), DataInicio = DateTime.Parse("2019-01-05 15:00:00"), DataFim = DateTime.Parse("2019-01-05 16:00:00"), LugarID = 1 });
            var get = await testController.GetReserva(5);

            //Assert
            Assert.IsType<Reserva>(get.Value);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
        /*[Fact]
        public async Task PutReservabyIDAsync_ShouldUpdateReserva1Async()
        {
            Thread.Sleep(2300);
            //Arrange
            var testContext = ParquePublicoAPIContextMocker.GetParquePublicoAPIContext("DBTest4PutReservaByID");
            var testController = new ReservasController(testContext);

            //Act
            var getReserva = await testController.GetReserva(1);
            var reserva = getReserva.Value;
            reserva.ReservaID = 1;
            reserva.DataReserva = DateTime.Parse("2018-01-01 11:00:00");
            reserva.DataInicio = DateTime.Parse("2018-01-05 15:00:00");
            reservas.DataFim = DateTime.Parse("2020-01-05 16:00:00");
            reserva.LugarID = 1;
            var result = await testController.PutReserva(reserva.ReservaID, reserva);
            var getresult = await testController.GetReserva(1);

            //Assert
            var items = Assert.IsType<Reserva>(getresult.Value);
            Assert.Equal(1, items.ReservaID);
            Assert.Equal(2, items.DataReserva);
            Assert.Equal(1, items.DataInicio);
            Assert.Equal(6, items.DataFim);
            Assert.Equal(1, items.LugarID);
            Assert.IsType<NoContentResult>(result);
        }*/

        [Fact]
        public async Task DeleteReservabyIDAsync_ShouldDeleteReserva1Async()
        {
            Thread.Sleep(3400);
            //Arrange
            var testContext = ParquePublicoAPIContextMocker.GetParquePublicoAPIContext("DBTest4DeleteReserva");
            var testController = new ReservasController(testContext);

            //Act
            var result = await testController.DeleteReserva(1);
            var get = await testController.GetReserva(1);

            //Assert
            Assert.IsType<NotFoundResult>(get.Result);
            //Assert.IsType<NoContentResult>(result);
        }
    }
}