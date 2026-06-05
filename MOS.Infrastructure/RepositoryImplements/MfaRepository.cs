using Microsoft.EntityFrameworkCore;
using MOS.Domain.Entities;
using MOS.Infrastructure.Db;
using MOS.Infrastructure.Interfaces;
using System.Security.Cryptography;


namespace MOS.Infrastructure.Implements
{
    public class MfaRepository : IMfaRepository
    {
        private readonly AppDbContext _context;
        public MfaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateMfaCode(Guid userId)
        {
            var randomCode = await GenerateUniqueCodeAsync();
            var mewMfaCode = new MfaCode(userId, randomCode);

            await _context.MfaCodes.AddAsync(mewMfaCode);
            await _context.SaveChangesAsync();

            return randomCode;
        }
        public async Task UpdateMfaCodeStatus(Guid userId, string code)
        {
            var mfaCode = await _context.MfaCodes
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Code == code && !c.IsUsed);

            if (mfaCode != null)
            {
                mfaCode.MarkAsUsed();
                _context.MfaCodes.Update(mfaCode);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<bool> VerifyMfaCode(Guid userId, string code)
        {
            var mfaCode = await _context.MfaCodes.Where(m =>
             m.UserId == userId &&
             m.Code == code &&
            !m.IsUsed &&
             m.ExpiresAt > DateTime.UtcNow)
            .FirstOrDefaultAsync();

            if (mfaCode == null) return false;
            return true;
        }

        private async Task<string> GenerateUniqueCodeAsync()
        {
            string code;
            bool exists;

            do
            {
                code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

                exists = await _context.MfaCodes
                    .AnyAsync(c => c.Code == code && !c.IsUsed && c.ExpiresAt > DateTime.UtcNow);
            }
            while (exists);

            return code;
        }

    }
}
