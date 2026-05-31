using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.Services.Interfaces;


namespace MOS.Application.Services.Implements
{
    // generate code, validate, expire
    public class MfaService : BaseService<MfaService>, IMfaService
    {
       private readonly IMfaRepository _IMfaRepository;
        public MfaService(IMfaRepository mfaRepository, 
            ILogger<MfaService> logger, 
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration) : base(logger, mapper, httpContextAccessor, configuration)
        {
            _IMfaRepository = mfaRepository;
        }
    }
}
