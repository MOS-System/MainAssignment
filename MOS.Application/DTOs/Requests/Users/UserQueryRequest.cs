using MOS.Domain.Enums;
using MOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Requests.Users
{
    // pagination, sort, search, filter
    public class UserQueryRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }           // "name", "email", "status"
        public string? SortDirection { get; set; }    // "asc", "desc"
        public string? Search { get; set; }           // searches name or email
        public UserStatus? StatusFilter { get; set; }
        public RoleType? RoleFilter { get; set; }
    }
}
