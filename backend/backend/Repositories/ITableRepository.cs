using backend.Models;

namespace backend.Repositories
{
    public interface ITableRepository
    {
        Task<Table> AddTableAsync(NewTable table);
        Task<List<Table>> GetAllTablesAsync();
        Task<Table?> GetTableByIdAsync(int id);
        Task<Table> ReserveTableAsync(int id);
    }
}