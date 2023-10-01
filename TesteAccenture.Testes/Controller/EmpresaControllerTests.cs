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
    public class EmpresaControllerTests
    {
        private readonly EmpresaController _empresaController;
        private readonly Mock<IEmpresaService> _empresaServiceMock = new Mock<IEmpresaService>();

        public EmpresaControllerTests()
        {
            _empresaController = new EmpresaController(_empresaServiceMock.Object);
        }

        [Fact]
        public void FiltrarEmpresa_DeveRetornarOk()
        {
            // Arrange
            var nomeFantasia = "EmpresaTeste";
            var cnpj = "12345678901234";
            var cep = "12345-678";
            var empresas = new List<Empresa>();

            _empresaServiceMock.Setup(service =>
                service.FiltrarEmpresa(nomeFantasia, cnpj, cep)).Returns(empresas);

            // Act
            var result = _empresaController.FiltrarEmpresa(nomeFantasia, cnpj, cep);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Empresa>>(okResult.Value);
            Assert.Same(empresas, model);
        }

        [Fact]
        public async Task ListarEmpresas_DeveRetornarUmActionResult()
        {
            // Arrange
            var empresas = new List<Empresa> { new Empresa() };

            _empresaServiceMock.Setup(service => service.ObterTodasAsync()).ReturnsAsync(empresas);

            // Act
            var result = await _empresaController.ListarEmpresas();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Empresa>>(okResult.Value);
            Assert.Same(empresas, model);
        }

        [Fact]
        public async Task EmpresaPorId_DeveRetornarEmpresa_QuandoEncontrada()
        {
            // Arrange
            var empresa = new Empresa { Id = 1 };

            _empresaServiceMock.Setup(service => service.ObterPorIdAsync(1)).ReturnsAsync(empresa);

            // Act
            var result = await _empresaController.EmpresaPorId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<Empresa>(okResult.Value);
            Assert.Same(empresa, model);
        }

        [Fact]
        public async Task EmpresaPorId_DeveRetornarNotFound_QuandoNaoEncontrada()
        {
            // Arrange
            _empresaServiceMock.Setup(service => service.ObterPorIdAsync(1)).ReturnsAsync((Empresa)null);

            // Act
            var result = await _empresaController.EmpresaPorId(1);

            // Assert
            var notFoundResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task AdicionarEmpresa_DeveRetornarCreated()
        {
            // Arrange
            var empresa = new Empresa();

            _empresaServiceMock.Setup(service => service.AdicionarAsync(empresa)).ReturnsAsync(1);

            // Act
            var result = await _empresaController.AdicionarEmpresa(empresa);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Empresa>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(nameof(_empresaController.EmpresaPorId), createdAtActionResult.ActionName);
            Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
            var model = Assert.IsAssignableFrom<Empresa>(createdAtActionResult.Value);
            Assert.Same(empresa, model);
        }

        [Fact]
        public async Task AtualizarEmpresa_DeveRetornarOk_QuandoAtualizada()
        {
            // Arrange
            var empresa = new Empresa { Id = 1 };

            _empresaServiceMock.Setup(service => service.AtualizarAsync(empresa));

            // Act
            var result = await _empresaController.AtualizarEmpresa(1, empresa);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Empresa>(okResult.Value);
            Assert.Same(empresa, model);
        }

        [Fact]
        public async Task AtualizarEmpresa_DeveRetornarBadRequest_QuandoIdNaoCorresponder()
        {
            // Arrange
            var empresa = new Empresa { Id = 2 };

            // Act
            var result = await _empresaController.AtualizarEmpresa(1, empresa);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeletarEmpresa_DeveRetornarNoContent()
        {
            // Act
            var result = await _empresaController.DeletarEmpresa(1);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeletarEmpresa_DeveRetornarBadRequest_QuandoExcecaoOcorrer()
        {
            // Arrange
            _empresaServiceMock.Setup(service => service.ExcluirAsync(1)).Throws(new Exception("Erro"));

            // Act
            var result = await _empresaController.DeletarEmpresa(1);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
