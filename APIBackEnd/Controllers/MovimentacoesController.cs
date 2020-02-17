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

        // GET: api/Movimentacoes
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

        // POST: api/Movimentacoes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Movimentacao>> PostMovimentacao(Movimentacao movimentacao)
        {
            if(_context.Fundos.Any(f => f.Id == movimentacao.IdDoFundo))
            {
                if(!VerificaValorInvestido(movimentacao.IdDoFundo, movimentacao.CpfDoCliente, movimentacao.ValorDaMovimentacao, movimentacao.TipoDaMovimentacao)) 
                    throw new InvalidOperationException("Movimentação Inválida : Saldo insuficiente");

                VerificaAporteMinimo(movimentacao.ValorDaMovimentacao, movimentacao.IdDoFundo, movimentacao.CpfDoCliente);

                _context.Movimentacoes.Add(movimentacao);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMovimentacao", new { id = movimentacao.Id }, movimentacao);
            }

            throw new InvalidOperationException("Movimentação Inválida : O fundo não existe");

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
