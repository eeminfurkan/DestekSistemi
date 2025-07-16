using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DestekSistemi.Entities;

namespace DestekSistemi.DataAccess.Repositories
{
    public interface ITalepRepository
    {
        Task AddAsync(Talep talep);
        Task<List<Talep>> GetAllByUserIdAsync(string kullaniciId);
        Task<List<Talep>> GetAllAsync();
        Task<Talep> GetByIdAsync(int id);

        // Gelecekte eklenecek diğer metotlar:
        // Task<Talep> GetByIdAsync(int id);
        // Task<List<Talep>> GetAllAsync();
        // Task UpdateAsync(Talep talep);
        // Task DeleteAsync(int id);
    }
}
