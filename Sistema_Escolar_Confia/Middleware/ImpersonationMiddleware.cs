using System.Security.Claims;

namespace Sistema_Escolar_Confia.Middleware
{
    public class ImpersonationMiddleware
    {
        private readonly RequestDelegate _next;

        public ImpersonationMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var impersonateHeader = context.Request.Headers["X-Impersonate-User"].FirstOrDefault();

            if (!string.IsNullOrEmpty(impersonateHeader) && context.User.Identity?.IsAuthenticated == true)
            {
                var esAdmin = context.User.HasClaim("realm_access", "admin")
                    || context.User.IsInRole("admin");

                if (context.User.Identity.IsAuthenticated)
                {
                    var identity = context.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        var subClaim = identity.FindFirst("sub");
                        if (subClaim != null)
                        {
                            identity.RemoveClaim(subClaim);
                            identity.AddClaim(new Claim("sub", impersonateHeader));
                        }
                    }
                }
            }

            await _next(context);
        }
    }
}
