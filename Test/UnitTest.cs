using Microsoft.VisualStudio.TestTools.UnitTesting;
using APIBackEnd.Models;
using APIBackEnd.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Test
{
    [TestClass]
    public class UnitTest
    {
        static APIContext _context;
        static FundosController _fcontroller;
        static MovimentacoesController _mcontroller;
        static List<Movimentacao> _movs;

        [ClassInitialize]
        public static void Start(TestContext context)
        {
            var options = new DbContextOptionsBuilder<APIContext>().
                UseInMemoryDatabase(databaseName: "testDB").Options;

            _context = new APIContext(options);

            var jsonString = File.ReadAllText("MockDB/DataFundos.json");
            var fund = JsonSerializer.Deserialize<List<Fundo>>(jsonString);

            jsonString = File.ReadAllText("MockDB/DataMov.json");
            _movs = JsonSerializer.Deserialize<List<Movimentacao>>(jsonString);

            _context.Fundos.AddRange(fund);

            _context.SaveChanges();

            _fcontroller = new FundosController(_context);
            _mcontroller = new MovimentacoesController(_context);
        }

        [TestMethod]
        public void GetFundos()
        {
            var response = _fcontroller.GetFundos();

            var result = response.Result.Result as ObjectResult;
            var listaFundos = result.Value as List<Fundo>;

            Assert.AreEqual(listaFundos.Count, 5);
            Assert.AreEqual(result.StatusCode, 200);

        }

        [TestMethod]
        public void AporteInicialAbaixo()
        {
            var response = _mcontroller.PostMovimentacao(_movs.Where(m => m.ValorDaMovimentacao < 100).FirstOrDefault());

            var message = response.Exception.InnerException.Message;

            Assert.AreEqual(message, "Movimentação Inválida : O valor investimento não é superior ao valor mínimo do fundo");
        }

        [TestMethod]
        public void AdicionaMovimentacao()
        {
            var response = _mcontroller.PostMovimentacao(_movs.Where(m => m.ValorDaMovimentacao == 2000).FirstOrDefault());

            var result = response.Result.Result as ObjectResult;
            var actionResult = result.Value as CreatedAtActionResult;
            
            Assert.AreEqual(actionResult.StatusCode, 201);
            Assert.AreEqual(actionResult.Value.GetType(), typeof(Movimentacao));
        }

        [TestMethod]
        public void SaldoInsuficiente()
        {
            var mov = _movs.Where(m => m.ValorDaMovimentacao >= 10000 && m.TipoDaMovimentacao == TipoMovimentacao.Resgate).FirstOrDefault();

            var response = _mcontroller.PostMovimentacao(mov).Result.Result as ObjectResult;

            Assert.AreEqual(response.Value, "Movimentação Inválida : Saldo insuficiente");
            Assert.AreEqual(response.StatusCode, 400);

        }

        [TestMethod]
        public void FundoInexistente()
        {
            var mov = _movs.Where(m => m.IdDoFundo == System.Guid.Parse("7565c6e5-89e0-4cec-ac18-b260e85d9b8d")).FirstOrDefault();

            var response = _mcontroller.PostMovimentacao(mov).Result.Result as ObjectResult;

            Assert.AreEqual(response.Value, "Movimentação Inválida : O fundo não existe");
            Assert.AreEqual(response.StatusCode, 400);
        }
    }
}
