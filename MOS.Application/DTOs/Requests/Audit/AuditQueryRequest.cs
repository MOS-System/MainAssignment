

namespace MOS.Application.DTOs.Requests.Audit
{
    // search by object, name, userId + pagination
    public class AuditQueryRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; } 
    }
}
