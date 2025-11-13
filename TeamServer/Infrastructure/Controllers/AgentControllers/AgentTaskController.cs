using ApiModels.DTOs.AgentDTOs;
using Microsoft.AspNetCore.Mvc;
using TeamServer.Application.Services.AgentServices.AgentCore;
using TeamServer.Application.Services.AgentServices.AgentCRUD;
using TeamServer.Domain.Entities.Agents;

namespace TeamServer.Infrastructure.Controllers.AgentControllers
{
    [ApiController]
    [Route("agent/{id}/tasks")]
    public class AgentTaskController : ControllerBase
    {
        private readonly IAgentCRUD _agentCRUD;
        private readonly IAgentCore _agentCore;

        public AgentTaskController(IAgentCRUD agentCRUD, IAgentCore agentCore)
        {
            _agentCRUD = agentCRUD;
            _agentCore = agentCore;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskResults(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("The id is invalid");

            var agent = await _agentCRUD.GetAgentAsync(id);
            if (agent == null) return NotFound($"Agent with the id: {id} not found");

            var results = await _agentCore.GetTaskResults();
            if (results == null || !results.Any())
                return Ok(Array.Empty<AgentTaskResult>);

            return Ok(results);
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskResult(Guid taskId, Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("The id is invalid");

            if (taskId == Guid.Empty)
                return BadRequest("The id is invalid");

            var agent = await _agentCRUD.GetAgentAsync(id);
            if (agent == null)
                return NotFound($"No agent with the id: {id} exists");

            var result = await _agentCore.GetTaskResult(taskId);
            if (result == null)
                return NotFound($"No result with the id: {taskId} exists");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> TaskAgent([FromBody] AgentTaskDTO dto, Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("The id is invalid");

            var agent = await _agentCRUD.GetAgentAsync(id);
            if (agent == null)
                return NotFound($"No agent with the id: {id} exists");

            var task = new AgentTask
            {
                Id = Guid.NewGuid(),
                Command = dto.Command,
                Arguments = dto.Arguments,
                File = dto.File
            };

            await _agentCore.QueueTask(task);

            return CreatedAtAction(nameof(GetTaskResult), new { id, taskId = task.Id }, task);
        }
    }
}
