﻿using System;
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

        // YENİ METOT 1: Sadece belirli bir kullanıcının taleplerini getirir.
        public async Task<List<Talep>> GetAllByUserIdAsync(string kullaniciId)
        {
            return await _context.Talepler
                                 .Where(t => t.KullaniciId == kullaniciId)
                                 .OrderByDescending(t => t.OlusturmaTarihi) // En yeni talepler üstte olsun
                                 .ToListAsync();
        }

        // YENİ METOT 2: Tüm talepleri getirir (Admin için).
        public async Task<Talep> GetByIdAsync(int id)
        {
            return await _context.Talepler
                                 .Include(t => t.Yorumlar) // YORUMLARI DA GETİR
                                 .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Talep>> GetAllAsync()
        {
            return await _context.Talepler
                                 .Include(t => t.Yorumlar) // YORUMLARI DA GETİR
                                 .OrderByDescending(t => t.OlusturmaTarihi)
                                 .ToListAsync();
        }

        // YENİ METOT:
        public async Task UpdateAsync(Talep talep)
        {
            _context.Talepler.Update(talep);
            await _context.SaveChangesAsync();
        }

    }
}
