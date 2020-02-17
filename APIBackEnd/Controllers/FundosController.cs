using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIBackEnd.Models;
using System.IO;
using System.Text.Json;

namespace APIBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundosController : ControllerBase
    {
        private readonly FundosContext _context;

        public FundosController(FundosContext context)
        {
            _context = context;
        }

        // GET: api/Fundos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fundo>>> GetFundos()
        {
            return await _context.Fundos.ToListAsync();
        }

        // GET: api/Fundos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fundo>> GetFundo(int id)
        {
            var fundo = await _context.Fundos.FindAsync(id);

            if (fundo == null)
            {
                return NotFound();
            }

            return fundo;
        }
    }
}
