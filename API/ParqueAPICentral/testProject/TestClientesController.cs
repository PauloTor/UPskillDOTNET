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

namespace testProject
{
    public class TestClientesController
    {
        [Fact]
        public async Task GetAllClientesAsync_ShouldReturnAllClientesAsync()
        {
            Thread.Sleep(2000);
            //Arrange
            var testContext = APICentralContextMocker.GetAPICentralContext("DBTest4GetAllClientes");
            var theController = new ClientesController(testContext);

            //Act
            var result = await theController.GetClientes();

            //Assert
            var items = Assert.IsType<List<Cliente>>(result.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public async Task PostClientes_ShouldCreateClientesAsync()
        {
            Thread.Sleep(2300);
            //Arrange
            var testContext = APICentralContextMocker.GetAPICentralContext("DBTest4PostCliente");
            var testController = new ClientesController(testContext);

            //Act
            var result = await testController.PostCliente(new Cliente ("Paulo Silva", "upskill4@upskill.pt", 123345678, "DD", 0, 4));
            var get = await testController.GetCliente(4);

            //Assert
            Assert.IsType<Cliente>(get.Value);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
        [Fact]
        public async Task PutClientebyIDAsync_ShouldUpdateCliente1Async()
        {
            Thread.Sleep(2400);
            //Arrange
            var testContext = APICentralContextMocker.GetAPICentralContext("DBTest4PutClienteByID");
            var testController = new ClientesController(testContext);

            //Act
            var getCliente = await testController.GetCliente(1);
            var cliente = getCliente.Value;
            cliente.ClienteID = 1;
            cliente.NomeCliente = "Victor Duarte";
            cliente.EmailCliente = "upskill1@upskill.pt";
            cliente.NifCliente = 123456789;
            cliente.MetodoPagamento = "DD";
            cliente.Credito = 15;
            cliente.Id = 1;
            var result = await testController.PutCliente(cliente.ClienteID, cliente);
            var getresult = await testController.GetCliente(1);

            //Assert
            var items = Assert.IsType<Cliente>(getresult.Value);
            Assert.Equal(1, items.ClienteID);
            Assert.Equal("Victor Duarte", items.NomeCliente);
            Assert.Equal("upskill1@upskill.pt", items.EmailCliente);
            Assert.Equal(123456789, items.NifCliente);
            Assert.Equal("DD", items.MetodoPagamento);
            Assert.Equal(15, items.Credito);
            Assert.Equal(1, items.Id);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteClientebyIDAsync_ShouldDeleteCliente1Async()
        {
            Thread.Sleep(2500);
            //Arrange
            var testContext = APICentralContextMocker.GetAPICentralContext("DBTest4DeleteCliente");
            var testController = new ClientesController(testContext);

            //Act
            var result = await testController.DeleteCliente(2);
            var get = await testController.GetCliente(2);

            //Assert
            Assert.IsType<NotFoundResult>(get.Result);
            //Assert.IsType<NoContentResult>(result);
        }
    }
}
