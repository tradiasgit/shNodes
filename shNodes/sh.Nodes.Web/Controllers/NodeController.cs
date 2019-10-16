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
    public class NodeController
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public NodeController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Node Get(string id)
        {
            var node = Node.GetNode(id);
            return node;
        }
    }
}
