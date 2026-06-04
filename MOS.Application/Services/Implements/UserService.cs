using AutoMapper;
using BCrypt.Net;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.Common;
using MOS.Application.DTOs.Requests.Users;
using MOS.Application.DTOs.Responses.Users;
using MOS.Application.Exceptions;
using MOS.Application.ExternalServices.SecurityInterfaces;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Domain.Enums;
using MOS.Infrastructure.Interfaces;
using System.Text.RegularExpressions;


namespace MOS.Application.Services.Implements
{
    // CRUD, batch delete, batch deactivate
    public class UserService : BaseService<UserService>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly IPasswordService _passwordService;
        private readonly IEmailService _emailService;
        private readonly ITenantRepository _tenantRepository;

        public UserService(
            IUserRepository userRepository,
            IPermissionRepository permissionRepository,
            IAuditRepository auditRepository,
            IPasswordService passwordService,
            IEmailService emailService,
            ITenantRepository tenantRepository,
            ILogger<UserService> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(logger, mapper, httpContextAccessor, configuration)
        {
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _auditRepository = auditRepository;
            _passwordService = passwordService;
            _emailService = emailService;
            _tenantRepository = tenantRepository;
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
                request.Role,
                SigninMethod.local
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


            await _emailService.SendEmailAsync(
                user.Email,
                "Your MOS account has been created",
                $"Hello {user.Name},\n\n" +
                "Your MOS account has been created.\n\n" +
                $"Username: {user.UserName}\n" +
                $"Temporary password: {request.RandomPassword}\n\n" +
                "Please log in using the provided information above."
            );

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

        public async Task<ImportResultResponse> ImportUsersFromExcelAsync(Stream fileStream)
        {
            var result = new ImportResultResponse();
            var wb = new XLWorkbook(fileStream); // uses ClosedXML
            var ws = wb.Worksheet("Users Import");

            // data starts at row 10 (rows 1-9 are headers/examples/separator)
            var rows = ws.RowsUsed().Where(r => r.RowNumber() >= 4);
            var rowNum = 0;
            foreach (var row in rows)
            {
                rowNum++;
                result.TotalRows++;
                try
                {
                    var userName = row.Cell(1).GetString().Trim();
                    var name = row.Cell(2).GetString().Trim();
                    var email = row.Cell(3).GetString().Trim();
                    var phone = row.Cell(4).GetString().Trim();
                    var password = row.Cell(5).GetString().Trim();
                    var signinMethod = row.Cell(6).GetString().Trim();
                    var role = row.Cell(7).GetString().Trim();
                    var tenantIdStr = row.Cell(8).GetString().Trim();

                    if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(email))
                    {
                        result.TotalRows--;
                        continue;
                    }

                    // ── Validate ──────────────────────────────────────
                    var validationErrors = ValidateUserRow(userName, name, email, phone, password, signinMethod, role, tenantIdStr);
                    if (validationErrors.Any())
                    {
                        result.FailedRows++;
                        // report all validation errors for this row together
                        result.ErrorLogs.Add($"Row {rowNum}: {string.Join(" | ", validationErrors)}");
                        continue; // skip to next row, don't save this one
                    }

                    // ── Check duplicate email ─────────────────────────
                    var emailExisting = await _userRepository.GetUserByEmailAsync(email);
                    if (emailExisting != null)
                    {
                        result.FailedRows++;
                        result.ErrorLogs.Add($"Row {rowNum}: Email {email} already exists");
                        continue;
                    }

                    var tenantExisting = await _tenantRepository.GetTenantByIdAsync(Guid.TryParse(tenantIdStr, out var tid) ? tid : Guid.Empty);
                    if (tenantExisting == null)
                    {
                        result.FailedRows++;
                        result.ErrorLogs.Add($"Row {rowNum}: Tenant Id {tenantIdStr} not exists");
                        continue;
                    }


                    // ── Hash password ─────────────────────────────────
                    var passwordHash = _passwordService.HashPassword(password);

                    // ── Parse enums ───────────────────────────────────
                    var signinMethodEnum = signinMethod == "1" ? SigninMethod.local : SigninMethod.microsoft;
                    var roleEnum = role switch
                    {
                        "1" => RoleType.Administrator,
                        "2" => RoleType.TenantAdministrator,
                        _ => RoleType.TenantUser
                    };

                    // ── Save ──────────────────────────────────────────
                    var user = new User(name, email, passwordHash, userName, phone, tenantExisting.Id, roleEnum, signinMethodEnum);
                    await _userRepository.AddUserAsync(user);
                    result.SuccessRows++;
                }
                catch (Exception ex)
                {
                    result.FailedRows++;
                    result.ErrorLogs.Add($"Row {row.RowNumber()}: {ex.InnerException}");
                }
            }


            return result;
        }

        private List<string> ValidateUserRow(string userName, string name, string email,
      string phone, string password, string signinMethod, string role, string tenantIdStr)
        {
            var errors = new List<string>();

            // UserName
            if (string.IsNullOrEmpty(userName))
                errors.Add("UserId is required");
            else if (userName.Length > 50)
                errors.Add("UserId must be less than 50 characters");

            // Name
            if (string.IsNullOrEmpty(name))
                errors.Add("Name is required");
            else if (name.Length > 200)
                errors.Add("Name must be less than 200 characters");

            // TenantId
            if (string.IsNullOrEmpty(tenantIdStr))
                errors.Add("TenantId is required and cannot be empty");
            else if (!Guid.TryParse(tenantIdStr, out _))
                errors.Add("TenantId must be a valid GUID");

            // Email
            if (string.IsNullOrEmpty(email))
                errors.Add("Email is required");
            else if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errors.Add("Email must be a valid email address");
            else if (email.Length > 200)
                errors.Add("Email must be less than 200 characters");

            // Password (blank = auto-generated, skip rules; provided = must meet all rules)
            if (!string.IsNullOrEmpty(password))
            {
                if (password.Length < 8)
                    errors.Add("Password must be at least 8 characters long");
                if (password.Length > 100)
                    errors.Add("Password must be less than 100 characters");
                if (!Regex.IsMatch(password, "[A-Z]"))
                    errors.Add("Password must contain at least one uppercase letter");
                if (!Regex.IsMatch(password, "[a-z]"))
                    errors.Add("Password must contain at least one lowercase letter");
                if (!Regex.IsMatch(password, "[0-9]"))
                    errors.Add("Password must contain at least one number");
                if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
                    errors.Add("Password must contain at least one special character");
            }

            // Phone
            if (string.IsNullOrEmpty(phone))
                errors.Add("Phone number is required");
            else if (!Regex.IsMatch(phone, @"^\+?[0-9]{7,15}$"))
                errors.Add("Phone number must be valid and contain 7-15 digits");

            // SigninMethod
            if (string.IsNullOrEmpty(signinMethod))
                errors.Add("SigninMethod is required");
            else if (signinMethod != "0" && signinMethod != "1")
                errors.Add("SigninMethod must be 0 (Local) or 1 (Microsoft)");

            // Role
            if (string.IsNullOrEmpty(role))
                errors.Add("Role is required");
            else if (role != "0" && role != "1" && role != "2")
                errors.Add("Role must be 0 (Admin), 1 (Manager), or 2 (User)");

            return errors;
        }
        public Task<byte[]> ExportUsersToExcelAsync(List<UserExportRequest> users)
        {
            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Users");

            // ── Title row ──────────────────────────────────────────
            ws.Range("A1:F1").Merge();
            ws.Cell("A1").Value = "MOS — User Export";
            ws.Cell("A1").Style
                .Font.SetBold(true)
                .Font.SetFontSize(14)
                .Font.SetFontColor(XLColor.White)
                .Fill.SetBackgroundColor(XLColor.FromHtml("#1F3864"))
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            ws.Row(1).Height = 30;

            // ── Subtitle / export date ──────────────────────────────
            ws.Range("A2:F2").Merge();
            ws.Cell("A2").Value = $"Exported on {DateTime.UtcNow:yyyy-MM-dd HH:mm} UTC  |  Total records: {users.Count}";
            ws.Cell("A2").Style
                .Font.SetFontSize(9)
                .Font.SetItalic(true)
                .Font.SetFontColor(XLColor.FromHtml("#595959"))
                .Fill.SetBackgroundColor(XLColor.FromHtml("#F2F2F2"))
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // ── Headers ─────────────────────────────────────────────
            var headers = new[] { "Display Name", "Username", "Role", "Sign-in Method", "Status", "Action" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cell(3, i + 1);
                cell.Value = headers[i];
                cell.Style
                    .Font.SetBold(true)
                    .Font.SetFontColor(XLColor.White)
                    .Font.SetFontSize(11)
                    .Fill.SetBackgroundColor(XLColor.FromHtml("#2F5496"))
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                    .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                    .Border.SetOutsideBorderColor(XLColor.White);
            }
            ws.Row(3).Height = 22;

            // ── Data rows ───────────────────────────────────────────
            for (int i = 0; i < users.Count; i++)
            {
                var user = users[i];
                var rowNum = i + 4;
                var isEven = i % 2 == 0;
                var rowBg = isEven
                    ? XLColor.FromHtml("#D9E1F2")   // light blue
                    : XLColor.FromHtml("#FFFFFF");   // white

                var values = new[] { user.Name, user.UserName, user.Role, user.SiginMethod, user.Status, user.Action };

                for (int col = 0; col < values.Length; col++)
                {
                    var cell = ws.Cell(rowNum, col + 1);
                    cell.Value = values[col];
                    cell.Style
                        .Fill.SetBackgroundColor(rowBg)
                        .Font.SetFontSize(10)
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                        .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                        .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                        .Border.SetOutsideBorderColor(XLColor.FromHtml("#BFC9DA"));

                    // color code Status column (col index 4)
                    if (col == 4)
                    {
                        var statusColor = user.Status?.ToLower() switch
                        {
                            "active" => XLColor.FromHtml("#E2EFDA"),  // green
                            "inactive" => XLColor.FromHtml("#FCE4D6"),  // red/orange
                            _ => rowBg
                        };
                        cell.Style.Fill.SetBackgroundColor(statusColor);
                    }

                    // color code Action column (col index 5)
                    if (col == 5)
                    {
                        var actionColor = user.Action?.ToLower() switch
                        {
                            "admin" => XLColor.FromHtml("#FFF2CC"),   // yellow
                            "edit" => XLColor.FromHtml("#DDEBF7"),   // blue
                            "delete" => XLColor.FromHtml("#FCE4D6"),   // red
                            _ => rowBg
                        };
                        cell.Style.Fill.SetBackgroundColor(actionColor);
                    }
                }
                ws.Row(rowNum).Height = 20;
            }

            // ── Column widths ────────────────────────────────────────
            ws.Column(1).Width = 25; // Name
            ws.Column(2).Width = 20; // UserName
            ws.Column(3).Width = 15; // Role
            ws.Column(4).Width = 18; // SigninMethod
            ws.Column(5).Width = 15; // Status
            ws.Column(6).Width = 15; // Action

            // ── Freeze header rows ───────────────────────────────────
            ws.SheetView.FreezeRows(3);

            using var stream = new MemoryStream();
            wb.SaveAs(stream);
            return Task.FromResult(stream.ToArray());
        }
    }
}
