using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIBackEnd.Models;

namespace APIBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaosController : ControllerBase
    {
        private readonly MovimentacoesContext _context;

        public MovimentacaosController(MovimentacoesContext context)
        {
            _context = context;
        }

        // GET: api/Movimentacaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimentacao>>> GetMovimentacoes()
        {
            return await _context.Movimentacoes.ToListAsync();
        }

        // GET: api/Movimentacaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimentacao>> GetMovimentacao(int id)
        {
            var movimentacao = await _context.Movimentacoes.FindAsync(id);

            if (movimentacao == null)
            {
                return NotFound();
            }

            return movimentacao;
        }

        // PUT: api/Movimentacaos/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMovimentacao(int id, Movimentacao movimentacao)
        //{
        //    if (id != movimentacao.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(movimentacao).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MovimentacaoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Movimentacaos
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<Movimentacao>> PostMovimentacao(Movimentacao movimentacao)
        //{
        //    _context.Movimentacoes.Add(movimentacao);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMovimentacao", new { id = movimentacao.id }, movimentacao);
        //}

        //// DELETE: api/Movimentacaos/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Movimentacao>> DeleteMovimentacao(int id)
        //{
        //    var movimentacao = await _context.Movimentacoes.FindAsync(id);
        //    if (movimentacao == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Movimentacoes.Remove(movimentacao);
        //    await _context.SaveChangesAsync();

        //    return movimentacao;
        //}

        private bool MovimentacaoExists(int id)
        {
            return _context.Movimentacoes.Any(e => e.Id == id);
        }
    }
}
