using MOS.Application.Common;
using MOS.Application.DTOs.Requests.Users;
using MOS.Application.DTOs.Responses.Users;

namespace MOS.Application.Services.Interfaces
{
    public interface IUserService
    {
        // GetPagedAsync - takes UserQueryRequest, returns PagedResult<UserResponse>
        Task<PagedResult<UserResponse>> GetUserPagedAsync(UserQueryRequest query);

        // GetByIdAsync - takes id, returns UserResponse
        // throws NotFoundException if not found
        Task<UserResponse> GetUserByIdAsync(int id);

        // CreateAsync - takes CreateUserRequest, returns UserResponse
        // generates random password, assigns permissions if TenantUser, logs audit
        Task<UserResponse> CreateUserAsync(CreateUserRequest request);

        // BatchCreateAsync - takes BatchCreateUserRequest
        // calls CreateAsync for each user
        Task BatchCreateUserAsync(BatchCreateUserRequest request);

        // UpdateAsync - takes id and UpdateUserRequest, returns UserResponse
        // updates user, updates permissions, logs audit
        Task<UserResponse> UpdateUserAsync(int id, UpdateUserRequest request);

        // BatchDeleteAsync - takes BatchDeleteRequest
        // checks users exist, soft deletes, logs audit
        Task BatchDeleteUserAsync(BatchDeleteRequest request);

        // BatchDeactivateAsync - takes BatchDeactivateRequest
        // checks users exist, deactivates, logs audit
        Task BatchDeactivateUserAsync(BatchDeactivateRequest request);
    }
}
