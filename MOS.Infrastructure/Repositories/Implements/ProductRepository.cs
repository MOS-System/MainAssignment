using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Infrastructure.Db;

namespace MOS.Infrastructure.Repositories.Implements
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        // TODO: GetAllAsync
        // TODO: GetByIdAsync
    }
}