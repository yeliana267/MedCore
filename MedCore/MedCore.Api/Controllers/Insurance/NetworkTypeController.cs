using MedCore.Application.Dtos.Insurance.NetworkType;
using MedCore.Application.Interfaces.Insurance;
using MedCore.Domain.Entities.Insurance;
using MedCore.Persistence.Interfaces.Insurance;
using MedCore.Persistence.Repositories.Insurance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.Insurance
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkTypeController : ControllerBase
    {
     
        public readonly INetworkTypeService _networkTypeService;
        public readonly ILogger<NetworkTypeController> _logger;
        public readonly IConfiguration _configuration;
        public NetworkTypeController(INetworkTypeService networkTypeService, 
                                     IConfiguration configuration, ILogger<NetworkTypeController> logger) 
        {
            _logger = logger;
            _configuration = configuration;
            _networkTypeService = networkTypeService;

        }
        // GET: api/<NetworkTypeController>
        [HttpGet("GetNeworkType")]
        public async Task<IActionResult> Get()
        {
            var networkTypes = await  _networkTypeService.GetAll();
            return Ok(networkTypes);
        }

        // GET api/<NetworkTypeController>/5
        [HttpGet("GetNetworkTypeById")]
        public async Task<IActionResult> Get(int id)
        {
            var networkType = await _networkTypeService.GetById(id);
            return Ok(networkType);

        }

        // POST api/<NetworkTypeController>
        [HttpPost("SaveNetworkType")]
        public async Task<IActionResult> Post([FromBody] SaveNetworkTypeDto networkType)
        {
            var network = await _networkTypeService.Save(networkType);
            return Ok(network);
        }


        // PUT api/<NetworkTypeController>/5
        [HttpPut("UpdateNetworkType")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateNetworkTypeDto networkTypes)
        {
            var networktype = await _networkTypeService.Update(networkTypes);
            return Ok(networktype);

        }


        // DELETE api/<NetworkTypeController>/5
        [HttpDelete("DeleteNetworkType")]
        public async Task<IActionResult> Delete(RemoveNetwokTypeDto NetworkTypeId)
        {
            var result = await _networkTypeService.Remove(NetworkTypeId);
            return Ok(result);
        }
    }

}