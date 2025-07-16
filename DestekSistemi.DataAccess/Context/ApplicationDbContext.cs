using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// DestekSistemi.DataAccess/Context/ApplicationDbContext.cs

using DestekSistemi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DestekSistemi.DataAccess.Context
{
    // IdentityDbContext'ten miras alıyoruz. Bu sayede hem Identity'nin User, Role gibi
    // tablolarını hem de kendi tablolarımızı tek bir context üzerinden yönetebiliriz.
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Veritabanında "Talepler" isminde bir tablo oluşturulacak
        // ve bu tablo Talep sınıfı ile eşleşecek.
        public DbSet<Talep> Talepler { get; set; }

        // Yorumlar tablosunu da şimdiden ekleyelim.
        // public DbSet<Yorum> Yorumlar { get; set; }

        // YENİ EKLENEN SATIR:
        public DbSet<Yorum> Yorumlar { get; set; }

    }
}