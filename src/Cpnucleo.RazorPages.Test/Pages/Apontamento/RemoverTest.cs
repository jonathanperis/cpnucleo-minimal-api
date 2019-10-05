﻿using Cpnucleo.Application.Interfaces;
using Cpnucleo.Infra.CrossCutting.Util.ViewModels;
using Cpnucleo.RazorPages.Pages.Apontamento;
using Moq;
using SparkyTestHelpers.AspNetMvc.Core;
using System;
using Xunit;

namespace Cpnucleo.RazorPages.Test.Pages.Apontamento
{
    public class RemoverTest
    {
        private readonly Mock<IApontamentoAppService> _apontamentoAppService;

        public RemoverTest()
        {
            _apontamentoAppService = new Mock<IApontamentoAppService>();
        }

        [Fact]
        public void Test_OnGet()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            ApontamentoViewModel apontamentoMock = new ApontamentoViewModel { };

            RemoverModel pageModel = new RemoverModel(_apontamentoAppService.Object)
            {
                PageContext = PageContextManager.CreatePageContext()
            };

            _apontamentoAppService.Setup(x => x.Consultar(id)).Returns(apontamentoMock);

            PageModelTester<RemoverModel> pageTester = new PageModelTester<RemoverModel>(pageModel);

            // Act
            pageTester
                .Action(x => () => x.OnGet(id))

                // Assert
                .TestPage();
        }

        [Fact]
        public void Test_OnPost()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            RemoverModel pageModel = new RemoverModel(_apontamentoAppService.Object)
            {
                Apontamento = new ApontamentoViewModel { Id = id },
                PageContext = PageContextManager.CreatePageContext()
            };

            _apontamentoAppService.Setup(x => x.Remover(id));

            PageModelTester<RemoverModel> pageTester = new PageModelTester<RemoverModel>(pageModel);

            // Act
            pageTester
                .Action(x => x.OnPost)

                // Assert
                .TestRedirectToPage("Listar");
        }
    }
}
