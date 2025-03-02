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
        public readonly INetworkTypeRepository _networkTypeRepository;
        public readonly ILogger<NetworkTypeController> _logger;
        public readonly IConfiguration _configuration;
        public NetworkTypeController(INetworkTypeRepository networkTypeRepository, 
                                     IConfiguration configuration, ILogger<NetworkTypeController> logger) 
        {
            _logger = logger;
            _configuration = configuration;
            _networkTypeRepository = networkTypeRepository;

        }
        // GET: api/<NetworkTypeController>
        [HttpGet("GetNeworkType")]
        public async Task<IActionResult> Get()
        {
            var networkTypes = await _networkTypeRepository.GetAllAsync();
            return Ok(networkTypes);
        }

        // GET api/<NetworkTypeController>/5
        [HttpGet("GetNetworkTypeById")]
        public async Task<IActionResult> Get(int id)
        {
            var networkType = await _networkTypeRepository.GetEntityByIdAsync(id);
            return Ok(networkType);

        }

        // POST api/<NetworkTypeController>
        [HttpPost("SaveNetworType")]
        public async Task<IActionResult> Post([FromBody] NetworkType networkType)
        {
            var network= await _networkTypeRepository.SaveEntityAsync(networkType);
            return Ok(network);
        }


        // PUT api/<NetworkTypeController>/5
        [HttpPut("UpdateNetworType")]
        public async Task<IActionResult> Put(int id, [FromBody] NetworkType networkTypes)
        {
            var networktype = await _networkTypeRepository.UpdateEntityAsync(id, networkTypes);
            return Ok(networktype);

        }


        // DELETE api/<NetworkTypeController>/5
        [HttpDelete("DeleteNetworkType")]
        public async Task<IActionResult> Delete(int NetworkTypeId)
        {
            var result = await _networkTypeRepository.DeleteEntityByIdAsync(NetworkTypeId);
            return Ok(result);
        }
    }

}