using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableRepository tableRepository;
        public TableController(ITableRepository tableRepository)
        {
            this.tableRepository = tableRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTables()
        {
            try
            {
                var tables = await tableRepository.GetAllTablesAsync();
                return Ok(tables);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            try
            {
                var table = await tableRepository.GetTableByIdAsync(id);
                return table == null ? NotFound() : Ok(table);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddTable([FromBody] NewTable table)
        {
            try
            {
                var createdTable = await tableRepository.AddTableAsync(table);
                return CreatedAtAction(nameof(GetTableById), new { id = createdTable.Id }, createdTable);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
