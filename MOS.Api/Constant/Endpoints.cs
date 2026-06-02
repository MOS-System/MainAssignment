

namespace MOS.Api.EndPoints
{
    public static class Endpoints
    {
        static Endpoints()
        {
        }

        private const string RootEndPoint = "/api";
        private const string ApiVersion = "/v1";
        public const string ApiEndpoint = RootEndPoint + ApiVersion;

        public static class AuthEnpoints
        {
            private const string ControllerEnpoint = ApiEndpoint + "/auth";
            public const string Login = ControllerEnpoint + "/login";
        }
        public static class AuditEnpoints
        {
            private const string ControllerEnpoint = ApiEndpoint + "/audit";
            // GET api/audit?search=john&page=1&pageSize=10
            public const string GetAuditLogs = ControllerEnpoint + "?search={search}&page={page}&pageSize={pageSize}";
         

        }
     
    }
}
