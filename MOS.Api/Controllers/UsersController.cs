using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOS.Api.Controllers;
using MOS.Api.EndPoints;
using MOS.Application.DTOs.Requests.Users;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Constants;
using MOS.Domain.Enums;


//[Authorize]
public class UsersController : BaseController<UsersController>
{
    private readonly IUserService _userService;

    public UsersController(IConfiguration configuration, ILogger<UsersController> logger, IUserService userService) : base(configuration, logger)
    {
        _userService = userService;
    }




    // GET api/users?page=1&pageSize=10&sortBy=name&search=john
    [HttpGet(Endpoints.UserEnpoints.FetchUsers)]
    public async Task<IActionResult> GetUsers([FromQuery] UserQueryRequest request)
    {
        // TODO: call _userService.GetPagedAsync
        // TODO: return 200 with PagedUserResponse
        var result = await _userService.GetUserPagedAsync(request);
        return Ok(result);
    }

    // GET api/users/{id}
    [HttpGet(Endpoints.UserEnpoints.GetUserById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        // TODO: call _userService.GetByIdAsync
        // TODO: return 200 with UserResponse
        var result = await _userService.GetUserByIdAsync(id);
        return Ok(result);
    }

    // POST api/users
    [HttpPost(Endpoints.UserEnpoints.CreateUser)]
    [Authorize(Policy = Permissions.AdminPolicy)]
    //[AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        // TODO: call _userService.CreateAsync
        // TODO: return 201 with created user
        var result = await _userService.CreateUserAsync(request);
        return StatusCode(201, result);
    }

    // POST api/users/batch
    [HttpPost(Endpoints.UserEnpoints.Batch)]
    [Authorize(Policy = Permissions.AdminPolicy)]
    //[AllowAnonymous]
    public async Task<IActionResult> BatchCreateUsers(
        [FromBody] BatchCreateUserRequest request)
    {
        // TODO: call _userService.BatchCreateAsync
        // TODO: return 201
        await _userService.BatchCreateUserAsync(request);
        return StatusCode(201);
    }

    // PUT api/users/{id}
    [HttpPut(Endpoints.UserEnpoints.UpdateUserById)]
    [Authorize(Policy = Permissions.AdminPolicy)]
    //[AllowAnonymous]
    public async Task<IActionResult> UpdateUser(
        Guid id, [FromBody] UpdateUserRequest request)
    {
        // TODO: call _userService.UpdateAsync
        // TODO: return 200 with updated user
        var result = await _userService.UpdateUserAsync(id, request);
        return Ok(result);
    }

    // DELETE api/v1/users/{id}
    [HttpDelete(Endpoints.UserEnpoints.DeleteUserById)]
    [Authorize(Policy = Permissions.AdminPolicy)]
    //[AllowAnonymous]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        // just wrap it as a batch call with one id
        await _userService.BatchDeleteUserAsync(new BatchDeleteRequest
        {
            UserIds = new List<Guid> { id }
        });
        return NoContent();
    }

    // DELETE api/users/batch
    [HttpDelete(Endpoints.UserEnpoints.DeleteUserByBatch)]
    [Authorize(Policy = Permissions.AdminPolicy)]
    //[AllowAnonymous]
    public async Task<IActionResult> BatchDelete([FromBody] BatchDeleteRequest request)
    {
        // TODO: call _userService.BatchDeleteAsync
        // TODO: return 204 no content
        await _userService.BatchDeleteUserAsync(request);
        return NoContent();
    }

    [HttpPut(Endpoints.UserEnpoints.DeActiveUserById)]
    [Authorize(Policy = Permissions.AdminPolicy)]
    //[AllowAnonymous]
    public async Task<IActionResult> DeactivateUser(Guid id)
    {
        await _userService.BatchDeactivateUserAsync(new BatchDeactivateRequest
        {
            UserIds = new List<Guid> { id }
        });

        return NoContent();
    }

    // PUT api/users/batch/deactivate
    [HttpPut(Endpoints.UserEnpoints.DeActiveUserByBatch)]
    [Authorize(Policy = Permissions.AdminPolicy)]
    //[AllowAnonymous]
    public async Task<IActionResult> BatchDeactivate(
        [FromBody] BatchDeactivateRequest request)
    {
        // TODO: call _userService.BatchDeactivateAsync
        // TODO: return 204 no content
        await _userService.BatchDeactivateUserAsync(request);
        return NoContent();
    }


    [HttpPut(Endpoints.UserEnpoints.ReActiveUserById)]
    [Authorize(Policy = Permissions.AdminPolicy)]
    //[AllowAnonymous]
    public async Task<IActionResult> ReactivateUser(Guid id)
    {
        await _userService.BatchReactivateUserAsync(new BatchReactivateRequest
        {
            UserIds = new List<Guid> { id }
        });

        return NoContent();
    }

    [HttpPut(Endpoints.UserEnpoints.ReActiveUserByBatch)]
    [Authorize(Policy = Permissions.AdminPolicy)]
    //[AllowAnonymous]
    public async Task<IActionResult> BatchReactivate([FromBody] BatchReactivateRequest request)
    {
        await _userService.BatchReactivateUserAsync(request);
        return NoContent();
    }

    [HttpPut("{userId:guid}/permissions")]
    [Authorize(Roles = "Administrator,TenantAdministrator")]
    public async Task<IActionResult> UpdateUserProductPermissions(
    Guid userId,
    [FromBody] UpdateUserProductPermissionsRequest request)
    {
        await _userService.UpdateUserProductPermissionsAsync(userId, request);
        return NoContent();
    }
    
    [HttpPost(Endpoints.UserEnpoints.ImportUsers)]
    [Authorize(Policy = Permissions.AdminPolicy)]
    public async Task<IActionResult> ImportUsers(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");

        if (!file.FileName.EndsWith(".xlsx"))
            return BadRequest("Only .xlsx files are supported");

        using var stream = file.OpenReadStream();
        var result = await _userService.ImportUsersFromExcelAsync(stream);

        return Ok(result);
    }

    [HttpPost(Endpoints.UserEnpoints.ExportUsers)]
    [Authorize(Policy = Permissions.AdminPolicy)]
    public async Task<IActionResult> ExportUsers([FromBody] List<UserExportRequest> users)
    {
        if (users == null || !users.Any())
            return BadRequest("No data to export");

        var fileBytes = await _userService.ExportUsersToExcelAsync(users);

        return File(
            fileBytes,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"users_export_{DateTime.UtcNow:yyyyMMdd_HHmmss}.xlsx"
        );
    }
}
