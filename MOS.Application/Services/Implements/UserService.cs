using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.Common;
using MOS.Application.DTOs.Requests.Users;
using MOS.Application.DTOs.Responses.Users;
using MOS.Application.Exceptions;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Domain.Enums;
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
        public async Task<PagedResult<UserExtentionResponse>> GetUserPagedAsync(UserQueryRequest query)
        {
            var pagedUsers = await _userRepository.GetUserPagedAsync(query);

            var userResponses = _mapper.Map<List<UserExtentionResponse>>(pagedUsers.Items);

            return new PagedResult<UserExtentionResponse>
            {
                Items = userResponses,
                TotalCount = pagedUsers.TotalCount,
                Page = pagedUsers.Page,
                PageSize = pagedUsers.PageSize
            };
        }

        // TODO: GetUserByIdAsync
        public async Task<UserExtentionResponse> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id)
                ?? throw new NotFoundException("User", id);
            return _mapper.Map<UserExtentionResponse>(user);
        }

        // TODO: CreateUserAsync
        public async Task<UserExtentionResponse> CreateUserAsync(CreateUserRequest request)
        {
            // check email taken
            if (await _userRepository.EmailExistsAsync(request.Email)) throw new ConflictException("User", "email");

            // create random password for new user
            var passwordHash = _passwordService.HashPassword(request.RandomPassword);

            // create new user
            var user = new User
            (
                request.Name,
                request.Email,
                passwordHash,
                request.UserName,
                request.Phone,
                request.TenantId,
                request.Role
            );
            await _userRepository.AddUserAsync(user);

            // assign product permissions if TenantUser
            if (request.Role == RoleType.TenantUser && request.ProductIds.Any())
            {
                foreach (var productId in request.ProductIds)
                {
                    var permission = new UserProductPermission(user.Id, productId, DateTime.UtcNow, PermissionLevel.Read);
                    await _permissionRepository.AddPermissionAsync(permission);
                }
            }

            // log audit

            await LogAudit(new List<User> { user }, CategoryLogType.Account, AuditAction.UserAdded);

            // log generated password for admin
            _logger.LogInformation(
                "User {Email} created with temporary password: {Password}",
                user.Email, request.RandomPassword);

            var response = _mapper.Map<UserExtentionResponse>(user);
            response.TemporaryPassword = request.RandomPassword;
            return response;

        }

        // TODO: UpdateAsync - takes id and UpdateUserRequest
        // update user, update permissions, log audit
        public async Task<UserExtentionResponse> UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            var user = await _userRepository.GetUserByIdAsync(id)
                ?? throw new NotFoundException("User", id);

            // update via entity method
            user.UpdateName(request.Name);
            user.UpdatePhone(request.Phone);
            user.UpdateUserId(request.UserName);
            user.ChangeRole(request.Role);
            await _userRepository.UpdateUserAsync(user);

            // remove old permissions and add new ones
            await _permissionRepository.RemovePermissionByIdAsync(user.Id);

            if (request.Role == RoleType.TenantUser && request.ProductIds.Any())
            {
                foreach (var productId in request.ProductIds)
                {
                    var permission = new UserProductPermission(
                        user.Id,
                        productId,
                        DateTime.UtcNow,
                        PermissionLevel.Read
                        );
                    await _permissionRepository.AddPermissionAsync(permission);
                }
            }

            // log audit
            await LogAudit(new List<User> { user }, CategoryLogType.Account, AuditAction.UserUpdated);

            // refetch user with updated permission for mapping
            var updatedUser = await _userRepository.GetUserByIdAsync(id);

            return _mapper.Map<UserExtentionResponse>(updatedUser);
        }

        // TODO BatchCreateUserAsync
        public async Task BatchCreateUserAsync(BatchCreateUserRequest request)
        {
            foreach (var createRequest in request.Users)
            {
                await CreateUserAsync(createRequest);
            }
        }


        // TODO: BatchDeleteAsync - takes BatchDeleteRequest
        // check users exist, delete, log audit
        public async Task BatchDeleteUserAsync(BatchDeleteRequest request)
        {
            // fetch BEFORE deleting
            var users = new List<User>();
            foreach (var id in request.UserIds)
            {
                var user = await _userRepository.GetUserByIdAsync(id)
                    ?? throw new NotFoundException("User", id);
                users.Add(user);
            }

            // now delete
            await _userRepository.DeleteUserRangeAsync(request.UserIds);

            // log with data already fetched
            await LogAudit(users, CategoryLogType.Account, AuditAction.UserDeleted);
        }

        // TODO: BatchDeactivateAsync - takes BatchDeactivateRequest
        // check users exist, deactivate, log audit
        public async Task BatchDeactivateUserAsync(BatchDeactivateRequest request)
        {
            // fetch BEFORE deleting
            var users = new List<User>();
            foreach (var id in request.UserIds)
            {
                var user = await _userRepository.GetUserByIdAsync(id)
                    ?? throw new NotFoundException("User", id);
                users.Add(user);
            }

            await _userRepository.DeactivateUserRangeAsync(request.UserIds);

            // log audit
            await LogAudit(users, CategoryLogType.Account, AuditAction.UserDeactivated);
        }

        public async Task BatchReactivateUserAsync(BatchReactivateRequest request)
        {
            var users = new List<User>();
            foreach (var id in request.UserIds)
            {
                var user = await _userRepository.GetUserByIdAsync(id)
                    ?? throw new NotFoundException("User", id);
                users.Add(user);
            }

            await _userRepository.ReactivateUserRangeAsync(request.UserIds);
            // log audit
            await LogAudit(users, CategoryLogType.Account, AuditAction.UserReactivated);
        }

        private async Task LogAudit(List<User> users, CategoryLogType type, AuditAction action)
        {
            foreach (var user in users)
            {
                await _auditRepository.AddAsync(new AuditLog(
                    GetUserIdFromJWT(),
                    user.Name,
                    user.UserName,
                    type.ToString(),
                    user.Email,
                    action,
                     $"User {user.Id} " + action.ToString()));
            }
        }
    }
}
