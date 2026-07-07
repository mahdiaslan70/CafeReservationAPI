using CafeReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Domain.Repositories
{
    public interface IMenuItemRepository
    {
        Task<int> AddAsync(MenuItem menuItem);
        Task<IEnumerable<MenuItem>> GetAllAsync();
        
    }
}
