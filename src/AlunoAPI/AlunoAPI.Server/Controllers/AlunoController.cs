using AlunoAPI.Data;
using AlunoAPI.Service;
using FTTBlazor.Client.Common.AlunoAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AlunoAPI.Server.Controllers
{
    // Todo:
    // Melhorar o microserviço para ser mais genérico

    [ApiController]
    [Route("[controller]")]
    public class AlunoController
    {
        private readonly ILogger<AlunoController> _logger;
        private readonly DatabaseContext _dbContext;

        public AlunoController(ILogger<AlunoController> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public AlunoDTO Get([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            return new AlunoService(_dbContext).Get(long.Parse(id));
        }

        [HttpGet]
        public IEnumerable<AlunoDTO> Get([FromQuery] int pagesize = 50, [FromQuery] int currentpage = 0)
        {
            return new AlunoService(_dbContext).GetAll(pagesize, currentpage);
        }

        [HttpPost]
        public void Post(AlunoDTO item)
        {
            new AlunoService(_dbContext).Add(item);
        }

        [HttpPut]
        public void Put(AlunoDTO item)
        {
            new AlunoService(_dbContext).Edit(item);
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            new AlunoService(_dbContext).Delete(long.Parse(id));
        }
    }
}
