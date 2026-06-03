

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
            private const string ControllerEndpoint = ApiEndpoint + "/auths";
            public const string Login = ControllerEndpoint + "/login";
            public const string Register = ControllerEndpoint + "/register";
        }
        public static class AuditEnpoints
        {
            private const string ControllerEndpoint = ApiEndpoint + "/audits";
            // GET api/audit?search=john&page=1&pageSize=10
            public const string GetAuditLogs = ControllerEndpoint + "?search={search}&page={page}&pageSize={pageSize}";
         

        }

        public static class UserEnpoints
        {
            private const string ControllerEndpoint = ApiEndpoint + "/users";
            public const string FetchUsers = ControllerEndpoint + "/fetch";
            public const string CreateUser = ControllerEndpoint + "/create";
            public const string Batch = ControllerEndpoint + "/batch";
            public const string DeleteUserByBatch = ControllerEndpoint + "/batch";
            public const string GetUserById = ControllerEndpoint + "/{id}";
            public const string UpdateUserById = ControllerEndpoint + "/{id}";
            public const string DeleteUserById = ControllerEndpoint + "/{id}";
            public const string DeActiveUserById = ControllerEndpoint + "/{id}/deactive";
            public const string DeActiveUserByBatch = ControllerEndpoint + "/batch/deactive";
            public const string ReActiveUserById = ControllerEndpoint + "/{id}/reactive";
            public const string ReActiveUserByBatch = ControllerEndpoint + "/batch/reactive";
        }

    }
}
