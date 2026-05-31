using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.DTOs.Requests.Users;
using MOS.Application.Services;
using MOS.Domain.Constants;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    // GET api/users?page=1&pageSize=10&sortBy=name&search=john
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] UserQueryRequest request)
    {
        // TODO: call _userService.GetPagedAsync
        // TODO: return 200 with PagedUserResponse
        throw new NotImplementedException();
    }

    // GET api/users/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // TODO: call _userService.GetByIdAsync
        // TODO: return 200 with UserResponse
        throw new NotImplementedException();
    }

    // POST api/users
    [HttpPost]
    [Authorize(Policy = Permissions.AdminPolicy)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        // TODO: call _userService.CreateAsync
        // TODO: return 201 with created user
        throw new NotImplementedException();
    }

    // POST api/users/batch
    [HttpPost("batch")]
    [Authorize(Policy = Permissions.AdminPolicy)]
    public async Task<IActionResult> BatchCreateUsers(
        [FromBody] BatchCreateUserRequest request)
    {
        // TODO: call _userService.BatchCreateAsync
        // TODO: return 201
        throw new NotImplementedException();
    }

    // PUT api/users/{id}
    [HttpPut("{id}")]
    [Authorize(Policy = Permissions.AdminPolicy)]
    public async Task<IActionResult> UpdateUser(
        int id, [FromBody] UpdateUserRequest request)
    {
        // TODO: call _userService.UpdateAsync
        // TODO: return 200 with updated user
        throw new NotImplementedException();
    }

    // DELETE api/users/batch
    [HttpDelete("batch")]
    [Authorize(Policy = Permissions.AdminPolicy)]
    public async Task<IActionResult> BatchDelete([FromBody] BatchDeleteRequest request)
    {
        // TODO: call _userService.BatchDeleteAsync
        // TODO: return 204 no content
        throw new NotImplementedException();
    }

    // PUT api/users/batch/deactivate
    [HttpPut("batch/deactivate")]
    [Authorize(Policy = Permissions.AdminPolicy)]
    public async Task<IActionResult> BatchDeactivate(
        [FromBody] BatchDeactivateRequest request)
    {
        // TODO: call _userService.BatchDeactivateAsync
        // TODO: return 204 no content
        throw new NotImplementedException();
    }
}
