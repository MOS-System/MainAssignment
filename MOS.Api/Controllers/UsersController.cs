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

    public UsersController(IUserService userService, ILogger<UsersController> logger) : base(logger)
    {
        _userService = userService;
    }


    // GET api/users?page=1&pageSize=10&sortBy=name&search=john
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] UserQueryRequest request)
    {
        // TODO: call _userService.GetPagedAsync
        // TODO: return 200 with PagedUserResponse
        var result = await _userService.GetUserPagedAsync(request);
        return Ok(result);
    }

    // GET api/users/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // TODO: call _userService.GetByIdAsync
        // TODO: return 200 with UserResponse
        var result = await _userService.GetUserByIdAsync(id);
        return Ok(result);
    }

    // POST api/users
    [HttpPost(Endpoints.UserEnpoints.CreateUser)]
    //[Authorize(Policy = Permissions.AdminPolicy)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        // TODO: call _userService.CreateAsync
        // TODO: return 201 with created user
        var result = await _userService.CreateUserAsync(request);
        return StatusCode(201, result);
    }

    // POST api/users/batch
    [HttpPost("batch")]
    //[Authorize(Policy = Permissions.AdminPolicy)]
    [AllowAnonymous]
    public async Task<IActionResult> BatchCreateUsers(
        [FromBody] BatchCreateUserRequest request)
    {
        // TODO: call _userService.BatchCreateAsync
        // TODO: return 201
        await _userService.BatchCreateUserAsync(request);
        return StatusCode(201);
    }

    // PUT api/users/{id}
    [HttpPut("{id}")]
    //[Authorize(Policy = Permissions.AdminPolicy)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateUser(
        int id, [FromBody] UpdateUserRequest request)
    {
        // TODO: call _userService.UpdateAsync
        // TODO: return 200 with updated user
        var result = await _userService.UpdateUserAsync(id, request);
        return Ok(result);
    }

    // DELETE api/v1/users/{id}
    [HttpDelete("{id}")]
    //[Authorize(Policy = Permissions.AdminPolicy)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteUser(int id)
    {
        // just wrap it as a batch call with one id
        await _userService.BatchDeleteUserAsync(new BatchDeleteRequest
        {
            UserIds = new List<int> { id }
        });
        return NoContent();
    }

    // DELETE api/users/batch
    [HttpDelete("batch")]
    //[Authorize(Policy = Permissions.AdminPolicy)]
    [AllowAnonymous]
    public async Task<IActionResult> BatchDelete([FromBody] BatchDeleteRequest request)
    {
        // TODO: call _userService.BatchDeleteAsync
        // TODO: return 204 no content
        await _userService.BatchDeleteUserAsync(request);
        return NoContent();
    }

    [HttpPut("{id}/deactivate")]
    //[Authorize(Policy = Permissions.AdminPolicy)]
    [AllowAnonymous]
    public async Task<IActionResult> DeactivateUser(int id)
    {
        await _userService.BatchDeactivateUserAsync(new BatchDeactivateRequest
        {
            UserIds = new List<int> { id }
        });

        return NoContent();
    }

    // PUT api/users/batch/deactivate
    [HttpPut("batch/deactivate")]
    //[Authorize(Policy = Permissions.AdminPolicy)]
    [AllowAnonymous]
    public async Task<IActionResult> BatchDeactivate(
        [FromBody] BatchDeactivateRequest request)
    {
        // TODO: call _userService.BatchDeactivateAsync
        // TODO: return 204 no content
        await _userService.BatchDeactivateUserAsync(request);
        return NoContent();
    }


    [HttpPut("{id}/reactivate")]
    //[Authorize(Policy = Permissions.AdminPolicy)]
    [AllowAnonymous]
    public async Task<IActionResult> ReactivateUser(int id)
    {
        await _userService.BatchReactivateUserAsync(new BatchReactivateRequest
        {
            UserIds = new List<int> { id }
        });

        return NoContent();
    }

    [HttpPut("batch/reactivate")]
    //[Authorize(Policy = Permissions.AdminPolicy)]
    [AllowAnonymous]
    public async Task<IActionResult> BatchReactivate([FromBody] BatchReactivateRequest request)
    {
        await _userService.BatchReactivateUserAsync(request);
        return NoContent();
    }
}
