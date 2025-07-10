using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DestekSistemi.DataAccess.Context;
using DestekSistemi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DestekSistemi.DataAccess.Repositories
{
    public class TalepRepository : ITalepRepository
    {
        private readonly ApplicationDbContext _context;

        public TalepRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Talep talep)
        {
            await _context.Talepler.AddAsync(talep);
            await _context.SaveChangesAsync();
        }
    }
}
