using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TesteAccenture.Controllers;
using TesteAccenture.Models;
using TesteAccenture.Services;
using Xunit;

namespace TesteAccenture.Testes.Controller
{
    public class FornecedorControllerTests
    {
        private readonly FornecedorController _fornecedorController;
        private readonly Mock<IFornecedorService> _fornecedorServiceMock = new Mock<IFornecedorService>();

        public FornecedorControllerTests()
        {
            _fornecedorController = new FornecedorController(_fornecedorServiceMock.Object);
        }

        [Fact]
        public async Task ListarFornecedores_DeveRetornarUmActionResult()
        {
            // Arrange
            var fornecedores = new List<Fornecedor> { new Fornecedor() };

            _fornecedorServiceMock.Setup(service => service.ObterTodosAsync()).ReturnsAsync(fornecedores);

            // Act
            var result = await _fornecedorController.ListarFornecedores();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Fornecedor>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Fornecedor>>(okResult.Value);
            Assert.Same(fornecedores, model);
        }

        [Fact]
        public void FiltrarFornecedores_DeveRetornarOk()
        {
            // Arrange
            var nome = "FornecedorTeste";
            var cnpj = "12345678901234";
            var email = "teste@example.com";
            var cep = "12345-678";
            var fornecedores = new List<Fornecedor>();

            _fornecedorServiceMock.Setup(service =>
                service.FiltrarFornecedores(nome, cnpj, email, cep)).Returns(fornecedores);

            // Act
            var result = _fornecedorController.FiltrarFornecedores(nome, cnpj, email, cep);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Fornecedor>>(okResult.Value);
            Assert.Same(fornecedores, model);
        }

        [Fact]
        public async Task FornecedorPorId_DeveRetornarFornecedor_QuandoEncontrado()
        {
            // Arrange
            var fornecedor = new Fornecedor { Id = 1 };

            _fornecedorServiceMock.Setup(service => service.ObterPorIdAsync(1)).ReturnsAsync(fornecedor);

            // Act
            var result = await _fornecedorController.FornecedorPorId(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Fornecedor>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<Fornecedor>(okResult.Value);
            Assert.Same(fornecedor, model);
        }

        [Fact]
        public async Task FornecedorPorId_DeveRetornarNotFound_QuandoNaoEncontrado()
        {
            // Arrange
            _fornecedorServiceMock.Setup(service => service.ObterPorIdAsync(1)).ReturnsAsync((Fornecedor)null);

            // Act
            var result = await _fornecedorController.FornecedorPorId(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Fornecedor>>(result);
            var notFoundResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task AdicionarFornecedor_DeveRetornarCreated()
        {
            // Arrange
            var fornecedor = new Fornecedor();

            _fornecedorServiceMock.Setup(service => service.AdicionarAsync(fornecedor)).ReturnsAsync(1);

            // Act
            var result = await _fornecedorController.AdicionarFornecedor(fornecedor);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Fornecedor>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(nameof(_fornecedorController.FornecedorPorId), createdAtActionResult.ActionName);
            Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
            var model = Assert.IsAssignableFrom<Fornecedor>(createdAtActionResult.Value);
            Assert.Same(fornecedor, model);
        }


        [Fact]
        public async Task AtualizarFornecedor_DeveRetornarOk_QuandoAtualizado()
        {
            // Arrange
            var fornecedor = new Fornecedor { Id = 1 };

            _fornecedorServiceMock.Setup(service => service.AtualizarAsync(fornecedor));

            // Act
            var result = await _fornecedorController.AtualizarFornecedor(1, fornecedor);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Fornecedor>(okResult.Value);
            Assert.Same(fornecedor, model);
        }

        [Fact]
        public async Task AtualizarFornecedor_DeveRetornarBadRequest_QuandoIdNaoCorresponder()
        {
            // Arrange
            var fornecedor = new Fornecedor { Id = 2 };

            // Act
            var result = await _fornecedorController.AtualizarFornecedor(1, fornecedor);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeletarFornecedor_DeveRetornarNoContent()
        {
            // Arrange

            // Act
            var result = await _fornecedorController.DeletarFornecedor(1);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeletarFornecedor_DeveRetornarNotFound_QuandoExcecaoOcorrer()
        {
            // Arrange
            _fornecedorServiceMock.Setup(service => service.ExcluirAsync(1)).Throws(new Exception("Erro"));

            // Act
            var result = await _fornecedorController.DeletarFornecedor(1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
