using MOS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.Services
{
    // tenant/account management
    public class TenantService
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantService(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        // TODO: GetByIdAsync - takes id, returns TenantResponse
        // throw NotFoundException if not found

        // TODO: CreateAsync - takes CreateTenantRequest, returns TenantResponse
        // check name not duplicate, create tenant
    }
}
