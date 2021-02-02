using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Number.Core;
using Number.Db;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace HomeWorkTrial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumbersController : ControllerBase
    {
        private readonly ILogger<NumbersController> _logger;
        private IinputServices _inputServices;

        public NumbersController(ILogger<NumbersController> logger, IinputServices inputServices)
        {
            _logger = logger;
            _inputServices = inputServices;
        }

        [HttpGet]
        [Route("{firstIndex}/{lastIndex}/{cached:bool?}/{time:int?}/{memoryUsage:int?}")]
        [Produces("application/json")]
        public  IActionResult GetFobanacciNumbers(int firstIndex, int lastIndex, bool cached = false, int time = 0, int memoryUsage=0)
        {
            
            Input input = new Input();

            List<string> errorMessages = _inputServices.InputValidation(firstIndex,lastIndex);
            if (errorMessages.Count != 0)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;   //Equals to HTTPResponse 500
                 return BadRequest(new { responseText = errorMessages[0].ToString() });
            }

            input.FirstIndex = firstIndex;
            input.LastIndex = lastIndex;
            input.isCached = cached ? cached : false;

            if(time != 0)
                input.Time = time;
            if (memoryUsage != 0)
                input.MemoryUsage = memoryUsage;

            try
            {
                var process = Process.GetCurrentProcess();              
                input.FibSequence =  _inputServices.GetFobanacci(input);
                long memoryUsed = process.PrivateMemorySize64;
                if (input.FibSequence.Count == 0)
                    return BadRequest("Finding first number took more time");
                if(input.MemoryUsage!=0)
                {
                    if (memoryUsed > input.MemoryUsage)
                    {
                        return BadRequest("All memory was used!!");
                    }
                }               
                var json = JsonConvert.SerializeObject(input, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});
                return Ok(json);
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
            }         
        }

    }
}
