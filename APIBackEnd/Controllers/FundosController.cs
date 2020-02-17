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
    public class FundosController : ControllerBase
    {
        private readonly APIContext _context;

        public FundosController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Fundos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fundo>>> GetFundos()
        {
            return await _context.Fundos.ToListAsync();
        }

    }
}
