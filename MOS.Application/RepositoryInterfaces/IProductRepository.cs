using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        // TODO: GetByIdAsync
    }
}
