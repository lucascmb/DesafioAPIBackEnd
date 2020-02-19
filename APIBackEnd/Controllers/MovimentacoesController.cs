using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIBackEnd.Models;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace APIBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacoesController : ControllerBase
    {
        private readonly APIContext _context;

        public MovimentacoesController(APIContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimentacao>>> GetMovimentacoes()
        {
            return await _context.Movimentacoes.ToListAsync();
        }

        // GET: api/Movimentacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimentacao>> GetMovimentacao(Guid id)
        {
            var movimentacao = await _context.Movimentacoes.FindAsync(id);

            if (movimentacao == null)
            {
                return NotFound();
            }

            return movimentacao;
        }

        /// <summary>
        /// [Adiciona uma movimentação de aplicação ou resgate]
        /// </summary>
        /// <remarks>
        /// Forma de realizar a Request :
        /// 
        ///     POST / Movimentação
        ///     (tipoDaMovimentacao : 1 para Aplicação e 2 para Resgate)
        ///     (idDoFundo : adicionar um ID que exista na tabela de fundos)
        ///     
        ///     {
        ///         "tipoDaMovimentacao" : 1,
        ///         "cpfDoCliente" : "25489635541",
        ///         "valorDaMovimentacao" : 2000,
        ///         "dataDaMovimentacao" : "2020-02-19T05:49:48.098Z",
        ///         "idDoFundo" : "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        /// </remarks>
        /// <param name="movimentacao"></param>
        /// <returns>Uma nova movimentação</returns>
        [HttpPost]
        public async Task<ActionResult<Movimentacao>> PostMovimentacao(Movimentacao movimentacao)
        {
            if(_context.Fundos.Any(f => f.Id == movimentacao.IdDoFundo))
            {
                if(!VerificaValorInvestido(movimentacao.IdDoFundo, movimentacao.CpfDoCliente, movimentacao.ValorDaMovimentacao, movimentacao.TipoDaMovimentacao)) 
                    return BadRequest("Movimentação Inválida : Saldo insuficiente");

                VerificaAporteMinimo(movimentacao.ValorDaMovimentacao, movimentacao.IdDoFundo, movimentacao.CpfDoCliente);

                _context.Movimentacoes.Add(movimentacao);
                await _context.SaveChangesAsync();

                return Ok(CreatedAtAction("GetMovimentacao", new { id = movimentacao.Id }, movimentacao));
            }

            return BadRequest("Movimentação Inválida : O fundo não existe");

            //var response = HttpContext.Features.Get<IExceptionHandlerFeature>();

            //return Problem(
            //    detail: response.Error.StackTrace,
            //    title: response.Error.Message
            //    );
        }

        bool VerificaValorInvestido(Guid id, string cpf, decimal valor, TipoMovimentacao tipo)
        {
            decimal somaTotal = 0;

            List<Movimentacao> mov = _context.Movimentacoes.Where(m => m.IdDoFundo == id && m.CpfDoCliente == cpf).ToList();

            if(mov.Count == 0 && tipo == TipoMovimentacao.Aplicacao) { return true; }

            foreach(Movimentacao m in mov)
            {
                if(m.TipoDaMovimentacao == TipoMovimentacao.Aplicacao) { somaTotal += m.ValorDaMovimentacao; }
                else { somaTotal -= m.ValorDaMovimentacao; }
            }

            if (somaTotal >= 0 && (somaTotal - valor) >= 0) return true;

            return false;
        }

        void VerificaAporteMinimo(decimal valor, Guid id, string cpf)
        {
            decimal aporteMinimo = _context.Fundos.Where(f => f.Id == id).Select(f => f.InvestimentoInicialMinimo).First();

            var movimentacao = _context.Movimentacoes.Where(m => m.CpfDoCliente == cpf && m.IdDoFundo == id).ToList();

            if(valor <= aporteMinimo && movimentacao.Count == 0) { throw new InvalidOperationException("Movimentação Inválida : O valor investimento não é superior ao valor mínimo do fundo"); }
        }
    }
}
