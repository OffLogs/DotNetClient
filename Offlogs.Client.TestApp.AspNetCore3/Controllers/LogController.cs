using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Offlogs.Client.TestApp.AspNetCore3.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            _logger.LogError("Some formatted {0} message", "Error");
            return new JsonResult(new {});
        }

        [HttpGet("exception")]
        public IActionResult Exception()
        {
            try
            {
                throw new Exception("Some exception");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return new JsonResult(new { });
        }

        [HttpGet("info")]
        public IActionResult Info()
        {
            _logger.LogInformation("Some formatted {0} message", "Info");
            return new JsonResult(new { });
        }

        [HttpGet("warning")]
        public IActionResult Warning()
        {
            _logger.LogWarning("Some formatted {0} message", "Warning");
            return new JsonResult(new { });
        }

        [HttpGet("debug")]
        public IActionResult Debug()
        {
            _logger.LogDebug("Some formatted {0} message", "Debug");
            return new JsonResult(new { });
        }

        [HttpGet("trace")]
        public IActionResult Trace()
        {
            _logger.LogDebug("Some formatted {0} message", "Trace");
            return new JsonResult(new { });
        }

        [HttpGet("critical")]
        public IActionResult Critical()
        {
            _logger.LogCritical("Some formatted {0} message", "Critical");
            return new JsonResult(new { });
        }
    }
}
