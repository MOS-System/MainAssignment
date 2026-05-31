

namespace MOS.Api.EndPoints
{
    public static class Endpoints
    {
        static Endpoints()
        {
        }

        public const string RootEndPoint = "/api";
        public const string ApiVersion = "/v1";
        public const string ApiEndpoint = RootEndPoint + ApiVersion;

        public static class AuditEnpoints
        {
            public const string ControllerEnpoint = ApiEndpoint + "audit";
            // GET api/audit?search=john&page=1&pageSize=10
            public const string GetAuditLogs = ControllerEnpoint + "?search={search}&page={page}&pageSize={pageSize}";
         

        }
     
    }
}
