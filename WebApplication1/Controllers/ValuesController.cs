using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        ILogger<ValuesController> _logger;
        public ValuesController(ILogger<ValuesController> logger)
        {
            this._logger = logger;
        }
        // GET api/values
        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("GET api/values");
            return "From Get";
        }

        // GET api/values
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<string> Post()
        {
            _logger.LogInformation("POST api/values");
            string posted = await GetPostData();
            return posted;

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        private async Task<string> GetPostData()
        {

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
