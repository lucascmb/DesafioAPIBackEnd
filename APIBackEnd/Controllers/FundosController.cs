using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIBackEnd.Models;

namespace APIBackEnd.Controllers
{
    [Route("APIBackEnd/[controller]")]
    [ApiController]
    public class FundosController : ControllerBase
    {
        private readonly APIContext _context;

        public FundosController(APIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// [Retorna a Lista com os Fundos que são carragados no banco de dados em memória]
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fundo>>> GetFundos()
        {
            var lista = await _context.Fundos.ToListAsync();
            return Ok(lista);
        }

    }
}
