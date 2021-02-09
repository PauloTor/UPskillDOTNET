using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ParquePrivateAPI.Controllers;
using ParquePrivateAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotFoundResult = Microsoft.AspNetCore.Mvc.NotFoundResult;
using Xunit;

namespace testProject
{
    public class TestLugaresController
    {
        [Fact]
        public async Task GetAllLugaresAsync_ShouldReturnAllLugaresAsync()
        {
            Thread.Sleep(2000);
            //Arrange
            var testContext = ParquePrivateAPIContextMocker.GetParquePrivateAPIContext("DBTest4GetAllLugares");
            var theController = new LugaresController(testContext);

            //Act
            var result = await theController.GetLugar();

            //Assert
            var items = Assert.IsType<List<Lugar>>(result.Value);
            Assert.Equal(10, items.Count);
        }

        [Fact]
        public async Task GetAllLugaresDisponiveisAsync_ShouldReturnAllLugaresDisponiveisAsync()
        {
            Thread.Sleep(2200);
            //Arrange
            var testContext = ParquePrivateAPIContextMocker.GetParquePrivateAPIContext("DBTest4GetAllLugaresDisponiveis");
            var theController = new LugaresController(testContext);

            //Act
            var result = await theController.GetLugaresSemReserva("2020-01-05 15:00:00", "2020-01-05 16:00:00");

            //Assert
            var items = Assert.IsType<List<Lugar>>(result.Value);
            Assert.Equal(9, items.Count);
        }

        [Fact]
        public async Task PostLugares_ShouldCreateLugaresAsync()
        {
            Thread.Sleep(2300);
            //Arrange
            var testContext = ParquePrivateAPIContextMocker.GetParquePrivateAPIContext("DBTest4PostLugares");
            var testController = new LugaresController(testContext);

            //Act
            var result = await testController.PostLugar(new Lugar { LugarID = 11, Fila = 2, Sector = 1, Preço = 5, NIFParqueID = 1 });
            var get = await testController.GetLugar(6);

            //Assert
            Assert.IsType<Lugar>(get.Value);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
        [Fact]
        public async Task PutLugarbyIDAsync_ShouldUpdateLugar1Async()
        {
            Thread.Sleep(2400);
            //Arrange
            var testContext = ParquePrivateAPIContextMocker.GetParquePrivateAPIContext("DBTest4PutLugarByID");
            var testController = new LugaresController(testContext);

            //Act
            var getLugar = await testController.GetLugar(1);
            var lugar = getLugar.Value;
            lugar.LugarID = 1;
            lugar.Fila = 2;
            lugar.Sector = 1;
            lugar.Preço = 6;
            lugar.NIFParqueID = 1;
            var result = await testController.PutLugar(lugar.LugarID, lugar);
            var getresult = await testController.GetLugar(1);
            
            //Assert
            var items = Assert.IsType<Lugar>(getresult.Value);
            Assert.Equal(1, items.LugarID);
            Assert.Equal(2, items.Fila);
            Assert.Equal(1, items.Sector);
            Assert.Equal(6, items.Preço);
            Assert.Equal(1, items.NIFParqueID);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteLugarbyIDAsync_ShouldDeleteLugar1Async()
        {
            Thread.Sleep(2500);
            //Arrange
            var testContext = ParquePrivateAPIContextMocker.GetParquePrivateAPIContext("DBTest4DeleteLugar");
            var testController = new LugaresController(testContext);

            //Act
            var result = await testController.DeleteLugar(2);
            var get = await testController.GetLugar(2);

            //Assert
            Assert.IsType<NotFoundResult>(get.Result);
            //Assert.IsType<NoContentResult>(result);
        }
    }
}
