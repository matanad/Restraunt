using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly TableContext _context;
        public TableRepository(TableContext context)
        {
            _context = context;
        }

        public Task<List<Table>> GetAllTablesAsync()
        {
            return _context.Tables.ToListAsync();
        }

        public async Task<Table?> GetTableByIdAsync(int id)
        {
            return await _context.Tables.FindAsync(id).AsTask();
        }

        public async Task<Table> AddTableAsync(NewTable table)
        {
            var existingTable = await _context.Tables
                .FirstOrDefaultAsync(t => t.Number == table.Number);
            if (existingTable != null)
                throw new InvalidOperationException($"Table with number {table.Number} already exists.");

            var newTable = new Table
            {
                Number = table.Number,
                Capacity = table.Capacity,
                IsAvailable = true
            };
            try
            {
                var entry = _context.Tables.Add(newTable);
                await _context.SaveChangesAsync();
                return entry.Entity;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("error", ex);
            }
        }

        public async Task<Table> ReserveTableAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null)
            {
                throw new KeyNotFoundException("Table not found");
            }
            if (!table.IsAvailable)
            {
                throw new InvalidOperationException("Table is already reserved");
            }
            table.IsAvailable = false;
            await _context.SaveChangesAsync();
            return table;
        }
    }
}
