using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;


namespace ConsoleProjectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsoleController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RunConsoleCommand([FromBody] LContext data)
        {
            try
            {
                LContext ll = (LContext) data;
                var jsonData = PipelineExecutorNS.LambdaEngine.Start(ll);

                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }


}
