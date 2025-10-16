using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repo;

        public TasksController(ITaskRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var t = await _repo.GetByIdAsync(id);
            return t is null ? NotFound() : Ok(t);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Titulo) || item.Titulo.Length > 100)
                return BadRequest("Título é obrigatório e deve ter no máximo 100 caracteres.");

            var allTasks = await _repo.GetAllAsync();
            bool exists = allTasks.Any(t =>
                t.Titulo.Trim().ToLower() == item.Titulo.Trim().ToLower());

            if (exists)
                return Conflict("Já existe uma tarefa com esse título.");
            
            TimeZoneInfo brasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Brazil/East");
            DateTime brazilianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brasiliaTimeZone);

            item.DataCriacao = brazilianTime;

            if (item.DataConclusao.HasValue)
            {
                var criacao = item.DataCriacao.Date;
                var conclusao = item.DataConclusao.Value.Date;

                if (conclusao < criacao)
                    return BadRequest("Data de conclusão não pode ser anterior à data de criação.");
            }

            await _repo.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskItem item)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(item.Titulo) || item.Titulo.Length > 100)
                return BadRequest("Título é obrigatório e deve ter no máximo 100 caracteres.");

            var allTasks = await _repo.GetAllAsync();
            bool duplicate = allTasks.Any(t =>
                t.Id != id &&
                t.Titulo.Trim().ToLower() == item.Titulo.Trim().ToLower());

            if (duplicate)
                return Conflict("Já existe uma tarefa com esse título.");

            if (item.DataConclusao.HasValue)
            {
                var criacao = existing.DataCriacao.Date;
                var conclusao = item.DataConclusao.Value.Date;

                if (conclusao < criacao)
                    return BadRequest("Data de conclusão não pode ser anterior à data de criação.");
            }

            existing.Titulo = item.Titulo.Trim();
            existing.Descricao = item.Descricao;
            existing.Status = item.Status;
            existing.DataConclusao = item.DataConclusao;

            await _repo.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
