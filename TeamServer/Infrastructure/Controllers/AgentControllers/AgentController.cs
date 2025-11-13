using Microsoft.AspNetCore.Mvc;
using TeamServer.Application.Services.AgentServices.AgentCRUD;
using TeamServer.Domain.Entities.Agents;

namespace TeamServer.Infrastructure.Controllers.AgentControllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly IAgentCRUD _agentCRUD;

        public AgentController(IAgentCRUD agentCRUD) => _agentCRUD = agentCRUD;

        [HttpGet]
        public async Task<IActionResult> GetAgents()
        {
            var agents = await _agentCRUD.GetAgentsAsync();
            if (agents == null || !agents.Any())
                return Ok(Array.Empty<Agent>());

            return Ok(agents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgent(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest($"The id: {id} is invalid");

            var agent = await _agentCRUD.GetAgentAsync(id);
            if (agent == null) 
                return NotFound($"No agent exists with the id: {id}");

            return Ok(agent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAgent(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest($"The id: {id} is invalid");

            var deleted = await _agentCRUD.RemoveAgentAsync(id);
            if (!deleted)
                return BadRequest($"Cannot delete. No agent with the id: {id} exists");

            return NoContent();
        }
    }
}
