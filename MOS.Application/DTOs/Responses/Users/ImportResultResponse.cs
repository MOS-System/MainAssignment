using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Responses.Users
{
    public class ImportResultResponse
    {
        public int TotalRows { get; set; }
        public int SuccessRows { get; set; }
        public int FailedRows { get; set; }
        public List<string> ErrorLogs { get; set; } = new List<string>();
    }
}
