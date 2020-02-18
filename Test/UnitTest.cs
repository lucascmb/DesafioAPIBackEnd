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
            var response = _fcontroller.GetFundos().Result.Value;
            var listaFundos = response.ToList();

            Assert.AreEqual(listaFundos.Count, 5);

        }

        [TestMethod]
        public void AdicionaMovimentacao()
        {

        }

        [TestMethod]
        public void AporteInicialAbaixo()
        {

        }

        [TestMethod]
        public void SaldoInsuficiente()
        {

        }
    }
}
