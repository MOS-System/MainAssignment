

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
            public const string VerifyMfaCode = ControllerEndpoint + "/verify-mfa";
            public const string Register = ControllerEndpoint + "/register";
            public const string Logout = ControllerEndpoint + "/logout";
            public const string MicrosoftLogin = ControllerEndpoint + "/microsoft/login";
            public const string GoogleLogin = ControllerEndpoint + "/google/login";
            public const string GoogleComplete = ControllerEndpoint + "/google/complete";
            public const string MicrosoftCallBack = ControllerEndpoint + "/microsoft/callback";
        }
        public static class AuditEnpoints
        {
            private const string ControllerEndpoint = ApiEndpoint + "/audits";
            // GET api/audit?search=john&page=1&pageSize=10
            public const string GetAuditLogs = ControllerEndpoint + "/fetch";


        }

        public static class EmailWhiteListEnpoints
        {
            private const string ControllerEndpoint = ApiEndpoint + "/email-whitelist";

            public const string Setting = ControllerEndpoint + "/setting";
            public const string GetEmailWhiteList = ControllerEndpoint + "/";
            public const string AddEmailWhiteList = ControllerEndpoint + "/";
            public const string RemoveEmailWhiteList = ControllerEndpoint + "/{id:guid}";


        }

        public static class ProductEnpoints
        {
            private const string ControllerEndpoint = ApiEndpoint + "/products";
            public const string GetAllProducts = ControllerEndpoint + "/";
            public const string AddFavorites = ControllerEndpoint + "/favorites/{productId}";
            public const string RemoveFavorites = ControllerEndpoint + "/favorites/{productId}";
        }
        public static class TenantEnpoints
        {
            private const string ControllerEndpoint = ApiEndpoint + "/tenants";
            public const string GetAllTenantNames = ControllerEndpoint + "/names";
            public const string GetTenantById = ControllerEndpoint + "/{id}";
            public const string CreateTenant = ControllerEndpoint + "/";
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
            public const string ImportUsers = ControllerEndpoint + "/import";
            public const string ExportUsers = ControllerEndpoint + "/export";
        }

    }
}
