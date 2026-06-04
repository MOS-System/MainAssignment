using MOS.Application.Common;
using MOS.Application.DTOs.Requests.Users;
using MOS.Application.DTOs.Responses.Users;

namespace MOS.Application.Services.Interfaces
{
    public interface IUserService
    {
        // GetPagedAsync - takes UserQueryRequest, returns PagedResult<UserResponse>
        Task<PagedResult<UserExtentionResponse>> GetUserPagedAsync(UserQueryRequest query);

        // GetByIdAsync - takes id, returns UserResponse
        // throws NotFoundException if not found
        Task<UserExtentionResponse> GetUserByIdAsync(Guid id);

        // CreateAsync - takes CreateUserRequest, returns UserResponse
        // generates random password, assigns permissions if TenantUser, logs audit
        Task<UserExtentionResponse> CreateUserAsync(CreateUserRequest request);

        // BatchCreateAsync - takes BatchCreateUserRequest
        // calls CreateAsync for each user
        Task BatchCreateUserAsync(BatchCreateUserRequest request);

        // UpdateAsync - takes id and UpdateUserRequest, returns UserResponse
        // updates user, updates permissions, logs audit
        Task<UserExtentionResponse> UpdateUserAsync(Guid id, UpdateUserRequest request);

        // BatchDeleteAsync - takes BatchDeleteRequest
        // checks users exist, soft deletes, logs audit
        Task BatchDeleteUserAsync(BatchDeleteRequest request);

        // BatchDeactivateAsync - takes BatchDeactivateRequest
        // checks users exist, deactivates, logs audit
        Task BatchDeactivateUserAsync(BatchDeactivateRequest request);

        Task BatchReactivateUserAsync(BatchReactivateRequest request);

        Task<ImportResultResponse> ImportUsersFromExcelAsync(Stream fileStream);
        Task<byte[]> ExportUsersToExcelAsync(List<UserExportRequest> users);
    }
}
