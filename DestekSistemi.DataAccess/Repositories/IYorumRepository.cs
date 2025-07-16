using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DestekSistemi.Entities;

namespace DestekSistemi.DataAccess.Repositories
{
    public interface IYorumRepository
    {
        Task AddAsync(Yorum yorum);
    }
}
