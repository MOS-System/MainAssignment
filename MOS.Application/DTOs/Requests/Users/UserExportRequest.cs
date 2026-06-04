

namespace MOS.Application.DTOs.Requests.Users
{
    public class UserExportRequest
    {
        //Display Name
        public string Name { get; set; } = string.Empty;
        //User Id
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string SiginMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
    }
}
