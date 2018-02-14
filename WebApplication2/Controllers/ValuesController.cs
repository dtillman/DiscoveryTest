using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Steeltoe.Common.Discovery;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        ILogger<ValuesController> _logger;
        DiscoveryHttpClientHandler _handler;
        public ValuesController(IDiscoveryClient client, ILogger<ValuesController> logger)
        {
            _handler = new DiscoveryHttpClientHandler(client);
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            Uri uri = new Uri(@"https://FORTUNESERVICE/api/values");
            var client = new HttpClient(_handler, false);
            var getOutput = await client.GetStringAsync(uri);
            return getOutput;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<string> Post()
        {

            Uri uri = new Uri(@"https://FORTUNESERVICE/api/values");
            var client = new HttpClient(_handler, false);
            try
            {
                string posted = await GetPostData();
                var result = await client.PostAsync(uri, new StringContent(posted));
                var strg = await result.Content.ReadAsStringAsync();
                return strg;

            } catch(Exception e)
            {
                _logger.LogInformation(e, "Failed");
            }
            return "Failed";
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
