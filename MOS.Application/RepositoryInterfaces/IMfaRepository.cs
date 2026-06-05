using MOS.Domain.Entities;

namespace MOS.Infrastructure.Interfaces
{
    public interface IMfaRepository
    {
        Task<string> GenerateMfaCode(Guid userId);
        Task<bool> VerifyMfaCode(Guid userId, string code);
        Task UpdateMfaCodeStatus(Guid userId, string code);
    }
}
