using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DestekSistemi.DataAccess.Context;
using DestekSistemi.Entities;

namespace DestekSistemi.DataAccess.Repositories
{
    public class YorumRepository : IYorumRepository
    {
        private readonly ApplicationDbContext _context;

        public YorumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Yorum yorum)
        {
            await _context.Yorumlar.AddAsync(yorum);
            await _context.SaveChangesAsync();
        }
    }
}
