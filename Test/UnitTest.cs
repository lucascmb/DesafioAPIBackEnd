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

        [ClassInitialize]
        public static void Start(TestContext context)
        {
            var options = new DbContextOptionsBuilder<APIContext>().
                UseInMemoryDatabase(databaseName: "testDB").Options;

            _context = new APIContext(options);

            var jsonString = File.ReadAllText("MockDB/DataFundos.json");
            var fund = JsonSerializer.Deserialize<List<Fundo>>(jsonString);

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
        public void AdicionaMovimentacao()
        {
            var jsonString = File.ReadAllText("MockDB/DataMov.json");
            var mov = JsonSerializer.Deserialize<List<Movimentacao>>(jsonString);

            var response = _mcontroller.PostMovimentacao(mov.First());

            var result = response.Result.Result as ObjectResult;
            var actionResult = result.Value as CreatedAtActionResult;
            
            Assert.AreEqual(actionResult.StatusCode, 201);
            Assert.AreEqual(actionResult.Value.GetType(), typeof(Movimentacao));
        }

        [TestMethod]
        public void AporteInicialAbaixo()
        {
            var jsonString = File.ReadAllText("MockDB/DataMov.json");
            var mov = JsonSerializer.Deserialize<List<Movimentacao>>(jsonString);

            var response = _mcontroller.PostMovimentacao(mov.Where(m => m.ValorDaMovimentacao < 100).FirstOrDefault());

            var message = response.Exception.InnerException.Message;

            Assert.AreEqual(message, "Movimentação Inválida : O valor investimento não é superior ao valor mínimo do fundo");
        }

        //[TestMethod]
        //public void SaldoInsuficiente()
        //{
        //    var jsonString = File.ReadAllText("MockDB/DataMov.json");
        //    var mov = JsonSerializer.Deserialize<List<Movimentacao>>(jsonString);

        //    string message;
        //    int statusCode;

        //    foreach(var m in mov)
        //    {
        //        var response = _mcontroller.PostMovimentacao(m);
        //        Assert.AreEqual(response, 501);
        //    }

        //    Assert.AreEqual(mov, 1);
        //}
    }
}
