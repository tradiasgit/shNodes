using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace sh.Nodes.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public FileController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}/{loadbase}")]
        public Node Get(string id, bool loadbase = false)
        {
            var node = Node.GetNode(id);
            return node;
        }
    }
}
