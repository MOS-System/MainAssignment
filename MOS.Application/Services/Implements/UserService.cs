using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.Services.Interfaces;
using MOS.Infrastructure.Interfaces;


namespace MOS.Application.Services.Implements
{
    // CRUD, batch delete, batch deactivate
    public class UserService : BaseService<UserService>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly IPasswordService _passwordService;

        public UserService(
            IUserRepository userRepository,
            IPermissionRepository permissionRepository,
            IAuditRepository auditRepository,
            IPasswordService passwordService,
            ILogger<UserService> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(logger, mapper, httpContextAccessor, configuration)
        {
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _auditRepository = auditRepository;
            _passwordService = passwordService;
        }

        // TODO: GetPagedAsync - takes UserQueryRequest, returns PagedResult<UserResponse>

        // TODO: GetByIdAsync - takes id, returns UserResponse
        // throw NotFoundException if not found

        // TODO: CreateAsync - takes CreateUserRequest
        // generate random password, assign permissions if TenantUser, log audit

        // TODO: BatchCreateAsync - takes BatchCreateUserRequest
        // call CreateAsync for each user

        // TODO: UpdateAsync - takes id and UpdateUserRequest
        // update user, update permissions, log audit

        // TODO: BatchDeleteAsync - takes BatchDeleteRequest
        // check users exist, delete, log audit

        // TODO: BatchDeactivateAsync - takes BatchDeactivateRequest
        // check users exist, deactivate, log audit
    }
}
